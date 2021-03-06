﻿using FarHorizon.DataSecurity;
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
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
<<<<<<< HEAD
using System.Text;
using System.Net;
using Newtonsoft.Json.Linq;

public partial class Cruise_Booking_AfterBookingDetails3 : System.Web.UI.Page
{

    string CompanyName = ConfigurationManager.AppSettings["cName"];
    string CompanyEmail = ConfigurationManager.AppSettings["cEmail"];
    string CompanyAddress = ConfigurationManager.AppSettings["cAddress"];
    string CompanyPhoneNo = ConfigurationManager.AppSettings["cPhoneNo"];
    string CompanyMobile = ConfigurationManager.AppSettings["cMobile"];
    string CompanyLogo = ConfigurationManager.AppSettings["cLogo"];
    //string ccEmail = ConfigurationManager.AppSettings["ccEmail"];
    string SmtpUserId = ConfigurationManager.AppSettings["SMTPUserId"];
    string SmtpPassword = ConfigurationManager.AppSettings["SMTPPwd"];
    string SmtpHost = ConfigurationManager.AppSettings["SMTPServer"];
=======
public partial class Cruise_Booking_AfterBookingDetails3 : System.Web.UI.Page
{
>>>>>>> 06df147e7f6e76b3ddcb27473f8305164d96b955
    DataTable Bookingdt;
    DataTable bookingmealdt;
    DataTable dtroominfo;

    public DataTable dtGetBookedRooms;
    public int LoopCounter = 0;
    public string imagepath;
    public string type;
    BALAgentPayment blagentpayment = new BALAgentPayment();
    DALAgentPayment dlagentpayment = new DALAgentPayment();

