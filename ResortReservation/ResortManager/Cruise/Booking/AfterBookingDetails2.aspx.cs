using FarHorizon.DataSecurity;
using FarHorizon.Reservations.BusinessServices;
using FarHorizon.Reservations.BusinessServices.Online.BAL;
using FarHorizon.Reservations.BusinessServices.Online.DAL;
using FarHorizon.Reservations.Common;
using System;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Net.Mail;
using System.Text.RegularExpressions;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Cruise_Booking_AfterBookingDetails2 : System.Web.UI.Page
{
    DataTable Bookingdt;
    DataTable bookingmealdt;
    DataTable dtroominfo;
    double TotalPaybleAmt = 0;
    public DataTable dtGetBookedRooms;
    public int LoopCounter = 0;
    public string imagepath;
    public string type;
    BALAgentPayment blagentpayment = new BALAgentPayment();
    DALAgentPayment dlagentpayment = new DALAgentPayment();

    DALOpenDates dlOpenDates = new DALOpenDates();
    BALOpenDates blOpenDates = new BALOpenDates();

    BALCustomers blcus = new BALCustomers();
    DALCustomers dlcus = new DALCustomers();
    //DataTable dtGetReturnedData;
    int getQueryResponse = 0;
    string email = "";
    string password = "";
    int CountryId = 0;
    double total1 = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["CustName"] != null)
        {
            lblUsername.Text = "Hello " + Session["CustName"].ToString();
            //lnkCustomerRegis.Visible = false;
            navlogin.Visible = false;
            LinkButton1.Visible = true;
            //dvpayment.Visible = true;
            dvreg.Visible = false;
            dvBilling.Visible = true;
            divspcl.Visible = true;
            BookRef.Visible = false;
            if (Session["HCheckin"] != null)
            {
                lblBalancedate.Text = Convert.ToDateTime(Session["HCheckin"].ToString()).AddDays(-90).ToString("dddd, MMMM d, yyyy");
            }
        }

        else if (Session["UserName"] != null)

        {
            lblUsername.Text = "Hello " + Session["UserName"].ToString();
            //lnkCustomerRegis.Visible = false;
            navlogin.Visible = false;
            LinkButton1.Visible = true;
            //dvpayment.Visible = true;
            dvBilling.Visible = true;
            dvreg.Visible = false;
            divspcl.Visible = true;
            if (Session["HCheckin"] != null)
            {
                lblBalancedate.Text = Convert.ToDateTime(Session["HCheckin"].ToString()).AddDays(-90).ToString("dddd, MMMM d, yyyy");
            }
        }
        else
        {
            //lnkCustomerRegis.Visible = false;
            navlogin.Visible = true;
            LinkButton1.Visible = false;
            //dvpayment.Visible = false;
            dvBilling.Visible = false;
            dvreg.Visible = true;
            divspcl.Visible = false;
        }

        if (!IsPostBack)
        {
            if (Session["UserCode"] != null || Session["CustomerCode"] != null)
            {
                pnllogin.Visible = false;
                custRegis.Visible = false;
                if (Session["userpass"] != null)
                {
                    password = Session["userpass"].ToString();
                }
                if (Session["CustMailId"] != null)
                {
                    email = Session["CustMailId"].ToString();
                    showfulldetails(email, password);
                }



                //if (Session["UserCode"] != null)
                //{
                //    BookRef.Style.Remove("display");
                //    ReqBookRef.Enabled = true;
                //}
                //else
                //{
                //    BookRef.Style.Add("display", "None");
                //    ReqBookRef.Enabled = false;
                //}
                //lnkLogout.Visible = true;
            }
            else
            {
                pnllogin.Visible = true;
                custRegis.Visible = true;
                //dvpanel.Visible = true;
                dvRefrence.Visible = true;

            }
            if (Session["CustMailId"] != null)
            {
                this.pnlFullDetails.Visible = true;
                pnlBookButton.Visible = true;
                panelwithoutCreditAgent.Visible = true;
                dvRefrence.Visible = false;
            }
            else
            {
                this.pnlFullDetails.Visible = false;
                pnlBookButton.Visible = false;
                panelwithoutCreditAgent.Visible = false;
                dvRefrence.Visible = true;
            }
            //this.pnllogin.Visible = false;

            Bookingdt = new DataTable();
            Bookingdt = SessionServices.RetrieveSession<DataTable>("Bookingdt");
            bookingmealdt = new DataTable();
            bookingmealdt = SessionServices.RetrieveSession<DataTable>("BookinMealdt");
            dtroominfo = new DataTable();
            dtroominfo = SessionServices.RetrieveSession<DataTable>("RoomInfo");

            loaddata();

            //lblChkin.Text = Session["Chkin"].ToString();
            //lblChkout.Text = Session["chkout"].ToString();

            //calcamt(Bookingdt);
            LoadCountries();

            //pnlCustReg.Visible = false;
            //customerLogin.Visible = false;
        }
    }
    private void LoadCountries()
    {
        try
        {
            blOpenDates._Action = "GetCountry";
            DataTable dtGetReturnedData = dlOpenDates.BindControls(blOpenDates);
            if (dtGetReturnedData.Rows.Count > 0)
            {
                ddlCountry.DataSource = dtGetReturnedData;
                ddlCountry.DataTextField = "CountryName";
                ddlCountry.DataValueField = "CountryId";
                ddlCountry.DataBind();
                ddlCountry.Items.Insert(0, new ListItem("-Select Country-", "0"));
                ddlCountry1.DataSource = dtGetReturnedData;
                ddlCountry1.DataTextField = "CountryName";
                ddlCountry1.DataValueField = "CountryId";
                ddlCountry1.DataBind();
                ddlCountry1.Items.Insert(0, new ListItem("-Select Country-", "0"));
            }
            else
            {
                ddlCountry.Items.Clear();
                ddlCountry.DataSource = null;
                ddlCountry.DataBind();
                ddlCountry.Items.Insert(0, new ListItem("-Select Country-", "0"));
                ddlCountry1.Items.Clear();
                ddlCountry1.DataSource = null;
                ddlCountry1.DataBind();
                ddlCountry1.Items.Insert(0, new ListItem("-Select Country-", "0"));

            }
        }
        catch (Exception sqe)
        {
            ddlCountry.Items.Clear();
            ddlCountry.DataSource = null;
            ddlCountry.DataBind();
            ddlCountry.Items.Insert(0, "-No Accom-");
            ddlCountry1.Items.Clear();
            ddlCountry1.DataSource = null;
            ddlCountry1.DataBind();
            ddlCountry1.Items.Insert(0, "-No Accom-");

        }
    }
    private void loaddata()
    {
        try
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("Image", typeof(string));
            dt.Columns.Add("Type", typeof(string));
            //dt.Columns.Add("bedtypoe", typeof(string));
            dt.Columns.Add("totPax", typeof(int));
            dt.Columns.Add("roomtype", typeof(string));
            dt.Columns.Add("description", typeof(string));
            dt.Columns.Add("rate", typeof(string));
            Bookingdt = new DataTable();
            Bookingdt = SessionServices.RetrieveSession<DataTable>("Bookingdt");
            bookingmealdt = new DataTable();
            bookingmealdt = SessionServices.RetrieveSession<DataTable>("BookinMealdt");
            dtroominfo = new DataTable();
            dtroominfo = SessionServices.RetrieveSession<DataTable>("RoomInfo");
            if (dtroominfo.Rows[0]["ImagePath"].ToString() != null || dtroominfo.Rows[0]["ImagePath"].ToString() != "")
            {
                imagepath = dtroominfo.Rows[0]["ImagePath"].ToString();
            }
            else
            {
                imagepath = "images/project-4.jpg";
            }
            if (Bookingdt.Rows[0]["ConvDouble"].ToString() == "true")
            {
                type = "Yes";
            }
            else
            {
                type = "No";
            }
            dt.Rows.Add(imagepath, type, Convert.ToInt32(Bookingdt.Rows[0]["Pax"].ToString()), dtroominfo.Rows[0]["RoomType"].ToString(), dtroominfo.Rows[0]["description"].ToString(), dtroominfo.Rows[0]["Amtc"].ToString());
            DataTable dt12 = new DataTable();
            if (Session["foraddroom"] != null)
            {
                dt12 = Session["foraddroom"] as DataTable;
            }
            if (dt12 != null && dt12.Rows.Count > 0)
            {
                for (int i = 0; i < dt12.Rows.Count; i++)
                {
                    total1 = total1 + Convert.ToDouble(dt12.Rows[0]["Total1"].ToString());
                }
                lblAllTotal.Text = "Total INR " + total1.ToString();
            }
            gdvHotelRoomRates.DataSource = dt12;
            gdvHotelRoomRates.DataBind();

        }
        catch (Exception ex) { }

    }


    private void showfulldetailsforguest(string emailid)
    {
        try
        {
            blcus.Email = emailid;


            blcus.action = "getforguest";

            DataTable dtCustomer = dlcus.checkDuplicateemail(blcus);

            if (dtCustomer != null)
            {
                if (dtCustomer.Rows.Count > 0)
                {
                    dvRefrence.Visible = false;
                    buttonUpdate.Visible = true;

                    Bookingdt = new DataTable();
                    Bookingdt = SessionServices.RetrieveSession<DataTable>("Bookingdt");
                    calcamt(Bookingdt);
                    ViewState["Pass"] = txtCustPass.Text.Trim();
                    lblBillingAddress.Text = dlcus.GetBillingAddress(dtCustomer.Rows[0]);




                    lblAgentName.Text = DataSecurityManager.Decrypt(dtCustomer.Rows[0]["FirstName"].ToString()) + " " + DataSecurityManager.Decrypt(dtCustomer.Rows[0]["LastName"].ToString());
                    lbPaymentMethod.Text = DataSecurityManager.Decrypt(dtCustomer.Rows[0]["PaymentMethod"].ToString());
                    hdnfPhoneNumber.Value = DataSecurityManager.Decrypt(dtCustomer.Rows[0]["Telephone"].ToString());

                    Session["CustId"] = dtCustomer.Rows[0]["CustId"].ToString();
                    Session["CustomerCode"] = dtCustomer.Rows[0]["CustId"].ToString();

                    Session.Add("CustomerMailId", DataSecurityManager.Decrypt(dtCustomer.Rows[0]["Email"].ToString()));
                    Session.Add("CustPassword", DataSecurityManager.Decrypt(dtCustomer.Rows[0]["Password"].ToString()));
                    DataTable dtrpax = SessionServices.RetrieveSession<DataTable>("Bookingdt");
                    if (Session["HCheckin"] != null)
                    {
                        lblBalancedate.Text = Convert.ToDateTime(Session["HCheckin"].ToString()).AddDays(-90).ToString("dddd, MMMM d, yyyy");
                    }
                    string BookRef = DataSecurityManager.Decrypt(dtCustomer.Rows[0]["FirstName"].ToString()) + DataSecurityManager.Decrypt(dtCustomer.Rows[0]["LastName"].ToString()) + "X" + Convert.ToDouble(dtrpax.Compute("SUM(Pax)", string.Empty)).ToString() + "-" + "Self";
                    ViewState["BookRef"] = BookRef;
                    lbPaymentMethod.Text = "N/A";
                    tblBref.Visible = false;

                    pnlFullDetails.Visible = true;
                    panelwithoutCreditAgent.Visible = true;
                    pnlBookButton.Visible = true;
                    pnllogin.Visible = false;
                    custRegis.Visible = false;

                    //customerLogin.Visible = false;
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Showstatus", "javascript:alert('Email Id incorrect')", true);
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Showstatus", "javascript:alert('Email Id incorrect')", true);
            }
            //Bookingdt = Session["Bookingdt"] as DataTable;
            //  preparetables(Bookingdt);

        }

        catch (Exception ex)
        {

        }
    }
    private void showfulldetails(string emailid, string custpassword)
    {
        try
        {
            blcus.Email = emailid;

            if (ViewState["CustPass"] != null)
            {
                blcus.Password = ViewState["CustPass"].ToString();
            }
            else
            {
                blcus.Password = custpassword;
            }
            blcus.action = "LoginCust";

            DataTable dtCustomer = dlcus.checkDuplicateemail(blcus);

            if (dtCustomer != null)
            {
                if (dtCustomer.Rows.Count > 0)
                {
                    dvRefrence.Visible = false;
                    buttonUpdate.Visible = true;

                    Bookingdt = new DataTable();
                    Bookingdt = SessionServices.RetrieveSession<DataTable>("Bookingdt");
                    calcamt(Bookingdt);
                    ViewState["Pass"] = txtCustPass.Text.Trim();
                    lblBillingAddress.Text = dlcus.GetBillingAddress(dtCustomer.Rows[0]);



                    lblAgentName.Text = DataSecurityManager.Decrypt(dtCustomer.Rows[0]["FirstName"].ToString()) + " " + DataSecurityManager.Decrypt(dtCustomer.Rows[0]["LastName"].ToString());
                    lbPaymentMethod.Text = DataSecurityManager.Decrypt(dtCustomer.Rows[0]["PaymentMethod"].ToString());
                    hdnfPhoneNumber.Value = DataSecurityManager.Decrypt(dtCustomer.Rows[0]["Telephone"].ToString());

                    Session["CustId"] = dtCustomer.Rows[0]["CustId"].ToString();
                    Session["CustomerCode"] = dtCustomer.Rows[0]["CustId"].ToString();

                    Session.Add("CustomerMailId", DataSecurityManager.Decrypt(dtCustomer.Rows[0]["Email"].ToString()));
                    Session.Add("CustPassword", DataSecurityManager.Decrypt(dtCustomer.Rows[0]["Password"].ToString()));
                    DataTable dtrpax = SessionServices.RetrieveSession<DataTable>("Bookingdt");

                    string BookRef = DataSecurityManager.Decrypt(dtCustomer.Rows[0]["FirstName"].ToString()) + DataSecurityManager.Decrypt(dtCustomer.Rows[0]["LastName"].ToString()) + "X" + Convert.ToDouble(dtrpax.Compute("SUM(Pax)", string.Empty)).ToString() + "-" + "Self";
                    ViewState["BookRef"] = BookRef;
                    lbPaymentMethod.Text = "N/A";


                    pnlFullDetails.Visible = true;
                    panelwithoutCreditAgent.Visible = true;
                    pnlBookButton.Visible = true;
                    pnllogin.Visible = false;
                    custRegis.Visible = false;

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

    protected void btnCustLogin_Click(object sender, EventArgs e)
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
                    dvRefrence.Visible = false;
                    buttonUpdate.Visible = true;
                    Session["userpass"] = txtCustPass.Text.Trim();
                    Session["CustMailId"] = txtCustMailId.Text.Trim();
                    Bookingdt = new DataTable();
                    Bookingdt = SessionServices.RetrieveSession<DataTable>("Bookingdt");
                    calcamt(Bookingdt);
                    ViewState["Pass"] = txtCustPass.Text.Trim();
                    Session["CustName"] = DataSecurityManager.Decrypt(dtCustomer.Rows[0]["FirstName"].ToString());
                    Session["CustomerCode"] = DataSecurityManager.Decrypt(dtCustomer.Rows[0]["CustId"].ToString());


                    lblBillingAddress.Text = dlcus.GetBillingAddress(dtCustomer.Rows[0]);
                    if (Session["HCheckin"] != null)
                    {
                        lblBalancedate.Text = "(75% of total) to be paid prior to " + Convert.ToDateTime(Session["HCheckin"].ToString()).AddDays(-30).ToString("dddd, MMMM d, yyyy");
                    }

                    lblAgentName.Text = DataSecurityManager.Decrypt(dtCustomer.Rows[0]["FirstName"].ToString()) + " " + DataSecurityManager.Decrypt(dtCustomer.Rows[0]["LastName"].ToString());
                    lbPaymentMethod.Text = DataSecurityManager.Decrypt(dtCustomer.Rows[0]["PaymentMethod"].ToString());
                    hdnfPhoneNumber.Value = DataSecurityManager.Decrypt(dtCustomer.Rows[0]["Telephone"].ToString());

                    Session["CustId"] = DataSecurityManager.Decrypt(dtCustomer.Rows[0]["CustId"].ToString());
                    Session["CustomerCode"] = DataSecurityManager.Decrypt(dtCustomer.Rows[0]["CustId"].ToString());

                    Session.Add("CustomerMailId", DataSecurityManager.Decrypt(dtCustomer.Rows[0]["Email"].ToString()));
                    Session.Add("CustPassword", DataSecurityManager.Decrypt(dtCustomer.Rows[0]["Password"].ToString()));
                    DataTable dtrpax = SessionServices.RetrieveSession<DataTable>("Bookingdt");

                    string BookRef = DataSecurityManager.Decrypt(dtCustomer.Rows[0]["FirstName"].ToString()) + DataSecurityManager.Decrypt(dtCustomer.Rows[0]["LastName"].ToString()) + "X" + Convert.ToDouble(dtrpax.Compute("SUM(Pax)", string.Empty)).ToString() + "-" + "Self";
                    ViewState["BookRef"] = BookRef;
                    lbPaymentMethod.Text = "N/A";

                    lblUsername.Text = "Hello " + Session["CustName"].ToString();
                    //lnkCustomerRegis.Visible = false;
                    navlogin.Visible = false;
                    LinkButton1.Visible = true;
                    pnlFullDetails.Visible = true;
                    panelwithoutCreditAgent.Visible = true;
                    pnlBookButton.Visible = true;
                    pnllogin.Visible = false;
                    custRegis.Visible = false;
                    //dvpayment.Visible = true;
                    dvBilling.Visible = true;
                    divspcl.Visible = true;
                    tblBref.Visible = false;
                    dvreg.Visible = false;
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
    public void checkfield()
    {
        if (txtFirstName.Text == "" || txtLastName.Text == "" || txtMailAddress.Text == "" || txtTelephone.Text == "" || txtAddress1.Text == "" || txtCity.Text == "" || txtState.Text == "" || txtPostcode.Text == "" || txtpassword.Text == "" || ddlCountry.SelectedIndex == 0)
        {
            Session["Phonecheck"] = 1;
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Showstatus", "javascript:alert('Plaese enter valid data')", true);
            return;
        }
    }
    public void checkfield1()
    {
        if (txtFirstname1.Text == "" || txtLastanme1.Text == "" || txtEmailid1.Text == "" || txtMobilephone1.Text == "" || txtAddress11.Text == "" || txtCity1.Text == "" || txtState1.Text == "" || txtPostCode1.Text == "" || ddlCountry1.SelectedIndex == 0)
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
            Session["Phonecheck"] = 1;
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Showstatus", "javascript:alert('Please enter valid email')", true);
            return;
        }
    }
    private void ValidateEmail1(string mail)
    {
        string email = mail;
        Regex regex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
        Match match = regex.Match(email);
        if (match.Success)
        {
            //blcus.Email = mail;
            //blcus.action = "chkDuplicate";
            //DataTable dt = dlcus.checkDuplicateemail(blcus);
            //if (dt != null && dt.Rows.Count > 0)
            //{
            //    Session["Phonecheck"] = 1;
            //    ScriptManager.RegisterStartupScript(this, this.GetType(), "Showstatus", "javascript:alert('This email is already register')", true);
            //    return;
            //}
        }

        else
        {
            Session["Phonecheck"] = 1;
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
        checkfield();
        ValidateEmail(txtMailAddress.Text);
        checkphone(txtTelephone.Text);
        checkpostpostcode(txtPostcode.Text);

        ViewState["CustPass"] = txtpassword.Text.Trim();
        sendMail();
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        checkfield1();
        ValidateEmail1(txtEmailid1.Text);
        checkphone(txtMobilephone1.Text);
        checkpostpostcode(txtPostCode1.Text);

        Session["Refrence"] = "1";
        ViewState["CustPass"] = txtPassword1.Text.Trim();
        customerLogin.Visible = false;
        clientRefrence();
        //showfulldetailsforguest(txtEmailid1.Text);
        //sendMail1();
    }
    public void sendMail()
    {
        if (Session["Phonecheck"] == null)
        {
            try
            {


                MailMessage mail = new MailMessage();
                SmtpClient SmtpServer = new SmtpClient("adventureresortscruises.in");

                mail.From = new MailAddress("reservations@adventureresortscruises.in");

                mail.To.Add(txtMailAddress.Text.Trim());


                mail.Subject = "Mail Verification";

                Random rnd = new Random();
                string Code = rnd.Next(10000, 99999).ToString();
                hfVCode.Value = Code;
                mail.Body = "Your code is " + Code;





                SmtpServer.Port = 587;
                SmtpServer.Credentials = new System.Net.NetworkCredential("reservations@adventureresortscruises.in", "Augurs@123");
                SmtpServer.EnableSsl = false;

                SmtpServer.Send(mail);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Showstatus", "javascript:alert('Please check your Mail for the Verification Code')", true);
                custRegis.Visible = false;
                customerLogin.Visible = true;
                pnllogin.Visible = false;
                dvRefrence.Visible = false;

                //TableCust.Visible = false;
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
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Showstatus", "javascript:alert('Please enter valid data')", true);
            return;
        }
    }
    public void sendMail1()
    {
        if (Session["Phonecheck"] == null)
        {
            try
            {


                MailMessage mail = new MailMessage();
                SmtpClient SmtpServer = new SmtpClient("adventureresortscruises.in");

                mail.From = new MailAddress("reservations@adventureresortscruises.in");

                mail.To.Add(txtEmailid1.Text.Trim());


                mail.Subject = "Mail Verification";

                Random rnd = new Random();
                string Code = rnd.Next(10000, 99999).ToString();
                hfVCode.Value = Code;
                mail.Body = "Your code is " + Code;





                SmtpServer.Port = 587;
                SmtpServer.Credentials = new System.Net.NetworkCredential("reservations@adventureresortscruises.in", "Augurs@123");
                SmtpServer.EnableSsl = false;

                SmtpServer.Send(mail);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Showstatus", "javascript:alert('Please check your Mail for the Verification Code')", true);
                custRegis.Visible = false;
                customerLogin.Visible = true;
                dvRefrence.Visible = false;
                //TableCust.Visible = false;
                tableVerify.Visible = true;
                pnllogin.Visible = false;
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Showstatus", "javascript:alert('" + ex.Message.ToString() + "')", true);
            }
        }
        else
        {
            Session["Phonecheck"] = null;
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Showstatus", "javascript:alert('Please check your Mail for the Verification Code')", true);
            return;
        }
    }
    public void checkpostpostcode(string postcode)
    {
        if (postcode != null && postcode != "")
        {
            if (postcode.Length < 6)
            {
                Session["Phonecheck"] = 1;
            }
            else
            {
                try
                {
                    long post = Convert.ToInt64(postcode);
                }
                catch
                {
                    Session["Phonecheck"] = 1;
                }
            }
        }
    }
    public void clientRefrence()
    {
        if (Session["Phonecheck"] == null)
        {
            try
            {
                blcus.action = "Saveforguest";
                blcus.Address1 = txtAddress11.Text;
                blcus.Address2 = txtadress22.Text;
                blcus.City = txtCity1.Text;
                Int32.TryParse(ddlCountry1.SelectedValue, out CountryId);
                blcus.CountryId = CountryId;
                blcus.Email = txtEmailid1.Text.Trim();
                Session["CustMailId"] = txtEmailid1.Text.Trim();
                Session["CustName"] = txtFirstname1.Text;
                blcus.FirstName = txtFirstname1.Text;
                blcus.LastName = txtLastanme1.Text;
                blcus.PostalCode = txtPostCode1.Text;
                blcus.State = txtState1.Text;
                blcus.Telephone = txtMobilephone1.Text.Trim();
                //blcus.Password = ViewState["CustPass"].ToString();
                //Session["userpass"] = ViewState["CustPass"].ToString();
                blcus.Title = ddlList1.SelectedItem.Text;
                blcus.nameoncard = txtNamenCard.Text;
                blcus.caardnumber = txtcardnumber.Text;
                blcus.expirydate = datepicker2.Text;
                blcus.specialqutos = specialrequest.Value;
                blcus.bilingaddress = txtBilingAddress.Text;
                //blcus.PaymentMethod = txtPaymentMethod.Text.Trim();


                getQueryResponse = dlcus.AddGuset(blcus);


                if (getQueryResponse > 0)
                {
                    lblUsername.Text = "Hello " + Session["CustName"].ToString();
                    //lnkCustomerRegis.Visible = false;
                    navlogin.Visible = false;
                    LinkButton1.Visible = true;
                    //dvpayment.Visible = true;
                    dvBilling.Visible = true;
                    divspcl.Visible = true;
                    Session["Refrence"] = null;
                    pnllogin.Visible = false;
                    dvBilling.Visible = true;
                    //dvpayment.Visible = true;
                    divspcl.Visible = true;
                    pnlFullDetails.Visible = true;
                    buttonUpdate.Visible = true;
                    dvreg.Visible = false;
                    showfulldetailsforguest(txtEmailid1.Text);
                    //ScriptManager.RegisterStartupScript(this, this.GetType(), "Showstatus", "javascript:alert('Verification Done')", true);


                }
                else
                {

                }


            }

            catch
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Showstatus", "javascript:alert('Please check entries')", true);
            }
        }
        else
        {
            Session["Phonecheck"] = null;
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Showstatus", "javascript:alert('Please enter valid data')", true);
            return;
        }
    }
    public void ClientRegister()
    {
        try
        {
            blcus.action = "Save";
            blcus.Address1 = txtAddress1.Text;
            blcus.Address2 = txtaddress2.Text;
            blcus.City = txtCity.Text;
            Int32.TryParse(ddlCountry.SelectedValue, out CountryId);
            blcus.CountryId = CountryId;
            blcus.Email = txtMailAddress.Text.Trim();
            Session["CustMailId"] = txtMailAddress.Text.Trim();
            Session["CustName"] = txtFirstName.Text;
            blcus.FirstName = txtFirstName.Text;
            blcus.LastName = txtLastName.Text;
            blcus.PostalCode = txtPostcode.Text;
            blcus.State = txtState.Text;
            blcus.Telephone = txtTelephone.Text.Trim();
            blcus.Password = ViewState["CustPass"].ToString();
            Session["userpass"] = ViewState["CustPass"].ToString();
            blcus.Title = ddltitle.SelectedItem.Text;
            blcus.nameoncard = txtNamenCard.Text;
            blcus.caardnumber = txtcardnumber.Text;
            blcus.expirydate = datepicker2.Text;
            blcus.specialqutos = specialrequest.Value;
            blcus.bilingaddress = txtBilingAddress.Text;
            //blcus.Password = txtpassword.Text;
            //blcus.PaymentMethod = txtPaymentMethod.Text.Trim();


            getQueryResponse = dlcus.AddCustomers(blcus);


            if (getQueryResponse > 0)
            {
                lblUsername.Text = "Hello " + Session["CustName"].ToString();
                //lnkCustomerRegis.Visible = false;
                navlogin.Visible = false;
                LinkButton1.Visible = true;
                //dvpayment.Visible = true;
                dvBilling.Visible = true;
                divspcl.Visible = true;
                pnlFullDetails.Visible = true;
                pnllogin.Visible = false;
                buttonUpdate.Visible = true;
                dvreg.Visible = false;
                showfulldetails(txtMailAddress.Text.Trim(), txtpassword.Text);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Showstatus", "javascript:alert('Verification Done')", true);


            }
            else
            {

            }


        }

        catch
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Showstatus", "javascript:alert('Please check entries')", true);
        }
    }
    protected void btnVerify_Click(object sender, EventArgs e)
    {
        try
        {
            if (txtCode.Text == hfVCode.Value)
            {
                if (Session["Refrence"] != null)
                {
                    clientRefrence();
                    txtCode.Text = "";
                    tableVerify.Visible = false;


                }
                else
                {
                    ClientRegister();
                    txtCode.Text = "";
                    tableVerify.Visible = false;

                }

                //TableCust.Visible = true;

            }

            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Showstatus", "javascript:alert('Wrong Code Entered!')", true);
                //TableCust.Visible = false;
                tableVerify.Visible = true;
                pnllogin.Visible = false;
            }
        }
        catch
        {
        }
    }
    public bool IsValid(string emailaddress)
    {
        try
        {
            MailAddress m = new MailAddress(emailaddress);

            return true;
        }
        catch (FormatException)
        {
            return false;
        }
    }
    protected void txtMailAddress_TextChanged(object sender, EventArgs e)
    {

    }
    protected void txtEmailid1_TextChanged(object sender, EventArgs e)
    {

    }
    private int InsertBookingTableData(int acmid, int acmtpid, int agid, string bkref, DateTime cin, DateTime cout)
    {
        try
        {
            BALBooking blsr = new BALBooking();
            DALBooking dlsr = new DALBooking();

            DataTable dtbkdetails = SessionServices.RetrieveSession<DataTable>("Bookingdt");
            blsr._sBookingRef = bkref;
            blsr._dtStartDate = cin;
            blsr._dtEndDate = cout;

            if (Session["CustId"] != null && Session["UserCode"] == null)
            {
                blsr.CustomerId = Session["CustId"].ToString();
            }
            else
            {
                blsr.CustomerId = "0";
            }

            blsr._iAccomTypeId = acmtpid;
            blsr._iAccomId = acmid;
            blsr._iAgentId = agid;

            blsr._iNights = Convert.ToInt32((cout - cin).TotalDays);
            blsr._iPersons = Convert.ToInt32(dtbkdetails.Compute("SUM(Pax)", string.Empty));
            blsr._BookingStatusId = 1;
            blsr._SeriesId = 0;
            blsr._proposedBooking = false;
            blsr._chartered = false;
            blsr._BookingStatusId = (int)BookingStatusTypes.BOOKED;
            blsr._proposedBooking = true;

            int bookingId = dlsr.AddParentBookingDetail(blsr);

            var bookingDetails = dlsr.GetBookingDetails(bookingId);
            if (bookingDetails != null)
            {
                blsr.BookingCode = bookingDetails.BookingCode;
            }
            blsr._iBookingId = bookingId;

            //Session["tblBookingBAL"] = blsr;
            SessionServices.SaveSession<BALBooking>("tblBookingBAL", blsr);
            return bookingId;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    public void BookTheHotel()
    {
        int iAccomId = 0;
        int iaccomtypeid = 0;
        int iagentid = 0;
        DateTime chkin, chkout;

        if (Session["Usercode"] != null)
        {
            Int32.TryParse(Session["AId"].ToString(), out iagentid);
        }
        Int32.TryParse(Session["AccomId"].ToString(), out iAccomId);
        Int32.TryParse(Session["iAccomtypeId"].ToString(), out iaccomtypeid);

        DateTime.TryParse(Session["Chkin"].ToString(), out chkin);
        DateTime.TryParse(Session["chkout"].ToString(), out chkout);

        string bookref = Session["BookingRef"].ToString();
        string bookingPaymentId = Session["BookingPayId"].ToString();

        int bookingId = InsertBookingTableData(iAccomId, iaccomtypeid, iagentid, bookref, chkin, chkout);
        InsertRoomBookingTableData(bookingId, bookingPaymentId);
    }
    private void RedirectToPaymentGatewayResponse()
    {
        string ts = "TRANSACTIONSTATUS=200";
        string apt = "APTRANSACTIONID=1234";
        string msg = "MESSAGE=success";
        string tid = "TRANSACTIONID=100";
        string amt = "AMOUNT=50";
        string ash = "ap_SecureHash=abc";

        string qs = string.Format("{0}&{1}&{2}&{3}&{4}&{5}", ts, apt, msg, tid, amt, ash);
        Response.Redirect("~/Cruise/Booking/PaymentGatewayResponse.aspx?" + qs);
    }
    private int InsertRoomBookingTableData(int bookingId, string bookingPaymentId)
    {
        try
        {
            BALBooking blsr = new BALBooking();
            DALBooking dlsr = new DALBooking();

            DataTable dtbkdetails = SessionServices.RetrieveSession<DataTable>("Bookingdt");
            BALBooking booking = dlsr.GetBookingDetails(bookingId);

            Session["maxBookId"] = bookingId;

            blsr._iBookingId = bookingId;
            blsr._iAccomId = booking._iAccomId;
            blsr.PaymentId = bookingPaymentId;

            //int LoopInsertStatus = 0;
            for (int LoopCounter = 0; LoopCounter < dtbkdetails.Rows.Count; LoopCounter++)
            {
                blsr._dtStartDate = booking._dtStartDate;
                blsr._dtEndDate = booking._dtEndDate;
                blsr._iPaxStaying = booking._iPaxStaying;
                blsr._bConvertTo_Double_Twin = Convert.ToBoolean(dtbkdetails.Rows[LoopCounter]["ConvDouble"].ToString());
                blsr._cRoomStatus = "B";

                blsr._sRoomNo = dtbkdetails.Rows[LoopCounter][7].ToString();
                blsr._PaidAmount = Convert.ToDouble(Session["Paid"]);

                blsr.action = "AddPriceDetailsToo";
                string[] arr = dtbkdetails.Rows[LoopCounter]["Total"].ToString().Split(' ');
                blsr._Amt = Convert.ToDecimal(arr[1]);

                int GetQueryResponse = dlsr.AddRoomBookingDetails(blsr);
            }
            return 1;
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Showstatus", "javascript:alert('" + ex.Message.ToString() + "')", true);
            throw ex;
        }
    }
    protected void btnPayProceed_Click(object sender, EventArgs e)
    {
        try
        {
            Session["Hotel"] = 1;
            if (btnPayProceed.Text == "Proceed For Payment")
            {
                if (Session["UserCode"] != null)
                {
                    //aev@farhorizonindia.com [1:48:55 PM] Augurs  Technologies Pvt. Ltd.: 12345

                    DataTable dtrpax = SessionServices.RetrieveSession<DataTable>("Bookingdt");

                    string BookRef = txtBookRef.Text + "X" + Convert.ToDouble(dtrpax.Compute("SUM(Pax)", string.Empty)).ToString();

                    Session.Add("BookingRef", BookRef);
                    Session["Paid"] = Convert.ToDouble(txtPaidAmt.Text.Trim() == "" ? "0" : txtPaidAmt.Text.Trim());
                    blagentpayment._Action = "MailValidate";
                    blagentpayment._EmailId = Session["AgentMailId"].ToString();
                    blagentpayment._Password = Session["Password"].ToString();
                    DataTable dtAgent = dlagentpayment.BindControls(blagentpayment);
                    if (dtAgent.Rows.Count > 0)
                    {
                        string BookingPayId = lbPaymentMethod.Text.Trim().Substring(0, 2) + DateTime.Now.ToString("MMddhhmmssfff");
                        string Email = Session["AgentMailId"].ToString();
                        string PhoneNumber = hdnfPhoneNumber.Value.Trim().ToString();
                        string FirstName = DataSecurityManager.Decrypt(dtAgent.Rows[0]["FirstName"].ToString());
                        string LastName = DataSecurityManager.Decrypt(dtAgent.Rows[0]["LastName"].ToString());
                        string PaidAmt = hftxtpaidamt.Value.Trim().ToString();
                        string PaymentId = BookingPayId.ToString();
                        string BillingAddress = lblBillingAddress.Text.Trim().ToString();
                        Session["BookingPayId"] = txtBookRef.Text.Trim();// BookingPayId;

                        Session["Address"] = BillingAddress;
                        Session["InvName"] = FirstName;
                        BookTheHotel();
                        Session["SubInvName"] = FirstName;
                        if (Debugger.IsAttached)
                        {
                            RedirectToPaymentGatewayResponse();
                        }
                        else
                        {
                            //Response.Redirect("PaymentGatewayResponse.aspx?BookingPayId=" + PaymentId + "&EmailId=" + Email.ToString() + "&PhoneNumber=" + PhoneNumber.ToString() + "&FirstName=" + FirstName.ToString() + "&LastName=" + LastName.ToString() + "&PaidAmt=" + PaidAmt.ToString() + "&BillingAddress=" + BillingAddress.ToString());
                            //http://adventureresortscruises.in/Cruise/booking/sendtoairpay.aspx?BookedId=0&PackName=7N8D+Downstream+Cruise&NoOfNights=7&CheckinDate=12%2f4%2f2016&PackId=Pack4
                            Response.Redirect("~/Cruise/booking/sendtoairpay.aspx?BookingPayId=" + PaymentId + "&EmailId=" + Email.ToString() + "&PhoneNumber=" + PhoneNumber.ToString() + "&FirstName=" + FirstName.ToString() + "&LastName=" + LastName.ToString() + "&PaidAmt=" + PaidAmt.ToString() + "&BillingAddress=" + BillingAddress.ToString());
                            //ScriptManager.RegisterStartupScript(this, this.GetType(), "Showstatus", "javascript:alert('not registered!!!')", true);
                        }
                    }
                }
                else
                {
                    Session.Add("BookingRef", ViewState["BookRef"].ToString());

                    blcus.Email = Session["CustomerMailId"].ToString();
                    blcus.Password = Session["CustPassword"].ToString();

                    blcus.action = "LoginCust";
                    DataTable dtCustomer = dlcus.checkDuplicateemail(blcus);
                    Session["Paid"] = Convert.ToDouble(txtPaidAmt.Text.Trim() == "" ? "0" : txtPaidAmt.Text.Trim());
                    if (dtCustomer.Rows.Count > 0)
                    {
                        Random rnd = new Random();
                        string BookingPayId = rnd.Next(10000, 20000).ToString() + DateTime.Now.ToString("MMddhhmmssfff");
                        Session["BookingPayId"] = BookingPayId;
                        string Email = Session["CustomerMailId"].ToString();
                        BookTheHotel();
                        string PhoneNumber = hdnfPhoneNumber.Value.Trim().ToString();
                        string FirstName = DataSecurityManager.Decrypt(dtCustomer.Rows[0]["FirstName"].ToString());
                        string LastName = DataSecurityManager.Decrypt(dtCustomer.Rows[0]["LastName"].ToString());
                        string PaidAmt = hftxtpaidamt.Value.Trim().ToString();
                        string PaymentId = BookingPayId.ToString();
                        string BillingAddress = lblBillingAddress.Text.Trim().ToString();
                        Session["Address"] = BillingAddress;
                        Session["InvName"] = DataSecurityManager.Decrypt(dtCustomer.Rows[0]["Title"].ToString()) + " " + FirstName + " " + LastName;
                        Session["SubInvName"] = LastName + ", " + DataSecurityManager.Decrypt(dtCustomer.Rows[0]["Title"].ToString()) + " " + FirstName;
                        if (Debugger.IsAttached)
                        {
                            RedirectToPaymentGatewayResponse();
                        }
                        else
                        {
                            Response.Redirect("~/Cruise/booking/sendtoairpay.aspx?BookingPayId=" + PaymentId + "&EmailId=" + Email.ToString() + "&PhoneNumber=" + PhoneNumber.ToString() + "&FirstName=" + FirstName.ToString() + "&LastName=" + LastName.ToString() + "&PaidAmt=" + PaidAmt.ToString() + "&BillingAddress=" + BillingAddress.ToString());

                            //http://localhost:1897/ResortManager/Cruise/booking/SummerisedDetails.aspx?BookedId=0&PackName=Ganges+Exclusive&NoOfNights=5&CheckinDate=5%2f1%2f2016

                            //ScriptManager.RegisterStartupScript(this, this.GetType(), "Showstatus", "javascript:alert('not registered!!!')", true);
                        }
                    }
                }
            }
            else
            {

                if (Convert.ToDecimal(txtPaidAmt.Text) <= Convert.ToDecimal(hdnfCreditLimit.Value))
                {
                    string BookingPayId = lbPaymentMethod.Text.Trim().Substring(0, 2) + DateTime.Now.ToString("MMddhhmmssfff");
                    Session["BookingPayId"] = BookingPayId;
                    Session.Add("BookingRef", txtBookRef.Text.Trim().ToString());
                    Session["Paid"] = Convert.ToDouble(hftxtpaidamt.Value.Trim() == "" ? "0" : hftxtpaidamt.Value.Trim().ToString());

                    Session["InvName"] = Session["UserName"].ToString();
                    Session["Address"] = null;
                    Response.Redirect("PaymentGatewayResponse.aspx");
                }
                else
                {
                    lblPaymentErr.Text = "Payment Amount Exceeding Credit Limit";
                }
            }
        }
        catch
        {

        }
    }
    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        Session.Clear();
        Session.Abandon();
        Session.RemoveAll();
        System.Web.Security.FormsAuthentication.SignOut();
        LinkButton1.Visible = false;
        Response.Redirect("SearchProperty1.aspx");
    }
    public void calcamt(DataTable dts)
    {
        try
        {
            for (int j = 0; j < dts.Rows.Count; j++)
            {
                string[] arr = dts.Rows[j]["Total"].ToString().Split(' ');

                TotalPaybleAmt = TotalPaybleAmt + Convert.ToInt32(arr[1]); ;
            }
            hdnfTotalPaybleAmt.Value = TotalPaybleAmt.ToString();

            txtPaidAmt.Text = Math.Round(((25 * TotalPaybleAmt) / 100)).ToString("N2");
            Bookingdt = SessionServices.RetrieveSession<DataTable>("Bookingdt");
            if (Session["Taxpax"] != null)
            {
                lblTax.Text = Session["Taxpax"].ToString() + "%";
            }
            lbltotAmt.Text = Bookingdt.Rows[0]["Currency"].ToString() + " " + TotalPaybleAmt.ToString();
            lblCurrency.Text = Bookingdt.Rows[0]["Currency"].ToString().ToString() + " ";
            txtPaidAmt.Text = Math.Round(((25 * TotalPaybleAmt) / 100)).ToString("#.##");
            hftxtpaidamt.Value = Convert.ToDouble(txtPaidAmt.Text).ToString("N2").Replace(",", "");

            lblBalanceAmt.Text = Bookingdt.Rows[0]["Currency"].ToString().ToString() + " " + Math.Round((TotalPaybleAmt - Convert.ToDouble(txtPaidAmt.Text))).ToString();

            //   lbltotAmt.Text = dtGetBookedRooms.Rows[0]["Currency"].ToString() + " " + TotalPaybleAmt.ToString();

        }

        catch
        {
        }
    }
    protected void Button2_Click(object sender, EventArgs e)
    {

        dvRefrence.Visible = false;
        blcus.action = "update";
        blcus.Email = Session["CustMailId"].ToString();
        blcus.nameoncard = txtNamenCard.Text;
        blcus.caardnumber = txtcardnumber.Text;
        blcus.expirydate = datepicker2.Text;
        blcus.bilingaddress = txtBilingAddress.Text;
        blcus.Refrenceid = txtBookRef.Text;
        blcus.specialqutos = specialrequest.Value;
        if (Session["CustId"] != null)
        {
            blcus.CustId = Convert.ToInt32(Session["CustId"].ToString());
        }
        int n = dlcus.Update(blcus);
        if (n == 1)
        {
            buttonUpdate.Visible = true;
            try
            {
                if (Session["userpass"] != null)
                {
                    blcus.Password = Session["userpass"].ToString();
                }


                blcus.action = "LoginCust";

                DataTable dtCustomer = dlcus.checkDuplicateemail(blcus);

                if (dtCustomer != null)
                {
                    if (dtCustomer.Rows.Count > 0)
                    {


                        Bookingdt = new DataTable();
                        Bookingdt = SessionServices.RetrieveSession<DataTable>("Bookingdt");
                        calcamt(Bookingdt);
                        ViewState["Pass"] = txtCustPass.Text.Trim();

                        if (txtBilingAddress.Text != null && txtBilingAddress.Text != "")
                        {
                            lblBillingAddress.Text = txtBilingAddress.Text;
                        }
                        else
                        {
                            lblBillingAddress.Text = dlcus.GetBillingAddress(dtCustomer.Rows[0]);
                        }
                        if (specialrequest.InnerText != null && specialrequest.InnerText != "")
                        {
                            lblSpecialRequest.Text = specialrequest.InnerText;
                        }
                        else
                        {
                            lblSpecialRequest.Text = "N/A";
                        }


                        lblAgentName.Text = DataSecurityManager.Decrypt(dtCustomer.Rows[0]["FirstName"].ToString()) + " " + DataSecurityManager.Decrypt(dtCustomer.Rows[0]["LastName"].ToString());
                        lbPaymentMethod.Text = DataSecurityManager.Decrypt(dtCustomer.Rows[0]["PaymentMethod"].ToString());
                        hdnfPhoneNumber.Value = DataSecurityManager.Decrypt(dtCustomer.Rows[0]["Telephone"].ToString());

                        Session["CustId"] = dtCustomer.Rows[0]["CustId"].ToString();
                        Session["CustomerCode"] = dtCustomer.Rows[0]["CustId"].ToString();

                        Session.Add("CustomerMailId", DataSecurityManager.Decrypt(dtCustomer.Rows[0]["Email"].ToString()));
                        Session.Add("CustPassword", DataSecurityManager.Decrypt(dtCustomer.Rows[0]["Password"].ToString()));
                        DataTable dtrpax = SessionServices.RetrieveSession<DataTable>("Bookingdt");

                        string BookRef = DataSecurityManager.Decrypt(dtCustomer.Rows[0]["FirstName"].ToString()) + DataSecurityManager.Decrypt(dtCustomer.Rows[0]["LastName"].ToString()) + "X" + Convert.ToDouble(dtrpax.Compute("SUM(Pax)", string.Empty)).ToString() + "-" + "Self";
                        ViewState["BookRef"] = BookRef;
                        lbPaymentMethod.Text = "N/A";


                        pnlFullDetails.Visible = true;
                        panelwithoutCreditAgent.Visible = true;
                        pnlBookButton.Visible = true;
                        pnllogin.Visible = false;
                        custRegis.Visible = false;

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


    protected void btnCustLogin_Click1(object sender, EventArgs e)
    {
        Response.Redirect("../Booking/ForgotPassword.aspx");
    }

    protected void btnaddroom_Click(object sender, EventArgs e)
    {
        if (Session["HNoofrooms"] != null)
        {
            Session["HNoofrooms"] = Convert.ToDecimal(Session["HNoofrooms"]) + 1;
        }
        else
        {
            Session["HNoofrooms"] = 2;
        }
        Response.Redirect("available.aspx");
    }

    protected void gdvHotelRoomRates_RowDataBound(object sender, GridViewRowEventArgs e)
    {

    }
}