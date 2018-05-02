using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Interface.Clinical;
using Application.Common;
using Application.Presentation;
using System.Data;

namespace PresentationApp.ClinicalForms.UserControl
{
    public partial class UserControl_AllergyExtruder : System.Web.UI.UserControl
    {
        IQCareUtils theUtils = new IQCareUtils();

        protected void Page_Load(object sender, EventArgs e)
        {
            //loadDrugAllergies();
        }

        public void loadDrugAllergies()
        {
            //DataView theDV = new DataView();
            //DataTable theDT = new DataTable();

            IAllergyInfo drugAllergies = (IAllergyInfo)ObjectFactory.CreateInstance("BusinessProcess.Clinical.BAllergyInfo, BusinessProcess.Clinical");
            if (Session["PatientId"].ToString() != "0")
            {
                DataSet theDS = drugAllergies.GetAllAllergyData(Convert.ToInt32(Session["PatientId"]));
                //theDV = new DataView(theDS.Tables[0]);
                //theDVDrugAllergy.RowFilter = "AllergyTypeID = 207";
                //theDT = (DataTable)theUtils.CreateTableFromDataView(theDV);
                //grdDrugAllergy.DataSource = theDTDrugAllergy;
                BindGridDrudAllergy(theDS.Tables[0]);
                //theDV.Dispose();
                //theDT.Dispose();
                theDS.Dispose();
                
            }
        }

        private void BindGridDrudAllergy(DataTable theDT)
        {
            grdDrugAllergy.Columns.Clear();

            BoundField theCol0 = new BoundField();
            theCol0.HeaderText = "Id";
            theCol0.DataField = "Id";
            theCol0.ItemStyle.CssClass = "textstyle";


            BoundField theCol1 = new BoundField();
            theCol1.HeaderText = "Patientid";
            theCol1.DataField = "ptn_pk";
            theCol1.ItemStyle.CssClass = "textstyle";


            BoundField theCol2 = new BoundField();
            theCol2.HeaderText = "AllergyTypeID";
            theCol2.DataField = "AllergyTypeID";
            theCol2.ItemStyle.CssClass = "textstyle";


            BoundField theCol3 = new BoundField();
            theCol3.HeaderText = "Allergy Type";
            theCol3.DataField = "AllergyTypeDesc";
            theCol3.ItemStyle.CssClass = "textstyle";
            theCol3.ReadOnly = true;


            BoundField theCol4 = new BoundField();
            theCol4.HeaderText = "AllergenTypeID";
            theCol4.DataField = "AllergenTypeID";
            theCol4.ItemStyle.CssClass = "textstyle";


            BoundField theCol5 = new BoundField();
            theCol5.HeaderText = "Allergen";
            theCol5.DataField = "AllergenDesc";
            theCol5.ItemStyle.CssClass = "textstyle";
            theCol5.ReadOnly = true;


            BoundField theCol6 = new BoundField();
            theCol6.HeaderText = "Other Allergen";
            theCol6.ItemStyle.CssClass = "textstyle";
            theCol6.DataField = "otherAllergen";
            theCol6.ReadOnly = true;


            BoundField theCol7 = new BoundField();
            theCol7.HeaderText = "Reaction Type";
            theCol7.ItemStyle.CssClass = "textstyle";
            theCol7.DataField = "typeReaction";
            theCol7.ReadOnly = true;


            BoundField theCol8 = new BoundField();
            theCol8.HeaderText = "SevrityTypeID";
            theCol8.DataField = "SevrityTypeID";
            theCol8.ItemStyle.CssClass = "textstyle";


            BoundField theCol9 = new BoundField();
            theCol9.HeaderText = "Severity";
            theCol9.ItemStyle.CssClass = "textstyle";
            theCol9.DataField = "severityDesc";
            theCol9.ReadOnly = true;


            BoundField theCol10 = new BoundField();
            theCol10.HeaderText = "Date Allergy";
            theCol10.DataField = "dateAllergy";
            theCol10.ItemStyle.CssClass = "textstyle";
            theCol10.DataFormatString = "{0:dd-MMM-yyyy}";

           
            grdDrugAllergy.Columns.Add(theCol3);
            
            grdDrugAllergy.Columns.Add(theCol5);
            //grdDrugAllergy.Columns.Add(theCol6);
            //grdDrugAllergy.Columns.Add(theCol7);
            
            //grdDrugAllergy.Columns.Add(theCol9);
            grdDrugAllergy.Columns.Add(theCol10);

            grdDrugAllergy.DataSource = theDT;
            grdDrugAllergy.DataBind();

        }
    }
}