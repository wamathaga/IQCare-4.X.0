using System;
using Application.Presentation;


public partial class Add_Edit_Education : LogPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["AppLocation"] == null || Session.Count == 0 || Session["AppUserID"].ToString() == "")
        {
            IQCareMsgBox.Show("SessionExpired", this);
            Response.Redirect("~/frmlogin.aspx",true);
        }

        lblH3.Text = Request.QueryString["name"];
        
        if (lblH3.Text == "Add")
        {
            
           txtEducationName.ReadOnly = true;
        }
        else if (lblH3.Text == "Edit")
        {
            txtEducationName.Text = "Primary";
            ddEducation.SelectedItem.Text = "Active";
        }
        else
            lblH3.Text = "Add/Edit Education";
    }
}
