using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BusinessProcess.EMR;
using System.Configuration;
using Interface.EMR;
using System.IO;
using System.Xml.Linq;
using System.Threading;
using Application.Common;

namespace IQCare.EMR
{
    public partial class frmEMRXMLCreation : Form
    {
        DataTable dtValues = new DataTable();

        DataSet data = new DataSet();

        public frmEMRXMLCreation()
        {
            InitializeComponent();
        }

        private void btnGenerateNDRXml_Click(object sender, EventArgs e)
        {
            try
            {
                string path = ConfigurationSettings.AppSettings["EMRXMLS"].ToString();
                DirectoryInfo fi = new DirectoryInfo(path);
                if (!fi.Exists)
                {
                    MessageBox.Show("NDR xml(s) path does not exits. Please change the NDR xml(s) path in config file.", "IQCare", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }


                btnGenerateXml.Enabled = false;
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

                CLogger.WriteLog(ELogLevel.ERROR, exd.ToString());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "IQCare", MessageBoxButtons.OK, MessageBoxIcon.Error);
                CLogger.WriteLog(ELogLevel.ERROR, ex.ToString());
            }
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
                        string facilityname = row["Facility-Satellite Name"].ToString();
                        XElement container = new XElement("Container",
                           new XElement(MessageHeader(messageStatusCode, facilityname)),
                           new XElement(SubjectDemography(dtValues, row)),
                           new XElement(SubjectClinicalInformation(dtValues, row)),
                           new XElement(SubjectARTRegimenChangeHistory(dtValues, row)),
                           new XElement(SubjectARTInterruptions(dtValues, row)),
                           new XElement(SubjectDFollowUpStatus(dtValues, row)),
                           new XElement(SubjectCounsellingAndSupportServices(dtValues, row)),
                           new XElement(SubjectRegimenPrescriptionHistory(dtValues, row)),
                           new XElement(SubjectLabResultHistory(dtValues, row)));

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
                btnGenerateXml.Enabled = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "IQCare.NDR.GenerateXML", MessageBoxButtons.OK, MessageBoxIcon.Error);
                CLogger.WriteLog(ELogLevel.ERROR, ex.ToString());
            }
        }

        private void GenerateXMlFile(XElement container, int patientid)
        {
            try
            {
                string fileName = "CCFN_" + ConfigurationSettings.AppSettings["AppLocationId"].ToString() + "_" + DateTime.Now.ToString("ddMMyyyy") + "_" + DateTime.Now.ToString("HHmmss") + ".xml";
                string path = ConfigurationSettings.AppSettings["EMRXMLS"].ToString();
                string xsdPath = ConfigurationSettings.AppSettings["XSDPATH"].ToString();
                container.Save(@path + fileName);
                //bool errors = IsValidXml(@path + fileName, @xsdPath);

                //if (!errors)
                //{
                //FileInfo fi = new FileInfo(@path + fileName);
                //fi.Delete();

                //fileName = "CCFN_" + ConfigurationSettings.AppSettings["AppLocationId"].ToString() + "_" + DateTime.Now.ToString("ddMMyyyy") + "_" + DateTime.Now.ToString("HHmmss") + "_ERROR.xml";
                //container.Save(@path + fileName);

                //}
                // else
                //{
                //INDRGeneration ndrGeneration;
                //DataSet ds = new DataSet();
                //ndrGeneration = new BNDRGeneration();
                //ndrGeneration.SavePatientDetails(patientid);
                //}

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "IQCare.NDR.GenerateXMlFile", MessageBoxButtons.OK, MessageBoxIcon.Error);
                CLogger.WriteLog(ELogLevel.ERROR, ex.ToString());
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

                    bool containsColumn = dt.AsEnumerable().Where(o => o.Field<String>("Values").ToUpper() == code.ToUpper()
                         && o.Field<String>("description").Contains(value.Trim())).Any();

                    if (containsColumn)
                    {
                        DataRow query =
                            (from concept in dt.AsEnumerable()
                             where concept.Field<String>("Values").ToUpper() == code.ToUpper()
                             && concept.Field<String>("description").Contains(value.Trim())
                             select concept).FirstOrDefault();

                        result = query.Field<string>("Code");
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "IQCare.NDR.GetConceptValue", MessageBoxButtons.OK, MessageBoxIcon.Error);
                CLogger.WriteLog(ELogLevel.ERROR, ex.ToString());
            }
            return result;
        }

        private XElement MessageHeader(string messageStatusCode, string facilityname)
        {
            DateTime dt = new DateTime();
            dt = DateTime.Now;
            Guid g = Guid.NewGuid();
            string strHostName = System.Net.Dns.GetHostName();
            XElement header =
            new XElement("MessageHeader",
                new XElement("MessageStatusCode", messageStatusCode),
                new XElement("MessageCreationDateTime", dt.ToString("yyyy-MM-ddTHH:mm:ss.ff")),
                new XElement("MessageSendingOrganization",
                    new XElement("IPName", "CCFN"),
                    new XElement("FacilityName", facilityname)
                    )
            );
            return header;
        }

        private XElement SubjectDemography(DataTable dtValues, DataRow drow)
        {
            XElement subjectdemography = new XElement("SubjectDemography");
            try
            {
                int patientId = Convert.ToInt32(drow["Ptn_Pk"].ToString());

                DataTable commonq1 = new DataTable();
                commonq1 = (DataTable)data.Tables[2];
                IEnumerable<DataRow> filterdata1 =
                    (from fdata in commonq1.AsEnumerable()
                     where fdata.Field<Int32>("Ptn_Pk") == patientId
                     select fdata);

                string sex = drow["Sex"].ToString().ToUpper() == "MALE" ? "M" : "F";
                DateTime DOB = DateTime.Parse(drow["DateofBirth"].ToString());
                subjectdemography.Add(new XElement("HospitalNumber", drow["LocationID"].ToString()),
                    new XElement("ARTEnrollmentNumber", drow["PatientEnrollment"].ToString()),
                    new XElement("Sex", sex),
                     new XElement("SubjectBirthDate", DOB.ToString("yyyy-MM-dd"))
                    );

                if (!DBNull.Value.Equals(drow["PatientAddress"]))
                {
                    subjectdemography.Add(new XElement("ResidenceAtEnrollment", drow["PatientAddress"].ToString()));
                }

                string enrolledinhivcaredate = string.Empty; ;
                string AgeatEnrollment = string.Empty; ;
                string ARTTransferInDate = string.Empty; ;
                string ARTStartDate = string.Empty;
                string pregnancystatus = string.Empty;
                string initialregimenname = string.Empty;

                if (!DBNull.Value.Equals(drow["AgeatEnrollment"]))
                {
                    AgeatEnrollment = drow["AgeatEnrollment"].ToString();
                }

                foreach (DataRow row in filterdata1)
                {
                    if (!DBNull.Value.Equals(row["EnrolledInHIVCareDate"]))
                    {
                        enrolledinhivcaredate = DateTime.Parse(row["EnrolledInHIVCareDate"].ToString()).ToString("yyyy-MM-dd");
                    }

                    if (!DBNull.Value.Equals(row["ARTTransferInDate"]))
                    {
                        ARTTransferInDate = DateTime.Parse(row["ARTTransferInDate"].ToString()).ToString("yyyy-MM-dd");
                    }

                    if (!DBNull.Value.Equals(row["ARTStartDate"]))
                    {
                        ARTStartDate = DateTime.Parse(row["ARTStartDate"].ToString()).ToString("yyyy-MM-dd");
                    }
                    if (!DBNull.Value.Equals(row["PregnancyStatus"]))
                    {
                        pregnancystatus = row["PregnancyStatus"].ToString();
                    }
                    if (!DBNull.Value.Equals(row["InitialRegimenName"]))
                    {
                        initialregimenname = row["InitialRegimenName"].ToString();
                    }
                }

                if (!String.IsNullOrEmpty(enrolledinhivcaredate))
                {
                    subjectdemography.Add(new XElement("ARTEnrollmentDate", enrolledinhivcaredate));
                }

                if (!String.IsNullOrEmpty(AgeatEnrollment))
                {
                    subjectdemography.Add(new XElement("AgeAtARTEnrollment", AgeatEnrollment.ToString()));
                }

                if (!String.IsNullOrEmpty(ARTTransferInDate))
                {
                    subjectdemography.Add(new XElement("ARTTransferDate", ARTTransferInDate.ToString()));
                }

                if (!String.IsNullOrEmpty(ARTStartDate))
                {
                    subjectdemography.Add(new XElement("ARTInitiationDate", ARTStartDate.ToString()));
                }

                string maritalstatus = string.Empty;
                if (!DBNull.Value.Equals(drow["MaritalStatus"]))
                {
                    maritalstatus = GetConceptValue(dtValues, "MARITAL STATUS", drow["MaritalStatus"].ToString());
                    subjectdemography.Add(new XElement("MaritalStatus", maritalstatus));
                }

                string partnerhivstatus = string.Empty;
                if (!DBNull.Value.Equals(drow["PatientStatus"]))
                {
                    string ps = drow["PatientStatus"].ToString();
                    switch (ps)
                    {
                        case "Active":
                            partnerhivstatus = "Pos";
                            break;
                        case "In-Active":
                            partnerhivstatus = "Neg";
                            break;
                        default:
                            partnerhivstatus = "U";
                            break;
                    }
                    subjectdemography.Add(new XElement("PartnerHIVStatus", partnerhivstatus));
                }
                string patienteducationallevel = string.Empty;
                if (!DBNull.Value.Equals(drow["EducationLevel"]))
                {
                    patienteducationallevel = GetConceptValue(dtValues, "MARITAL STATUS", drow["EducationLevel"].ToString());
                    subjectdemography.Add(new XElement("PatientEducationalLevel", maritalstatus));
                }

                string otherpatienteducationallevel = string.Empty;
                if (!DBNull.Value.Equals(drow["EducationOther"]))
                {
                    subjectdemography.Add(new XElement("OtherPatientEducationalLevel", drow["EducationOther"].ToString()));
                }

                if (!String.IsNullOrEmpty(pregnancystatus))
                {
                    if (sex == "M")
                    {
                        subjectdemography.Add(new XElement("PatientPregnancyStatus", "NA"));
                    }
                    else
                    {
                        subjectdemography.Add(new XElement("PatientPregnancyStatus", pregnancystatus));
                    }

                }

                if (!DBNull.Value.Equals(drow["TransferIn"]))
                {
                    string transferin = drow["TransferIn"].ToString() == "1" ? "Y" : "N";
                    subjectdemography.Add(new XElement("ARTTransferIn", transferin));
                }

                if (!String.IsNullOrEmpty(initialregimenname))
                {
                    subjectdemography.Add(new XElement("ARTRegimenName", initialregimenname.ToString()));
                }
                subjectdemography.Add(new XElement("ARTRegimenNameOthers", ""));

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "IQCare.EMR.SubjectDemography", MessageBoxButtons.OK, MessageBoxIcon.Error);
                CLogger.WriteLog(ELogLevel.ERROR, ex.ToString());
            }
            return subjectdemography;
        }

        private XElement SubjectClinicalInformation(DataTable dtValues, DataRow drow)
        {
            XElement subjectclinicalinformation = new XElement("SubjectClinicalInformation");
            try
            {
                int patientId = Convert.ToInt32(drow["Ptn_Pk"].ToString());

                DataTable commonq1 = new DataTable();
                commonq1 = (DataTable)data.Tables[2];
                IEnumerable<DataRow> filterdata1 =
                    (from fdata in commonq1.AsEnumerable()
                     where fdata.Field<Int32>("Ptn_Pk") == patientId
                     select fdata);

                string hivconfirmeddate = string.Empty;
                string WnH = string.Empty;
                string eligibledate = string.Empty;
                string artstartdate = string.Empty;
                string hivpositivetest = "N";
                string whyeligible = string.Empty;
                string CD4 = string.Empty;
                string functionalstatus = string.Empty;
                string whoclinicalstage = string.Empty;
                string tbstatus = string.Empty;
                string inhprophylaxis = string.Empty;
                string cotrimoxazoleonlastvisit = string.Empty;

                foreach (DataRow row in filterdata1)
                {
                    if (!DBNull.Value.Equals(row["ConfirmHIVPosDate"]))
                    {
                        hivconfirmeddate = DateTime.Parse(row["ConfirmHIVPosDate"].ToString()).ToString("yyyy-MM-dd");
                        hivpositivetest = "Y";
                    }
                    if (!DBNull.Value.Equals(row["WnH"]))
                    {
                        WnH = row["WnH"].ToString();
                    }

                    if (!DBNull.Value.Equals(row["EligibleDate"]))
                    {
                        eligibledate = DateTime.Parse(row["EligibleDate"].ToString()).ToString("yyyy-MM-dd");
                    }

                    if (!DBNull.Value.Equals(row["ARTStartDate"]))
                    {
                        artstartdate = DateTime.Parse(row["ARTStartDate"].ToString()).ToString("yyyy-MM-dd");
                    }

                    if (!DBNull.Value.Equals(row["WhyEligible"]))
                    {
                        whyeligible = row["WhyEligible"].ToString();
                    }
                    if (!DBNull.Value.Equals(row["CD4Details"]))
                    {
                        CD4 = row["CD4Details"].ToString();
                    }
                    if (!DBNull.Value.Equals(row["FunctionalStatus"]))
                    {
                        functionalstatus = row["FunctionalStatus"].ToString();
                    }
                    if (!DBNull.Value.Equals(row["WHOClinicalStage"]))
                    {
                        whoclinicalstage = row["WHOClinicalStage"].ToString();
                    }

                    if (!DBNull.Value.Equals(row["TBStatus"]))
                    {
                        tbstatus = row["TBStatus"].ToString();
                    }

                    if (!DBNull.Value.Equals(row["INHProphylaxis"]))
                    {
                        inhprophylaxis = row["INHProphylaxis"].ToString();
                    }

                    if (!DBNull.Value.Equals(row["CotrimoxazoleDose"]))
                    {
                        cotrimoxazoleonlastvisit = row["INHProphylaxis"].ToString();
                    }
                }

                if (!String.IsNullOrEmpty(hivconfirmeddate))
                {
                    subjectclinicalinformation.Add(new XElement("HivConfirmedDate", hivconfirmeddate));
                }

                if (!String.IsNullOrEmpty(WnH))
                {
                    string[] wnh = WnH.ToString().Split(';');
                    subjectclinicalinformation.Add(new XElement("PatientHeight", wnh[0].ToString()));
                    subjectclinicalinformation.Add(new XElement("PatientWeight", wnh[1].ToString()));
                }

                if (!String.IsNullOrEmpty(eligibledate))
                {
                    subjectclinicalinformation.Add(new XElement("ARTEligibilityDate", eligibledate));
                }

                if (!String.IsNullOrEmpty(artstartdate))
                {
                    subjectclinicalinformation.Add(new XElement("ARTStartDate", artstartdate));
                }

                XElement arteligibilitycriteria =
                    new XElement("ARTEligibilityCriteria",
                        new XElement("Clinicallyonly", "NULL"),
                        new XElement("CD4Count", "Null"),
                        new XElement("HIVPositiveTest", hivpositivetest)
                     );

                subjectclinicalinformation.Add(arteligibilitycriteria);

                if (!String.IsNullOrEmpty(whyeligible))
                {
                    subjectclinicalinformation.Add(new XElement("OtherARTEligibilityCriteria", whyeligible));
                }

                if (!String.IsNullOrEmpty(CD4))
                {
                    string[] cd4 = CD4.ToString().Split(';');
                    subjectclinicalinformation.Add(new XElement("CD4CellCount", cd4[0].ToString()));

                    DateTime cd4testdate = DateTime.Parse(cd4[1].ToString());
                    subjectclinicalinformation.Add(new XElement("CD4TestDate", cd4testdate.ToString("yyyy-MM-dd")));
                }

                if (!String.IsNullOrEmpty(functionalstatus))
                {
                    subjectclinicalinformation.Add(new XElement("FunctionalStatus", functionalstatus));
                }

                if (!String.IsNullOrEmpty(whoclinicalstage))
                {
                    subjectclinicalinformation.Add(new XElement("ARTClinicalStage", whoclinicalstage));
                }

                if (!String.IsNullOrEmpty(tbstatus))
                {
                    subjectclinicalinformation.Add(new XElement("TBStatus", tbstatus));
                }

                if (!String.IsNullOrEmpty(inhprophylaxis))
                {
                    subjectclinicalinformation.Add(new XElement("INHProphylaxis", inhprophylaxis));
                }

                subjectclinicalinformation.Add(new XElement("SexuallyTransmittedInfections", "0"));
                subjectclinicalinformation.Add(new XElement("OtherSexuallyTransmittedInfections", ""));

                subjectclinicalinformation.Add(new XElement("ChronicIllness", "N"));
                subjectclinicalinformation.Add(new XElement("OtherChronicIllness", ""));

                if (!String.IsNullOrEmpty(cotrimoxazoleonlastvisit))
                {
                    string[] cotrimoxazoledose = cotrimoxazoleonlastvisit.ToString().Split(';');
                    subjectclinicalinformation.Add(new XElement("Cotrimoxazole", "Y"));
                    subjectclinicalinformation.Add(new XElement("CotrimoxazoleOnLastVisit", "N"));
                }
                else
                {
                    subjectclinicalinformation.Add(new XElement("Cotrimoxazole", "Y"));
                    subjectclinicalinformation.Add(new XElement("CotrimoxazoleOnLastVisit", "N"));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "IQCare.EMR.SubjectClinicalInformation", MessageBoxButtons.OK, MessageBoxIcon.Error);
                CLogger.WriteLog(ELogLevel.ERROR, ex.ToString());
            }
            return subjectclinicalinformation;
        }

        private XElement SubjectARTRegimenChangeHistory(DataTable dtValues, DataRow drow)
        {
            XElement subjectartregimenchangehistory = new XElement("SubjectARTRegimenChangeHistory");
            try
            {
                int patientId = Convert.ToInt32(drow["Ptn_Pk"].ToString());
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

                subjectartregimenchangehistory.Add(new XElement("NoRegimenChanges", "Y"));

                string doc = string.Empty;

                foreach (DataRow rowdata in filterdata.OrderBy(o => o.Field<DateTime>("VisitDate")).ToList())
                {
                    XElement artregimen = new XElement("ARTRegimen");

                    DateTime VD = DateTime.Parse(rowdata["VisitDate"].ToString());
                    artregimen.Add(new XElement("DateOfChange", VD.ToString("yyyy-MM-dd")));
                    if (!DBNull.Value.Equals(rowdata["ReasonForRegimenSwitchSubs"]))
                    {
                        artregimen.Add(new XElement("ReasonForChange", rowdata["ReasonForRegimenSwitchSubs"].ToString()));
                    }

                    if (!DBNull.Value.Equals(rowdata["PrescribedRegimen"]))
                    {
                        string[] arv = rowdata["PrescribedRegimen"].ToString().Split(';');

                        artregimen.Add(new XElement("OldRegimen", arv[0].ToString()));

                        //artregimen.Add(new XElement("PrescribedRegimen",
                        //   new XElement("Code", arv[0].ToString()),
                        //   new XElement("CodeDescTxt", arv[1].ToString())
                        //));

                    }

                    if (!DBNull.Value.Equals(rowdata["PrescribedRegimenNext"]))
                    {
                        string[] arv = rowdata["PrescribedRegimenNext"].ToString().Split(';');

                        artregimen.Add(new XElement("NewRegimen", arv[0].ToString()));
                    }

                    subjectartregimenchangehistory.Add(artregimen);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "IQCare.EMR.SubjectARTRegimenChangeHistory", MessageBoxButtons.OK, MessageBoxIcon.Error);
                CLogger.WriteLog(ELogLevel.ERROR, ex.ToString());
            }
            return subjectartregimenchangehistory;
        }

        private XElement SubjectARTInterruptions(DataTable dtValues, DataRow drow)
        {
            XElement subjectartinterruptions = new XElement("SubjectARTInterruptions");
            try
            {
                int patientId = Convert.ToInt32(drow["Ptn_Pk"].ToString());
                DataTable encounter = new DataTable();
                encounter = (DataTable)data.Tables[4];
                IEnumerable<DataRow> filterdata =
                    (from fdata in encounter.AsEnumerable()
                     where fdata.Field<Int32>("PatientID") == patientId
                     select fdata);

                bool rowCount = encounter.AsEnumerable().Where(o => o.Field<Int32>("PatientID") == patientId).Any();

                XElement artinterruption = new XElement("ARTInterruption");

                if (rowCount)
                {
                    string artstophistory = string.Empty;
                    string patientstoppedartfirsttime = "No";
                    string firststopdate = string.Empty;
                    string patientrestartedart = "No";
                    string firstrestartdate = string.Empty;
                    string patientstoppedartsecondtime = "No";
                    string secondstopdate = string.Empty;
                    string patientrestartedartsecondtime = "No";
                    string secondrestartdate = string.Empty;
                    string patientstoppedartthirdtime = "No";
                    string thirdstopdate = string.Empty;
                    string patientrestartedartthirdtime = "No";
                    string thirdrestartdate = string.Empty;


                    foreach (DataRow rowdata in filterdata)
                    {
                        artstophistory = "Yes";
                        if (!DBNull.Value.Equals(rowdata["_2ndRegimen"]))
                        {
                            patientstoppedartfirsttime = "Yes";
                            patientrestartedart = "Yes";
                        }
                        if (!DBNull.Value.Equals(rowdata["_2ndRegimenChangeDate"]))
                        {
                            firststopdate = DateTime.Parse(rowdata["_2ndRegimenChangeDate"].ToString()).ToString("yyyy-MM-dd");
                            firstrestartdate = DateTime.Parse(rowdata["_2ndRegimenChangeDate"].ToString()).ToString("yyyy-MM-dd");
                        }

                        if (!DBNull.Value.Equals(rowdata["_3rdRegimen"]))
                        {
                            patientstoppedartsecondtime = "Yes";
                            patientrestartedartsecondtime = "Yes";
                        }
                        if (!DBNull.Value.Equals(rowdata["_3rdRegimenChangeDate"]))
                        {
                            secondstopdate = DateTime.Parse(rowdata["_3rdRegimenChangeDate"].ToString()).ToString("yyyy-MM-dd");
                            secondrestartdate = DateTime.Parse(rowdata["_3rdRegimenChangeDate"].ToString()).ToString("yyyy-MM-dd");
                        }

                        if (!DBNull.Value.Equals(rowdata["_3rdRegimen"]))
                        {
                            patientstoppedartthirdtime = "Yes";
                            patientrestartedartthirdtime = "Yes";
                        }
                        if (!DBNull.Value.Equals(rowdata["_3rdRegimenChangeDate"]))
                        {
                            thirdstopdate = DateTime.Parse(rowdata["_3rdRegimenChangeDate"].ToString()).ToString("yyyy-MM-dd");
                            thirdrestartdate = DateTime.Parse(rowdata["_3rdRegimenChangeDate"].ToString()).ToString("yyyy-MM-dd");
                        }
                    }

                    artinterruption.Add(new XElement("ARTStopHistory", artstophistory.ToString()));

                    artinterruption.Add(new XElement("PatientStoppedARTFirstTime", patientstoppedartfirsttime.ToString()));
                    artinterruption.Add(new XElement("FirstStopDate", firststopdate.ToString()));
                    artinterruption.Add(new XElement("PatientRestartedART", patientrestartedart.ToString()));
                    artinterruption.Add(new XElement("FirstRestartDate", firstrestartdate.ToString()));

                    artinterruption.Add(new XElement("PatientStoppedARTSecondTime", patientstoppedartsecondtime.ToString()));
                    artinterruption.Add(new XElement("SecondStopDate", secondstopdate.ToString()));
                    artinterruption.Add(new XElement("PatientRestartedARTSecondTime", patientrestartedartsecondtime.ToString()));
                    artinterruption.Add(new XElement("SecondRestartDate", secondrestartdate.ToString()));

                    artinterruption.Add(new XElement("PatientStoppedARTThirdTime", patientstoppedartthirdtime.ToString()));
                    artinterruption.Add(new XElement("ThirdStopDate", thirdstopdate.ToString()));
                    artinterruption.Add(new XElement("PatientRestartedARTThirdTime", patientrestartedartthirdtime.ToString()));
                    artinterruption.Add(new XElement("ThirdReStartDate", thirdrestartdate.ToString()));

                    subjectartinterruptions.Add(artinterruption);

                    encounter = new DataTable();
                    encounter = (DataTable)data.Tables[5];
                    IEnumerable<DataRow> filterdata1 =
                    (from fdata in encounter.AsEnumerable()
                     where fdata.Field<Int32>("ptn_pk") == patientId
                     select fdata);

                    XElement artstop = new XElement("ARTStop");
                    bool rowCount1 = encounter.AsEnumerable().Where(o => o.Field<Int32>("ptn_pk") == patientId).Any();

                    if (rowCount1)
                    {
                        foreach (DataRow rowdata in filterdata1)
                        {
                            if (!DBNull.Value.Equals(rowdata["DevelopedActiveTB"]))
                            {
                                string tb = rowdata["DevelopedActiveTB"].ToString();
                                if (tb.Contains(";"))
                                {
                                    string[] arv = rowdata["DevelopedActiveTB"].ToString().Split(';');
                                    artstop.Add(new XElement("DevelopedActiveTB", arv[0].ToString()));
                                    artstop.Add(new XElement("DevelopedActiveTBDate", arv[1].ToString()));
                                }
                                else
                                {
                                    artstop.Add(new XElement("DevelopedActiveTB", rowdata["DevelopedActiveTB"].ToString()));
                                    artstop.Add(new XElement("DevelopedActiveTBDate", ""));
                                }
                            }

                            if (!DBNull.Value.Equals(rowdata["Toxicity"]))
                            {
                                string toxicity = rowdata["Toxicity"].ToString();
                                if (toxicity.Contains(";"))
                                {
                                    string[] arv1 = rowdata["Toxicity"].ToString().Split(';');
                                    artstop.Add(new XElement("DrugToxicity", arv1[0].ToString()));
                                    artstop.Add(new XElement("DrugToxicityDate", arv1[1].ToString()));
                                }
                                else
                                {
                                    artstop.Add(new XElement("DrugToxicity", rowdata["Toxicity"].ToString()));
                                    artstop.Add(new XElement("DrugToxicityDate", ""));
                                }
                            }
                        }

                        artstop.Add(new XElement("IRIS", "No"));

                        artstop.Add(new XElement("IRISDate", ""));
                        artstop.Add(new XElement("OtherSpecify", ""));
                        artstop.Add(new XElement("UnknownReason", "No"));

                        subjectartinterruptions.Add(artstop);

                        subjectartinterruptions.Add(new XElement("OtherStopReasonSpecify", ""));
                        subjectartinterruptions.Add(new XElement("AdverseEvents", ""));

                        XElement documentedadverseevents =
                                      new XElement("DocumentedAdverseEvents",
                                          new XElement("Anemia", "NUll"),
                                          new XElement("Hepatitis", "NUll"),
                                          new XElement("Neuropathy", "NUll"),
                                          new XElement("SevereRash", "NUll"),
                                          new XElement("IRIS", "NUll"),
                                          new XElement("RenalImpairment", "NUll"),
                                           new XElement("OthersSpecify", "NUll")
                                          );
                        subjectartinterruptions.Add(documentedadverseevents);
                        subjectartinterruptions.Add(new XElement("OtherAdverseEventsSpecify", ""));

                    }

                }
                else
                {
                    artinterruption.Add(new XElement("ARTStopHistory", "N"));
                }


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "IQCare.EMR.SubjectARTInterruptions", MessageBoxButtons.OK, MessageBoxIcon.Error);
                CLogger.WriteLog(ELogLevel.ERROR, ex.ToString());
            }
            return subjectartinterruptions;
        }


        private XElement SubjectDFollowUpStatus(DataTable dtValues, DataRow drow)
        {
            XElement subjectdfollowupstatus = new XElement("SubjectDFollowUpStatus");
            try
            {
                int patientId = Convert.ToInt32(drow["Ptn_Pk"].ToString());
                DataTable encounter = new DataTable();
                encounter = (DataTable)data.Tables[6];
                IEnumerable<DataRow> filterdata =
                    (from fdata in encounter.AsEnumerable()
                     where fdata.Field<Int32>("ptn_pk") == patientId
                     select fdata);

                foreach (DataRow rowdata in filterdata.ToList())
                {
                    XElement patientscheduledvisits = new XElement("PatientScheduledVisits");
                    if (!DBNull.Value.Equals(rowdata["visitdate"]))
                    {
                        patientscheduledvisits.Add(new XElement("ScheduledDate", rowdata["visitdate"].ToString()));
                    }
                    else
                    {
                        patientscheduledvisits.Add(new XElement("ScheduledDate", ""));
                    }

                    if (!DBNull.Value.Equals(rowdata["appdate"]))
                    {
                        patientscheduledvisits.Add(new XElement("VisitDate", rowdata["appdate"].ToString()));
                    }
                    else
                    {
                        patientscheduledvisits.Add(new XElement("VisitDate", ""));
                    }

                    patientscheduledvisits.Add(new XElement("Weight", rowdata["Weight"].ToString()));

                    string status = string.Empty;
                    string statusOther = string.Empty;

                    if (!DBNull.Value.Equals(rowdata["Outcomeoftracking"]))
                    {
                        status = GetConceptValue(dtValues, "FOLLOW UP STATUS", rowdata["Outcomeoftracking"].ToString());
                    }

                    if (string.IsNullOrEmpty(status))
                    {
                        status = GetConceptValue(dtValues, "FOLLOW UP STATUS", rowdata["Reason"].ToString());
                    }
                    if (string.IsNullOrEmpty(status))
                    {
                        status = "8";
                        statusOther = rowdata["Status"].ToString();
                    }

                    patientscheduledvisits.Add(new XElement("FollowUpStatus", status.ToString()));
                    patientscheduledvisits.Add(new XElement("FollowUpStatusOthers", statusOther.ToString()));
                }


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "IQCare.EMR.SubjectARTInterruptions", MessageBoxButtons.OK, MessageBoxIcon.Error);
                CLogger.WriteLog(ELogLevel.ERROR, ex.ToString());
            }
            return subjectdfollowupstatus;
        }

        private XElement SubjectCounsellingAndSupportServices(DataTable dtValues, DataRow drow)
        {
            XElement subjectcounsellingandsupportservices = new XElement("SubjectCounsellingAndSupportServices");
            try
            {
                int patientId = Convert.ToInt32(drow["Ptn_Pk"].ToString());
                DataTable encounter = new DataTable();
                encounter = (DataTable)data.Tables[7];
                IEnumerable<DataRow> filterdata =
                    (from fdata in encounter.AsEnumerable()
                     where fdata.Field<Int32>("ptn_pk") == patientId
                     select fdata);

                string preartcounselling = string.Empty;
                string numberofpreartcounselling = string.Empty;
                string artinitiationcounselling = string.Empty;
                string numberofartinitiationcounselling = string.Empty;
                string counsellingduringfollowup = string.Empty;
                string numberofcounsellingduringfollowup = string.Empty;
                string supportgroup = string.Empty;
                string supportgroupspecify = string.Empty;
                //--SupportServicesSinceARTInitiation
                string homebasedcare = string.Empty;
                string nutritionalsupport = string.Empty;
                string communitybasedsupportgroup = string.Empty;
                string ovc = string.Empty;
                string psychosocialservices = string.Empty;
                string notusinganysupportservices = string.Empty;
                string missing = string.Empty;
                string othersupportservices = string.Empty;
                string othersupportservicesspecify = string.Empty;

                string condomsuse = string.Empty;

                foreach (DataRow rowdata in filterdata.ToList())
                {
                    if (!DBNull.Value.Equals(rowdata["PreARTCounselling"]))
                    {
                        preartcounselling = rowdata["PreARTCounselling"].ToString();
                    }
                    else
                    {
                        preartcounselling = "N";
                    }
                    if (!DBNull.Value.Equals(rowdata["NumberofPreARTCounselling"]))
                    {
                        numberofpreartcounselling = rowdata["NumberofPreARTCounselling"].ToString();
                    }
                    else
                    {
                        numberofpreartcounselling = "N";
                    }
                    if (!DBNull.Value.Equals(rowdata["ARTInitiationCounselling"]))
                    {
                        artinitiationcounselling = rowdata["ARTInitiationCounselling"].ToString();
                    }
                    else
                    {
                        artinitiationcounselling = "N";
                    }
                    if (!DBNull.Value.Equals(rowdata["NumberofARTInitiationCounselling"]))
                    {
                        numberofartinitiationcounselling = rowdata["NumberofARTInitiationCounselling"].ToString();
                    }
                    else
                    {
                        numberofartinitiationcounselling = "N";
                    }
                    if (!DBNull.Value.Equals(rowdata["CounsellingDuringFollowUp"]))
                    {
                        counsellingduringfollowup = rowdata["CounsellingDuringFollowUp"].ToString();
                    }
                    else
                    {
                        counsellingduringfollowup = "N";
                    }
                    if (!DBNull.Value.Equals(rowdata["NumberofCounsellingDuringFollowUp"]))
                    {
                        numberofcounsellingduringfollowup = rowdata["NumberofCounsellingDuringFollowUp"].ToString();
                    }
                    else
                    {
                        numberofcounsellingduringfollowup = "N";
                    }
                    if (!DBNull.Value.Equals(rowdata["HomeBasedCare"]))
                    {
                        homebasedcare = rowdata["HomeBasedCare"].ToString();
                    }
                    else
                    {
                        homebasedcare = "N";
                    }

                    if (!DBNull.Value.Equals(rowdata["NutritionalSupport"]))
                    {
                        nutritionalsupport = rowdata["NutritionalSupport"].ToString();
                    }
                    else
                    {
                        nutritionalsupport = "N";
                    }

                    if (!DBNull.Value.Equals(rowdata["CommunityBasedSupportGroup"]))
                    {
                        communitybasedsupportgroup = rowdata["CommunityBasedSupportGroup"].ToString();
                    }
                    else
                    {
                        communitybasedsupportgroup = "N";
                    }

                    if (!DBNull.Value.Equals(rowdata["OVC"]))
                    {
                        ovc = rowdata["OVC"].ToString();
                    }
                    else
                    {
                        ovc = "N";
                    }

                    if (!DBNull.Value.Equals(rowdata["PsychosocialServices"]))
                    {
                        psychosocialservices = rowdata["PsychosocialServices"].ToString();
                    }
                    else
                    {
                        psychosocialservices = "N";
                    }

                    if (!DBNull.Value.Equals(rowdata["NotUsingAnySupportServices"]))
                    {
                        notusinganysupportservices = rowdata["NotUsingAnySupportServices"].ToString();
                    }
                    else
                    {
                        notusinganysupportservices = "N";
                    }

                    if (!DBNull.Value.Equals(rowdata["Missing"]))
                    {
                        missing = rowdata["Missing"].ToString();
                    }
                    else
                    {
                        missing = "N";
                    }

                    if (!DBNull.Value.Equals(rowdata["OtherSupportServices"]))
                    {
                        othersupportservices = rowdata["OtherSupportServices"].ToString();
                    }
                    else
                    {
                        othersupportservices = "N";
                    }

                    if (!DBNull.Value.Equals(rowdata["OtherSupportServicesSpecify"]))
                    {
                        othersupportservicesspecify = rowdata["OtherSupportServicesSpecify"].ToString();
                    }
                    else
                    {
                        othersupportservicesspecify = "N";
                    }

                    if (!DBNull.Value.Equals(rowdata["OtherSupportServices"]))
                    {
                        othersupportservices = rowdata["OtherSupportServices"].ToString();
                    }
                    else
                    {
                        othersupportservices = "N";
                    }

                    if (!DBNull.Value.Equals(rowdata["CondomsUse"]))
                    {
                        condomsuse = rowdata["CondomsUse"].ToString();
                    }
                    else
                    {
                        condomsuse = "N";
                    }

                    if (int.Parse(rowdata["Age"].ToString()) >= 12)
                    {
                        if (!DBNull.Value.Equals(rowdata["ASupportGroup"]))
                        {
                            supportgroup = rowdata["ASupportGroup"].ToString();
                        }
                        else
                        {
                            supportgroup = "N";
                        }

                        if (!DBNull.Value.Equals(rowdata["ASupportGroupM"]))
                        {
                            supportgroupspecify = rowdata["ASupportGroupM"].ToString();
                        }
                        else
                        {
                            supportgroupspecify = "";
                        }

                    }
                    else
                    {
                        if (!DBNull.Value.Equals(rowdata["PSupportGroup"]))
                        {
                            supportgroup = rowdata["PSupportGroup"].ToString();
                        }
                        else
                        {
                            supportgroup = "N";
                        }

                        if (!DBNull.Value.Equals(rowdata["PSupportGroupM"]))
                        {
                            supportgroupspecify = rowdata["PSupportGroupM"].ToString();
                        }
                        else
                        {
                            supportgroupspecify = "";
                        }
                    }
                }


                subjectcounsellingandsupportservices.Add(new XElement("PreARTCounselling", preartcounselling.ToString()));
                subjectcounsellingandsupportservices.Add(new XElement("NumberofPreARTCounselling", numberofpreartcounselling.ToString()));
                subjectcounsellingandsupportservices.Add(new XElement("ARTInitiationCounselling", artinitiationcounselling.ToString()));
                subjectcounsellingandsupportservices.Add(new XElement("NumberofARTInitiationCounselling", numberofartinitiationcounselling.ToString()));
                subjectcounsellingandsupportservices.Add(new XElement("CounsellingDuringFollowUp", counsellingduringfollowup.ToString()));
                subjectcounsellingandsupportservices.Add(new XElement("NumberofCounsellingDuringFollowUp", numberofcounsellingduringfollowup.ToString()));
                subjectcounsellingandsupportservices.Add(new XElement("SupportGroup", supportgroup.ToString()));
                subjectcounsellingandsupportservices.Add(new XElement("SupportGroupSpecify", supportgroupspecify.ToString()));

                XElement supportservicessinceartinitiation =
                                      new XElement("SupportServicesSinceARTInitiation",
                                          new XElement("HomeBasedCare", homebasedcare.ToString()),
                                          new XElement("NutritionalSupport", nutritionalsupport.ToString()),
                                          new XElement("CommunityBasedSupportGroup", communitybasedsupportgroup.ToString()),
                                          new XElement("OVC", ovc.ToString()),
                                          new XElement("PsychosocialServices", psychosocialservices.ToString()),
                                          new XElement("NotUsingAnySupportServices", notusinganysupportservices.ToString()),
                                           new XElement("Missing", missing.ToString()),
                                           new XElement("OtherSupportServices", othersupportservices.ToString()),
                                           new XElement("OtherSupportServicesSpecify", othersupportservicesspecify.ToString())
                                          );
                subjectcounsellingandsupportservices.Add(supportservicessinceartinitiation);

                subjectcounsellingandsupportservices.Add(new XElement("CondomsUse", condomsuse.ToString()));

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "IQCare.EMR.SubjectCounsellingAndSupportServices", MessageBoxButtons.OK, MessageBoxIcon.Error);
                CLogger.WriteLog(ELogLevel.ERROR, ex.ToString());
            }
            return subjectcounsellingandsupportservices;
        }

        private XElement SubjectRegimenPrescriptionHistory(DataTable dtValues, DataRow drow)
        {
            XElement subjectregimenprescriptionhistory = new XElement("SubjectRegimenPrescriptionHistory");
            try
            {
                int patientId = Convert.ToInt32(drow["Ptn_Pk"].ToString());
                DataTable encounter = new DataTable();
                encounter = (DataTable)data.Tables[8];
                IEnumerable<DataRow> filterdata =
                    (from fdata in encounter.AsEnumerable()
                     where fdata.Field<Int32>("ptn_pk") == patientId
                     select fdata);

                foreach (DataRow rowdata in filterdata.ToList())
                {
                    XElement collectedarvs =
                                     new XElement("CollectedARVs",
                                         new XElement("ARVRefillDate", DateTime.Parse(rowdata["DateRegimenStarted"].ToString()).ToString("yyyy-MM-dd")),
                                         new XElement("ARVRegimen", rowdata["PrescribedRegimen"].ToString()),
                                         new XElement("NoOfDaysOfPrescription", rowdata["NoOfDaysOfPrescription"].ToString()));

                    subjectregimenprescriptionhistory.Add(collectedarvs);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "IQCare.EMR.SubjectRegimenPrescriptionHistory", MessageBoxButtons.OK, MessageBoxIcon.Error);
                CLogger.WriteLog(ELogLevel.ERROR, ex.ToString());
            }
            return subjectregimenprescriptionhistory;
        }

        private XElement SubjectLabResultHistory(DataTable dtValues, DataRow drow)
        {
            XElement subjectlabresulthistory = new XElement("SubjectLabResultHistory");
            try
            {
                int patientId = Convert.ToInt32(drow["Ptn_Pk"].ToString());
                DataTable encounter = new DataTable();
                encounter = (DataTable)data.Tables[9];
                IEnumerable<DataRow> filterdata =
                    (from fdata in encounter.AsEnumerable()
                     where fdata.Field<Int32>("ptn_pk") == patientId
                     select fdata);


                IEnumerable<DataRow> CD4Results =
                                (from fdata in filterdata.AsEnumerable()
                                 where fdata.Field<String>("testname") == "CD4"
                                 select fdata
                                 ).ToList();

                string vDate = string.Empty;
                XElement CD4Result = new XElement("CD4Results");
                foreach (DataRow rowdata in CD4Results.ToList())
                {
                    if (!DBNull.Value.Equals(rowdata["VisitDate"]))
                    {
                        vDate = DateTime.Parse(rowdata["VisitDate"].ToString()).ToString("yyyy-MM-dd");
                    }

                    XElement CD4 =
                                     new XElement("CD4",
                                         new XElement("CD4CellCount", rowdata["TestResults"].ToString()),
                                         new XElement("CD4CellCount", vDate));
                    CD4Result.Add(CD4);
                }
                subjectlabresulthistory.Add(CD4Result);

                IEnumerable<DataRow> ViralLoads =
                                (from fdata in filterdata.AsEnumerable()
                                 where fdata.Field<String>("testname") == "Viral Load"
                                 select fdata
                                 ).ToList();

                XElement ViralLoadResults = new XElement("ViralLoadResults");
                foreach (DataRow rowdata in ViralLoads.ToList())
                {
                    if (!DBNull.Value.Equals(rowdata["VisitDate"]))
                    {
                        vDate = DateTime.Parse(rowdata["VisitDate"].ToString()).ToString("yyyy-MM-dd");
                    }
                    XElement ViralLoad =
                                     new XElement("ViralLoad",
                                         new XElement("ViralLoad", rowdata["TestResults"].ToString()),
                                         new XElement("ViralLoadTestDate", vDate));
                    ViralLoadResults.Add(ViralLoad);
                }
                subjectlabresulthistory.Add(ViralLoadResults);

                IEnumerable<DataRow> Hbs =
                               (from fdata in filterdata.AsEnumerable()
                                where fdata.Field<String>("testname") == "Hb"
                                select fdata
                                ).ToList();

                XElement HbResults = new XElement("HbResults");
                foreach (DataRow rowdata in Hbs.ToList())
                {
                    if (!DBNull.Value.Equals(rowdata["VisitDate"]))
                    {
                        vDate = DateTime.Parse(rowdata["VisitDate"].ToString()).ToString("yyyy-MM-dd");
                    }

                    XElement Hb =
                                     new XElement("Hb",
                                         new XElement("Hemoglobin", rowdata["TestResults"].ToString()),
                                         new XElement("HemoglobinTestDate", vDate));
                    HbResults.Add(Hb);
                }
                subjectlabresulthistory.Add(HbResults);

                IEnumerable<DataRow> ALTResult =
                               (from fdata in filterdata.AsEnumerable()
                                where fdata.Field<String>("testname").Contains("ALT")
                                select fdata
                                ).ToList();

                XElement ALTResults = new XElement("ALTResults");
                foreach (DataRow rowdata in ALTResult.ToList())
                {
                    if (!DBNull.Value.Equals(rowdata["VisitDate"]))
                    {
                        vDate = DateTime.Parse(rowdata["VisitDate"].ToString()).ToString("yyyy-MM-dd");
                    }
                    XElement Hb =
                                     new XElement("ALT",
                                         new XElement("ALT", rowdata["TestResults"].ToString()),
                                         new XElement("ALTTestDate", vDate));
                    ALTResults.Add(Hb);
                }
                subjectlabresulthistory.Add(ALTResults);

                IEnumerable<DataRow> Creatinines =
                               (from fdata in filterdata.AsEnumerable()
                                where fdata.Field<String>("testname").Contains("Creatinine")
                                select fdata
                                ).ToList();

                XElement CreatinineResults = new XElement("CreatinineResults");
                foreach (DataRow rowdata in Creatinines.ToList())
                {
                    if (!DBNull.Value.Equals(rowdata["VisitDate"]))
                    {
                        vDate = DateTime.Parse(rowdata["VisitDate"].ToString()).ToString("yyyy-MM-dd");
                    }

                    XElement Hb =
                                     new XElement("Creatinine",
                                         new XElement("SerumCreatinine", rowdata["TestResults"].ToString()),
                                         new XElement("SerumCreatinineTestDate", vDate));
                    CreatinineResults.Add(Hb);
                }
                subjectlabresulthistory.Add(CreatinineResults);

                bool containsColumn = filterdata.AsEnumerable().Where(o => o.Field<String>("testname").Contains("Hepatitis B")).Any();
                if (containsColumn)
                {
                    DataRow query =
                        (from concept in filterdata.AsEnumerable()
                         where concept.Field<String>("testname").Contains("Hepatitis B")
                         select concept).OrderByDescending(q => q.Field<DateTime>("VisitDate")).FirstOrDefault();

                    string result = GetConceptValue(dtValues, "HEPATITIS B STATUS", query.Field<string>("TestResults").ToString());
                    subjectlabresulthistory.Add(new XElement("HepatitisBStatus", result.ToString()));
                }
                else
                {
                    subjectlabresulthistory.Add(new XElement("HepatitisBStatus", "5"));
                }

                bool HepatitisC = filterdata.AsEnumerable().Where(o => o.Field<String>("testname").Contains("Hepatitis C")).Any();
                if (HepatitisC)
                {
                    IEnumerable<DataRow> query =
                        (from concept in filterdata.AsEnumerable()
                         where concept.Field<String>("testname").Contains("Hepatitis C")
                         select concept).OrderByDescending(q => q.Field<DateTime>("VisitDate")).ToList();

                    DataRow rowdata = query.FirstOrDefault();

                    if (!DBNull.Value.Equals(rowdata["VisitDate"]))
                    {
                        vDate = DateTime.Parse(rowdata["VisitDate"].ToString()).ToString("yyyy-MM-dd");
                    }
                    string result = GetConceptValue(dtValues, "HEPATITIS C STATUS", rowdata["TestResults"].ToString());
                    subjectlabresulthistory.Add(new XElement("HepatitisCStatus", result.ToString()));
                    subjectlabresulthistory.Add(new XElement("HepatitisCTestDate", vDate));

                }
                else
                {
                    subjectlabresulthistory.Add(new XElement("HepatitisCStatus", "0"));
                    subjectlabresulthistory.Add(new XElement("HepatitisCTestDate", ""));
                }

                encounter = (DataTable)data.Tables[10];
                string outcome = string.Empty;
                string patientdeathdate = string.Empty;
                string transferdate = string.Empty;
                string stopdate = string.Empty;
                string causeofdeath = string.Empty;
                XElement PatientOutcome = new XElement("PatientOutcome");
                bool patientoutcome = encounter.AsEnumerable().Where(o => o.Field<Int32>("PatientID") == patientId).Any();
                if (patientoutcome)
                {
                    IEnumerable<DataRow> filterdata1 =
                        (from fdata in encounter.AsEnumerable()
                         where fdata.Field<Int32>("PatientID") == patientId
                         select fdata);


                    foreach (DataRow rowdata in filterdata1.ToList())
                    {
                        if (!DBNull.Value.Equals(rowdata["OutcomeOfTracking"]))
                        {
                            outcome = GetConceptValue(dtValues, "PATIENT OUTCOME", rowdata["OutcomeOfTracking"].ToString());
                            PatientOutcome.Add(new XElement("PatientOutcome", outcome));
                        }
                        else
                        {
                            PatientOutcome.Add(new XElement("PatientOutcome", "0"));
                        }
                        if (!DBNull.Value.Equals(rowdata["DeathDate"]))
                        {
                            PatientOutcome.Add(new XElement("PatientDeathDate", DateTime.Parse(rowdata["DeathDate"].ToString()).ToString("yyyy-MM-dd")));
                        }
                        else
                        {
                            PatientOutcome.Add(new XElement("PatientDeathDate", ""));
                        }

                        if (!DBNull.Value.Equals(rowdata["TransferredOutDate"]))
                        {
                            PatientOutcome.Add(new XElement("TransferDate", DateTime.Parse(rowdata["TransferredOutDate"].ToString()).ToString("yyyy-MM-dd")));
                        }
                        else
                        {
                            PatientOutcome.Add(new XElement("TransferDate", ""));
                        }

                        if (!DBNull.Value.Equals(rowdata["DiscontinuedCareDate"]))
                        {
                            PatientOutcome.Add(new XElement("StopDate", DateTime.Parse(rowdata["DiscontinuedCareDate"].ToString()).ToString("yyyy-MM-dd")));
                        }
                        else
                        {
                            PatientOutcome.Add(new XElement("StopDate", ""));
                        }

                        if (!DBNull.Value.Equals(rowdata["CauseOfDeath"]))
                        {
                            causeofdeath = rowdata["CauseOfDeath"].ToString();
                        }

                    }

                    subjectlabresulthistory.Add(PatientOutcome);

                }
                else
                {
                    PatientOutcome.Add(new XElement("PatientOutcome", "0"));
                    PatientOutcome.Add(new XElement("PatientDeathDate", ""));
                    PatientOutcome.Add(new XElement("TransferDate", ""));
                    PatientOutcome.Add(new XElement("StopDate", ""));
                    subjectlabresulthistory.Add(PatientOutcome);
                }

                string deathCause = GetConceptValue(dtValues, "CAUSE OF DEATH", causeofdeath.ToString());

                subjectlabresulthistory.Add(new XElement("CauseOfDeath", deathCause.ToString()));

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "IQCare.EMR.SubjectLabResultHistory", MessageBoxButtons.OK, MessageBoxIcon.Error);
                CLogger.WriteLog(ELogLevel.ERROR, ex.ToString());
            }
            return subjectlabresulthistory;
        }
    }
}
