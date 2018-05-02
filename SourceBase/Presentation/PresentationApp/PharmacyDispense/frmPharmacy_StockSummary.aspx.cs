using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Application.Presentation;
using Application.Common;
using Interface.SCM;
using AjaxControlToolkit;
using System.IO;
using System.Drawing;

namespace PresentationApp.PharmacyDispense
{
    public partial class frmPharmacy_StockSummary : LogPage
    {
        BindFunctions theBindManager = new BindFunctions();

        protected void Page_Load(object sender, EventArgs e)
        {
            (Master.FindControl("pnlExtruder") as Panel).Visible = false;
            (Master.FindControl("level2Navigation") as Control).Visible = true;
            //(Master.FindControl("levelTwoNavigationUserControl1").FindControl("lblformname") as Label).Text = "Stock Summary";
            (Master.FindControl("levelTwoNavigationUserControl1").FindControl("patientLevelMenu") as Menu).Visible = false;
            (Master.FindControl("levelTwoNavigationUserControl1").FindControl("PharmacyDispensingMenu") as Menu).Visible = true;
            (Master.FindControl("levelTwoNavigationUserControl1").FindControl("UserControl_Alerts1") as UserControl).Visible = false;
            (Master.FindControl("levelTwoNavigationUserControl1").FindControl("PanelPatiInfo") as Panel).Visible = false;
            //(Master.FindControl("facilityBanner") as Control).Visible = false;
            //(Master.FindControl("patientBanner") as Control).Visible = false;
            //(Master.FindControl("username1") as Control).Visible = false;
            //(Master.FindControl("currentdate1") as Control).Visible = false;
            //(Master.FindControl("facilityName") as Control).Visible = false;
            //(Master.FindControl("imageFlipLevel2") as Control).Visible = false;

            if (txtSearch.Text == "")
            {
                hdCustID.Value = "0";
            }

            if (!IsPostBack)
            {
                BindCombo();
                dtFrom.Value = DateTime.Now.ToString("dd-MMM-yyyy");
                dtTo.Value = DateTime.Now.ToString("dd-MMM-yyyy");
            }
        }


        private void BindCombo()
        {
            try
            {
                DataSet XMLDS = new DataSet();
                XMLDS.ReadXml(MapPath("..\\XMLFiles\\AllMasters.con"));

                DataView theDV = new DataView(XMLDS.Tables["Mst_Store"]);
                theDV.RowFilter = "(DeleteFlag =0 or DeleteFlag is null)";
                theDV.Sort = "Name ASC";
                DataTable theStoreDT = theDV.ToTable();
                theBindManager.BindCombo(ddlStore, theStoreDT, "Name", "Id");
            }
            catch (Exception err)
            {
                MsgBuilder theBuilder = new MsgBuilder();
                theBuilder.DataElements["MessageText"] = err.Message.ToString();
                IQCareMsgBox.Show("#C1", theBuilder, this);
            }

        }

        public class Drugs
        {
            protected int _DrugId;
            public int DrugId
            {
                get { return _DrugId; }
                set { _DrugId = value; }
            }

            protected int _avlqty;
            public int AvlQty
            {
                get { return _avlqty; }
                set { _avlqty = value; }
            }

            protected string _drugName;
            public string DrugName
            {
                get { return _drugName; }
                set { _drugName = value; }
            }


        }

        [System.Web.Script.Services.ScriptMethod()]
        [System.Web.Services.WebMethod]
        public static List<string> SearchBorrower(string prefixText, int count)
        {
            DataSet theDS = (DataSet)HttpContext.Current.Session["theStocks"];
            List<string> Drugsdetail = new List<string>();

            var drugs = from DataRow tmp in theDS.Tables[0].AsEnumerable()
                        where tmp["DrugName"].ToString().ToLower().Contains(prefixText.ToLower())
                        select tmp; // new { drugName = tmp["DrugName"].ToString(), drugID = tmp["Drug_pk"].ToString() };

            foreach (DataRow c in drugs)
            {
                Drugsdetail.Add(AutoCompleteExtender.CreateAutoCompleteItem(c[2].ToString(), c[1].ToString()));
            }


            return Drugsdetail;
        }