    DALOpenDates dlOpenDates = new DALOpenDates();
    BALOpenDates blOpenDates = new BALOpenDates();
    DALBooking dlbook = new DALBooking();
    BALCustomers blcus = new BALCustomers();
    DALCustomers dlcus = new DALCustomers();
    //DataTable dtGetReturnedData;
    int getQueryResponse = 0;
    string email = "";
    string password = "";
    int CountryId = 0;
    double total1 = 0;
    SqlConnection con;
    string ccEmail = ConfigurationManager.AppSettings["ccEmail"];
    protected void Page_Load(object sender, EventArgs e)
    {
        con = new SqlConnection(GetConnectionString());
        string url = HttpContext.Current.Request.Url.AbsoluteUri;
        Session["getavailable"] = url;
        btnPayProceed.Visible = true;
        if (Session["AccomId"] != "" && Session["AccomId"] != null)
        {
            PopulateModule(Convert.ToInt32(Session["AccomId"]));
            string AccomPolicyUrl = ViewState["ModuleDt"].ToString();
            linkTandCSign.Attributes["href"] = AccomPolicyUrl;
            linkTandCReg.Attributes["href"] = AccomPolicyUrl;
            linkTandCGuest.Attributes["href"] = AccomPolicyUrl;
        }
        if (chkterms.Checked)
        {
            btnPayProceed.Visible = true;
        }
        else
        {
            // btnPayProceed.Visible = false;
        }
        if (Session["check"] == null)
        {
            Session["check"] = 1;
            Session["foraddroom"] = null;
            Session["Bookingdt"] = null;
            Response.Redirect("searchproperty1.aspx");
        }
        if (Session["CustName"] != null)
        {
<<<<<<< HEAD
            
=======
>>>>>>> 06df147e7f6e76b3ddcb27473f8305164d96b955

            lblUsername.Text = "Hello " + Session["CustName"].ToString();
            dvpaneldefault.Visible = false;
            //lnkCustomerRegis.Visible = false;
            navlogin.Visible = false;
            LinkButton1.Visible = true;
            //dvpayment.Visible = true;
            dvreg.Visible = false;
            //dvBilling.Visible = true;
            //divspcl.Visible = true;
            BookRef.Visible = false;
            pnllogin.Visible = false;
            custRegis.Visible = false;
            dvRefrence.Visible = false;
            pnllogin.Visible = false;
            custRegis.Visible = false;
            dvpaneldefault.Visible = false;
            if (Session["guest"] == null)
            {
                if (Session["userpass"] != null)
                {
                    password = Session["userpass"].ToString();
                }
                if (Session["CustMailId"] != null)
                {
                    email = Session["CustMailId"].ToString();
                    showfulldetails(email, password);
                }
                if (Session["UserName"] != null)
                {
                    loadall();
                }
            }
            //if (Session["HCheckin"] != null)
            //{
            //    lblBalancedate.Text = Convert.ToDateTime(Session["HCheckin"].ToString()).AddDays(-90).ToString("dddd, MMMM d, yyyy");
            //}
        }
        else if (Session["UserName"] != null)

        {
            dvreg.Visible = false;
            pnllogin.Visible = false;
            custRegis.Visible = false;
            dvRefrence.Visible = false;
            pnlFullDetails.Visible = true;
            buttonUpdate.Visible = true;
            //  dvpnlDefault.Visible = false;
            lblUsername.Text = "Hello " + Session["UserName"].ToString();
            //if (Session["Getcheckindate"] != null)
            //{
            //    lblBalancedate.Text = "(75% of total) to be paid prior to " + Convert.ToDateTime(Session["Getcheckindate"].ToString()).AddDays(-90).ToString("dddd, MMMM d, yyyy");
            //}
            lnkCustomerRegis.Visible = false;
            navlogin.Visible = false;
            LinkButton1.Visible = true;
            //dvBilling.Visible = true;
            //dvpayment.Visible = true;
            //divspcl.Visible = true;
            //this.pnlFullDetails.Visible = false;
            pnlBookButton.Visible = true;
            panelwithoutCreditAgent.Visible = true;
            dvreg.Visible = false;
            //getdetail();
            loadall();
        }
        //else if (Session["UserName"] != null)

        //{
        //    lblUsername.Text = "Hello " + Session["UserName"].ToString();
        //    dvpaneldefault.Visible = false;
        //    //lnkCustomerRegis.Visible = false;
        //    navlogin.Visible = false;
        //    LinkButton1.Visible = true;
        //    //dvpayment.Visible = true;
        //    //dvBilling.Visible = true;
        //    dvreg.Visible = false;

        //    //divspcl.Visible = true;
        //    //if (Session["HCheckin"] != null)
        //    //{
        //    //    lblBalancedate.Text = Convert.ToDateTime(Session["HCheckin"].ToString()).AddDays(-90).ToString("dddd, MMMM d, yyyy");
        //    //}
        //}
        else
        {
            //lnkCustomerRegis.Visible = false;
            navlogin.Visible = true;
            LinkButton1.Visible = false;
            //dvpayment.Visible = false;
            dvBilling.Visible = false;
            dvreg.Visible = true;
            //divspcl.Visible = false;
        }

        if (!IsPostBack)
        {
            if (Session["AccomName"] != null)
            {
                if (Session["AccomName"].ToString() == "Dera Dune Retreat, Jamba")
                {
                    //dvJamba.Visible = true;
                    //dvJamba3.Visible = true;
                    //dvvikundam.Visible = false;
                    //dvkalakho.Visible = false;
                    //dvvikundam2.Visible = false;
                    //dvkalakho1.Visible = false;
                }
                if (Session["AccomName"].ToString() == "Vaikundam, Backwaters" || Session["AccomName"].ToString() == "Sauvernigam, Backwaters")
                {
                    //dvJamba.Visible = false;
                    //dvvikundam.Visible = true;
                    //dvvikundam2.Visible = true;
                    //dvkalakho.Visible = false;
                    //dvkalakho1.Visible = false;
                    //dvJamba3.Visible = false;
                }
                if (Session["AccomName"].ToString() == "Dera Village Retreat, Kalakho")
                {
                    //dvJamba.Visible = false;
                    //dvvikundam.Visible = false;
                    //dvkalakho.Visible = true;
                    //dvkalakho1.Visible = true;
                    //dvJamba3.Visible = false;
                    //dvvikundam2.Visible = false;
                }
            }
            if (Session["guest"] != null)
            {
                dvpaneldefault.Visible = false;
                lblUsername.Text = "Hello " + Session["CustName"].ToString();
                showfulldetailsforguest(Session["guest"].ToString());
                Bookingdt = new DataTable();
                Bookingdt = SessionServices.RetrieveSession<DataTable>("Bookingdt");
                //Bookingdt = Session["Bookingdt"] as DataTable;
                calcamt(Bookingdt);
                //Session["Refrence"] = "1";
                //ViewState["CustPass"] = txtPassword1.Text.Trim();
                customerLogin.Visible = false;
                custRegis.Visible = false;
                dvRefrence.Visible = false;
                panelwithoutCreditAgent.Visible = true;
                //lnkCustomerRegis.Visible = false;
                pnllogin.Visible = false;
                navlogin.Visible = false;
                LinkButton1.Visible = true;
                //dvpayment.Visible = true;
                dvreg.Visible = false;
                //dvBilling.Visible = true;
                //divspcl.Visible = true;
                BookRef.Visible = false;
                pnlFullDetails.Visible = true;

                //if (Session["HCheckin"] != null)
                //{
                //    lblBalancedate.Text = Convert.ToDateTime(Session["HCheckin"].ToString()).AddDays(-90).ToString("dddd, MMMM d, yyyy");
                //}
            }
            if (Session["guest"] == null)
            {
                //if (Session["UserCode"] != null || Session["CustomerCode"] != null)
                //{
                //    pnllogin.Visible = false;
                //    custRegis.Visible = false;
                //    if (Session["userpass"] != null)
                //    {
                //        password = Session["userpass"].ToString();
                //    }
                //    if (Session["CustMailId"] != null)
                //    {
                //        email = Session["CustMailId"].ToString();
                //        showfulldetails(email, password);
                //    }



                //    //if (Session["UserCode"] != null)
                //    //{
                //    //    BookRef.Style.Remove("display");
                //    //    ReqBookRef.Enabled = true;
                //    //}
                //    //else
                //    //{
                //    //    BookRef.Style.Add("display", "None");
                //    //    ReqBookRef.Enabled = false;
                //    //}
                //    //lnkLogout.Visible = true;
                //}
                //else
                //{
                //    pnllogin.Visible = true;
                //    custRegis.Visible = true;
                //    //dvpanel.Visible = true;
                //    dvRefrence.Visible = true;

                //}
                if (Session["CustMailId"] != null || Session["UserName"] != null)
                {
                    this.pnlFullDetails.Visible = true;
                    pnlBookButton.Visible = true;
                    panelwithoutCreditAgent.Visible = true;
                    dvRefrence.Visible = false;
                    dvpaneldefault.Visible = false;

                }
                else
                {
                    this.pnlFullDetails.Visible = false;
                    pnlBookButton.Visible = false;
                    panelwithoutCreditAgent.Visible = false;
                    dvRefrence.Visible = true;
                }
            }
<<<<<<< HEAD


=======
>>>>>>> 06df147e7f6e76b3ddcb27473f8305164d96b955
            //this.pnllogin.Visible = false;

            Bookingdt = new DataTable();
            Bookingdt = SessionServices.RetrieveSession<DataTable>("Bookingdt");
            //Bookingdt = Session["Bookingdt"] as DataTable;
            bookingmealdt = new DataTable();
            bookingmealdt = SessionServices.RetrieveSession<DataTable>("BookinMealdt");
            // bookingmealdt = Session["BookinMealdt"] as DataTable;
            dtroominfo = new DataTable();
            dtroominfo = SessionServices.RetrieveSession<DataTable>("RoomInfo");
            //dtroominfo = Session["RoomInfo"] as DataTable;

<<<<<<< HEAD
            //loaddata();
=======
            loaddata();
>>>>>>> 06df147e7f6e76b3ddcb27473f8305164d96b955

            //lblChkin.Text = Session["Chkin"].ToString();
            //lblChkout.Text = Session["chkout"].ToString();

            //calcamt(Bookingdt);
            LoadCountries();
            btnPayProceed.Visible = true;
            //pnlCustReg.Visible = false;
            //customerLogin.Visible = false;
<<<<<<< HEAD


            if (Session["SetCurrency"].ToString() != "")
            {
                ddlCurrency.Text = Session["SetCurrency"].ToString();
                if (ddlCurrency.Text != "USD")
                {
                    ddlCurrency_SelectedIndexChanged(this, e);
                }
                else
                {
                    loaddata();
                }
            }
            else
            {
                loaddata();
            }

=======
>>>>>>> 06df147e7f6e76b3ddcb27473f8305164d96b955
        }
    }
    private void LoadCountries()
    {
        try
        {
<<<<<<< HEAD
            
=======
>>>>>>> 06df147e7f6e76b3ddcb27473f8305164d96b955
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
            //Bookingdt = Session["Bookingdt"] as DataTable;
            bookingmealdt = new DataTable();
            bookingmealdt = SessionServices.RetrieveSession<DataTable>("BookinMealdt");
            //bookingmealdt = Session["BookinMealdt"] as DataTable;
            dtroominfo = new DataTable();
            dtroominfo = SessionServices.RetrieveSession<DataTable>("RoomInfo");
            // dtroominfo = Session["RoomInfo"] as DataTable;
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
            Session["RoomDescription"] = dtroominfo.Rows[0]["RoomDescription"].ToString();
            dt.Rows.Add(imagepath, type, Convert.ToInt32(Bookingdt.Rows[0]["Pax"].ToString()), dtroominfo.Rows[0]["RoomType"].ToString(), dtroominfo.Rows[0]["description"].ToString(), dtroominfo.Rows[0]["Amtc"].ToString());
            DataTable dt12 = new DataTable();
            if (Session["foraddroom"] != null)
            {
                dt12 = Session["foraddroom"] as DataTable;
            }

            if (Bookingdt != null && Bookingdt.Rows.Count > 0)
            {
                for (int i = 0; i < dt12.Rows.Count; i++)
                {
                    total1 = total1 + Convert.ToDouble(Bookingdt.Rows[i]["Inclusivetax"].ToString().Split(' ')[1]);
                }
<<<<<<< HEAD
                if(ddlCurrency.Text != "USD")
                {
                    currency1(Convert.ToInt32(total1).ToString("##,0"), ddlCurrency.Text);
                    lblAllTotal.Text = "USD " + ViewState["Comman"];
                }
                else
                {
                    lblAllTotal.Text = "INR " + Convert.ToInt32(total1).ToString("##,0");

                }
               
=======
                lblAllTotal.Text = "INR " + Convert.ToInt32(total1).ToString("##,0");
>>>>>>> 06df147e7f6e76b3ddcb27473f8305164d96b955
            }

            gdvHotelRoomRates.DataSource = Bookingdt;
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

            DataTable dtCustomer = dlcus.getforguest(blcus);

            if (dtCustomer != null)
            {
                if (dtCustomer.Rows.Count > 0)
                {
                    dvRefrence.Visible = false;
                    buttonUpdate.Visible = true;

                    //Bookingdt = new DataTable();
                    //Bookingdt = SessionServices.RetrieveSession<DataTable>("Bookingdt");
                    //calcamt(Bookingdt);
                    ViewState["Pass"] = txtCustPass.Text.Trim();
                    if (Session["txtbillingaddress"] != null)
                    {
                        lblBillingAddress.Text = Session["txtbillingaddress"].ToString();
                    }
                    else
                    {
                        lblBillingAddress.Text = dlcus.GetBillingAddress(dtCustomer.Rows[0]);
                    }

                    //if (Session["specialrequest"] != null)
                    //{
                    //    lblSpecialRequest.Text = Session["specialrequest"].ToString();
                    //}
                    //else
                    //{
                    //    lblSpecialRequest.Text=
                    //}



                    lblAgentName.Text = DataSecurityManager.Decrypt(dtCustomer.Rows[0]["FirstName"].ToString()) + " " + DataSecurityManager.Decrypt(dtCustomer.Rows[0]["LastName"].ToString());
                    lbPaymentMethod.Text = DataSecurityManager.Decrypt(dtCustomer.Rows[0]["PaymentMethod"].ToString());
                    hdnfPhoneNumber.Value = DataSecurityManager.Decrypt(dtCustomer.Rows[0]["Telephone"].ToString());

                    Session["CustId"] = dtCustomer.Rows[0]["CustId"].ToString();
                    Session["CustomerCode"] = dtCustomer.Rows[0]["CustId"].ToString();

                    Session.Add("guest", DataSecurityManager.Decrypt(dtCustomer.Rows[0]["Email"].ToString()));
                    //Session.Add("CustPassword", DataSecurityManager.Decrypt(dtCustomer.Rows[0]["Password"].ToString()));
                    DataTable dtrpax = SessionServices.RetrieveSession<DataTable>("Bookingdt");
                    //DataTable dtrpax = Session["Bookingdt"] as DataTable;
                    //if (Session["HCheckin"] != null)
                    //{
                    //    lblBalancedate.Text = Convert.ToDateTime(Session["HCheckin"].ToString()).AddDays(-90).ToString("dddd, MMMM d, yyyy");
                    //}
                    string BookRef = DataSecurityManager.Decrypt(dtCustomer.Rows[0]["FirstName"].ToString()) + DataSecurityManager.Decrypt(dtCustomer.Rows[0]["LastName"].ToString()) + "X" + Convert.ToDouble(dtrpax.Compute("SUM(Pax)", string.Empty)).ToString();
                    ViewState["BookRef"] = BookRef;
                    lbPaymentMethod.Text = "N/A";
                    //Bookingdt = SessionServices.RetrieveSession<DataTable>("Bookingdt");
                    //calcamt(Bookingdt);


                    //tblBref.Visible = false;
                    //buttonUpdate.Visible = true;
                    //pnlFullDetails.Visible = true;
                    //panelwithoutCreditAgent.Visible = true;
                    //pnlBookButton.Visible = true;
                    //pnllogin.Visible = false;
                    //custRegis.Visible = false;
                    //pnllogin.Visible = false;
                    //regdv.Visible = false;
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
    private void loadall()
    {
        if (Session["UserCode"] != null)
        {
            // pnlLogin.Visible = true;
            try
            {
                customerLogin.Visible = false;
                blagentpayment._Action = "MailValidate";
                if (Session["AgentMailId"] != null && Session["Password"] != null)
                {
                    blagentpayment._EmailId = Session["AgentMailId"].ToString();
                    blagentpayment._Password = Session["Password"].ToString();

                    DataTable dtAgent = dlagentpayment.BindControls(blagentpayment);
                    if (dtAgent.Rows.Count > 0)
                    {
                        lblAgentName.Text = Session["UserName"].ToString();
                        lblBillingAddress.Text = dtAgent.Rows[0]["BillingAddress"].ToString();
                        lbPaymentMethod.Text = dtAgent.Rows[0]["PaymentMethod"].ToString();
                        hdnfPhoneNumber.Value = dtAgent.Rows[0]["Phone"].ToString();
                        hdnfCreditLimit.Value = dtAgent.Rows[0]["CreditLimit"].ToString();
                        bool oncredit = Convert.ToBoolean(dtAgent.Rows[0]["ChkCredit"].ToString());
                        //if (Session["Getcheckindate"] != null)
                        //{
                        //    lblBalancedate.Text = "(75% of total) to be paid prior to " + Convert.ToDateTime(Session["Getcheckindate"].ToString()).AddDays(-90).ToString("dddd, MMMM d, yyyy");
                        //}
                        pnlFullDetails.Visible = true;
                        pnlBookButton.Visible = true;
                        Bookingdt = new DataTable();
                        Bookingdt = SessionServices.RetrieveSession<DataTable>("Bookingdt");
                        //Bookingdt = Session["Bookingdt"] as DataTable;
                        if (Session["foraddroom"] != null)
                        {
                            Bookingdt = Session["foraddroom"] as DataTable;
                            calcamt(Bookingdt);
                        }
                        else
                        {
                            calcamt(Bookingdt);
                        }

                        if (oncredit)
                        {
                            panelwithoutCreditAgent.Visible = false;

                            btnPayProceed.Text = "Book";
                        }
                        else
                        {
                            panelwithoutCreditAgent.Visible = true;
                            btnPayProceed.Text = "Proceed For Payment";
                        }
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Showstatus", "javascript:alert('Agent Payment Details Not found')", true);
                    }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Showstatus", "javascript:alert('It seems you are not logged in')", true);
                }
            }
            catch (Exception sqe)
            {

            }

        }

        else
        {
            customerLogin.Visible = false;
            customerLogin.Visible = true;

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
                    //Bookingdt = Session["Bookingdt"] as DataTable;
                    if (Session["foraddroom"] != null)
                    {
                        Bookingdt = Session["foraddroom"] as DataTable;
                        calcamt(Bookingdt);
                    }
                    else
                    {
                        calcamt(Bookingdt);
                    }

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
                    //DataTable dtrpax = Session["Bookingdt"] as DataTable;

                    string BookRef = DataSecurityManager.Decrypt(dtCustomer.Rows[0]["FirstName"].ToString()) + DataSecurityManager.Decrypt(dtCustomer.Rows[0]["LastName"].ToString()) + "X" + Convert.ToDouble(dtrpax.Compute("SUM(Pax)", string.Empty)).ToString();
                    ViewState["BookRef"] = BookRef;
                    lbPaymentMethod.Text = "N/A";

                    dvpaneldefault.Visible = false;
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
                    //Bookingdt = Session["Bookingdt"] as DataTable;
                    if (Session["foraddroom"] != null)
                    {
                        Bookingdt = Session["foraddroom"] as DataTable;
                        calcamt(Bookingdt);
                    }
                    else
                    {
                        calcamt(Bookingdt);
                    }

                    ViewState["Pass"] = txtCustPass.Text.Trim();
                    Session["CustName"] = DataSecurityManager.Decrypt(dtCustomer.Rows[0]["FirstName"].ToString());
                    Session["CustomerCode"] = DataSecurityManager.Decrypt(dtCustomer.Rows[0]["CustId"].ToString());


                    lblBillingAddress.Text = dlcus.GetBillingAddress(dtCustomer.Rows[0]);
                    //if (Session["HCheckin"] != null)
                    //{
                    //    lblBalancedate.Text = "(75% of total) to be paid prior to " + Convert.ToDateTime(Session["HCheckin"].ToString()).AddDays(-30).ToString("dddd, MMMM d, yyyy");
                    //}

                    lblAgentName.Text = DataSecurityManager.Decrypt(dtCustomer.Rows[0]["FirstName"].ToString()) + " " + DataSecurityManager.Decrypt(dtCustomer.Rows[0]["LastName"].ToString());
                    lbPaymentMethod.Text = DataSecurityManager.Decrypt(dtCustomer.Rows[0]["PaymentMethod"].ToString());
                    hdnfPhoneNumber.Value = DataSecurityManager.Decrypt(dtCustomer.Rows[0]["Telephone"].ToString());

                    Session["CustId"] = DataSecurityManager.Decrypt(dtCustomer.Rows[0]["CustId"].ToString());
                    Session["CustomerCode"] = DataSecurityManager.Decrypt(dtCustomer.Rows[0]["CustId"].ToString());

                    Session.Add("CustomerMailId", DataSecurityManager.Decrypt(dtCustomer.Rows[0]["Email"].ToString()));
                    Session.Add("CustPassword", DataSecurityManager.Decrypt(dtCustomer.Rows[0]["Password"].ToString()));
                    DataTable dtrpax = SessionServices.RetrieveSession<DataTable>("Bookingdt");
                    //DataTable dtrpax = Session["Bookingdt"] as DataTable;

                    string BookRef = DataSecurityManager.Decrypt(dtCustomer.Rows[0]["FirstName"].ToString()) + DataSecurityManager.Decrypt(dtCustomer.Rows[0]["LastName"].ToString()) + "X" + Convert.ToDouble(dtrpax.Compute("SUM(Pax)", string.Empty)).ToString();
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
                    dvpaneldefault.Visible = false;
                    //dvpayment.Visible = true;
                    //dvBilling.Visible = true;
                    //divspcl.Visible = true;
                    tblBref.Visible = false;
                    dvreg.Visible = false;
                    //customerLogin.Visible = false;
                }
                else
                {
                    lblLoginMsg.Text = "Please enter valid credentials.";
                    lblLoginMsg.ForeColor = System.Drawing.Color.Red;
                }
            }
            else
            {
                lblLoginMsg.Text = "Please enter valid credentials.";
                lblLoginMsg.ForeColor = System.Drawing.Color.Red;
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
        if (chkRegTerm.Checked == true)
        {
            if (txtpassword.Text != txtConfirmPassword.Text)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Showstatus", "javascript:alert('Password Not match')", true);
                return;
            }
            else
            {
                checkfield();
                ValidateEmail(txtMailAddress.Text);
                checkphone(txtTelephone.Text);
                checkpostpostcode(txtPostcode.Text);

                ViewState["CustPass"] = txtpassword.Text.Trim();
                Session["term"] = true;
                sendMail();
            }
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Showstatus", "javascript:alert('Plaese select terms and condition')", true);
            return;
        }

    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        if (chkTerm.Checked == true)
        {
            if (txtFirstname1.Text == "" || txtLastanme1.Text == "" || txtEmailid1.Text == "" || txtMobilephone1.Text == "" || txtAddress11.Text == "" || txtCity1.Text == "" || txtState1.Text == "" || txtPostCode1.Text == "" || ddlCountry1.SelectedIndex == 0)
            {
                //Session["Phonecheck"] = 1;
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Showstatus", "javascript:alert('Plaese enter valid data')", true);
                return;
            }
            string email = txtEmailid1.Text;
            Regex regex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
            Match match = regex.Match(email);
            if (match.Success)
            {
                blcus.Email = email;
                blcus.action = "chkDuplicate";
                DataTable dt = dlcus.checkDuplicateemail(blcus);
                if (dt != null && dt.Rows.Count > 0)
                {

                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Showstatus", "javascript:alert('This email is already register')", true);
                    return;
                }
            }

            else
            {
                //Session["Phonecheck"] = 1;
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Showstatus", "javascript:alert('Please enter valid email')", true);
                return;
            }
            if (txtMobilephone1.Text != null || txtMobilephone1.Text != "")
            {
                try
                {
                    long value = Convert.ToInt64(txtMobilephone1.Text);
                    if (txtMobilephone1.Text.Length < 10)
                    {
                        //Session["Phonecheck"] = 1;
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Showstatus", "javascript:alert('Please enter valid phone no')", true);
                        return;
                    }
                }
                catch
                {
                    //Session["Phonecheck"] = 1;
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Showstatus", "javascript:alert('Please enter valid phone no')", true);
                    return;
                }
            }
            if (txtPostCode1.Text != null && txtPostCode1.Text != "")
            {
                if (txtPostCode1.Text.Length < 6)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Showstatus", "javascript:alert('Please enter valid Post Code')", true);
                    return;
                    //Session["Phonecheck"] = 1;
                }
                else
                {
                    try
                    {
                        long post = Convert.ToInt64(txtPostCode1.Text);
                    }
                    catch
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Showstatus", "javascript:alert('Please enter valid Post Code')", true);
                        //Session["Phonecheck"] = 1;
                        return;
                    }
                }
            }
            //checkfield1();
            //ValidateEmail1(txtEmailid1.Text);
            //checkphone(txtMobilephone1.Text);
            //checkpostpostcode(txtPostCode1.Text);
            clientRefrence();

            Session["guest"] = txtEmailid1.Text;
            showfulldetailsforguest(txtEmailid1.Text);
            Bookingdt = new DataTable();

            Bookingdt = SessionServices.RetrieveSession<DataTable>("Bookingdt");
            if (Session["foraddroom"] != null)
            {
                DataTable dtforadd = Session["foraddroom"] as DataTable;
                calcamt(dtforadd);
            }
            else
            {
                calcamt(Bookingdt);
            }

            lblUsername.Text = Session["CustName"].ToString();
            //Session["Refrence"] = "1";
            //ViewState["CustPass"] = txtPassword1.Text.Trim();
            pnlBookButton.Visible = true;
            customerLogin.Visible = false;
            custRegis.Visible = false;
            dvRefrence.Visible = false;
            panelwithoutCreditAgent.Visible = true;
            //lnkCustomerRegis.Visible = false;
            pnllogin.Visible = false;
            navlogin.Visible = false;
            LinkButton1.Visible = true;
            //dvpayment.Visible = true;
            dvreg.Visible = false;
            //dvBilling.Visible = true;
            //divspcl.Visible = true;
            BookRef.Visible = false;
            pnlFullDetails.Visible = true;
            dvpaneldefault.Visible = false;
            //if (Session["HCheckin"] != null)
            //{
            //    lblBalancedate.Text = Convert.ToDateTime(Session["HCheckin"].ToString()).AddDays(-90).ToString("dddd, MMMM d, yyyy");
            //}

            //sendMail1();
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Showstatus", "javascript:alert('Please select terms and condition')", true);
            //Session["Phonecheck"] = 1;
            return;
        }
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
                mail.CC.Add(ccEmail);

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
                mail.CC.Add(ccEmail);

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
                    return;
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
                blcus.action = "Save";
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

                if (chkTerm.Checked)
                {
                    blcus.term = true;
                }
                else
                {
                    blcus.term = false;
                }

                getQueryResponse = dlcus.AddGuset(blcus);


                if (getQueryResponse > 0)
                {
                    lblUsername.Text = "Hello " + Session["CustName"].ToString();
                    //lnkCustomerRegis.Visible = false;

                    //showfulldetailsforguest(txtEmailid1.Text);
                    //ScriptManager.RegisterStartupScript(this, this.GetType(), "Showstatus", "javascript:alert('Verification Done')", true);


                }
                else
                {
                    Session["Phonecheck"] = 1;
                }


            }

            catch
            {
                Session["Phonecheck"] = 1;
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
            blcus.action = "InsCustomers";
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

            if (Session["term"] != null)
            {
                bool kj = Convert.ToBoolean(Session["term"].ToString());
                blcus.term = Convert.ToBoolean(Session["term"].ToString());
            }
            getQueryResponse = dlcus.AddCustomers(blcus);


            if (getQueryResponse > 0)
            {
                lblUsername.Text = "Hello " + Session["CustName"].ToString();
                //lnkCustomerRegis.Visible = false;
                navlogin.Visible = false;
                LinkButton1.Visible = true;
                //dvpayment.Visible = true;
                BookRef.Visible = false;
                //dvBilling.Visible = true;
                //divspcl.Visible = true;
                pnlFullDetails.Visible = true;
                pnllogin.Visible = false;
                buttonUpdate.Visible = true;
                dvreg.Visible = false;
                dvpaneldefault.Visible = false;
                //if (Session["HCheckin"] != null)
                //{
                //    lblBalancedate.Text = Convert.ToDateTime(Session["HCheckin"].ToString()).AddDays(-90).ToString("dddd, MMMM d, yyyy");
                //}
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
            //DataTable dtbkdetails = Session["Bookingdt"] as DataTable;
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

            if (Session["UserCode"] != null)
            {
                blsr.agentcommission = getcommission(acmtpid, acmid);
            }
            int bookingId = dlsr.AddParentBookingDetail(blsr);

            var bookingDetails = dlsr.GetBookingDetails(bookingId);
            if (bookingDetails != null)
            {
                blsr.BookingCode = bookingDetails.BookingCode;
            }
            blsr._iBookingId = bookingId;

            //Session["tblBookingBAL"] = blsr;
            SessionServices.SaveSession<BALBooking>("tblBookingBAL", blsr);
            //Session["tblBookingBAL"] = blsr;
            return bookingId;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    private decimal getcommission(int accomtype, int accomname)
    {
        decimal commission = 0;
        DataTable dt = dlagentpayment.selectbyaccom(accomtype, accomname);
        if (dt != null && dt.Rows.Count > 0)
        {
            try
            {
                commission = Convert.ToDecimal(dt.Rows[0]["Commision"].ToString());
                commission = Convert.ToDecimal(txtPaidAmt.Text) * commission / 100;
            }
            catch { }
        }
        return commission;
    }
    public void BookTheHotel()
    {
        int iAccomId = 0;
        int iaccomtypeid = 0;
        int iagentid = 0;
        DateTime chkin, chkout;

<<<<<<< HEAD
        if (Session["AgentId"] != null)
        {
            iagentid = Convert.ToInt32(Session["AgentId"]);
        }
        else
        {
            if (Session["Usercode"] != null)
            {
                Int32.TryParse(Session["AId"].ToString(), out iagentid);
            }
            else
            {
                iagentid = 248;
            }
=======
        if (Session["Usercode"] != null)
        {
            Int32.TryParse(Session["AId"].ToString(), out iagentid);
        }
        else
        {
            iagentid = 248;
>>>>>>> 06df147e7f6e76b3ddcb27473f8305164d96b955
        }
        Int32.TryParse(Session["AccomId"].ToString(), out iAccomId);
        Int32.TryParse(Session["iAccomtypeId"].ToString(), out iaccomtypeid);

        DateTime.TryParse(Session["Chkin"].ToString(), out chkin);
        DateTime.TryParse(Session["chkout"].ToString(), out chkout);

        string bookref = Session["BookingRef"].ToString();
        string bookingPaymentId = Session["BookingPayId"].ToString();

        int bookingId = InsertBookingTableData(iAccomId, iaccomtypeid, iagentid, bookref, chkin, chkout);
<<<<<<< HEAD

        Session["BookingID"] = bookingId;

        try
        {
            SqlDataAdapter adp = new SqlDataAdapter("select * from tblbooking where bookingid=" + bookingId + "", con);
            DataTable dts = new DataTable();
            adp.Fill(dts);
            if (dts.Rows.Count > 0)
            {
                Session["bookingcodes"] = dts.Rows[0]["bookingcode"];
                Session["bookingdates"] = Convert.ToDateTime(dts.Rows[0]["bookingdate"].ToString()).ToString("d MMMM, yyyy");

            }
        }
        catch { }

=======
>>>>>>> 06df147e7f6e76b3ddcb27473f8305164d96b955
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
         //   int Pax = Convert.ToInt32(dtbkdetails.Rows[0]["Pax"]);

            //DataTable dtbkdetails = Session["Bookingdt"] as DataTable;
            BALBooking booking = dlsr.GetBookingDetails(bookingId);

            Session["maxBookId"] = bookingId;

            blsr._iBookingId = bookingId;
            blsr._iAccomId = booking._iAccomId;
            blsr.PaymentId = bookingPaymentId;
         //   booking._iPaxStaying = Pax;
            //int LoopInsertStatus = 0;
            for (int LoopCounter = 0; LoopCounter < dtbkdetails.Rows.Count; LoopCounter++)
            {
                blsr._dtStartDate = booking._dtStartDate;
                blsr._dtEndDate = booking._dtEndDate;
                //blsr._iPaxStaying = booking._iPaxStaying;
                blsr._iPaxStaying = Convert.ToInt32(dtbkdetails.Rows[LoopCounter]["Pax"]);
                blsr._bConvertTo_Double_Twin = Convert.ToBoolean(dtbkdetails.Rows[LoopCounter]["ConvDouble"].ToString());
                blsr._cRoomStatus = "B";
                blsr.roomcatid = Convert.ToInt32(dtbkdetails.Rows[LoopCounter][1].ToString());
                blsr._sRoomNo = dtbkdetails.Rows[LoopCounter][8].ToString();
                blsr._PaidAmount = Convert.ToDouble(Session["Paid"]);

                blsr.action = "AddPriceDetailsToo";
                string[] arr = dtbkdetails.Rows[LoopCounter]["Total"].ToString().Split(' ');
                blsr._Amt = Convert.ToDecimal(dtbkdetails.Rows[LoopCounter]["Inclusivetax"].ToString().Split('R')[1].Replace(",", ""));
                blsr.taxamount = Convert.ToDecimal(dtbkdetails.Rows[LoopCounter]["Tax"].ToString().Split('R')[1].Replace(",", ""));
                blsr.priceperperson = Convert.ToDecimal(dtbkdetails.Rows[LoopCounter]["Price"].ToString().Split('R')[1].Replace(",", ""));
                blsr.taxpercentage = Convert.ToDecimal(Session["Taxpax"]);
                blsr.bedconfig = dtbkdetails.Rows[LoopCounter]["RoomType"].ToString();
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
<<<<<<< HEAD

    public void sendMail_Proceed(string Name, string Email)
    {

        try
        {
            MailMessage mail = new MailMessage();
            SmtpClient SmtpServer = new SmtpClient(SmtpHost);
            mail.From = new MailAddress(SmtpUserId);
            if (Session["AgentEmailId"] != null)
            {
                mail.To.Add(Session["AgentEmailId"].ToString());
                string ccEmail = ConfigurationManager.AppSettings["ccEmail"];
                mail.CC.Add(ccEmail);
            }
            else
            {
                // string ccEmail = ConfigurationManager.AppSettings["ccEmail"];
                //string cEmail = ConfigurationManager.AppSettings["cEmail"];
                mail.To.Add("amit.rana475@gmail.com");
            }
            //string ccEmail = ConfigurationManager.AppSettings["ccEmail"];
            //mail.CC.Add(ccEmail);

            //mail.Subject = "Booking Info";

            string bookid = Session["BookingID"].ToString();
            StringBuilder sb = new StringBuilder();

            sb.Append("<div> The following details is being booked by the client </div> <div><br/></div>");
            string custid=Session["CustomerCode"].ToString();
            SqlDataAdapter adp1 = new SqlDataAdapter("select a.*, b.countryname from tblCustomers a inner join tblCountry b on a.CountryId =b.CountryId where a.custid=" + custid + " ", con);
            DataTable dt = new DataTable();
            adp1.Fill(dt);
            string firstname = "";
            string lastname = "";
            string title = "";
            if (dt.Rows.Count >0)
            {
                 firstname = DataSecurityManager.Decrypt(dt.Rows[0]["FirstName"].ToString());
                 lastname = DataSecurityManager.Decrypt(dt.Rows[0]["lastName"].ToString());
                string email = DataSecurityManager.Decrypt(dt.Rows[0]["Email"].ToString());
                string contact = DataSecurityManager.Decrypt(dt.Rows[0]["Telephone"].ToString());
                string city = DataSecurityManager.Decrypt(dt.Rows[0]["City"].ToString());
                string country = DataSecurityManager.Decrypt(dt.Rows[0]["countryname"].ToString());
                 title = DataSecurityManager.Decrypt(dt.Rows[0]["title"].ToString());
                sb.Append("<div> Name: " + title + ". " + firstname + " " + lastname + " </div>");
                sb.Append("<div> Email: " + Email + "</div>");
                sb.Append("<div> Contact No: " + contact + "</div>");
                sb.Append("<div> Country: " + country + "</div>");
                sb.Append("<div> City: " + city + "</div><div><br/></div>");
                
            }

            SqlDataAdapter adp = new SqlDataAdapter("Sp_SendMailFormat", con);
            adp.SelectCommand.CommandType = CommandType.StoredProcedure;
            adp.SelectCommand.Parameters.AddWithValue("@bookingid", bookid);
            DataSet dts = new DataSet();
            //DataTable dts = new DataTable();
            adp.Fill(dts);

       
            //sb.Append("<div> Dear "+Session["CustName"]+",</div> <br/>");
            //sb.Append("<div> We are truly pleased to confirm your booking as per the below details:</div> <div><br/></div> ");
            if (dts.Tables[0].Rows. Count > 0)
            {
                sb.Append("<table><tr><td style=background-color:#dce7e8> BookingId: </td><td> " + dts.Tables[0].Rows[0]["bookingcode"] + " </td></tr><tr><td style=background-color:#dce7e8> Accomdation: </td><td> " + dts.Tables[0].Rows[0]["accomname"] + " </td></tr><tr><td style=background-color:#dce7e8> Check In: </td><td> " + Convert.ToDateTime(dts.Tables[0].Rows[0]["startdate"]).ToString("d MMMM, yyyy") + " </td></tr><tr><td style=background-color:#dce7e8> Check Out: </td><td>" + Convert.ToDateTime(dts.Tables[0].Rows[0]["enddate"]).ToString("d MMMM, yyyy") + " </td></tr><tr><td style=background-color:#dce7e8> Pax: </td><td> " + dts.Tables[0].Rows[0]["noofpersons"] + " </td></tr><tr><td style=background-color:#dce7e8> Nights: </td><td> " + dts.Tables[0].Rows[0]["noofnights"] + " </td></tr></table>");


            }
            for (int i = 0; i < dts.Tables[1].Rows.Count; i++)
            {
                sb.Append("<table><tr style=background-color:#dce7e8><td> RoomCategory</td><td> RoomType </td><td> Booked </td><td> Waitlisted </td></tr><tr><td> " + dts.Tables[1].Rows[i]["RoomCategory"] + " </td><td> " + dts.Tables[1].Rows[i]["RoomType"] + " </td><td> " + dts.Tables[1].Rows[i]["Booked"] + "</td><td> " + dts.Tables[1].Rows[0]["Waitlist"] + " </td></tr></table>");
            }
            //string text = "<table><tr><td>BookingID :</td><td>"+ Session["bookingcodes"] + "</td></tr><tr><td>Accomdation</td><td>accomvalue</td></tr></tr><tr><td>CheckInDate :</td><td>"+ Session["bookingdates"] + "</td></tr><tr><td>CheckOutDate :</td><td>" + Session["bookingdates"] + "</td></tr><tr><td>Pax :</td><td>" + Session["bookingdates"] + "</td></tr><tr><td>Nights :</td><td>" + Session["bookingdates"] + "</td></tr><tr><td>RoomCategory</td><td>RoomType</td><td>Booked</td><td>Waitlisted</td></tr><tr><td>RoomCategory</td><td>RoomType</td><td>Booked</td><td>Waitlisted</td></tr></table>";



            //Session["getPaid"].ToString()

            //sb.Append("<div> &nbsp;Booking Amount: " + Session["getPaid"].ToString() + "</div>");
            sb.Append("<div> &nbsp;Booking Amount: " + dts.Tables[0].Rows[0]["paidamt"] + "</div>");
            //sb.Append("<div> &nbsp;Booking Amount: " + Session["NetAmounts"] + "</div>");
            //sb.Append("<div>");
            //sb.Append("<div> Booking No.-  " + Session["bookingcodes"] + ",</div> <div><br/></div> ");
            //sb.Append("<div> Date of Booking.-  " + Session["bookingdates"] + ",</div> <div><br/></div> ");


            sb.Append("<div><br/></div><br/><br/>");
            sb.Append("<div> With warm Regards,</div> ");
            sb.Append("<div> Reservations</div> ");
            sb.Append("<div> Far Horizon Tours Pvt. Ltd. India</div> <div><br/></div> ");
            //sb.Append("<img src='" + CompanyLogo + "' alt='Image'/><br /><div> " + CompanyName + "</div><div> " + CompanyAddress + " </div> <div> Phone: " + CompanyPhoneNo + " </div><div> Mobile: " + CompanyMobile + " </div><div><br/> </div> ");

            mail.Subject = "Reservation - " + title + ". " + firstname + " " + lastname + "  - " + dts.Tables[0].Rows[0]["bookingcode"] + " ";
            mail.IsBodyHtml = true;
            mail.Body = sb.ToString();
            SmtpServer.Port = 587;
            SmtpServer.Credentials = new System.Net.NetworkCredential(SmtpUserId, SmtpPassword);
            SmtpServer.EnableSsl = false;
            SmtpServer.Send(mail);
            //ScriptManager.RegisterStartupScript(this, this.GetType(), "Showstatus", "javascript:alert('Thank you for registering. To validate your email id we have sent a code on your email, please key in the same to validate it.')", true);
            custRegis.Visible = false;
            customerLogin.Visible = true;
            buttonUpdate.Visible = false;
            pnllogin.Visible = false;
            tableVerify.Visible = true;
            dvRefrence.Visible = false;
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Showstatus", "javascript:alert('" + ex.Message.ToString() + "')", true);
            return;
        }

    }
    protected void btnPayProceed_Click(object sender, EventArgs e)
    {
        
=======
    protected void btnPayProceed_Click(object sender, EventArgs e)
    {
>>>>>>> 06df147e7f6e76b3ddcb27473f8305164d96b955

        if (lbltotAmt.Text != "")
        {
            try
            {
<<<<<<< HEAD
                if (ddlCurrency.Text == "INR")
                {
                    ddlCurrency.Text = "USD";
                    ddlCurrency_SelectedIndexChanged(this, e);
                }

                Session["Proceed"] = "Proceed";
=======

>>>>>>> 06df147e7f6e76b3ddcb27473f8305164d96b955
                Session["Hotel"] = 1;
                if (btnPayProceed.Text == "Proceed For Payment")
                {
                    if (Session["UserCode"] != null)
                    {
                        //aev@farhorizonindia.com [1:48:55 PM] Augurs  Technologies Pvt. Ltd.: 12345

                        DataTable dtrpax = SessionServices.RetrieveSession<DataTable>("Bookingdt");
                        //DataTable dtrpax = Session["Bookingdt"] as DataTable;

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
                            string AgentLastName = DataSecurityManager.Decrypt(dtAgent.Rows[0]["LastName"].ToString());

                            string LastName = "XYZ";//agentLastName; //dtGetReturnedData.Rows[0]["LastName"].ToString();
                            string PaidAmt = hftxtpaidamt.Value.Trim().ToString();
                            string PaymentId = BookingPayId.ToString();
                            string BillingAddress = lblBillingAddress.Text.Trim().ToString();
                            Session["BookingPayId"] = txtBookRef.Text.Trim();// BookingPayId;

<<<<<<< HEAD
                            sendMail_Proceed(FirstName, Email);

=======
>>>>>>> 06df147e7f6e76b3ddcb27473f8305164d96b955
                            Session["Address"] = BillingAddress;
                            Session["InvName"] = FirstName;
                            BookTheHotel();
                            Session["SubInvName"] = FirstName;
                            
                            try
                            {
                                if (Session["getbalanceamount"] != null)
                                {
                                    if (Session["getbalanceamount"].ToString() != "0.00" || Session["getbalanceamount"].ToString() != "0")
                                    {
                                        int n = dlbook.adddueamount(email, Convert.ToDecimal(Session["getbalanceamount"].ToString()), PaymentId);
                                        if (n > 0)
                                        {

                                        }
                                    }
                                }
                            }
                            catch { }
                            string[] arr = { };
                            if (FirstName != "" && FirstName != null)
                            {
                                arr = FirstName.Split(' ');
                            }

                            if (Debugger.IsAttached)
                            {
                                RedirectToPaymentGatewayResponse();
                            }
                            else
                            {
                                //Response.Redirect("PaymentGatewayResponse.aspx?BookingPayId=" + PaymentId + "&EmailId=" + Email.ToString() + "&PhoneNumber=" + PhoneNumber.ToString() + "&FirstName=" + FirstName.ToString() + "&LastName=" + LastName.ToString() + "&PaidAmt=" + PaidAmt.ToString() + "&BillingAddress=" + BillingAddress.ToString());
                                //http://adventureresortscruises.in/Cruise/booking/sendtoairpay.aspx?BookedId=0&PackName=7N8D+Downstream+Cruise&NoOfNights=7&CheckinDate=12%2f4%2f2016&PackId=Pack4

                                //Response.Redirect("sendtoairpay.aspx?BookingPayId=" + PaymentId + "&EmailId=" + Email.ToString() + "&PhoneNumber=" + PhoneNumber.ToString() + "&FirstName=" + FirstName.ToString() + "&LastName=" + LastName.ToString() + "&PaidAmt=" + PaidAmt.ToString() + "&BillingAddress=" + BillingAddress.ToString());

                                Response.Redirect("sendtoairpay.aspx?BookingPayId=" + PaymentId + "&EmailId=" + Email.ToString() + "&PhoneNumber=" + PhoneNumber.ToString() + "&FirstName=" + arr[0].ToString() + "&LastName=" + LastName.ToString() + "&PaidAmt=" + PaidAmt.ToString() + "&BillingAddress=" + BillingAddress.ToString());

                                //ScriptManager.RegisterStartupScript(this, this.GetType(), "Showstatus", "javascript:alert('not registered!!!')", true);
                            }
                        }
                    }
                    else
                    {
                        Session.Add("BookingRef", ViewState["BookRef"].ToString());
                        DataTable dtCustomer = new DataTable();
                        if (Session["guest"] != null)
                        {
                            blcus.Email = Session["guest"].ToString();
                            blcus.action = "getforguest";
                            dtCustomer = dlcus.getforguest(blcus);
                        }
                        else
                        {
                            blcus.Email = Session["CustomerMailId"].ToString();
                            blcus.Password = Session["CustPassword"].ToString();

                            blcus.action = "LoginCust";
                            dtCustomer = dlcus.checkDuplicateemail(blcus);
                        }

                        //blcus.action = "LoginCust";
                        //DataTable dtCustomer = dlcus.checkDuplicateemail(blcus);
                        Session["Paid"] = Convert.ToDouble(txtPaidAmt.Text.Trim() == "" ? "0" : txtPaidAmt.Text.Trim());
                        if (dtCustomer.Rows.Count > 0)
                        {
                            Random rnd = new Random();
                            string BookingPayId = rnd.Next(10000, 20000).ToString() + DateTime.Now.ToString("MMddhhmmssfff");
                            Session["BookingPayId"] = BookingPayId;
                            string Email = "";
                            if (Session["guest"] != null)
                            {
                                Email = Session["guest"].ToString();
                            }
                            else
                            {
                                Email = Session["CustomerMailId"].ToString();
                            }

                            BookTheHotel();
                            string PhoneNumber = hdnfPhoneNumber.Value.Trim().ToString();
                            string FirstName = DataSecurityManager.Decrypt(dtCustomer.Rows[0]["FirstName"].ToString());
                            string LastName = DataSecurityManager.Decrypt(dtCustomer.Rows[0]["LastName"].ToString());
                            string PaidAmt = hftxtpaidamt.Value.Trim().ToString();
                            string PaymentId = BookingPayId.ToString();
                            string BillingAddress = lblBillingAddress.Text.Trim().ToString();
                            Session["Address"] = BillingAddress;
<<<<<<< HEAD

                            sendMail_Proceed(FirstName, Email);


                            Session["InvName"] = DataSecurityManager.Decrypt(dtCustomer.Rows[0]["Title"].ToString()) + ". " + FirstName + " " + LastName;
=======
                            Session["InvName"] = DataSecurityManager.Decrypt(dtCustomer.Rows[0]["Title"].ToString()) + " " + FirstName + " " + LastName;
>>>>>>> 06df147e7f6e76b3ddcb27473f8305164d96b955
                            Session["SubInvName"] = LastName + ", " + DataSecurityManager.Decrypt(dtCustomer.Rows[0]["Title"].ToString()) + " " + FirstName;
                            try
                            {
                                if (Session["getbalanceamount"] != null)
                                {
                                    if (Session["getbalanceamount"].ToString() != "0.00" || Session["getbalanceamount"].ToString() != "0")
                                    {
                                        int n = dlbook.adddueamount(email, Convert.ToDecimal(Session["getbalanceamount"].ToString()), PaymentId);
                                        if (n > 0)
                                        {

                                        }
                                    }
                                }
                            }
                            catch { }
                            if (Debugger.IsAttached)
                            {
                                RedirectToPaymentGatewayResponse();
                            }
                            else
                            {
                                Response.Redirect("sendtoairpay.aspx?BookingPayId=" + PaymentId + "&EmailId=" + Email.ToString() + "&PhoneNumber=" + PhoneNumber.ToString() + "&FirstName=" + FirstName.ToString() + "&LastName=" + LastName.ToString() + "&PaidAmt=" + PaidAmt.ToString() + "&BillingAddress=" + BillingAddress.ToString());

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
                        blagentpayment._Action = "MailValidate";
                        blagentpayment._EmailId = Session["AgentMailId"].ToString();
                        blagentpayment._Password = Session["Password"].ToString();
                        DataTable dtAgent = dlagentpayment.BindControls(blagentpayment);


                        string BookingPayId = lbPaymentMethod.Text.Trim().Substring(0, 2) + DateTime.Now.ToString("MMddhhmmssfff");
                        Session["BookingPayId"] = BookingPayId;
                        Session.Add("BookingRef", txtBookRef.Text.Trim().ToString());
                        Session["Paid"] = Convert.ToDouble(hftxtpaidamt.Value.Trim() == "" ? "0" : hftxtpaidamt.Value.Trim().ToString());

                        string FirstName = DataSecurityManager.Decrypt(dtAgent.Rows[0]["FirstName"].ToString());
                        string LastName = DataSecurityManager.Decrypt(dtAgent.Rows[0]["LastName"].ToString());
                        Session["InvName"] = Session["UserName"].ToString();

                        Session["SubInvName"] = FirstName;

                        Session["Address"] = null;
                        BookTheHotel();
                        if (Debugger.IsAttached)
                        {
                            RedirectToPaymentGatewayResponse();
                        }
                        else
                        {


                            //Response.Redirect("PaymentGatewayResponse.aspx?BookingPayId=" + PaymentId + "&EmailId=" + Email.ToString() + "&PhoneNumber=" + PhoneNumber.ToString() + "&FirstName=" + FirstName.ToString() + "&LastName=" + LastName.ToString() + "&PaidAmt=" + PaidAmt.ToString() + "&BillingAddress=" + BillingAddress.ToString());
                            //http://adventureresortscruises.in/Cruise/booking/sendtoairpay.aspx?BookedId=0&PackName=7N8D+Downstream+Cruise&NoOfNights=7&CheckinDate=12%2f4%2f2016&PackId=Pack4
                           // Response.Redirect("sendtoairpay.aspx?BookingPayId=" + PaymentId + "&EmailId=" + Email.ToString() + "&PhoneNumber=" + PhoneNumber.ToString() + "&FirstName=" + FirstName.ToString() + "&LastName=" + LastName.ToString() + "&PaidAmt=" + PaidAmt.ToString() + "&BillingAddress=" + BillingAddress.ToString());
                            //ScriptManager.RegisterStartupScript(this, this.GetType(), "Showstatus", "javascript:alert('not registered!!!')", true);
                        }

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
        else

        {
            return;
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
        double TotalPaybleAmt = 0;
        double tottax = 0;
        try
        {
            if (dts.Rows.Count > 0 && dts != null)
            {
                for (int j = 0; j < dts.Rows.Count; j++)
                {
                    string[] arr = dts.Rows[j]["Total"].ToString().Split(' ');

                    TotalPaybleAmt = TotalPaybleAmt + Convert.ToInt32(arr[1].Replace(",", string.Empty));

                    tottax = tottax + Convert.ToDouble(dts.Rows[j]["Tax"].ToString().Split(' ')[1].Replace(",", string.Empty));
                }
<<<<<<< HEAD
                if(ddlCurrency.Text != "USD")
                {
                    currency1(tottax.ToString("##,0"), ddlCurrency.Text);
                    lblTax.Text = "USD " + ViewState["Comman"];

                    currency1(Session["gettotal"].ToString(), ddlCurrency.Text);
                    lblGross.Text = "USD " +ViewState["Comman"] ;

                }
                else
                {
                    lblTax.Text = "INR " + tottax.ToString("##,0");
                    lblGross.Text = dts.Rows[0]["Currency"].ToString() + " " + Session["gettotal"].ToString();

                }

              
=======
                lblTax.Text = "INR " + tottax.ToString("##,0");
                lblGross.Text = dts.Rows[0]["Currency"].ToString() + " " + Session["gettotal"].ToString();
>>>>>>> 06df147e7f6e76b3ddcb27473f8305164d96b955

                hdnfTotalPaybleAmt.Value = lblGross.Text;
                //lblGross.Text = Convert.ToInt32(lblGross.Text).ToString("##,0");
                if (Convert.ToDateTime(Session["HCheckin"].ToString()).AddDays(-30) < System.DateTime.Now)
                {
<<<<<<< HEAD
                    if(ddlCurrency.Text != "USD")
                    {
                        currency1(Math.Round(((100 * Convert.ToDouble(Session["gettotal"].ToString())) / 100)).ToString("##,0"), ddlCurrency.Text);
                        txtPaidAmt.Text = ViewState["Comman"].ToString();
                    }
                    else
                    {
                        txtPaidAmt.Text = Math.Round(((100 * Convert.ToDouble(Session["gettotal"].ToString())) / 100)).ToString("##,0");
                    }

                  
=======
                    txtPaidAmt.Text = Math.Round(((100 * Convert.ToDouble(Session["gettotal"].ToString())) / 100)).ToString("##,0");
>>>>>>> 06df147e7f6e76b3ddcb27473f8305164d96b955
                    lblpertext.Text = "";
                    Priorto.Text = " ";
                    ///lblPrToDate.Text = "N/A";

                    lblBalancedate.Text = "N/A";
                    trbalancedate.Visible = false;
                }
                else
                {
<<<<<<< HEAD
                    if (ddlCurrency.Text != "USD")
                    {
                        currency1(Math.Round(((25 * Convert.ToDouble(Session["gettotal"].ToString())) / 100)).ToString("##,0"), ddlCurrency.Text);
                        txtPaidAmt.Text = ViewState["Comman"].ToString();
                    }
                    else
                    {
                        txtPaidAmt.Text = Math.Round(((25 * Convert.ToDouble(Session["gettotal"].ToString())) / 100)).ToString("##,0");
                    }
                    //txtPaidAmt.Text = Math.Round(((25 * Convert.ToDouble(Session["gettotal"].ToString())) / 100)).ToString("##,0");
=======
                    txtPaidAmt.Text = Math.Round(((25 * Convert.ToDouble(Session["gettotal"].ToString())) / 100)).ToString("##,0");
>>>>>>> 06df147e7f6e76b3ddcb27473f8305164d96b955
                    if (Session["HCheckin"] != null)
                    {
                        lblBalancedate.Text = Convert.ToDateTime(Session["HCheckin"].ToString()).AddDays(-30).ToString("dddd, MMMM d, yyyy");
                    }
                    lblpertext.Text = "(25% of Total)";
                    //Priorto.Text = "(75% of total) to be paid prior to";
                    //lblPrToDate.Text = Convert.ToDateTime(Request.QueryString["CheckinDate"]).AddDays(-90).ToString("dddd, MMMM d, yyyy");
                }

                hftxtpaidamt.Value = Convert.ToDouble(txtPaidAmt.Text).ToString("N2").Replace(",", "");
                //txtPaidAmt.Text = Math.Round(((25 * TotalPaybleAmt) / 100)).ToString("N2");
                Bookingdt = SessionServices.RetrieveSession<DataTable>("Bookingdt");
                //Bookingdt = Session["Bookingdt"] as DataTable;
                if (Session["Taxpax"] != null)
                {
                    lbltextIn.Text = Session["Taxpax"].ToString() + "%";
                }
<<<<<<< HEAD
                if(ddlCurrency.Text != "USD")
                {
                    currency1(TotalPaybleAmt.ToString("##,0"), ddlCurrency.Text);
                    lbltotAmt.Text = "USD " + ViewState["Comman"] ;
                }
                else
                {
                    lbltotAmt.Text = Bookingdt.Rows[0]["Currency"].ToString() + " " + TotalPaybleAmt.ToString("##,0");

                }
                if(ddlCurrency.Text != "USD")
                {
                    lblCurrency.Text = "USD ";
                }
                else
                {
                    lblCurrency.Text = Bookingdt.Rows[0]["Currency"].ToString().ToString() + " ";

                }

                
=======
                lbltotAmt.Text = Bookingdt.Rows[0]["Currency"].ToString() + " " + TotalPaybleAmt.ToString("##,0");
                lblCurrency.Text = Bookingdt.Rows[0]["Currency"].ToString().ToString() + " ";
>>>>>>> 06df147e7f6e76b3ddcb27473f8305164d96b955


                //txtPaidAmt.Text = Math.Round(((25 * TotalPaybleAmt) / 100)).ToString("#.##");
                //hftxtpaidamt.Value = Convert.ToDouble(txtPaidAmt.Text).ToString("N2").Replace(",", "");

<<<<<<< HEAD
                if(ddlCurrency.Text !="USD")
                {
                    currency1(Math.Round((Convert.ToDouble(Session["gettotal"].ToString()) - Convert.ToDouble(txtPaidAmt.Text.Replace(",", string.Empty)))).ToString("##,0"), ddlCurrency.Text);
                    lblBalanceAmt.Text ="USD " + ViewState["Comman"];
                }
                else
                {
                    lblBalanceAmt.Text = Bookingdt.Rows[0]["Currency"].ToString().ToString() + " " + Math.Round((Convert.ToDouble(Session["gettotal"].ToString()) - Convert.ToDouble(txtPaidAmt.Text.Replace(",", string.Empty)))).ToString("##,0");
                }

                //lblBalanceAmt.Text = Bookingdt.Rows[0]["Currency"].ToString().ToString() + " " + Math.Round((Convert.ToDouble(Session["gettotal"].ToString()) - Convert.ToDouble(txtPaidAmt.Text.Replace(",", string.Empty)))).ToString("##,0");
=======
                lblBalanceAmt.Text = Bookingdt.Rows[0]["Currency"].ToString().ToString() + " " + Math.Round((Convert.ToDouble(Session["gettotal"].ToString()) - Convert.ToDouble(txtPaidAmt.Text.Replace(",", string.Empty)))).ToString("##,0");
>>>>>>> 06df147e7f6e76b3ddcb27473f8305164d96b955
                string getvalue = Math.Round((Convert.ToDouble(Session["gettotal"].ToString()) - Convert.ToDouble(txtPaidAmt.Text.Replace(",", string.Empty)))).ToString("##,0");
                if (getvalue == "0")
                {
                    trbalanceamount.Visible = false;
                }
                Session["getbalanceamount"] = Math.Round((Convert.ToDouble(Session["gettotal"].ToString()) - Convert.ToDouble(txtPaidAmt.Text))).ToString();
                //   lbltotAmt.Text = dtGetBookedRooms.Rows[0]["Currency"].ToString() + " " + TotalPaybleAmt.ToString();

            }
            else
            {
                lblGross.Text = "";
                txtPaidAmt.Text = "";
                hftxtpaidamt.Value = "";
                lblBalanceAmt.Text = "";
                lbltotAmt.Text = "";
                lblCurrency.Text = "";
                lblBalancedate.Text = "";
                lblTax.Text = "";
                lblBalancedate.Text = "";

            }
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

                DataTable dtCustomer = new DataTable();


                if (Session["guest"] != null)
                {
                    blcus.action = "getforguest";
                    dtCustomer = dlcus.getforguest(blcus);
                }
                else
                {

                    blcus.action = "LoginCust";

                    dtCustomer = dlcus.checkDuplicateemail(blcus);
                }

                if (dtCustomer != null)
                {
                    if (dtCustomer.Rows.Count > 0)
                    {


                        Bookingdt = new DataTable();
                        Bookingdt = SessionServices.RetrieveSession<DataTable>("Bookingdt");
                        //Bookingdt =Session["Bookingdt"] as DataTable;
                        if (Session["foraddroom"] != null)
                        {
                            Bookingdt = Session["foraddroom"] as DataTable;
                            calcamt(Bookingdt);
                        }
                        else
                        {
                            calcamt(Bookingdt);
                        }

                        ViewState["Pass"] = txtCustPass.Text.Trim();

                        if (txtBilingAddress.Text != null && txtBilingAddress.Text != "")
                        {
                            lblBillingAddress.Text = txtBilingAddress.Text;
                            Session["txtbillingaddress"] = txtBilingAddress.Text;
                        }
                        else
                        {
                            Session["txtbillingaddress"] = null;
                            lblBillingAddress.Text = dlcus.GetBillingAddress(dtCustomer.Rows[0]);
                        }
                        //if (specialrequest.InnerText != null && specialrequest.InnerText != "")
                        //{
                        //    Session["specialrequest"] = specialrequest.InnerText;
                        //    lblSpecialRequest.Text = specialrequest.InnerText;
                        //}
                        //else
                        //{
                        //    lblSpecialRequest.Text = "N/A";
                        //}


                        lblAgentName.Text = DataSecurityManager.Decrypt(dtCustomer.Rows[0]["FirstName"].ToString()) + " " + DataSecurityManager.Decrypt(dtCustomer.Rows[0]["LastName"].ToString());
                        lbPaymentMethod.Text = DataSecurityManager.Decrypt(dtCustomer.Rows[0]["PaymentMethod"].ToString());
                        hdnfPhoneNumber.Value = DataSecurityManager.Decrypt(dtCustomer.Rows[0]["Telephone"].ToString());

                        Session["CustId"] = dtCustomer.Rows[0]["CustId"].ToString();
                        Session["CustomerCode"] = dtCustomer.Rows[0]["CustId"].ToString();

                        Session.Add("CustomerMailId", DataSecurityManager.Decrypt(dtCustomer.Rows[0]["Email"].ToString()));
                        Session.Add("CustPassword", DataSecurityManager.Decrypt(dtCustomer.Rows[0]["Password"].ToString()));
                        DataTable dtrpax = SessionServices.RetrieveSession<DataTable>("Bookingdt");
                        //DataTable dtrpax =  Session["Bookingdt"] as DataTable;

                        string BookRef = DataSecurityManager.Decrypt(dtCustomer.Rows[0]["FirstName"].ToString()) + DataSecurityManager.Decrypt(dtCustomer.Rows[0]["LastName"].ToString()) + "X" + Convert.ToDouble(dtrpax.Compute("SUM(Pax)", string.Empty)).ToString();
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
        //if (Session["HNoofrooms"] != null)
        //{
        //    Session["HNoofrooms"] = Convert.ToDecimal(Session["HNoofrooms"]) + 1;
        //}
        //else
        //{
        //    Session["HNoofrooms"] = 2;
        //}
        if (Session["HNoofrooms"] != null)
        {
            string ijl = Session["HNoofrooms"].ToString();
            if (Convert.ToDouble(Session["HNoofrooms"]) > 1)
            {
                Session["getcheck"] = 1;
            }
            else
            {
                Session["getcheck"] = null;
            }
        }
        else
        {
            Session["getcheck"] = 1;
        }
        Session["dvclass"] = 1;
        Response.Redirect("available.aspx");
    }

    protected void gdvHotelRoomRates_RowDataBound(object sender, GridViewRowEventArgs e)
    {

    }
    public void CalculateRoomRates()
    {
        double rtotal = 0;
        DataTable dt = Session["foraddroom"] as DataTable;
        try
        {
            for (int j = 0; j < dt.Rows.Count; j++)
            {
                double value = Convert.ToDouble(dt.Rows[j]["Inclusivetax"].ToString().Split(' ')[1]);


                rtotal = rtotal + value;
            }
<<<<<<< HEAD
            if(ddlCurrency.Text != "USD")
            {
                currency1(rtotal.ToString("##,0"), ddlCurrency.Text);
                lblAllTotal.Text = "USD " + ViewState["Comman"];
                Session["gettotal"] = rtotal.ToString("##,0");
            }
            else
            {
                lblAllTotal.Text = "INR " + rtotal.ToString("##,0");
                Session["gettotal"] = rtotal.ToString("##,0");
            }
          
=======
            lblAllTotal.Text = "INR " + rtotal.ToString("##,0");
            Session["gettotal"] = rtotal.ToString("##,0");
>>>>>>> 06df147e7f6e76b3ddcb27473f8305164d96b955
        }
        catch
        {

        }
    }
    protected void gdvHotelRoomRates_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Remove")
        {
            try
            {
                ImageButton imgbtn = (ImageButton)e.CommandSource;
                GridViewRow grow = (GridViewRow)imgbtn.NamingContainer;
                DataTable dtnew = Session["foraddroom"] as DataTable;
                dtnew.Rows.RemoveAt(grow.RowIndex);
                dtnew.AcceptChanges();
                Session["foraddroom"] = dtnew;
                //Session["foraddroom"] = dtnew;
                gdvHotelRoomRates.DataSource = dtnew;
                if (Convert.ToDouble(Session["HNoofrooms"]) != 0)
                {
                    Session["HNoofrooms"] = Convert.ToDouble(Session["HNoofrooms"]) - 1;
                    if (Convert.ToDouble(Session["HNoofrooms"]) > 1)
                    { }
                    else
                    {
                        Session["getcheck"] = null;
                    }
                }
                else
                {
                    Session["getcheck"] = null;
                    Session["HNoofrooms"] = 0;
                }
                //Session["Bookingdt"] = null;
                gdvHotelRoomRates.DataBind();
                CalculateRoomRates();
                calcamt(dtnew);
                //if (gdvHotelRoomRates.Rows.Count > 0)
                //{
                //    DivRmRate.Style.Remove("display");
                //}
                //else
                //{
                //    DivRmRate.Style.Add("display", "None");
                //    ViewState["VsRoomDetails"] = null;
                //}
            }
            catch (Exception ex)
            {
            }
        }
    }
    private string GetConnectionString()
    {

        return Convert.ToString(System.Configuration.ConfigurationManager.ConnectionStrings["ReservationConnectionString"]);


    }
    private void PopulateModule(int AccomId)
    {

        con.Open();
        SqlDataAdapter da = new SqlDataAdapter("select AccomPolicyUrl from tblAccomMaster where  AccomId=" + AccomId, con);
        DataTable dt = new DataTable();
        da.Fill(dt);


        ViewState["ModuleDt"] = dt.Rows[0]["AccomPolicyUrl"].ToString();
        con.Close();
        // chkModule.Items.Insert(0, new ListItem("All", "0"));


    }
    //protected void lbtnTandC_Click(object sender, EventArgs e)
    //{
    //    PopulateModule(Convert.ToInt32(Session["AccomId"]));
    //    string AccomPolicyUrl = ViewState["ModuleDt"].ToString();
    //    //Response.Redirect("" + AccomPolicyUrl + "");
    //    string s = "window.open('" + AccomPolicyUrl + "', 'popup_window', 'width=750,height=600,resizable=yes');";
    //    ClientScript.RegisterStartupScript(this.GetType(), "script", s, true);
    //    //btnPayProceed.Visible = true;

    //    // string url = Request.Url.ToString();
    //    // string newUrl = url.Replace(url, "" + AccomPolicyUrl + "");
    //    //string newUrl = url.Replace(url.Substring(url.IndexOf("Services.aspx?") + "Services.aspx?".Length), string.Format("idProject={0}&idService={1}", Services.IdProject, Services.IdService));
    //    //Response.Redirect("~/" + AccomPolicyUrl + "");
    //    // Response.Redirect("" + newUrl + "");
    //    // Response.Redirect("" + newUrl + "");
    //    //  ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), "alert('Process successfully Done.');window.location='" + newUrl + "';", true);

    //}
    protected void chkterms_CheckedChanged(object sender, EventArgs e)
    {
        if (chkterms.Checked)
        {
            btnPayProceed.Visible = true;
        }
        else
        {
            btnPayProceed.Visible = false;
        }
    }
<<<<<<< HEAD


    public string currency(string amount, string code)
    {
        var countryname = "";
        try
        {
            if (Session["Dollar"].ToString() == "1")
            {
                WebClient obj = new WebClient();
                var rsponse = obj.DownloadString("http://www.apilayer.net/api/live?access_key=1f011c76b84ee0d412dd6611c0545986&format=1&source=USD");
                dynamic data = JObject.Parse(rsponse);
                var aa = data.quotes;
                foreach (var r in aa)
                {
                    var sd = "USD" + code;
                    if (r.Name.Trim().ToLower() == sd.Trim().ToLower())
                    {
                        var m = r.Name;
                        var n = r.Value;

                        string input = amount;
                        string sub = input.Substring(4);

                        decimal amounts = Convert.ToDecimal(sub);
                        decimal dollar = Convert.ToDecimal(r.Value);
                        var am = amounts / dollar;
                        Session["Dollar"] = dollar;
                        string value = am.ToString("#.00");




                        ViewState["Comman"] = value;

                        break;
                    }
                    else
                    {

                        if (r.Name.Trim().ToLower() == "usdusd")
                        {
                            var am = String.Format("{0:0.00}", r.Value * amount);
                            return am;
                        }
                    }
                }
            }
            else
            {
                decimal dollar = Convert.ToDecimal(Session["Dollar"]);

                string input = amount;

             
                string sub = input.Substring(4);
             

                decimal amounts = Convert.ToDecimal(sub);

                var am = amounts / dollar;
                Session["Dollar"] = dollar;
                string value = am.ToString("#.00");


                ViewState["Comman"] = value;

            }

        }
        catch { }
        return countryname;
    }
    public string currency1(string amount, string code)
    {
        var countryname = "";
        try
        {
            if (Session["Dollar"].ToString() == "1")
            {
                WebClient obj = new WebClient();
                var rsponse = obj.DownloadString("http://www.apilayer.net/api/live?access_key=1f011c76b84ee0d412dd6611c0545986&format=1&source=USD");
                dynamic data = JObject.Parse(rsponse);
                var aa = data.quotes;
                foreach (var r in aa)
                {
                    var sd = "USD" + code;
                    if (r.Name.Trim().ToLower() == sd.Trim().ToLower())
                    {
                        var m = r.Name;
                        var n = r.Value;
                        decimal amounts = Convert.ToDecimal(amount);
                        decimal dollar = Convert.ToDecimal(r.Value);
                        var am = amounts / dollar;
                        Session["Dollar"] = dollar;
                        string value = am.ToString("#.00");




                        ViewState["Comman"] = value;

                        break;
                    }
                    else
                    {

                        if (r.Name.Trim().ToLower() == "usdusd")
                        {
                            var am = String.Format("{0:0.00}", r.Value * amount);
                            return am;
                        }
                    }
                }
            }
            else
            {
                decimal dollar = Convert.ToDecimal(Session["Dollar"]);

                
                decimal amounts = Convert.ToDecimal(amount);

                var am = amounts / dollar;
                Session["Dollar"] = dollar;
                string value = am.ToString("#.00");


                ViewState["Comman"] = value;

            }

        }
        catch { }
        return countryname;
    }
    protected void ddlCurrency_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlCurrency.Text != "USD")
        {
            Session["SetCurrency"] = "USD";

        }
        else
        {
            Session["SetCurrency"] = "INR";
        }
        if (Session["AccomName"] != null)
        {
            if (Session["AccomName"].ToString() == "Dera Dune Retreat, Jamba")
            {
            }
            if (Session["AccomName"].ToString() == "Vaikundam, Backwaters" || Session["AccomName"].ToString() == "Sauvernigam, Backwaters")
            {
            }
            if (Session["AccomName"].ToString() == "Dera Village Retreat, Kalakho")
            {
            }
        }
        if (Session["guest"] != null)
        {
            dvpaneldefault.Visible = false;
            lblUsername.Text = "Hello " + Session["CustName"].ToString();
            showfulldetailsforguest(Session["guest"].ToString());
            Bookingdt = new DataTable();
            Bookingdt = SessionServices.RetrieveSession<DataTable>("Bookingdt");
            calcamt(Bookingdt);
            customerLogin.Visible = false;
            custRegis.Visible = false;
            dvRefrence.Visible = false;
            panelwithoutCreditAgent.Visible = true;
            pnllogin.Visible = false;
            navlogin.Visible = false;
            LinkButton1.Visible = true;
            dvreg.Visible = false;
            BookRef.Visible = false;
            pnlFullDetails.Visible = true;

        }
        if (Session["guest"] == null)
        {
            if (Session["CustMailId"] != null || Session["UserName"] != null)
            {
                this.pnlFullDetails.Visible = true;
                pnlBookButton.Visible = true;
                panelwithoutCreditAgent.Visible = true;
                dvRefrence.Visible = false;
                dvpaneldefault.Visible = false;

            }
            else
            {
                this.pnlFullDetails.Visible = false;
                pnlBookButton.Visible = false;
                panelwithoutCreditAgent.Visible = false;
                dvRefrence.Visible = true;
            }
        }

        Bookingdt = new DataTable();
        Bookingdt = SessionServices.RetrieveSession<DataTable>("Bookingdt");
        bookingmealdt = new DataTable();
        bookingmealdt = SessionServices.RetrieveSession<DataTable>("BookinMealdt");
        dtroominfo = new DataTable();
        dtroominfo = SessionServices.RetrieveSession<DataTable>("RoomInfo");
        loaddata();
        LoadCountries();
        btnPayProceed.Visible = true;

    }
=======
>>>>>>> 06df147e7f6e76b3ddcb27473f8305164d96b955
}