using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Net.Mail;
namespace IQCareIssue_Report
{
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Utility objUtil = new Utility();
            string constr = objUtil.Decrypt(ConfigurationManager.AppSettings["SQLConnString"]);
            SqlConnection connection = new SqlConnection(constr);
            try
            {
                SqlDataAdapter sqlda = new SqlDataAdapter();
                sqlda.SelectCommand = new SqlCommand("pr_GetSeverity", connection);
                sqlda.SelectCommand.CommandType = CommandType.StoredProcedure;
                DataSet ds = new DataSet();
                sqlda.Fill(ds);
                DataTable dtSeverity = ds.Tables[0];
                DataTable dtDTComponent = ds.Tables[1];
                BindFunctions theBndMgr = new BindFunctions();
                theBndMgr.BindCombo(dtSeverity, "Severity", "ID", cmbseverity);
                theBndMgr.BindCombo(dtDTComponent, "component", "ID", cmbcomponent); 

            }
            catch (Exception)
            {
                Console.WriteLine("Error: " + e);
            }
            finally
            {
                connection.Close();
            }
        }

        protected void btnsave_Click(object sender, EventArgs e)
        {
            Utility objUtil = new Utility();
            SqlConnection myConn = new SqlConnection();
            myConn.ConnectionString = objUtil.Decrypt(ConfigurationManager.AppSettings["SQLConnString"]);
            myConn.Open();
            try
            {
                if (FileUpload1.HasFile)
                {
                    FileUpload1.SaveAs("C:\\IQCare_Uploadfile\\" + FileUpload1.FileName);
                }
                SqlCommand com = new SqlCommand("pr_SaveDefect", myConn);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("@Name", txtname.Text);
                com.Parameters.AddWithValue("@email", txtemail.Text);
                com.Parameters.AddWithValue("@component", cmbcomponent.SelectedValue);
                com.Parameters.AddWithValue("@severity", cmbseverity.SelectedValue);
                com.Parameters.AddWithValue("@step", txtreproduce.Text);
                com.Parameters.AddWithValue("@summary", txtsummary.Text);
                com.Parameters.AddWithValue("@fileupload", FileUpload1.FileName);
                com.Parameters.AddWithValue("@version", txtversion.Text);
                int i = (int)com.ExecuteScalar();
                if (i > 0)
                {
                    string script = "<script language = 'javascript' defer ='defer' id = 'confirm'>\n";
                    script += "javascript:alert('Thanks you. Defects is been submitted to technical team, will be resolved soon.');\n";
                    script += "</script>\n";
                    RegisterStartupScript("confirm", script);
                }
                SendEmail(txtemail.Text, txtversion.Text, txtsummary.Text);
            }
            catch (Exception)
            {
                Console.WriteLine("Error: " + e);
            }
            finally
            {
                myConn.Close();
            }
        }

        protected void btnreset_Click(object sender, EventArgs e)
        {
            txtname.Text = "";
            txtemail.Text = "";
            cmbcomponent.SelectedValue= "0";
            cmbseverity.SelectedValue= "0";
            txtreproduce.Text = "";
            txtsummary.Text = "";
            txtversion.Text = ""; 
        }

        protected string SendEmail(string toAddress, string subject, string body)
        {
            //string result = "Message Sent Successfully..!!";
            //var fromAddress = new MailAddress("iqcaredefect@gmail.com", "IQCare Issue");
            //var toRAdd = new MailAddress("iqsupport@futuresgroup.com", "To Name");
            //var toaddressCC = new MailAddress("" + txtemail.Text + "", "To Name");
            //var toaddressBCC = new MailAddress("ADwivedi@futuresgroup.com", "To Name");
            ////var toaddressBCC = new MailAddress("jkumar@futuresgroup.com", "To Name");
            //const string fromPassword = "c0nste!!a";
            //try
            //{
            //    var smtp = new SmtpClient
            //    {
            //        Host = "smtp.gmail.com",
            //        Port = 587,
            //        EnableSsl = true,
            //        DeliveryMethod = SmtpDeliveryMethod.Network,
            //        Credentials = new System.Net.NetworkCredential(fromAddress.Address, fromPassword),
            //        Timeout = 50000
            //    };
            //    using (var Rmessage = new MailMessage(fromAddress, toRAdd)
            //    {
            //        Subject = txtsummary.Text,
            //        Body = txtreproduce.Text
            //    })
            //    //CC
            //    using (var CCMessage = new MailMessage(fromAddress, toaddressCC)
            //    {
            //        Subject = txtsummary.Text,
            //        Body = txtreproduce.Text
            //    })
            //    //BCC
            //    using (var BCCMessage = new MailMessage(fromAddress, toaddressBCC)
            //    {
            //        Subject = txtsummary.Text,
            //        Body = txtreproduce.Text
            //    })
            //    {
            //        smtp.Send(Rmessage);
            //        smtp.Send(CCMessage);
            //        smtp.Send(BCCMessage);
            //    }
            //}
            //catch (Exception ex)
            //{
            //    result = "Error sending email.!!!";
            //}
            //return result;

            string result = "Message Sent Successfully..!!";
            var fromAddress = new MailAddress("iqsupport@futuresgroup.com", "IQCare Issue");
            var toRAdd = new MailAddress("iqsupport@futuresgroup.com", "To Name");
            var toaddressCC = new MailAddress("" + txtemail.Text + "", "To Name");
            var toaddressBCC = new MailAddress("ADwivedi@futuresgroup.com", "To Name");
            //var toaddressBCC = new MailAddress("Jkumar@futuresgroup.com", "To Name");
            const string fromPassword = "!QSupp0rt!";
            try
            {
                var smtp = new SmtpClient
                {
                    Host = "outlook.office365.com",
                    Port = 587,
                    EnableSsl = true,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    Credentials = new System.Net.NetworkCredential(fromAddress.Address, fromPassword),
                 };
                //recipient
                using (var Rmessage = new MailMessage(fromAddress, toRAdd)
                {
                    Subject = txtsummary.Text,
                    Body = txtreproduce.Text
                })
                //CC
                using (var CCMessage = new MailMessage(fromAddress, toaddressCC)
                {
                    Subject = txtsummary.Text,
                    Body = txtreproduce.Text
                })
                //BCC
                using (var BCCMessage = new MailMessage(fromAddress, toaddressBCC)
                {
                    Subject = txtsummary.Text,
                    Body = txtreproduce.Text
                })
                {
                    smtp.Send(Rmessage);
                    smtp.Send(CCMessage);
                    smtp.Send(BCCMessage);
                }
            }
            catch (Exception ex)
            {
                result = "Error sending email.!!!";
            }
            return result;
       }
    }
}
