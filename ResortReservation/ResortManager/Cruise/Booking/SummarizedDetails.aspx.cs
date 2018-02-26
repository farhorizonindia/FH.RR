using FarHorizon.DataSecurity;
using FarHorizon.Reservations.BusinessServices;
using FarHorizon.Reservations.BusinessServices.Online.BAL;
using FarHorizon.Reservations.BusinessServices.Online.DAL;
using FarHorizon.Reservations.Common;
using FarHorizon.Reservations.Common.DataEntities.Masters;
using FarHorizon.Reservations.MasterServices;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Net.Mail;
using System.Text;
using System.Text.RegularExpressions;
using System.Configuration;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
public partial class Cruise_Booking_SummarizedDetails : System.Web.UI.Page
{

    #region Variable(s)
    DataTable Bookingdt;
    DataTable bookingmealdt;
    double TotalPaybleAmt = 0;
    public DataTable dtGetBookedRooms;
    public int LoopCounter = 0;
    public DataTable dtrpax;
    BALAgentPayment blagentpayment = new BALAgentPayment();
    DALAgentPayment dlagentpayment = new DALAgentPayment();

    DALOpenDates dlOpenDates = new DALOpenDates();
    BALOpenDates blOpenDates = new BALOpenDates();

    BALSearch blsrch = new BALSearch();
    DALSearch dlsrch = new DALSearch();
    DALBooking dlbook = new DALBooking();
    BALCustomers blcus = new BALCustomers();
    DALCustomers dlcus = new DALCustomers();
    BALCustomers balcustomer = new BALCustomers();
    DALCustomers dalcustomer = new DALCustomers();
    //DataTable dtGetReturnedData;
    int getQueryResponse = 0;

