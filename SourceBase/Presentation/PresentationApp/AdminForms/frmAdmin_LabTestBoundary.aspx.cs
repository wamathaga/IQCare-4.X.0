using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using Interface.Scheduler;
using Interface.Clinical;
using Application.Common;
using Application.Presentation;
using Interface.Security;
using Interface.Administration;

public partial class frmAdmin_LabTestBoundary : LogPage
{
    String currentDate;
    #region "User Functions"
    private void setFromToDateAndShowData()
    {
        DateTime currDateType;
        currentDate = string.Format("{0:dd/mm/yyyy}", Application["AppCurrentDate"].ToString());
        currDateType = Convert.ToDateTime(currentDate);

        fillDropDownList();
     
    }

    private void fillDropDownList()
    {
        ddUnit.DataSource = ((DataSet)ViewState["MstDS"]).Tables[0];
        ddUnit.DataTextField = "Name";
        ddUnit.DataValueField = "UnitID";
        ddUnit.DataBind();

        ddUnit.Items.Insert(0, "Select");

        ddDefault.Items.Add("No");
        ddDefault.Items.Add("Yes");
        ddldetect.Items.Add("No");
        ddldetect.Items.Add("Yes");
    }

    //private DataSet fillDataSet()
    //{
    //    IAppointment FormManager;
    //    DataSet theDtSet;
    //    IQCareUtils clsUtil = new IQCareUtils();

    //    FormManager = (IAppointment)ObjectFactory.CreateInstance("BusinessProcess.Scheduler.BAppointment, BusinessProcess.Scheduler");
    //    IQCareUtils theUtil = new IQCareUtils();

    //    theDtSet = new DataSet();
        
    //    grdSearchResult.Columns.Clear();
    //    grdSearchResult.DataSource = null;

    //    return theDtSet;
    //}
    
    private void BindGrid()
    {
       BoundField theCol0 = new BoundField();
        theCol0.HeaderText = "SubTestID";
        theCol0.DataField = "SubTestID";
        theCol0.ItemStyle.CssClass = " textstyle";
        theCol0.ReadOnly = true;
        grdSearchResult.Columns.Add(theCol0);
        grdSearchResult.Columns[0].Visible = false;

        BoundField theCol1 = new BoundField();
        theCol1.HeaderText = "SubTestName";
        theCol1.DataField = "SubTestName";
        theCol1.ItemStyle.CssClass = "textstyle";
        theCol1.ReadOnly = true;
        grdSearchResult.Columns.Add(theCol1);
        grdSearchResult.Columns[1].Visible = false;

        BoundField theCol2 = new BoundField();
        theCol2.HeaderText = "UnitID";
        theCol2.DataField = "UnitID";
        theCol2.ItemStyle.CssClass = "textstyle";
        theCol2.ReadOnly = true;
        grdSearchResult.Columns.Add(theCol2);
        grdSearchResult.Columns[2].Visible = false;

        BoundField theCol3 = new BoundField();
        theCol3.HeaderText = "Units";
        theCol3.DataField = "UnitName";
        theCol3.ItemStyle.CssClass = "textstyle";
        theCol3.SortExpression = "UnitName";
        theCol3.ReadOnly = true;
        grdSearchResult.Columns.Add(theCol3);
        grdSearchResult.Columns[3].Visible = true;

        BoundField theCol4 = new BoundField();
        theCol4.HeaderText = "Lower Boundary";
        theCol4.DataField = "MinBoundaryValue";
        theCol4.ItemStyle.CssClass = "textstyle";
        theCol4.SortExpression = "MinBoundaryValue";
        theCol4.ReadOnly = true;
        grdSearchResult.Columns.Add(theCol4);
        grdSearchResult.Columns[4].Visible = true;

        BoundField theCol5 = new BoundField();
        theCol5.HeaderText = "Upper Boundary";
        theCol5.DataField = "MaxBoundaryValue";
        theCol5.ItemStyle.CssClass = "textstyle";
        theCol5.SortExpression = "MaxBoundaryValue";
        theCol5.ReadOnly = true;
        grdSearchResult.Columns.Add(theCol5);
        grdSearchResult.Columns[5].Visible = true;

        BoundField theCol6 = new BoundField();
        theCol6.HeaderText = "Default";
        theCol6.DataField = "DefaultUnit";
        theCol6.ItemStyle.CssClass = "textstyle";
        theCol6.ReadOnly = true;
        theCol6.SortExpression = "DefaultUnit";
        grdSearchResult.Columns.Add(theCol6);
        grdSearchResult.Columns[6].Visible = true;

        BoundField theCol7 = new BoundField();
        theCol7.HeaderText = "ID";
        theCol7.DataField = "ID";
        theCol7.ItemStyle.CssClass = "textstyle";
        theCol7.ReadOnly = true;
        grdSearchResult.Columns.Add(theCol7);
        grdSearchResult.Columns[7].Visible = false;

        BoundField theCol8 = new BoundField();
        theCol8.HeaderText = "Undetectable";
        theCol8.DataField = "undetectable";
        theCol8.ItemStyle.CssClass = "textstyle";
        theCol8.ReadOnly = true;
        theCol8.SortExpression = "undetectable";
        grdSearchResult.Columns.Add(theCol8);
        grdSearchResult.Columns[8].Visible = true;

        //grdSearchResult.DataSource = ((DataSet)ViewState["MstDS"]).Tables[1];
        grdSearchResult.DataBind();
    }
    #endregion

