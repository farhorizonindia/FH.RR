using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Net.Mail;

public partial class testMail : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        ScriptManager.RegisterStartupScript(Page, this.GetType(), "alert", string.Format("alert('{0}');", "Page hit"), true);
        send_mail("Madhvendra", "123", "goodguybilly97@hotmail.com");
    }

    //sent email for password recovery
    public void send_mail(string Name, string uPass, string email)
    {
        string MailAdd, MailPwd;
        try
        {
            MailAdd = "reservations@adventureresortscruises.in";
            MailPwd = "Augurs@123";
            System.Net.Mail.MailMessage mail = new System.Net.Mail.MailMessage();
            mail.To.Add(email);
            mail.From = new MailAddress(MailAdd);
            mail.Subject = "Easy2Exams...Password Recovery!!!";
           mail.Body = "<div>Subject: Reservation -Anil Shukla;Ai0808124512863</div><div><br /></div> <div><div>Booking No: Ai0808124512863</div> <div> Date of Booking: 8/8/2016 </div> <div><br /> </div> <div> Dear Mr.Ladsaria,</div> <div><br /></div><div> Namaskar!Greetings from Assam, India!</div> <div><br /> </div><div> Thank you for booking 7N8D Downstream Cruise on MV Mahabaahu.</div> <div><br /></div> <div>The cruise showcases Living, Natural and Cultural History where silk and cotton vie your attention.  A cup of famous Assam tea beckons you over to the little known north eastern part of India.</div> <div><br /> </div> <div> This pristine destination unfolds the history of an ancient civilisation of the Tibeto - Burman Ahoms who reigned in the region for more than 600 years.The river brings you up close to the simplistic ways of a speckled tribal andmultiraciallife. </div><div> We take this opportunity to inform you that the final confirmation for the cruise is to be completed prior to day - 90.You will receive an automated e - reminder on day - 110 and another on day - 100.Please ignore if paid.<br /> </div> <div><br /> </div> <div> We look forward to your confirmation.</div> <div><br /> </div> <div> Appreciations!</div> <div><br /> </div> <div> TheMahabaahu Team!</div> </div><img src='http://adventureresortscruises.in/Cruise/booking/img_logo.png' alt='Image'/><br /><div> Adventure Resorts and Cruises Pvt. Ltd.</div><div> B209, CR Park, New Delhi 110019 </div> <div> Phone: <a href = 'tel:%2B91-011-41057370' value = '+911141057370' target = '_blank'> +91 - 011 - 41057370 </ a>/ 1 / 2 </div><div> Mobile: <a href = 'tel:91-9599755353' value = '+919599755353' target = '_blank'> +91 - 9599755353 </ a></div><div><br /> </div> <div>The booking policy of the cruise can be referred to at <a href='http://www.mahabaahucruiseindia.com/cruise-policy' target='_blank' data-saferedirecturl='https://www.google.com/url?hl=en&amp;q=http://www.mahabaahucruiseindia.com/cruise-policy&amp;source=gmail&amp;ust=1470139247045000&amp;usg=AFQjCNH3vyzjL507K4FspRY6TihAfogUug'>http://www.mahabaahucruiseindia.com/cruise-policy </a>.</div><div><br /></div><div> Enclosure:</div><div> Invoice for your booking is attached with this email.</div> ";
            mail.IsBodyHtml = true;
            System.Net.Mail.SmtpClient smtp = new System.Net.Mail.SmtpClient("adventureresortscruises.in");
            smtp.UseDefaultCredentials = false;
            smtp.Credentials = new System.Net.NetworkCredential(MailAdd, MailPwd);
            smtp.EnableSsl = false;
            smtp.Port = 587;
            smtp.Send(mail);
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(Page, this.GetType(), "alert", string.Format("alert('{0}');", ex.Message), true);
        }
    }

}