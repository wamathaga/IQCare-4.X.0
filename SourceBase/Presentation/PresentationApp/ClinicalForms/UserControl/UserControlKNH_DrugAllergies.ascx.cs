using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Interface.Clinical;
using Application.Common;
using Application.Presentation;

namespace PresentationApp.ClinicalForms.UserControl
{
    public partial class UserControlKNH_DrugAllergies : System.Web.UI.UserControl
    {
        IQCareUtils theUtils = new IQCareUtils();

        protected void Page_Load(object sender, EventArgs e)
        {
            Page.ClientScript.GetPostBackEventReference(this, string.Empty);
            if (this.IsPostBack)
            {
                string eventTarget = (Page.Request["__EVENTTARGET"] == null) ? string.Empty : Page.Request["__EVENTTARGET"];

                if (eventTarget == "Search_OnClick")
                {
                    loadDrugAllergies();
                }
            }
            if (!IsPostBack)
            {
                loadDrugAllergies();
            }
        }

        protected void btnAllergies_Click(object sender, EventArgs e)
        {
            IQCareUtils.Redirect("frmAllergy.aspx?name=Add&opento=popup", "_blank", "toolbars=no,location=no,directories=no,dependent=yes,top=100,left=30,maximize=no,resize=no,width=1000,height=800,scrollbars=yes");
        }

        
        public void loadDrugAllergies()
        {
            DataView theDVDrugAllergy = new DataView();
            DataTable theDTDrugAllergy = new DataTable();

            IAllergyInfo drugAllergies = (IAllergyInfo)ObjectFactory.CreateInstance("BusinessProcess.Clinical.BAllergyInfo, BusinessProcess.Clinical");
            if (Session["PatientId"].ToString() != "0")
            {
                DataSet theDSDrugAllergy = drugAllergies.GetAllAllergyData(Convert.ToInt32(Session["PatientId"]));
                theDVDrugAllergy = new DataView(theDSDrugAllergy.Tables[0]);
                theDVDrugAllergy.RowFilter = "AllergyTypeID = 207";
                theDTDrugAllergy = (DataTable)theUtils.CreateTableFromDataView(theDVDrugAllergy);
                //grdDrugAllergy.DataSource = theDTDrugAllergy;
                BindGridDrudAllergy(theDTDrugAllergy);
                theDVDrugAllergy.Dispose();
                theDTDrugAllergy.Dispose();
                theDSDrugAllergy.Dispose();
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

            //grdDrugAllergy.Columns.Add(theCol0);
            //grdDrugAllergy.Columns.Add(theCol1);
            //grdDrugAllergy.Columns.Add(theCol2);
            grdDrugAllergy.Columns.Add(theCol3);
            //grdDrugAllergy.Columns.Add(theCol4);
            grdDrugAllergy.Columns.Add(theCol5);
            grdDrugAllergy.Columns.Add(theCol6);
            grdDrugAllergy.Columns.Add(theCol7);
            //grdDrugAllergy.Columns.Add(theCol8);
            grdDrugAllergy.Columns.Add(theCol9);
            grdDrugAllergy.Columns.Add(theCol10);
            //grdDrugAllergy.Columns[0].Visible = false;
            //grdDrugAllergy.Columns[1].Visible = false;
            //grdDrugAllergy.Columns[2].Visible = false;
            //grdDrugAllergy.Columns[4].Visible = false;
            //grdDrugAllergy.Columns[8].Visible = false;

            grdDrugAllergy.DataSource = theDT;
            grdDrugAllergy.DataBind();
            
        }
    }
}