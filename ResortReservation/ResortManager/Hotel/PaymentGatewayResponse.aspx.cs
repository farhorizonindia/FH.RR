using System;
using System.Collections.Generic;
using System.Collections;
using System.Web;
using System.Web.UI;
using System.Xml;
using System.Xml.Serialization;
using System.IO;
using System.Web.UI.WebControls;
using System.Text;
using System.Diagnostics;
using System.Globalization;
using System.Data;
using System.Data.SqlClient;
using System.Security.Cryptography;
using System.Net.Mail;
using System.Configuration;


public partial class response : System.Web.UI.Page
{
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





    DataTable dtbkdetails;






    int iagentid = 0;
    int iAccomId = 0;
    int iaccomtypeid = 0;
    DateTime chkin;
    DateTime chkout;
    int custId = 0;

    string bookref;


    protected void Page_Load(object sender, EventArgs e)
    {

        //if (!IsPostBack)
        //{

        //    lbBookinDate.Text = Convert.ToDateTime(System.DateTime.Now).ToString("d MMMM, yyyy");
        //    lbInvoiceNO.Text = "IVc" + DateTime.Now.ToString("MMddhhmmssfff");
        //    dated.Text = Convert.ToDateTime(System.DateTime.Now).ToString("d MMMM, yyyy");
        //}
        //lblBuyerName.Text = Session["InvName"].ToString();
        //if (Session["Address"] != null)
        //{
        //    lblBuyerAddress.Text = Session["Address"].ToString();

        //}

        //if (Session["Hotel"] != null)
        //{

        //    try
        //    {

        //        if (Session["Usercode"] != null)
        //        {
        //            Int32.TryParse(Session["AId"].ToString(), out iagentid);

        //        }
        //        else if (Session["CustId"] != null)
        //        {

        //            Int32.TryParse(Session["CustId"].ToString(), out custId);
        //            iagentid = custId;


        //        }


        //        Int32.TryParse(Session["AccomId"].ToString(), out iAccomId);
        //        Int32.TryParse(Session["iAccomtypeId"].ToString(), out iaccomtypeid);


        //        DateTime.TryParse(Session["Chkin"].ToString(), out chkin);
        //        DateTime.TryParse(Session["chkout"].ToString(), out chkout);

        //        bookref = Session["BookingRef"].ToString();


        //        blsr._sBookingRef = bookref;
        //        blsr._dtStartDate = chkin;
        //        blsr._dtEndDate = chkout;


        //        blsr._iAccomTypeId = iaccomtypeid;
        //        blsr._iAccomId = iAccomId;

        //        int iBRC = dlsr.GetBookingReferenceCount(blsr);

        //        if (iBRC > 0)
        //        {


        //            System.Web.UI.ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "AlertBox", "alert('The Booking Reference mentioned by you is not unique. Please enter a different reference number.');", true);

        //            return;
        //        }

              




        //        //    Response.Redirect("HotelInvoice.aspx");



        //    }
        //    catch
        //    {
        //    }
        //}





    }

