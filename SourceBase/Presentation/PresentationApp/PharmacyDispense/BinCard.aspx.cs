using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Interface.SCM;
using Application.Presentation;
using Microsoft.Reporting.WebForms;
using CrystalDecisions.Web;
using System.IO;


namespace PresentationApp.PharmacyDispense
{
    public partial class BinCard : LogPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            int storeid = Convert.ToInt32(Request.QueryString["storeid"]);
            int itemid = Convert.ToInt32(Request.QueryString["itemid"]);
            DateTime dateFrom = Convert.ToDateTime(Request.QueryString["dtFrom"]);
            DateTime dateTo = Convert.ToDateTime(Request.QueryString["dtTo"]);
            DataSet theDS = GetBINCard(storeid, itemid, dateFrom, dateTo);

            ////////////////////////////////////////////////////////////////
            //Image Streaming
            DataTable dtFacility = new DataTable();
            // object of data row
            DataRow drow = null;
            // add the column in table to store the image of Byte array type
            dtFacility.Columns.Add("FacilityImage", System.Type.GetType("System.Byte[]"));
            drow = dtFacility.NewRow();
            // define the filestream object to read the image
            FileStream fs = default(FileStream);
            // define te binary reader to read the bytes of image
            BinaryReader br = default(BinaryReader);
            int ImageFlag = 0;

            // check the existance of image
            if (File.Exists(GblIQCare.GetPath() + theDS.Tables[3].Rows[0]["FacilityLogo"].ToString()))
            {
                // open image in file stream
                fs = new FileStream(GblIQCare.GetPath() + theDS.Tables[3].Rows[0]["FacilityLogo"].ToString(), FileMode.Open);

                // initialise the binary reader from file streamobject
                br = new BinaryReader(fs);
                // define the byte array of filelength
                byte[] imgbyte = new byte[fs.Length + 1];
                // read the bytes from the binary reader
                imgbyte = br.ReadBytes(Convert.ToInt32((fs.Length)));
                drow[0] = imgbyte;
                // add the image in bytearray
                dtFacility.Rows.Add(drow);
                ImageFlag = 1;
                // add row into the datatable
                br.Close();
                // close the binary reader
                fs.Close();
                // close the file stream
            }

            theDS.Tables.Add(dtFacility);
            ////////////////////////////////////////

            //theDS.WriteXmlSchema(GblIQCare.GetXMLPath() + "\\BinCard.xml");

            rptBinCard rep = new rptBinCard();
            rep.SetDataSource(theDS);
            //  rep.ParameterFields["FormDate","1"];
            if (Session["AppLocation"] != null)
            {
                rep.SetParameterValue("facilityname", Session["AppLocation"].ToString());
            }

            crViewer.ToolPanelView = ToolPanelViewType.None;
            crViewer.ReportSource = rep;
            crViewer.DataBind();

        }

        protected DataSet GetBINCard(int StoreId, int ItemsId, DateTime FromDate, DateTime ToDate)
        {

            ISCMReport binCard = (ISCMReport)ObjectFactory.CreateInstance("BusinessProcess.SCM.BSCMReport,BusinessProcess.SCM");
            return binCard.GetBINCard(StoreId, ItemsId, FromDate, ToDate, Convert.ToInt32(Session["AppLocationId"]));
        }
    }
}