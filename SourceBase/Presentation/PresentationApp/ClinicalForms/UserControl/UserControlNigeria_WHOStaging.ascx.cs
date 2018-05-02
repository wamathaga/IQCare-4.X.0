using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using Interface.Clinical;
using Interface.Security;
using Application.Common;
using Application.Presentation;

namespace PresentationApp.ClinicalForms.UserControl
{

    public partial class UserControlNigeria_WHOStaging : System.Web.UI.UserControl
    {
        IKNHStaticForms WHOManager;

        protected void Page_Load(object sender, EventArgs e)
        {
            WHOManager = (IKNHStaticForms)ObjectFactory.CreateInstance("BusinessProcess.Clinical.BKNHStaticForms, BusinessProcess.Clinical");

            if (!IsPostBack)
            {                
                    BindGridView(gvWHO1, "NigeriaWHOStageIConditions");
                    BindGridView(gvWHO2, "NigeriaWHOStageIIConditions");
                    BindGridView(gvWHO3, "NigeriaWHOStageIIIConditions");
                    BindGridView(gvWHO4, "NigeriaWHOStageIVConditions");
                             
                
                BindDropdown(ddlInitiationWHOstage, "InitiationWHOstage");
                BindDropdown(ddlwhostage1, "InitiationWHOstage");
                BindDropdown(ddlhivassociated, "HIVAssociatedConditionsPeads");
                

                DataSet dsLatestWHOStage = WHOManager.GetLatestWHOStage(Convert.ToInt32(Session["PatientId"]));

                if (dsLatestWHOStage.Tables[0].Rows.Count > 0)
                {
                    ddlwhostage1.SelectedValue = dsLatestWHOStage.Tables[0].Rows[0]["WHOStage"].ToString();
                }

                for (int i = 0; i < ddlwhostage1.SelectedIndex; i++)
                {
                    ddlwhostage1.Items[i].Attributes.Add("Disabled", "Disabled");
                }

            }

        }
        private void BindGridView(GridView gv, string fieldname)
        {
            IQCareUtils theUtils = new IQCareUtils();
            DataSet theDSXML = new DataSet();
            theDSXML.ReadXml(MapPath("..\\..\\XMLFiles\\AllMasters.con"));
            DataView theCodeDV = new DataView(theDSXML.Tables["MST_CODE"]);
            theCodeDV.RowFilter = "DeleteFlag=0 and Name='" + fieldname + "'";
            DataTable theCodeDT = (DataTable)theUtils.CreateTableFromDataView(theCodeDV);
            DataTable theDT = new DataTable();
            if (theCodeDT.Rows.Count > 0)
            {
                DataView theDV = new DataView(theDSXML.Tables["MST_DECODE"]);
                theDV.RowFilter = "DeleteFlag=0 and SystemID IN(0," + Convert.ToString(Session["SystemId"]) + ") and CodeID=" + theCodeDT.Rows[0]["CodeId"];

                theDT = (DataTable)theUtils.CreateTableFromDataView(theDV);
            }
            gv.DataSource = theDT;
            gv.DataBind();
        }
       
        private void BindDropdown(DropDownList DropDownID, string fieldname)
        {
            DataSet theDS = new DataSet();
            theDS.ReadXml(MapPath("..\\..\\XMLFiles\\ALLMasters.con"));
            BindFunctions BindManager = new BindFunctions();
            IQCareUtils theUtils = new IQCareUtils();
            DataView theCodeDV = new DataView(theDS.Tables["MST_CODE"]);
            theCodeDV.RowFilter = "DeleteFlag=0 and Name='" + fieldname + "'";
            DataTable theCodeDT = (DataTable)theUtils.CreateTableFromDataView(theCodeDV);

            if (theDS.Tables["Mst_Decode"] != null)
            {
                DataView theDV = new DataView(theDS.Tables["Mst_Decode"]);
                if (theCodeDT.Rows.Count > 0)
                {
                    theDV.RowFilter = "DeleteFlag=0 and CodeId=" + theCodeDT.Rows[0]["CodeId"];
                    if (theDV.Table != null)
                    {
                        DataTable theDT = (DataTable)theUtils.CreateTableFromDataView(theDV);
                        BindManager.BindCombo(DropDownID, theDT, "Name", "Id");
                    }
                }

            }
        }
    }
}