using FarHorizon.DataSecurity;
using FarHorizon.Reservations.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Net.Mail;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Cruise_booking_SummarizedDetails : System.Web.UI.Page
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

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            try
            {
                Session["PackageId"] = PackId = Request.QueryString["PackId"];
                PackageName = Request.QueryString["PackageName"];
                NoOfNights = Request.QueryString["NoOfNights"];
                CheckIndate = Request.QueryString["CheckIndate"];
                DepartureId = Request.QueryString["DepartureId"];

                Session["Redirection"] = "SummarizedDetails.aspx";
                if (Session["UserCode"] != null || Session["CustomerCode"] != null)
                {
                    LinkButton1.Visible = true;
                }
                else
                {
                    LinkButton1.Visible = false;
                }
                LoadCountries();
                this.LoadBookedRoomDetails();
                roomnosgrid();

                getpackagesearchresults(Request.QueryString["PackId"]);

                PackageDesc(Request.QueryString["PackId"]);
                if (Session["UserCode"] != null || Session["CustomerCode"] != null)
                {

                    this.pnlLogin.Visible = false;
                    this.pnlFullDetails.Visible = false;

                    if (Session["UserCode"] != null)
                    {
                        BookRef.Style.Remove("display");
                        ReqBookRef.Enabled = true;
                    }
                    else
                    {
                        BookRef.Style.Add("display", "None");
                        ReqBookRef.Enabled = false;
                    }
                }
                else
                {
                    //Response.Redirect("agentLogin.aspx");
                    BookRef.Style.Add("display", "None");
                    ReqBookRef.Enabled = false;

                }

                this.pnlLogin.Visible = false;
                this.pnlFullDetails.Visible = false;
                pnlBookButton.Visible = false;
                panelwithoutCreditAgent.Visible = false;
                pnlCustReg.Visible = false;
                customerLogin.Visible = false;
            }
            catch
            {
            }

        }
    }

    public void roomnosgrid()
    {
        try
        {

            DataTable dtrpax = Session["BookedRooms"] as DataTable;
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

            GridRoomPaxDetail.FooterRow.Cells[0].Text = "<b>Total Cabins:</b>" + GridRoomPaxDetail.Rows.Count.ToString();
            GridRoomPaxDetail.FooterRow.Cells[3].Text = "<b>Total</b>";
            GridRoomPaxDetail.FooterRow.Cells[5].Text = "INR" + " " + Convert.ToDouble(dtrpax.Compute("SUM(Price)", string.Empty)).ToString();
        }
        catch
        {

        }
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
            lblChkin.Text = "Check-in at " + dtres.Rows[0]["AccomName"].ToString();
            lblChkout.Text = "Check-out at " + dtres.Rows[0]["AccomName"].ToString();
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
                lblDesc.Text = dtPackDesc.Rows[0]["PackageDescription"].ToString();

                //lblPackDesc.Text = dtPackDesc.Rows[0]["PackageDescription"].ToString();
                lblChkin.Text = lblChkin.Text + "," + dtPackDesc.Rows[0]["BordingFrom"].ToString() + ": " + Convert.ToDateTime(Request.QueryString["CheckinDate"]).ToString("dddd, MMMM d, yyyy");
                lblChkout.Text = lblChkout.Text + "," + dtPackDesc.Rows[0]["BoadingTo"].ToString() + ": " + Convert.ToDateTime(Request.QueryString["CheckinDate"]).AddDays(Convert.ToInt32(Request.QueryString["NoOfNights"])).ToString("dddd, MMMM d, yyyy");
            }
        }
        catch
        {
        }
    }

    #region UDF
    private void LoadBookedRoomDetails()
    {
        try
        {
            dtGetBookedRooms = Session["BookedRooms"] as DataTable;
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
            foreach (DataRow dr1 in dtgroupedData.Rows)
            {
                string category = dr1["categoryName"].ToString();
                DataView dv;
                dv = new DataView(dtGetBookedRooms, "categoryName='" + category + "'", "categoryName", DataViewRowState.CurrentRows);
                DataTable dtFiltered = dv.ToTable();
                int packs = 0;
                decimal price = 0;
                foreach (DataRow dr3 in dtFiltered.Rows)
                {
                    packs = packs + Convert.ToInt32(dr3["Pax"].ToString());
                    price = price + Convert.ToDecimal(dr3["Price"].ToString());
                }
                dr1["Pax"] = packs.ToString();
                dr1["Price"] = price.ToString();
                dr1["Currency"] = dv.ToTable().Rows[0]["Currency"].ToString();
                TotalPaybleAmt = TotalPaybleAmt + Convert.ToDecimal(price);
            }
            hdnfTotalPaybleAmt.Value = TotalPaybleAmt.ToString();
            lbltotAmt.Text = dtGetBookedRooms.Rows[0]["Currency"].ToString() + " " + TotalPaybleAmt.ToString();
            lblCurrency.Text = dtGetBookedRooms.Rows[0]["Currency"].ToString() + " ";

            if (Convert.ToDateTime(Request.QueryString["CheckinDate"]).AddDays(-90) < System.DateTime.Now)
            {
                txtPaidAmt.Text = Math.Round(((100 * TotalPaybleAmt) / 100)).ToString("#.##");
                lblpertext.Text = "";
                Priorto.Text = "<b>Due Date: </b>";
                lblPrToDate.Text = "N/A";
            }
            else
            {
                txtPaidAmt.Text = Math.Round(((25 * TotalPaybleAmt) / 100)).ToString("#.##");
                lblpertext.Text = "(25% of Total)";
                Priorto.Text = "(75% of total) to be paid prior to";
                lblPrToDate.Text = Convert.ToDateTime(Request.QueryString["CheckinDate"]).AddDays(-90).ToString("dddd, MMMM d, yyyy");
            }



            hftxtpaidamt.Value = Convert.ToDouble(txtPaidAmt.Text).ToString("N2").Replace(",", "");

            lblBalanceAmt.Text = dtGetBookedRooms.Rows[0]["Currency"].ToString() + " " + Math.Round((TotalPaybleAmt - Convert.ToDecimal(txtPaidAmt.Text))).ToString();
            //    txtmailied.Text = Session["AgentMailId"].ToString();
            dtGetBookedRooms = dtgroupedData;
            GridSummerizeRoomDetails.DataSource = dtGetBookedRooms;
            GridSummerizeRoomDetails.DataBind();
            #endregion
        }
        catch
        {
        }
    }
    #endregion

    protected void btnSmbt_Click(object sender, EventArgs e)
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

                        pnlFullDetails.Visible = true;
                        pnlBookButton.Visible = true;

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
        else if (Session["CustomerCode"] != null)
        {
            try
            {
                blcus.Email = Session["CustomerMailId"].ToString();
                blcus.Password = Session["CustPassword"].ToString();
                blcus.action = "LoginCust";
                DataTable dtCustomer = dlcus.checkDuplicateemail(blcus);
                if (dtCustomer != null)
                {
                    ViewState["Pass"] = txtCustPass.Text.Trim();

                    Session["CustMailId"] = Session["CustomerMailId"].ToString();
                    lblAgentName.Text = dtCustomer.Rows[0]["FirstName"].ToString() + " " + dtCustomer.Rows[0]["LastName"].ToString();
                    lblBillingAddress.Text = dlcus.GetBillingAddress(dtCustomer.Rows[0]);
                    lbPaymentMethod.Text = dtCustomer.Rows[0]["PaymentMethod"].ToString();
                    hdnfPhoneNumber.Value = dtCustomer.Rows[0]["Telephone"].ToString();
                    Session["CustId"] = dtCustomer.Rows[0]["CustId"].ToString();
                    DataTable dtrpax = Session["BookedRooms"] as DataTable;

                    string BookRef = dtCustomer.Rows[0]["FirstName"].ToString() + dtCustomer.Rows[0]["LastName"].ToString() + "X" + Convert.ToDouble(dtrpax.Compute("SUM(Pax)", string.Empty)).ToString() + "-" + "Direct Client";
                    ViewState["BookRef"] = BookRef;
                    lbPaymentMethod.Text = "Online";
                    pnlFullDetails.Visible = true;
                    panelwithoutCreditAgent.Visible = true;
                    pnlBookButton.Visible = true;
                    customerLogin.Visible = false;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        else
        {
            pnlLogin.Visible = false;
            customerLogin.Visible = true;

        }
    }

    protected void btnLogin_Click(object sender, EventArgs e)
    {
        this.pnlFullDetails.Visible = true;
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        try
        {
            blagentpayment._Action = "MailValidate";
            blagentpayment._EmailId = Session["AgentMailId"].ToString();
            blagentpayment._Password = Session["Password"].ToString();
            DataTable dtAgent = dlagentpayment.BindControls(blagentpayment);
            if (dtAgent.Rows.Count > 0)
            {
                lblBillingAddress.Text = dtAgent.Rows[0]["BillingAddress"].ToString();
                lbPaymentMethod.Text = dtAgent.Rows[0]["PaymentMethod"].ToString();
                hdnfPhoneNumber.Value = dtAgent.Rows[0]["Phone"].ToString();
                pnlFullDetails.Visible = true;
            }
        }
        catch (Exception sqe)
        {

        }
    }

    protected void btnPayProceed_Click(object sender, EventArgs e)
    {
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
                    DataTable dtrpax = Session["BookedRooms"] as DataTable;

                    string agentFirstName = DataSecurityManager.Decrypt(dtAgentData.Rows[0]["FirstName"].ToString());
                    string agentLastName = DataSecurityManager.Decrypt(dtAgentData.Rows[0]["LastName"].ToString());

                    string BRef = txtBookRef.Text.Trim().ToString() + "X" + Convert.ToDouble(dtrpax.Compute("SUM(Pax)", string.Empty)).ToString() + Session["UserName"] != null ? "-" + Session["UserName"].ToString() : string.Empty;
                    Session.Add("BookingRef", BRef);
                    Session["Paid"] = Convert.ToDouble(txtPaidAmt.Text.Trim() == "" ? "0" : txtPaidAmt.Text.Trim());

                    string Email = Session["AgentMailId"].ToString();
                    string PhoneNumber = "9999999999";// hdnfPhoneNumber.Value.Trim().ToString();
                    string FirstName = agentFirstName;

                    string LastName = agentLastName; //dtGetReturnedData.Rows[0]["LastName"].ToString();
                    string PaidAmt = hftxtpaidamt.Value.Trim().ToString();
                    string PaymentId = BookingPayId.ToString();
                    string BillingAddress = "abc/wsdd,vasant vihar";// lblBillingAddress.Text.Trim().ToString();
                                                                    //Session["BookingPayId"] = txtBookRef.Text.Trim();// BookingPayId;

                    Session["Address"] = lblBillingAddress.Text.Trim().ToString(); ;
                    Session["InvName"] = FirstName;
                    Session["SubInvName"] = FirstName;

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
                        Response.Redirect("~/Cruise/booking/sendtoairpay.aspx?BookingPayId=" + PaymentId + "&EmailId=" + Email.ToString() + "&PhoneNumber=" + PhoneNumber.ToString() + "&FirstName=" + arr[0].ToString() + "&LastName=" + LastName.ToString() + "&PaidAmt=" + PaidAmt.ToString() + "&BillingAddress=" + BillingAddress.ToString());
                    }
                    #endregion
                }
                else
                {
                    #region Book Through Customer
                    blcus.Email = Session["CustomerMailId"].ToString();
                    blcus.Password = Session["CustPassword"].ToString();

                    blcus.action = "LoginCust";
                    var dtCustomerData = dlcus.checkDuplicateemail(blcus);

                    if (dtCustomerData.Rows.Count <= 0)
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Showstatus", "javascript:alert('Customer not registered!!!')", true);
                        return;
                    }

                    int persons = GetPax();
                    string bookingRef = string.Format("{0} {1} X {2}", dtCustomerData.Rows[0]["FirstName"], dtCustomerData.Rows[0]["LastName"], persons.ToString());
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
                    string PhoneNumber = "9999999999";// hdnfPhoneNumber.Value.Trim().ToString();
                    string FirstName = dtCustomerData.Rows[0]["FirstName"].ToString();
                    string LastName = "XYZ";// dtGetReturnedData.Rows[0]["LastName"].ToString();

                    string PaidAmt = hftxtpaidamt.Value.Trim().ToString();
                    string PaymentId = BookingPayId.ToString();
                    string BillingAddress = "abc/wsdd,vasant vihar";// lblBillingAddress.Text.Trim().ToString();

                    Session["Address"] = lblBillingAddress.Text.Trim().ToString();
                    Session["InvName"] = dtCustomerData.Rows[0]["Title"].ToString() + " " + " " + dtCustomerData.Rows[0]["LastName"].ToString();

                    Session["SubInvName"] = dtCustomerData.Rows[0]["LastName"].ToString() + ", " + dtCustomerData.Rows[0]["Title"].ToString() + " " + FirstName;

                    if (Debugger.IsAttached)
                    {
                        RedirectToPaymentGatewayResponse();
                    }
                    else
                    {
                        Response.Redirect("~/Cruise/booking/sendtoairpay.aspx?BookingPayId=" + PaymentId + "&EmailId=" + Email.ToString() + "&PhoneNumber=" + PhoneNumber.ToString() + "&FirstName=" + FirstName.ToString() + "&LastName=" + LastName.ToString() + "&PaidAmt=" + PaidAmt.ToString() + "&BillingAddress=" + BillingAddress.ToString());
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
                    string BookingPayId = lbPaymentMethod.Text.Trim().Substring(0, 2) + DateTime.Now.ToString("MMddhhmmssfff");
                    Session["BookingPayId"] = BookingPayId;
                    Session.Add("BookingRef", txtBookRef.Text.Trim().ToString());
                    Session["Paid"] = Convert.ToDouble(hftxtpaidamt.Value.Trim() == "" ? "0" : hftxtpaidamt.Value.Trim());
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
        catch (Exception exp)
        {
            throw exp;
        }
    }

    private bool IsBookingAvailable()
    {
        BALBookingLock bl = Session["BookingLock"] != null ? (BALBookingLock)Session["BookingLock"] : null;

        DALBookingLock dbl = new DALBookingLock();
        if (dbl.IsLocked(bl))
        {
            lblBookingLockFound.Visible = true;
            lblBookingLockFound.Text = "The room(s) you are trying to book are no longer available. Please click on the link below to choose the rooms again.";
            lnkBackToCruiseBooking.Visible = true;
            lnkBackToCruiseBooking.NavigateUrl = string.Format("~/Cruise/Booking/CruiseBooking.aspx?PackId={0}&PackageName={1}&NoOfNights={2}&CheckIndate={3}&DepartureId={4}", PackId, PackageName, NoOfNights, CheckIndate, DepartureId);
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
        string amt = "AMOUNT=50";
        string ash = "ap_SecureHash=abc";

        string qs = string.Format("~/Cruise/Booking/PaymentGatewayResponse.aspx?{0}&{1}&{2}&{3}&{4}&{5}", ts, apt, msg, tid, amt, ash);
        Response.Redirect(qs);
        //Response.Redirect("PaymentGatewayResponse.aspx?" + qs);
    }

    //protected void btnPayProceed_Click(object sender, EventArgs e)
    //{
    //    try
    //    {
    //        Session["Hotel"] = null;
    //        if (btnPayProceed.Text == "Proceed For Payment")
    //        {

    //            if (Session["UserCode"] != null)
    //            {
    //                //aev@farhorizonindia.com [1:48:55 PM] Augurs  Technologies Pvt. Ltd.: 12345


    //                DataTable dtrpax = Session["BookedRooms"] as DataTable;

    //                string BRef = txtBookRef.Text.Trim().ToString() + "X" + Convert.ToDouble(dtrpax.Compute("SUM(Pax)", string.Empty)).ToString() + "-" + Session["UserName"].ToString();
    //                Session.Add("BookingRef", BRef);




    //                Session["Paid"] = Convert.ToDouble(txtPaidAmt.Text.Trim() == "" ? "0" : txtPaidAmt.Text.Trim());
    //                blagentpayment._Action = "MailValidate";
    //                blagentpayment._EmailId = Session["AgentMailId"].ToString();
    //                blagentpayment._Password = Session["Password"].ToString();
    //                dtGetReturnedData = dlagentpayment.BindControls(blagentpayment);
    //                if (dtGetReturnedData.Rows.Count > 0)
    //                {
    //                    string BookingPayId = lbPaymentMethod.Text.Trim().Substring(0, 2) + DateTime.Now.ToString("MMddhhmmssfff");
    //                    string Email = Session["AgentMailId"].ToString();
    //                    string PhoneNumber = "9999999999";// hdnfPhoneNumber.Value.Trim().ToString();
    //                    string FirstName = dtGetReturnedData.Rows[0]["FirstName"].ToString();
    //                    string LastName = "XYZ"; //dtGetReturnedData.Rows[0]["LastName"].ToString();
    //                    string PaidAmt = hftxtpaidamt.Value.Trim().ToString();
    //                    string PaymentId = BookingPayId.ToString();
    //                    string BillingAddress = "abc/wsdd,vasant vihar";// lblBillingAddress.Text.Trim().ToString();
    //                    Session["BookingPayId"] = txtBookRef.Text.Trim();// BookingPayId;

    //                    Session["Address"] = lblBillingAddress.Text.Trim().ToString(); ;
    //                    Session["InvName"] = FirstName;

    //                    Session["SubInvName"] = FirstName;

    //                    string[] arr = { };
    //                    if (FirstName != "" && FirstName != null)
    //                    {
    //                        arr = FirstName.Split(' ');
    //                    }
    //                    //Response.Redirect("PaymentGatewayResponse.aspx?BookingPayId=" + PaymentId + "&EmailId=" + Email.ToString() + "&PhoneNumber=" + PhoneNumber.ToString() + "&FirstName=" + FirstName.ToString() + "&LastName=" + LastName.ToString() + "&PaidAmt=" + PaidAmt.ToString() + "&BillingAddress=" + BillingAddress.ToString());
    //                    //http://adventureresortscruises.in/Cruise/booking/sendtoairpay.aspx?BookedId=0&PackName=7N8D+Downstream+Cruise&NoOfNights=7&CheckinDate=12%2f4%2f2016&PackId=Pack4
    //                    Response.Redirect("~/Cruise/booking/sendtoairpay.aspx?BookingPayId=" + PaymentId + "&EmailId=" + Email.ToString() + "&PhoneNumber=" + PhoneNumber.ToString() + "&FirstName=" + arr[0].ToString() + "&LastName=" + LastName.ToString() + "&PaidAmt=" + PaidAmt.ToString() + "&BillingAddress=" + BillingAddress.ToString());
    //                    //ScriptManager.RegisterStartupScript(this, this.GetType(), "Showstatus", "javascript:alert('not registered!!!')", true);

    //                }
    //            }
    //            else
    //            {

    //                Session.Add("BookingRef", ViewState["BookRef"].ToString());


    //                blcus.Email = Session["CustomerMailId"].ToString();
    //                blcus.Password = Session["CustPassword"].ToString();

    //                blcus.action = "LoginCust";
    //                dtGetReturnedData = new DataTable();
    //                dtGetReturnedData = dlcus.checkDuplicateemail(blcus);
    //                Session["Paid"] = Convert.ToDouble(txtPaidAmt.Text.Trim() == "" ? "0" : txtPaidAmt.Text.Trim());
    //                if (dtGetReturnedData.Rows.Count > 0)
    //                {

    //                    Random rnd = new Random();

    //                    string BookingPayId = rnd.Next(10000, 20000).ToString() + DateTime.Now.ToString("MMddhhmmssfff");
    //                    Session["BookingPayId"] = BookingPayId;
    //                    string Email = Session["CustomerMailId"].ToString();

    //                    string PhoneNumber = "9999999999";// hdnfPhoneNumber.Value.Trim().ToString();
    //                    string FirstName = dtGetReturnedData.Rows[0]["FirstName"].ToString();
    //                    string LastName = "XYZ";// dtGetReturnedData.Rows[0]["LastName"].ToString();


    //                    string PaidAmt = hftxtpaidamt.Value.Trim().ToString();
    //                    string PaymentId = BookingPayId.ToString();
    //                    string BillingAddress = "abc/wsdd,vasant vihar";// lblBillingAddress.Text.Trim().ToString();


    //                    Session["Address"] = lblBillingAddress.Text.Trim().ToString();
    //                    Session["InvName"] = dtGetReturnedData.Rows[0]["Title"].ToString() + " " + " " + dtGetReturnedData.Rows[0]["LastName"].ToString();

    //                    Session["SubInvName"] = dtGetReturnedData.Rows[0]["LastName"].ToString() + ", " + dtGetReturnedData.Rows[0]["Title"].ToString() + " " + FirstName;
    //                    string[] arr = { };
    //                    if (FirstName != "" && FirstName != null)
    //                    {
    //                        arr = FirstName.Split(' ');
    //                    }

    //                    Response.Redirect("~/Cruise/booking/sendtoairpay.aspx?BookingPayId=" + PaymentId + "&EmailId=" + Email.ToString() + "&PhoneNumber=" + PhoneNumber.ToString() + "&FirstName=" + FirstName.ToString() + "&LastName=" + LastName.ToString() + "&PaidAmt=" + PaidAmt.ToString() + "&BillingAddress=" + BillingAddress.ToString());
    //                    //  Response.Redirect("~/Cruise/booking/sendtoairpay.aspx?BookingPayId=" + PaymentId + "&EmailId=" + Email.ToString() + "&PhoneNumber=" + PhoneNumber.ToString() + "&FirstName=" + FirstName.ToString() + "&LastName=" + LastName.ToString() + "&PaidAmt=" + PaidAmt.ToString() + "&BillingAddress=" + BillingAddress.ToString());

    //                    //http://localhost:1897/ResortManager/Cruise/booking/SummerisedDetails.aspx?BookedId=0&PackName=Ganges+Exclusive&NoOfNights=5&CheckinDate=5%2f1%2f2016

    //                    //ScriptManager.RegisterStartupScript(this, this.GetType(), "Showstatus", "javascript:alert('not registered!!!')", true);

    //                }
    //            }
    //        }
    //        else
    //        {

    //            if (Convert.ToDecimal(txtPaidAmt.Text) <= Convert.ToDecimal(hdnfCreditLimit.Value))
    //            {
    //                string BookingPayId = lbPaymentMethod.Text.Trim().Substring(0, 2) + DateTime.Now.ToString("MMddhhmmssfff");
    //                Session["BookingPayId"] = BookingPayId;
    //                Session.Add("BookingRef", txtBookRef.Text.Trim().ToString());
    //                Session["Paid"] = Convert.ToDouble(hftxtpaidamt.Value.Trim() == "" ? "0" : hftxtpaidamt.Value.Trim());
    //                Session["InvName"] = Session["UserName"].ToString();
    //                Session["Address"] = null;
    //                Response.Redirect("PaymentGatewayResponse.aspx");
    //            }
    //            else
    //            {
    //                lblPaymentErr.Text = "Payment Amount Exceeding Credit Limit";
    //            }
    //        }
    //    }
    //    catch
    //    {

    //    }
    //}
    protected void btnSbmt_Click(object sender, EventArgs e)
    {
    }

    protected void txtRegNow_Click(object sender, EventArgs e)
    {
        pnlCustReg.Visible = true;
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
                    ViewState["Pass"] = txtCustPass.Text.Trim();

                    Session["CustMailId"] = txtCustMailId.Text.Trim();
                    lblAgentName.Text = DataSecurityManager.Decrypt(dtCustomer.Rows[0]["FirstName"].ToString()) + " " + DataSecurityManager.Decrypt(dtCustomer.Rows[0]["LastName"].ToString());
                    lblBillingAddress.Text = dlcus.GetBillingAddress(dtCustomer.Rows[0]);
                    lbPaymentMethod.Text = DataSecurityManager.Decrypt(dtCustomer.Rows[0]["PaymentMethod"].ToString());
                    hdnfPhoneNumber.Value = DataSecurityManager.Decrypt(dtCustomer.Rows[0]["Telephone"].ToString());
                    Session["CustId"] = dtCustomer.Rows[0]["CustId"].ToString();
                    Session["CustomerCode"] = dtCustomer.Rows[0]["CustId"].ToString();
                    Session.Add("CustomerMailId", DataSecurityManager.Decrypt(dtCustomer.Rows[0]["Email"].ToString()));
                    Session.Add("CustPassword", DataSecurityManager.Decrypt(dtCustomer.Rows[0]["Password"].ToString()));
                    DataTable dtrpax = Session["BookedRooms"] as DataTable;

                    string BookRef = DataSecurityManager.Decrypt(dtCustomer.Rows[0]["FirstName"].ToString()) + DataSecurityManager.Decrypt(dtCustomer.Rows[0]["LastName"].ToString()) + "X" + Convert.ToDouble(dtrpax.Compute("SUM(Pax)", string.Empty)).ToString() + "-" + "Direct Client";
                    ViewState["BookRef"] = BookRef;
                    lbPaymentMethod.Text = "Online";
                    pnlFullDetails.Visible = true;
                    panelwithoutCreditAgent.Visible = true;
                    pnlBookButton.Visible = true;
                    customerLogin.Visible = false;
                    pnlCustReg.Visible = false;
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

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        ViewState["CustPass"] = txtPassWord.Text.Trim();
        sendMail();
        pnlCustReg.Visible = false;
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
            mail.From = new MailAddress("reservations@adventureresortscruises.in");

            mail.To.Add(txtMailAddress.Text.Trim());
            mail.Subject = "Mail Verification";

            Random rnd = new Random();
            string Code = rnd.Next(10000, 99999).ToString();
            hfVCode.Value = Code;

            StringBuilder sb = new StringBuilder();
            sb.Append("<div>");
            sb.Append("<div> Dear " + txtFirstName.Text + ",</div> <div><br/></div><div>Thanks for your registering with us.</div> <div><br/> </div><div>For security reasons we have added this step so that we verify the email address before any booking details is sent across.</div> <div><br/></div> ");
            sb.Append(" <div>To verify your email address please enter the code " + Code + " in the registration screen. </div> <div><br/> </div><div>Do contact us if you have any issue at reservations@adventureresort</div><div><br/></div><div>Thanking you,</div><div><br/></div><div>Reservations Office</div> ");
            sb.Append("</div>");
            sb.Append("<img src='http://adventureresortscruises.in/Cruise/booking/img_logo.png' alt='Image'/><br /><div> Adventure Resorts & Cruises Pvt. Ltd.</div><div> B209, CR Park, New Delhi 110019 </div> <div> Phone: +91 - 011 - 41057370 / 1 / 2 </div><div> Mobile: +91 - 9599755353 </div><div><br/> </div> ");

            mail.IsBodyHtml = true;
            mail.Body = sb.ToString();

            SmtpServer.Port = 587;
            SmtpServer.Credentials = new System.Net.NetworkCredential("reservations@adventureresortscruises.in", "Augurs@123");
            SmtpServer.EnableSsl = false;

            SmtpServer.Send(mail);
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Showstatus", "javascript:alert('Please check your Mail for the Verification Code')", true);
            pnlCustReg.Visible = false;
            customerLogin.Visible = true;

            TableCust.Visible = false;
            tableVerify.Visible = true;
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Showstatus", "javascript:alert('" + ex.Message.ToString() + "')", true);
        }
    }

    protected void btnCloseCust_Click(object sender, EventArgs e)
    {
        pnlCustReg.Visible = false;
    }

    protected void txtMailAddress_TextChanged(object sender, EventArgs e)
    {
        try
        {
            if (IsValid(txtMailAddress.Text.Trim()))
            {
                blcus.action = "chkDuplicate";
                blcus.Email = txtMailAddress.Text.Trim();
                DataTable dtCustomer = dlcus.checkDuplicateemail(blcus);
                if (dtCustomer != null)
                {
                    if (dtCustomer.Rows.Count > 0)
                    {
                        lblError.Text = "This Email Id already Exists";
                        txtMailAddress.Text = "";
                    }
                    else
                    {
                        lblError.Text = "";
                    }
                }
            }
            else
            {
                lblError.Text = "Invalid Email Id";
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
                ddlCountry.Items.Insert(0, new ListItem("-Select-", "0"));
            }
            else
            {
                ddlCountry.Items.Clear();
                ddlCountry.DataSource = null;
                ddlCountry.DataBind();
                ddlCountry.Items.Insert(0, new ListItem("-Select-", "0"));
            }
        }
        catch (Exception sqe)
        {
            ddlCountry.Items.Clear();
            ddlCountry.DataSource = null;
            ddlCountry.DataBind();
            ddlCountry.Items.Insert(0, "-No Accom-");

        }
    }

    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        Session.Clear();
        Session.Abandon();
        Session.RemoveAll();
        System.Web.Security.FormsAuthentication.SignOut();
        Response.Redirect("SearchProperty.aspx");
    }

    protected void btnBack_Click(object sender, EventArgs e)
    {
        try
        {
            Response.Redirect(Session["cruiseBookingUrl"].ToString());


        }
        catch
        {
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
                TableCust.Visible = true;
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Showstatus", "javascript:alert('Wrong Code Entered!')", true);
                TableCust.Visible = false;
                tableVerify.Visible = true;
            }
        }
        catch
        {
        }
    }

    private void BookTheCruise()
    {
        int bookingId = InsertParentTableData();
        InsertChildTableData(bookingId);
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

            blsr._iNights = Convert.ToInt32(packageRow["NoOfNights"]);
            blsr._BookingStatusId = (int)BookingStatusTypes.PROPOSED; //This is a proposed booking and it will be marked as booked on the next page once the payment is received.
            blsr._SeriesId = 0;
            blsr._proposedBooking = true;
            blsr._chartered = false;

            Session.Add("tblBookingBAL", blsr);
            int iBRC = dlsr.GetBookingReferenceCount(blsr);

            if (iBRC > 0)
            {
                System.Web.UI.ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "AlertBox", "alert('The Booking Reference mentioned by you is not unique. Please enter a different reference number.');", true);
                return -1;
            }
            int bookingId = dlsr.AddParentBookingDetail(blsr);
            blsr._iBookingId = bookingId;

            Session["tblBookingBAL"] = blsr;
            return bookingId;
        }
        catch
        {
            throw;
        }
    }

    private int GetPax()
    {
        DataTable dtRoomBookingDetails = Session["BookedRooms"] as DataTable;
        return Convert.ToInt32(dtRoomBookingDetails.Compute("SUM(Pax)", string.Empty));
    }

    private void InsertChildTableData(int bookingId)
    {
        BALBooking blsr = new BALBooking();
        DALBooking dlsr = new DALBooking();

        BALBooking booking = dlsr.GetBookingDetails(bookingId);

        if (booking != null)
        {
            DataTable GridRoomPaxDetail = Session["BookedRooms"] as DataTable;
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

                    blsr.PaymentId = Session["BookingPayId"].ToString();
                    blsr._Amt = Convert.ToDecimal(GridRoomPaxDetail.Rows[LoopCounter]["Price"].ToString());

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

    #region Obsolete Method(s)
    [Obsolete]
    private int InsertBookingTableData(int acmid, int acmtpid, int agid, string bkref, DateTime cin, DateTime cout)
    {
        try
        {
            BALBooking blsr = new BALBooking();
            DALBooking dlsr = new DALBooking();

            DataTable dtbkdetails = new DataTable();
            dtbkdetails = Session["Bookingdt"] as DataTable;

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
            //blsr._BookingStatusId = 1;
            blsr._BookingStatusId = (int)BookingStatusTypes.PROPOSED; //Saving this booking as a proposed booking. Once the payment is done then it will be confirmed.
            blsr._SeriesId = 0;
            //blsr._proposedBooking = false;
            blsr._proposedBooking = true;

            blsr._chartered = false;

            int bookingId = dlsr.AddParentBookingDetail(blsr);
            return bookingId;
        }
        catch (Exception ex)
        {
            return 0;
        }
    }

    [Obsolete]
    private int InsertRoomBookingTableData(DataTable dtbooking, int bookingId, DateTime cin, DateTime cout, int acmid)
    {
        try
        {
            BALBooking blsr = new BALBooking();
            DALBooking dlsr = new DALBooking();

            DataTable dtbkdetails = Session["Bookingdt"] as DataTable;
            Session["maxBookId"] = bookingId;

            int LoopInsertStatus = 0;
            for (int LoopCounter = 0; LoopCounter < dtbkdetails.Rows.Count; LoopCounter++)
            {
                blsr._dtStartDate = cin;
                blsr._dtEndDate = cout;
                blsr._iPaxStaying = Convert.ToInt32(dtbooking.Rows[LoopCounter][3].ToString());
                blsr._bConvertTo_Double_Twin = Convert.ToBoolean(dtbkdetails.Rows[LoopCounter]["ConvDouble"].ToString());
                blsr._cRoomStatus = "B";

                blsr._sRoomNo = dtbkdetails.Rows[LoopCounter][7].ToString();
                blsr._PaidAmount = Convert.ToDouble(Session["Paid"]);

                blsr.action = "AddPriceDetailsToo";
                string[] arr = dtbkdetails.Rows[LoopCounter]["Total"].ToString().Split(' ');

                blsr._Amt = Convert.ToDecimal(arr[1]);
                blsr.PaymentId = Session["BookingPayId"].ToString();

                int GetQueryResponse = dlsr.AddRoomBookingDetails(blsr);

                if (GetQueryResponse > 0)
                    LoopInsertStatus++;
                else
                {

                }
            }

            //DataTable Bookingdt = Session["Bookingdt"] as DataTable;
            if (LoopInsertStatus == dtbooking.Rows.Count)
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Showstatus", "javascript:alert('" + ex.Message.ToString() + "')", true);
            return 0;
        }
    }
    #endregion
}