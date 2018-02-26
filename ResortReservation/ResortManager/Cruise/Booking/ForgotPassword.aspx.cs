using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text.RegularExpressions;
using FarHorizon.DataSecurity;
using FarHorizon.Reservations.BusinessServices;
using FarHorizon.Reservations.BusinessServices.Online.BAL;
using FarHorizon.Reservations.BusinessServices.Online.DAL;
using System.Data;
using System.Diagnostics;
using System.Net.Mail;
using System.Text;
using System.Configuration;
using FarHorizon.Reservations.MasterServices;

public partial class Cruise_Booking_ForgotPassword : System.Web.UI.Page
{
    BALCustomers blcus = new BALCustomers();
    DALCustomers dlcus = new DALCustomers();
    DALAgentPayment dagnt = new DALAgentPayment();
    BALAgentPayment bagnt = new BALAgentPayment();
    EventMessageMaster emsg = new EventMessageMaster();

    string CompanyName = ConfigurationManager.AppSettings["cName"];
    string CompanyEmail = ConfigurationManager.AppSettings["cEmail"];
    string CompanyAddress = ConfigurationManager.AppSettings["cAddress"];
    string CompanyPhoneNo = ConfigurationManager.AppSettings["cPhoneNo"];
    string CompanyMobile = ConfigurationManager.AppSettings["cMobile"];
    string CompanyLogo = ConfigurationManager.AppSettings["cLogo"];
    string SmtpUserId = ConfigurationManager.AppSettings["SMTPUserId"];
    string SmtpPassword = ConfigurationManager.AppSettings["SMTPPwd"];
    string SmtpHost = ConfigurationManager.AppSettings["SMTPServer"]; 
    protected void Page_Load(object sender, EventArgs e)
    {

        string st = DataSecurityManager.Encrypt("randeep@xportsoft.com");
        if (Session["CustomerMailId"] == null || Session["AgentMailId"] == null)
        {
            LinkButton1.Visible = false;
        }
        if (Session["CustName"] != null)
        {
            lblUsername.Text = "Hello " + Session["CustName"].ToString();
            //lnkCustomerRegis.Visible = false;
            navlogin.Visible = false;
            LinkButton1.Visible = true;
        }

        else if (Session["UserName"] != null)

        {
            lblUsername.Text = "Hello " + Session["UserName"].ToString();
            //lnkCustomerRegis.Visible = false;
            navlogin.Visible = false;
            LinkButton1.Visible = true;
        }
        else
        {
            //lnkCustomerRegis.Visible = false;
            navlogin.Visible = true;
            LinkButton1.Visible = false;
        }

    }
    public static Boolean IsEmailValid(string EmailAddr)
    {

        if (EmailAddr != null || EmailAddr != "")
        {

            Regex n = new Regex("(?<user>[^@]+)@(?<host>.+)");
            Match v = n.Match(EmailAddr);

            if (!v.Success || EmailAddr.Length != v.Length)
            {

                return false;
            }

            else

            {

                return true;
            }

        }

        else

        {

            return false;
        }

    }
    public void sendMail(string name, string password)
    {
        try
        {
            MailMessage mail = new MailMessage();
            SmtpClient SmtpServer = new SmtpClient(SmtpHost);
            //   mail.From = new MailAddress("reservations@adventureresortscruises.in");
            mail.From = new MailAddress(CompanyEmail);
            mail.To.Add(txtCustMailId.Value.Trim());
            // mail.Subject = "Mail Verification";
            mail.Subject = "Forgot Password";
            string msgsubject = "";
            Random rnd = new Random();
            string Code = rnd.Next(10000, 99999).ToString();
            //hfVCode.Value = Code;
            DataTable dt = emsg.getmessgaeforpassword();
            if (dt != null && dt.Rows.Count > 0)
            {
                msgsubject = dt.Rows[0]["EventMessage"].ToString();
            }
            StringBuilder sb = new StringBuilder();
            sb.Append("<div>");
            sb.Append(msgsubject);
            //sb.Append(" <div>Your password is: " + password + " . </div> <div><br/> </div><div>Do contact us if you have any issue at reservations@adventureresort.com</div><div><br/></div><div>Thanking you,</div><div><br/></div><div>Reservations Office</div> ");
            sb.Append(" <div>Your password is: " + password + " . </div> <div><br/> </div><div><br/></div><div>Thanking you,</div><div><br/></div><div>Reservations Office</div> ");
            sb.Append("</div>");
            //sb.Append("<img src='http://adventureresortscruises.in/Cruise/booking/ARC_Logo.jpg.png' alt='Image'/><br /><div> Adventure Resorts & Cruises Pvt. Ltd.</div><div> B209, CR Park, New Delhi 110019 </div> <div> Phone: +91 - 011 - 41057370 / 1 / 2 </div><div> Mobile: +91 - 9599755353 </div><div><br/> </div> ");
            sb.Append("<img src='" + CompanyLogo + "' alt='Image'/><br /><div> " + CompanyName + "</div><div> " + CompanyAddress + " </div> <div> Phone: " + CompanyPhoneNo + " </div><div> Mobile: " + CompanyMobile + " </div><div><br/> </div> ");
            mail.IsBodyHtml = true;
            mail.Body = sb.ToString();

            SmtpServer.Port = 587;
            // SmtpServer.Credentials = new System.Net.NetworkCredential("reservations@adventureresortscruises.in", "Augurs@123");
            // SmtpServer.Credentials = new System.Net.NetworkCredential("" + CompanyEmail + "", "Augurs@123");
            SmtpServer.Credentials = new System.Net.NetworkCredential(SmtpUserId, SmtpPassword);
            SmtpServer.EnableSsl = false;

            SmtpServer.Send(mail);
            //ScriptManager.RegisterStartupScript(this, this.GetType(), "Showstatus", "javascript:alert('Please check your Mail for the Password')", true);
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Showstatus", "javascript:alert('Password has been sent to your email id " + txtCustMailId.Value.Trim() + "')", true);
            //pnlCustReg.Visible = false;
            //customerLogin.Visible = true;

            //TableCust.Visible = false;
            //tableVerify.Visible = true;
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Showstatus", "javascript:alert('" + ex.Message.ToString() + "')", true);
        }
    }

