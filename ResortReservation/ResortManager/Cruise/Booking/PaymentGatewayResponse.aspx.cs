using FarHorizon.Reservations.BusinessServices;
using FarHorizon.Reservations.BusinessServices.Online;
using FarHorizon.Reservations.BusinessServices.Online.BAL;
using FarHorizon.Reservations.BusinessServices.Online.DAL;

using FarHorizon.Reservations.Common;
using iTextSharp.text;
using iTextSharp.text.pdf;
using Spire.Pdf;
using Spire.Pdf.HtmlConverter;
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.IO;
using System.Net.Mail;
using System.Text;
using System.Threading;
using System.Web.UI;
using System.Web.UI.WebControls;
using FarHorizon.DataSecurity;

public partial class response : System.Web.UI.Page
{
    #region Variable(s)
    BALBooking blsr = new BALBooking();
    DALBooking dlsr = new DALBooking();
    public string packageid = string.Empty;
    DataView dv;
    DataTable dt;
    int totpax = 0;
    int roomCatId = 0;
    int irpax = 0;
    double Totamt = 0;
    public int BookedId = 0;
    DataTable dtGetReturnedData;
    byte[] pdfBytes;
    DALPackageRateCard dlpack = new DALPackageRateCard();
    SmtpClient _smtpServer;
    DataTable dtbkdetails;
    int iagentid = 0;
    int iAccomId = 0;
    int iaccomtypeid = 0;
    DateTime chkin;
    DateTime chkout;
    int custId = 0;
    string bookref;
    #endregion

    #region Properties
    string SmtpServerAddress
    {
        get
        {
            return (ConfigurationManager.AppSettings["SMTPServer"] != null ? ConfigurationManager.AppSettings["SMTPServer"] : "adventureresortscruises.in");
        }
    }

    string SmtpUserId
    {
        get
        {
            return (ConfigurationManager.AppSettings["SMTPUserId"] != null ? ConfigurationManager.AppSettings["SMTPUserId"] : "reservations@adventureresortscruises.in");
        }
    }

    string SmtpPassword
    {
        get
        {
            return (ConfigurationManager.AppSettings["SMTPPwd"] != null ? ConfigurationManager.AppSettings["SMTPPwd"] : "Augurs@123");
        }
    }