    public void sendMail(string TRANSACTIONID)
    {
        try
        {


            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["ReservationConnectionString"].ConnectionString);
            con.Open();
            string sqlQuery = "SELECT [PackageName] ,(select LocationName from dbo.Locations where       LocationId = tblPackages.BordingFrom       )as 'BoardFrom'  ,(select LocationName from dbo.Locations where       LocationId = tblPackages.BoadingTo       )as'BoardTo'   FROM[cruise].[dbo].[tblPackages] where PackageId = '" + Session["PackageId"] + "'";
            SqlDataAdapter adp = new SqlDataAdapter(sqlQuery, con);
            DataTable dtGetPackageDetails = new DataTable();
            adp.Fill(dtGetPackageDetails);

            lbpackageName.Text = dtGetPackageDetails.Rows[0]["PackageName"].ToString();
            lbStrtEnd.Text = "Cruise starts from " + dtGetPackageDetails.Rows[0]["BoardFrom"].ToString() + "  and ends at " + dtGetPackageDetails.Rows[0]["BoardTo"].ToString();

            con.Close();

            MailMessage mail = new MailMessage();
            SmtpClient SmtpServer = new SmtpClient("adventureresortscruises.in");

            mail.From = new MailAddress("reservations@adventureresortscruises.in");
            if (Session["AgentMailId"] != null)
            {
                mail.To.Add(Session["AgentMailId"].ToString());
            }
            else
            {
                mail.To.Add(Session["CustMailId"].ToString());
            }
            string bref = Session["BookingRef"].ToString();
            mail.Subject = "adventureresortscruises";

            StringBuilder sb = new StringBuilder();


            sb.Append("<div>Subject: Reservation -" + Session["InvName"].ToString() + ";" + TRANSACTIONID + "</div><div><br/></div> ");
            sb.Append("<div>");
            sb.Append("<div>Booking No:&#160" + TRANSACTIONID + "</div>  <div> Date of Booking: " + Convert.ToDateTime(System.DateTime.Now).ToShortDateString() + " </div> <div><br/> </div> <div> Dear " + Session["InvName"].ToString() + " ,</div> <div><br/></div><div> Namaskar!Greetings from Assam, India!</div> <div><br/> </div><div> Thank you for booking " + dtGetPackageDetails.Rows[0]["PackageName"].ToString() + " on MV Mahabaahu.</div> <div><br/></div> ");
            sb.Append(" <div>The cruise showcases Living, Natural and Cultural History where silk and cotton vie your attention.  A cup of famous Assam tea beckons you over to the little known north eastern part of India.</div> <div><br/> </div> <div> This pristine destination unfolds the history of an ancient civilisation of the Tibeto - Burman Ahoms who reigned in the region for more than 600 years.The river brings you up close to the simplistic ways of a speckled tribal and multiracial life. </div><div> We take this opportunity to inform you that the final confirmation for the cruise is to be completed prior to day - 90.You will receive an automated e - reminder on day - 110 and another on day - 100.Please ignore if paid.<br/> </div>  <div><br/> </div>  <div> We look forward to your confirmation.</div> <div><br/>   </div> <div> Appreciations!</div> <div><br/>  </div> <div> TheMahabaahu Team!</div> ");
            sb.Append("</div>");
            sb.Append("<img src='http://adventureresortscruises.in/Cruise/booking/img_logo.png' alt='Image'/><br /><div> Adventure Resorts & amp; Cruises Pvt. Ltd.</div><div> B209, CR Park, New Delhi 110019 </div> <div> Phone:  +91 - 011 - 41057370 / 1 / 2 </div><div> Mobile: +91 - 9599755353 </div><div><br/> </div> ");
            sb.Append("<div>The booking policy of the cruise can be referred to at <a href='http://www.mahabaahucruiseindia.com/cruise-policy' target='_blank' data-saferedirecturl='https://www.google.com/url?hl=en&amp;q=http://www.mahabaahucruiseindia.com/cruise-policy&amp;source=gmail&amp;ust=1470139247045000&amp;usg=AFQjCNH3vyzjL507K4FspRY6TihAfogUug'>http://www.mahabaahucruiseindia.com/cruise-policy </a>.</div><div><br/></div><div> Enclosure:</div><div> Invoice for your booking is attached with this email.</div> ");

            mail.IsBodyHtml = true;
            mail.Body = sb.ToString();
            //mail.CC = "reservations@adventureresortscruises.com"; 



            SmtpServer.Port = 587;
            SmtpServer.Credentials = new System.Net.NetworkCredential("reservations@adventureresortscruises.in", "Augurs@123");
            SmtpServer.EnableSsl = false;

            SmtpServer.Send(mail);

            ScriptManager.RegisterStartupScript(this, this.GetType(), "Showstatus", "javascript:alert('mail Sent')", true);
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Showstatus", "javascript:alert('" + ex.Message.ToString() + "')", true);
        }
    }




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
                blsr._iAgentId = Convert.ToInt32(Session["CustId"].ToString());
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
            lbBookingNo.Text = dtmaxId.Rows[0].ItemArray[0].ToString();
            BookedId = MaxBookingId;
            blsr._iBookingId = MaxBookingId;
            DataTable GridRoomPaxDetail = Session["BookedRooms"] as DataTable;

            gdvCruiseRooms.DataSource = GridRoomPaxDetail;
            gdvCruiseRooms.DataBind();

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
                        LoopInsertStatus++;
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

    public void calculatePAx()
    {
        try
        {

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
                    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["ReservationConnectionString"].ConnectionString);
                    con.Open();
                    SqlCommand cmd = new SqlCommand("insert into tblTransactionDetails values('" + hfBookingId.Value + "', '" + TRANSACTIONID + "','" + APTRANSACTIONID + "' , '" + Convert.ToDecimal(AMOUNT) + "','" + TRANSACTIONSTATUS + "','" + System.DateTime.Now + "')", con);
                    int QueryResponse = cmd.ExecuteNonQuery();

                    con.Close();
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
                    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["ReservationConnectionString"].ConnectionString);
                    con.Open();
                    SqlCommand cmd = new SqlCommand("insert into tblTransactionDetails values('" + hfBookingId.Value + "', '" + TRANSACTIONID + "','" + APTRANSACTIONID + "' , '" + Convert.ToDecimal(AMOUNT) + "','" + TRANSACTIONSTATUS + "','" + System.DateTime.Now + "')", con);
                    int QueryResponse = cmd.ExecuteNonQuery();
                    con.Close();
                    if (QueryResponse > 0)
                    {
                        // lbBookingNo.Text = TRANSACTIONID.ToString();
                        FiilPackage();


                    }
                    DataTable GridRoomPaxDetail = Session["BookedRooms"] as DataTable;



                    gdvCruiseRooms.FooterRow.Cells[1].Text = "Total </br> Service Tax @ 4.50% </br> <b> Grand Total </b>";
                    gdvCruiseRooms.FooterRow.Cells[3].Text = Math.Round(Convert.ToDouble(GridRoomPaxDetail.Compute("SUM(Price)", string.Empty))).ToString("#.##") + "</br> " + Math.Round((4.5 * (Convert.ToInt32(GridRoomPaxDetail.Compute("SUM(Price)", string.Empty)) / 100))).ToString("#.##") + " </br> " + Math.Round((Convert.ToDouble(GridRoomPaxDetail.Compute("SUM(Price)", string.Empty)) + (4.5 * (Convert.ToInt32(GridRoomPaxDetail.Compute("SUM(Price)", string.Empty)) / 100)))).ToString("#.##");
                    lblTotAMt.Text = Math.Round((Convert.ToDouble(GridRoomPaxDetail.Compute("SUM(Price)", string.Empty)) + (4.5 * (Convert.ToInt32(GridRoomPaxDetail.Compute("SUM(Price)", string.Empty)) / 100)))).ToString("#.##");
                    lblBalance.Text = Math.Round((Convert.ToDouble(lblTotAMt.Text) - Convert.ToDouble(lblTotPaid.Text))).ToString("#.##");


                    lbBalanceDueIn.Text = Convert.ToDateTime(lblArrvDate.Text).AddDays(-90).ToString("d MMMM, yyyy");


                    //gdvCruiseRooms.FooterRow.Cells[1].Text = "Total </br> Service Tax @ 4.50%";
                    //gdvCruiseRooms.FooterRow.Cells[3].Text = Convert.ToDouble(GridRoomPaxDetail.Compute("SUM(Price)", string.Empty)).ToString();
                    //lblTotAMt.Text = (Convert.ToDouble(GridRoomPaxDetail.Compute("SUM(Price)", string.Empty)) + (4.5 * (Convert.ToInt32(GridRoomPaxDetail.Compute("SUM(Price)", string.Empty)) / 100))).ToString();
                    //lblBalance.Text = (Convert.ToDouble(lblTotAMt.Text) - Convert.ToDouble(lblTotPaid.Text)).ToString();


                    Session["BookedRooms"] = null;
                    Session["PackageId"] = null;
                    Session["BookingRef"] = null;

                }





                lbRuppeeinwords.Text = NumbersToWords(Convert.ToInt32(lblTotAMt.Text));
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
            lbpackageName.Text = dtGetPackageDetails.Rows[0]["PackageName"].ToString();
            lbStrtEnd.Text = "Cruise starts from " + dtGetPackageDetails.Rows[0]["BoardFrom"].ToString() + "  and ends at " + dtGetPackageDetails.Rows[0]["BoardTo"].ToString();

            con.Close();
        }
        catch (Exception sqe)
        {

        }
    }

    private void GenrateBill(string transactionId)
    {
        this.InsertParentTableData();
        this.InsertChildTableData();
        sendMail(transactionId);

    }


    public static string NumbersToWords(int inputNumber)
    {
        int inputNo = inputNumber;

        if (inputNo == 0)
            return "Zero";

        int[] numbers = new int[4];
        int first = 0;
        int u, h, t;
        System.Text.StringBuilder sb = new System.Text.StringBuilder();

        if (inputNo < 0)
        {
            sb.Append("Minus ");
            inputNo = -inputNo;
        }

        string[] words0 = {"" ,"One ", "Two ", "Three ", "Four ",
            "Five " ,"Six ", "Seven ", "Eight ", "Nine "};
        string[] words1 = {"Ten ", "Eleven ", "Twelve ", "Thirteen ", "Fourteen ",
            "Fifteen ","Sixteen ","Seventeen ","Eighteen ", "Nineteen "};
        string[] words2 = {"Twenty ", "Thirty ", "Forty ", "Fifty ", "Sixty ",
            "Seventy ","Eighty ", "Ninety "};
        string[] words3 = { "Thousand ", "Lakh ", "Crore " };

        numbers[0] = inputNo % 1000; // units
        numbers[1] = inputNo / 1000;
        numbers[2] = inputNo / 100000;
        numbers[1] = numbers[1] - 100 * numbers[2]; // thousands
        numbers[3] = inputNo / 10000000; // crores
        numbers[2] = numbers[2] - 100 * numbers[3]; // lakhs

        for (int i = 3; i > 0; i--)
        {
            if (numbers[i] != 0)
            {
                first = i;
                break;
            }
        }
        for (int i = first; i >= 0; i--)
        {
            if (numbers[i] == 0) continue;
            u = numbers[i] % 10; // ones
            t = numbers[i] / 10;
            h = numbers[i] / 100; // hundreds
            t = t - 10 * h; // tens
            if (h > 0) sb.Append(words0[h] + "Hundred ");
            if (u > 0 || t > 0)
            {
                if (h > 0 || i == 0) sb.Append("and ");
                if (t == 0)
                    sb.Append(words0[u]);
                else if (t == 1)
                    sb.Append(words1[u]);
                else
                    sb.Append(words2[t - 2] + words0[u]);
            }
            if (i != 0) sb.Append(words3[i - 1]);
        }
        return "INR " + sb.ToString().TrimEnd() + " Only";
    }




    public void sendMail1()
    {
        try
        {
            MailMessage mail = new MailMessage();
            SmtpClient SmtpServer = new SmtpClient("adventureresortscruises.in");

            mail.From = new MailAddress("reservations@adventureresortscruises.in");
            if (Session["AgentMailId"] != null)
            {
                mail.To.Add(Session["AgentMailId"].ToString());
            }
            else
            {
                mail.To.Add(Session["CustMailId"].ToString());
            }
            mail.Subject = "adventureresortscruises";
            mail.Body = "your booking ref no is '" + Session["BookingRef"].ToString() + "'";

            SmtpServer.Port = 587;
            SmtpServer.Credentials = new System.Net.NetworkCredential("reservations@adventureresortscruises.in", "Augurs@123");
            SmtpServer.EnableSsl = false;

            SmtpServer.Send(mail);
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Showstatus", "javascript:alert('mail Sent')", true);
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Showstatus", "javascript:alert('" + ex.Message.ToString() + "')", true);
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
            lbBookingNo.Text = dtmaxId.Rows[0].ItemArray[0].ToString();
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






            DataTable Bookingdt;
            Bookingdt = new DataTable();
            Bookingdt = Session["Bookingdt"] as DataTable;
            //Response.Write(Bookingdt.Rows[0]["Total1"].ToString());

            //gdvSelectedRooms.DataSource = Bookingdt;
            //gdvSelectedRooms.DataBind();

            //gdvSelectedRooms.FooterRow.Cells[1].Text = "Total </br> Service Tax @ 4.50% </br> <b> Grand Total </b> ";

            //gdvSelectedRooms.FooterRow.Cells[3].Text = Math.Round(Convert.ToDouble(Bookingdt.Compute("SUM(Total1)", "[Total1] > 0"))).ToString("#.##") + "</br> " + Math.Round((4.5 * (Convert.ToInt32(Bookingdt.Compute("SUM(Total1)", "[Total1] > 0")) / 100))).ToString("#.##") + " </br> " + Math.Round((Convert.ToDouble(Bookingdt.Compute("SUM(Total1)", "[Total1] > 0")) + (4.5 * (Convert.ToInt32(Bookingdt.Compute("SUM(Total1)", "[Total1] > 0")) / 100)))).ToString("#.##");
            //Response.Write("def");

            //lbPax.Text = Convert.ToInt32(Bookingdt.Compute("SUM(Pax)", "[Pax] > 0")).ToString(); ;
            //lblTotAMt.Text = Math.Round((Convert.ToDouble(Bookingdt.Compute("SUM(Total1)", "[Total1] > 0")) + (4.5 * (Convert.ToInt32(Bookingdt.Compute("SUM(Total1)", "[Total1] > 0")) / 100)))).ToString("#.##");

            //lblBalance.Text = Math.Round((Convert.ToDouble(lblTotAMt.Text) - Convert.ToDouble(lblTotPaid.Text))).ToString("#.##");
            //lbBalanceDueIn.Text = Convert.ToDateTime(lblArrvDate.Text).AddDays(-90).ToString("d MMMM, yyyy");
            //hfBookingId.Value = MaxBookingId.ToString();

            //lblArrvDate.Text = Session["Chkin"].ToString();
            //lblDepartDate.Text = Session["chkout"].ToString(); ;
            //lblacm.Text = "Accomodation Name: " + Session["AccomName"].ToString();

            if (LoopInsertStatus == dtbooking.Rows.Count)
            {

                return 1;

            }
            else
            {
                return
                    0;
            }



        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Showstatus", "javascript:alert('" + ex.Message.ToString() + "')", true);
            return 0;
                  
        }


    }


    private void GenrateBill1(string transactionId)
    {

        this.InsertBookingTableData(iAccomId, iaccomtypeid, iagentid, bookref, chkin, chkout, dtbkdetails);

        this.InsertRoomBookingTableData(dtbkdetails, chkin, chkout, iAccomId);
        sendMail1();

    }


    protected void gdvCruiseRooms_RowDataBound(object sender, GridViewRowEventArgs e)
    {

    }
}
