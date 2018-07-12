using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using FarHorizon.Reservations.BusinessServices.Online.BAL;
using FarHorizon.Reservations.BusinessServices.Online.DAL;
using System.Data;
using System.Net.Mail;
using System.Text;
using FarHorizon.Reservations.Bases.BasePages;
public partial class ClientUI_PaymentreminderEmail : MasterBasePage
{
    DALBooking dlbooking = new DALBooking();
    BALBooking blbooking = new BALBooking();
    protected void Page_Load(object sender, EventArgs e)
    {
        
    }

    public void sendMail(string email, string name, int bookingid)
    {

        try
        {
            MailMessage mail = new MailMessage();
            SmtpClient SmtpServer = new SmtpClient("adventureresortscruises.in");
            mail.From = new MailAddress("reservations@adventureresortscruises.in");

            mail.To.Add(email);

            mail.Subject = "Mail Verification";

            Random rnd = new Random();
            string Code = rnd.Next(10000, 99999).ToString();


            StringBuilder sb = new StringBuilder();
            sb.Append("<div>");
            sb.Append("<div> Dear " + name + "and your Booking Id" + bookingid + ",</div> <div><br/></div><div>This is a reminder Email.Please deposite the due amount before your journy date</div> <div><br/> </div><div>For security reasons we have added this step so that we verify the email address before any booking details is sent across.</div> <div><br/></div> ");
            sb.Append("<div><br/> </div><div>Do contact us if you have any issue at reservations@adventureresort.com</div><div><br/></div><div>Thanking you,</div><div><br/></div><div>Reservations Office</div> ");
            sb.Append("</div>");
            sb.Append("<img src='http://adventureresortscruises.in/Cruise/booking/img_logo.png' alt='Image'/><br /><div> Adventure Resorts & Cruises Pvt. Ltd.</div><div> B209, CR Park, New Delhi 110019 </div> <div> Phone: +91 - 011 - 41057370 / 1 / 2 </div><div> Mobile: +91 - 9599755353 </div><div><br/> </div> ");

            mail.IsBodyHtml = true;
            mail.Body = sb.ToString();

            SmtpServer.Port = 587;
            SmtpServer.Credentials = new System.Net.NetworkCredential("reservations@adventureresortscruises.in", "Augurs@123");
            SmtpServer.EnableSsl = false;

            SmtpServer.Send(mail);
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Showstatus", "javascript:alert('Please check your Mail for the Verification Code')", true);

        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Showstatus", "javascript:alert('" + ex.Message.ToString() + "')", true);
            return;
        }

    }
    public void sendMailforfinal(string email, string name, int bookingid)
    {

        try
        {
            MailMessage mail = new MailMessage();
            SmtpClient SmtpServer = new SmtpClient("adventureresortscruises.in");
            mail.From = new MailAddress("reservations@adventureresortscruises.in");

            mail.To.Add(email);

            mail.Subject = "Mail Verification";

            Random rnd = new Random();
            string Code = rnd.Next(10000, 99999).ToString();


            StringBuilder sb = new StringBuilder();
            sb.Append("<div>");
            sb.Append("<div> Dear " + name + "and your Booking Id" + bookingid + ",</div> <div><br/></div><div>This is a final reminder Email.Please click on this link and after login fill the detail 'http://test.adventureresortscruises.in/Cruise/Booking/AfterBookingDetails2.aspx'</div> <div><br/> </div><div>For security reasons we have added this step so that we verify the email address before any booking details is sent across.</div> <div><br/></div> ");
            sb.Append("<div><br/> </div><div>Please click on below link for final payment</div><div><br/></div><div><br/>http://test.adventureresortscruises.in/Cruise/Booking/FinalpaymentLinkPage.aspx?Email=" + email + "</div><div><br/></div><div>Reservations Office</div> ");
            sb.Append("<div><br/> </div><div>Do contact us if you have any issue at reservations@adventureresortscruises.in</div><div><br/></div><div>Thanking you,</div><div><br/></div><div>Reservations Office</div> ");
            sb.Append("</div>");
            sb.Append("<img src='http://adventureresortscruises.in/Cruise/booking/img_logo.png' alt='Image'/><br /><div> Adventure Resorts & Cruises Pvt. Ltd.</div><div> B209, CR Park, New Delhi 110019 </div> <div> Phone: +91 - 011 - 41057370 / 1 / 2 </div><div> Mobile: +91 - 9599755353 </div><div><br/> </div> ");

            mail.IsBodyHtml = true;
            mail.Body = sb.ToString();

            SmtpServer.Port = 587;
            SmtpServer.Credentials = new System.Net.NetworkCredential("reservations@adventureresortscruises.in", "Augurs@123");
            SmtpServer.EnableSsl = false;

            SmtpServer.Send(mail);
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Showstatus", "javascript:alert('Please check your Mail for the Verification Code')", true);

        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Showstatus", "javascript:alert('" + ex.Message.ToString() + "')", true);
            return;
        }

    }
    protected void btnSend_Click(object sender, EventArgs e)
    {
        DataTable dt = dlbooking.paymentreminder(blbooking);
        if (dt != null && dt.Rows.Count > 0)
        {
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                sendMail(dt.Rows[i]["Email"].ToString(), dt.Rows[i]["FirstName"].ToString(), Convert.ToInt32(dt.Rows[i]["BookingID"].ToString()));
            }
            lblMsg.ForeColor = System.Drawing.Color.Green;
            lblMsg.Text = "Reminder mail sent successfully";
        }
    }

    protected void btnFinalreminderemail_Click(object sender, EventArgs e)
    {
        DataTable dt = dlbooking.finalpaymentremidermail(blbooking);
        if (dt != null && dt.Rows.Count > 0)
        {
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                sendMailforfinal(dt.Rows[i]["Email"].ToString(), dt.Rows[i]["FirstName"].ToString(), Convert.ToInt32(dt.Rows[i]["BookingID"].ToString()));
            }
            lblMsg.ForeColor = System.Drawing.Color.Green;
            lblMsg.Text = "Final Reminder mail sent successfully";
        }
    }
}