    SmtpClient SmtpServer
    {
        get
        {
            if (_smtpServer == null)
            {
                _smtpServer = new SmtpClient(SmtpServerAddress);
                _smtpServer.Port = 587;
                _smtpServer.Credentials = new System.Net.NetworkCredential(SmtpUserId, SmtpPassword);
                _smtpServer.EnableSsl = false;
            }
            return _smtpServer;
        }
    }
    #endregion
    string CompanyName = ConfigurationManager.AppSettings["cName"];
    string CompanyEmail = ConfigurationManager.AppSettings["cEmail"];
    string CompanyAddress = ConfigurationManager.AppSettings["cAddress"];
    string CompanyPhoneNo = ConfigurationManager.AppSettings["cPhoneNo"];
    string CompanyMobile = ConfigurationManager.AppSettings["cMobile"];
    string CompanyLogo = ConfigurationManager.AppSettings["cLogo"];
    string ccEmail = ConfigurationManager.AppSettings["ccEmail"];
    SqlConnection con;
    protected void Page_Load(object sender, EventArgs e)
    {
        con = new SqlConnection(GetConnectionString());
        Session["check"] = null;
        if (!IsPostBack)
        {
            Session["nullsession"] = 1;
            ViewState["sentMail"] = "0";
            lbBookinDate.Text = Convert.ToDateTime(System.DateTime.Now).ToString("d MMMM, yyyy");
            lbInvoiceNO.Text = getinvoice();
            dated.Text = Convert.ToDateTime(System.DateTime.Now).ToString("d MMMM, yyyy");
        }

        lblBuyerName.Text = Session["InvName"] != null ? Session["InvName"].ToString() : string.Empty;
        lblBuyerAddress.Text = Session["Address"] != null ? Session["Address"].ToString() : string.Empty;

        if (Session["Hotel"] != null)
        {
            try
            {
                if (Session["Usercode"] != null)
                {
                    Int32.TryParse(Session["AId"].ToString(), out iagentid);
                }
                else if (Session["CustId"] != null)
                {
                    iagentid = 248;
                }

                Int32.TryParse(Session["AccomId"].ToString(), out iAccomId);
                Int32.TryParse(Session["iAccomtypeId"].ToString(), out iaccomtypeid);

                DateTime.TryParse(Session["Chkin"].ToString(), out chkin);
                DateTime.TryParse(Session["chkout"].ToString(), out chkout);

                bookref = Session["BookingRef"].ToString();
                blsr._sBookingRef = bookref;
                blsr._dtStartDate = chkin;
                blsr._dtEndDate = chkout;
                blsr._iAccomTypeId = iaccomtypeid;
                blsr._iAccomId = iAccomId;

                int iBRC = dlsr.GetBookingReferenceCount(blsr);
                if (iBRC > 0)
                {
                    System.Web.UI.ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "AlertBox", "alert('The Booking Reference mentioned by you is not unique. Please enter a different reference number.');", true);
                    return;
                }
            }
            catch
            {
            }
        }
        if (Session["getdata"] != null)
        {
            //lbltotalGross.Visible = false;
            //lblGst.Visible = false;
            trtot.Visible = true;
            tradvance.Visible = true;
            Label5.Visible = false;
            lblBalanceDueOn.Visible = false;
            DataTable dt = Session["getdata"] as DataTable;
            string AccomId = dt.Rows[0]["AccomId"].ToString();
            string AgentId = dt.Rows[0]["AgentId"].ToString();
            if (AccomId == "7")
            {
                gdvCruiseRooms.DataSource = dt;
                gdvCruiseRooms.DataBind();
                //gdvSelectedRooms.DataSource = dt;
                //gdvSelectedRooms.DataBind();

                calculateall(dt);
                Session["Title"] = dt.Rows[0]["Title"].ToString();
                Session["getpayid"] = dt.Rows[0]["paymentId"].ToString();
                if (AgentId == "247")
                {
                    Session["geteamil"] = dt.Rows[0]["Email"].ToString();
                }
                else
                {
                    Session["AgentMailId"] = dt.Rows[0]["Email"].ToString();
                }
                lbBookingNo.Text = dt.Rows[0]["BookingCode"].ToString();
                lbBookinDate.Text = Convert.ToDateTime(dt.Rows[0]["BookingDate"].ToString()).ToString("d MMMM, yyyy");
                lblArrvDate.Text = Convert.ToDateTime(dt.Rows[0]["StartDate"].ToString()).ToString("d MMMM, yyyy");
                lblDepartDate.Text = Convert.ToDateTime(dt.Rows[0]["enddate"].ToString()).ToString("d MMMM, yyyy");
                lbStrtEnd.Text = "Cruise starts from " + Convert.ToDateTime(dt.Rows[0]["StartDate"].ToString()).ToString("d MMMM, yyyy") + "  and ends at " + Convert.ToDateTime(dt.Rows[0]["enddate"].ToString()).ToString("d MMMM, yyyy");
                lblVessel.Text = "MVM Mahabahu";
                lbpackageName.Text = dt.Rows[0]["Packagename"].ToString();
                // lbRuppeeinwords.Text = GF.NumbersToWords(Convert.ToInt32(Session["gettotal"].ToString()));
                lbRuppeeinwords.Text = GF.NumbersToWords(Convert.ToInt32(Session["gettotal"].ToString()));
                
                lblTotAMt.Text = Convert.ToDecimal(Session["gettotal"].ToString()).ToString("##,0");
                lbladvance.Text = Convert.ToDecimal(Session["getpaid"]).ToString("##,0");
                lblTotPaid.Text = Convert.ToDecimal(Session["balancamnt"]).ToString("##,0");
                lblBalance.Text = (Convert.ToDecimal(lblTotAMt.Text.Replace(",", "")) - (Convert.ToDecimal(lbladvance.Text.Replace(",", "")) + Convert.ToDecimal(lblTotPaid.Text.Replace(",", "")))).ToString();
            }
            else
            {
                Label5.Visible = true;
                lblBalanceDueOn.Visible = true;
                trtot.Visible = false;
                tradvance.Visible = false;
                double tax = 0;
                Session["categoryAlias"] = dt.Rows[0]["AccomInitial"].ToString();
                lbInvoiceNO.Text = getinvoice();
                //  DataTable Bookingdt = SessionServices.RetrieveSession<DataTable>("Bookingdt");
                // DataTable Bookingdt = Session["Bookingdt"] as DataTable;
                //if (dt != null && dt.Rows.Count > 0)
                //{
                //    for (int i = 0; i < dt.Rows.Count; i++)
                //    {
                //        dt.Rows[i]["Price"] = dt.Rows[i]["Price"].ToString().Split('R')[1];
                //        dt.Rows[i]["Inclusivetax"] = dt.Rows[i]["Inclusivetax"].ToString().Split('R')[1];
                //        tax = tax + Convert.ToDouble(dt.Rows[i]["TaxPercentage"].ToString());
                //    }

                //}
                gdvSelectedRooms.DataSource = dt;
                gdvSelectedRooms.DataBind();

                lblacm.Text = "Accomodation Name: " + dt.Rows[0]["AccomName"].ToString();
                //gdvSelectedRooms.FooterRow.Cells[2].Text = "Service Tax @ " + Session["Taxpax"].ToString() + "%" + " </r> <b> Grand Total </b> ";

                double totalAmount = Math.Round(Convert.ToDouble(dt.Compute("SUM(Inclusivetax)", "[Inclusivetax] > 0")));

                double totalAmount1 = caclculatetotalFinal(dt);
                double TaxAmount = caclculateTax(dt);
                //     double totagross = caclculategrossforHotel(dt);

                Session["Title"] = dt.Rows[0]["Title"].ToString();
                Session["getpayid"] = dt.Rows[0]["paymentId"].ToString();
                if (AgentId == "248")
                {
                    Session["geteamil"] = dt.Rows[0]["Email"].ToString();
                }
                else
                {
                    Session["AgentMailId"] = dt.Rows[0]["Email"].ToString();
                }
                // Session["geteamil"] = dt.Rows[0]["Email"].ToString();
                lbBookingNo.Text = dt.Rows[0]["BookingCode"].ToString();

                lbBookinDate.Text = Convert.ToDateTime(dt.Rows[0]["BookingDate"].ToString()).ToString("d MMMM, yyyy");
                lblArrvDate.Text = Convert.ToDateTime(dt.Rows[0]["StartDate"].ToString()).ToString("d MMMM, yyyy");
                lblDepartDate.Text = Convert.ToDateTime(dt.Rows[0]["enddate"].ToString()).ToString("d MMMM, yyyy");
                //  lblGross.Text = "INR " + Convert.ToInt32(totagross).ToString("##,0");
                // lbpackageName.Text = dt.Rows[0]["AccomName"].ToString();

                //lblgetTotal.Text = "INR " + Convert.ToInt32(totalAmount1.ToString()).ToString("##,0");
                lblgetTotal.Text = Convert.ToInt32(totalAmount1.ToString()).ToString("##,0");
                string amountText = string.Format("{0}<br>{1}<br>{2}",
                        totalAmount.ToString("#.##"),
                        " ",
                        totalAmount1.ToString("#.##"));
                //gdvSelectedRooms.FooterRow.Cells[3].Text = amountText;

                Label1.Text = "@" + Convert.ToDouble(dt.Rows[0]["TaxPercentage"].ToString()) + "%";
                //gdvSelectedRooms.FooterRow.Cells[3].Text = Math.Round(Convert.ToDouble(Bookingdt.Compute("SUM(Total1)", "[Total1] > 0"))).ToString("#.##") + " </br> " + Math.Round((4.5 * (Convert.ToInt32(Bookingdt.Compute("SUM(Total1)", "[Total1] > 0")) / 100))).ToString("#.##") + " </br> " + Math.Round((Convert.ToDouble(Bookingdt.Compute("SUM(Total1)", "[Total1] > 0")) + (4.5 * (Convert.ToInt32(Bookingdt.Compute("SUM(Total1)", "[Total1] > 0")) / 100)))).ToString("#.##") + " ";
                //Label3.Text = "INR " + Convert.ToInt32(TaxAmount).ToString("##,0");
                Label3.Text = Convert.ToInt32(TaxAmount).ToString("##,0");
                lbPax.Text = Convert.ToInt32(dt.Compute("SUM(Pax)", "[Pax] > 0")).ToString();
                Label2.Text = "Grand Total";
                //lblGetGrandTotal.Text = "INR " + Convert.ToInt32(totalAmount.ToString()).ToString("##,0");
                lblGetGrandTotal.Text = Convert.ToInt32(totalAmount.ToString()).ToString("##,0");

                //lblTotAMt.Text = Math.Round((Convert.ToDouble(Bookingdt.Compute("SUM(Total1)", "[Total1] > 0")) + (4.5 * (Convert.ToInt32(Bookingdt.Compute("SUM(Total1)", "[Total1] > 0")) / 100)))).ToString("#.##");
                //     lbRuppeeinwords.Text = GF.NumbersToWords(Convert.ToInt32(lblGross.Text.Split('R')[1].Replace(",", "")));

                //lbRuppeeinwords.Text = GF.NumbersToWords(Convert.ToInt32(lblGetGrandTotal.Text.Split('R')[1].Replace(",", "")));

                lbRuppeeinwords.Text = GF.NumbersToWords(Convert.ToInt32(lblGetGrandTotal.Text));
                //    lblTotAMt.Text = Convert.ToInt32(totalAmount1.ToString("#.##")).ToString("##,0");
                string PaidAmt = dt.Rows[0]["PaidAmt"].ToString();
                //double PaidAmt= Convert.ToDouble(dt.Rows[i]["TaxPercentage"].ToString());
                //double PaidAmt = Math.Round(Convert.ToDouble(dt.Compute("SUM(PaidAmt)", "[PaidAmt] > 0")));

                double balanceAmount = totalAmount1;
                double GrandTotal = totalAmount;
                lblTotPaid.Text = Math.Round((Convert.ToDouble(balanceAmount) - Convert.ToDouble(PaidAmt))).ToString("#.##");
                //Math.Round((Convert.ToDouble(lblTotAMt.Text.Replace(",", "")) - Convert.ToDouble(lblTotPaid.Text.Replace(",", ""))));

                if (balanceAmount > 0)
                {
                    //lblBalance.Text = Math.Round((Convert.ToDouble(balanceAmount) - Convert.ToDouble(PaidAmt))).ToString("#.##");

                    //lblBalance.Text = Convert.ToInt32(lblBalance.Text).ToString("##,0");

                    lblBalance.Text = "0";
                    // lblTotPaid.Text = Math.Round((Convert.ToDouble(GrandTotal) - Convert.ToDouble(lblBalance.Text))).ToString("#.##"); 
                    lbBalanceDueIn.Text = Convert.ToDateTime(lblArrvDate.Text).AddDays(-30).ToString("d MMMM, yyyy");
                }
                else
                {
                    lblBalance.Text = "0";
                    lblTotPaid.Text = "0";
                    lbBalanceDueIn.Text = string.Empty;
                    lblBalanceDueOn.Visible = false;
                }

            }
        }
    }
    private void calculateall(DataTable dt)
    {
        double gross = 0;
        double TaxableAmount = 0;
        double TaxAmount = 0;
        double discount = 0;
        double invoiceamount = 0;
        Label1.Text = "@" + dt.Rows[0]["TaxPercentage"].ToString().Split('.')[0] + "%";
        for (int n = 0; n < dt.Rows.Count; n++)
        {
            TaxableAmount = TaxableAmount + Convert.ToDouble(dt.Rows[n]["TaxableAmount"].ToString());
            gross = gross + Convert.ToDouble(dt.Rows[n]["Gross"].ToString());
            TaxAmount = TaxAmount + Convert.ToDouble(dt.Rows[n]["TaxAmount"].ToString());
            invoiceamount = invoiceamount + Convert.ToDouble(dt.Rows[n]["Amount"].ToString());
            if (dt.Rows[n]["TaxAmount"].ToString() == "0.00")
            {

            }
            else
            {
                discount = discount + Convert.ToDouble(dt.Rows[n]["DiscountAmount"].ToString());
            }
        }
        Label6.Text = "Invoice Amount";
        //lblGross.Text = "INR " + invoiceamount.ToString("##,0");
        //Label3.Text = "INR " + TaxAmount.ToString("##,0");
        //lblTaxableAmount.Text = "INR " + TaxableAmount.ToString("##,0");
        //lblgetTotal.Text = "INR " + gross.ToString("##,0");

        //Label7.Text = "Taxable Amount";
        //if (discount != 0)
        //{
        //    Label4.Text = "Discount";
        //    lblDiscount.Text = "INR " + discount.ToString("##,0");
        //}
        lblGross.Text = invoiceamount.ToString("##,0");
        Label3.Text = TaxAmount.ToString("##,0");
//        lblTaxableAmount.Text = TaxableAmount.ToString("##,0");
        lblgetTotal.Text = gross.ToString("##,0");

       // Label7.Text = "Taxable Amount";
        if (discount != 0)
        {
            Label4.Text = "Discount";
            lblDiscount.Text = discount.ToString("##,0");
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
    public void Sendfinalmail()
    {
        try
        {
            //string packId = Session["PackageId"] != null ? Session["PackageId"].ToString() : string.Empty;
            //string packageName = string.Empty;

            //if (!string.IsNullOrEmpty(packId))
            //{
            //    string sqlQuery = "SELECT [PackageName], (select LocationName from dbo.Locations where LocationId = tblPackages.BordingFrom) as 'BoardFrom', (select LocationName from dbo.Locations where LocationId = tblPackages.BoadingTo) as 'BoardTo', NoOfNights FROM [dbo].[tblPackages] where PackageId = '" + Session["PackageId"] + "'";
            //    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["ReservationConnectionString"].ConnectionString);
            //    con.Open();
            //    SqlDataAdapter adp = new SqlDataAdapter(sqlQuery, con);
            //    DataTable dtGetPackageDetails = new DataTable();

            //    adp.Fill(dtGetPackageDetails);
            //    if (string.Compare(packageName, dtGetPackageDetails.Rows[0]["PackageName"].ToString(), true) != 0)
            //    {
            //        packageName = dtGetPackageDetails.Rows[0]["PackageName"].ToString();
            //    }

            //    lbpackageName.Text = "Package: " + packageName + ", " + dtGetPackageDetails.Rows[0]["NoOfNights"].ToString() + " Nights";
            //    lbStrtEnd.Text = "Cruise starts from " + dtGetPackageDetails.Rows[0]["BoardFrom"].ToString() + "  and ends at " + dtGetPackageDetails.Rows[0]["BoardTo"].ToString();
            //    con.Close();
            //}

            //blsr.action = "getmaxbookingcode";
            //DataTable dtbkcode = dlsr.GetMaxBookingId(blsr);

            //string bref = Session["BookingRef"].ToString();
            string l = Session["InvName"].ToString();

            char[] spaceSeparator = new char[] { ' ' };
            char[] commaSeparator = new char[] { ',' };
            string[] result;
            string firstName = " ";
            string lastName = " ";

            result = l.Split(spaceSeparator, StringSplitOptions.None);
            for (int i = 0; i <= result.Length; i++)
            {
                if (i == 0)
                {
                    firstName = result[i];
                }
                else if (i == 1)
                {
                    lastName = result[i];
                }
            }

            #region Preparing MailMessage
            MailMessage mail = new MailMessage();
            //mail.From = new MailAddress("reservations@adventureresortscruises.in", "ARC Reservations");
            mail.From = new MailAddress("" + CompanyEmail + "", "Reservations");

            // string toEmailId = Session["geteamil"].ToString();
            string toEmailId = Session["AgentMailId"] != null ? Session["AgentMailId"].ToString() : Session["geteamil"] != null ? Session["geteamil"].ToString() : Session["CustMailId"].ToString();
            if (Debugger.IsAttached)
                toEmailId = "rohit@farhorizonindia.com";

            if (string.IsNullOrEmpty(toEmailId))
                return;

            mail.To.Add(toEmailId);
            mail.CC.Add(ccEmail);
            if (Session["geteamil"] != null && Session["geteamil"] != "")
            {
                if (Session["AgentMailId"] != null && Session["AgentMailId"] != "")
                {
                    // mail.Subject = "Payment Received For Reservation- " + DataSecurityManager.Decrypt(Session["Title"] != null ? Session["Title"].ToString():" ") + ". " + Session["InvName"].ToString() + " - " + lbBookingNo.Text + "";
                    mail.Subject = "Payment Received For Reservation - " + Session["InvName"].ToString() + " - " + lbBookingNo.Text + "";
                }
                else
                {
                    mail.Subject = "Payment Received For Reservation - " + DataSecurityManager.Decrypt(Session["Title"] != null ? Session["Title"].ToString() : " ") + ". " + Session["InvName"].ToString() + " - " + lbBookingNo.Text + "";
                }
            }
            else
            {
                if (Session["AgentMailId"] != null && Session["AgentMailId"] != "")
                {
                    mail.Subject = "Reservation - " + Session["SubInvName"].ToString() + " - " + lbBookingNo.Text + " ";
                }
                else
                {
                    mail.Subject = "Reservation - " + Session["InvName"].ToString() + " - " + lbBookingNo.Text + " ";
                }
            }
            #endregion

            #region Email Body
            StringBuilder sb = new StringBuilder();
            //sb.Append("<div>");
            //sb.Append("<div>Booking No:" + lbBookingNo.Text + "</div> <div><br/></div> <div> Date of Booking: " + Convert.ToDateTime(lbBookinDate.Text).ToString("d MMMM, yyyy") + " </div> <div><br/> </div> <div> Dear Mr. " + lastName + ",</div> <div><br/></div><div> Namaskar! Greetings from ARC Adventure Resorts & Cruises Pvt. Ltd. ! </div> <div><br/> </div><div> Thank you for the payment of Rs." + lblTotPaid.Text + " .</div><div><br/> </div><div> Your booking is <strong>confirmed</strong>.</div><div><br/> </div><div>Request you to <a href=http://test1.adventureresortscruises.in/Cruise/Masters/NewRegister.aspx?bid=" + Session["getbid"].ToString() + ">Click Here</a> to share your details to help us server you better.</div><div><br/>  </div></div> ");
            //sb.Append(" <div>Best Wishes,</div> <div><br/>  </div> <div> The Mahabaahu Team!</div> ");
            //sb.Append("</div>");
            //sb.Append("<img src='http://test1.adventureresortscruises.in/Cruise/booking/ARC_Logo.jpg.png' alt='Image'/><br /><div> Adventure Resorts & Cruises Pvt. Ltd.</div><div> B209, CR Park, New Delhi 110019 </div> <div>Phone: +91 - 011 - 41057370 / 1 / 2 </div><div> Mobile: +91 - 9599755353 </div><div><br/> </div> ");
            //sb.Append("<div>The booking policy of the cruise can be referred to at <a href='http://www.mahabaahucruiseindia.com/cruise-policy' target='_blank' data-saferedirecturl='https://www.google.com/url?hl=en&amp;q=http://www.mahabaahucruiseindia.com/cruise-policy&amp;source=gmail&amp;ust=1470139247045000&amp;usg=AFQjCNH3vyzjL507K4FspRY6TihAfogUug'>http://www.mahabaahucruiseindia.com/cruise-policy </a>.</div><div><br/></div><div> Enclosure:</div><div> Invoice for your booking is attached with this email showing full payment received.</div> ");

            sb.Append("<div>");

            //if (Session["CustMailId"] != null && Session["CustMailId"] != "")
            //{
            //    sb.Append("<div>Booking No:" + lbBookingNo.Text + "</div> <div><br/></div> <div> Date of Booking: " + Convert.ToDateTime(lbBookinDate.Text).ToString("d MMMM, yyyy") + " </div> <div><br/> </div> <div> Dear Mr. " + lastName + ",</div> <div><br/></div><div> Namaskar! Greetings from ARC " + CompanyName + " ! </div> <div><br/> </div><div> Thank you for the payment of Rs." + lblTotPaid.Text + " .</div><div><br/> </div><div> Your booking is <strong>confirmed</strong>.</div><div><br/> </div><div>Request you to <a href=http://test.adventureresortscruises.in/Cruise/Masters/NewRegister.aspx?bid=" + Session["getbid"].ToString() + ">Click Here</a> to share your details to help us server you better.</div><div><br/>  </div></div> ");
            //}
            if (Session["geteamil"] != null && Session["geteamil"] != "")
            {
                if (Session["AgentMailId"] != null && Session["AgentMailId"] != "")
                {
                    sb.Append("<div>Booking No:" + lbBookingNo.Text + "</div> <div><br/></div> <div> Date of Booking: " + Convert.ToDateTime(lbBookinDate.Text).ToString("d MMMM, yyyy") + " </div> <div><br/> </div> <div> Dear Sir/Madam,</div> <div><br/></div><div> Namaskar! Greetings from " + CompanyName + " ! </div> <div><br/> </div><div> Thank you for the payment of Rs." + lblTotPaid.Text + " .</div><div><br/> </div><div> Your booking is <strong>confirmed</strong>.</div><div><br/> </div><div>Request you to <a href=http://test.adventureresortscruises.in/Cruise/Booking/agentLogin1.aspx?bid=" + Session["getbid"].ToString() + ">Click Here</a> to share your details to help us server you better.</div><div><br/>  </div></div> ");
                }
                // else if (Session["geteamil"] != null && Session["geteamil"] != "")
                else
                {
                    sb.Append("<div>Booking No:" + lbBookingNo.Text + "</div> <div><br/></div> <div> Date of Booking: " + Convert.ToDateTime(lbBookinDate.Text).ToString("d MMMM, yyyy") + " </div> <div><br/> </div> <div> Dear Mr. " + firstName + "  " + lastName + ",</div> <div><br/></div><div> Namaskar! Greetings from " + CompanyName + " ! </div> <div><br/> </div><div> Thank you for the payment of Rs." + lblTotPaid.Text + " .</div><div><br/> </div><div> Your booking is <strong>confirmed</strong>.</div><div><br/> </div><div>Request you to <a href=http://test.adventureresortscruises.in/Cruise/Masters/NewRegister.aspx?bid=" + Session["getbid"].ToString() + ">Click Here</a> to share your details to help us server you better.</div><div><br/>  </div></div> ");
                }
            }
            else
            {
                if (Session["AgentMailId"] != null && Session["AgentMailId"] != "")
                {
                    sb.Append("<div>Booking No:" + lbBookingNo.Text + "</div> <div><br/></div> <div> Date of Booking: " + Convert.ToDateTime(lbBookinDate.Text).ToString("d MMMM, yyyy") + " </div> <div><br/> </div> <div> Dear Sir/Madam,</div> <div><br/></div><div> Namaskar! Greetings from " + CompanyName + " ! </div> <div><br/> </div><div> Thank you for the payment of Rs." + lblTotPaid.Text + " .</div><div><br/> </div><div> Your booking is <strong>confirmed</strong>.</div><div><br/> </div><div>Request you to <a href=http://test.adventureresortscruises.in/Cruise/Booking/agentLogin1.aspx?bid=" + Session["bookingId"].ToString() + ">Click Here</a> to share your details to help us server you better.</div><div><br/>  </div></div> ");
                }
                // else if (Session["geteamil"] != null && Session["geteamil"] != "")
                else
                {
                    sb.Append("<div>Booking No:" + lbBookingNo.Text + "</div> <div><br/></div> <div> Date of Booking: " + Convert.ToDateTime(lbBookinDate.Text).ToString("d MMMM, yyyy") + " </div> <div><br/> </div> <div> Dear " + Session["InvName"].ToString() + ",</div> <div><br/></div><div> Namaskar! Greetings from " + CompanyName + " ! </div> <div><br/> </div><div> Thank you for the payment of Rs." + lblTotPaid.Text + " .</div><div><br/> </div><div> Your booking is <strong>confirmed</strong>.</div><div><br/> </div><div>Request you to <a href=http://test.adventureresortscruises.in/Cruise/Masters/NewRegister.aspx?bid=" + Session["bookingId"].ToString() + ">Click Here</a> to share your details to help us server you better.</div><div><br/>  </div></div> ");
                }
            }
            // sb.Append("<div>Booking No:" + lbBookingNo.Text + "</div> <div><br/></div> <div> Date of Booking: " + Convert.ToDateTime(lbBookinDate.Text).ToString("d MMMM, yyyy") + " </div> <div><br/> </div> <div> Dear Mr. " + lastName + ",</div> <div><br/></div><div> Namaskar! Greetings from ARC " + CompanyName + " ! </div> <div><br/> </div><div> Thank you for the payment of Rs." + lblTotPaid.Text + " .</div><div><br/> </div><div> Your booking is <strong>confirmed</strong>.</div><div><br/> </div><div>Request you to <a href=http://test.adventureresortscruises.in/Cruise/Masters/NewRegister.aspx?bid=" + Session["getbid"].ToString() + ">Click Here</a> to share your details to help us server you better.</div><div><br/>  </div></div> ");
            sb.Append(" <div>Best Wishes,</div> <div><br/>  </div> <div> The Mahabaahu Team!</div> ");
            sb.Append("</div>");
            sb.Append("<img src='" + CompanyLogo + "' alt='Image'/><br /><div> " + CompanyName + "</div><div> " + CompanyAddress + " </div> <div>Phone: " + CompanyPhoneNo + " </div><div> Mobile: " + CompanyMobile + " </div><div><br/> </div> ");

            // sb.Append("<div>The booking policy of the cruise can be referred to at <a href='http://www.mahabaahucruiseindia.com/cruise-policy' target='_blank' data-saferedirecturl='https://www.google.com/url?hl=en&amp;q=http://www.mahabaahucruiseindia.com/cruise-policy&amp;source=gmail&amp;ust=1470139247045000&amp;usg=AFQjCNH3vyzjL507K4FspRY6TihAfogUug'>http://www.mahabaahucruiseindia.com/cruise-policy </a>.</div><div><br/></div><div> Enclosure:</div><div> Invoice for your booking is attached with this email showing full payment received.</div> ");
            if (Session["AccomId"] != "" && Session["AccomId"] != null)
            {
                PopulateModule(Convert.ToInt32(Session["AccomId"]));
                string AccomPolicyUrl = ViewState["ModuleDt"].ToString();
                sb.Append("<div>The booking policy of the cruise can be referred to at <a href='" + AccomPolicyUrl + "' target='_blank' data-saferedirecturl='https://www.google.com/url?hl=en&amp;q=" + AccomPolicyUrl + "&amp;source=gmail&amp;ust=1470139247045000&amp;usg=AFQjCNH3vyzjL507K4FspRY6TihAfogUug'>" + AccomPolicyUrl + " </a>.</div><div><br/></div><div> Enclosure:</div><div> Invoice for your booking is attached with this email showing full payment received.</div> ");
            }
            else
            {
                sb.Append("<div>The booking policy of the cruise can be referred to at <a href='http://www.mahabaahucruiseindia.com/cruise-policy' target='_blank' data-saferedirecturl='https://www.google.com/url?hl=en&amp;q=http://www.mahabaahucruiseindia.com/cruise-policy&amp;source=gmail&amp;ust=1470139247045000&amp;usg=AFQjCNH3vyzjL507K4FspRY6TihAfogUug'>http://www.mahabaahucruiseindia.com/cruise-policy </a>.</div><div><br/></div><div> Enclosure:</div><div> Invoice for your booking is attached with this email showing full payment received.</div> ");
            }

            #endregion
            mail.IsBodyHtml = true;
            mail.Body = sb.ToString();
            // mail.CC.Add("reservations@adventureresortscruises.com");
            mail.Attachments.Add(new Attachment(Server.MapPath("inv/" + lbInvoiceNO.Text + "Invoice.pdf")));

            try
            {
                SmtpServer.Send(mail);
            }
            catch (Exception exp)
            {
                Console.WriteLine(exp.Message);
            }
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Showstatus", "javascript:alert('Payment Successfull, Please Print your Invoice, Booking Details have been sent to your e-mail.')", true);
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Showstatus", "javascript:alert('" + ex.Message.ToString() + "')", true);
        }
    }
    public void SendCancelledMail()
    {
        try
        {
            string packId = Session["PackageId"] != null ? Session["PackageId"].ToString() : string.Empty;
            string packageName = string.Empty;

            if (!string.IsNullOrEmpty(packId))
            {
                string sqlQuery = "SELECT [PackageName], (select LocationName from dbo.Locations where LocationId = tblPackages.BordingFrom) as 'BoardFrom', (select LocationName from dbo.Locations where LocationId = tblPackages.BoadingTo) as 'BoardTo', NoOfNights FROM [dbo].[tblPackages] where PackageId = '" + Session["PackageId"] + "'";
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["ReservationConnectionString"].ConnectionString);
                con.Open();
                SqlDataAdapter adp = new SqlDataAdapter(sqlQuery, con);
                DataTable dtGetPackageDetails = new DataTable();

                adp.Fill(dtGetPackageDetails);
                if (string.Compare(packageName, dtGetPackageDetails.Rows[0]["PackageName"].ToString(), true) != 0)
                {
                    packageName = dtGetPackageDetails.Rows[0]["PackageName"].ToString();
                }

                lbpackageName.Text = "Package: " + packageName + ", " + dtGetPackageDetails.Rows[0]["NoOfNights"].ToString() + " Nights";
                lbStrtEnd.Text = "Cruise starts from " + dtGetPackageDetails.Rows[0]["BoardFrom"].ToString() + "  and ends at " + dtGetPackageDetails.Rows[0]["BoardTo"].ToString();
                con.Close();
            }

            blsr.action = "getmaxbookingcode";
            DataTable dtbkcode = dlsr.GetMaxBookingId(blsr);

            string bref = Session["BookingRef"].ToString();
            string l = Session["InvName"].ToString();

            #region Preparing MailMessage
            MailMessage mail = new MailMessage();
            // mail.From = new MailAddress("reservations@adventureresortscruises.in", "ARC Reservations");
            mail.From = new MailAddress("" + CompanyEmail + "", "Reservations");
            string toEmailId = Session["AgentMailId"] != null ? Session["AgentMailId"].ToString() : Session["CustMailId"].ToString();

            if (Debugger.IsAttached)
                toEmailId = "rohit@farhorizonindia.com";

            if (string.IsNullOrEmpty(toEmailId))
                return;

            mail.To.Add(toEmailId);
            mail.CC.Add(ccEmail);
            mail.Subject = "Reservation - " + Session["InvName"].ToString() + " - " + dtbkcode.Rows[0][0].ToString() + " ";
            #endregion

            #region Email Body
            StringBuilder sb = new StringBuilder();
            if (Session["AgentMailId"] != null && Session["AgentMailId"] != "")
            {
                sb.Append("<div>Booking No : " + dtbkcode.Rows[0].ItemArray[0].ToString() + "</div>  <div> Date of Booking : " + Convert.ToDateTime(System.DateTime.Now).ToString("d MMMM, yyyy") + " </div> <div><br/> </div> <div> Dear Sir/Madam,</div> <div><br/></div><div> Namaskar! Greetings from " + CompanyName + "!</div> <div><br/> </div><div> Thank you for showing interest in Mahabaabu. <br /> You were trying to make a booking from " + lblArrvDate.Text + " to " + lblDepartDate.Text + " but no payment was processed against this booking. </div><div><br/></div> ");
            }
            else
            {
                sb.Append("<div>Booking No : " + dtbkcode.Rows[0].ItemArray[0].ToString() + "</div>  <div> Date of Booking : " + Convert.ToDateTime(System.DateTime.Now).ToString("d MMMM, yyyy") + " </div> <div><br/> </div> <div> Dear " + Session["InvName"].ToString() + ",</div> <div><br/></div><div> Namaskar! Greetings from " + CompanyName + "!</div> <div><br/> </div><div> Thank you for showing interest in Mahabaabu. <br /> You were trying to make a booking from " + lblArrvDate.Text + " to " + lblDepartDate.Text + " but no payment was processed against this booking. </div><div><br/></div> ");
            }
            sb.Append(" <div>Please do let us know if you need any assistance to help you with your travel arrangements. <div><br /></div><div>Best Wishes,</div><div><br/></div><div>The Mahabaahu Team!</div>");
            sb.Append("</div>");
            sb.Append("<img src='" + CompanyLogo + "' alt='Image'/><br /><div> " + CompanyName + "</div><div> " + CompanyAddress + " </div> <div> Phone: " + CompanyPhoneNo + " </div><div> Mobile: " + CompanyMobile + " </div><div><br/> </div> ");
            //sb.Append("<div>The booking policy of the cruise can be referred to at<a href='http://www.mahabaahucruiseindia.com/cruise-policy' target='_blank' data-saferedirecturl='https://www.google.com/url?hl=en&amp;q=http://www.mahabaahucruiseindia.com/cruise-policy&amp;source=gmail&amp;ust=1470139247045000&amp;usg=AFQjCNH3vyzjL507K4FspRY6TihAfogUug'>http://www.mahabaahucruiseindia.com/cruise-policy </a>.</div><div><br/></div>");
            if (Session["AccomId"] != "" && Session["AccomId"] != null)
            {
                PopulateModule(Convert.ToInt32(Session["AccomId"]));
                string AccomPolicyUrl = ViewState["ModuleDt"].ToString();
                sb.Append("<div>The booking policy of the cruise can be referred to at <a href='" + AccomPolicyUrl + "' target='_blank' data-saferedirecturl='https://www.google.com/url?hl=en&amp;q=" + AccomPolicyUrl + "&amp;source=gmail&amp;ust=1470139247045000&amp;usg=AFQjCNH3vyzjL507K4FspRY6TihAfogUug'>" + AccomPolicyUrl + " </a>.</div><div><br/></div>");
            }
            else
            {
                sb.Append("<div>The booking policy of the cruise can be referred to at <a href='http://www.mahabaahucruiseindia.com/cruise-policy' target='_blank' data-saferedirecturl='https://www.google.com/url?hl=en&amp;q=http://www.mahabaahucruiseindia.com/cruise-policy&amp;source=gmail&amp;ust=1470139247045000&amp;usg=AFQjCNH3vyzjL507K4FspRY6TihAfogUug'>http://www.mahabaahucruiseindia.com/cruise-policy </a>.</div><div><br/></div>");
            }
            #endregion
            mail.Headers.Add("Content-Type", "content=text/html; charset=\"UTF-8\"");
            mail.IsBodyHtml = true;
            mail.Body = sb.ToString();
            mail.BodyEncoding = System.Text.Encoding.UTF8;
            // mail.CC.Add("reservations@adventureresortscruises.com");

            try
            {
                SmtpServer.Send(mail);
            }
            catch (Exception exp)
            {
                Console.WriteLine(exp.Message);
            }
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Showstatus", "javascript:alert('Payment UnSuccessfull, Booking Details have been sent to your e-mail.')", true);
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Showstatus", "javascript:alert('" + ex.Message.ToString() + "')", true);
        }
    }
    private string getinvoice()
    {
        string catalies = "";
        Random getrand = new Random();
        if (Session["categoryAlias"] != null)
        {
            catalies = Session["categoryAlias"].ToString() + getrand.Next(10000, 90000);
        }
        else
        {
            catalies = "MVM" + getrand.Next(10000, 90000);
        }
        return catalies;
    }
    protected override void Render(HtmlTextWriter writer)
    {
        if (ViewState["sentMail"] == "0")
        {
            try
            {
                #region Generate Invoice
                Spire.Pdf.PdfDocument pdf = new Spire.Pdf.PdfDocument();
                PdfHtmlLayoutFormat htmlLayoutFormat = new PdfHtmlLayoutFormat();

                //webBrowser load html whether Waiting
                htmlLayoutFormat.IsWaiting = false;
                //page setting
                PdfPageSettings setting = new PdfPageSettings();

                setting.Size = PdfPageSize.A3;
                string pageSource;

                var sw1 = new StringWriter();
                var hw = new HtmlTextWriter(sw1);

                using (sw1)
                using (hw)
                {
                    base.Render(hw);
                    pageSource = sw1.ToString();
                }
                writer.Write(pageSource);

                string htmlCode = pageSource.ToString();
                //htmlCode = htmlCode.Replace("dwew", "none");
                //use single thread to generate the pdf from above html code
                Thread thread = new Thread(() =>
                { pdf.LoadFromHTML(htmlCode, false, setting, htmlLayoutFormat); });
                thread.SetApartmentState(ApartmentState.STA);
                thread.Start();
                thread.Join();


                // Save the file to PDF.
                //pdf.SaveToFile(rename(Server.MapPath("inv/" + lbInvoiceNO.Text + ".pdf")));
                pdf.SaveToFile(rename(Server.MapPath("inv/" + lbInvoiceNO.Text + "Invoice1.pdf")));


                //Launching the Pdf file.


                #endregion

                #region  ViewInvoice
                //View the PDF.
                int pageNumber = 1;
                PdfReader reader = new PdfReader(Server.MapPath("inv/" + lbInvoiceNO.Text + "Invoice1.pdf"));

                Rectangle cropbox = reader.GetCropBox(1);
                iTextSharp.text.Rectangle size = new iTextSharp.text.Rectangle(cropbox.Width - 0, cropbox.Height - 12);
                Document document = new Document(size);
                iTextSharp.text.pdf.PdfWriter writer1 = PdfWriter.GetInstance(document,
                new FileStream(Server.MapPath("inv/" + lbInvoiceNO.Text + "Invoice.pdf").Replace(lbInvoiceNO.Text + ".pdf", lbInvoiceNO.Text + "File.pdf"),
                FileMode.Create, FileAccess.Write));
                document.Open();
                PdfContentByte cb = writer1.DirectContent;
                document.NewPage();
                PdfImportedPage page = writer1.GetImportedPage(reader,
                pageNumber);
                cb.AddTemplate(page, 0, 0);
                document.Close();
                #endregion
                if (Session["getdata"] != null)
                {

                    string lkjh = Session["getpayid"].ToString();
                    //string kh = Session["balancamnt"].ToString();
                    Response.Write(lblTotPaid.Text);
                    decimal amount = Convert.ToDecimal(lblTotPaid.Text.Replace(",", ""));
                    int n1 = dlpack.updatepayment(lbBookingNo.Text, Session["getpayid"].ToString(), amount);
                    if (n1 == 2)
                    {


                        Sendfinalmail();
                    }

                }
                if (Session["getdata"] == null)
                {
                    if (Session["Hotel"] != null)
                    {
                        SendHotelMail();
                    }
                    else
                    {
                        if (Session["getPaid"] != null)
                        {
                            SendCruiseMailfor25();
                        }
                        else
                        {
                            Sendfinalmail();
                            //   SendCancelledMail();
                        }



                    }
                }

                Session["BookedRooms"] = null;
                Session["PackageId"] = null;
                Session["BookingRef"] = null;
                Session["getdata"] = null;
                Session.RemoveAll();
                Session.Abandon();
            }
            catch (Exception ex)
            { }
        }
    }

    public string rename(string fullpath)
    {
        int count = 1;

        string fileNameOnly = Path.GetFileNameWithoutExtension(fullpath);
        string extension = Path.GetExtension(fullpath);
        string path = Path.GetDirectoryName(fullpath);
        string newFullPath = fullpath;

        while (File.Exists(newFullPath))
        {
            string tempFileName = string.Format("{0}({1})", fileNameOnly, count++);
            newFullPath = Path.Combine(path, tempFileName + extension);
        }
        return newFullPath;
    }

    /// <summary>
    /// This method will move the booking from Proposed state to Booked state.
    /// As payment is confirmed. Else the booking will stay as proposed booking for back-office operations.
    /// </summary>
    private void UpdateCruiseBookingToBooked()
    {
        try
        {
            //BALBooking blsr = Session["tblBookingBAL"] as BALBooking;
            BALBooking blsr = SessionServices.RetrieveSession<BALBooking>("tblBookingBAL");
            //BALBooking blsr = Session["tblBookingBAL"] as BALBooking;
            DALBooking dlr = new DALBooking();

            dlr.UpdateBookingStatus(blsr._iBookingId, BookingStatusTypes.BOOKED);
            SetPaymentDetails(blsr);
            dlr.UpdatePaymentDetails(blsr);
        }
        catch
        {

        }
    }

    /// <summary>
    /// This method will move the booking from Proposed state to Booked state.
    /// As payment is confirmed. Else the booking will stay as proposed booking for back-office operations.
    /// </summary>
    private void UpdateHotelBookingToBooked()
    {
        try
        {
            //BALBooking blsr = Session["tblBookingBAL"] as BALBooking;
            BALBooking blsr = SessionServices.RetrieveSession<BALBooking>("tblBookingBAL");
            // BALBooking blsr = Session["tblBookingBAL"] as BALBooking;
            DALBooking dlr = new DALBooking();
            dlr.UpdateBookingStatus(blsr._iBookingId, BookingStatusTypes.BOOKED);

            SetPaymentDetails(blsr);
            dlr.UpdatePaymentDetails(blsr);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    private void SetPaymentDetails(BALBooking blsr)
    {
        string amount = Request.Params.Get("AMOUNT");
        double paidAmount;
        double.TryParse(amount.Trim(), out paidAmount);

        blsr._PaidAmount = paidAmount;
        blsr.PaymentId = Session["BookingPayId"] != null ? Session["BookingPayId"].ToString() : "NO_PAYMENT_ID";
    }

    private void ShowCruiseBookingDetails(BALBooking bookingDetail)
    {
        lbBookingNo.Text = bookingDetail.BookingCode;
        //lbInvoiceNO.Text = Convert.ToString(bookingDetail._iBookingId);
        DataTable GridRoomPaxDetail = SessionServices.RetrieveSession<DataTable>("BookedRooms");
        // DataTable GridRoomPaxDetail = Session["BookedRooms"] as DataTable;
        Session["totroom"] = 1;
        gdvCruiseRooms.DataSource = GridRoomPaxDetail;
        gdvCruiseRooms.DataBind();
        // hidecolumns();
        string kjh = Session["getcruiseinvoice"].ToString();
        //Label3.Text = "INR " + Convert.ToInt32(Session["getcruiseinvoice"].ToString().Split('R')[1]).ToString("##,0");
        Label3.Text = Convert.ToInt32(Session["getcruiseinvoice"].ToString().Split('R')[1]).ToString("##,0");
        lblacm.Text = "M V Mahabaahu";
        lblVessel.Text = "Vessel: ";
        lbPax.Text = Convert.ToInt32(GridRoomPaxDetail.Compute("SUM(Pax)", string.Empty)).ToString();
        //    lblTotoAmt.Text = Convert.ToInt32(GridRoomPaxDetail.Compute("SUM(Price)", string.Empty)).ToString();

        lblArrvDate.Text = bookingDetail._dtStartDate.ToString("d MMMM, yyyy");
        lblDepartDate.Text = bookingDetail._dtEndDate.ToString("d MMMM, yyyy");
        hfBookingId.Value = bookingDetail._iBookingId.ToString();
    }
    private double caclculatetotal(DataTable dt)
    {
        double tot = 0;
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            tot = tot + Convert.ToDouble(dt.Rows[i]["Inclusivetax"].ToString());
        }
        return tot;
    }
    private double caclculatetotalFinal(DataTable dt)
    {
        double tot = 0;
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            tot = tot + Convert.ToDouble(dt.Rows[i]["Price"].ToString());
        }
        return tot;
    }
    private double caclculateTax(DataTable dt)
    {
        double tot = 0;
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            tot = tot + Convert.ToDouble(dt.Rows[i]["TaxAmount"].ToString());
        }
        return tot;
    }
    private double caclculatetotalforcruise(DataTable dt)
    {
        double tot = 0;
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            tot = tot + (Convert.ToDouble(dt.Rows[i]["pricewithouttax"].ToString()) * Convert.ToDouble(dt.Rows[i]["Pax"].ToString()));
        }
        return tot;
    }
    private double caclculategrossforcruise(DataTable dt)
    {
        double tot = 0;
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            tot = tot + (Convert.ToDouble(dt.Rows[i]["Totalprice"].ToString().Replace(",", "")));
        }
        return tot;
    }
    private double caclculategrossforHotel(DataTable dt)
    {
        double tot = 0;
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            tot = tot + (Convert.ToDouble(dt.Rows[i]["Price"].ToString().Replace(",", "")));
        }
        return tot;
    }
    private double caclculatediscountforcruise(DataTable dt)
    {
        double tot = 0;
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            tot = tot + (Convert.ToDouble(dt.Rows[i]["Discountprice"].ToString().Replace(",", "")));
        }
        return tot;
    }

    private double caclculategrandtotalforcruise(DataTable dt)
    {
        double tot = 0;
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            tot = tot + Convert.ToDouble(dt.Rows[i]["pricewithouttax"].ToString());
        }
        return tot;
    }
    private void ShowHotelBookingDetails(BALBooking booking)
    {
        try
        {
            double tax = 0;
            lbBookingNo.Text = booking.BookingCode;
            hfBookingId.Value = booking._iBookingId.ToString();

            lblArrvDate.Text = Convert.ToDateTime(Session["Chkin"]).ToString("d MMMM, yyyy");
            lblDepartDate.Text = Convert.ToDateTime(Session["chkout"]).ToString("d MMMM, yyyy");
            lblacm.Text = "Accomodation Name: " + Session["AccomName"].ToString();

            DataTable Bookingdt = SessionServices.RetrieveSession<DataTable>("Bookingdt");
            // DataTable Bookingdt = Session["Bookingdt"] as DataTable;
            if (Bookingdt != null && Bookingdt.Rows.Count > 0)
            {
                for (int i = 0; i < Bookingdt.Rows.Count; i++)
                {
                    Bookingdt.Rows[i]["Price"] = Bookingdt.Rows[i]["Price"].ToString();
                    Bookingdt.Rows[i]["Inclusivetax"] = Bookingdt.Rows[i]["Inclusivetax"].ToString();
                    tax = tax + Convert.ToDouble(Bookingdt.Rows[i]["Tax"].ToString());
                }

            }
            gdvSelectedRooms.DataSource = Bookingdt;
            gdvSelectedRooms.DataBind();

            //gdvSelectedRooms.FooterRow.Cells[2].Text = "Service Tax @ " + Session["Taxpax"].ToString() + "%" + " </r> <b> Grand Total </b> ";
            //Label3.Text = "INR " + Convert.ToInt32(tax.ToString()).ToString("##,0");
            Label3.Text = Convert.ToInt32(tax.ToString()).ToString("##,0");
            double totalAmount = Math.Round(Convert.ToDouble(Bookingdt.Compute("SUM(Total1)", "[Total1] > 0")));

            double totalAmount1 = caclculatetotal(Bookingdt);
            //lblgetTotal.Text = "INR " + Convert.ToInt32(totalAmount.ToString()).ToString("##,0");
            //lblGetGrandTotal.Text = "INR " + Convert.ToInt32(totalAmount1.ToString()).ToString("##,0");
            lblgetTotal.Text = Convert.ToInt32(totalAmount.ToString()).ToString("##,0");
            lblGetGrandTotal.Text = Convert.ToInt32(totalAmount1.ToString()).ToString("##,0");
            string amountText = string.Format("{0}<br>{1}<br>{2}",
                    totalAmount.ToString("#.##"),
                    " ",
                    totalAmount1.ToString("#.##"));
            //gdvSelectedRooms.FooterRow.Cells[3].Text = amountText;
            Label2.Text = "Grand Total";
            Label1.Text = "@" + Session["Taxpax"].ToString() + "%";
            //gdvSelectedRooms.FooterRow.Cells[3].Text = Math.Round(Convert.ToDouble(Bookingdt.Compute("SUM(Total1)", "[Total1] > 0"))).ToString("#.##") + " </br> " + Math.Round((4.5 * (Convert.ToInt32(Bookingdt.Compute("SUM(Total1)", "[Total1] > 0")) / 100))).ToString("#.##") + " </br> " + Math.Round((Convert.ToDouble(Bookingdt.Compute("SUM(Total1)", "[Total1] > 0")) + (4.5 * (Convert.ToInt32(Bookingdt.Compute("SUM(Total1)", "[Total1] > 0")) / 100)))).ToString("#.##") + " ";

            lbPax.Text = Convert.ToInt32(Bookingdt.Compute("SUM(Pax)", "[Pax] > 0")).ToString(); ;
            //lblTotAMt.Text = Math.Round((Convert.ToDouble(Bookingdt.Compute("SUM(Total1)", "[Total1] > 0")) + (4.5 * (Convert.ToInt32(Bookingdt.Compute("SUM(Total1)", "[Total1] > 0")) / 100)))).ToString("#.##");

            lblTotAMt.Text = Convert.ToInt32(totalAmount1.ToString("#.##")).ToString("##,0");

            double balanceAmount = Math.Round((Convert.ToDouble(lblTotAMt.Text.Replace(",", "")) - Convert.ToDouble(lblTotPaid.Text.Replace(",", ""))));
            if (balanceAmount > 0)
            {
                lblBalance.Text = Math.Round((Convert.ToDouble(lblTotAMt.Text.Replace(",", "")) - Convert.ToDouble(lblTotPaid.Text.Replace(",", "")))).ToString("#.##");
                lblBalance.Text = Convert.ToInt32(lblBalance.Text).ToString("##,0");
                lbBalanceDueIn.Text = Convert.ToDateTime(lblArrvDate.Text).AddDays(-30).ToString("d MMMM, yyyy");
            }
            else
            {
                lblBalance.Text = "0";
                lbBalanceDueIn.Text = string.Empty;
                lblBalanceDueOn.Visible = false;
            }
        }
        catch (Exception ex)
        { }
    }

    public void hidecolumns()
    {
        try
        {
            foreach (GridViewRow grow in gdvCruiseRooms.Rows)
            {
                grow.Cells[0].Visible = false;
                grow.Cells[1].Visible = false;
            }
        }
        catch
        {
        }
    }

    public string CRCCode(String ClearString, String key, string TRANSACTIONSTATUS, string APTRANSACTIONID, string MESSAGE, string TRANSACTIONID, string AMOUNT)
    {
        try
        {
            UInt32 Output = 0;
            UInt32 Output1 = 0;
            String hash = String.Empty;

            if (!Debugger.IsAttached)
            {
                Crc32 crc32 = new Crc32();
                byte[] mybytes = Encoding.UTF8.GetBytes(ClearString);
                foreach (byte b in crc32.ComputeHash(mybytes)) hash += b.ToString("x2");
                Output = UInt32.Parse(hash, System.Globalization.NumberStyles.HexNumber);
                Output1 = UInt32.Parse(key);
            }

            if (Output1 == Output)
            {
                if (Session["Hotel"] != null)
                {
                    lblTotPaid.Text = Convert.ToDouble(AMOUNT).ToString("#.##");
                    lblTotPaid.Text = Convert.ToInt32(lblTotPaid.Text).ToString("##,0");
                    //BALBooking blsr = Session["tblBookingBAL"] as BALBooking;
                    BALBooking blsr = SessionServices.RetrieveSession<BALBooking>("tblBookingBAL");
                    // BALBooking blsr = Session["tblBookingBAL"] as BALBooking;

                    ShowHotelBookingDetails(blsr);

                    GenrateHotelBill(TRANSACTIONID);
                    int QueryResponse = AddTransactionDetails(TRANSACTIONSTATUS, APTRANSACTIONID, TRANSACTIONID, AMOUNT);
                    if (QueryResponse > 0)
                    {
                        // lbBookingNo.Text = TRANSACTIONID.ToString();
                        //FiilPackage();
                    }
                }
                else
                {
                    lblTotPaid.Text = Convert.ToDouble(AMOUNT).ToString("#.##"); ;
                    lblTotPaid.Text = Convert.ToInt32(lblTotPaid.Text).ToString("##,0");
                    #region Show The Details
                    //BALBooking blsr = Session["tblBookingBAL"] as BALBooking;

                    BALBooking blsr = SessionServices.RetrieveSession<BALBooking>("tblBookingBAL");
                    //BALBooking blsr = Session["tblBookingBAL"] as BALBooking;
                    DALBooking dlr = new DALBooking();

                    BALBooking bookingDetail = dlr.GetBookingDetails(blsr._iBookingId);
                    ShowCruiseBookingDetails(bookingDetail);
                    #endregion


                    GenerateCruiseBill(TRANSACTIONID);

                    int QueryResponse = AddTransactionDetails(TRANSACTIONSTATUS, APTRANSACTIONID, TRANSACTIONID, AMOUNT);
                    if (QueryResponse > 0)
                    {
                        // lbBookingNo.Text = TRANSACTIONID.ToString();
                        FiilPackage();
                    }
                    DataTable GridRoomPaxDetail = SessionServices.RetrieveSession<DataTable>("BookedRooms");
                    //DataTable GridRoomPaxDetail = Session["BookedRooms"] as DataTable;

                    double totalAmount = Math.Round(Convert.ToDouble(GridRoomPaxDetail.Compute("SUM(Price)", string.Empty)));
                    double totalAmount1 = caclculatetotalforcruise(GridRoomPaxDetail); /*Math.Round(Convert.ToDouble(GridRoomPaxDetail.Compute("SUM(pricewithouttax)", string.Empty)));*/
                    double totdiscount = caclculatediscountforcruise(GridRoomPaxDetail); /*Math.Round(Convert.ToDouble(GridRoomPaxDetail.Compute("SUM(pricewithouttax)", string.Empty)));*/
                    double totagross = caclculategrossforcruise(GridRoomPaxDetail); /*Math.Round(Convert.ToDouble(GridRoomPaxDetail.Compute("SUM(pricewithouttax)", string.Empty)));*/
                                                                                    //lblgetTotal.Text = "INR " + Convert.ToInt32(totalAmount1.ToString()).ToString("##,0");
                                                                                    //lblTaxableAmount.Text = "INR " + Convert.ToInt32((totalAmount1 - totdiscount).ToString()).ToString("##,0");

                    lblgetTotal.Text = Convert.ToInt32(totalAmount1.ToString()).ToString("##,0");
                   // lblTaxableAmount.Text = Convert.ToInt32((totalAmount1 - totdiscount).ToString()).ToString("##,0");

                    //gdvCruiseRooms.FooterRow.Cells[2].Text = "Total <br> Service Tax @ 4.50% <br> <b> Grand Total </b>";
                    Label1.Text = Session["gettax"].ToString() + "%";
                    string amountText = string.Format("{0}<br>{1}<br>{2}",

                        totalAmount.ToString("#.##"),
                        " ",
                        totalAmount.ToString("#.##"));
                    //Label7.Text = "Taxable Amount";
                    if (totdiscount == 0)
                    {
                        Label4.Text = " ";
                        lblDiscount.Text = " ";
                    }
                    else
                    {
                        Label4.Text = "Discount(" + Session["getdiscountvalue"].ToString() + "%" + ")";
                        //lblDiscount.Text = "INR " + Convert.ToInt32(totdiscount).ToString("##,0");
                        lblDiscount.Text = Convert.ToInt32(totdiscount).ToString("##,0");
                    }

                    //lblGross.Text = "INR " + Convert.ToInt32(totagross).ToString("##,0");
                    lblGross.Text = Convert.ToInt32(totagross).ToString("##,0");
                    //gdvCruiseRooms.FooterRow.Cells[3].Text = amountText;

                    //gdvCruiseRooms.FooterRow.Cells[3].Text = Math.Round(Convert.ToDouble(GridRoomPaxDetail.Compute("SUM(Price)", string.Empty))).ToString("#.##") + "</br> " + Math.Round((4.5 * (Convert.ToInt32(GridRoomPaxDetail.Compute("SUM(Price)", string.Empty)) / 100))).ToString("#.##") + " </br> " + Math.Round((Convert.ToDouble(GridRoomPaxDetail.Compute("SUM(Price)", string.Empty)) + (4.5 * (Convert.ToInt32(GridRoomPaxDetail.Compute("SUM(Price)", string.Empty)) / 100)))).ToString("#.##");
                    //lblTotAMt.Text = Math.Round((Convert.ToDouble(GridRoomPaxDetail.Compute("SUM(Price)", string.Empty)) + (4.5 * (Convert.ToInt32(GridRoomPaxDetail.Compute("SUM(Price)", string.Empty)) / 100)))).ToString("#.##");

                    Label6.Text = "Invoice Amount";
                    lblTotAMt.Text = totalAmount.ToString("#.##");
                    lblTotAMt.Text = Convert.ToInt32(lblTotAMt.Text).ToString("##,0");
                    double balanceAmount = Math.Round((Convert.ToDouble(lblGross.Text.Replace(",", "")) - Convert.ToDouble(lblTotPaid.Text.Replace(",", ""))));
                    if (balanceAmount > 0)
                    {
                        lblBalance.Text = Math.Round((Convert.ToDouble(lblGross.Text.Replace(",", "")) - Convert.ToDouble(lblTotPaid.Text.Replace(",", "")))).ToString("#.##");
                        lblBalance.Text = Convert.ToInt32(lblBalance.Text).ToString("##,0");
                        lbBalanceDueIn.Text = Convert.ToDateTime(lblArrvDate.Text).AddDays(-90).ToString("d MMMM, yyyy");
                    }
                    else
                    {
                        lblBalance.Text = "0";
                        lbBalanceDueIn.Text = string.Empty;
                        lblBalanceDueOn.Visible = false;
                    }
                }
                try
                {

                    //  lbRuppeeinwords.Text = GF.NumbersToWords(Convert.ToInt32(lblGross.Text.Split('R')[1].Replace(",", "")));
                    //lbRuppeeinwords.Text = GF.NumbersToWords(Convert.ToInt32(lblGross.Text));
                    lbRuppeeinwords.Text = GF.NumbersToWords(Convert.ToInt32(lblGross.Text.Replace(",", ""))); 
                }
                catch
                {
                    //  lbRuppeeinwords.Text = GF.NumbersToWords(Convert.ToInt32(lblGetGrandTotal.Text.Split('R')[1].Replace(",", "")));
                    // lbRuppeeinwords.Text = GF.NumbersToWords(Convert.ToInt32(lblGetGrandTotal.Text));
                    lbRuppeeinwords.Text = GF.NumbersToWords(Convert.ToInt32(lblGetGrandTotal.Text.Replace(",", "")));
                }
            }
            else
            {
                Response.Write("Secure Hash mismatch.");
            }

            return hash;
        }
        catch (Exception ex)
        {
            return null;
        }
    }

    private int AddTransactionDetails(string TRANSACTIONSTATUS, string APTRANSACTIONID, string TRANSACTIONID, string AMOUNT)
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["ReservationConnectionString"].ConnectionString);
        con.Open();
        SqlCommand cmd = new SqlCommand("insert into tblTransactionDetails values('" + hfBookingId.Value + "', '" + TRANSACTIONID + "','" + APTRANSACTIONID + "' , '" + Convert.ToDecimal(AMOUNT) + "','" + TRANSACTIONSTATUS + "','" + System.DateTime.Now + "')", con);
        int QueryResponse = cmd.ExecuteNonQuery();
        con.Close();

        return QueryResponse;
    }

    private void FiilPackage()
    {
        try
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["ReservationConnectionString"].ConnectionString);
            con.Open();
            string sqlQuery = "SELECT [PackageName], (select LocationName from dbo.Locations where LocationId = tblPackages.BordingFrom) as 'BoardFrom', (select LocationName from dbo.Locations where LocationId = tblPackages.BoadingTo) as 'BoardTo' FROM [dbo].[tblPackages] where PackageId = '" + Session["PackageId"] + "'";
            SqlDataAdapter adp = new SqlDataAdapter(sqlQuery, con);
            DataTable dtGetPackageDetails = new DataTable();
            adp.Fill(dtGetPackageDetails);
            lbpackageName.Text = dtGetPackageDetails.Rows[0]["PackageName"].ToString();
            lbStrtEnd.Text = "Cruise starts from " + dtGetPackageDetails.Rows[0]["BoardFrom"].ToString() + "  and ends at " + dtGetPackageDetails.Rows[0]["BoardTo"].ToString();

            con.Close();
        }
        catch (Exception sqe)
        {

        }
    }

    private void GenerateCruiseBill(string transactionId)
    {
        ReleaseBookingLock();
        UpdateCruiseBookingToBooked();
        //sendMail(transactionId);
    }

    private void GenrateHotelBill(string transactionId)
    {
        ReleaseBookingLock();
        UpdateHotelBookingToBooked();
        //sendMail1(transactionId);
    }

    public void SendHotelMail()
    {
        try
        {
            int noofpx = 0;
            for (int n = 0; n < gdvSelectedRooms.Rows.Count; n++)
            {
                noofpx += Convert.ToInt32(gdvSelectedRooms.Rows[n].Cells[1].Text);
            }

            blsr.action = "getmaxbookingcode";
            DataTable dtbkcode = dlsr.GetMaxBookingId(blsr);

            MailMessage mail = new MailMessage();
            //mail.From = new MailAddress("reservations@adventureresortscruises.in", "ARC Reservations");

            mail.From = new MailAddress("" + CompanyEmail + "", "Reservations");

            string toEmailId = Session["AgentMailId"] != null ? Session["AgentMailId"].ToString() : Session["CustMailId"].ToString();

            if (Debugger.IsAttached)
                toEmailId = "rohit@farhorizonindia.com";

            if (string.IsNullOrEmpty(toEmailId))
                return;

            mail.To.Add(toEmailId);
            mail.CC.Add(ccEmail);
            mail.Subject = "Reservation - " + Session["InvName"].ToString() + " - " + dtbkcode.Rows[0].ItemArray[0].ToString() + "";

            #region Email Body
            StringBuilder sb = new StringBuilder();
            sb.Append("<div>");
            if (Session["AgentMailId"] != null && Session["AgentMailId"] != "")
            {
                sb.Append("<div> Dear Sir/madam,</div> <div><br/></div><div>Greetings from " + Session["AccomName"].ToString() + "! </div> <div><br/> </div><div>Thank you for booking with us.</div> <div><br/></div> ");
            }
            else
            {
                sb.Append("<div> Dear " + Session["InvName"].ToString() + ",</div> <div><br/></div><div>Greetings from " + Session["AccomName"].ToString() + "! </div> <div><br/> </div><div>Thank you for booking with us.</div> <div><br/></div> ");
            }
            sb.Append(" <div>Check In :" + lblArrvDate.Text + " </div> <div></div> <div>Check Out :" + lblDepartDate.Text + "  </div>  <div><br/> </div>  <div><b>Accommodation Details</b></div> <div>  </div> <div> Number of Rooms: " + gdvSelectedRooms.Rows.Count.ToString() + "</div> <div> </div> <div>Number of Persons: " + noofpx + " </div> ");
            sb.Append("<div><br/></div><div>We look forward to having you with us.</div><div><br/></div><div>With best wishes,</div><div><br/></div>");

            sb.Append("<div>" + Session["AccomName"].ToString() + " Team <div><div><br/><div>");

            sb.Append("</div>");
            sb.Append("<div><br/><div><div>Enclosure: Invoice for your booking is attached with this email.<div>");
            #endregion

            mail.IsBodyHtml = true;
            mail.Body = sb.ToString();
            //mail.CC.Add("reservations@adventureresortscruises.com");
            mail.Attachments.Add(new Attachment(Server.MapPath("inv/" + lbInvoiceNO.Text + "Invoice.pdf")));

            try
            {
                SmtpServer.Send(mail);
            }
            catch (Exception exp)
            {
                Console.WriteLine(exp.Message);
            }
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Showstatus", "javascript:alert('Payment Successfull, Please Print your Invoice, Booking Details have been sent to your e-mail.')", true);
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Showstatus", "javascript:alert('" + ex.Message.ToString() + "')", true);
        }
    }
    public void SendCruiseMailfor25()
    {
        try
        {
            string packId = Session["PackageId"] != null ? Session["PackageId"].ToString() : string.Empty;
            string packageName = string.Empty;

            if (!string.IsNullOrEmpty(packId))
            {
                string sqlQuery = "SELECT [PackageName], (select LocationName from dbo.Locations where LocationId = tblPackages.BordingFrom) as 'BoardFrom', (select LocationName from dbo.Locations where LocationId = tblPackages.BoadingTo) as 'BoardTo', NoOfNights FROM [dbo].[tblPackages] where PackageId = '" + Session["PackageId"] + "'";
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["ReservationConnectionString"].ConnectionString);
                con.Open();
                SqlDataAdapter adp = new SqlDataAdapter(sqlQuery, con);
                DataTable dtGetPackageDetails = new DataTable();

                adp.Fill(dtGetPackageDetails);
                if (string.Compare(packageName, dtGetPackageDetails.Rows[0]["PackageName"].ToString(), true) != 0)
                {
                    packageName = dtGetPackageDetails.Rows[0]["PackageName"].ToString();
                }

                lbpackageName.Text = "Package: " + packageName + ", " + dtGetPackageDetails.Rows[0]["NoOfNights"].ToString() + " Nights";
                lbStrtEnd.Text = "Cruise starts from " + dtGetPackageDetails.Rows[0]["BoardFrom"].ToString() + "  and ends at " + dtGetPackageDetails.Rows[0]["BoardTo"].ToString();
                con.Close();
            }

            blsr.action = "getmaxbookingcode";
            DataTable dtbkcode = dlsr.GetMaxBookingId(blsr);

            string bref = Session["BookingRef"].ToString();
            string l = Session["SubInvName"].ToString();

            #region Preparing MailMessage
            MailMessage mail = new MailMessage();
            // mail.From = new MailAddress("reservations@adventureresortscruises.in", "ARC Reservations");
            mail.From = new MailAddress("" + CompanyEmail + "", "Reservations");
            string toEmailId = Session["AgentMailId"] != null ? Session["AgentMailId"].ToString() : Session["CustMailId"].ToString();

            if (Debugger.IsAttached)
                toEmailId = "rohit@farhorizonindia.com";

            if (string.IsNullOrEmpty(toEmailId))
                return;

            mail.To.Add(toEmailId);
            mail.CC.Add(ccEmail);
            if (Session["AgentMailId"] != null && Session["AgentMailId"] != "")
            {
                mail.Subject = "Reservation - " + Session["SubInvName"].ToString() + " - " + dtbkcode.Rows[0][0].ToString() + " ";
            }
            else
            {
                mail.Subject = "Reservation - " + Session["InvName"].ToString() + " - " + dtbkcode.Rows[0][0].ToString() + " ";
            }
            #endregion

            #region Email Body
            StringBuilder sb = new StringBuilder();
            if (Session["AgentMailId"] != null && Session["AgentMailId"] != "")
            {
                sb.Append("<div>Booking No : " + dtbkcode.Rows[0].ItemArray[0].ToString() + "</div>  <div> Date of Booking : " + Convert.ToDateTime(System.DateTime.Now).ToString("d MMMM, yyyy") + " </div> <div><br/> </div> <div> Dear Sir/Madam,</div> <div><br/></div><div> Namaskar! Greetings from  " + CompanyName + "!</div> <div><br/> </div><div> Thank you for booking " + packageName + " from " + lblArrvDate.Text + " to " + lblDepartDate.Text + ". We are in receipt of 25% Payment amounting to Indian Rupees " + Session["getPaid"].ToString() + ".</div><div><br/></div> ");
            }
            else
            {
                sb.Append("<div>Booking No : " + dtbkcode.Rows[0].ItemArray[0].ToString() + "</div>  <div> Date of Booking : " + Convert.ToDateTime(System.DateTime.Now).ToString("d MMMM, yyyy") + " </div> <div><br/> </div> <div> Dear " + Session["InvName"].ToString() + ",</div> <div><br/></div><div> Namaskar! Greetings from  " + CompanyName + "!</div> <div><br/> </div><div> Thank you for booking " + packageName + " from " + lblArrvDate.Text + " to " + lblDepartDate.Text + ". We are in receipt of 25% Payment amounting to Indian Rupees " + Session["getPaid"].ToString() + ".</div><div><br/></div> ");
            }
            sb.Append(" <div>The cruise showcases the Living, Natural and Cultural History of where silk and cotton; vie for attention. A cup of famous Assam tea invites you over to the little known north eastern region of India.</div><div><br/> </div><div> This pristine destination unfolds the history of an ancient civilisation of the Tibeto-Burman Ahoms who reigned in the region for more than 600 years. The river Brahmaputra takes you to the heart of the simplistic ways of a speckled tribal and multiracial life.</div><div><br/></div><div>The UNESCO World Heritage Site of Kaziranga beckons you to experience the wild life on elephant back, by Jeep Safari and an exclusive boat safari.  Majuli, the Cultural stronghold of the Neo-Vaishnav philosophy unfolds the rich and unique art forms.</div><div><br/></div><div>We sail amidst mighty sandbars carved afresh each year and walk on spotless silver sands. No two sails on; this third largest and one of the highest silting rivers of the world, are ever identical.  The River is never short of surprises!</div><div><br/></div><div>We take this opportunity to inform you of the following:<strong>The final payment</strong> should be with us <strong>90 days prior to </strong>the date of ships departure.You will receive an <strong>automated reminder</strong> 110 day and another 100 days prior to the date of ships departure.Please ignore if paid.</div><div><br/></div><div><strong>After the confirmation mail on full and final payment</strong> you are expect to send the <strong>Guest Details any time 70 days prior</strong> so that we are aware of your preferences and some medical history and can help make your experience interesting and safe.</div><div><br/></div><div>We look forward to you cruising with us on MV Mahabaahu.</div><div><br/></div><div>Appreciations!</div><div><br/></div><div>The Mahabaahu Team!</div>");
            sb.Append("</div>");
            sb.Append("<img src='" + CompanyLogo + "' alt='Image'/><br /><div> " + CompanyName + "</div><div> " + CompanyAddress + " </div> <div>Phone : " + CompanyPhoneNo + " </div><div> Mobile: " + CompanyMobile + " </div><div><br/> </div> ");
            //sb.Append("<div>The booking policy of the cruise can be referred to at<a href='http://www.mahabaahucruiseindia.com/cruise-policy' target='_blank' data-saferedirecturl='https://www.google.com/url?hl=en&amp;q=http://www.mahabaahucruiseindia.com/cruise-policy&amp;source=gmail&amp;ust=1470139247045000&amp;usg=AFQjCNH3vyzjL507K4FspRY6TihAfogUug'>http://www.mahabaahucruiseindia.com/cruise-policy </a>.</div><div><br/></div><div> Enclosure:</div><div> Invoice for your booking is attached with this email.</div>");
            if (Session["AccomId"] != "" && Session["AccomId"] != null)
            {
                PopulateModule(Convert.ToInt32(Session["AccomId"]));
                string AccomPolicyUrl = ViewState["ModuleDt"].ToString();
                sb.Append("<div>The booking policy of the cruise can be referred to at <a href='" + AccomPolicyUrl + "' target='_blank' data-saferedirecturl='https://www.google.com/url?hl=en&amp;q=" + AccomPolicyUrl + "&amp;source=gmail&amp;ust=1470139247045000&amp;usg=AFQjCNH3vyzjL507K4FspRY6TihAfogUug'>" + AccomPolicyUrl + " </a>.</div><div><br/></div><div> Enclosure:</div><div> Invoice for your booking is attached with this email.</div>");
            }
            else
            {
                sb.Append("<div>The booking policy of the cruise can be referred to at <a href='http://www.mahabaahucruiseindia.com/cruise-policy' target='_blank' data-saferedirecturl='https://www.google.com/url?hl=en&amp;q=http://www.mahabaahucruiseindia.com/cruise-policy&amp;source=gmail&amp;ust=1470139247045000&amp;usg=AFQjCNH3vyzjL507K4FspRY6TihAfogUug'>http://www.mahabaahucruiseindia.com/cruise-policy </a>.</div><div><br/></div><div> Enclosure:</div><div> Invoice for your booking is attached with this email.</div>");
            }

            #endregion
            mail.Headers.Add("Content-Type", "content=text/html; charset=\"UTF-8\"");
            mail.IsBodyHtml = true;
            mail.Body = sb.ToString();
            mail.BodyEncoding = System.Text.Encoding.UTF8;
            mail.CC.Add(ccEmail);
            //   mail.CC.Add("reservations@adventureresortscruises.com");
            mail.Attachments.Add(new Attachment(Server.MapPath("inv/" + lbInvoiceNO.Text + "Invoice.pdf")));

            try
            {
                SmtpServer.Send(mail);
            }
            catch (Exception exp)
            {
                Console.WriteLine(exp.Message);
            }
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Showstatus", "javascript:alert('Payment Successfull, Please Print your Invoice, Booking Details have been sent to your e-mail.')", true);
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Showstatus", "javascript:alert('" + ex.Message.ToString() + "')", true);
        }
    }

    public void SendCruiseMail()
    {
        try
        {
            string packId = Session["PackageId"] != null ? Session["PackageId"].ToString() : string.Empty;
            string packageName = string.Empty;

            if (!string.IsNullOrEmpty(packId))
            {
                string sqlQuery = "SELECT [PackageName], (select LocationName from dbo.Locations where LocationId = tblPackages.BordingFrom) as 'BoardFrom', (select LocationName from dbo.Locations where LocationId = tblPackages.BoadingTo) as 'BoardTo', NoOfNights FROM [dbo].[tblPackages] where PackageId = '" + Session["PackageId"] + "'";
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["ReservationConnectionString"].ConnectionString);
                con.Open();
                SqlDataAdapter adp = new SqlDataAdapter(sqlQuery, con);
                DataTable dtGetPackageDetails = new DataTable();

                adp.Fill(dtGetPackageDetails);
                if (string.Compare(packageName, dtGetPackageDetails.Rows[0]["PackageName"].ToString(), true) != 0)
                {
                    packageName = dtGetPackageDetails.Rows[0]["PackageName"].ToString();
                }

                lbpackageName.Text = "Package: " + packageName + ", " + dtGetPackageDetails.Rows[0]["NoOfNights"].ToString() + " Nights";
                lbStrtEnd.Text = "Cruise starts from " + dtGetPackageDetails.Rows[0]["BoardFrom"].ToString() + "  and ends at " + dtGetPackageDetails.Rows[0]["BoardTo"].ToString();
                con.Close();
            }

            blsr.action = "getmaxbookingcode";
            DataTable dtbkcode = dlsr.GetMaxBookingId(blsr);

            string bref = Session["BookingRef"].ToString();
            string l = Session["SubInvName"].ToString();

            #region Preparing MailMessage
            MailMessage mail = new MailMessage();
            // mail.From = new MailAddress("reservations@adventureresortscruises.in", "ARC Reservations");
            mail.From = new MailAddress("" + CompanyEmail + "", "Reservations");

            string toEmailId = Session["AgentMailId"] != null ? Session["AgentMailId"].ToString() : Session["CustMailId"].ToString();

            if (Debugger.IsAttached)
                toEmailId = "rohit@farhorizonindia.com";

            if (string.IsNullOrEmpty(toEmailId))
                return;

            mail.To.Add(toEmailId);
            mail.CC.Add(ccEmail);
            mail.Subject = "Reservation - " + Session["InvName"].ToString() + " - <" + dtbkcode.Rows[0][0].ToString() + ">";
            #endregion

            #region Email Body
            StringBuilder sb = new StringBuilder();
            sb.Append("<div>");
            if (Session["AgentMailId"] != null && Session["AgentMailId"] != "")
            {
                sb.Append("<div>Booking No:" + dtbkcode.Rows[0].ItemArray[0].ToString() + "</div>  <div> Date of Booking: " + Convert.ToDateTime(System.DateTime.Now).ToString("d MMMM, yyyy") + " </div> <div><br/> </div> <div> Dear Sir/Madam,</div> <div><br/></div><div> Namaskar! Greetings from " + CompanyName + "! </div> <div><br/> </div><div> Thank you for choosing MV Mahabaahu Cruise, River Brahmaputra for your vacation.<br/>" + packageName + " on MV Mahabaahu.</div><div><div> <div><br/></div><div>Check In: " + lblArrvDate.Text + "</div><div><br/></div><div>Check Out: " + lblDepartDate.Text + "</div><div><br/></div> ");
            }
            else
            {
                sb.Append("<div>Booking No:" + dtbkcode.Rows[0].ItemArray[0].ToString() + "</div>  <div> Date of Booking: " + Convert.ToDateTime(System.DateTime.Now).ToString("d MMMM, yyyy") + " </div> <div><br/> </div> <div> Dear " + Session["InvName"].ToString() + ",</div> <div><br/></div><div> Namaskar! Greetings from " + CompanyName + "! </div> <div><br/> </div><div> Thank you for choosing MV Mahabaahu Cruise, River Brahmaputra for your vacation.<br/>" + packageName + " on MV Mahabaahu.</div><div><div> <div><br/></div><div>Check In: " + lblArrvDate.Text + "</div><div><br/></div><div>Check Out: " + lblDepartDate.Text + "</div><div><br/></div> ");
            }
            sb.Append(" <div>The cruise showcases the Living, Natural and Cultural History of Assam.We trust that you will come to love the riches that the Brahmaputra River has to offer. An amazing wildlife experience awaits you along with the experiences of tea picking, watching monks perform at one of the largest River Island and visiting villages inhabited by tribesmen.</div> <div><br/> </div> <div> We take this opportunity to inform you that the final confirmation for the cruise is to be completed prior to  </div><div><br/> </div>  <div> day - 90. You will receive an automated e - reminder on day - 110 and another on day - 100. Please ignore if paid.</div><div><br/>We look forward to your confirmation.</div> <div><br/>   </div> <div> Appreciations!</div> <div><br/>  </div> <div> TheMahabaahu Team!</div> ");
            sb.Append("</div>");
            sb.Append("<img src='" + CompanyLogo + "' alt='Image'/><br /><div> " + CompanyName + "</div><div> " + CompanyAddress + " </div> <div> Phone: " + CompanyPhoneNo + " </div><div> Mobile: " + CompanyMobile + " </div><div><br/> </div> ");
            if (Session["AccomId"] != "" && Session["AccomId"] != null)
            {
                PopulateModule(Convert.ToInt32(Session["AccomId"]));
                string AccomPolicyUrl = ViewState["ModuleDt"].ToString();
                sb.Append("<div>The booking policy of the cruise can be referred to at <a href='" + AccomPolicyUrl + "' target='_blank' data-saferedirecturl='https://www.google.com/url?hl=en&amp;q=" + AccomPolicyUrl + "&amp;source=gmail&amp;ust=1470139247045000&amp;usg=AFQjCNH3vyzjL507K4FspRY6TihAfogUug'>" + AccomPolicyUrl + " </a>.</div><div><br/></div><div> Enclosure:</div><div> Invoice for your booking is attached with this email.</div> ");
            }
            else
            {
                sb.Append("<div>The booking policy of the cruise can be referred to at <a href='http://www.mahabaahucruiseindia.com/cruise-policy' target='_blank' data-saferedirecturl='https://www.google.com/url?hl=en&amp;q=http://www.mahabaahucruiseindia.com/cruise-policy&amp;source=gmail&amp;ust=1470139247045000&amp;usg=AFQjCNH3vyzjL507K4FspRY6TihAfogUug'>http://www.mahabaahucruiseindia.com/cruise-policy </a>.</div><div><br/></div><div> Enclosure:</div><div> Invoice for your booking is attached with this email.</div> ");
            }


            #endregion
            mail.IsBodyHtml = true;
            mail.Body = sb.ToString();
            //    mail.CC.Add("reservations@adventureresortscruises.com");
            mail.Attachments.Add(new Attachment(Server.MapPath("inv/" + lbInvoiceNO.Text + "Invoice.pdf")));

            try
            {
                SmtpServer.Send(mail);
            }
            catch (Exception exp)
            {
                Console.WriteLine(exp.Message);
            }
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Showstatus", "javascript:alert('Payment Successfull, Please Print your Invoice, Booking Details have been sent to your e-mail.')", true);
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Showstatus", "javascript:alert('" + ex.Message.ToString() + "')", true);
        }
    }

    private void ReleaseBookingLock()
    {
        BALBookingLock bl = SessionServices.RetrieveSession<BALBookingLock>("BookingLock");
        //BALBookingLock bl = Session["BookingLock"] as BALBookingLock;

        //BALBookingLock bl = Session["BookingLock"] != null ? (BALBookingLock)Session["BookingLock"] : null;

        if (bl != null)
        {
            DALBookingLock dbl = new DALBookingLock();
            dbl.ReleaseLock(bl);
        }
    }
    protected void gdvCruiseRooms_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Cells[0].Text = e.Row.Cells[0].Text + " (" + gdvCruiseRooms.DataKeys[e.Row.RowIndex].Values["Bed Configuration"].ToString() + "), Cabin no " + gdvCruiseRooms.DataKeys[e.Row.RowIndex].Values["RoomNumber"].ToString();
            }
        }
        catch
        { }
    }

    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("SearchProperty1.aspx");
    }

    protected void btnPrint_Click(object sender, EventArgs e)
    {
        try
        {
            btnBack.Visible = false;
            Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "MyFunction()", true);
        }
        catch
        {
        }
    }

    #region Obsolete Method(s)
    [Obsolete]
    private void InsertParentTableData()
    {
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
            dtGetReturnedData = dlsr.GetDepartureDetails(blsr);
            blsr._sBookingRef = Session["BookingRef"].ToString();
            blsr._dtStartDate = Convert.ToDateTime(dtGetReturnedData.Rows[0]["CheckInDate"]);
            blsr._dtEndDate = Convert.ToDateTime(dtGetReturnedData.Rows[0]["CheckOutDate"]);
            blsr._iAccomTypeId = Convert.ToInt32(dtGetReturnedData.Rows[0]["AccomTypeId"]);
            blsr._iAccomId = Convert.ToInt32(dtGetReturnedData.Rows[0]["AccomId"]);

            blsr._iNights = Convert.ToInt32(dtGetReturnedData.Rows[0]["NoOfNights"]);
            DataTable dtRoomBookingDetails = SessionServices.RetrieveSession<DataTable>("BookedRooms");
            blsr._iPersons = Convert.ToInt32(dtRoomBookingDetails.Compute("SUM(Pax)", string.Empty));
            blsr._BookingStatusId = 1;
            blsr._SeriesId = 0;
            blsr._proposedBooking = false;
            blsr._chartered = false;

            Session.Add("tblBookingBAL", blsr);
            int iBRC = dlsr.GetBookingReferenceCount(blsr);

            if (iBRC > 0)
            {
                System.Web.UI.ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "AlertBox", "alert('The Booking Reference mentioned by you is not unique. Please enter a different reference number.');", true);
                return;
            }
            int GetQueryResponse = dlsr.AddParentBookingDetail(blsr);
        }
        catch
        {
        }
    }

    [Obsolete]
    private void InsertChildTableData()
    {
        #region Fetching Departure Details
        blsr.action = "GetDepartureDetails";
        blsr.PackageId = Session["PackageId"].ToString();
        dtGetReturnedData = dlsr.GetDepartureDetails(blsr);
        blsr._iAccomId = Convert.ToInt32(dtGetReturnedData.Rows[0]["AccomId"]);
        #endregion
        blsr.action = "getMaxBookId";
        DataTable dtmaxId = dlsr.GetMaxBookingId(blsr);

        if (dtGetReturnedData != null)
        {
            int MaxBookingId = Convert.ToInt32(dtmaxId.Rows[0].ItemArray[0].ToString());
            blsr.action = "getmaxbookingcode";
            DataTable dtbkcode = dlsr.GetMaxBookingId(blsr);

            lbBookingNo.Text = dtbkcode.Rows[0].ItemArray[0].ToString();

            BookedId = MaxBookingId;
            blsr._iBookingId = MaxBookingId;

            DataTable GridRoomPaxDetail = SessionServices.RetrieveSession<DataTable>("BookedRooms");

            gdvCruiseRooms.DataSource = GridRoomPaxDetail;
            gdvCruiseRooms.DataBind();
            // hidecolumns();

            lblacm.Text = "M V Mahabaahu";
            lblVessel.Text = "Vessel: ";
            lbPax.Text = Convert.ToInt32(GridRoomPaxDetail.Compute("SUM(Pax)", string.Empty)).ToString();
            //    lblTotoAmt.Text = Convert.ToInt32(GridRoomPaxDetail.Compute("SUM(Price)", string.Empty)).ToString();

            lblDepartDate.Text = Convert.ToDateTime(dtGetReturnedData.Rows[0]["CheckOutDate"]).ToString("d MMMM, yyyy");
            lblArrvDate.Text = Convert.ToDateTime(dtGetReturnedData.Rows[0]["CheckInDate"]).ToString("d MMMM, yyyy");

            int LoopInsertStatus = 0;
            try
            {
                for (int LoopCounter = 0; LoopCounter < GridRoomPaxDetail.Rows.Count; LoopCounter++)
                {
                    blsr._dtStartDate = Convert.ToDateTime(dtGetReturnedData.Rows[0]["CheckInDate"]);
                    blsr._dtEndDate = Convert.ToDateTime(dtGetReturnedData.Rows[0]["CheckOutDate"]);
                    blsr._iPaxStaying = Convert.ToInt32(GridRoomPaxDetail.Rows[LoopCounter]["Pax"].ToString());

                    blsr._bConvertTo_Double_Twin = GridRoomPaxDetail.Rows[LoopCounter]["Convertable"].ToString() == "1" ? true : false;
                    blsr._cRoomStatus = "B";
                    blsr._sRoomNo = GridRoomPaxDetail.Rows[LoopCounter]["RoomNumber"].ToString();
                    blsr.action = "AddPriceDetailsToo";
                    blsr._Amt = Convert.ToDecimal(GridRoomPaxDetail.Rows[LoopCounter]["Price"].ToString());
                    blsr.PaymentId = Session["BookingPayId"].ToString();
                    blsr._PaidAmount = Convert.ToDouble(Session["Paid"]);
                    int GetQueryResponse = dlsr.AddRoomBookingDetails(blsr);
                    if (GetQueryResponse > 0)
                    {
                        LoopInsertStatus++;
                    }
                    else
                    {
                        //do nothing
                    }
                }
                hfBookingId.Value = MaxBookingId.ToString();

            }
            catch
            {
            }
        }
        else
        {
        }
    }

    [Obsolete]
    private int InsertBookingTableData(int acmid, int acmtpid, int agid, string bkref, DateTime cin, DateTime cout, DataTable vsdetails)
    {
        try
        {
            dtbkdetails = new DataTable();
            dtbkdetails = SessionServices.RetrieveSession<DataTable>("Bookingdt");

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

            int GetQueryResponse = dlsr.AddParentBookingDetail(blsr);
            if (GetQueryResponse > 0)
                return 1;
            else
                return 0;
        }
        catch (Exception ex)
        {
            return 0;
        }
    }

    [Obsolete]
    private int InsertRoomBookingTableData(DataTable dtbooking, DateTime cin, DateTime cout, int acmid)
    {
        try
        {
            dtbkdetails = new DataTable();
            dtbkdetails = SessionServices.RetrieveSession<DataTable>("Bookingdt");
            blsr._iAccomId = acmid;

            blsr.action = "getMaxBookId";
            DataTable dtmaxId = dlsr.GetMaxBookingId(blsr);

            int MaxBookingId = Convert.ToInt32(dtmaxId.Rows[0].ItemArray[0].ToString());

            blsr.action = "getmaxbookingcode";
            DataTable dtbkcode = dlsr.GetMaxBookingId(blsr);

            lbBookingNo.Text = dtbkcode.Rows[0].ItemArray[0].ToString();
            //  BookedId = MaxBookingId;
            blsr._iBookingId = MaxBookingId;

            Session["maxBookId"] = MaxBookingId;
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

            lblArrvDate.Text = Convert.ToDateTime(Session["Chkin"]).ToString("d MMMM, yyyy");
            lblDepartDate.Text = Convert.ToDateTime(Session["chkout"]).ToString("d MMMM, yyyy");
            lblacm.Text = "Accomodation Name: " + Session["AccomName"].ToString();

            DataTable Bookingdt = SessionServices.RetrieveSession<DataTable>("Bookingdt");
            gdvSelectedRooms.DataSource = Bookingdt;
            gdvSelectedRooms.DataBind();

            gdvSelectedRooms.FooterRow.Cells[2].Text = "Total <br>Tax @ 4.50% <br> <b> Grand Total </b> ";
            //gdvSelectedRooms.FooterRow.Cells[3].Text = "Included" + " </br> " + Math.Round((4.5 * (Convert.ToInt32(Bookingdt.Compute("SUM(Total1)", "[Total1] > 0")) / 100))).ToString("#.##") + " </br> " + Math.Round((Convert.ToDouble(Bookingdt.Compute("SUM(Total1)", "[Total1] > 0")) + (4.5 * (Convert.ToInt32(Bookingdt.Compute("SUM(Total1)", "[Total1] > 0")) / 100)))).ToString("#.##") + " ";

            string amountText = string.Format("{0}<br>{1}<br>{2}",
                Math.Round(Convert.ToDouble(Bookingdt.Compute("SUM(Total1)", "[Total1] > 0"))).ToString("#.##"),
                " ",
                Math.Round(Convert.ToDouble(Bookingdt.Compute("SUM(Total1)", "[Total1] > 0"))).ToString("#.##"));
            gdvSelectedRooms.FooterRow.Cells[3].Text = amountText;

            //gdvSelectedRooms.FooterRow.Cells[3].Text = Math.Round(Convert.ToDouble(Bookingdt.Compute("SUM(Total1)", "[Total1] > 0"))).ToString("#.##") + " </br> " + Math.Round((4.5 * (Convert.ToInt32(Bookingdt.Compute("SUM(Total1)", "[Total1] > 0")) / 100))).ToString("#.##") + " </br> " + Math.Round((Convert.ToDouble(Bookingdt.Compute("SUM(Total1)", "[Total1] > 0")) + (4.5 * (Convert.ToInt32(Bookingdt.Compute("SUM(Total1)", "[Total1] > 0")) / 100)))).ToString("#.##") + " ";

            lbPax.Text = Convert.ToInt32(Bookingdt.Compute("SUM(Pax)", "[Pax] > 0")).ToString(); ;
            lblTotAMt.Text = Math.Round((Convert.ToDouble(Bookingdt.Compute("SUM(Total1)", "[Total1] > 0")) + (4.5 * (Convert.ToInt32(Bookingdt.Compute("SUM(Total1)", "[Total1] > 0")) / 100)))).ToString("#.##");

            double balanceAmount = Math.Round((Convert.ToDouble(lblTotAMt.Text) - Convert.ToDouble(lblTotPaid.Text)));

            if (balanceAmount > 0)
            {
                lblBalance.Text = Math.Round((Convert.ToDouble(lblTotAMt.Text) - Convert.ToDouble(lblTotPaid.Text))).ToString("#.##");
                lbBalanceDueIn.Text = Convert.ToDateTime(lblArrvDate.Text).AddDays(-90).ToString("d MMMM, yyyy");
            }
            else
            {
                lblBalanceDueOn.Visible = false;
                lblBalance.Text = "0";
                lbBalanceDueIn.Text = string.Empty;
            }
            hfBookingId.Value = MaxBookingId.ToString();

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
