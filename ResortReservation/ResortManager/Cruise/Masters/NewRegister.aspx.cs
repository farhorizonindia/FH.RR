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
using System.Text.RegularExpressions;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
public partial class Cruise_Masters_NewRegister : System.Web.UI.Page
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
    string ccEmail = ConfigurationManager.AppSettings["ccEmail"];
    string SmtpUserId = ConfigurationManager.AppSettings["SMTPUserId"];
    string SmtpPassword = ConfigurationManager.AppSettings["SMTPPwd"];
    string SmtpHost = ConfigurationManager.AppSettings["SMTPServer"];
    protected void Page_Load(object sender, EventArgs e)
    {
        string t = "VDIzaDQ1aXMxMjRJNTRz";
        string PassportNo = DataSecurityManager.Decrypt(t.ToString());
        if (Session["CustName"] != null)
        {
            //lblUsername.Text = "Hello " + Session["CustName"].ToString();
        }
        if (!IsPostBack)
        {
            LoadCountries();
        }
    }
    private void LoadCountries()
    {
        try
        {
            blOpenDates._Action = "GetCountry";
            DataTable dtCountries = dlOpenDates.BindControls(blOpenDates);
            if (dtCountries.Rows.Count > 0)
            {
                ddlCountry.DataSource = dtCountries;
                ddlCountry.DataTextField = "CountryName";
                ddlCountry.DataValueField = "CountryId";
                ddlCountry.DataBind();
                ddlCountry.Items.Insert(0, new ListItem("-Select Country-", "0"));
            }
            else
            {
                ddlCountry.Items.Clear();
                ddlCountry.DataSource = null;
                ddlCountry.DataBind();
                ddlCountry.Items.Insert(0, new ListItem("-Select Country-", "0"));
            }
        }
        catch (Exception sqe)
        {
            ddlCountry.Items.Clear();
            ddlCountry.DataSource = null;
            ddlCountry.DataBind();
            ddlCountry.Items.Insert(0, "-No Country-");

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
            Session["CustMailId"] = txtMailAddress.Text.Trim();
            blcus.FirstName = txtFirstName.Text;
            Session["CustName"] = txtFirstName.Text;
            blcus.LastName = txtLastName.Text;
            blcus.PostalCode = txtPostcode.Text;
            blcus.State = txtState.Text;
            blcus.Telephone = txtTelephone.Text.Trim();
            Session["userpass"] = ViewState["CustPass"].ToString();
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
    public void checkpostpostcode()
    {
        if (txtPostcode.Text != null && txtPostcode.Text != "")
        {
            if (txtPostcode.Text.Length < 6)
            {
                Session["Phonecheck"] = 1;
            }
            else
            {
                try
                {
                    long post = Convert.ToInt64(txtPostcode.Text);
                }
                catch
                {
                    Session["Phonecheck"] = 1;
                }
            }
        }
    }
    public void sendMail()
    {
        if (Session["Phonecheck"] == null)
        {
            try
            {
                MailMessage mail = new MailMessage();

                SmtpClient SmtpServer = new SmtpClient(SmtpHost);

                // SmtpClient SmtpServer = new SmtpClient("adventureresortscruises.in");
             //   mail.From = new MailAddress("reservations@adventureresortscruises.in");
                mail.From = new MailAddress( CompanyEmail);
                mail.To.Add(txtMailAddress.Text.Trim());
                mail.Subject = "Mail Verification";

                Random rnd = new Random();
                string Code = rnd.Next(10000, 99999).ToString();
                hfVCode.Value = Code;

                StringBuilder sb = new StringBuilder();
                sb.Append("<div>");
                sb.Append("<div> Dear " + txtFirstName.Text + ",</div> <div><br/></div><div>Thanks for your registering with us.</div> <div><br/> </div><div>For security reasons we have added this step so that we verify the email address before any booking details is sent across.</div> <div><br/></div> ");
                sb.Append(" <div>To verify your email address please enter the code " + Code + " in the registration screen. </div> <div><br/> </div><div>Do contact us if you have any issue at reservations@adventureresortscruises.in</div><div><br/></div><div>Thanking you,</div><div><br/></div><div>Reservations Office</div> ");
                sb.Append("</div>");
                //   sb.Append("<img src='http://test1.adventureresortscruises.in/Cruise/booking/ARC_Logo.jpg.png' alt='Image'/><br /><div> Adventure Resorts & Cruises Pvt. Ltd.</div><div> B209, CR Park, New Delhi 110019 </div> <div> Phone: +91 - 011 - 41057370 / 1 / 2 </div><div> Mobile: +91 - 9599755353 </div><div><br/> </div> ");
                sb.Append("<img src='" + CompanyLogo + "' alt='Image'/><br /><div> " + CompanyName + "</div><div> " + CompanyAddress + " </div> <div> Phone: " + CompanyPhoneNo + " </div><div> Mobile: " + CompanyMobile + " </div><div><br/> </div> ");
                mail.IsBodyHtml = true;
                mail.Body = sb.ToString();

                SmtpServer.Port = 587;
                //SmtpServer.Credentials = new System.Net.NetworkCredential("reservations@adventureresortscruises.in", "Augurs@123");
                SmtpServer.Credentials = new System.Net.NetworkCredential(SmtpUserId, SmtpPassword);
                SmtpServer.EnableSsl = false;

                SmtpServer.Send(mail);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Showstatus", "javascript:alert('Please check your Mail for the Verification Code')", true);

                customerLogin.Visible = true;

                custRegis.Visible = false;
                pnllogin.Visible = false;
                tableVerify.Visible = true;
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Showstatus", "javascript:alert('" + ex.Message.ToString() + "')", true);
            }
        }
        else
        {
            Session["Phonecheck"] = null;
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Showstatus", "javascript:alert('PLease enter valid data into field')", true);
            return;
        }
    }
    protected void btnCustLogin_Click(object sender, EventArgs e)
    {
        if (Request.QueryString["bid"] != null)
        {
            try
            {
                string bid = Request.QueryString["bid"].ToString();
                blcus.Email = txtCustMailId.Text.Trim();
                blcus.Password = txtCustPass.Text.Trim();

                blcus.action = "LoginCustForTEntry";
                blcus.BookingId = Convert.ToInt32(bid);
                DataTable dtCustomer = dlcus.checkemailForTouristEntry(blcus);

                if (dtCustomer != null)
                {
                    if (dtCustomer.Rows.Count > 0)
                    {
                        Session["userpass"] = txtCustPass.Text.Trim();
                        Session["CustMailId"] = txtCustMailId.Text.Trim();
                        Session["CustName"] = DataSecurityManager.Decrypt(dtCustomer.Rows[0]["FirstName"].ToString());
                        Session["CustomerCode"] = DataSecurityManager.Decrypt(dtCustomer.Rows[0]["CustId"].ToString());
                        string bEmail = dtCustomer.Rows[0][4].ToString();
                        string bPassword = dtCustomer.Rows[0][13].ToString();
                        string Email = DataSecurityManager.Encrypt(txtCustMailId.Text.Trim());
                        string Password = DataSecurityManager.Encrypt(txtCustPass.Text.Trim());
                        if (bEmail == Email)
                        {
                            if (bPassword == Password)
                            {
                                if (Request.QueryString["bid"] != null)
                                {
                                    Response.Redirect("http://test1.adventureresortscruises.in/Cruise/Booking/Touristentry.aspx?bid=" + bid);
                                }
                            }
                            else
                            {
                                ScriptManager.RegisterStartupScript(this, this.GetType(), "Showstatus", "javascript:alert('Password  incorrect')", true);
                            }
                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "Showstatus", "javascript:alert('Email Id incorrect')", true);
                        }

                        //customerLogin.Visible = false;
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Showstatus", "javascript:alert('Password or Email Id incorrect')", true);
                    }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Showstatus", "javascript:alert('Password or Email Id incorrect')", true);
                }
                //Bookingdt = Session["Bookingdt"] as DataTable;
                //  preparetables(Bookingdt);

            }

            catch (Exception ex)
            {

            }
        }
        else
        {
            if (Session["CustMailId"] != null)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Showstatus", "javascript:alert('You are already login')", true);
                return;
            }
            else
            {
                try
                {
                    blcus.Email = txtCustMailId.Text.Trim();
                    blcus.Password = txtCustPass.Text.Trim();

                    blcus.action = "LoginCust";

                    DataTable dtCustomer = dlcus.checkDuplicateemail(blcus);

                    if (dtCustomer != null)
                    {
                        if (dtCustomer.Rows.Count > 0)
                        {
                            Session["userpass"] = txtCustPass.Text.Trim();
                            Session["CustMailId"] = txtCustMailId.Text.Trim();
                            Session["CustName"] = DataSecurityManager.Decrypt(dtCustomer.Rows[0]["FirstName"].ToString());
                            Session["CustomerCode"] = DataSecurityManager.Decrypt(dtCustomer.Rows[0]["CustId"].ToString());
                            if (Session["getavailable"] != null)
                            {
                                Response.Redirect(Session["getavailable"].ToString());
                            }
                            else
                            {
                                Response.Redirect("../Booking/searchproperty1.aspx");
                            }

                            //customerLogin.Visible = false;
                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "Showstatus", "javascript:alert('Password or Email Id incorrect')", true);
                        }
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Showstatus", "javascript:alert('Password or Email Id incorrect')", true);
                    }
                    //Bookingdt = Session["Bookingdt"] as DataTable;
                    //  preparetables(Bookingdt);

                }

                catch (Exception ex)
                {

                }
            }
        }
    }
    public void checkfield1()
    {
        if (txtFirstName.Text == "" || txtLastName.Text == "" || txtMailAddress.Text == "" || txtTelephone.Text == "" || txtAddress1.Text == "" || txtCity.Text == "" || txtState.Text == "" || txtPostcode.Text == "" || txtpassword.Text == "" || ddlCountry.SelectedIndex == 0)
        {

            Session["Phonecheck"] = 1;
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Showstatus", "javascript:alert('Plaese enter valid data')", true);
            return;
        }
    }
    private void ValidateEmail(string mail)
    {
        string email = mail;
        Regex regex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
        Match match = regex.Match(email);
        if (match.Success)
        {
            blcus.Email = mail;
            blcus.action = "chkDuplicate";
            DataTable dt = dlcus.checkDuplicateemail(blcus);
            if (dt != null && dt.Rows.Count > 0)
            {
                Session["Phonecheck"] = 1;
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Showstatus", "javascript:alert('This email is already register')", true);
                return;
            }
        }

        else
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Showstatus", "javascript:alert('Please enter valid email')", true);
            return;
        }
    }
    private void checkphone(string phone)
    {
        if (phone != null || phone != "")
        {
            try
            {
                long value = Convert.ToInt64(phone);
                if (value < 10)
                {
                    Session["Phonecheck"] = 1;
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Showstatus", "javascript:alert('Please enter valid phone no')", true);
                    return;
                }
            }
            catch
            {
                Session["Phonecheck"] = 1;
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Showstatus", "javascript:alert('Please enter valid phone no')", true);
                return;
            }
        }
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        if (txtpassword.Text != txtConfirmPassword.Text)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Showstatus", "javascript:alert('Password not match')", true);
            return;
        }
        else
        {
            checkfield1();
            ValidateEmail(txtMailAddress.Text);
            checkphone(txtTelephone.Text);
            checkpostpostcode();

            if (Session["CustName"] != null)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Showstatus", "javascript:alert('You are already login')", true);
                return;
            }
            else
            {
                ViewState["CustPass"] = txtpassword.Text;
                sendMail();
            }
        }
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
                if (Session["getavailable"] != null)
                {
                    Response.Redirect(Session["getavailable"].ToString());
                }
                else
                {
                    Response.Redirect("../Booking/searchproperty1.aspx");
                }

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

    protected void btnCustLogin_Click1(object sender, EventArgs e)
    {
        Response.Redirect("../Booking/ForgotPassword.aspx");
    }

    protected void Button2_Click(object sender, EventArgs e)
    {

    }
}
