using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Interface.Clinical;
using Application.Presentation;
using Application.Common;
using System.Drawing;

namespace IQCare.Web
{
    public partial class frmFacilityHome2 : LogPage
    {
        string ObjFactoryParameter = "BusinessProcess.Clinical.BPatientRegistration, BusinessProcess.Clinical";
        String[] aTileBackgroundColor = { "#5724A9", "#A62241", "#4765EB", "#2C7F96", "#CA4D2F", "#2F3FB0", "#890094", "#479ADA", "#1E8C00", "#8824A9", "#479ADA" };
        AuthenticationManager Authentication = new AuthenticationManager();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["AppLocation"] == null || Session.Count == 0 || Session["AppUserID"].ToString() == "")
            {
                IQCareMsgBox.Show("SessionExpired", this);
                Response.Redirect("~/frmlogin.aspx", true);
            }
            try
            {
                // (Master.FindControl("levelOneNavigationUserControl1").FindControl("lblheader") as Label).Text = "Facility Home";
                //(Master.FindControl("levelOneNavigationUserControl1").FindControl("lblRoot") as Label).Visible = false;
                if (!IsPostBack)
                {
                    Init_page();
                }
                createserviceButtons();
            }
            catch (Exception ex)
            {

                CLogger.WriteLog(ELogLevel.ERROR, ex.ToString());
                if (Session["PatientId"] == null || Convert.ToInt32(Session["PatientId"]) != 0)
                {
                    IQCareMsgBox.NotifyAction("Application has an issue, Please contact Administrator!", "Application Error", true, this, "window.location.href='" + Page.ResolveUrl("~/frmFindAddCustom.aspx") + "?srvNm=" + Session["TechnicalAreaName"] + "&mod=0'");
                    //Response.Write("<script>alert('Application has an issue, Please contact Administrator!') ; window.location.href='../frmFindAddCustom.aspx?srvNm=" + Session["TechnicalAreaName"] + "&mod=0'</script>");
                }
                else
                {
                    if (Session["TechnicalAreaId"] != null || Convert.ToInt16(Session["TechnicalAreaId"]) != 0)
                    {
                        IQCareMsgBox.NotifyAction("Application has an issue, Please contact Administrator!", "Application Error", true, this, "window.location.href='" + Page.ResolveUrl("~/frmFacilityHome.aspx;'"));
                        //Response.Write("<script>alert('Application has an issue, Please contact Administrator!') ; window.location.href='../frmFacilityHome.aspx'</script>");

                    }
                    else
                    {

                        IQCareMsgBox.NotifyAction("Application has an issue, Please contact Administrator!", "Application Error", true, this, "window.location.href='" + Page.ResolveUrl("~/frmLogin.aspx;'"));
                        //Response.Write("<script>alert('Application has an issue, Please contact Administrator!') ; window.location.href='../frmLogin.aspx'</script>");
                    }
                }
                ex = null;
            }

        }
        private void Init_page()
        {
            Session["PatientId"] = 0;

            createserviceButtons();
        }
        private Boolean setBillingRights()
        {
            btnBillSettings.Visible = false;
            Boolean hasRights = false;
            //check if billing is enabled
            if (Session["Billing"].ToString() != "1") return false;

            if (Authentication.HasFeatureRight(ApplicationAccess.Billing, (DataTable)Session["UserRight"]))
            {
                btnPatientBill.Visible = true;
                hasRights = true;
            }
            if (Authentication.HasFeatureRight(ApplicationAccess.BillingReports, (DataTable)Session["UserRight"]))
            {
                btnBillReports.Visible = true;
                hasRights = true;
            }
            if (Authentication.HasFeatureRight(ApplicationAccess.BillingReversal, (DataTable)Session["UserRight"]))
            {
                btnBillReversal.Visible = true;
                hasRights = true;
            }

            if (Authentication.HasFeatureRight(ApplicationAccess.Consumables, (DataTable)Session["UserRight"]))
            {
                btnConsumables.Visible = true;
                hasRights = true;
            }
            if (Authentication.HasFeatureRight(ApplicationAccess.BillingConfiguration, (DataTable)Session["UserRight"]))
            {
                btnBillSettings.Visible = true;
                hasRights = true;
                btnPriceList.Visible = true;
            }
            return hasRights;

        }

        private void createserviceButtons()
        {
            try
            {
                IPatientRegistration ptnMgr = (IPatientRegistration)ObjectFactory.CreateInstance(ObjFactoryParameter);
                DataSet DSModules = new DataSet();
                DataTable theDT = new DataTable();
                DataTable theDTIdent = new DataTable();
                if (Convert.ToInt32(Session["AppUserId"]) > 1)
                {
                    DSModules = ptnMgr.GetModuleNames(Convert.ToInt32(Session["AppLocationId"]), Convert.ToInt32(Session["AppUserId"]));
                    theDT = DSModules.Tables[2];
                    theDTIdent = DSModules.Tables[3];
                }
                else
                {
                    DSModules = ptnMgr.GetModuleNames(Convert.ToInt32(Session["AppLocationId"]));
                    theDT = DSModules.Tables[0];
                    theDTIdent = DSModules.Tables[2];
                    Session["AppModule"] = DSModules.Tables[3];
                }
                ViewState["theDTIdent"] = theDTIdent;// Add this line by Rahmat(29 March-2017) which is used at RepeaterseriveAreas_ItemCommand
                //removing Workplan if KNH Static form service is not available
                Session["isKNHEnabled"] = false;
                DataTable dt = theDT;

                if (!object.Equals(theDT, null))
                {
                    int tmp = dt.AsEnumerable().Where(p => p.Field<int>("moduleid") == Convert.ToInt32("204")).Select(p => p.Field<int>("moduleid")).FirstOrDefault();
                    if (tmp > 0)
                        Session["isKNHEnabled"] = true;
                }

                //DataTable dtTiles = DSModules.Tables[0];
                DataTable dtTiles =dt;



                //returns true if user has at least some rights in billing and billing is enabled
                if (setBillingRights())
                {
                    DataRow dr = dtTiles.NewRow();
                    int maxSequence = DSModules.Tables[0].AsEnumerable().Max(r => r.Field<int>("Sequence"));
                    dr["FacilityID"] = Convert.ToInt32(Session["FacilityID"].ToString());
                    dr["ModuleID"] = 9999;
                    dr["ModuleName"] = "Billing";
                    dr["Sequence"] = maxSequence + 1;
                    dtTiles.Rows.Add(dr);
                }

                //Add the Wards button only if wards is enabled and user has rights to it
                if (Session["Wards"].ToString() == "1" && Authentication.HasFeatureRight(ApplicationAccess.Wards, (DataTable)Session["UserRight"]))
                {
                    DataRow dr = dtTiles.NewRow();
                    int maxSequence = DSModules.Tables[0].AsEnumerable().Max(r => r.Field<int>("Sequence"));
                    dr["FacilityID"] = Convert.ToInt32(Session["FacilityID"].ToString());
                    dr["ModuleID"] = 9998;
                    dr["ModuleName"] = "Wards";
                    dr["Sequence"] = maxSequence + 1;
                    dtTiles.Rows.Add(dr);
                }

                theDT = this.GetTileData(dtTiles);
                RepeaterseriveAreas.DataSource = theDT;
                RepeaterseriveAreas.DataBind();
            }
            catch (Exception)
            {

                throw;
            }

        }

        private DataTable GetTileData(DataTable dtModTable)
        {
            DataTable theDT = new DataTable();
            theDT.Columns.Add("ModuleName", typeof(String));
            theDT.Columns.Add("ModuleUrl", typeof(String));
            theDT.Columns.Add("BackColor", typeof(String));
            theDT.Columns.Add("Icon", typeof(String));
            theDT.Columns.Add("ModuleId", typeof(Int32));
            String[] aTileBackgroundColor = { "#5724A9", "#A62241", "#4765EB", "#2C7F96", "#CA4D2F", "#2F3FB0", "#890094", "#479ADA", "#1E8C00", "#8824A9", "#479ADA" };
            List<TableCell> cells = new List<TableCell>();
            int colorIndex = 0;
            try
            {
                foreach (DataRow drMods in dtModTable.Rows)
                {
                    DataRow row = theDT.NewRow();
                    row["ModuleName"] = Convert.ToString(drMods["ModuleName"]);
                    row["ModuleId"] = Convert.ToInt32(drMods["ModuleID"]);
                    colorIndex = (colorIndex > 6 ? 0 : colorIndex);
                    row["BackColor"] = aTileBackgroundColor[colorIndex].ToString();

                    if (drMods["ModuleName"].ToString().Contains("257"))
                    {
                        row["Icon"] = "fa fa-heartbeat";
                    }
                    else if (drMods["ModuleName"].ToString().ToUpper().Contains("HIV"))
                    {
                        row["Icon"] = "iq iq-awareness-ribbon";
                    }
                    else if (drMods["ModuleName"].ToString().ToUpper().Contains("STATIC"))
                    {
                        row["Icon"] = "fa fa-user-md";
                    }
                    else if (drMods["ModuleName"].ToString().ToUpper().Contains("SMART"))
                    {
                        row["Icon"] = "iq iq-lungs";
                    }
                    else if (drMods["ModuleName"].ToString().ToUpper().Contains("NIGERIA"))
                    {
                        row["Icon"] = "fa fa-credit-card";
                    }
                    else if (drMods["ModuleName"].ToString().ToUpper().Contains("PATIENT"))
                    {
                        row["Icon"] = "fa fa-low-vision";
                    }
                    else if (drMods["ModuleName"].ToString().ToUpper().Contains("ART"))
                    {
                        row["Icon"] = "fa fa-feed";
                    }
                    else if (drMods["ModuleName"].ToString().ToUpper() == "PEP")
                    {
                        row["Icon"] = "fa fa-bookmark-o";
                    }
                    else if (drMods["ModuleName"].ToString().ToUpper().Contains("PHARMACY"))
                    {
                        row["Icon"] = "iq iq-lab";
                    }
                    else if (drMods["ModuleName"].ToString().ToUpper().Contains("SCM"))
                    {
                        row["Icon"] = "fa fa-chevron-down";
                    }
                    else if (drMods["ModuleName"].ToString().ToUpper() == "PMTCT")
                    {
                        row["Icon"] = "fa fa-github";
                    }
                    else if (drMods["ModuleName"].ToString().ToUpper() == "RECORDS")
                    {
                        row["Icon"] = "fa fa-files-o";
                    }
                    else if (drMods["ModuleName"].ToString().Contains("TB"))
                    {
                        row["Icon"] = "FA fa-bar-chart-o";
                    }
                    else if (drMods["ModuleName"].ToString().ToUpper() == "BILLING")
                    {
                        row["Icon"] = "fa fa-sign-out";
                    }
                    else if (drMods["ModuleName"].ToString().ToUpper() == "WARDS")
                    {
                        row["Icon"] = "fa fa-bed";
                    }
                    else
                    { row["Icon"] = "iq-inpatient:before"; }

                    //Setting the Hyperlinks for various features...................
                    if (Convert.ToInt32(drMods["ModuleID"].ToString()) == 206)
                    {
                        row["ModuleUrl"] = String.Format("PharmacyDispense/frmPharmacy_Dashboard.aspx");
                        //row["ModuleUrl"] = String.Format("frmFindAddCustom.aspx?srvNm={0}&mod={1}", drMods["ModuleName"].ToString(), drMods["ModuleID"]);
                    }
                    else if (drMods["ModuleName"].ToString().ToUpper() == "WARDS")
                    {
                        row["ModuleUrl"] = String.Format("Admission/frmAdmissionHome.aspx?from=home");
                    }
                    else if (drMods["ModuleName"].ToString().ToUpper() == "BILLING")
                    {
                        row["ModuleUrl"] = String.Format("'showPopup();'");
                    }
                    else if (drMods["ModuleName"].ToString().ToUpper() == "RECORDS")
                    {
                        row["ModuleUrl"] = String.Format("frmFindAddCustom.aspx?srvNm={0}&mod={1}", drMods["ModuleName"].ToString(), 0);
                    }
                    else
                        row["ModuleUrl"] = String.Format("frmFindAddCustom.aspx?srvNm={0}&mod={1}", drMods["ModuleName"].ToString(), drMods["ModuleID"]);


                    theDT.Rows.Add(row);
                    colorIndex++;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }



            return theDT;
        }

        //protected void LoadServiceCommand_OnCommand(object sender, CommandEventArgs e)
        //{
        //    int moduleid;
        //    if (int.TryParse(e.CommandArgument.ToString(), out moduleid))
        //    {
        //        ThisSession.SetCurrentModule(moduleid);

        //        HomePageLandScape home = ThisSession.CurrentLandScape.Where(m => m.MenuId == moduleid).FirstOrDefault();
        //        if (home != null)
        //        {
        //            string url = "";
        //            if (home.ClickAction == RedirectAction.FindAddPatient)
        //            {
        //                url = String.Format("~/Patient/FindAdd.aspx?srvNm={0}&mod={1}", home.MenuName, home.MenuId);
        //            }
        //            else if (home.ClickAction == RedirectAction.ModuleAction)
        //            {
        //                string folderName = home.ServiceAreaName.Replace(" ", String.Empty);
        //                if (System.IO.File.Exists(Server.MapPath(string.Format("~/{0}/Home.aspx", folderName))))
        //                {
        //                    Guid g = Guid.NewGuid();
        //                    url = string.Format("~/{1}/Home.aspx?key={0}", g.ToString(), folderName);

        //                }

        //            }
        //            if (url != "")
        //            {
        //                HttpContext.Current.ApplicationInstance.CompleteRequest();
        //                Response.Redirect(url, false);
        //            }
        //        }
        //    }

        //}

        protected string formatUrl(object url)
        {
            return String.Format(@"./{0}", url.ToString());
        }

        private void AddServiceButtons()
        {


        }

        protected string showIcon(string iconfont)
        {
            return String.Format(@"<i class='{0} iq-tile' style='font-size:40px;'></i>", iconfont);

        }


        protected string showName(string name)
        {
            return String.Format(@"<H5>{0}</H5>", name);
        }

        protected Boolean GetRecords(string parameter, DataTable theDT)
        {

            String str = theDT.AsEnumerable().Where(p => p.Field<string>("ModuleName") == Convert.ToString(parameter)).Select(p => p.Field<string>("ModuleName")).FirstOrDefault();
            if (str != null)
            { return true; }
            return false;
        }
       
        protected void RepeaterseriveAreas_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            IQCareUtils utils = new IQCareUtils();
            try
            {
                int rowid = (e.Item.ItemIndex);                
                HiddenField hdnModuleId = (HiddenField)RepeaterseriveAreas.Items[rowid].FindControl("hdnModuleId");
                int moduleId = Convert.ToInt32(hdnModuleId == null ? "0" : hdnModuleId.Value);
                HiddenField hdnModuleURL = (HiddenField)RepeaterseriveAreas.Items[rowid].FindControl("hdnModuleURL"); 
                if (moduleId == 9998)//Wards
                {
                    Response.Redirect(Convert.ToString(hdnModuleURL == null ? "" : hdnModuleURL.Value), false);
                }
                else if (moduleId == 9999)//Billing
                {
                    //Response.Redirect(Convert.ToString(hdnModuleURL == null ? "" : hdnModuleURL.Value), false);
                    billingOptionsPopup.Show();
                }
                else
                {
                    DataTable theDT = (DataTable)ViewState["theDTIdent"];
                    if (theDT != null && theDT.Rows.Count > 0)
                    {
                        DataView theDV = new DataView(theDT);
                        theDV.RowFilter = "ModuleId=" + Convert.ToInt32(hdnModuleId == null ? "0" : hdnModuleId.Value);
                        DataTable theIdenDT = utils.CreateTableFromDataView(theDV);
                        if (theIdenDT.Rows.Count == 0)
                        {
                            Response.Write("<script>alert('The Service Area doesn\\'t have any Identifier.\\n Please Add/Link atleast one Identifier with it from IQCare Management.');</script>");
                        }
                        else
                        {
                            Response.Redirect(Convert.ToString(hdnModuleURL == null ? "" : hdnModuleURL.Value), false);
                        }
                    }    
                }
                                             
                           
            }
            catch (Exception ex)
            {

            }
           
        }

    }
}
