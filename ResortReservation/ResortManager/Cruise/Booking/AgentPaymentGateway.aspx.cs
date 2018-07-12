using FarHorizon.DataSecurity;
using FarHorizon.Reservations.BusinessServices;
using FarHorizon.Reservations.BusinessServices.Online.BAL;
using FarHorizon.Reservations.BusinessServices.Online.DAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
using System.Text.RegularExpressions;

public partial class AgentPaymentGateway : System.Web.UI.Page
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
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                string AgentURL = "";
                if (Session["AgentId"].ToString() != null)
                {

                    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["ReservationConnectionString"].ConnectionString);
                    con.Open();
                    string sqlQuery = "select agenturl from tblagentmaster where agentid =" + Session["AgentId"] + "";
                    SqlDataAdapter adp = new SqlDataAdapter(sqlQuery, con);
                    DataTable dtGetagnturl = new DataTable();
                    adp.Fill(dtGetagnturl);

                    if (dtGetagnturl.Rows.Count > 0)
                    {
                        AgentURL = dtGetagnturl.Rows[0][0].ToString();
                    }

                    con.Close();
                }


                String agnt = AgentURL;
                //Regex to check if string starts with http or https
                Regex rgx = new Regex(@"^(http|https)://.*$");
                //Regex to remove the text content
                if (rgx.IsMatch(agnt))
                {
                    //do your task here
                }
                else
                {
                    agnt = "http://" + agnt;
                }
                //Session["agnturi"] = agnt;
                lblpckgname.Text = Session["packagenames"].ToString();
                lblchkin.Text = Session["chkindate"].ToString();
                lblchkout.Text = Session["chkoutdate"].ToString();
                loadall();
                LoadBookedRoomDetails();

                agnt += "?PackageName=" + Session["packagenames"].ToString() + "&CheckInDate=" + Session["chkindate"].ToString() + "&CheckOutdate=" + Session["chkoutdate"].ToString() + "&InvoiceTo=" + lblAgentName.Text.ToString() + "&Gross=" + lblGross.Text.ToString() + "&TotalAmt=" + lbltotAmt.Text.ToString() + "&TaxableAmt=" + lbltaxin.Text.ToString() + "&GST=" + lblTax.Text.ToString() + "&AdvanceAmt=" + txtPaidAmt.Text.ToString() + "";

                Session["agnturi"] = agnt;

             
                

                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "script", "$(function () { openWin(); });", true);
            }
        }
        catch { }
    }


    //private string currency(string name, string amount, string code)
    //{

    //    // RegionInfo regionInfo = new RegionInfo(code);
    //    // Console.WriteLine(regionInfo.CurrencySymbol); 
    //    var countrydetails = "INR";
    //    var countryname = "";
    //    WebClient obj = new WebClient();
    //    var rsponse = obj.DownloadString("http://www.apilayer.net/api/live?access_key=21a30315c656c3feec8b24cd3c211dfa&format=1&source=USD");


    //    var CompanyInfoData = Newtonsoft.Json.JsonConvert.DeserializeObject<Dataval>(rsponse);
    //    dynamic data = JObject.Parse(rsponse);
    //    var aa = data.quotes;
    //    if (countrydetails != null)
    //    {


    //        foreach (var r in aa)
    //        {
    //            var sd = "USD" + countrydetails.Code;
    //            if (r.Name.Trim().ToLower() == sd.Trim().ToLower())
    //            {

    //                var m = r.Name;
    //                var n = r.Value;
    //                var am = String.Format("{0:0.00}", r.Value * amount);
    //                return am;
    //            }
    //            else
    //            {
    //                if (r.Name.Trim().ToLower() == "usdusd")
    //                {
    //                    var am = String.Format("{0:0.00}", r.Value * amount);
    //                    return am;
    //                }
    //            }

    //        }
    //        return countryname;

    //    }
    //    else
    //    {
    //        foreach (var r in aa)
    //        {
    //            if (r.Name.Trim().ToLower() == "usdusd")
    //            {
    //                var am = String.Format("{0:0.00}", r.Value * amount);
    //                return am;
    //            }
    //        }
    //    }
    //    return countryname;

    //}
    private void currency_converter()
    {
   

    ///7adab05b358d13cdb27549445ba9f15d
    //var rsponse = obj.DownloadString("http://www.apilayer.net/api/live?access_key=21a30315c656c3feec8b24cd3c211dfa&format=1&source=USD");

        //try
        //{
        //    string fromcountry = ddlfromcountry.Value;
        //    string tocountry = ddltocountry.Value;
        //    string amount = txtamount.Text;
        //    string URL = "http://www.google.com/finance/converter?a=" + amount + "&from=" + fromcountry + "&to=" + tocountry;
        //    byte[] databuffer = Encoding.ASCII.GetBytes("test=postvar&test2=another");
        //    HttpWebRequest _webreqquest = (HttpWebRequest)WebRequest.Create(URL);
        //    _webreqquest.Method = "POST";
        //    _webreqquest.ContentType = "application/x-www-form-urlencoded";
        //    _webreqquest.ContentLength = databuffer.Length;
        //    Stream PostData = _webreqquest.GetRequestStream();
        //    PostData.Write(databuffer, 0, databuffer.Length);
        //    PostData.Close();
        //    HttpWebResponse WebResp = (HttpWebResponse)_webreqquest.GetResponse();
        //    Stream finalanswer = WebResp.GetResponseStream();
        //    StreamReader _answer = new StreamReader(finalanswer);
        //    string[] value = Regex.Split(_answer.ReadToEnd(), "&nbsp;");


    //    int first = value[1].IndexOf("<div id=currency_converter_result>");
    //    int last = value[1].LastIndexOf("</span>");

    //    string str2 = value[1].Substring(first, last - first);
    //    Label1.Text = str2;
    //}
    //catch
    //{
    //    Label1.Text = "Please enter a valid amount.";
    //}
}
    #region FetchingDataMethod
   
    private void loadall()
    {
        if (Session["UserCode"] != null)
        {         
            try
            {
               
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
                        if (oncredit)
                        {
                            panelwithoutCreditAgent.Visible = false;
                            panelwithoutCreditAgent.Visible = true;
                        }
                        else
                        {
                            panelwithoutCreditAgent.Visible = true;
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
                    

                    Session["CustomerMailId"] = Session["CustMailId"].ToString();
                    lblAgentName.Text = DataSecurityManager.Decrypt(dtCustomer.Rows[0]["FirstName"].ToString()) + " " + DataSecurityManager.Decrypt(dtCustomer.Rows[0]["LastName"].ToString());
                    lblBillingAddress.Text = dlcus.GetBillingAddress(dtCustomer.Rows[0]);
                    lbPaymentMethod.Text = dtCustomer.Rows[0]["PaymentMethod"].ToString();
                    hdnfPhoneNumber.Value = dtCustomer.Rows[0]["Telephone"].ToString();
                    Session["CustId"] = dtCustomer.Rows[0]["CustId"].ToString();
                    DataTable dtrpax = SessionServices.RetrieveSession<DataTable>("BookedRooms");                  
                    string BookRef = dtCustomer.Rows[0]["FirstName"].ToString() + dtCustomer.Rows[0]["LastName"].ToString() + "X" + Convert.ToDouble(dtrpax.Compute("SUM(Pax)", string.Empty)).ToString() + "-" + "Direct Client";
                    ViewState["BookRef"] = BookRef;
                    lbPaymentMethod.Text = "Online";
                    pnlFullDetails.Visible = true;
                    panelwithoutCreditAgent.Visible = true;                 
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
      
    }
    private void LoadBookedRoomDetails()
    {
        try
        {
            dtGetBookedRooms = SessionServices.RetrieveSession<DataTable>("BookedRooms");       
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
                TotalPaybleAmt = TotalPaybleAmt + Convert.ToDecimal(price);              
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
            lbltaxin.Text = "INR " + taxableamt.ToString("##,0");        
            if (lblDiscountper.Text != "0%")
            {
                lblDiscount.Text = dtGetBookedRooms.Rows[0]["Currency"].ToString() + " " + totyadiscount.ToString("##,0");
            }
            else
            {               
                getdiscount.Visible = false;
            }
            lblalltotal.Text = dtGetBookedRooms.Rows[0]["Currency"].ToString() + " " + getalltotal.ToString("##,0");
            lbltotAmt.Text = dtGetBookedRooms.Rows[0]["Currency"].ToString() + " " + total.ToString("##,0");
            lblGross.Text = dtGetBookedRooms.Rows[0]["Currency"].ToString() + " " + Convert.ToDouble(grosstotal.ToString()).ToString("##,0");
            lblCurrency.Text = dtGetBookedRooms.Rows[0]["Currency"].ToString() + " ";          
            if(Session["advanceamount"].ToString()== "")
            {
                txtPaidAmt.Text = Convert.ToDouble(Math.Round(((100 * TotalPaybleAmt) / 100)).ToString("#.##")).ToString("##,0");               
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
            }
          
            lblBalanceAmt.Text = dtGetBookedRooms.Rows[0]["Currency"].ToString() + " " + Convert.ToDouble(Math.Round((TotalPaybleAmt - Convert.ToDecimal(txtPaidAmt.Text))).ToString()).ToString("##,0");
            if (lblBalanceAmt.Text == "INR 0")
            {
                trbalanceamount.Visible = false;
            }
            Session["getbalanceSummerlizede"] = Math.Round((TotalPaybleAmt - Convert.ToDecimal(txtPaidAmt.Text))).ToString();          
            dtGetBookedRooms = dtgroupedData;          
            #endregion
        }
        catch (Exception ex)
        {
        }
    }
    #endregion
    //protected void btnSubmit_Click(object sender, EventArgs e)
    //{
    //    try
    //    {
    //        if (Session["AgentId"].ToString() != null)
    //        {

    //            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["ReservationConnectionString"].ConnectionString);
    //            con.Open();
    //            string sqlQuery = "select agenturl from tblagentmaster where agentid =" + Session["AgentId"] + "";
    //            SqlDataAdapter adp = new SqlDataAdapter(sqlQuery, con);
    //            DataTable dtGetagnturl = new DataTable();
    //            adp.Fill(dtGetagnturl);
    //            string AgentURL = "";
    //            if (dtGetagnturl.Rows.Count > 0)
    //            {
    //                AgentURL = dtGetagnturl.Rows[0][0].ToString();
    //            }
    //string addurl = "?PackageName=" + Session["packagenames"].ToString() + "&CheckInDate=" + Session["chkindate"].ToString() + "&CheckOutdate=" + Session["chkoutdate"].ToString() + "&InvoiceTo=" + lblAgentName.Text.ToString() + "&Gross=" + lblGross.Text.ToString() + "";
    //Response.Redirect(AgentURL + "" + addurl);

    //            // Response.Write(string.Format("<script>window.open('{0}','_blank');</script>", AgentURL));

    //            //string redirect = "<script>window.open('"+ AgentURL + "');</script>";

    //            Page.ClientScript.RegisterStartupScript(this.GetType(), "OpenWindow", "window.open('"+ AgentURL + "','_newtab');", true);

    //            //Response.Write(String.Format("window.open('{0}','_blank')", ResolveUrl(AgentURL)));
    //            //Response.Redirect(AgentURL);
    //            con.Close();
    //        }
    //    }
    //    catch { }
    //}
}