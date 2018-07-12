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
using FarHorizon.Reservations.BusinessServices.Online.BAL;
using FarHorizon.Reservations.BusinessServices.Online.DAL;
using FarHorizon.DataSecurity;
using System.Globalization;
public partial class Cruise_Booking_FinalpaymentLinkPage : System.Web.UI.Page
{
    DataTable Bookingdt;
    DataTable bookingmealdt;
    DataTable dtroominfo;
    DALBooking dlbooking = new DALBooking();
    BALBooking blbooking = new BALBooking();
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
    string bookingid = "";
    string amount = "";
    string address1=String.Empty;
    string address2 = String.Empty;
    string city = String.Empty;
    string state = String.Empty;
    string postalCode = String.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {

            bookingid = Request.QueryString["bid"];
            
            Session["getbid"] = Request.QueryString["bid"];
            loaddetails(Convert.ToInt32(bookingid));
        }
    }
    public string GetBillingAddress(DataTable dataRow)

    {
       

        address1 = DataSecurityManager.Decrypt(dataRow.Rows[0]["Address1"].ToString());
        address2 = DataSecurityManager.Decrypt(dataRow.Rows[0]["Address2"].ToString());
        city = DataSecurityManager.Decrypt(dataRow.Rows[0]["City"].ToString());
        state = DataSecurityManager.Decrypt(dataRow.Rows[0]["State"].ToString());
        postalCode = DataSecurityManager.Decrypt(dataRow.Rows[0]["PostalCode"].ToString());

        return string.Format("{0} {1}, {2} {3} {4}", address1, address2, city, state, postalCode);
    }
    private void calculate(DataTable dt)
    {
        double total = 0;
        double paidamount = 0;
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            total = total + Convert.ToDouble(dt.Rows[i]["Amount"].ToString());
        }
        lbltotAmt.Text = "INR " + total.ToString("##,0");
        Session["gettotal"] = total;
        txtPaidAmt.Text = "INR " + Convert.ToDecimal(dt.Rows[0]["PaidAmt"].ToString()).ToString("##,0");
        Session["getpaid"] = Convert.ToDecimal(dt.Rows[0]["PaidAmt"].ToString()).ToString("##,0");
        lblBalanceAmt.Text = "INR " + (total - Convert.ToDouble(dt.Rows[0]["PaidAmt"].ToString())).ToString("##,0");
        Session["balancamnt"] = (total - Convert.ToDouble(dt.Rows[0]["PaidAmt"].ToString())).ToString("##,0");
        amount = (total - Convert.ToDouble(dt.Rows[0]["PaidAmt"].ToString())).ToString("##,0");
    }
    private void loaddetails(int bookingid)
    {
        blbooking._iBookingId = Convert.ToInt32(bookingid);
        DataTable dt = dlbooking.paymentreminder(blbooking);
        if (dt != null && dt.Rows.Count > 0)
        {
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                dt.Rows[i]["Name"] = DataSecurityManager.Decrypt(dt.Rows[i]["Name"].ToString());
                dt.Rows[i]["lastname"] = DataSecurityManager.Decrypt(dt.Rows[i]["lastname"].ToString());
                dt.Rows[i]["Email"] = DataSecurityManager.Decrypt(dt.Rows[i]["Email"].ToString());
                dt.Rows[i]["Telephone"] = DataSecurityManager.Decrypt(dt.Rows[i]["Telephone"].ToString());


            }
        }
        else
        {
            // ScriptManager.RegisterStartupScript(this, this.GetType(), "Showstatus", "javascript:alert('Your final payment against this booking has been received. There is no outstanding.')", true);
            //ScriptManager.RegisterStartupScript(this, this.GetType(), "message", "alert('Your final payment against this booking has been received. There is no outstanding.');window.location.href = 'searchproperty1.aspx';", true);

            //string jv = "alert('Your final payment against this booking has been received. There is no outstanding.');";
            //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "msg", jv, true);
            Response.Redirect("searchproperty1.aspx?ID=01");

           // Response.Redirect("searchproperty1.aspx");
        }
        lblAgentName.Text = dt.Rows[0]["Name"].ToString() + " " + dt.Rows[0]["lastname"].ToString();
        Session["InvName"] = DataSecurityManager.Decrypt(dt.Rows[0]["Name"].ToString()) + " " + DataSecurityManager.Decrypt(dt.Rows[0]["lastname"].ToString());

        try
        {

            {
                lblBillingAddress.Text = dt.Rows[0]["BillingAddress"].ToString();
                Session["Address"] = dt.Rows[0]["BillingAddress"].ToString();
            }
        }
        catch
        {
            lblBillingAddress.Text = GetBillingAddress(dt);
            Session["Address"] = GetBillingAddress(dt);
        }
        calculate(dt);
        Session["getdata"] = dt;
    }






    protected void btnSubmit_Click(object sender, EventArgs e)
    {


    }

    protected void btnPayProceed_Click(object sender, EventArgs e)
    {
        try
        {
           
            //DataTable dt = dlcus.getbyemailid(email);
            if (Session["getbid"] != null)
            {
                bookingid = Session["getbid"].ToString();
            }
            blbooking._iBookingId = Convert.ToInt32(bookingid);
            DataTable dt = Session["getdata"] as DataTable;
            string PaymentId = "";
            string PaidAmt = lblBalanceAmt.Text.Split('R')[1];
            PaidAmt = Convert.ToDecimal(PaidAmt).ToString("0.00");


            if (dt != null && dt.Rows.Count > 0)
            {
                PaymentId = dt.Rows[0]["paymentId"].ToString();
                Session["getpayid"] = dt.Rows[0]["paymentId"].ToString();
                Session["getpayid"] = PaymentId;
                string L = dt.Rows[0]["lastname"].ToString();
                if (L == " ")
                {
                    dt.Rows[0]["lastname"] = "XYZ";
                }

                 Response.Redirect("~/Cruise/booking/PaymentGatewayResponse.aspx?BookingPayId=" + PaymentId + "&EmailId=" + dt.Rows[0]["Email"].ToString() + "&PhoneNumber=" + dt.Rows[0]["Telephone"].ToString() + "&FirstName=" + dt.Rows[0]["Name"].ToString() + "&LastName=" + dt.Rows[0]["lastname"].ToString() + "&PaidAmt=" + PaidAmt + "&BillingAddress=" + lblBillingAddress.Text);
              //  Response.Redirect("~/Cruise/booking/sendtoairpay.aspx?BookingPayId=" + PaymentId + "&EmailId=" + dt.Rows[0]["Email"].ToString() + "&PhoneNumber=" + dt.Rows[0]["Telephone"].ToString() + "&FirstName=" + dt.Rows[0]["Name"].ToString() + "&LastName=" + dt.Rows[0]["lastname"].ToString() + "&PaidAmt=" + Convert.ToDecimal(PaidAmt) + "&BillingAddress=" + lblBillingAddress.Text + "&City=" + city + "&State=" + state + "&PinCode=" + postalCode);
            }

        }
        catch (Exception ex) { }
    }
}