        private DataSet GetItems(int StoreId, int ItemsId, DateTime FromDate, DateTime ToDate)
        {
            ISCMReport objOpenStock = (ISCMReport)ObjectFactory.CreateInstance("BusinessProcess.SCM.BSCMReport,BusinessProcess.SCM");
            return objOpenStock.GetStockSummary(StoreId, ItemsId, FromDate, ToDate);
        }

        protected void ddlStore_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (Convert.ToInt32(ddlStore.SelectedValue) != 0)
                {
                    if (FieldValidation())
                    {

                        Session["theStocks"] = GetItems(Convert.ToInt32(ddlStore.SelectedValue), Convert.ToInt32(hdCustID.Value), Convert.ToDateTime(dtFrom.Value), Convert.ToDateTime(dtTo.Value));

                        DataSet stockSummary = (DataSet)Session["theStocks"];

                        DataView theDV = new DataView(stockSummary.Tables[1]);
                        if (hdCustID.Value != "0")
                            theDV.RowFilter = "ItemId = " + hdCustID.Value;
                        //DataTable theDT = theDV.ToTable();

                        populateGrid(theDV.ToTable());
                        //grdStockSummary.DataSource = theDV.ToTable();
                        //grdStockSummary.DataBind();
                    }

                }
                else
                {
                    //txtItemName.Text = "";
                    grdStockSummary.Columns.Clear();
                    grdStockSummary.DataSource = null;
                }

            }
            catch (Exception err)
            {
                MsgBuilder theBuilder = new MsgBuilder();
                theBuilder.DataElements["MessageText"] = err.Message.ToString();
                IQCareMsgBox.Show("#C1", theBuilder, this);
                //IQCareWindowMsgBox.ShowWindowConfirm("#C1", theBuilder, this);
            }
        }

        protected void populateGrid(DataTable theDT)
        {
            grdStockSummary.DataSource = theDT;
            grdStockSummary.DataBind();
            Session["populateGrid"] = theDT;
        }

        private Boolean FieldValidation()
        {
            //Store Validation
            if (ddlStore.SelectedValue == "0")
            {
                MsgBuilder theBuilder = new MsgBuilder();
                theBuilder.DataElements["MessageText"] =  "Store is not selected";
                IQCareMsgBox.Show("#C1", theBuilder, this);
                Label lblError = new Label();
                lblError.Text = (Master.FindControl("lblError") as Label).Text;
                return false;
            }
            if (dtTo.Value != "")
            {
                TimeSpan difference = Convert.ToDateTime(dtTo.Value) - Convert.ToDateTime(Application["AppCurrentDate"]);
                double days = difference.TotalDays;
                if (days > 0)
                {
                    MsgBuilder theBuilder = new MsgBuilder();
                    theBuilder.DataElements["MessageText"] = "To Date cannot be greater than todays date";
                    IQCareMsgBox.Show("#C1", theBuilder, this);
                    dtTo.Focus();
                    return false;
                }
            }
            IQCareMsgBox.HideMessage(this);
            return true;
        }

        protected void Button4_Click(object sender, EventArgs e)
        {
            if (FieldValidation() == false)
            {
                return;
            }
            Session["theStocks"] = GetItems(Convert.ToInt32(ddlStore.SelectedValue), Convert.ToInt32(hdCustID.Value), Convert.ToDateTime(dtFrom.Value), Convert.ToDateTime(dtTo.Value));
            DataSet stockSummary = (DataSet)Session["theStocks"];
            DataView theDV = new DataView(stockSummary.Tables[1]);
            if (hdCustID.Value != "0")
                theDV.RowFilter = "ItemId = " + hdCustID.Value;
            //DataTable theDT = theDV.ToTable();

            populateGrid(theDV.ToTable());
            //grdStockSummary.DataSource = theDV.ToTable(); // stockSummary.Tables[1].Select("ItemId=" + hdCustID.Value);
            //grdStockSummary.DataBind();
        }

        protected void grdStockSummary_RowCommand(object sender, GridViewCommandEventArgs e)
        {            
            int storeid = Convert.ToInt32(ddlStore.SelectedValue.ToString());
            int index = Convert.ToInt32(e.CommandArgument.ToString());
            grdStockSummary.SelectedIndex = index;
            int itemid =  Convert.ToInt32(grdStockSummary.SelectedDataKey["ItemId"].ToString());
            DateTime dateFrom = Convert.ToDateTime(Convert.ToDateTime(dtFrom.Value).ToString("yyyy-MM-dd"));
            DateTime dateTo = Convert.ToDateTime(Convert.ToDateTime(dtTo.Value).ToString("yyyy-MM-dd"));

            //Iframe1.Visible = true;
            //Iframe1.Attributes.Add("src","BinCard.aspx?storeid=" + storeid.ToString() + "&itemid=" + itemid.ToString() + "&dtFrom=" + dateFrom.ToString()
            //    + "&dtTo=" + dateTo.ToString());

            //Page.ClientScript.RegisterStartupScript(this.GetType(), "reloadIframe", "var iframe = document.getElementById('ctl00_IQCareContentPlaceHolder_Iframe1'); iframe.src = iframe.src;", true);

            //binCard_ModalPopupExtender.Show();

            IQCareUtils.Redirect("BinCard.aspx?storeid=" + storeid.ToString() + "&itemid=" + itemid.ToString() + "&dtFrom=" + dateFrom.ToString() + "&dtTo=" + dateTo.ToString(), "_blank", "toolbars=no,location=no,directories=no,dependent=yes,top=100,left=30,maximize=no,resize=no,width=1000,height=800,scrollbars=yes");            
            //Response.Redirect("BinCard.aspx?storeid=" + storeid.ToString() + "&itemid=" + itemid.ToString() + "&dtFrom=" + dateFrom.ToString() + "&dtTo=" + dateTo.ToString(), false);
            //Response.Redirect("../Reports/frmReportViewer.aspx?ReportName=StockSummary&storeid=" + storeid.ToString() + "&itemid=" + itemid.ToString() + "&dtFrom=" + dateFrom.ToString() + "&dtTo=" + dateTo.ToString() + "", false);

            //Response.Redirect("BinCard.aspx?storeid=" + storeid.ToString() + "&itemid=" + itemid.ToString() + "&dtFrom=" + dateFrom.ToString()
            //    + "&dtTo=" + dateTo.ToString() + "");

            
            
            
        }

        protected void btnExportToExcel_Click(object sender, EventArgs e)
        {
            ExportToExcel();
            //Response.Clear();
            //Response.Buffer = true;
            //Response.AddHeader("content-disposition", "attachment;filename=BIN_Card.xls");
            //Response.Charset = "";
            //Response.ContentType = "application/vnd.ms-excel";
            //using (StringWriter sw = new StringWriter())
            //{
            //    HtmlTextWriter hw = new HtmlTextWriter(sw);

            //    //To Export all pages
            //    //grdStockSummary.AllowPaging = false;
            //    populateGrid((DataTable)Session["populateGrid"]);

            //    grdStockSummary.HeaderRow.BackColor = Color.White;
            //    foreach (TableCell cell in grdStockSummary.HeaderRow.Cells)
            //    {
            //        cell.BackColor = grdStockSummary.HeaderStyle.BackColor;
            //    }
            //    foreach (GridViewRow row in grdStockSummary.Rows)
            //    {

            //        row.BackColor = Color.White;
            //        foreach (TableCell cell in row.Cells)
            //        {
            //            if (row.RowIndex % 2 == 0)
            //            {
            //                //cell.BackColor = grdStockSummary.AlternatingRowStyle.BackColor;
            //                cell.BackColor = ColorTranslator.FromHtml("#CCCCFF");
            //            }
            //            else
            //            {
            //                cell.BackColor = grdStockSummary.RowStyle.BackColor;
            //            }
            //            cell.CssClass = "textmode";
            //        }
            //    }
            //    grdStockSummary.Columns[8].Visible = false;
            //    grdStockSummary.RenderControl(hw);

            //    //style to format numbers to string
            //    string style = @"<style> .textmode { } </style>";
            //    Response.Write(style);
            //    Response.Output.Write(sw.ToString());
            //    Response.Flush();
            //    Response.End();
            //}
        }
        private void ExportToExcel()
        {
            try
            {
                var gv = new GridView();
                DataTable dtExcel = new DataTable();
                if (Session["populateGrid"] != null)
                {
                    dtExcel = (DataTable)Session["populateGrid"];
                    string[] existingcol = { "ItemId","AdjustmentQuantity","StoreName" };
                    foreach (string colName in existingcol)
                    {
                        dtExcel.Columns.Remove(colName);
                    }                    
                    gv.DataSource = dtExcel;
                    gv.DataBind();
                    //gv.Columns[0].Visible = false;
                }

                // style the header

                gv.HeaderRow.Cells[0].Text = "Drug Name";
                gv.HeaderRow.Cells[1].Text = "Unit";
                gv.HeaderRow.Cells[2].Text = "Opening Stock";
                gv.HeaderRow.Cells[3].Text = "Received Qty";
                gv.HeaderRow.Cells[4].Text = "Interstore Issue";
                gv.HeaderRow.Cells[5].Text = "Quantity Disp";
                gv.HeaderRow.Cells[6].Text = "Adjusted Qty";
                gv.HeaderRow.Cells[7].Text = "Closing Qty";
                //gv.HeaderRow.Height = Unit.Pixel(35);
                //gv.HeaderRow.Cells[0].Text = "Some custom header text";
                //gv.HeaderRow.Cells[0].Width = Unit.Pixel(400);

                //for (int col = 0; col < gv.HeaderRow.Controls.Count; col++)
                //{
                //    TableCell tc = gv.HeaderRow.Cells[col];
                //    tc.Style.Add("color", "#FFFFFF");
                //    tc.Style.Add("background-color", "#444");
                //    tc.Style.Add("border-color", "#000");
                //}

                // And your code as follows

                Response.Clear();
                Response.Charset = "";
                Response.Buffer = true;
                Response.AddHeader("content-disposition", String.Format("attachment;filename={0}", "BIN_Card.xls"));

                // Prompt for Open/Save/Cancel
                Response.Cache.SetCacheability(HttpCacheability.Private);
                Response.ContentType = "application/vnd.ms-excel";

                System.IO.StringWriter stringWrite = new System.IO.StringWriter();
                System.Web.UI.HtmlTextWriter htmlWrite =
                     new HtmlTextWriter(stringWrite);

                //For Alternate row color
                gv.HeaderRow.BackColor = Color.White;
                foreach (TableCell cell in gv.HeaderRow.Cells)
                {
                    cell.BackColor = gv.HeaderStyle.BackColor;
                }
                foreach (GridViewRow row in gv.Rows)
                {

                    row.BackColor = Color.White;
                    foreach (TableCell cell in row.Cells)
                    {
                        if (row.RowIndex % 2 == 0)
                        {
                            //cell.BackColor = grdStockSummary.AlternatingRowStyle.BackColor;
                            cell.BackColor = ColorTranslator.FromHtml("#CCCCFF");
                        }
                        else
                        {
                            cell.BackColor = gv.RowStyle.BackColor;
                        }
                        cell.CssClass = "textmode";
                    }
                }
                //

                gv.RenderControl(htmlWrite);

                string style = @"<style> .textmode { } </style>";
                Response.Write(style);

                Response.Write(stringWrite.ToString());
                Response.End();

            }
            catch(Exception ex)
            {
            }
        }
        
        public override void VerifyRenderingInServerForm(Control control)
        {
            /* Verifies that the control is rendered */
        }

       
    }
}