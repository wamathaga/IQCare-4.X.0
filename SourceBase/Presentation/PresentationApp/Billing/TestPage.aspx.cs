using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace IQCare.Web.Billing
{
    public partial class TestPage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected override void OnInit(EventArgs e)
        {
        /*    base.OnInit(e);
            this.ReversalForm.IsApprovalMode = true;
         //   this.ReversalForm.CanExpand = true;
            this.ReversalForm.CanRefund = true;
            this.ReversalForm.PatientID = 0;
            this.ReversalForm.RowCommand += new CommandEventHandler(ReversalForm_RowCommand);*/
           
        }

        void ReversalForm_RowCommand(object sender, CommandEventArgs e)
        {
            //throw new NotImplementedException();
        }
    }
}