using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Application.Common;
using Application.Presentation;
using Interface.Clinical;
using System.Drawing;

namespace PresentationApp.ClinicalForms.UserControl
{
    public partial class UserControlNigeria_VitalSigns : System.Web.UI.UserControl
    {
        DataSet theDSXML = new DataSet();
        DataView theDVCodeID, theDV;
        DataTable theDT;
        BindFunctions BindManager = new BindFunctions();
        IQCareUtils theUtils = new IQCareUtils();
        IKNHStaticForms KNHStatic;

        protected void Page_Load(object sender, EventArgs e)
        {
            KNHStatic = (IKNHStaticForms)ObjectFactory.CreateInstance("BusinessProcess.Clinical.BKNHStaticForms, BusinessProcess.Clinical");

            if (!IsPostBack)
            {
                addAttributes();
                Bind_Select_Lists();
            }
            JavaScriptFunctionsOnLoad();

        }
        private void Bind_Select_Lists()
        {
            theDSXML.ReadXml(MapPath("..\\..\\XMLFiles\\AllMasters.con"));

            theDVCodeID = new DataView(theDSXML.Tables["Mst_Code"]);
            theDVCodeID.RowFilter = "Name='NigeriaMUAC'";
            if (((DataTable)theDVCodeID.ToTable()).Rows.Count > 0)
            {
                theDV = new DataView(theDSXML.Tables["Mst_Decode"]);
                theDV.RowFilter = "CodeID=" + ((DataTable)theDVCodeID.ToTable()).Rows[0]["CodeID"].ToString();
                theDV.Sort = "SRNo asc, Name";
                theDT = (DataTable)theUtils.CreateTableFromDataView(theDV);
                BindManager.BindCombo(ddlMUAC, theDT, "Name", "ID");
            }
        }

        private void JavaScriptFunctionsOnLoad()
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "BMI", "CalcualteBMI('" + Convert.ToDouble(Session["patientageinyearmonth"].ToString()) + "','" + txtWeight.ClientID + "','" + txtHeight.ClientID + "','" + txtBMI.ClientID + "','" + lblBMIClassification.ClientID + "');", true);
            double age = Convert.ToDouble(Session["patientageinyearmonth"].ToString());

