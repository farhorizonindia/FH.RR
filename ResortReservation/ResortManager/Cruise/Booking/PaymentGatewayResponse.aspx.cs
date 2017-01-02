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
    MailMessage mail = new MailMessage();
    SmtpClient SmtpServer = new SmtpClient("adventureresortscruises.in");

    DataTable dtbkdetails;
    int iagentid = 0;
    int iAccomId = 0;
    int iaccomtypeid = 0;
    DateTime chkin;
    DateTime chkout;
    int custId = 0;    
    string bookref;
    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ViewState["sentMail"] = "0";
            lbBookinDate.Text = Convert.ToDateTime(System.DateTime.Now).ToString("d MMMM, yyyy");
            lbInvoiceNO.Text = "IVc" + DateTime.Now.ToString("MMddhhmmssfff");
            dated.Text = Convert.ToDateTime(System.DateTime.Now).ToString("d MMMM, yyyy");
        }

        lblBuyerName.Text = Session["InvName"].ToString();
        if (Session["Address"] != null)
        {
            lblBuyerAddress.Text = Session["Address"].ToString();
        }

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
    }

    protected override void Render(HtmlTextWriter writer)
    {
        if (ViewState["sentMail"] == "0")
        {
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
            htmlCode = htmlCode.Replace("dwew", "none");
            //use single thread to generate the pdf from above html code
            Thread thread = new Thread(() =>
            { pdf.LoadFromHTML(htmlCode, false, setting, htmlLayoutFormat); });
            thread.SetApartmentState(ApartmentState.STA);
            thread.Start();
            thread.Join();

            // Save the file to PDF and preview it.
            pdf.SaveToFile(rename(Server.MapPath("inv/" + lbInvoiceNO.Text + ".pdf")));

            int pageNumber = 1;

            PdfReader reader = new PdfReader(Server.MapPath("inv/" + lbInvoiceNO.Text + ".pdf"));
            Rectangle cropbox = reader.GetCropBox(1);
            iTextSharp.text.Rectangle size = new iTextSharp.text.Rectangle(cropbox.Width - 60, cropbox.Height - 12);
            Document document = new Document(size);
            iTextSharp.text.pdf.PdfWriter writer1 = PdfWriter.GetInstance(document,
            new FileStream(Server.MapPath("inv/" + lbInvoiceNO.Text + ".pdf").Replace(lbInvoiceNO.Text + ".pdf", lbInvoiceNO.Text + "File.pdf"),
            FileMode.Create, FileAccess.Write));
            document.Open();
            PdfContentByte cb = writer1.DirectContent;
            document.NewPage();
            PdfImportedPage page = writer1.GetImportedPage(reader,
            pageNumber);
            cb.AddTemplate(page, 0, 0);
            document.Close();

            mail.Attachments.Add(new Attachment(Server.MapPath("inv/" + lbInvoiceNO.Text + "File.pdf")));
            if (!Debugger.IsAttached)
            {
                SmtpServer.Send(mail);
            }
            ViewState["sentMail"] = "1";
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
            BALBooking blsr = Session["tblBookingBAL"] as BALBooking;
            DALBooking dlr = new DALBooking();
            dlr.UpdateBookingStatus(blsr._iBookingId, BookingStatusTypes.BOOKED);

            BALBooking bookingDetail = dlr.GetBookingDetails(blsr._iBookingId);
            ShowCruiseBookingDetails(bookingDetail);
        }
        catch
        {

        }
    }

    private void ShowCruiseBookingDetails(BALBooking bookingDetail)
    {
        lbBookingNo.Text = bookingDetail.BookingCode;

        DataTable GridRoomPaxDetail = Session["BookedRooms"] as DataTable;
        gdvCruiseRooms.DataSource = GridRoomPaxDetail;
        gdvCruiseRooms.DataBind();
        // hidecolumns();

        lblacm.Text = "M V Mahabaahu";
        lblVessel.Text = "Vessel: ";
        lbPax.Text = Convert.ToInt32(GridRoomPaxDetail.Compute("SUM(Pax)", string.Empty)).ToString();
        //    lblTotoAmt.Text = Convert.ToInt32(GridRoomPaxDetail.Compute("SUM(Price)", string.Empty)).ToString();

        lblDepartDate.Text = bookingDetail._dtStartDate.ToString("d MMMM, yyyy");
        lblArrvDate.Text = bookingDetail._dtEndDate.ToString("d MMMM, yyyy");
        hfBookingId.Value = bookingDetail._iBookingId.ToString();
    }

    /// <summary>
    /// This method will move the booking from Proposed state to Booked state.
    /// As payment is confirmed. Else the booking will stay as proposed booking for back-office operations.
    /// </summary>
    private void UpdateHotelBookingToBooked()
    {
        try
        {
            BALBooking blsr = Session["tblBookingBAL"] as BALBooking;
            DALBooking dlr = new DALBooking();
            dlr.UpdateBookingStatus(blsr._iBookingId, BookingStatusTypes.BOOKED);

            //BALBooking bookingDetail = dlr.GetBookingDetails(blsr._iBookingId);
            ShowHotelBookingDetails(blsr._iBookingId);
        }
        catch(Exception ex)
        {
            throw ex;
        }
    }

    private void ShowHotelBookingDetails(int bookingId)
    {
        lblArrvDate.Text = Convert.ToDateTime(Session["Chkin"]).ToString("d MMMM, yyyy");
        lblDepartDate.Text = Convert.ToDateTime(Session["chkout"]).ToString("d MMMM, yyyy");
        lblacm.Text = "Accomodation Name: " + Session["AccomName"].ToString();

        DataTable Bookingdt;
        Bookingdt = new DataTable();
        Bookingdt = Session["Bookingdt"] as DataTable;

        gdvSelectedRooms.DataSource = Bookingdt;
        gdvSelectedRooms.DataBind();

        gdvSelectedRooms.FooterRow.Cells[2].Text = "Total </br> Service Tax @ 4.50% </br> <b> Grand Total </b> ";

        gdvSelectedRooms.FooterRow.Cells[3].Text = Math.Round(Convert.ToDouble(Bookingdt.Compute("SUM(Total1)", "[Total1] > 0"))).ToString("#.##") + " </br> " + Math.Round((4.5 * (Convert.ToInt32(Bookingdt.Compute("SUM(Total1)", "[Total1] > 0")) / 100))).ToString("#.##") + " </br> " + Math.Round((Convert.ToDouble(Bookingdt.Compute("SUM(Total1)", "[Total1] > 0")) + (4.5 * (Convert.ToInt32(Bookingdt.Compute("SUM(Total1)", "[Total1] > 0")) / 100)))).ToString("#.##") + " ";

        lbPax.Text = Convert.ToInt32(Bookingdt.Compute("SUM(Pax)", "[Pax] > 0")).ToString(); ;
        lblTotAMt.Text = Math.Round((Convert.ToDouble(Bookingdt.Compute("SUM(Total1)", "[Total1] > 0")) + (4.5 * (Convert.ToInt32(Bookingdt.Compute("SUM(Total1)", "[Total1] > 0")) / 100)))).ToString("#.##");

        lblBalance.Text = Math.Round((Convert.ToDouble(lblTotAMt.Text) - Convert.ToDouble(lblTotPaid.Text))).ToString("#.##");
        lbBalanceDueIn.Text = Convert.ToDateTime(lblArrvDate.Text).AddDays(-90).ToString("d MMMM, yyyy");
        hfBookingId.Value = bookingId.ToString();
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
            Crc32 crc32 = new Crc32();
            String hash = String.Empty;
            byte[] mybytes = Encoding.UTF8.GetBytes(ClearString);
            foreach (byte b in crc32.ComputeHash(mybytes)) hash += b.ToString("x2");
            UInt32 Output = UInt32.Parse(hash, System.Globalization.NumberStyles.HexNumber);
            UInt32 Output1 = UInt32.Parse(key);            

            if (Output1 == Output)
            {
                if (Session["Hotel"] != null)
                {
                    lblTotPaid.Text = Convert.ToDouble(AMOUNT).ToString("#.##");

                    GenrateBill1(TRANSACTIONID);
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

                    GenrateBill(TRANSACTIONID);

                    int QueryResponse = AddTransactionDetails(TRANSACTIONSTATUS, APTRANSACTIONID, TRANSACTIONID, AMOUNT);
                    if (QueryResponse > 0)
                    {
                        // lbBookingNo.Text = TRANSACTIONID.ToString();
                        FiilPackage();
                    }
                    DataTable GridRoomPaxDetail = Session["BookedRooms"] as DataTable;

                    gdvCruiseRooms.FooterRow.Cells[2].Text = "Total </br> Service Tax @ 4.50% </br> <b> Grand Total </b>";
                    gdvCruiseRooms.FooterRow.Cells[3].Text = Math.Round(Convert.ToDouble(GridRoomPaxDetail.Compute("SUM(Price)", string.Empty))).ToString("#.##") + "</br> " + Math.Round((4.5 * (Convert.ToInt32(GridRoomPaxDetail.Compute("SUM(Price)", string.Empty)) / 100))).ToString("#.##") + " </br> " + Math.Round((Convert.ToDouble(GridRoomPaxDetail.Compute("SUM(Price)", string.Empty)) + (4.5 * (Convert.ToInt32(GridRoomPaxDetail.Compute("SUM(Price)", string.Empty)) / 100)))).ToString("#.##");
                    lblTotAMt.Text = Math.Round((Convert.ToDouble(GridRoomPaxDetail.Compute("SUM(Price)", string.Empty)) + (4.5 * (Convert.ToInt32(GridRoomPaxDetail.Compute("SUM(Price)", string.Empty)) / 100)))).ToString("#.##");
                    lblBalance.Text = Math.Round((Convert.ToDouble(lblTotAMt.Text) - Convert.ToDouble(lblTotPaid.Text))).ToString("#.##");
                    lbBalanceDueIn.Text = Convert.ToDateTime(lblArrvDate.Text).AddDays(-90).ToString("d MMMM, yyyy");

                    Session["BookedRooms"] = null;
                    Session["PackageId"] = null;
                    Session["BookingRef"] = null;
                }
                lbRuppeeinwords.Text = GF.NumbersToWords(Convert.ToInt32(lblTotAMt.Text));
            }
            else
            {
                Response.Write("Secure Hash mismatch.");
            }
            return hash;
        }
        catch
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
            string sqlQuery = "SELECT [PackageName] ,(select LocationName from dbo.Locations where       LocationId = tblPackages.BordingFrom       )as 'BoardFrom'  ,(select LocationName from dbo.Locations where       LocationId = tblPackages.BoadingTo       )as'BoardTo'   FROM[cruise].[dbo].[tblPackages] where PackageId = '" + Session["PackageId"] + "'";
            SqlDataAdapter adp = new SqlDataAdapter(sqlQuery, con);
            DataTable dtGetPackageDetails = new DataTable();
            adp.Fill(dtGetPackageDetails);
            //  lbpackageName.Text = dtGetPackageDetails.Rows[0]["PackageName"].ToString();
            lbStrtEnd.Text = "Cruise starts from " + dtGetPackageDetails.Rows[0]["BoardFrom"].ToString() + "  and ends at " + dtGetPackageDetails.Rows[0]["BoardTo"].ToString();

            con.Close();
        }
        catch (Exception sqe)
        {

        }
    }

    private void GenrateBill(string transactionId)
    {
        UpdateCruiseBookingToBooked();
        sendMail(transactionId);
    }

    private void GenrateBill1(string transactionId)
    {        
        UpdateHotelBookingToBooked();
        sendMail1(transactionId);
    }

    public void sendMail1(string TRANSACTIONID)
    {
        try
        {
            int noofpx = 0;

            for (int n = 0; n < gdvSelectedRooms.Rows.Count; n++)
            {
                noofpx += Convert.ToInt32(gdvSelectedRooms.Rows[n].Cells[1].Text);
            }
            //MailMessage mail = new MailMessage();
            //SmtpClient SmtpServer = new SmtpClient("adventureresortscruises.in");

            mail.From = new MailAddress("reservations@adventureresortscruises.in", "ARC Reservations");
            if (Session["AgentMailId"] != null)
            {
                mail.To.Add(Session["AgentMailId"].ToString());
            }
            else
            {
                mail.To.Add(Session["CustMailId"].ToString());
            }

            blsr.action = "getmaxbookingcode";
            DataTable dtbkcode = dlsr.GetMaxBookingId(blsr);
            mail.Subject = "Reservation -" + Session["SubInvName"].ToString() + " - " + dtbkcode.Rows[0].ItemArray[0].ToString() + "";

            StringBuilder sb = new StringBuilder();

            sb.Append("<div>");
            sb.Append("<div> Dear " + Session["InvName"].ToString() + ",</div> <div><br/></div><div>Greetings from " + Session["AccomName"].ToString() + "! </div> <div><br/> </div><div>Thank you for booking with us.</div> <div><br/></div> ");
            sb.Append(" <div>Check In :" + lblArrvDate.Text + " </div> <div></div> <div>Check Out :" + lblDepartDate.Text + "  </div>  <div><br/> </div>  <div><b>Accommodation Details</b></div> <div>  </div> <div> Number of Rooms: " + gdvSelectedRooms.Rows.Count.ToString() + "</div> <div> </div> <div>Number of Persons: " + noofpx + " </div> ");
            sb.Append("<div><br/></div><div>We look forward to having you with us.</div><div><br/></div><div>With best wishes,</div><div><br/></div>");

            sb.Append("<div>" + Session["AccomName"].ToString() + " Team <div><div><br/><div>");

            sb.Append("</div>");
            sb.Append("<div></br><div><div>Enclosure: Invoice for your booking is attached with this email.<div>");


            mail.IsBodyHtml = true;
            mail.Body = sb.ToString();
            mail.CC.Add("reservations@adventureresortscruises.com");
            SmtpServer.Port = 587;
            SmtpServer.Credentials = new System.Net.NetworkCredential("reservations@adventureresortscruises.in", "Augurs@123");
            SmtpServer.EnableSsl = false;

            if (!Debugger.IsAttached)
            {
                SmtpServer.Send(mail);
            }
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Showstatus", "javascript:alert('Payment Successfull, Please Print your Invoice, Booking Details have been sent to your e-mail.')", true);
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Showstatus", "javascript:alert('" + ex.Message.ToString() + "')", true);
        }
    }

    public void sendMail(string TRANSACTIONID)
    {
        try
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["ReservationConnectionString"].ConnectionString);
            con.Open();
            string sqlQuery = "SELECT [PackageName] ,(select LocationName from dbo.Locations where       LocationId = tblPackages.BordingFrom       )as 'BoardFrom'  ,(select LocationName from dbo.Locations where       LocationId = tblPackages.BoadingTo       )as'BoardTo',NoOfNights   FROM[cruise].[dbo].[tblPackages] where PackageId = '" + Session["PackageId"] + "'";
            SqlDataAdapter adp = new SqlDataAdapter(sqlQuery, con);
            DataTable dtGetPackageDetails = new DataTable();
            adp.Fill(dtGetPackageDetails);

            lbpackageName.Text = "Package: " + dtGetPackageDetails.Rows[0]["PackageName"].ToString() + ", " + dtGetPackageDetails.Rows[0]["NoOfNights"].ToString() + " Nights";
            lbStrtEnd.Text = "Cruise starts from " + dtGetPackageDetails.Rows[0]["BoardFrom"].ToString() + "  and ends at " + dtGetPackageDetails.Rows[0]["BoardTo"].ToString();

            con.Close();

            // MailMessage mail = new MailMessage();
            // SmtpClient SmtpServer = new SmtpClient("adventureresortscruises.in");

            mail.From = new MailAddress("reservations@adventureresortscruises.in", "ARC Reservations");
            if (Session["AgentMailId"] != null)
            {
                mail.To.Add(Session["AgentMailId"].ToString());
            }
            else
            {
                mail.To.Add(Session["CustMailId"].ToString());
            }
            blsr.action = "getmaxbookingcode";
            DataTable dtbkcode = dlsr.GetMaxBookingId(blsr);

            string bref = Session["BookingRef"].ToString();

            string l = Session["SubInvName"].ToString();
            mail.Subject = "Reservation - " + Session["SubInvName"].ToString() + " - " + dtbkcode.Rows[0][0].ToString() + "";

            StringBuilder sb = new StringBuilder();

            sb.Append("<div>");
            sb.Append("<div>Booking No:" + dtbkcode.Rows[0].ItemArray[0].ToString() + "</div>  <div> Date of Booking: " + Convert.ToDateTime(System.DateTime.Now).ToString("d MMMM, yyyy") + " </div> <div><br/> </div> <div> Dear " + Session["InvName"].ToString() + ",</div> <div><br/></div><div> Namaskar! Greetings from Assam, India!</div> <div><br/> </div><div> Thank you for booking " + dtGetPackageDetails.Rows[0]["PackageName"].ToString() + " on MV Mahabaahu.</div> <div><br/></div> ");
            sb.Append(" <div>The cruise showcases Living, Natural and Cultural History where silk and cotton vie your attention. A cup of famous Assam tea beckons you over to the little known north eastern part of India.</div> <div><br/> </div> <div> This pristine destination unfolds the history of an ancient civilisation of the Tibeto - Burman Ahoms who reigned in the region for more than 600 years. The river brings you up close to the simplistic ways of a speckled tribal and multiracial life. </div><div> We take this opportunity to inform you that the final confirmation for the cruise is to be completed prior to day - 90. You will receive an automated e - reminder on day - 110 and another on day - 100. Please ignore if paid.<br/> </div>  <div><br/> </div>  <div> We look forward to your confirmation.</div> <div><br/>   </div> <div> Appreciations!</div> <div><br/>  </div> <div> TheMahabaahu Team!</div> ");
            sb.Append("</div>");
            sb.Append("<img src='http://adventureresortscruises.in/Cruise/booking/img_logo.png' alt='Image'/><br /><div> Adventure Resorts & Cruises Pvt. Ltd.</div><div> B209, CR Park, New Delhi 110019 </div> <div> Phone: +91 - 011 - 41057370 / 1 / 2 </div><div> Mobile: +91 - 9599755353 </div><div><br/> </div> ");
            sb.Append("<div>The booking policy of the cruise can be referred to at<a href='http://www.mahabaahucruiseindia.com/cruise-policy' target='_blank' data-saferedirecturl='https://www.google.com/url?hl=en&amp;q=http://www.mahabaahucruiseindia.com/cruise-policy&amp;source=gmail&amp;ust=1470139247045000&amp;usg=AFQjCNH3vyzjL507K4FspRY6TihAfogUug'>http://www.mahabaahucruiseindia.com/cruise-policy </a>.</div><div><br/></div><div> Enclosure:</div><div> Invoice for your booking is attached with this email.</div> ");

            mail.IsBodyHtml = true;
            mail.Body = sb.ToString();
            mail.CC.Add("reservations@adventureresortscruises.com");

            SmtpServer.Port = 587;
            SmtpServer.Credentials = new System.Net.NetworkCredential("reservations@adventureresortscruises.in", "Augurs@123");
            SmtpServer.EnableSsl = false;

            if (!Debugger.IsAttached)
            {
                SmtpServer.Send(mail);
            }
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Showstatus", "javascript:alert('Payment Successfull, Please Print your Invoice, Booking Details have been sent to your e-mail.')", true);
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Showstatus", "javascript:alert('" + ex.Message.ToString() + "')", true);
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
        Response.Redirect("SearchProperty.aspx");
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
            DataTable dtRoomBookingDetails = Session["BookedRooms"] as DataTable;
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

            DataTable GridRoomPaxDetail = Session["BookedRooms"] as DataTable;

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
                    blsr._Paid = Convert.ToDouble(Session["Paid"]);
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

    private int InsertBookingTableData(int acmid, int acmtpid, int agid, string bkref, DateTime cin, DateTime cout, DataTable vsdetails)
    {
        try
        {
            dtbkdetails = new DataTable();
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

    private int InsertRoomBookingTableData(DataTable dtbooking, DateTime cin, DateTime cout, int acmid)
    {
        try
        {
            dtbkdetails = new DataTable();
            dtbkdetails = Session["Bookingdt"] as DataTable;
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
                blsr._Paid = Convert.ToDouble(Session["Paid"]);

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

            DataTable Bookingdt;
            Bookingdt = new DataTable();
            Bookingdt = Session["Bookingdt"] as DataTable;

            gdvSelectedRooms.DataSource = Bookingdt;
            gdvSelectedRooms.DataBind();

            gdvSelectedRooms.FooterRow.Cells[2].Text = "Total </br> Service Tax @ 4.50% </br> <b> Grand Total </b> ";

            gdvSelectedRooms.FooterRow.Cells[3].Text = Math.Round(Convert.ToDouble(Bookingdt.Compute("SUM(Total1)", "[Total1] > 0"))).ToString("#.##") + " </br> " + Math.Round((4.5 * (Convert.ToInt32(Bookingdt.Compute("SUM(Total1)", "[Total1] > 0")) / 100))).ToString("#.##") + " </br> " + Math.Round((Convert.ToDouble(Bookingdt.Compute("SUM(Total1)", "[Total1] > 0")) + (4.5 * (Convert.ToInt32(Bookingdt.Compute("SUM(Total1)", "[Total1] > 0")) / 100)))).ToString("#.##") + " ";

            lbPax.Text = Convert.ToInt32(Bookingdt.Compute("SUM(Pax)", "[Pax] > 0")).ToString(); ;
            lblTotAMt.Text = Math.Round((Convert.ToDouble(Bookingdt.Compute("SUM(Total1)", "[Total1] > 0")) + (4.5 * (Convert.ToInt32(Bookingdt.Compute("SUM(Total1)", "[Total1] > 0")) / 100)))).ToString("#.##");

            lblBalance.Text = Math.Round((Convert.ToDouble(lblTotAMt.Text) - Convert.ToDouble(lblTotPaid.Text))).ToString("#.##");
            lbBalanceDueIn.Text = Convert.ToDateTime(lblArrvDate.Text).AddDays(-90).ToString("d MMMM, yyyy");
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
