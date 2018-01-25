using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FarHorizon.Reservations.BusinessServices;
using FarHorizon.Reservations.BusinessServices.Online.BAL;
using FarHorizon.Reservations.BusinessServices.Online.DAL;
using FarHorizon.Reservations.Common;
using FarHorizon.Reservations.MasterServices;
using System.Configuration;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Net.Http;
/// <summary>
/// Summary description for Cabin
/// </summary>
public class Cabin
{
    BALBooking blsr = new BALBooking();
    DALBooking dlsr = new DALBooking();
    DataTable dt;
    Check chk = new Check();
    public DataTable bindroomddl(string pakackageid, int departureId)
    {
        chk.name = "rajib";
        chk.surname = "sarkar";

        try
        {
            //using (var client = new HttpClient())
            //{
            //    Student p = new Student { StudName = "Sourav", StudCourse = "Kayal" };
            //    client.BaseAddress = new Uri("http://localhost:5024/");
            //    var response = client.PostAsJsonAsync("api/Values", chk).Result;
            //    if (response.IsSuccessStatusCode)
            //    {
            //        Console.Write("Success");
            //    }
            //    else
            //        Console.Write("Error");
            //}
            blsr.action = "GetcruiseRooms";
            blsr.PackageId = pakackageid;
            blsr.DepartureId = departureId;
            //blsr._iAgentId = usercode;
            dt = new DataTable();
            dt = dlsr.GetCruiseRooms(blsr);
            if (dt != null)
            {

                return dt;
            }
            else
            {
                return null;

            }
        }
        catch
        {
            return null;
        }
    }


    public void LockTheBooking(string roomno, int accoid, int roomcatid)
    {
        try
        {
            int lockDuration = ConfigurationManager.AppSettings["LockDuration"] != null ? Convert.ToInt16(ConfigurationManager.AppSettings["LockDuration"]) : 10;
            BALBookingLock bl = new BALBookingLock();

            int accomId = accoid;

            Guid uniqueIdentifier = Guid.NewGuid();

            bl.AccomId = accomId;
            bl.LockIdentifier = uniqueIdentifier.ToString();
            bl.LockExpireAt = DateTime.Now.AddMinutes(lockDuration);
            bl.rooms = roomno;
            bl.roocatid = roomcatid;

            bl.LockRooms = new List<LockRoom>();
            //foreach (var lockRoom in bl.LockRooms)
            //{

            LockRoom lr = new LockRoom { RoomCategoryId = Convert.ToInt16(roomcatid), RoomNo = roomno.ToString() };
            bl.LockRooms.Add(lr);

            //}
            //foreach (var lockRoom in bl.LockRooms)
            //{
                DALBookingLock dbl = new DALBookingLock();
                dbl.PlaceLock(bl);
            //}

        }
        catch (Exception exp)
        {

            throw exp;
        }
    }
    public DataTable loadbookingDetails(string Packagename, int bookingId, int pax, string roomno, string paymentmethod, DateTime chekindate, DateTime chekoutdate, string packageid, int totpax, int roomcatid, int noOfnight)
    {
        decimal amount = 0;
        BALBooking booking = dlsr.GetBookingDetails(bookingId);
        DataTable dt = bindRoomRates(chekindate, packageid, totpax);
        DataView dv = new DataView(dt);
        dv = new DataView(dt, "roomcategoryid='" + roomcatid + "'", "roomcategoryid", DataViewRowState.CurrentRows);
        DataTable dt1 = dv.ToTable();
        DataTable dtNew = new DataTable();
        dtNew.Columns.Add("Packagename", typeof(string));
        dtNew.Columns.Add("BookingId", typeof(int));
        dtNew.Columns.Add("Totalguest", typeof(int));
        dtNew.Columns.Add("CheckInDate", typeof(string));
        dtNew.Columns.Add("CheckOutDate", typeof(String));
        dtNew.Columns.Add("NoOFNights", typeof(int));
        dtNew.Columns.Add("TotalPrice", typeof(Decimal));
        dtNew.Columns.Add("RoomType", typeof(String));
        dtNew.Columns.Add("Cabincategory", typeof(String));
        dtNew.Columns.Add("DefaultNoOfBed", typeof(string));
        if (dt1.Rows[0]["RmType"].ToString() == "Twin")
        {

            //string strContent = dt1.Rows[0]["Price Per Person on Twin Sharing inclusive of taxes"].ToString().Replace("INR", string.Empty);
            amount = Convert.ToDecimal(dt1.Rows[0]["Price Per Person on Twin Sharing inclusive of taxes"].ToString().Replace("INR", string.Empty)) * Convert.ToDecimal(totpax);
        }
        else if (dt1.Rows[0]["RmType"].ToString() == "Double")
        {
            amount = Convert.ToDecimal(dt1.Rows[0]["Price Per Person on Twin Sharing inclusive of taxes"].ToString().Replace("INR", string.Empty)) * Convert.ToDecimal(totpax);
        }
        dtNew.Rows.Add(Packagename, bookingId, pax, chekindate, chekoutdate, noOfnight, amount, dt1.Rows[0]["RmType"].ToString(), dt1.Rows[0]["Cabin Category"].ToString(), dt1.Rows[0]["DefaultNoOfBeds"].ToString());
        return dtNew;
    }
    public void InsertChildTableData(int bookingId, int pax, string roomno, string paymentmethod, DateTime chekindate, string packageid, int totpax, int roomcatid)
    {
        BALBooking blsr = new BALBooking();
        DALBooking dlsr = new DALBooking();

        BALBooking booking = dlsr.GetBookingDetails(bookingId);
        DataTable dt = bindRoomRates(chekindate, packageid, totpax);
        DataView dv = new DataView(dt);
        dv = new DataView(dt, "roomcategoryid='" + roomcatid + "'", "roomcategoryid", DataViewRowState.CurrentRows);
        DataTable dt1 = dv.ToTable();
        if (booking != null)
        {
            //DataTable GridRoomPaxDetail = SessionServices.RetrieveSession<DataTable>("BookedRooms");
            int LoopInsertStatus = 0;
            try
            {
                blsr._iBookingId = bookingId;

                blsr._iAccomId = booking._iAccomId;
                blsr._dtStartDate = booking._dtStartDate;
                blsr._dtEndDate = booking._dtEndDate;
                blsr._iPaxStaying = pax;
                if (dt1.Rows[0]["RmType"].ToString() == "Twin")
                {
                    blsr._bConvertTo_Double_Twin = dt1.Rows[0]["RmType"].ToString() == "Twin" ? true : false;
                    //string strContent = dt1.Rows[0]["Price Per Person on Twin Sharing inclusive of taxes"].ToString().Replace("INR", string.Empty);
                    blsr._Amt = Convert.ToDecimal(dt1.Rows[0]["Price Per Person on Twin Sharing inclusive of taxes"].ToString().Replace("INR", string.Empty)) * Convert.ToDecimal(totpax);
                }
                else if (dt1.Rows[0]["RmType"].ToString() == "Double")
                {
                    blsr._bConvertTo_Double_Twin = dt1.Rows[0]["RmType"].ToString() == "Double" ? true : false;
                }
                blsr._cRoomStatus = "B";
                blsr._sRoomNo = roomno;
                blsr.action = "AddPriceDetailsToo";
                string BookingPayId = paymentmethod.Trim().Substring(0, 2) + DateTime.Now.ToString("MMddhhmmssfff");
                blsr.PaymentId = BookingPayId;
                //blsr._Amt = Convert.ToDecimal(GridRoomPaxDetail.Rows[LoopCounter]["Price"].ToString());

                int GetQueryResponse = dlsr.AddRoomBookingDetails(blsr);
                if (GetQueryResponse > 0)
                {
                    LoopInsertStatus++;
                }

            }
            catch (Exception e)
            {
                throw;
            }
        }
    }