    int CountryId = 0;
    DataTable dt = new DataTable();
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
    SqlConnection con;
    protected void Page_Load(object sender, EventArgs e)
    {
        con = new SqlConnection(GetConnectionString());
        string url = HttpContext.Current.Request.Url.AbsoluteUri;
        Session["getavailable"] = url;
        if (Session["AccomId"] != "" && Session["AccomId"] != null)
        {
            PopulateModule(Convert.ToInt32(Session["AccomId"]));
            string AccomPolicyUrl = ViewState["ModuleDt"].ToString();
            linkTandCReg.Attributes["href"] = AccomPolicyUrl;
        }
        //if (Session["nullsession"] != null)
        //{
        //    Session["nullsession"] = null;
        //    Response.Redirect("searchproperty1.aspx");
        //}
        //if (chkterms.Checked)
        //{
        //    btnPayProceed.Visible = true;
        //}
        //else
        //{
        //    btnPayProceed.Visible = false;
        //}
        Session["get"] = null;
        if (Session["check"] == null)
        {
            Session["check"] = 1;
            Response.Redirect("searchproperty.aspx");
        }
        if (Session["CustName"] != null)
        {

            dvreg.Visible = false;
            pnllogin.Visible = false;
            custRegis.Visible = false;
            dvRefrence.Visible = false;
            pnlFullDetails.Visible = true;
            buttonUpdate.Visible = true;
            dvpnlDefault.Visible = false;
            lblUsername.Text = "Hello " + Session["CustName"].ToString();
            //if (Session["Getcheckindate"] != null)
            //{
            //    lblBalancedate.Text = "(75% of total) to be paid prior to " + Convert.ToDateTime(Session["Getcheckindate"].ToString()).AddDays(-90).ToString("dddd, MMMM d, yyyy");
            //}
            lnkCustomerRegis.Visible = false;
            navlogin.Visible = false;
            LinkButton1.Visible = true;
            //this.pnlFullDetails.Visible = false;
            pnlBookButton.Visible = true;
            //dvBilling.Visible = true;
            //dvpayment.Visible = true;
            //divspcl.Visible = true;
            dvreg.Visible = false;
            panelwithoutCreditAgent.Visible = true;
            //getdetail();
            loadall();
        }
        else if (Session["UserName"] != null)

        {
            dvreg.Visible = false;
            pnllogin.Visible = false;
            custRegis.Visible = false;
            dvRefrence.Visible = false;
            pnlFullDetails.Visible = true;
            buttonUpdate.Visible = true;
            dvpnlDefault.Visible = false;
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
        else
        {
            pnllogin.Visible = true;
            custRegis.Visible = true;
            dvRefrence.Visible = true;
            lnkCustomerRegis.Visible = true;
            navlogin.Visible = true;
            LinkButton1.Visible = false;
            this.pnlFullDetails.Visible = false;
            pnlBookButton.Visible = false;
            panelwithoutCreditAgent.Visible = false;
            //dvBilling.Visible = false;
            dvreg.Visible = false;
            dvreg.Visible = true;
            //dvpayment.Visible = false;
            //divspcl.Visible = false;
        }
        //if (Session["CustName"] != null)
        //{
        //    lblUsername.Text = "Hello " + Session["CustName"].ToString();
        //}
        //if (Session["UserName"] != null)

        //{
        //    lblUsername.Text = "Hello " + Session["UserName"].ToString();

        //}

        if (!IsPostBack)
        {
            //string s = "";
            //if (Session["get"] != null)
            //{
            //    s = Session["get"].ToString();
            //}
            //string path = Server.MapPath("~/images/aspnet_imagemap" + s + ".png");
            //FileInfo file = new FileInfo(path);
            //if (file.Exists)
            //{
            //    file.Delete();
            //}
            try
            {
                Session["PackageId"] = PackId = Request.QueryString["PackId"];
                PackageName = Request.QueryString["PackageName"];
                NoOfNights = Request.QueryString["NoOfNights"];
                CheckIndate = Request.QueryString["CheckIndate"];
                Session["Getcheckindate"] = Request.QueryString["CheckIndate"];
                DepartureId = Request.QueryString["DepartureId"];
                PackageDesc(PackId);
                Session["Redirection"] = "SummarizedDetails.aspx";
                //if (Session["UserCode"] != null || Session["CustomerCode"] != null)
                //{
                //    LinkButton1.Visible = true;
                //}
                //else
                //{
                //    LinkButton1.Visible = false;
                //}
                LoadCountries();
                this.LoadBookedRoomDetails();
                roomnosgrid();

                getpackagesearchresults(Request.QueryString["PackId"]);

                PackageDesc(Request.QueryString["PackId"]);
                if (Session["UserCode"] != null || Session["CustomerCode"] != null)
                {

                    //this.pnlLogin.Visible = false;
                    //this.pnlFullDetails.Visible = false;

                    if (Session["UserCode"] != null)
                    {
                        BookRef.Style.Remove("display");
                        //ReqBookRef.Enabled = true;
                    }
                    else
                    {
                        BookRef.Style.Add("display", "None");
                        //ReqBookRef.Enabled = false;
                    }
                }
                else
                {
                    //Response.Redirect("agentLogin.aspx");
                    BookRef.Style.Add("display", "None");
                    //ReqBookRef.Enabled = false;

                }
                if (Session["forimage"] != null)
                {

                }

                //this.pnlLogin.Visible = false;
                ////this.pnlFullDetails.Visible = false;
                //pnlBookButton.Visible = false;
                //panelwithoutCreditAgent.Visible = false;
                //pnlCustReg.Visible = false;
                customerLogin.Visible = false;
            }
            catch
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
    //    Response.Redirect("" + AccomPolicyUrl + "");


    //}
    private decimal getcommision(int accomtype, int accomname)
    {
        decimal commisssion = 0;
        DataTable dt = dlagentpayment.selectbyaccom(accomtype, accomname);
        if (dt != null && dt.Rows.Count > 0)
        {
            try
            {
                commisssion = Convert.ToDecimal(dt.Rows[0]["Commision"].ToString());
                commisssion = (Convert.ToDecimal(txtPaidAmt.Text) * commisssion) / 100;
            }
            catch { }
        }
        return commisssion;
    }
    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        Session.Clear();
        Session.Abandon();
        Session.RemoveAll();
        //System.Web.Security.FormsAuthentication.SignOut();
        LinkButton1.Visible = false;
        Response.Redirect("SearchProperty.aspx");
    }

    public void getpackagesearchresults(string packid)
    {
        #region GetSearch data
        try
        {
            blsrch.action = "GetResultBasedOnPackage";


            blsrch.PackageId = packid;
            blsrch.StartDate = Convert.ToDateTime(Request.QueryString["CheckinDate"]);
            blsrch.EndDate = Convert.ToDateTime(Request.QueryString["CheckinDate"]).AddDays(Convert.ToInt32(Request.QueryString["NoOfNights"]));
            DataTable dtres = dlsrch.GetResultBasedOnPackage(blsrch);
            //lblChkin.Text = "Check-in at " + dtres.Rows[0]["AccomName"].ToString();
            //lblChkout.Text = "Check-out at " + dtres.Rows[0]["AccomName"].ToString();
        }
        catch
        {

        }
        #endregion
    }

    public void PackageDesc(string PackId)
    {
        try
        {
            blsrch.action = "getPackagebyPackId";
            blsrch.PackageId = PackId;
            DataTable dtPackDesc = dlsrch.getPackageDescription(blsrch);
            if (dtPackDesc != null)
            {
                Session["Packagedesc"] = dtPackDesc.Rows[0]["PackageDescription"].ToString();
                lblPakagedescrip.Text = dtPackDesc.Rows[0]["PackageDescription"].ToString();
                Label4.Text = Session["GetcruiseCheckin"].ToString();
                Label5.Text = Session["GetcruiseCheckOUt"].ToString();
                Image1.ImageUrl = "/" + Session["forimage"].ToString();
                ////lblPackDesc.Text = dtPackDesc.Rows[0]["PackageDescription"].ToString();
                //lblChkin.Text = lblChkin.Text + "," + dtPackDesc.Rows[0]["BordingFrom"].ToString() + ": " + Convert.ToDateTime(Request.QueryString["CheckinDate"]).ToString("dddd, MMMM d, yyyy");
                //lblChkout.Text = lblChkout.Text + "," + dtPackDesc.Rows[0]["BoadingTo"].ToString() + ": " + Convert.ToDateTime(Request.QueryString["CheckinDate"]).AddDays(Convert.ToInt32(Request.QueryString["NoOfNights"])).ToString("dddd, MMMM d, yyyy");
            }
        }
        catch
        {
        }
    }
    public void calculateTotal1(DataTable dt)
    {
        try
        {
            double Totamt = 0;

            if (dt != null && dt.Rows.Count > 0)
            {
                for (int k = 0; k < dt.Rows.Count; k++)
                {
                    Totamt = Totamt + Convert.ToDouble(dt.Rows[k]["Totalprice"].ToString());
                }


                lblnetAmount.Text = "INR " + Totamt.ToString("##,0");

                //GridRoomPaxDetail.FooterRow.Cells[3].Text =;
                //GridRoomPaxDetail.FooterRow.Cells[6].Text = "<strong style='font-weight: bolder;float: left; color: Black;'>Total :</strong>" + " " + "<strong style='font-weight: bolder; color: Black;float: right;padding-right: 24%;'> INR" + " " + Totamt.ToString("##,0") + " </strong>" + "           " + " ";



            }
            else
            {

            }
        }

        catch
        {
        }
    }
    public void roomnosgrid()
    {
        try
        {

            dtrpax = SessionServices.RetrieveSession<DataTable>("BookedRooms");
            //DataTable dtrpax = Session["BookedRooms"] as DataTable;
            if (dtrpax != null)
            {

                GridRoomPaxDetail.DataSource = dtrpax;
                GridRoomPaxDetail.DataBind();
            }
            else
            {
                GridRoomPaxDetail.DataSource = null;
                GridRoomPaxDetail.DataBind();

            }

            calculateTotal1(dtrpax);
            GridRoomPaxDetail.FooterRow.Cells[0].Text = "<b>Total Cabins:</b>" + GridRoomPaxDetail.Rows.Count.ToString();
            GridRoomPaxDetail.FooterRow.Cells[3].Text = "<b>Total</b>";
            GridRoomPaxDetail.FooterRow.Cells[5].Text = "INR" + " " + Convert.ToDouble(dtrpax.Compute("SUM(Price)", string.Empty)).ToString();
        }
        catch (Exception e)
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

                        if (oncredit)
                        {
                            panelwithoutCreditAgent.Visible = false;
                            panelwithoutCreditAgent.Visible = true;
                            // btnPayProceed.Text = "Book";
                            // btnPayProceed.Text = "Proceed For Payment";

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
        else if (Session["CustomerCode"] != null)
        {
            try
            {
                DataTable dtCustomer = new DataTable();
                blcus.Email = Session["CustMailId"].ToString();

                if (Session["guest"] != null)
                {
                    blcus.action = "getforguest";
                    dtCustomer = dlcus.getforguest(blcus);
                }
                else
                {
                    blcus.Password = Session["userpass"].ToString();
                    Session["CustPassword"] = Session["userpass"].ToString();
                    blcus.action = "LoginCust";
                    dtCustomer = dlcus.checkDuplicateemail(blcus);
                }

                if (dtCustomer != null && dtCustomer.Rows.Count > 0)
                {
                    ViewState["Pass"] = txtCustPass.Text.Trim();

                    Session["CustomerMailId"] = Session["CustMailId"].ToString();
                    lblAgentName.Text = DataSecurityManager.Decrypt(dtCustomer.Rows[0]["FirstName"].ToString()) + " " + DataSecurityManager.Decrypt(dtCustomer.Rows[0]["LastName"].ToString());
                    lblBillingAddress.Text = dlcus.GetBillingAddress(dtCustomer.Rows[0]);
                    lbPaymentMethod.Text = dtCustomer.Rows[0]["PaymentMethod"].ToString();
                    hdnfPhoneNumber.Value = dtCustomer.Rows[0]["Telephone"].ToString();
                    Session["CustId"] = dtCustomer.Rows[0]["CustId"].ToString();
                    DataTable dtrpax = SessionServices.RetrieveSession<DataTable>("BookedRooms");
                    //DataTable dtrpax = Session["BookedRooms"] as DataTable;

                    string BookRef = dtCustomer.Rows[0]["FirstName"].ToString() + dtCustomer.Rows[0]["LastName"].ToString() + "X" + Convert.ToDouble(dtrpax.Compute("SUM(Pax)", string.Empty)).ToString() + "-" + "Direct Client";
                    ViewState["BookRef"] = BookRef;
                    lbPaymentMethod.Text = "Online";
                    pnlFullDetails.Visible = true;
                    panelwithoutCreditAgent.Visible = true;
                    pnlBookButton.Visible = true;
                    customerLogin.Visible = false;
                }
                else
                {

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        else
        {
            customerLogin.Visible = false;
            customerLogin.Visible = true;

        }
    }
    public void getdetail()
    {
        try
        {
            if (Session["userpass"] != null)
            {
                blcus.Password = Session["userpass"].ToString();
            }
            if (Session["CustMailId"] != null)
            {
                blcus.Email = Session["CustMailId"].ToString();
            }


            blcus.action = "LoginCust";
            DataTable dtCustomer = dlcus.checkDuplicateemail(blcus);
            if (dtCustomer != null)
            {
                if (dtCustomer.Rows.Count > 0)
                {
                    pnllogin.Visible = false;
                    custRegis.Visible = false;
                    dvRefrence.Visible = false;
                    buttonUpdate.Visible = true;

                    lblAgentName.Text = DataSecurityManager.Decrypt(dtCustomer.Rows[0]["FirstName"].ToString()) + " " + DataSecurityManager.Decrypt(dtCustomer.Rows[0]["LastName"].ToString());
                    lblBillingAddress.Text = dlcus.GetBillingAddress(dtCustomer.Rows[0]);
                    lbPaymentMethod.Text = DataSecurityManager.Decrypt(dtCustomer.Rows[0]["PaymentMethod"].ToString());
                    hdnfPhoneNumber.Value = DataSecurityManager.Decrypt(dtCustomer.Rows[0]["Telephone"].ToString());
                    Session["CustId"] = dtCustomer.Rows[0]["CustId"].ToString();
                    Session["CustomerCode"] = dtCustomer.Rows[0]["CustId"].ToString();
                    Session.Add("CustomerMailId", DataSecurityManager.Decrypt(dtCustomer.Rows[0]["Email"].ToString()));
                    Session.Add("CustPassword", DataSecurityManager.Decrypt(dtCustomer.Rows[0]["Password"].ToString()));
                    DataTable dtrpax = SessionServices.RetrieveSession<DataTable>("BookedRooms");
                    //DataTable dtrpax = Session["BookedRooms"] as DataTable;
                    //if (Session["Getcheckindate"] != null)
                    //{
                    //    lblBalancedate.Text = "(70% of total) to be paid prior to " + Convert.ToDateTime(Session["Getcheckindate"].ToString()).AddDays(-90).ToString("dddd, MMMM d, yyyy");
                    //}
                    string BookRef = DataSecurityManager.Decrypt(dtCustomer.Rows[0]["FirstName"].ToString()) + DataSecurityManager.Decrypt(dtCustomer.Rows[0]["LastName"].ToString()) + "X" + Convert.ToDouble(dtrpax.Compute("SUM(Pax)", string.Empty)).ToString() + "-" + "Direct Client";
                    ViewState["BookRef"] = BookRef;
                    lbPaymentMethod.Text = "Online";
                    pnlFullDetails.Visible = true;
                    panelwithoutCreditAgent.Visible = true;
                    pnlBookButton.Visible = true;
                    customerLogin.Visible = false;
                    //pnlCustReg.Visible = false;
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
        }
        catch (Exception exp)
        {
            throw exp;
        }
    }
    public void getdetail1()
    {
        try
        {
            //if (Session["userpass"] != null)
            //{
            //    blcus.Password = Session["userpass"].ToString();
            //}
            if (Session["CustMailId"] != null)
            {
                blcus.Email = Session["CustMailId"].ToString();
            }


            blcus.action = "getforguest";
            DataTable dtCustomer = dlcus.getforguest(blcus);
            if (dtCustomer != null)
            {
                if (dtCustomer.Rows.Count > 0)
                {
                    pnllogin.Visible = false;
                    custRegis.Visible = false;
                    dvRefrence.Visible = false;
                    buttonUpdate.Visible = true;

                    lblAgentName.Text = DataSecurityManager.Decrypt(dtCustomer.Rows[0]["FirstName"].ToString()) + " " + DataSecurityManager.Decrypt(dtCustomer.Rows[0]["LastName"].ToString());
                    lblBillingAddress.Text = dlcus.GetBillingAddress(dtCustomer.Rows[0]);
                    lbPaymentMethod.Text = DataSecurityManager.Decrypt(dtCustomer.Rows[0]["PaymentMethod"].ToString());
                    hdnfPhoneNumber.Value = DataSecurityManager.Decrypt(dtCustomer.Rows[0]["Telephone"].ToString());
                    Session["CustId"] = dtCustomer.Rows[0]["CustId"].ToString();
                    Session["CustomerCode"] = dtCustomer.Rows[0]["CustId"].ToString();
                    Session.Add("CustomerMailId", DataSecurityManager.Decrypt(dtCustomer.Rows[0]["Email"].ToString()));
                    Session.Add("CustPassword", DataSecurityManager.Decrypt(dtCustomer.Rows[0]["Password"].ToString()));
                    DataTable dtrpax = SessionServices.RetrieveSession<DataTable>("BookedRooms");
                    //DataTable dtrpax = Session["BookedRooms"] as DataTable;
                    //if (Session["Getcheckindate"] != null)
                    //{
                    //    lblBalancedate.Text = "(70% of total) to be paid prior to " + Convert.ToDateTime(Session["Getcheckindate"].ToString()).AddDays(-90).ToString("dddd, MMMM d, yyyy");
                    //}
                    string BookRef = DataSecurityManager.Decrypt(dtCustomer.Rows[0]["FirstName"].ToString()) + DataSecurityManager.Decrypt(dtCustomer.Rows[0]["LastName"].ToString()) + "X" + Convert.ToDouble(dtrpax.Compute("SUM(Pax)", string.Empty)).ToString() + "-" + "Direct Client";
                    ViewState["BookRef"] = BookRef;
                    lbPaymentMethod.Text = "Online";
                    pnlFullDetails.Visible = true;
                    panelwithoutCreditAgent.Visible = true;
                    pnlBookButton.Visible = true;
                    customerLogin.Visible = false;
                    //pnlCustReg.Visible = false;
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
        }
        catch (Exception exp)
        {
            throw exp;
        }
    }
    protected void btnCustLogin_Click(object sender, EventArgs e)
    {
        try
        {
            roomnosgrid();
            blcus.Email = txtCustMailId.Text.Trim();
            blcus.Password = txtCustPass.Text.Trim();
            blcus.action = "LoginCust";
            DataTable dtCustomer = dlcus.checkDuplicateemail(blcus);

            if (dtCustomer != null)
            {
                if (dtCustomer.Rows.Count > 0)
                {
                    pnllogin.Visible = false;
                    custRegis.Visible = false;
                    dvRefrence.Visible = false;
                    buttonUpdate.Visible = true;
                    Session["CustName"] = DataSecurityManager.Decrypt(dtCustomer.Rows[0]["FirstName"].ToString());
                    Session["CustomerCode"] = DataSecurityManager.Decrypt(dtCustomer.Rows[0]["CustId"].ToString());
                    ViewState["Pass"] = txtCustPass.Text.Trim();
                    Session["userpass"] = txtCustPass.Text.Trim();
                    Session["CustMailId"] = txtCustMailId.Text.Trim();
                    lblAgentName.Text = DataSecurityManager.Decrypt(dtCustomer.Rows[0]["FirstName"].ToString()) + " " + DataSecurityManager.Decrypt(dtCustomer.Rows[0]["LastName"].ToString());
                    lblBillingAddress.Text = dlcus.GetBillingAddress(dtCustomer.Rows[0]);
                    lbPaymentMethod.Text = DataSecurityManager.Decrypt(dtCustomer.Rows[0]["PaymentMethod"].ToString());
                    hdnfPhoneNumber.Value = DataSecurityManager.Decrypt(dtCustomer.Rows[0]["Telephone"].ToString());
                    Session["CustId"] = DataSecurityManager.Decrypt(dtCustomer.Rows[0]["CustId"].ToString());
                    Session["CustomerCode"] = DataSecurityManager.Decrypt(dtCustomer.Rows[0]["CustId"].ToString());
                    Session["CustomerMailId"] = DataSecurityManager.Decrypt(dtCustomer.Rows[0]["Email"].ToString());
                    Session["CustPassword"] = DataSecurityManager.Decrypt(dtCustomer.Rows[0]["Password"].ToString());
                    //Session.Add("CustomerMailId", DataSecurityManager.Decrypt(dtCustomer.Rows[0]["Email"].ToString()));
                    //Session.Add("CustPassword", DataSecurityManager.Decrypt(dtCustomer.Rows[0]["Password"].ToString()));
                    DataTable dtrpax = SessionServices.RetrieveSession<DataTable>("BookedRooms");
                    // DataTable dtrpax = Session["BookedRooms"] as DataTable;
                    //if (Session["Getcheckindate"] != null)
                    //{
                    //    lblBalancedate.Text = "(70% of total) to be paid prior to " + Convert.ToDateTime(Session["Getcheckindate"].ToString()).AddDays(-90).ToString("dddd, MMMM d, yyyy");
                    //}
                    string BookRef = DataSecurityManager.Decrypt(dtCustomer.Rows[0]["FirstName"].ToString()) + DataSecurityManager.Decrypt(dtCustomer.Rows[0]["LastName"].ToString()) + "X" + Convert.ToDouble(dtrpax.Compute("SUM(Pax)", string.Empty)).ToString() + "-" + "Direct Client";
                    ViewState["BookRef"] = BookRef;
                    lbPaymentMethod.Text = "Online";
                    pnlFullDetails.Visible = true;
                    panelwithoutCreditAgent.Visible = true;
                    pnlBookButton.Visible = true;
                    customerLogin.Visible = false;
                    pnllogin.Visible = false;
                    custRegis.Visible = false;
                    dvRefrence.Visible = false;
                    pnlFullDetails.Visible = true;
                    buttonUpdate.Visible = true;
                    lblUsername.Text = "Hello " + Session["CustName"].ToString();
                    lnkCustomerRegis.Visible = false;
                    navlogin.Visible = false;
                    LinkButton1.Visible = true;
                    dvpnlDefault.Visible = false;
                    //this.pnlFullDetails.Visible = false;
                    pnlBookButton.Visible = true;
                    panelwithoutCreditAgent.Visible = true;
                    //dvBilling.Visible = true;
                    //dvpayment.Visible = true;
                    //divspcl.Visible = true;
                    dvreg.Visible = false;
                    roomnosgrid();
                    //pnlCustReg.Visible = false;
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Showstatus", "javascript:alert('Please enter valid email or password')", true);
                }
            }

            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Showstatus", "javascript:alert('Please enter valid email or password')", true);
            }
        }
        catch (Exception exp)
        {
            throw exp;
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
            balcustomer.Email = mail;
            balcustomer.action = "chkDuplicate";
            DataTable dt = dalcustomer.checkDuplicateemail(balcustomer);
            if (dt != null && dt.Rows.Count > 0)
            {
                Session["Phonecheck"] = 1;
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Showstatus", "javascript:alert('You are a registered member, please sign in.')", true);
                string pass = DataSecurityManager.Decrypt(dt.Rows[0]["Password"].ToString());
                Session["AlreadyPassword"] = pass;
                sendMailForAlreadyGuestPassword();
                return;
            }
            Session["getemail"] = mail;
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
            //balcustomer.Email = mail;
            //balcustomer.action = "chkDuplicate";
            //DataTable dt = dalcustomer.checkDuplicateemail(balcustomer);
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

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        roomnosgrid();
        if (chkRegTerm.Checked == true)
        {

            //checkfield();
            ValidateEmail(txtMailAddress.Text);
            //checkphone(txtTelephone.Text);
            //checkpostpostcode(txtPostcode.Text);

            ViewState["CustPass"] = txtpassword.Text.Trim();
            Session["term"] = true;
            sendMail();
            roomnosgrid();
            //pnlCustReg.Visible = false;
            buttonUpdate.Visible = false;
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Showstatus", "javascript:alert('Please agree to the Terms and Condition and Booking Policy.')", true);
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
    private void RefreshGrid()
    {
        int AccomodationId = Convert.ToInt32(Session["AccomId"].ToString());
        AccomodationContactsMaster oAccomodationContactMaster;
        AccomContactDTO[] oAccomodationContactData;
        oAccomodationContactMaster = new AccomodationContactsMaster();
        oAccomodationContactData = oAccomodationContactMaster.GetAccomodationContacts(AccomodationId);
        if (oAccomodationContactData != null)
        {
            if (oAccomodationContactData.Length > 0)
            {
                Session["getemail"] = oAccomodationContactData[0].CCId;
            }
        }
        else
        {

        }

        oAccomodationContactMaster = null;
        oAccomodationContactData = null;
    }

    public void sendMailForGuestPassword()
    {
        if (Session["Phonecheck"] == null)
        {
            try
            {
                MailMessage mail = new MailMessage();
                //   SmtpClient SmtpServer = new SmtpClient("adventureresortscruises.in");
                SmtpClient SmtpServer = new SmtpClient(SmtpHost);
                // mail.From = new MailAddress("reservations@adventureresortscruises.in");
                mail.From = new MailAddress(SmtpUserId);
                mail.To.Add(txtEmailid1.Text.Trim());
                string ccEmail = ConfigurationManager.AppSettings["ccEmail"];
                mail.Subject = "Genarated Password";

                Random rnd = new Random();
                string Code = rnd.Next(10000, 99999).ToString();
                hfVCode.Value = Code;
                Session["Code"] = Code;
                StringBuilder sb = new StringBuilder();
                sb.Append("<div>");

                sb.Append(" <div>Your password is:" + Code + ". </div> <div><br/> </div><div>Do contact us if you have any issue at " + txtEmailid1.Text.Trim() + "</div><div><br/></div><div>Thanking you,</div><div><br/></div><div>Reservations Office</div> ");
                sb.Append("</div>");
                sb.Append("<img src='" + CompanyLogo + "' alt='Image'/><br /><div> " + CompanyName + "</div><div> " + CompanyAddress + " </div> <div> Phone: " + CompanyPhoneNo + " </div><div> Mobile: " + CompanyMobile + " </div><div><br/> </div> ");

                mail.IsBodyHtml = true;
                mail.Body = sb.ToString();

                SmtpServer.Port = 587;
                //SmtpServer.Credentials = new System.Net.NetworkCredential("reservations@adventureresortscruises.in", "Augurs@123");

                SmtpServer.Credentials = new System.Net.NetworkCredential(SmtpUserId, SmtpPassword);
                SmtpServer.EnableSsl = false;

                SmtpServer.Send(mail);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Showstatus", "javascript:alert('Thank you for registering.Please Check Your Email For Password.')", true);
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
        else
        {
            Session["Phonecheck"] = null;
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Showstatus", "javascript:alert('Please enter valid phone no')", true);
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
                SmtpClient SmtpServer = new SmtpClient(SmtpHost);
                // SmtpClient SmtpServer = new SmtpClient("adventureresortscruises.in");
                // mail.From = new MailAddress("reservations@adventureresortscruises.in");
                mail.From = new MailAddress(SmtpUserId);
                mail.To.Add(txtMailAddress.Text.Trim());
                string ccEmail = ConfigurationManager.AppSettings["ccEmail"];
                mail.Subject = "Mail Verification";

                Random rnd = new Random();
                string Code = rnd.Next(10000, 99999).ToString();
                hfVCode.Value = Code;

                StringBuilder sb = new StringBuilder();
                sb.Append("<div>");
                sb.Append("<div> Dear " + txtFirstName.Text + ",</div> <div><br/></div><div>Thanks for your registering with us.</div> <div><br/> </div><div>For security reasons we have added this step so that we verify the email address before any booking details is sent across.</div> <div><br/></div> ");
                sb.Append(" <div>To verify your email address please enter the code " + Code + " in the registration screen. </div> <div><br/> </div><div>Do contact us if you have any issue at " + Session["getemail"].ToString() + "</div><div><br/></div><div>Thanking you,</div><div><br/></div><div>Reservations Office</div> ");
                sb.Append("</div>");
                sb.Append("<img src='" + CompanyLogo + "' alt='Image'/><br /><div> " + CompanyName + "</div><div> " + CompanyAddress + " </div> <div> Phone: " + CompanyPhoneNo + " </div><div> Mobile: " + CompanyMobile + " </div><div><br/> </div> ");

                mail.IsBodyHtml = true;
                mail.Body = sb.ToString();

                SmtpServer.Port = 587;
                //SmtpServer.Credentials = new System.Net.NetworkCredential("reservations@adventureresortscruises.in", "Augurs@123");

                //  SmtpServer.Credentials = new System.Net.NetworkCredential("" + CompanyEmail + "", "Augurs@123");
                SmtpServer.Credentials = new System.Net.NetworkCredential(SmtpUserId, SmtpPassword);
                //  SmtpServer.Credentials = new System.Net.NetworkCredential("bookings@adventureresortscruises.com", "XSyBs^p5");

                SmtpServer.EnableSsl = false;

                SmtpServer.Send(mail);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Showstatus", "javascript:alert('Thank you for registering. To validate your email id we have sent a code on your email, please key in the same to validate it.')", true);
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
        else
        {
            Session["Phonecheck"] = null;
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Showstatus", "javascript:alert('Please enter valid phone no')", true);
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
                SmtpClient SmtpServer = new SmtpClient(SmtpHost);
                //mail.From = new MailAddress("reservations@adventureresortscruises.in");
                mail.From = new MailAddress(SmtpUserId);
                mail.To.Add(txtEmailid1.Text.Trim());
                string ccEmail = ConfigurationManager.AppSettings["ccEmail"];
                mail.Subject = "Mail Verification";

                Random rnd = new Random();
                string Code = rnd.Next(10000, 99999).ToString();
                hfVCode.Value = Code;

                StringBuilder sb = new StringBuilder();
                sb.Append("<div>");
                sb.Append("<div> Dear " + txtFirstname1.Text + ",</div> <div><br/></div><div>Thanks for your registering with us.</div> <div><br/> </div><div>For security reasons we have added this step so that we verify the email address before any booking details is sent across.</div> <div><br/></div> ");
                sb.Append(" <div>To verify your email address please enter the code " + Code + " in the registration screen. </div> <div><br/> </div><div>Do contact us if you have any issue at " + Session["getemail"].ToString() + "</div><div><br/></div><div>Thanking you,</div><div><br/></div><div>Reservations Office</div> ");
                sb.Append("</div>");
                // sb.Append("<img src='http://test1.adventureresortscruises.in/Cruise/booking/ARC_Logo.jpg.png' alt='Image'/><br /><div> Adventure Resorts & Cruises Pvt. Ltd.</div><div> B209, CR Park, New Delhi 110019 </div> <div> Phone: +91 - 011 - 41057370 / 1 / 2 </div><div> Mobile: +91 - 9599755353 </div><div><br/> </div> ");
                sb.Append("<img src='" + CompanyLogo + "' alt='Image'/><br /><div> " + CompanyName + "</div><div> " + CompanyAddress + " </div> <div> Phone: " + CompanyPhoneNo + " </div><div> Mobile: " + CompanyMobile + " </div><div><br/> </div> ");

                mail.IsBodyHtml = true;
                mail.Body = sb.ToString();

                SmtpServer.Port = 587;
                //SmtpServer.Credentials = new System.Net.NetworkCredential("reservations@adventureresortscruises.in", "Augurs@123");
                SmtpServer.Credentials = new System.Net.NetworkCredential(SmtpUserId, SmtpPassword);
                SmtpServer.EnableSsl = false;

                SmtpServer.Send(mail);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Showstatus", "javascript:alert('Please check your Mail for the Verification Code')", true);
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
            }
        }
        else
        {
            Session["refrence"] = null;
            Session["Phonecheck"] = null;
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Showstatus", "javascript:alert('Please enter valid phone no')", true);
            return;
        }
    }

    public void sendCancelledMail()
    {
        if (Session["Phonecheck"] == null)
        {
            try
            {
                MailMessage mail = new MailMessage();
                SmtpClient SmtpServer = new SmtpClient(SmtpHost);
                // mail.From = new MailAddress("reservations@adventureresortscruises.in");
                mail.From = new MailAddress(SmtpUserId);
                mail.To.Add(txtMailAddress.Text.Trim());
                string ccEmail = ConfigurationManager.AppSettings["ccEmail"];
                mail.Subject = "Mail Verification";

                Random rnd = new Random();
                string Code = rnd.Next(10000, 99999).ToString();
                hfVCode.Value = Code;

                StringBuilder sb = new StringBuilder();
                sb.Append("<div>");
                sb.Append("<div> Dear " + txtFirstName.Text + ",</div> <div><br/></div><div>Thanks for your registering with us.</div> <div><br/> </div><div>For security reasons we have added this step so that we verify the email address before any booking details is sent across.</div> <div><br/></div> ");
                sb.Append(" <div>To verify your email address please enter the code " + Code + " in the registration screen. </div> <div><br/> </div><div>Do contact us if you have any issue at " + Session["getemail"].ToString() + "</div><div><br/></div><div>Thanking you,</div><div><br/></div><div>Reservations Office</div> ");
                sb.Append("</div>");
                //  sb.Append("<img src='http://test1.adventureresortscruises.in/Cruise/booking/ARC_Logo.jpg.png' alt='Image'/><br /><div> Adventure Resorts & Cruises Pvt. Ltd.</div><div> B209, CR Park, New Delhi 110019 </div> <div> Phone: +91 - 011 - 41057370 / 1 / 2 </div><div> Mobile: +91 - 9599755353 </div><div><br/> </div> ");
                sb.Append("<img src='" + CompanyLogo + "' alt='Image'/><br /><div> " + CompanyName + "</div><div> " + CompanyAddress + " </div> <div> Phone: " + CompanyPhoneNo + " </div><div> Mobile: " + CompanyMobile + " </div><div><br/> </div> ");
                mail.IsBodyHtml = true;
                mail.Body = sb.ToString();

                SmtpServer.Port = 587;
                //SmtpServer.Credentials = new System.Net.NetworkCredential("reservations@adventureresortscruises.in", "Augurs@123");
                SmtpServer.Credentials = new System.Net.NetworkCredential(SmtpUserId, SmtpPassword);
                SmtpServer.EnableSsl = false;

                SmtpServer.Send(mail);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Showstatus", "javascript:alert('Thank you for registering. To validate your email id we have sent a code on your email, please key in the same to validate it.')", true);
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
        else
        {
            Session["Phonecheck"] = null;
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Showstatus", "javascript:alert('Please enter valid phone no')", true);
            return;
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
                ddlCountry1.DataSource = dtCountries;
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
    public double CalculateTotal(double discount, double Actualprice)

    {

        double total = 0;

        total = Actualprice - ((Actualprice * discount) / 100);



        return total;

    }
    private void LoadBookedRoomDetails()
    {
        try
        {
            dtGetBookedRooms = SessionServices.RetrieveSession<DataTable>("BookedRooms");
            //dtGetBookedRooms = Session["BookedRooms"] as DataTable;

            DataTable dtgroupedData = new DataTable();
            dtgroupedData.Columns.Add("categoryName");
            dtgroupedData.Columns.Add("Pax");
            dtgroupedData.Columns.Add("Price");
            dtgroupedData.Columns.Add("Currency");
            DataTable dtUniqueCategories = dtGetBookedRooms.DefaultView.ToTable(true, "categoryName");

            #region Adding distict Room categories
            foreach (DataRow dr1 in dtUniqueCategories.Rows)
            {
                string categoryName = dr1["categoryName"].ToString();
                DataRow dr2 = dtgroupedData.NewRow();
                dr2["categoryName"] = categoryName;
                dtgroupedData.Rows.Add(dr2);
            }
            #endregion

            #region calculating values
            decimal TotalPaybleAmt = 0;
            double gettax1 = 0;
            foreach (DataRow dr1 in dtgroupedData.Rows)
            {

                string category = dr1["categoryName"].ToString();
                DataView dv;
                dv = new DataView(dtGetBookedRooms, "categoryName='" + category + "'", "categoryName", DataViewRowState.CurrentRows);
                DataTable dtFiltered = dv.ToTable();
                int packs = 0;
                decimal price = 0;
                decimal discountamount = 0;
                foreach (DataRow dr3 in dtFiltered.Rows)
                {
                    packs = packs + Convert.ToInt32(dr3["Pax"].ToString());
                    price = price + Convert.ToDecimal(dr3["Totalprice"].ToString().Replace(",", ""));

                }
                dr1["Pax"] = packs.ToString();
                dr1["Price"] = price.ToString();
                dr1["Currency"] = dv.ToTable().Rows[0]["Currency"].ToString();

                double discount = 0;
                if (Session["getdiscountvalue"] != null)
                {
                    try
                    {
                        discount = Convert.ToDouble(Session["getdiscountvalue"].ToString());
                    }
                    catch { }
                }
                lblDiscountper.Text = Session["getdiscountvalue"].ToString() + "%";
                //lblDiscount.Text =;
                TotalPaybleAmt = TotalPaybleAmt + Convert.ToDecimal(price);
                //double gettotal = CalculateTotal(discount, Convert.ToDouble(TotalPaybleAmt));
                //TotalPaybleAmt = Convert.ToDecimal(gettotal);
                try
                {
                    gettax1 = Convert.ToDouble(Session["gettax"].ToString().Split('R')[1]);
                }
                catch
                {
                    gettax1 = Convert.ToDouble(Session["gettax"].ToString());
                }
            }
            double total = 0;
            double gettax = 0;
            double getalltotal = 0;
            double totyadiscount = 0;
            double grosstotal = 0;
            double taxableamt = 0;
            for (int i = 0; i < dtGetBookedRooms.Rows.Count; i++)
            {
                taxableamt = taxableamt + Convert.ToDouble(dtGetBookedRooms.Rows[i]["taxablepamt"].ToString());
                gettax = gettax + (Convert.ToDouble(dtGetBookedRooms.Rows[i]["Tax1"].ToString()));
                total = Convert.ToDouble(total) + (Convert.ToDouble(dtGetBookedRooms.Rows[i]["pricewithouttax"].ToString().Replace(",", "")) * Convert.ToDouble(dtGetBookedRooms.Rows[i]["Pax"].ToString()));
                getalltotal = Convert.ToDouble(getalltotal) + (Convert.ToDouble(dtGetBookedRooms.Rows[i]["CRPrice"].ToString().Split('R')[1].ToString().Replace(",", "")));
                totyadiscount = Convert.ToDouble(totyadiscount) + (Convert.ToDouble(dtGetBookedRooms.Rows[i]["Discountprice"].ToString().Replace(",", "")));
                grosstotal = Convert.ToDouble(grosstotal) + (Convert.ToDouble(dtGetBookedRooms.Rows[i]["Totalprice"].ToString().Replace(",", "")));
            }
            Session["getcruisetax"] = "INR" + ((Convert.ToDouble(dtGetBookedRooms.Rows[0]["pricewithouttax"].ToString()) * gettax1) / 100).ToString("##,0");
            lblTax.Text = "INR " + Convert.ToDouble(gettax.ToString()).ToString("##,0");
            Session["getcruiseinvoice"] = "INR " + Math.Round(gettax).ToString();
            hdnfTotalPaybleAmt.Value = TotalPaybleAmt.ToString();
            lbltaxin.Text = "INR " + taxableamt.ToString("##,0");
            //a = a.Replace(",", "");

            if (lblDiscountper.Text != "0%")
            {
                lblDiscount.Text = dtGetBookedRooms.Rows[0]["Currency"].ToString() + " " + totyadiscount.ToString("##,0");
            }
            else
            {
                //lblDiscount.Visible = false;
                //lblDiscountper.Visible = false;
                getdiscount.Visible = false;
            }
            lblalltotal.Text = dtGetBookedRooms.Rows[0]["Currency"].ToString() + " " + getalltotal.ToString("##,0");
            lbltotAmt.Text = dtGetBookedRooms.Rows[0]["Currency"].ToString() + " " + total.ToString("##,0");
            lblGross.Text = dtGetBookedRooms.Rows[0]["Currency"].ToString() + " " + Convert.ToDouble(grosstotal.ToString()).ToString("##,0");
            lblCurrency.Text = dtGetBookedRooms.Rows[0]["Currency"].ToString() + " ";
            //lblSpecialRequest.Text = specialrequest.InnerText;
            if (Convert.ToDateTime(Request.QueryString["CheckinDate"]).AddDays(-90) < System.DateTime.Now)
            {
                txtPaidAmt.Text = Convert.ToDouble(Math.Round(((100 * TotalPaybleAmt) / 100)).ToString("#.##")).ToString("##,0");
                //lblpertext.Text = "";
                //Priorto.Text = "<b>Due Date: </b>";
                //lblPrToDate.Text = "N/A";

                lbl25.Text = "";
                lblBalancedate.Text = "N/A";
                trbalancedate.Visible = false;

            }
            else
            {
                txtPaidAmt.Text = Convert.ToDouble(Math.Round(((25 * TotalPaybleAmt) / 100)).ToString("#.##")).ToString("##,0");
                if (Session["Getcheckindate"] != null)
                {
                    lblBalancedate.Text = "(75% of total) to be paid prior to " + Convert.ToDateTime(Session["Getcheckindate"].ToString()).AddDays(-90).ToString("dddd, MMMM d, yyyy");
                }
                lbl25.Text = "(25% of Total)";
                Session["get25"] = 1;
                Session["getPaid"] = txtPaidAmt.Text;
                //lblpertext.Text = "(25% of Total)";
                //Priorto.Text = "(75% of total) to be paid prior to";
                //lblPrToDate.Text = Convert.ToDateTime(Request.QueryString["CheckinDate"]).AddDays(-90).ToString("dddd, MMMM d, yyyy");
            }

            hftxtpaidamt.Value = Convert.ToDouble(txtPaidAmt.Text).ToString("N2").Replace(",", "");
            if (txtBilingAddress.Text != "")
            {
                lblBillingAddress.Text = txtBilingAddress.Text;
            }
            lblBalanceAmt.Text = dtGetBookedRooms.Rows[0]["Currency"].ToString() + " " + Convert.ToDouble(Math.Round((TotalPaybleAmt - Convert.ToDecimal(txtPaidAmt.Text))).ToString()).ToString("##,0");
            if (lblBalanceAmt.Text == "INR 0")
            {
                trbalanceamount.Visible = false;
            }
            Session["getbalanceSummerlizede"] = Math.Round((TotalPaybleAmt - Convert.ToDecimal(txtPaidAmt.Text))).ToString();
            //    txtmailied.Text = Session["AgentMailId"].ToString();
            dtGetBookedRooms = dtgroupedData;
            //GridSummerizeRoomDetails.DataSource = dtGetBookedRooms;
            //GridSummerizeRoomDetails.DataBind();
            #endregion
        }
        catch (Exception ex)
        {
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
            blcus.PaymentMethod = "Online";
            if (Session["term"] != null)
            {
                bool kj = Convert.ToBoolean(Session["term"].ToString());
                blcus.term = Convert.ToBoolean(Session["term"].ToString());
            }
            getQueryResponse = dlcus.AddCustomers(blcus);
            if (getQueryResponse > 0)
            {
                lblUsername.Text = "Hello " + Session["CustName"].ToString();
                navlogin.Visible = false;
                LinkButton1.Visible = true;
                dvpnlDefault.Visible = false;
                //dvBilling.Visible = true;
                //dvpayment.Visible = true;
                //divspcl.Visible = true;
                tableVerify.Visible = false;
                pnlFullDetails.Visible = true;
                dvreg.Visible = false;
                getdetail();
                roomnosgrid();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Showstatus", "javascript:alert('Your email id has been verified')", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Showstatus", "javascript:alert('This email id already registered')", true);
                return;
            }
        }
        catch
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Showstatus", "javascript:alert('Please check entries')", true);
            return;
        }
    }
    private string getinvoice()
    {

        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["ReservationConnectionString"].ConnectionString);
        con.Open();
        string sqlQuery = "select max(invoicesequence )as InvSeq from tblPayment";
        SqlDataAdapter adp = new SqlDataAdapter(sqlQuery, con);
        DataTable dtGetinvoiceno = new DataTable();
        adp.Fill(dtGetinvoiceno);
        string invo = "";
        if (dtGetinvoiceno.Rows.Count > 0)
        {
            invo = dtGetinvoiceno.Rows[0][0].ToString();
        }
        con.Close();
        if (invo == "")
        {
            invo = "1";
        }
        else
        {
            int invonus = Convert.ToInt32(invo) + 1;
            invo = Convert.ToInt32(invonus).ToString();
        }
        int invonu = Convert.ToInt32(invo);
        string catalies = "";
        //Random getrand = new Random();
        if (Session["categoryAlias"] != null)
        {
            //catalies = Session["categoryAlias"].ToString() + getrand.Next(10000, 90000);
            catalies = Session["categoryAlias"].ToString() + invonu;
        }
        else
        {
            //catalies = "MVM" + getrand.Next(10000, 90000);
            catalies = "MVM" + invonu;
        }
        return catalies + "!" + invonu;


    }
    public void ClientRegister1()
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
            Session["guest"] = txtEmailid1.Text.Trim();
            blcus.FirstName = txtFirstname1.Text;
            Session["CustName"] = txtFirstname1.Text;
            //Session["CustomerCode"] = dtCustomer.Rows[0]["CustId"].ToString();
            blcus.LastName = txtLastanme1.Text;
            blcus.PostalCode = txtPostCode1.Text;
            blcus.State = txtState1.Text;
            blcus.Telephone = txtMobilephone1.Text.Trim();
            string pass = GetPassword();

            blcus.Password = pass;
            Session["userpass"] = pass;

            blcus.Title = ddlList1.SelectedItem.Text;
            blcus.PaymentMethod = "Online";
            if (chkTerm.Checked)
            {
                blcus.term = true;
            }
            else
            {
                blcus.term = false;
            }
            getQueryResponse = dlcus.AddCustomers(blcus);
            if (getQueryResponse > 0)
            {
                lblUsername.Text = "Hello " + Session["CustName"].ToString();
                navlogin.Visible = false;
                LinkButton1.Visible = true;
                tableVerify.Visible = false;
                pnlFullDetails.Visible = true;
                dvpnlDefault.Visible = false;
                getdetail1();
                //dvBilling.Visible = true;
                dvreg.Visible = false;
                //roomnosgrid();
                //dvpayment.Visible = true;
                //divspcl.Visible = true;

            }
            roomnosgrid();
        }
        catch
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Showstatus", "javascript:alert('Please check entries')", true);
        }
    }
    private void SendEventEmail(int BookingId, string BookingStatus, bool bookingUpdated)
    {
        ENums.EventName eventName = GetEventName(BookingStatus, bookingUpdated);
        EventEmailServices eventEmailService = new EventEmailServices();
        eventEmailService.SendEventMail(BookingId, eventName);
        //Thread emailThread = new Thread(new ThreadStart(EventEmailManager.SendEventMail(BookingId, AccomodationId, EventName)));
        //emailThread.Priority = ThreadPriority.Highest;
        //emailThread.Start();
    }

    private ENums.EventName GetEventName(string BookingStatus, bool BookingUpdated)
    {
        ENums.EventName eventName = ENums.EventName.NONE;
        switch (BookingStatus)
        {
            case "booked":
            case "waitlisted":
            case "Proposed Booking":
                eventName = ENums.EventName.BOOKING;
                if (BookingUpdated)
                    eventName = ENums.EventName.BOOKINGUPDATED;
                break;
            case "confirmed":
                eventName = ENums.EventName.CONFIRMATION;
                if (BookingUpdated)
                    eventName = ENums.EventName.CONFIRMATIONUPDATED;
                break;
            case "cancelled":
            case "confirmation_cancelled":
                eventName = ENums.EventName.CANCELLED;
                break;
            case "deleted":
                eventName = ENums.EventName.DELETED;
                break;
        }
        return eventName;
    }

    protected void btnVerify_Click(object sender, EventArgs e)
    {
        try
        {
            if (txtCode.Text == hfVCode.Value)
            {
                if (Session["refrence"] != null)
                {
                    ClientRegister1();
                    Session["refrence"] = null;
                    txtCode.Text = "";
                }
                else
                {
                    ClientRegister();
                    txtCode.Text = "";

                }

                roomnosgrid();
                //TableCust.Visible = true;
            }
            else
            {
                pnlFullDetails.Visible = false;
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Showstatus", "javascript:alert('Wrong Code Entered!')", true);
                //TableCust.Visible = false;
                tableVerify.Visible = true;
                pnllogin.Visible = false;
                custRegis.Visible = false;
                dvRefrence.Visible = false;
                buttonUpdate.Visible = false;
            }
        }
        catch
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Showstatus", "javascript:alert('Wrong Code Entered!')", true);
        }
    }
    private string GetPassword()
    {

        var chars = "ABCDEF!GHIJKLMNOP@QRSTUVWXYZabc#defghijklm$nopqr&stuvwx*yz0123456789";
        var stringChars = new char[6];
        var random = new Random();
        for (int i = 0; i < stringChars.Length; i++)
        {
            stringChars[i] = chars[random.Next(chars.Length)];
        }
        var pass = new String(stringChars);
        return pass;

    }
    public void sendMailForAlreadyGuestPassword()
    {
        if (Session["Phonecheck"] == null)
        {
            try
            {
                string Code = Session["AlreadyPassword"].ToString();
                hfVCode.Value = Code;
                Session["Code"] = Code;
                MailMessage mail = new MailMessage();
                SmtpClient SmtpServer = new SmtpClient(SmtpHost);
                mail.From = new MailAddress(CompanyEmail);
                mail.To.Add(txtEmailid1.Text.Trim());
                string ccEmail = ConfigurationManager.AppSettings["ccEmail"];
                mail.Subject = "Genarated Password";
                StringBuilder sb = new StringBuilder();
                sb.Append("<div>");
                sb.Append(" <div>Your password is:" + Code + ". </div> <div><br/> </div><div>Do contact us if you have any issue at " + Session["getemail"] + "</div><div><br/></div><div>Thanking you,</div><div><br/></div><div>Reservations Office</div> ");
                sb.Append("</div>");
                sb.Append("<img src='" + CompanyLogo + "' alt='Image'/><br /><div> " + CompanyName + "</div><div> " + CompanyAddress + " </div> <div> Phone: " + CompanyPhoneNo + " </div><div> Mobile: " + CompanyMobile + " </div><div><br/> </div> ");

                mail.IsBodyHtml = true;
                mail.Body = sb.ToString();
                SmtpServer.Port = 587;
                SmtpServer.Credentials = new System.Net.NetworkCredential(SmtpUserId, SmtpPassword);
                SmtpServer.EnableSsl = false;
                SmtpServer.Send(mail);
                //ScriptManager.RegisterStartupScript(this, this.GetType(), "Showstatus", "javascript:alert('Thank you for registering.Please Check Your Email For Password.')", true);
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
        else
        {
            Session["Phonecheck"] = null;
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Showstatus", "javascript:alert('Please enter valid phone no')", true);
            return;
        }
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        roomnosgrid();
        if (chkTerm.Checked == true)
        {
            //if (txtFirstname1.Text == "" || txtLastanme1.Text == "" || txtEmailid1.Text == "" || txtMobilephone1.Text == "" || txtAddress11.Text == "" || txtCity1.Text == "" || txtState1.Text == "" || txtPostCode1.Text == "" || ddlCountry1.SelectedIndex == 0)
            //{
            //    Session["Phonecheck"] = 1;
            //    ScriptManager.RegisterStartupScript(this, this.GetType(), "Showstatus", "javascript:alert('Plaese enter valid data')", true);
            //    return;
            //}
            //checkfield1();
            string email = txtEmailid1.Text;
            Regex regex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
            Match match = regex.Match(email);
            if (match.Success)
            {
                balcustomer.Email = email;
                balcustomer.action = "chkDuplicate";
                DataTable dt = dalcustomer.checkDuplicateemail(balcustomer);
                if (dt != null && dt.Rows.Count > 0)
                {
                    string pass = DataSecurityManager.Decrypt(dt.Rows[0]["Password"].ToString());
                    Session["AlreadyPassword"] = pass;
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Showstatus", "javascript:alert('You are a registered member,an email send to with your password.')", true);
                    sendMailForAlreadyGuestPassword();

                    return;
                }
                else
                {
                    // sendMailForGuestPassword();
                   // pnlguestlogin.Visible = true;
                  //  ClientRegister1();
                }
            }

            else
            {
                //Session["Phonecheck"] = 1;
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Showstatus", "javascript:alert('Please enter valid email')", true);
                return;
            }
            //ValidateEmail1(txtEmailid1.Text);
            //if (txtMobilephone1.Text != null || txtMobilephone1.Text != "")
            //{
            //    try
            //    {
            //        long value = Convert.ToInt64(txtMobilephone1.Text);
            //        if (txtMobilephone1.Text.Length < 10)
            //        {
            //            //Session["Phonecheck"] = 1;
            //            ScriptManager.RegisterStartupScript(this, this.GetType(), "Showstatus", "javascript:alert('Please enter valid phone no')", true);
            //            return;
            //        }
            //    }
            //    catch
            //    {
            //        //Session["Phonecheck"] = 1;
            //        ScriptManager.RegisterStartupScript(this, this.GetType(), "Showstatus", "javascript:alert('Please enter valid phone no')", true);
            //        return;
            //    }
            //}
            //checkphone(txtMobilephone1.Text);
            //if (txtPostCode1.Text != null && txtPostCode1.Text != "")
            //{
            //    if (txtPostCode1.Text.Length < 6)
            //    {
            //        Session["Phonecheck"] = 1;
            //    }
            //    else
            //    {
            //        try
            //        {
            //            long post = Convert.ToInt64(txtPostCode1.Text);
            //        }
            //        catch
            //        {
            //            return;
            //        }
            //    }
            //}
            //checkpostpostcode(txtPostCode1.Text);
            Session["refrence"] = 1;
            ViewState["CustPass"] = txtpassword.Text.Trim();

            //sendMail1();
            //pnlCustReg.Visible = false;

            buttonUpdate.Visible = true;
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Showstatus", "javascript:alert('Please agree to the Terms and Condition and Booking Policy.')", true);
            return;
        }
    }
    protected void Button12_Click(object sender, EventArgs e)
    {
        roomnosgrid();
        if (chkTerm.Checked == true)
        {
            //if (txtFirstname1.Text == "" || txtLastanme1.Text == "" || txtEmailid1.Text == "" || txtMobilephone1.Text == "" || txtAddress11.Text == "" || txtCity1.Text == "" || txtState1.Text == "" || txtPostCode1.Text == "" || ddlCountry1.SelectedIndex == 0)
            //{
            //    Session["Phonecheck"] = 1;
            //    ScriptManager.RegisterStartupScript(this, this.GetType(), "Showstatus", "javascript:alert('Plaese enter valid data')", true);
            //    return;
            //}
            //checkfield1();
            string email = txtEmailid1.Text;
            Regex regex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
            Match match = regex.Match(email);
            if (match.Success)
            {
                balcustomer.Email = email;
                balcustomer.action = "chkDuplicate";
                DataTable dt = dalcustomer.checkDuplicateemail(balcustomer);
                if (dt != null && dt.Rows.Count > 0)
                {

                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Showstatus", "javascript:alert('You are a registered member, please sign in.')", true);
                    return;
                }
            }

            else
            {
                //Session["Phonecheck"] = 1;
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Showstatus", "javascript:alert('Please enter valid email')", true);
                return;
            }
            //ValidateEmail1(txtEmailid1.Text);
            //if (txtMobilephone1.Text != null || txtMobilephone1.Text != "")
            //{
            //    try
            //    {
            //        long value = Convert.ToInt64(txtMobilephone1.Text);
            //        if (txtMobilephone1.Text.Length < 10)
            //        {
            //            //Session["Phonecheck"] = 1;
            //            ScriptManager.RegisterStartupScript(this, this.GetType(), "Showstatus", "javascript:alert('Please enter valid phone no')", true);
            //            return;
            //        }
            //    }
            //    catch
            //    {
            //        //Session["Phonecheck"] = 1;
            //        ScriptManager.RegisterStartupScript(this, this.GetType(), "Showstatus", "javascript:alert('Please enter valid phone no')", true);
            //        return;
            //    }
            //}
            //checkphone(txtMobilephone1.Text);
            //if (txtPostCode1.Text != null && txtPostCode1.Text != "")
            //{
            //    if (txtPostCode1.Text.Length < 6)
            //    {
            //        Session["Phonecheck"] = 1;
            //    }
            //    else
            //    {
            //        try
            //        {
            //            long post = Convert.ToInt64(txtPostCode1.Text);
            //        }
            //        catch
            //        {
            //            return;
            //        }
            //    }
            //}
            //checkpostpostcode(txtPostCode1.Text);
            Session["refrence"] = 1;
            ViewState["CustPass"] = txtpassword.Text.Trim();
            sendMailForGuestPassword();
            //sendMail1();
            //pnlCustReg.Visible = false;
            ClientRegister1();
            buttonUpdate.Visible = true;
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Showstatus", "javascript:alert('Please agree to the Terms and Condition and Booking Policy.')", true);
            return;
        }
    }
    protected void buttonUpdate_Click(object sender, EventArgs e)

    {
        if (Session["CustMailId"] != null)
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
                    LoadBookedRoomDetails();
                    txtBilingAddress.Text = "";
                    txtcardnumber.Text = "";
                    txtNamenCard.Text = "";
                    specialrequest.Value = "";

                }

                catch (Exception ex)
                {

                }
            }
        }
    }
    private void InsertChildTableData(int bookingId)
    {
        BALBooking blsr = new BALBooking();
        DALBooking dlsr = new DALBooking();

        BALBooking booking = dlsr.GetBookingDetails(bookingId);

        if (booking != null)
        {
            DataTable GridRoomPaxDetail = SessionServices.RetrieveSession<DataTable>("BookedRooms");
            //DataTable GridRoomPaxDetail = Session["BookedRooms"] as DataTable;
            int LoopInsertStatus = 0;
            try
            {
                blsr._iBookingId = bookingId;
                for (int LoopCounter = 0; LoopCounter < GridRoomPaxDetail.Rows.Count; LoopCounter++)
                {
                    blsr._iAccomId = booking._iAccomId;
                    blsr._dtStartDate = booking._dtStartDate;
                    blsr._dtEndDate = booking._dtEndDate;
                    blsr._iPaxStaying = Convert.ToInt32(GridRoomPaxDetail.Rows[LoopCounter]["Pax"].ToString());

                    blsr._bConvertTo_Double_Twin = GridRoomPaxDetail.Rows[LoopCounter]["Convertable"].ToString() == "1" ? true : false;
                    blsr._cRoomStatus = "B";
                    blsr._sRoomNo = GridRoomPaxDetail.Rows[LoopCounter]["RoomNumber"].ToString();
                    blsr.action = "AddPriceDetailsToo";
                    blsr.roomcatid = Convert.ToInt32(GridRoomPaxDetail.Rows[LoopCounter]["RoomCategoryId"].ToString());
                    blsr.PaymentId = Session["BookingPayId"].ToString();
                    blsr._Amt = Convert.ToDecimal(GridRoomPaxDetail.Rows[LoopCounter]["Totalprice"].ToString().Replace(",", ""));
                    blsr.taxableamount = Convert.ToDecimal(GridRoomPaxDetail.Rows[LoopCounter]["taxablepamt"].ToString().Replace(",", ""));
                    blsr.taxamount = Convert.ToDecimal(GridRoomPaxDetail.Rows[LoopCounter]["Tax1"].ToString().Replace(",", ""));
                    blsr.taxpercentage = Convert.ToDecimal(GridRoomPaxDetail.Rows[LoopCounter]["Tax"].ToString().Replace(",", ""));
                    blsr.Discount = Convert.ToDecimal(GridRoomPaxDetail.Rows[LoopCounter]["Discount"].ToString().Replace("%", ""));
                    blsr.priceperperson = Convert.ToDecimal(GridRoomPaxDetail.Rows[LoopCounter]["pricewithouttax"].ToString().Replace(",", ""));
                    blsr.ToTal = Convert.ToDecimal(GridRoomPaxDetail.Rows[LoopCounter]["Total"].ToString().Replace(",", ""));
                    blsr.bedconfig = GridRoomPaxDetail.Rows[LoopCounter]["Bconfig"].ToString();
                    string[] inv = getinvoice().Split('!');

                    blsr.InvoiceNo = inv[0];
                    blsr.InvoiceSequence = Convert.ToInt32(inv[1]).ToString();

                    Session["invoiceno"] = blsr.InvoiceNo;
                    try
                    {
                        blsr.DiscountPrice = Convert.ToDecimal(GridRoomPaxDetail.Rows[LoopCounter]["Discountprice"].ToString().Replace(",", ""));
                    }
                    catch
                    {
                        blsr.DiscountPrice = 0;
                    }
                    int GetQueryResponse = dlsr.AddRoomBookingDetails(blsr);
                    if (GetQueryResponse > 0)
                    {
                        LoopInsertStatus++;
                    }
                }
            }
            catch
            {
                throw;
            }
        }
    }

    private int InsertParentTableData()
    {
        BALBooking blsr = new BALBooking();
        DALBooking dlsr = new DALBooking();

        try
        {
            if (Session["UserCode"] != null)
            {
                blsr._iAgentId = Convert.ToInt32(Session["UserCode"].ToString());

            }
            else
            {
                if (Session["CustId"] != null && Session["UserCode"] == null)
                {
                    blsr.CustomerId = Session["CustId"].ToString();
                    blsr._iAgentId = 247;
                }
                else
                {
                    blsr.CustomerId = "0";
                }
            }

            blsr.action = "GetDepartureDetails";
            blsr._iBookingId = 0;
            blsr.PackageId = Session["PackageId"].ToString();
            DataTable dtDepartureDetails = dlsr.GetDepartureDetails(blsr);

            DateTime startDate = Request.QueryString["CheckInDate"] != null ? Convert.ToDateTime(Request.QueryString["CheckInDate"].ToString()) : Convert.ToDateTime(Session["checkin"]);
            DataRow packageRow = null;
            foreach (DataRow row in dtDepartureDetails.Rows)
            {
                if (DateTime.Compare(Convert.ToDateTime(row["CheckInDate"]), startDate) == 0)
                {
                    packageRow = row;
                    break;
                }
            }

            if (packageRow == null)
                return -1;

            blsr._iPersons = GetPax();
            blsr._sBookingRef = Session["BookingRef"] == null ? string.Empty : Session["BookingRef"].ToString();
            blsr._dtStartDate = Convert.ToDateTime(packageRow["CheckInDate"]);
            blsr._dtEndDate = Convert.ToDateTime(packageRow["CheckOutDate"]);
            blsr._iAccomTypeId = Convert.ToInt32(packageRow["AccomTypeId"]);
            blsr._iAccomId = Convert.ToInt32(packageRow["AccomId"]);
            if (Session["CustId"] != "" && Session["CustId"] != null)
            {
                blsr.CustomerId = Session["CustId"].ToString();
            }
            blsr._iNights = Convert.ToInt32(packageRow["NoOfNights"]);
            blsr._BookingStatusId = (int)BookingStatusTypes.BOOKED; //This is a proposed booking and it will be marked as booked on the next page once the payment is received.
            blsr._SeriesId = 0;
            blsr._proposedBooking = true;
            blsr._chartered = false;
            // blsr.PackageId = Session["PackId"].ToString();
            blsr.PackageId = Session["PackageId"].ToString();
            if (Session["UserCode"] != null)
            {
                blsr.agentcommission = getcommision(Convert.ToInt32(packageRow["AccomTypeId"]), Convert.ToInt32(packageRow["AccomId"]));
            }
            //Session.Add("tblBookingBAL", blsr);

            SessionServices.SaveSession<BALBooking>("tblBookingBAL", blsr);
            //Session["tblBookingBAL"] = blsr;
            int iBRC = dlsr.GetBookingReferenceCount(blsr);

            if (iBRC > 0)
            {
                System.Web.UI.ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "AlertBox", "alert('The Booking Reference mentioned by you is not unique. Please enter a different reference number.');", true);
                return -1;
            }
            int bookingId = dlsr.AddParentBookingDetail(blsr);

            //var bookingDetails = dlsr.GetBookingDetails(bookingId);
            //if (bookingDetails != null)
            //{
            //    blsr.BookingCode = bookingDetails.BookingCode;
            //}
            blsr._iBookingId = bookingId;

            SessionServices.SaveSession<BALBooking>("tblBookingBAL", blsr);
            // Session["tblBookingBAL"] = blsr;
            return bookingId;
        }
        catch
        {
            throw;
        }
    }
    private void BookTheCruise()
    {
        int bookingId = InsertParentTableData();
        Session["bookingId"] = bookingId;
        InsertChildTableData(bookingId);
        bool check = Convert.ToBoolean(1);
        SendEventEmail(bookingId, "Propsed", check);
    }
    private bool IsBookingAvailable()
    {
        BALBookingLock bl = SessionServices.RetrieveSession<BALBookingLock>("BookingLock");
        //BALBookingLock bl = Session["BookingLock"] as BALBookingLock;
        //BALBookingLock bl = Session["BookingLock"] != null ? (BALBookingLock)Session["BookingLock"] : null;

        DALBookingLock dbl = new DALBookingLock();
        if (dbl.IsLocked(bl))
        {
            lblBookingLockFound.Visible = true;
            lblBookingLockFound.Text = "The room(s) you are trying to book are no longer available. Please click on the link below to choose the rooms again.";
            lnkBackToCruiseBooking.Visible = true;
            lnkBackToCruiseBooking.NavigateUrl = string.Format("CruiseBooking.aspx?PackId={0}&PackageName={1}&NoOfNights={2}&CheckIndate={3}&DepartureId={4}", PackId, PackageName, NoOfNights, CheckIndate, DepartureId);
            return false;
        }
        return true;
    }
    private void RedirectToPaymentGatewayResponse()
    {
        string ts = "TRANSACTIONSTATUS=200";
        string apt = "APTRANSACTIONID=1234";
        string msg = "MESSAGE=success";
        string tid = "TRANSACTIONID=100";
        string amt = "AMOUNT=104";
        string ash = "ap_SecureHash=abc";

        string qs = string.Format("~/Cruise/Booking/PaymentGatewayResponse.aspx?{0}&{1}&{2}&{3}&{4}&{5}", ts, apt, msg, tid, amt, ash);
        Response.Redirect(qs);
        //Response.Redirect("PaymentGatewayResponse.aspx?" + qs);
    }
    private int GetPax()
    {
        DataTable dtRoomBookingDetails = SessionServices.RetrieveSession<DataTable>("BookedRooms");
        //DataTable dtRoomBookingDetails = Session["BookedRooms"] as DataTable;
        return Convert.ToInt32(dtRoomBookingDetails.Compute("SUM(Pax)", string.Empty));
    }
    protected void lnkbtnAdd_Click(object sender, EventArgs e)
    {
        string RedirecturlAdd = "CruiseBooking.aspx?PackId=" + Request.QueryString["PackId"].ToString() + "&PackageName=" + Request.QueryString["PackName"].ToString() + "&NoOfNights=" + Request.QueryString["NoOfNights"].ToString() + "&CheckIndate=" + Request.QueryString["CheckIndate"].ToString() + "&CheckOutdate=" + Request.QueryString["CheckOutdate"].ToString() + "&Discount=" + Request.QueryString["Discount"].ToString() + "&DepartureId=" + Request.QueryString["DepartureId"].ToString();
        Response.Redirect(RedirecturlAdd);

        //"SummarizedDetails1.aspx?BookedId=" + BookedId + "&PackName=" + Request.QueryString["PackageName"].ToString() + "&NoOfNights=" + Request.QueryString["NoOfNights"].ToString() + "&CheckinDate=" + Request.QueryString["CheckInDate"].ToString() + "&CheckOutdate=" + Request.QueryString["CheckOutDate"].ToString() + "&Discount=" + Request.QueryString["Discount"].ToString() + "&PackId=" + Session["PackageId"].ToString() + "&DepartureId=" + Request.QueryString["DepartureId"].ToString();
    }
    protected void btnPayProceed_Click(object sender, EventArgs e)
    {
        //if (chkterms.Checked)
        //{
        try
        {
            Session["Hotel"] = null;
            if (btnPayProceed.Text == "Proceed For Payment")
            {
                #region Check For Locked Booking
                if (!IsBookingAvailable())
                {
                    return;
                }
                #endregion

                #region Proceed For Payment
                if (Session["UserCode"] != null)
                {
                    Session.Add("BookingRef", txtBookRef.Text);

                    #region Book Through Agent
                    blagentpayment._Action = "MailValidate";
                    blagentpayment._EmailId = Session["AgentMailId"].ToString();
                    blagentpayment._Password = Session["Password"].ToString();

                    DataTable dtAgentData = dlagentpayment.BindControls(blagentpayment);

                    if (dtAgentData.Rows.Count <= 0)
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Showstatus", "javascript:alert('Agent not registered!!!')", true);
                        return;
                    }

                    string BookingPayId = lbPaymentMethod.Text.Trim().Substring(0, 2) + DateTime.Now.ToString("MMddhhmmssfff");
                    Session["BookingPayId"] = BookingPayId;

                    #region Book The Tour as Proposed Booking
                    //If everything looks good then book a proposed booking and confirm that on the next screen
                    BookTheCruise();
                    #endregion

                    //aev@farhorizonindia.com [1:48:55 PM] Augurs Technologies Pvt. Ltd.: 12345
                    DataTable dtrpax = SessionServices.RetrieveSession<DataTable>("BookedRooms");
                    //DataTable dtrpax = Session["BookedRooms"] as DataTable;

                    string agentFirstName = DataSecurityManager.Decrypt(dtAgentData.Rows[0]["FirstName"].ToString());
                    string agentLastName = DataSecurityManager.Decrypt(dtAgentData.Rows[0]["LastName"].ToString());

                    string BRef = txtBookRef.Text.Trim().ToString() + "X" + Convert.ToDouble(dtrpax.Compute("SUM(Pax)", string.Empty)).ToString() + Session["UserName"] != null ? "-" + Session["UserName"].ToString() : string.Empty;
                    Session.Add("BookingRef", BRef);
                    Session["Paid"] = Convert.ToDouble(txtPaidAmt.Text.Trim() == "" ? "0" : txtPaidAmt.Text.Trim());

                    string Email = Session["AgentMailId"].ToString();
                    string PhoneNumber = hdnfPhoneNumber.Value.Trim().ToString();
                    string FirstName = agentFirstName;

                    string LastName = "XYZ";//agentLastName; //dtGetReturnedData.Rows[0]["LastName"].ToString();
                    string PaidAmt = hftxtpaidamt.Value.Trim().ToString();
                    string PaymentId = BookingPayId.ToString();
                    string BillingAddress = lblBillingAddress.Text.Trim().ToString();
                    //Session["BookingPayId"] = txtBookRef.Text.Trim();// BookingPayId;

                    string City = "Lucknow";
                    string State = "UP";
                    string PinCode = "226005";
                    string Country = "INDIA";


                    Session["Address"] = lblBillingAddress.Text.Trim().ToString(); ;
                    Session["InvName"] = FirstName + " " + LastName;
                    Session["SubInvName"] = FirstName;
                    try
                    {
                        if (Session["getbalanceSummerlizede"] != null)
                        {
                            if (Session["getbalanceSummerlizede"].ToString() != "0.00" || Session["getbalanceSummerlizede"].ToString() != "0")
                            {
                                int n = dlbook.adddueamount(Email, Convert.ToDecimal(Session["getbalanceSummerlizede"].ToString()), PaymentId);
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
                        // Response.Redirect("sendtoairpay.aspx?BookingPayId=" + PaymentId + "&EmailId=" + Email.ToString() + "&PhoneNumber=" + PhoneNumber.ToString() + "&FirstName=" + arr[0].ToString() + "&LastName=" + LastName.ToString() + "&PaidAmt=" + PaidAmt.ToString() + "&BillingAddress=" + BillingAddress.ToString());

                        Response.Redirect("sendtoairpay.aspx?BookingPayId=" + PaymentId + "&EmailId=" + Email.ToString() + "&PhoneNumber=" + PhoneNumber.ToString() + "&FirstName=" + arr[0].ToString() + "&LastName=" + LastName.ToString() + "&PaidAmt=" + PaidAmt.ToString() + "&BillingAddress=" + BillingAddress.ToString() + "&City=" + City.ToString() + "&State=" + State.ToString() + "&PinCode=" + PinCode.ToString() + "&Country=" + Country.ToString());
                    }
                    #endregion
                }
                else
                {
                    DataTable dtrpax = SessionServices.RetrieveSession<DataTable>("BookedRooms");
                    // DataTable dtrpax = Session["BookedRooms"] as DataTable;
                    DataTable dtCustomerData = new DataTable();
                    #region Book Through Customer
                    blcus.Email = Session["CustomerMailId"].ToString();
                    try
                    {
                        blcus.Password = Session["CustPassword"].ToString();
                    }
                    catch { }
                    if (Session["guest"] != null)
                    {
                        blcus.action = "getforguest";
                        dtCustomerData = dlcus.getforguest(blcus);
                    }
                    else
                    {
                        blcus.action = "LoginCust";
                        dtCustomerData = dlcus.checkDuplicateemail(blcus);
                    }

                    //DataTable dtCustomerData = dlcus.checkDuplicateemail(blcus);

                    if (dtCustomerData.Rows.Count <= 0)
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Showstatus", "javascript:alert('Customer not registered!!!')", true);
                        return;
                    }

                    int persons = GetPax();
                    string bookingRef = string.Format("{0} {1} X {2}", DataSecurityManager.Decrypt(dtCustomerData.Rows[0]["FirstName"].ToString()), DataSecurityManager.Decrypt(dtCustomerData.Rows[0]["LastName"].ToString()), persons.ToString());
                    if (Session["BookingRef"] == null)
                        Session.Add("BookingRef", bookingRef);
                    else
                        Session["BookingRef"] = bookingRef;

                    Random rnd = new Random();
                    string BookingPayId = rnd.Next(10000, 20000).ToString() + DateTime.Now.ToString("MMddhhmmssfff");
                    Session["BookingPayId"] = BookingPayId;

                    #region Book The Tour as Proposed Booking
                    //If everything looks good then book a proposed booking and confirm that on the next screen
                    BookTheCruise();
                    #endregion

                    Session["Paid"] = Convert.ToDouble(txtPaidAmt.Text.Trim() == "" ? "0" : txtPaidAmt.Text.Trim());

                    string Email = Session["CustomerMailId"].ToString();
                    string PhoneNumber = DataSecurityManager.Decrypt(dtCustomerData.Rows[0]["Telephone"].ToString());
                    string FirstName = DataSecurityManager.Decrypt(dtCustomerData.Rows[0]["FirstName"].ToString());
                    string LastName = DataSecurityManager.Decrypt(dtCustomerData.Rows[0]["LastName"].ToString());
                    string City = DataSecurityManager.Decrypt(dtCustomerData.Rows[0]["City"].ToString());
                    string State = DataSecurityManager.Decrypt(dtCustomerData.Rows[0]["State"].ToString());
                    string PinCode = DataSecurityManager.Decrypt(dtCustomerData.Rows[0]["PostalCode"].ToString());
                    string Country = dtCustomerData.Rows[0]["CountryName"].ToString();

                    string PaidAmt = hftxtpaidamt.Value.Trim().ToString();
                    string PaymentId = BookingPayId.ToString();
                    string BillingAddress = lblBillingAddress.Text.Trim().ToString();

                    Session["Address"] = lblBillingAddress.Text.Trim().ToString();
                    Session["InvName"] = DataSecurityManager.Decrypt(dtCustomerData.Rows[0]["Title"].ToString()) + ". " + DataSecurityManager.Decrypt(dtCustomerData.Rows[0]["FirstName"].ToString()) + "" + " " + DataSecurityManager.Decrypt(dtCustomerData.Rows[0]["LastName"].ToString());

                    Session["SubInvName"] = DataSecurityManager.Decrypt(dtCustomerData.Rows[0]["LastName"].ToString()) + ", " + DataSecurityManager.Decrypt(dtCustomerData.Rows[0]["Title"].ToString()) + " " + FirstName;
                    try
                    {
                        if (Session["getbalanceSummerlizede"] != null)
                        {
                            if (Session["getbalanceSummerlizede"].ToString() != "0.00" || Session["getbalanceSummerlizede"].ToString() != "0")
                            {
                                int n = dlbook.adddueamount(Email, Convert.ToDecimal(Session["getbalanceSummerlizede"].ToString()), PaymentId);
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
                        Response.Redirect("sendtoairpay.aspx?BookingPayId=" + PaymentId + "&EmailId=" + Email.ToString() + "&PhoneNumber=" + PhoneNumber.ToString() + "&FirstName=" + FirstName.ToString() + "&LastName=" + LastName.ToString() + "&PaidAmt=" + PaidAmt.ToString() + "&BillingAddress=" + BillingAddress.ToString() + "&City=" + City.ToString() + "&State=" + State.ToString() + "&PinCode=" + PinCode.ToString() + "&Country=" + Country.ToString());
                        //Response.Redirect("sendtoairpay.aspx?BookingPayId=" + PaymentId + "&EmailId=" + Email.ToString() + "&PhoneNumber=" + PhoneNumber.ToString() + "&FirstName=" + FirstName.ToString() + "&LastName=" + LastName.ToString() + "&PaidAmt=" + PaidAmt.ToString() + "&BillingAddress=" + BillingAddress.ToString());
                        //  Response.Redirect("~/Cruise/booking/sendtoairpay.aspx?BookingPayId=" + PaymentId + "&EmailId=" + Email.ToString() + "&PhoneNumber=" + PhoneNumber.ToString() + "&FirstName=" + FirstName.ToString() + "&LastName=" + LastName.ToString() + "&PaidAmt=" + PaidAmt.ToString() + "&BillingAddress=" + BillingAddress.ToString());
                        //http://localhost:1897/ResortManager/Cruise/booking/SummerisedDetails.aspx?BookedId=0&PackName=Ganges+Exclusive&NoOfNights=5&CheckinDate=5%2f1%2f2016                    
                    }
                    #endregion
                }
                #endregion
            }
            else
            {
                if (Convert.ToDecimal(txtPaidAmt.Text) <= Convert.ToDecimal(hdnfCreditLimit.Value))
                {
                    DataTable dtrpax = SessionServices.RetrieveSession<DataTable>("BookedRooms");
                    // DataTable dtrpax = Session["BookedRooms"] as DataTable;


                    blagentpayment._Action = "MailValidate";
                    blagentpayment._EmailId = Session["AgentMailId"].ToString();
                    blagentpayment._Password = Session["Password"].ToString();

                    DataTable dtAgentData = dlagentpayment.BindControls(blagentpayment);

                    string agentFirstName = DataSecurityManager.Decrypt(dtAgentData.Rows[0]["FirstName"].ToString());
                    string agentLastName = DataSecurityManager.Decrypt(dtAgentData.Rows[0]["LastName"].ToString());
                    string FirstName = agentLastName;

                    string BookingPayId = lbPaymentMethod.Text.Trim().Substring(0, 2) + DateTime.Now.ToString("MMddhhmmssfff");
                    Session["BookingPayId"] = BookingPayId;
                    Session.Add("BookingRef", txtBookRef.Text.Trim().ToString());
                    Session["Paid"] = Convert.ToDouble(hftxtpaidamt.Value.Trim() == "" ? "0" : hftxtpaidamt.Value.Trim());
                    Session["InvName"] = DataSecurityManager.Decrypt(Session["UserName"].ToString());

                    Session["SubInvName"] = FirstName;

                    Session["Address"] = null;
                    Response.Redirect("PaymentGatewayResponse.aspx");
                }
                else
                {
                    lblPaymentErr.Text = "Payment Amount Exceeding Credit Limit";
                }
            }
        }
        catch (Exception exp)
        {
            throw exp;
        }
        //}
        //else
        //{

        //    ScriptManager.RegisterStartupScript(this, this.GetType(), "Showstatus", "javascript:alert('Please accept terms and condition')", true);
        //    return;
        //}
    }

    protected void btnCustLogin_Click1(object sender, EventArgs e)
    {
        Response.Redirect("../Booking/ForgotPassword.aspx");
    }







    protected void Button3_Click(object sender, EventArgs e)
    {

    }

    protected void chkterms_CheckedChanged(object sender, EventArgs e)
    {
        //if (chkterms.Checked)
        //{
        //    btnPayProceed.Visible = true;
        //}
        //else
        //{
        //    btnPayProceed.Visible = false;
        //}
    }


   

}