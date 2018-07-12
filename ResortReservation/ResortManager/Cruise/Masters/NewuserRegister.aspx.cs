using FarHorizon.DataSecurity;
using FarHorizon.Reservations.BusinessServices;
using FarHorizon.Reservations.BusinessServices.Online.BAL;
using FarHorizon.Reservations.BusinessServices.Online.DAL;
using FarHorizon.Reservations.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Net.Mail;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
public partial class Cruise_Masters_NewuserRegister : System.Web.UI.Page
{
    #region Variable(s)
    DataTable Bookingdt;
    DataTable bookingmealdt;
    double TotalPaybleAmt = 0;
    public DataTable dtGetBookedRooms;
    public int LoopCounter = 0;

    BALAgentPayment blagentpayment = new BALAgentPayment();
    DALAgentPayment dlagentpayment = new DALAgentPayment();

    DALOpenDates dlOpenDates = new DALOpenDates();
    BALOpenDates blOpenDates = new BALOpenDates();

    BALSearch blsrch = new BALSearch();
    DALSearch dlsrch = new DALSearch();

    BALCustomers blcus = new BALCustomers();
    DALCustomers dlcus = new DALCustomers();
    //DataTable dtGetReturnedData;
    int getQueryResponse = 0;

    int CountryId = 0;

    /// <summary>
    /// These properties are required to preapre the return string to go to CruiseBooking Screen.
    /// </summary>
    string PackId; //=Pack1&
    string PackageName; //=7 night 8 day MV Mahabaahu Upstream Cruise&
    string NoOfNights; //=7&
    string CheckIndate; //=2/19/2017&
    string DepartureId; //=15
    #endregion

    string CompanyName = ConfigurationManager.AppSettings["cName"];
    string CompanyEmail = ConfigurationManager.AppSettings["cEmail"];
    string CompanyAddress = ConfigurationManager.AppSettings["cAddress"];
    string CompanyPhoneNo = ConfigurationManager.AppSettings["cPhoneNo"];
    string CompanyMobile = ConfigurationManager.AppSettings["cMobile"];
    string CompanyLogo = ConfigurationManager.AppSettings["cLogo"];
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["CustName"] != null)
        {
            //lblUsername.Text = "Hello " + Session["CustName"].ToString();
        }
    }
    public void ClientRegister()
    {
        try
        {
            blcus.action = "InsCustomers";
            blcus.Address1 = txtAddress1.Text;
            blcus.Address2 = txtaddress2.Text;
            blcus.City = txtCity.Text;
            Int32.TryParse(ddlCountry.SelectedValue, out CountryId);
            blcus.CountryId = CountryId;
            blcus.Email = txtMailAddress.Text.Trim();
            blcus.FirstName = txtFirstName.Text;
            Session["CustName"] = txtFirstName.Text;
            blcus.LastName = txtLastName.Text;
            blcus.PostalCode = txtPostcode.Text;
            blcus.State = txtState.Text;
            blcus.Telephone = txtTelephone.Text.Trim();
            blcus.Password = ViewState["CustPass"].ToString();
            blcus.Title = ddltitle.SelectedItem.Text;
            blcus.PaymentMethod = "Online";

            getQueryResponse = dlcus.AddCustomers(blcus);
            if (getQueryResponse > 0)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Showstatus", "javascript:alert('Verification Done! Please Login')", true);
            }
        }
        catch
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Showstatus", "javascript:alert('Please check entries')", true);
        }
    }

    public void sendMail()
    {
        try
        {
            MailMessage mail = new MailMessage();
            SmtpClient SmtpServer = new SmtpClient("adventureresortscruises.in");
            // mail.From = new MailAddress("reservations@adventureresortscruises.in");
            mail.From = new MailAddress("" + CompanyEmail + "");
            mail.To.Add(txtMailAddress.Text.Trim());
            mail.Subject = "Mail Verification";

            Random rnd = new Random();
            string Code = rnd.Next(10000, 99999).ToString();
            hfVCode.Value = Code;

            StringBuilder sb = new StringBuilder();
            sb.Append("<div>");
            sb.Append("<div> Dear " + txtFirstName.Text + ",</div> <div><br/></div><div>Thanks for your registering with us.</div> <div><br/> </div><div>For security reasons we have added this step so that we verify the email address before any booking details is sent across.</div> <div><br/></div> ");
            sb.Append(" <div>To verify your email address please enter the code " + Code + " in the registration screen. </div> <div><br/> </div><div>Do contact us if you have any issue at reservations@adventureresort.com</div><div><br/></div><div>Thanking you,</div><div><br/></div><div>Reservations Office</div> ");
            sb.Append("</div>");
            //sb.Append("<img src='http://adventureresortscruises.in/Cruise/booking/img_logo.png' alt='Image'/><br /><div> Adventure Resorts & Cruises Pvt. Ltd.</div><div> B209, CR Park, New Delhi 110019 </div> <div> Phone: +91 - 011 - 41057370 / 1 / 2 </div><div> Mobile: +91 - 9599755353 </div><div><br/> </div> ");
            sb.Append("<img src='" + CompanyLogo + "' alt='Image'/><br /><div> " + CompanyName + "</div><div> " + CompanyAddress + " </div> <div> Phone: " + CompanyPhoneNo + " </div><div> Mobile: " + CompanyMobile + " </div><div><br/> </div> ");
            mail.IsBodyHtml = true;
            mail.Body = sb.ToString();

            SmtpServer.Port = 587;
            SmtpServer.Credentials = new System.Net.NetworkCredential("" + CompanyEmail + "", "Augurs@123");
            SmtpServer.EnableSsl = false;

            SmtpServer.Send(mail);
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Showstatus", "javascript:alert('Please check your Mail for the Verification Code')", true);

            customerLogin.Visible = true;


            tableVerify.Visible = true;
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Showstatus", "javascript:alert('" + ex.Message.ToString() + "')", true);
        }
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        //ClientRegister();
        sendMail();
    }

    protected void btnVerify_Click(object sender, EventArgs e)
    {
        try
        {
            if (txtCode.Text == hfVCode.Value)
            {

                ClientRegister();
                txtCode.Text = "";
                tableVerify.Visible = false;
                Response.Redirect("SearchProperty.aspx");
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Showstatus", "javascript:alert('Wrong Code Entered!')", true);

                tableVerify.Visible = true;
            }
        }
        catch
        {
        }
    }
}