    public DataTable bindRoomRates(DateTime checkindate, string packageid, int totpax)
    {
        try
        {

            blsr.action = "RoomRatesCustAgent";

            blsr._dtStartDate = checkindate;

            blsr.PackageId = packageid;

            blsr.totpax = 0;

            blsr.totpax = totpax;

            dt = dlsr.GetRoomCategoryWiseRates(blsr);

            DataView dv = dt.DefaultView;
            dv.Sort = "PPRoomRate asc";
            DataTable sortedDT = dv.ToTable();


        }
        catch (Exception exp)
        {

        }
        return dt;
    }
    public int InsertParentTableData(string packageid, DateTime chekindate, DateTime checkoutdate, int pax, string bRef, int Agentid)
    {
        BALBooking blsr = new BALBooking();
        DALBooking dlsr = new DALBooking();

        try
        {

            //blsr._iAgentId = 247;
            blsr._iAgentId = Agentid;
            blsr.CustomerId = "0";


            blsr.action = "GetDepartureDetails";
            blsr._iBookingId = 0;
            blsr.PackageId = packageid;
            DataTable dtDepartureDetails = dlsr.GetDepartureDetails(blsr);

            DateTime startDate = chekindate;
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

            blsr._iPersons = pax;
            blsr._sBookingRef = bRef;
            blsr._dtStartDate = Convert.ToDateTime(packageRow["CheckInDate"]);
            blsr._dtEndDate = checkoutdate;
            blsr._iAccomTypeId = Convert.ToInt32(packageRow["AccomTypeId"]);
            blsr._iAccomId = Convert.ToInt32(packageRow["AccomId"]);

            blsr._iNights = Convert.ToInt32(packageRow["NoOfNights"]);
            blsr._BookingStatusId = (int)BookingStatusTypes.BOOKED; //This is a proposed booking and it will be marked as booked on the next page once the payment is received.
            blsr._SeriesId = 0;
            blsr._proposedBooking = true;
            blsr._chartered = false;

            //Session.Add("tblBookingBAL", blsr);

            //SessionServices.SaveSession<BALBooking>("tblBookingBAL", blsr);
            int iBRC = dlsr.GetBookingReferenceCount(blsr);

            if (iBRC > 0)
            {
                //System.Web.UI.ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "AlertBox", "alert('The Booking Reference mentioned by you is not unique. Please enter a different reference number.');", true);
                return -1;
            }
            int bookingId = dlsr.AddParentBookingDetail(blsr);

            //var bookingDetails = dlsr.GetBookingDetails(bookingId);
            //if (bookingDetails != null)
            //{
            //    blsr.BookingCode = bookingDetails.BookingCode;
            //}
            blsr._iBookingId = bookingId;

            //SessionServices.SaveSession<BALBooking>("tblBookingBAL", blsr);
            return bookingId;
        }
        catch
        {
            throw;
        }
    }
}