    //protected void Page_Init(object sender, EventArgs e)
    //{
        
    //    /***************** Check For User Rights ****************/
    //    AuthenticationManager Authentiaction = new AuthenticationManager();

    //    if (Authentiaction.HasFunctionRight(ApplicationAccess.Schedular, FunctionAccess.Update, (DataTable)Session["UserRight"]) == false)
    //    {
    //        //btnSubmit.Enabled = false;
    //        //btnNewAppointment.Enabled = false;
    //    }
    //    if (Authentiaction.HasFunctionRight(ApplicationAccess.Schedular, FunctionAccess.Add, (DataTable)Session["UserRight"]) == false)
    //    {
    //       // btnSubmit.Enabled = false;
    //        //btnNewAppointment.Enabled = false;
    //    }
    //}

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["AppLocation"] == null || Session.Count == 0 || Session["AppUserID"].ToString() == "")
        {
            IQCareMsgBox.Show("SessionExpired", this);
            Response.Redirect("~/frmlogin.aspx",true);
        }
        if (Session["AppLocation"] == null || Session.Count == 0 || Session["AppUserID"].ToString() == "")
        {
            IQCareMsgBox.Show("SessionExpired", this);
            Response.Redirect("~/frmlogin.aspx",true);
        }
        //(Master.FindControl("levelOneNavigationUserControl1").FindControl("lblRoot") as Label).Text = "Scheduler";
        //(Master.FindControl("levelOneNavigationUserControl1").FindControl("lblheader") as Label).Text = "";
        if (Request.QueryString["Fid"] != null)
        {
            ViewState["FID"] = Request.QueryString["Fid"].ToString();

        }
        
        if (Request.QueryString["sts"] != null)
        {
            //(Master.FindControl("lblpntStatus") as Label).Text = Request.QueryString["sts"].ToString();

        }
        if (!IsPostBack)
        {
            
            ViewState["a"] = "1"; 
            //---pr_Admin_GetSubTestDetails_Constella , lnk_LabValue rupesh
            ILabMst LabManager;
            LabManager = (ILabMst)ObjectFactory.CreateInstance("BusinessProcess.Administration.BLabMst, BusinessProcess.Administration");
            DataSet theDS = LabManager.GetSubTestDetails(Convert.ToInt32(Request.QueryString["LabId"]));
            ViewState["MstDS"] = theDS;

            fillDropDownList();
            ddUnit.SelectedIndex = 0;
            if (((DataSet)ViewState["MstDS"]).Tables[1].Rows.Count > 0)
            {
                lblSubTest.Text = ((DataSet)ViewState["MstDS"]).Tables[1].Rows[0][1].ToString();

                if (ViewState["grdDataSource"] == null)
                    ViewState["grdDataSource"] = ((DataSet)ViewState["MstDS"]).Tables[1];
                ViewState["SortDirection"] = "Desc";

                grdSearchResult.DataSource = ((DataSet)ViewState["MstDS"]).Tables[1];
                BindGrid();
            }
            {
                //give msg and go to prv page
            }
            //RegisterClientScriptBlock("setVisibility('divUnit','divBoundary','divDefault','divButtons', 'none');", "");
            //RegisterClientScriptBlock("HideShow();", "");
            
            
        }
        else
        {
            ViewState["a"] = "2";
        }

        lblSubTest.Text = ((DataSet)ViewState["MstDS"]).Tables[2].Rows[0][0].ToString();
        
        txtUpper.Attributes.Add("onkeyup", "chkNumeric('" + txtUpper.ClientID + "')");
        txtLower.Attributes.Add("onkeyup", "chkNumeric('" + txtLower.ClientID + "')");

        Page.EnableViewState = true;
        AuthenticationManager Authentication = new AuthenticationManager();
        if (Authentication.HasFunctionRight(Convert.ToInt32(ViewState["FID"]), FunctionAccess.Update, (DataTable)Session["UserRight"]) == false)
        {
            btnSave.Enabled = false;
        }
       
    }

    protected void grdSearchResult_Sorting(object sender, GridViewSortEventArgs e)
    {
        IQCareUtils clsUtil = new IQCareUtils();

        SortAndSetDataInGrid(e.SortExpression);
        if (ViewState["SortDirection"].ToString() == "Asc")
        {
            ViewState["SortDirection"] = "Desc";
        }
        else
        {
            ViewState["SortDirection"] = "Asc";
        }

    }

    protected void grdSearchResult_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        //So that when the user clicks on the row - the corresponding row is edited
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Attributes.Add("onmouseover", "this.style.cursor='pointer';this.style.BackColor='#666699';");
            e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor='';");
            e.Row.Attributes.Add("onclick",Page.ClientScript.GetPostBackEventReference(grdSearchResult, "Select$" + e.Row.RowIndex.ToString()));

            //e.Row.Attributes.Add("onclick", "setVisibility('divUnit','divBoundary','divDefault','divButtons', 'inline');" + Page.ClientScript.GetPostBackEventReference(,SetValues(e.Row.RowIndex)));
            //e.Row.Attributes.Add("onclick", "Show1('" + ddUnit.ClientID + "');"); 
            //grdSearchResult_SelectedIndexChanging(sender,(System.Web.UI.WebControls.GridViewSelectEventArgs) e.Row);
        }
    }
   
    protected void grdSearchResult_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        ddUnit.Enabled = false;
        txtOperation.Value = "EDIT";

        //RegisterClientScriptBlock("Run", "setVisibility('divUnit','divBoundary','divDefault','divButtons', 'inline');");
        //RegisterStartupScript("Run", "setVisibility('divUnit','divBoundary','divDefault','divButtons', 'inline');");
        
        //Edit the selected row
        int thePage = grdSearchResult.PageIndex;
        int thePageSize = grdSearchResult.PageSize;
        ddUnit.Visible = true;

        GridViewRow theRow = grdSearchResult.Rows[e.NewSelectedIndex];
        int theIndex = thePageSize * thePage + theRow.RowIndex;

        DataTable theDT = ((DataSet)ViewState["MstDS"]).Tables[1];
        int i = 0;
        foreach (DataRow theDR in theDT.Rows)
        {
            if (i == theIndex)
            {
                ViewState["SelID"] = theDR["ID"].ToString(); // SelectedID
                ddUnit.SelectedValue = theDR["UnitID"].ToString();
                txtLower.Text = theDR["MinBoundaryValue"].ToString();
                txtUpper.Text = theDR["MaxBoundaryValue"].ToString();
                if (theDR["DefaultUnit"].ToString() == "No")
                {
                    ddDefault.SelectedIndex = 0;
                    ViewState["Default"] = "No"; //storing the orginal default value yes/no for the seleted labtest
                }
                else
                {
                    ddDefault.SelectedIndex = 1;
                    ViewState["Default"] = "Yes";
                }
                if (theDR["undetectable"].ToString() == "No")
                {
                    ddldetect.SelectedIndex = 0;
                    
                }
                else
                {
                    ddldetect.SelectedIndex = 1;
                    
                }
                break;
            }
            i++;
        }
        
        
        //int LabId = Convert.ToInt32(theRow.Cells[0].Text.ToString());
        
        //string theUrl = string.Format("{0}LabId={1}", "frmAdmin_LabTestBoundary.aspx?name=" + "Edit" + "&", LabId);
        //Response.Redirect(theUrl);
        //RegisterClientScriptBlock("setVisibility('divUnit','divBoundary','divDefault','divButtons', 'inline');", "");
        
    }

    
    private void SortAndSetDataInGrid(String SortExpression)
    {
        IQCareUtils clsUtil = new IQCareUtils();
        DataView theDV;
        
        theDV = clsUtil.GridSort((DataTable)ViewState["grdDataSource"], SortExpression, ViewState["SortDirection"].ToString());

        //grdSearchResult.DataSource = null;
        grdSearchResult.Columns.Clear();

        grdSearchResult.DataSource = theDV;
        BindGrid();
    }

    protected void btnClose1_Click(object sender, EventArgs e)
    {
        Boolean blnDefault = false; 
        DataTable theDT=new DataTable();
        theDT = ((DataSet)ViewState["MstDS"]).Tables[1];
        foreach (DataRow theDR in theDT.Rows)
        {
            if (theDR["DefaultUnit"].ToString() == "Yes")
            {
                blnDefault = true;
                break;
            }
        }
        if (blnDefault ==true)
        {
            grdSearchResult.Dispose();
            Response.Redirect("frmAdmin_LaboratoryList.aspx?TableName=&CategoryId=0&LstName=Laboratory%20Configuration&Fid=" + Request.QueryString["FID"].ToString() + "&Upd=0&CCID=99&ModId=0");
        }
        else
        {
            IQCareMsgBox.Show("MandatoryDefault", this);
            ddDefault.Focus();
        }
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        //pr_Admin_SaveLabUnitLinks_Constella
        if (Validation() == true)
        {
            int defUnit = 0, detectable = 0;
            string OldID;

            ILabMst LabManager;
            LabManager = (ILabMst)ObjectFactory.CreateInstance("BusinessProcess.Administration.BLabMst, BusinessProcess.Administration");
            DataSet theDS = (DataSet)ViewState["MstDS"];

            //---find ID old Default unitl----

            DataView theDV=new DataView(theDS.Tables[1]);
            theDV.RowFilter = "DefaultUnit='Yes'";
            if (theDV.Count > 0)
            {
                OldID = theDV[0]["ID"].ToString();//ID of Old Default Unit


                if ((ddDefault.SelectedIndex == 1) && (txtOperation.Value == ""))
                {
                    DataTable theDT2 = LabManager.ChangeDefaultUnit(Convert.ToInt32(OldID));
                }
               
                if (ViewState["SelID"] != null)
                {
                    if (ViewState["SelID"].ToString() != "")
                    {
                        if ((ddDefault.SelectedIndex == 1) && (ViewState["SelID"].ToString() != OldID))
                        {
                            DataTable theDT2 = LabManager.ChangeDefaultUnit(Convert.ToInt32(OldID));
                        }
                    }
                }
            }


            if (ViewState["SelID"] != null) // for add
            {
                if (ViewState["SelID"].ToString() != "")
                {

                    //---edit old unit
                    foreach (DataRow theDR in theDS.Tables[1].Rows)
                    {
                        if (theDR["ID"].ToString() == ViewState["SelID"].ToString())
                        {
                            theDR["UnitID"] = ddUnit.SelectedValue.ToString();
                            theDR["UnitName"] = ddUnit.SelectedItem.ToString();
                            theDR["MinBoundaryValue"] = Convert.ToDecimal(txtLower.Text);
                            theDR["MaxBoundaryValue"] = Convert.ToDecimal(txtUpper.Text);
                            //if (ddDefault.SelectedIndex == 0)
                            //    theDR["DefaultUnit"] = "No";
                            //else
                            //    theDR["DefaultUnit"] = "Yes";

                               // ------- convert previous default unit to "No"---- 

                            if ((ddDefault.SelectedIndex == 0) & (theDS.Tables[1].Rows.Count >= 0))
                            {
                                theDR["DefaultUnit"] = "No";
                                defUnit = 0;
                            }
                            else
                            {
                                theDR["DefaultUnit"] = "Yes";
                                defUnit = 1;
                            }
                            if ((ddldetect.SelectedIndex == 0) & (theDS.Tables[1].Rows.Count >= 0))
                            {
                                theDR["undetectable"] = "No";
                                detectable = 0;
                            }
                            else
                            {
                                theDR["undetectable"] = "Yes";
                                detectable = 1;
                            }


                            DataTable theDT2 = LabManager.SaveLabUnitLinks(Convert.ToInt32(ViewState["SelID"]),
                            Convert.ToInt32(theDS.Tables[1].Rows[0][0]), Convert.ToDecimal(txtLower.Text), Convert.ToDecimal(txtUpper.Text),
                            Convert.ToInt32(ddUnit.SelectedValue), defUnit, detectable);
                            break;
                        }
                    }
                }
            }
            if ((ViewState["SelID"] == null))//for edit
            {
                //---add new unit
                DataRow theDR = theDS.Tables[1].NewRow();
                //theDR["SubTestID"] = theDS.Tables[1].Rows[0][0];//Request.QueryString["LabId"]
                theDR["SubTestID"] = Request.QueryString["LabId"];
                theDR["SubTestName"] = lblSubTest.Text.ToString();
                theDR["UnitID"] = ddUnit.SelectedValue.ToString();
                theDR["UnitName"] = ddUnit.SelectedItem.ToString();
                theDR["MinBoundaryValue"] = Convert.ToDecimal(txtLower.Text);
                theDR["MaxBoundaryValue"] = Convert.ToDecimal(txtUpper.Text);

                //---first unit should be "Yes" by default-----
                
                if ((ddDefault.SelectedIndex == 0) & (theDS.Tables[1].Rows.Count >= 0))
                {
                    theDR["DefaultUnit"] = "No";
                    defUnit = 0;
                }
                else
                {
                    theDR["DefaultUnit"] = "Yes";
                    defUnit = 1;
                }
                if ((ddldetect.SelectedIndex == 0) & (theDS.Tables[1].Rows.Count >= 0))
                {
                    theDR["undetectable"] = "No";
                    detectable = 0;
                }
                else
                {
                    theDR["undetectable"] = "Yes";
                    detectable = 1;
                }
                theDS.Tables[1].Rows.Add(theDR);

                DataTable theDT2 = LabManager.SaveLabUnitLinks(99999,
                            Convert.ToInt32(theDS.Tables[1].Rows[0][0]), Convert.ToDecimal(txtLower.Text), Convert.ToDecimal(txtUpper.Text),
                            Convert.ToInt32(ddUnit.SelectedValue), defUnit, detectable);
            }
            //--again get saved values from database)
            
            LabManager = (ILabMst)ObjectFactory.CreateInstance("BusinessProcess.Administration.BLabMst, BusinessProcess.Administration");
            theDS = LabManager.GetSubTestDetails(Convert.ToInt32(Request.QueryString["LabId"]));
            ViewState["MstDS"] = theDS;
            grdSearchResult.Columns.Clear();
            grdSearchResult.DataSource = ((DataSet)ViewState["MstDS"]).Tables[1];
            BindGrid();
            ViewState["SelID"] = null;
            ViewState["a"] = 1;
            
            txtLower.Text = "";
            txtUpper.Text = "";
            ddDefault.SelectedIndex = 0;
            ddUnit.Enabled = true;
            ddUnit.SelectedIndex = 0;


            ViewState["grdDataSource"] = ((DataSet)ViewState["MstDS"]).Tables[1];
            ViewState["SortDirection"] = "Desc";

            txtOperation.Value = "";// 11Jan08 -- it was creating duplicate default unit
            string theUrl;
            if(Request.QueryString["name"]=="Add")
                theUrl = "frmAdmin_LabTestBoundary.aspx?name=Add&FID=" + Request.QueryString["FID"].ToString();
            else
                theUrl = string.Format("{0}LabId={1}&FID={2}", "frmAdmin_LabTestBoundary.aspx?name=" + "Edit" + "&", Request.QueryString["LabId"].ToString(), Request.QueryString["FID"].ToString());

            Response.Redirect(theUrl);
        }
        int a = 2;
        
    }
    
    protected Boolean Validation()
    {
        string Msg = "";
        DataSet theDS = (DataSet)ViewState["MstDS"];
        //----incase of add - check
        //1.if same unit already exists
        //2. if default is being saved, then chk if anything else is already default

        //---- incase of edit - check
        //1. if default is being saved, then chk if anything else is already default

        if (txtLower.Text == "")
        {
            MsgBuilder theMsg = new MsgBuilder();
            theMsg.DataElements["Control"] = "Lower Boundary";
            IQCareMsgBox.Show("BlankTextBox", theMsg, this);
            return false;
        }
        if (txtUpper.Text == "")
        {
            MsgBuilder theMsg = new MsgBuilder();
            theMsg.DataElements["Control"] = "Upper Boundary";
            IQCareMsgBox.Show("BlankTextBox", theMsg, this);
            return false;
        }
        if (ddUnit.SelectedIndex==0)
        {
            MsgBuilder theMsg = new MsgBuilder();
            theMsg.DataElements["Control"] = "Unit";
            IQCareMsgBox.Show("BlankDropDown", theMsg, this);
            return false;
        }
        if (Convert.ToDecimal(txtLower.Text) > Convert.ToDecimal(txtUpper.Text))
        {
            IQCareMsgBox.Show("Boundary", this);
            txtUpper.Focus();
            return false;
        }
        
        //if (txtOperation.Value == "ADD")
        if (txtOperation.Value == "")
        {
            foreach (DataRow theDR in theDS.Tables[1].Rows)
            {
                if (theDR["UnitID"].ToString() == ddUnit.SelectedValue.ToString())
                {
                    IQCareMsgBox.Show("UnitExists", this);
                    ddUnit.Focus();
                    return false;
                }
                //if ((theDR["DefaultUnit"].ToString() == "Yes") && (ddDefault.SelectedIndex ==1))
                {
                    //IQCareMsgBox.Show("DefaultUnitExists", this);
                    //ddDefault.Focus();
                    //return false;
                }
            }
        }
        else if (txtOperation.Value == "EDIT")
        {
            //dont let any "Yes" to be converted to "No"
            if ((ViewState["Default"].ToString() == "Yes") && (ddDefault.SelectedIndex == 0)) //for selected labtest original is yes/no
            {
                IQCareMsgBox.Show("ChooseOtherDefault", this);
                ddDefault.Focus();
                return false;
            }
        }
        return true;
    }
    private void Test()
    { 
        //onclick="setVisibility('divUnit','divBoundary','divDefault','divButtons', 'inline');";> 
        //protected int funca(string a,out string x,params int[] b)
        //{
            
        //    //x = "jjj";
        //    //return 1;
        //}

        //string xyz;
        //funca("abc", out xyz, 5, 6, 7);

        //------------------------ 1 start -----------------------
        //        <script language="javascript" type="text/javascript">
            
        //    if('<%= ViewState["a"] %>'=='1')
        //    {
        //     document.getElementById('<%= divUnit.ClientID %>').style.display = 'none';
        //     document.getElementById('<%= divBoundary.ClientID %>').style.display = 'none';
        //     document.getElementById('<%= divDefault.ClientID %>').style.display = 'none';
        //     document.getElementById('<%= divButtons.ClientID %>').style.display = 'none';
        //    }
        //</script>
        //------------------------ 1 end -----------------------

        //------------------------ 2 start -----------------------
        //<tr>
        //            <td  align="right" > <!-- class="border pad5 whitebg"-->
        //                <table class = "center" width="100%"  border="0" cellpadding="0" cellspacing="6">
        //                    <tr>
        //                        <td class="border pad5 whitebg" valign="top" width="20%">
        //                            <div runat="server" id="divUnit">
        //                                <label >Units:</label>
        //                                <asp:DropDownList ID="ddUnit" runat="server"  Width="120px" >
        //                                </asp:DropDownList>
        //                            </div>
        //                        </td>
        //                        <td class="border pad5 whitebg" id="tdDate" runat="server" visible="true" valign="top" width="60%">
        //                            <div  runat="server" id="divBoundary">
        //                                <label>Boundary:        Lower</label>
        //                                <asp:TextBox id="txtLower" maxlength="11" runat="server" Width="70px" ></asp:TextBox>
        //                                <label>Upper:</label>
        //                                <asp:TextBox id="txtUpper" maxlength="11" runat="server" Width="70px"></asp:TextBox>
        //                            </div>
        //                        </td>
        //                        <td class="border pad5 whitebg" valign="top" width="20%">
        //                            <div  runat="server" id="divDefault" >
        //                                <label >Default:</label>
        //                                <asp:DropDownList ID="ddDefault" runat="server"  Width="70px" ></asp:DropDownList>
        //                            </div>
        //                        </td>
        //                    </tr>
        //                </table>
        //            </td>
        //        </tr>
        //------------------------ 2 end -----------------------
    }




    //protected void ddUnit_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    txtOperation.Value = "";//for Add //11Jan08
    //}
}
