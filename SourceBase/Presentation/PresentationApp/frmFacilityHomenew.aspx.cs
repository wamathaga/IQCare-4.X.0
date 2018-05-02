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
using Application.Common;
using Application.Presentation;
using Interface.Security;
using Interface.Reports;
using graph = Microsoft.Office.Interop.Owc11;
using System.Text;


public partial class frmFacilityHomenew : LogPage
{
    double theGraphLowCD4, theGraphRecentCD4, ChartChartTypeEnum, theGraphHb = 0.0, theGraphHct = 0.0, theGraphAST = 0.0, theGraphCr = 0.0, Graph = 0.0;
    string month1, month2, month3, month4, month5, month6;
    System.Data.DataSet theFacilityDS;
    System.Data.DataSet theDS;
    System.Data.DataSet theDS1;
    System.Data.DataSet theDS2;
    StringBuilder stringFacility = new StringBuilder();

    protected void Page_Load(object sender, EventArgs e)
    {

        if (Session["AppLocation"] == null || Session.Count == 0 || Session["AppUserID"].ToString() == "")
        {
            IQCareMsgBox.Show("SessionExpired", this);
            Response.Redirect("~/frmlogin.aspx",true);
        }
            Init_Form();
       

    }

    private void Init_Form()
    {

        IFacility FacilityManager;
        Double thePercent, theResultPercent, theTotalPateint, theTotalPMTCTPatient;
        FacilityManager = (IFacility)ObjectFactory.CreateInstance("BusinessProcess.Security.BFacility, BusinessProcess.Security");
        theDS = FacilityManager.GetFacilityStats(Convert.ToInt32(754));
        
        ViewState["theDS"] = theDS;
        FacilityManager = null;
        DataTable dttecareas = new DataTable();
        dttecareas = theDS.Tables[10];

        //pnl_FacTexhAreas.Controls.Clear();
        ////if (ddFacility.SelectedValue != "9999")
        ////{
        for (int k = 0; k <= dttecareas.Rows.Count - 1; k++)
        {

            pnl_FacTexhAreas.Controls.Add(new LiteralControl("<table width='100%'><tr>"));
            pnl_FacTexhAreas.Controls.Add(new LiteralControl("<td align='left'>"));
            Label theLabelTitle = new Label();
            theLabelTitle.ID = "Lbl_" + dttecareas.Rows[k]["ModuleName"].ToString() + dttecareas.Rows[k]["FacilityID"].ToString();
            theLabelTitle.Text = dttecareas.Rows[k]["ModuleName"].ToString();
            theLabelTitle.Font.Size = 9;
            theLabelTitle.Font.Bold = true;
            pnl_FacTexhAreas.Controls.Add(theLabelTitle);
            pnl_FacTexhAreas.Controls.Add(new LiteralControl("</td></tr>"));

            DataRow[] theDR = theDS.Tables[11].Select("ModuleID=" + dttecareas.Rows[k]["ModuleId"].ToString());
            if (theDR.Length > 0)
            {
                int m = 2;
                for (int j = 0; j <= theDR.Length - 1; j++)
                {
                    pnl_FacTexhAreas.Controls.Add(new LiteralControl("<tr><td style='width: 43%; height: 25px;' align='left'>"));
                    Label theLabel = new Label();
                    theLabel.ID = "Lbl_" + (theDR[j]["IndicatorName"].ToString() + dttecareas.Rows[k]["ModuleId"].ToString() + dttecareas.Rows[k]["FacilityID"].ToString());
                    theLabel.Text = theDR[j]["IndicatorName"].ToString();
                    //theLabel.CssClass = "bold pad18";
                    theLabel.Width = 200;
                    theLabel.Font.Size = 9;
                    theLabel.Font.Bold = true;
                    //theLabel.Font.Underline = true;
                    theLabel.ForeColor = System.Drawing.Color.Blue;
                    pnl_FacTexhAreas.Controls.Add(theLabel);
                    pnl_FacTexhAreas.Controls.Add(new LiteralControl("</td><td align='left'>"));

                    if (theDS.Tables[13 + m].Columns.Count > 1)
                    {
                        DataGrid objdView = new DataGrid();
                        objdView.ID = "Dview_" + (theDR[j]["IndicatorName"].ToString() + dttecareas.Rows[k]["ModuleId"].ToString() + dttecareas.Rows[k]["FacilityID"].ToString()) + "_Val";
                        objdView.EnableViewState = true;
                        objdView.AutoGenerateColumns = true;
                        objdView.DataSource = theDS.Tables[13];
                        //Panel theGrdPnl = new Panel();
                        //theGrdPnl.ID = "sanjaypnl";
                        pnl_FacTexhAreas.Controls.Add(objdView);
                        objdView.Attributes.Add("onload", "alert('GridLoaded');");
                        //theGrdPnl.Controls.Add(new LiteralControl("<table width='100%'>"));
                        //theGrdPnl.Controls.Add(new LiteralControl("<tr>"));
                        //theGrdPnl.Controls.Add(new LiteralControl("<td>"));
                        //theGrdPnl.Controls.Add(new LiteralControl("<div class='GridView whitebg'>"));
                        //theGrdPnl.Controls.Add(objdView);
                        //theGrdPnl.Controls.Add(new LiteralControl("</div>"));
                        //theGrdPnl.Controls.Add(new LiteralControl("</td>"));
                        //theGrdPnl.Controls.Add(new LiteralControl("</tr>"));
                        //theGrdPnl.Controls.Add(new LiteralControl("</table>"));
                        objdView.Visible = true;
                        objdView.DataBind();
                        //objdView += new EventHandler(objdView_PreRender);
                        //objdView.DataBind();

                    }
                    else
                    {
                        Label theValueLabel = new Label();
                        theValueLabel.ID = "Lbl_" + (theDR[j]["IndicatorName"].ToString() + dttecareas.Rows[k]["ModuleId"].ToString() + dttecareas.Rows[k]["FacilityID"].ToString()) + "_Val";
                        theValueLabel.Text = theDS.Tables[13 + m].Rows[0][0].ToString();
                        //theValueLabel.Text = theDS.Tables[13+m].Rows[0][0].ToString();
                        theValueLabel.CssClass = "pad18";
                        theValueLabel.Width = 200;
                        pnl_FacTexhAreas.Controls.Add(theValueLabel);

                    }
                    m = m + 2;
                }
                
            }
            pnl_FacTexhAreas.Controls.Add(new LiteralControl("</td></tr>"));
            pnl_FacTexhAreas.Controls.Add(new LiteralControl("<br/>"));
            pnl_FacTexhAreas.Controls.Add(new LiteralControl("</tr></table>"));
        }
       
        ////}

        

    }

}
