using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PresentationApp.Laboratory.UserControl
{
    public partial class UserControl_LabOrderDetails : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        public void SetData(string LabName, DateTime LabDate)
        {
            txtlabnumber.Text = LabName;
            txtSpecLabOrderdt.SelectedDate = LabDate;
        } 
    }
}