            if (age >= 15)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "HighLightAbnormalValuesAdults", "HighLightAbnormalValuesAdults('" + txtTemp.ClientID + "','" + txtRR.ClientID + "','"  + txtBPSystolic.ClientID + "','" + txtBPDiastolic.ClientID + "');", true);
                Page.ClientScript.RegisterStartupScript(this.GetType(), "showHidePeads", "show_hide('divshowvitalsign','notvisible');", true);
            }
            else
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "HighLightAbnormalValuesPeads", "HighLightAbnormalValuesPeads('" + Convert.ToDouble(Session["patientageinyearmonth"].ToString()) + "','" + txtTemp.ClientID + "','" + txtRR.ClientID + "','"  + txtBPSystolic.ClientID + "','" + txtBPDiastolic.ClientID + "');", true);
                Page.ClientScript.RegisterStartupScript(this.GetType(), "showHidePeads", "show_hide('divshowvitalsign','visible');", true);
            }
        }

        private void addAttributes()
        {
            //cblReferredTo.Attributes.Add("OnClick", "TriageCheckBoxHideUnhideOtherSpecialistClinic('" + cblReferredTo.ClientID + "');TriageCheckBoxHideUnhideOtherReferral('" + cblReferredTo.ClientID + "');");


            txtHeight.Attributes.Add("OnBlur", "CalcualteBMI('" + Convert.ToDouble(Session["patientageinyearmonth"].ToString()) + "','" + txtWeight.ClientID + "','" + txtHeight.ClientID + "','" + txtBMI.ClientID + "','" + lblBMIClassification.ClientID + "');isBetween('" + txtHeight.ClientID + "', '" + "Height" + "', '" + 0 + "', '" + 250 + "');");
            txtWeight.Attributes.Add("OnBlur", "CalcualteBMI('" + Convert.ToDouble(Session["patientageinyearmonth"].ToString()) + "','" + txtWeight.ClientID + "','" + txtHeight.ClientID + "','" + txtBMI.ClientID + "','" + lblBMIClassification.ClientID + "');isBetween('" + txtWeight.ClientID + "', '" + "Weight" + "', '" + 0 + "', '" + 225 + "');");
            txtHeight.Attributes.Add("onkeyup", "chkDecimal('" + txtHeight.ClientID + "');");
            txtWeight.Attributes.Add("onkeyup", "chkDecimal('" + txtWeight.ClientID + "');");
            txtheadcircumference.Attributes.Add("onkeyup", "chkDecimal('" + txtheadcircumference.ClientID + "');");
            //txtweightforheight.Attributes.Add("onkeyup", "chkDecimal('" + txtweightforheight.ClientID + "');");
            
           

            
            txtWeight.Attributes.Add("onkeyup", "chkDecimal('" + txtWeight.ClientID + "')");
            txtWeight.Attributes.Add("onBlur", "isBetween('" + txtWeight.ClientID + "', '" + "physWeight" + "', '" + 0 + "', '" + 225 + "');");

            double age = Convert.ToDouble(Session["patientageinyearmonth"].ToString());

            if (age >= 15)
            {
                txtTemp.Attributes.Add("OnBlur", "HighLightAbnormalValuesAdults('" + txtTemp.ClientID + "','" + txtRR.ClientID + "','" + txtBPSystolic.ClientID + "','" + txtBPDiastolic.ClientID + "');isBetween('" + txtTemp.ClientID + "', '" + "Temperature" + "', '" + 30 + "', '" + 50 + "');");
                txtTemp.Attributes.Add("onkeyup", "HighLightAbnormalValuesAdults('" + txtTemp.ClientID + "','" + txtRR.ClientID + "','" + txtBPSystolic.ClientID + "','" + txtBPDiastolic.ClientID + "');chkDecimal('" + txtTemp.ClientID + "');AddBoundary('" + txtTemp.ClientID + "','" + 30 + "','" + 50 + "');");

                txtBPSystolic.Attributes.Add("OnBlur", "HighLightAbnormalValuesAdults('" + txtTemp.ClientID + "','" + txtRR.ClientID + "','" + txtBPSystolic.ClientID + "','" + txtBPDiastolic.ClientID + "');isBetween('" + txtBPSystolic.ClientID + "', '" + "BPSystolic" + "', '" + 50 + "', '" + 250 + "');");
                txtBPSystolic.Attributes.Add("onkeyup", "HighLightAbnormalValuesAdults('" + txtTemp.ClientID + "','" + txtRR.ClientID + "','"  + txtBPSystolic.ClientID + "','" + txtBPDiastolic.ClientID + "');chkDecimal('" + txtBPSystolic.ClientID + "');");

                txtBPDiastolic.Attributes.Add("OnBlur", "HighLightAbnormalValuesAdults('" + txtTemp.ClientID + "','" + txtRR.ClientID + "','" + txtBPSystolic.ClientID + "','" + txtBPDiastolic.ClientID + "');isBetween('" + txtBPDiastolic.ClientID + "', '" + "BPDiastolic" + "', '" + 30 + "', '" + 150 + "')");
                txtBPDiastolic.Attributes.Add("onkeyup", "HighLightAbnormalValuesAdults('" + txtTemp.ClientID + "','" + txtRR.ClientID + "','"  + txtBPSystolic.ClientID + "','" + txtBPDiastolic.ClientID + "');chkDecimal('" + txtBPDiastolic.ClientID + "');");

                txtRR.Attributes.Add("OnBlur", "HighLightAbnormalValuesAdults('" + txtTemp.ClientID + "','" + txtRR.ClientID + "','" + txtBPSystolic.ClientID + "','" + txtBPDiastolic.ClientID + "');isBetween('" + txtRR.ClientID + "', '" + "RR" + "', '" + 4 + "', '" + 100 + "')");
                txtRR.Attributes.Add("onkeyup", "HighLightAbnormalValuesAdults('" + txtTemp.ClientID + "','" + txtRR.ClientID + "','"  + txtBPSystolic.ClientID + "','" + txtBPDiastolic.ClientID + "');chkDecimal('" + txtRR.ClientID + "');");

                
            }
            else
            {
                txtTemp.Attributes.Add("OnBlur", "HighLightAbnormalValuesPeads('" + Convert.ToDouble(Session["patientageinyearmonth"].ToString()) + "','" + txtTemp.ClientID + "','" + txtRR.ClientID + "','" + txtBPSystolic.ClientID + "','" + txtBPDiastolic.ClientID + "');isBetween('" + txtTemp.ClientID + "', '" + "Temperature" + "', '" + 30 + "', '" + 50 + "');");
                txtTemp.Attributes.Add("onkeyup", "HighLightAbnormalValuesPeads('" + Convert.ToDouble(Session["patientageinyearmonth"].ToString()) + "','" + txtTemp.ClientID + "','" + txtRR.ClientID + "','" + txtBPSystolic.ClientID + "','" + txtBPDiastolic.ClientID + "');chkDecimal('" + txtTemp.ClientID + "');AddBoundary('" + txtTemp.ClientID + "','" + 30 + "','" + 50 + "');");

                txtBPSystolic.Attributes.Add("OnBlur", "HighLightAbnormalValuesPeads('" + Convert.ToDouble(Session["patientageinyearmonth"].ToString()) + "','" + txtTemp.ClientID + "','" + txtRR.ClientID + "','" + txtBPSystolic.ClientID + "','" + txtBPDiastolic.ClientID + "');isBetween('" + txtBPSystolic.ClientID + "', '" + "physBPSystolic" + "', '" + 50 + "', '" + 250 + "');");
                txtBPSystolic.Attributes.Add("onkeyup", "HighLightAbnormalValuesPeads('" + Convert.ToDouble(Session["patientageinyearmonth"].ToString()) + "','" + txtTemp.ClientID + "','" + txtRR.ClientID + "','"  + txtBPSystolic.ClientID + "','" + txtBPDiastolic.ClientID + "');chkDecimal('" + txtBPSystolic.ClientID + "');");

                txtBPDiastolic.Attributes.Add("OnBlur", "HighLightAbnormalValuesPeads('" + Convert.ToDouble(Session["patientageinyearmonth"].ToString()) + "','" + txtTemp.ClientID + "','" + txtRR.ClientID + "','" + txtBPSystolic.ClientID + "','" + txtBPDiastolic.ClientID + "');isBetween('" + txtBPDiastolic.ClientID + "', '" + "physBPDiastolic" + "', '" + 30 + "', '" + 150 + "');");
                txtBPDiastolic.Attributes.Add("onkeyup", "HighLightAbnormalValuesPeads('" + Convert.ToDouble(Session["patientageinyearmonth"].ToString()) + "','" + txtTemp.ClientID + "','" + txtRR.ClientID + "','" + txtBPSystolic.ClientID + "','" + txtBPDiastolic.ClientID + "');chkDecimal('" + txtBPDiastolic.ClientID + "');");

                txtRR.Attributes.Add("OnBlur", "HighLightAbnormalValuesPeads('" + Convert.ToDouble(Session["patientageinyearmonth"].ToString()) + "','" + txtTemp.ClientID + "','" + txtRR.ClientID + "','" + txtBPSystolic.ClientID + "','" + txtBPDiastolic.ClientID + "');isBetween('" + txtRR.ClientID + "', '" + "RR" + "', '" + 4 + "', '" + 100 + "');");
                txtRR.Attributes.Add("onkeyup", "HighLightAbnormalValuesPeads('" + Convert.ToDouble(Session["patientageinyearmonth"].ToString()) + "','" + txtTemp.ClientID + "','" + txtRR.ClientID + "','"  + txtBPSystolic.ClientID + "','" + txtBPDiastolic.ClientID + "');chkDecimal('" + txtRR.ClientID + "');");

                
            }


            
            
            
        }

        //public void showHideZscores()
        //{
        //    if (Convert.ToDouble(Session["patientageinyearmonth"].ToString()) >= 5)
        //    {
        //        lblWALabel.Visible = false;
        //        lblWA.Visible = false;
        //        lblWAClassification.Visible = false;

        //        lblWHLabel.Visible = false;
        //        lblWH.Visible = false;
        //        lblWHClassification.Visible = false;

        //        lblBMIzLabel.Visible = true;
        //        lblBMIz.Visible = true;
        //        lblBMIzClassification.Visible = true;
        //    }
        //    else
        //    {
        //        lblWALabel.Visible = true;
        //        lblWA.Visible = true;
        //        lblWAClassification.Visible = true;

        //        lblWHLabel.Visible = true;
        //        lblWH.Visible = true;
        //        lblWHClassification.Visible = true;

        //        lblBMIzLabel.Visible = false;
        //        lblBMIz.Visible = false;
        //        lblBMIzClassification.Visible = false;
        //    }
        //}

        //public void calculateZScores()
        //{
        //    double L = 0, M = 0, S = 0, WeightAgeZ = 0, WeightHeightZ = 0, HAz = 0, BMIz = 0, weight = 0, heightInCm = 0, bmi = 0;
        //    DataSet ZScoreDS = new DataSet();
        //    ZScoreDS = KNHStatic.GetZScoreValues(Convert.ToInt32(Session["PatientId"]), Convert.ToString(Session["PatientSex"]), txtHeight.Text);

        //    //////weight for Age//////////
        //    if (Convert.ToDouble(Session["patientageinyearmonth"].ToString()) < 15)
        //    {
        //        if (ZScoreDS.Tables[0].Rows.Count > 0)
        //        {
        //            L = Convert.ToDouble(ZScoreDS.Tables[0].Rows[0]["L"].ToString());
        //            M = Convert.ToDouble(ZScoreDS.Tables[0].Rows[0]["M"].ToString());
        //            S = Convert.ToDouble(ZScoreDS.Tables[0].Rows[0]["S"].ToString());
        //            if (txtWeight.Text != "")
        //                weight = Convert.ToDouble(txtWeight.Text);
        //            else
        //                weight = 0;

        //            //Weight for age calculation
        //            if (L != 0)
        //                WeightAgeZ = ((Math.Pow((weight / M), L)) - 1) / (S * L);
        //            else
        //                WeightAgeZ = (Math.Log(weight / M)) / S;


        //            if (WeightAgeZ >= 4)
        //            {
        //                lblWA.Text = "4";
        //                lblWAClassification.Text = " Overweight";
        //                lblWA.ForeColor = Color.Red;
        //                lblWAClassification.ForeColor = Color.Red;
        //            }
        //            else if (WeightAgeZ >= 3 && WeightAgeZ < 4)
        //            {
        //                lblWA.Text = "3";
        //                lblWAClassification.Text = " Overweight";
        //                lblWA.ForeColor = Color.Red;
        //                lblWAClassification.ForeColor = Color.Red;
        //            }
        //            else if (WeightAgeZ >= 2 && WeightAgeZ < 3)
        //            {
        //                lblWA.Text = "2";
        //                lblWAClassification.Text = " Overweight";
        //                lblWA.ForeColor = Color.Red;
        //                lblWAClassification.ForeColor = Color.Red;
        //            }
        //            else if (WeightAgeZ >= 1 && WeightAgeZ < 2)
        //            {
        //                lblWA.Text = "1";
        //                lblWAClassification.Text = " Overweight";
        //                lblWA.ForeColor = Color.Red;
        //                lblWAClassification.ForeColor = Color.Red;
        //            }
        //            else if (WeightAgeZ > -1 && WeightAgeZ < 1)
        //            {
        //                lblWA.Text = "0";
        //                lblWAClassification.Text = " Normal";
        //                lblWA.ForeColor = Color.Green;
        //                lblWAClassification.ForeColor = Color.Green;
        //            }
        //            else if (WeightAgeZ <= -1 && WeightAgeZ > -2)
        //            {
        //                lblWA.Text = "-1";
        //                lblWAClassification.Text = " Mild";
        //                lblWA.ForeColor = Color.Orange;
        //                lblWAClassification.ForeColor = Color.Orange;
        //            }
        //            else if (WeightAgeZ <= -2 && WeightAgeZ > -3)
        //            {
        //                lblWA.Text = "-2";
        //                lblWAClassification.Text = " Moderate";
        //                lblWA.ForeColor = Color.Red;
        //                lblWAClassification.ForeColor = Color.Red;
        //            }
        //            else if (WeightAgeZ <= -3 && WeightAgeZ > -4)
        //            {
        //                lblWA.Text = "-3";
        //                lblWAClassification.Text = " Severe";
        //                lblWA.ForeColor = Color.Red;
        //                lblWAClassification.ForeColor = Color.Red;
        //            }
        //            else if (WeightAgeZ <= -4)
        //            {
        //                lblWA.Text = "-4";
        //                lblWAClassification.Text = " Severe";
        //                lblWA.ForeColor = Color.Red;
        //                lblWAClassification.ForeColor = Color.Red;
        //            }

        //        }
        //        else
        //        {
        //            lblWAClassification.Text = "Out of range";
        //        }
        //    }
        //    else
        //    {
        //        lblWA.Text = "";
        //        lblWAClassification.Text = "";
        //    }
        //    /////////////////////////////////

        //    ///////Weight for height calculation//////////////////////////////
        //    if (Convert.ToDouble(Session["patientageinyearmonth"].ToString()) < 15)
        //    {
        //        if (Convert.ToDouble(txtHeight.Text) <= 120 && Convert.ToDouble(txtHeight.Text) >= 45)
        //        {
        //            try
        //            {

        //                if (ZScoreDS.Tables[1].Rows.Count > 0)
        //                {
        //                    L = Convert.ToDouble(ZScoreDS.Tables[1].Rows[0]["L"].ToString());
        //                    M = Convert.ToDouble(ZScoreDS.Tables[1].Rows[0]["M"].ToString());
        //                    S = Convert.ToDouble(ZScoreDS.Tables[1].Rows[0]["S"].ToString());
        //                    if (txtWeight.Text != "")
        //                        weight = Convert.ToDouble(txtWeight.Text);
        //                    else
        //                        weight = 0;

        //                    if (L != 0)
        //                        WeightHeightZ = ((Math.Pow((weight / M), L)) - 1) / (S * L);
        //                    else
        //                        WeightHeightZ = (Math.Log(weight / M)) / S;

        //                    //lblWH.Text = string.Format("{0:f2}", WeightHeightZ);// WeightHeightZ.ToString("##.##");

        //                }
        //                else
        //                {
        //                    lblWH.Text = "Out of range";
        //                }

        //                if (WeightHeightZ >= 4)
        //                {
        //                    lblWH.Text = "4";
        //                    lblWHClassification.Text = " Overweight";
        //                    lblWH.ForeColor = Color.Red;
        //                    lblWHClassification.ForeColor = Color.Red;
        //                }
        //                else if (WeightHeightZ >= 3 && WeightHeightZ < 4)
        //                {
        //                    lblWH.Text = "3";
        //                    lblWHClassification.Text = " Overweight";
        //                    lblWH.ForeColor = Color.Red;
        //                    lblWHClassification.ForeColor = Color.Red;
        //                }
        //                else if (WeightHeightZ >= 2 && WeightHeightZ < 3)
        //                {
        //                    lblWH.Text = "2";
        //                    lblWHClassification.Text = " Overweight";
        //                    lblWH.ForeColor = Color.Red;
        //                    lblWHClassification.ForeColor = Color.Red;
        //                }
        //                else if (WeightHeightZ >= 1 && WeightHeightZ < 2)
        //                {
        //                    lblWH.Text = "1";
        //                    lblWHClassification.Text = " Overweight";
        //                    lblWH.ForeColor = Color.Red;
        //                    lblWHClassification.ForeColor = Color.Red;
        //                }
        //                else if (WeightHeightZ > -1 && WeightHeightZ < 1)
        //                {
        //                    lblWH.Text = "0";
        //                    lblWHClassification.Text = " Normal";
        //                    lblWH.ForeColor = Color.Green;
        //                    lblWHClassification.ForeColor = Color.Green;
        //                }
        //                else if (WeightHeightZ <= -1 && WeightHeightZ > -2)
        //                {
        //                    lblWH.Text = "-1";
        //                    lblWHClassification.Text = " Mild";
        //                    lblWH.ForeColor = Color.Orange;
        //                    lblWHClassification.ForeColor = Color.Orange;
        //                }
        //                else if (WeightHeightZ <= -2 && WeightHeightZ > -3)
        //                {
        //                    lblWH.Text = "-2";
        //                    lblWHClassification.Text = " Moderate";
        //                    lblWH.ForeColor = Color.Red;
        //                    lblWHClassification.ForeColor = Color.Red;
        //                }
        //                else if (WeightHeightZ <= -3 && WeightHeightZ > -4)
        //                {
        //                    lblWH.Text = "-3";
        //                    lblWHClassification.Text = " Severe";
        //                    lblWH.ForeColor = Color.Red;
        //                    lblWHClassification.ForeColor = Color.Red;
        //                }
        //                else if (WeightHeightZ <= -4)
        //                {
        //                    lblWH.Text = "-4";
        //                    lblWHClassification.Text = " Severe";
        //                    lblWH.ForeColor = Color.Red;
        //                    lblWHClassification.ForeColor = Color.Red;
        //                }
        //            }

        //            catch (Exception ex)
        //            {
        //            }
        //        }
        //        else
        //        {
        //            lblWH.Text = "";
        //            lblWHClassification.Text = "";
        //        }
        //    }
        //    else
        //    {
        //        lblWH.Text = "";
        //        lblWHClassification.Text = "";
        //    }

        //    //////////////////////////////////////////////////////////////////////

        //    ////BMIz (Z-Score Calculation)////////////////////////////
        //    if (Convert.ToDouble(Session["patientageinyearmonth"].ToString()) <= 15)
        //    {
        //        if (ZScoreDS.Tables[2].Rows.Count > 0)
        //        {
        //            L = Convert.ToDouble(ZScoreDS.Tables[2].Rows[0]["L"].ToString());
        //            M = Convert.ToDouble(ZScoreDS.Tables[2].Rows[0]["M"].ToString());
        //            S = Convert.ToDouble(ZScoreDS.Tables[2].Rows[0]["S"].ToString());
        //            if (txtHeight.Text != "" && txtWeight.Text != "")
        //                bmi = Convert.ToDouble(txtWeight.Text) / ((Convert.ToDouble(txtHeight.Text) / 100) * (Convert.ToDouble(txtHeight.Text) / 100));
        //            else
        //                bmi = 0;

        //            if (L != 0)
        //                BMIz = ((Math.Pow((bmi / M), L)) - 1) / (S * L);
        //            else
        //                BMIz = (Math.Log(bmi / M)) / S;

        //            lblBMIz.Text = string.Format("{0:f2}", BMIz);

        //            if (BMIz >= 4)
        //            {
        //                lblBMIz.Text = "4";
        //                lblBMIzClassification.Text = " Overweight";
        //                lblBMIz.ForeColor = Color.Red;
        //                lblBMIzClassification.ForeColor = Color.Red;
        //            }
        //            else if (BMIz >= 3 && BMIz < 4)
        //            {
        //                lblBMIz.Text = "3";
        //                lblBMIzClassification.Text = " Overweight";
        //                lblBMIz.ForeColor = Color.Red;
        //                lblBMIzClassification.ForeColor = Color.Red;
        //            }
        //            else if (BMIz >= 2 && BMIz < 3)
        //            {
        //                lblBMIz.Text = "2";
        //                lblBMIzClassification.Text = " Overweight";
        //                lblBMIz.ForeColor = Color.Red;
        //                lblBMIzClassification.ForeColor = Color.Red;
        //            }
        //            else if (BMIz >= 1 && BMIz < 2)
        //            {
        //                lblBMIz.Text = "1";
        //                lblBMIzClassification.Text = " Overweight";
        //                lblBMIz.ForeColor = Color.Red;
        //                lblBMIzClassification.ForeColor = Color.Red;
        //            }
        //            else if (BMIz > -1 && BMIz < 1)
        //            {
        //                lblBMIz.Text = "0";
        //                lblBMIzClassification.Text = " Normal";
        //                lblBMIz.ForeColor = Color.Green;
        //                lblBMIzClassification.ForeColor = Color.Green;
        //            }
        //            else if (BMIz <= -1 && BMIz > -2)
        //            {
        //                lblBMIz.Text = "-1";
        //                lblBMIzClassification.Text = " Mild";
        //                lblBMIz.ForeColor = Color.Orange;
        //                lblBMIzClassification.ForeColor = Color.Orange;
        //            }
        //            else if (BMIz <= -2 && BMIz > -3)
        //            {
        //                lblBMIz.Text = "-2";
        //                lblBMIzClassification.Text = " Moderate";
        //                lblBMIz.ForeColor = Color.Red;
        //                lblBMIzClassification.ForeColor = Color.Red;
        //            }
        //            else if (BMIz <= -3 && BMIz > -4)
        //            {
        //                lblBMIz.Text = "-3";
        //                lblBMIzClassification.Text = " Severe";
        //                lblBMIz.ForeColor = Color.Red;
        //                lblBMIzClassification.ForeColor = Color.Red;
        //            }
        //            else if (BMIz <= -4)
        //            {
        //                lblBMIz.Text = "-4";
        //                lblBMIzClassification.Text = " Severe";
        //                lblBMIz.ForeColor = Color.Red;
        //                lblBMIzClassification.ForeColor = Color.Red;
        //            }
        //        }
        //        else
        //        {
        //            lblBMIz.Text = "";
        //            lblBMIzClassification.Text = "";
        //        }

        //    }
        //    else
        //    {
        //        lblBMIz.Text = "";
        //        lblBMIzClassification.Text = "";
        //    }
        //    /////////////////////////////////////////////////////////

        //    ///////Height for age calculation/////////////////////////////
        //    //if (L != 0)
        //    //    HAz = ((Math.Pow((heightInCm / M), L)) - 1) / (S * L);
        //    //else
        //    //    HAz = (Math.Log(heightInCm / M)) / S;

        //    /////////////////////////////////////////////////////////////

            
            

            

        //    ZScoreDS.Dispose();

        //}

        protected void txtHeight_TextChanged(object sender, EventArgs e)
        {
            if (Convert.ToDouble(Session["patientageinyearmonth"].ToString()) <= 15)
            {
                //if (txtHeight.Text != "" && txtWeight.Text != "")
                //{
                //    //calculateZScores();
                //}
                //else if (txtHeight.Text == "")
                //{
                //    lblWHClassification.Text = "Enter Height";
                //    lblWHClassification.ForeColor = Color.Red;
                //}
                //else if (txtWeight.Text == "")
                //{
                //    //lblWAClassification.Text = "Enter Weight";
                //    lblWHClassification.Text = "Enter weight";
                //    //lblWAClassification.ForeColor = Color.Red;
                //    lblWHClassification.ForeColor = Color.Red;
                //}
            }

        }

        protected void txtWeight_TextChanged(object sender, EventArgs e)
        {
            if (Convert.ToDouble(Session["patientageinyearmonth"].ToString()) <= 15)
            {
                //if (txtHeight.Text != "" && txtWeight.Text != "")
                //{
                //    //calculateZScores();
                //}
                //else if (txtHeight.Text == "")
                //{
                //    lblWHClassification.Text = "Enter Height";
                //    lblWHClassification.ForeColor = Color.Red;
                //}
                //else if (txtWeight.Text == "")
                //{
                //    //lblWAClassification.Text = "Enter Weight";
                //    lblWHClassification.Text = "Enter weight";
                //    //lblWAClassification.ForeColor = Color.Red;
                //    lblWHClassification.ForeColor = Color.Red;
                //}
            }

        }

        
    }
}