    protected void btnBack_Click(object sender, EventArgs e)
    {

    }

    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        Session.Clear();
        Session.Abandon();
        Session.RemoveAll();
        System.Web.Security.FormsAuthentication.SignOut();
        Response.Redirect("SearchProperty1.aspx");
    }

    protected void btnVerify_Click(object sender, EventArgs e)
    {

    }

    protected void btnCustLogin_Click(object sender, EventArgs e)

    {
        if (rdbAgent.Checked == true || rdbCustomer.Checked == true)
        {

        }
        if (rdbAgent.Checked == true && rdbCustomer.Checked == true)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Showstatus", "javascript:alert('Please select only one type')", true);

            return;
        }
        if (rdbCustomer.Checked == true)
        {
            if (IsEmailValid(txtCustMailId.Value) == true)
            {

                if (Session["CustomerMailId"] == null)
                {
                    try
                    {
                       // blcus.Email = txtCustMailId.Value
                        blcus.Email = DataSecurityManager.Encrypt(txtCustMailId.Value.Trim());

                        blcus.action = "checkemail";
                        DataTable dtCustomer = dlcus.checkmail(blcus);
                        if (dtCustomer != null && dtCustomer.Rows.Count > 0)
                        {
                            string password = DataSecurityManager.Decrypt(dtCustomer.Rows[0]["Password"].ToString());
                            sendMail(txtCustMailId.Value, password);
                            txtCustMailId.Value = "";
                            rdbAgent.Checked = false;
                            rdbCustomer.Checked = false;
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "Showstatus", "javascript:alert('We have send an email with your password,Please check your email')", true);

                            //lblMsg.Text = "We have send an email with your password,Please check your email id";
                            //lblMsg.ForeColor = System.Drawing.Color.Green;
                            //string BookRef = dtCustomer.Rows[0]["FirstName"].ToString() + dtCustomer.Rows[0]["LastName"].ToString() + "X" + Convert.ToDouble(dtrpax.Compute("SUM(Pax)", string.Empty)).ToString() + "-" + "Direct Client";
                            //ViewState["BookRef"] = BookRef;

                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "Showstatus", "javascript:alert('This is not registered email')", true);
                            return;
                        }
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Showstatus", "javascript:alert('You are already log in')", true);
                    return;
                    //lblMsg.Text = "You are already log in";
                    //lblMsg.ForeColor = System.Drawing.Color.Red;
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Showstatus", "javascript:alert('Please enter valid email ')", true);
                return;
                //lblMsg.Text = "Please enter valid email id";
                //lblMsg.ForeColor = System.Drawing.Color.Red;
            }
        }
        if (rdbAgent.Checked == true)
        {
            if (IsEmailValid(txtCustMailId.Value) == true)
            {

                if (Session["AgentMailId"] == null)
                {
                    try
                    {
                        
                        // bagnt._EmailId = txtCustMailId.Value;
                        bagnt._EmailId = DataSecurityManager.Encrypt(txtCustMailId.Value.Trim());

                        bagnt._Action = "checkagentemail";
                        DataTable dtagent = dagnt.checkagentemail(bagnt);
                        if (dtagent != null && dtagent.Rows.Count > 0)
                        {
                            // string password = dtagent.Rows[0]["Password"].ToString();
                            string password = DataSecurityManager.Decrypt(dtagent.Rows[0]["Password"].ToString());                           
                            sendMail(txtCustMailId.Value, password);
                            txtCustMailId.Value = "";
                            rdbAgent.Checked = false;
                            rdbCustomer.Checked = false;
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "Showstatus", "javascript:alert('We have send an email with your password,Please check your email ')", true);
                            //lblMsg.Text = "We have send an email with your password,Please check your email id";
                            //lblMsg.ForeColor = System.Drawing.Color.Green;
                            //string BookRef = dtCustomer.Rows[0]["FirstName"].ToString() + dtCustomer.Rows[0]["LastName"].ToString() + "X" + Convert.ToDouble(dtrpax.Compute("SUM(Pax)", string.Empty)).ToString() + "-" + "Direct Client";
                            //ViewState["BookRef"] = BookRef;

                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "Showstatus", "javascript:alert('This is not registered email')", true);
                            return;
                        }
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Showstatus", "javascript:alert('You are already log in ')", true);
                    return;
                    //lblMsg.Text = "You are already log in";
                    //lblMsg.ForeColor = System.Drawing.Color.Red;
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Showstatus", "javascript:alert('Please enter valid email id')", true);
                return;
                //lblMsg.Text = "Please enter valid email id";
                //lblMsg.ForeColor = System.Drawing.Color.Red;
            }
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Showstatus", "javascript:alert('Please select type')", true);
            return;
            //lblMsg.Text = "Please select type";
            //lblMsg.ForeColor = System.Drawing.Color.Red;
        }
    }



    protected void btnCustLogin_Click1(object sender, EventArgs e)
    {

    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        Response.Redirect("../Masters/NewRegister.aspx");
    }
}