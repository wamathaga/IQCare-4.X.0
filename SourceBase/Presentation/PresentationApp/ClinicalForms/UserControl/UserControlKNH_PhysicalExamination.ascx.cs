using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using Interface.Security;
using Application.Presentation;
using Application.Common;
using Interface.Clinical;
using System.Text;
using Interface.Administration;

namespace PresentationApp.ClinicalForms.UserControl
{
    public partial class UserControlKNH_PhysicalExaminationascx : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindChkboxlst();
                
            }

            BindCheckUncheklogic(cblGeneralConditions);
            BindCheckUncheklogic(cblCardiovascularConditions);
            BindCheckUncheklogic(cblOralCavityConditions);
            BindCheckUncheklogic(cblGenitalUrinaryConditions);
            BindCheckUncheklogic(cblCNSConditions);
            BindCheckUncheklogic(cblChestLungsConditions);
            BindCheckUncheklogic(cblSkinConditions);
            BindCheckUncheklogic(cblAbdomenConditions);
        }
        public void BindChkboxlst()
        {
            BindChkboxlstControl(cblGeneralConditions, "GeneralConditions");
            BindChkboxlstControl(cblCardiovascularConditions, "CardiovascularConditions");
            BindChkboxlstControl(cblOralCavityConditions, "OralCavityConditions");
            BindChkboxlstControl(cblGenitalUrinaryConditions, "GenitalUrinaryConditions");
            BindChkboxlstControl(cblCNSConditions, "CNSConditions");
            BindChkboxlstControl(cblChestLungsConditions, "ChestLungsConditions");
            BindChkboxlstControl(cblSkinConditions, "SkinConditions");
            BindChkboxlstControl(cblAbdomenConditions, "AbdomenConditions");
            cblGeneralConditions.Attributes.Add("OnClick", "CheckBoxToggleShowHidePE('" + cblGeneralConditions.ClientID + "','divgeneralothercondition','Other','" + txtOtherGeneralConditions.ClientID + "')");
            cblCardiovascularConditions.Attributes.Add("OnClick", "CheckBoxToggleShowHidePE('" + cblCardiovascularConditions.ClientID + "','divOtherCardiovascularConditions','Other','" + txtOtherCardiovascularConditions.ClientID + "')");
            cblOralCavityConditions.Attributes.Add("OnClick", "CheckBoxToggleShowHidePE('" + cblOralCavityConditions.ClientID + "','divOtherOralCavityConditions','Other','" + txtOtherOralCavityConditions.ClientID + "')");
            cblGenitalUrinaryConditions.Attributes.Add("OnClick", "CheckBoxToggleShowHidePE('" + cblGenitalUrinaryConditions.ClientID + "','divOtherGenitourinaryConditions','Other','" + txtOtherGenitourinaryConditions.ClientID + "')");
            cblCNSConditions.Attributes.Add("OnClick", "CheckBoxToggleShowHidePE('" + cblCNSConditions.ClientID + "','divOtherCNSConditions','Other','" + txtOtherCNSConditions.ClientID + "')");
            cblChestLungsConditions.Attributes.Add("OnClick", "CheckBoxToggleShowHidePE('" + cblChestLungsConditions.ClientID + "','divOtherChestLungsConditions','Other','" + txtOtherChestLungsConditions.ClientID + "')");
            cblSkinConditions.Attributes.Add("OnClick", "CheckBoxToggleShowHidePE('" + cblSkinConditions.ClientID + "','divOtherSkinConditions','Other','" + txtOtherSkinConditions.ClientID + "')");
            cblAbdomenConditions.Attributes.Add("OnClick", "CheckBoxToggleShowHidePE('" + cblAbdomenConditions.ClientID + "','divOtherAbdomenConditions','Other','" + txtOtherAbdomenConditions.ClientID + "')");

            
        }
        public void BindChkboxlstControl(CheckBoxList chklst, string fieldname)
        {
            DataTable thedeCodeDT=new DataTable();
            IQCareUtils iQCareUtils = new IQCareUtils();
            BindFunctions BindManager = new BindFunctions();
            DataSet theDSXML = new DataSet();
            theDSXML.ReadXml(MapPath("..\\..\\XMLFiles\\AllMasters.con"));


            DataView theCodeDV = new DataView(theDSXML.Tables["MST_CODE"]);
            theCodeDV.RowFilter = "DeleteFlag=0 and Name='" + fieldname + "'";
            DataTable theCodeDT = (DataTable)iQCareUtils.CreateTableFromDataView(theCodeDV);
             DataView theDV = new DataView(theDSXML.Tables["MST_DECODE"]);

             if (theCodeDT.Rows.Count > 0)
            {

                theDV.RowFilter = "DeleteFlag=0 and SystemID IN(0," + Convert.ToString(Session["SystemId"]) + ") and CodeID=" + theCodeDT.Rows[0]["CodeId"];
                theDV.Sort = "SRNo ASC";
                thedeCodeDT = (DataTable)iQCareUtils.CreateTableFromDataView(theDV);
            }

            if (thedeCodeDT.Rows.Count > 0)
            {
                BindManager.BindCheckedList(chklst, thedeCodeDT, "Name", "ID");
            }
            

        }
        public void BindCheckUncheklogic(CheckBoxList chklst)
        {
            for (int i = 0; i < chklst.Items.Count; i++)
            {
                ListItem li = chklst.Items[i];
                if (li.Text == "Normal")
                {
                    li.Attributes.Add("onclick", "fnUncheckall('" + chklst.ClientID + "')");
                }
                else
                {
                    li.Attributes.Add("onclick", "fnUncheckNormal('" + chklst.ClientID + "')");
                }

            }
        }
       
    }
}