using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Application.Presentation;
using Application.Common;
using Interface.Scheduler;
using System.Data;

namespace PresentationApp.ClinicalForms.UserControl
{
    public partial class UserControlKNH_NextAppointment : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            addAttributes();
        }

        protected void btnPrescribeDrugs_Click(object sender, EventArgs e)
        {
            IQCareUtils.Redirect("../Scheduler/frmScheduler_AppointmentNewHistory.aspx?name=Add&FormName=Appointment History New&opento=popup", "_blank", "toolbars=no,location=no,directories=no,dependent=yes,top=100,left=30,maximize=no,resize=no,width=1000,height=800,scrollbars=yes");
        }

        public void addAttributes()
        {
            rdoTCAYes.Attributes.Add("OnClick", "ShowHide('trNextAppointment','show');ShowHide('trCareEnd','hide');");
            rdoTCANo.Attributes.Add("OnClick", "ShowHide('trCareEnd','show');ShowHide('trNextAppointment','hide');");
        }

        protected void btnCareEnd_Click(object sender, EventArgs e)
        {
            ICareEnded CEControl = (ICareEnded)ObjectFactory.CreateInstance("BusinessProcess.Scheduler.BCareEnded,BusinessProcess.Scheduler");
            DataSet theDS = CEControl.GetDynamicControl(Convert.ToInt32(Session["TechnicalAreaId"]));
            if (theDS.Tables.Count>2)
            {
                if(theDS.Tables[3].Rows.Count > 0)
                    IQCareUtils.Redirect("../Scheduler/frmScheduler_ContactCareTracking.aspx?opento=popup", "_blank", "toolbars=no,location=no,directories=no,dependent=yes,top=100,left=30,maximize=no,resize=no,width=1000,height=500,scrollbars=yes");
                else
                {
                    // Get a ClientScriptManager reference from the Page class.
                    ClientScriptManager cs = Page.ClientScript;
                    Type cstype = this.GetType();
                    // Check to see if the startup script is already registered.
                    if (!cs.IsStartupScriptRegistered(cstype, "CareEndScript"))
                    {

                        String script = "";
                        script += "alert('You have not configure CareEnd. Please configure careend form first.');\n";

                        cs.RegisterStartupScript(cstype, "CareEndScript", script, true);
                    }

                }
            }
            else
            {                
                // Get a ClientScriptManager reference from the Page class.
                ClientScriptManager cs = Page.ClientScript;
                Type cstype = this.GetType();
                // Check to see if the startup script is already registered.
                if (!cs.IsStartupScriptRegistered(cstype, "CareEndScript"))
                {

                    String script = "";                    
                    script += "alert('You have not configure CareEnd. Please configure careend form first.');\n";                  
                    
                    cs.RegisterStartupScript(cstype, "CareEndScript", script, true);
                }
                
            }
        }
    }
}