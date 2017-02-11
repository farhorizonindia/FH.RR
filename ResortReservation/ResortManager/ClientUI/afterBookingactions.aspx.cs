using FarHorizon.Reservations.Bases.BasePages;
using FarHorizon.Reservations.BusinessServices;
using FarHorizon.Reservations.Common;
using FarHorizon.Reservations.Common.DataEntities.Client;
using System;
using System.Text;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

public partial class ClientUI_afterBookingactions : ClientBasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string status = "";

        int iBookingId = 0;
        int iAccomodationId = 0;
        bool updated = false;

        int.TryParse(Convert.ToString(Request.QueryString["bid"]), out iBookingId);
        status = Convert.ToString(Request.QueryString["bstatus"]);

        if (Request.QueryString["aid"] != null)
        {
            int.TryParse(Convert.ToString(Request.QueryString["aid"]), out iAccomodationId);
        }
        if (Request.QueryString["updated"] != null)
        {
            updated = Convert.ToBoolean(Request.QueryString["updated"]);
        }

        if (!IsPostBack)
        {
            SendEventEmail(iBookingId, status, updated);
            DisplayBookingResult(iBookingId, status);
            ShowBookingDetails(iBookingId);

            if (status == "confirmed")
            {
                lblSubject.Text = "Booking Confirmed";
                if (SessionServices.RetrieveSession("confirmDet") == null)
                {
                    Table tblparent = new Table();
                    TableRow tr;
                    TableCell tc;

                    tr = new TableRow();
                    tc = new TableCell();

                    tc.Controls.Add(BookDetailsformail(iBookingId, ""));
                    tr.Cells.Add(tc);
                    tblparent.Rows.Add(tr);
                    tr = new TableRow();
                    tc = new TableCell();
                    tc.Controls.Add(BookingConfirmRoomDetailsformail(iBookingId, ""));
                    tr.Cells.Add(tc);
                    tblparent.Rows.Add(tr);

                    tr = new TableRow();
                    tc = new TableCell();
                    tc.Controls.Add(Bookconfirmformail(iBookingId, ""));
                    tr.Cells.Add(tc);
                    tblparent.Rows.Add(tr);
                    PnlMailformat.Controls.Add(tblparent);

                }
                else
                {
                    Table tblparent = new Table();
                    TableRow tr;
                    TableCell tc;

                    tr = new TableRow();
                    tc = new TableCell();
                    tc.ColumnSpan = 2;
                    tc.Controls.Add(BookDetailsformail(iBookingId, ""));
                    tr.Cells.Add(tc);
                    tblparent.Rows.Add(tr);
                    BookingDTO[] objcon = SessionServices.RetrieveSession("confirmDet") as BookingDTO[];

                    tr = new TableRow();
                    tc = new TableCell();
                    tc.Controls.Add(PrepareConfirmMail(objcon, "Previous Details"));
                    tr.Cells.Add(tc);
                    tblparent.Rows.Add(tr);
                    tr = new TableRow();
                    tc = new TableCell();
                    tc.Controls.Add(Bookconfirmformail(iBookingId, "Updated Details"));
                    tr.Cells.Add(tc);
                    tblparent.Rows.Add(tr);
                    PnlMailformat.Controls.Add(tblparent);
                }
            }

            if ((status == "waitlisted" || status == "booked") && updated == false)
            {
                lblSubject.Text = "Booking New";
                Table tblparent = new Table();
                TableRow tr;
                TableCell tc;

                tr = new TableRow();
                tc = new TableCell();
                tc.Controls.Add(BookDetailsformail(iBookingId, ""));
                tr.Cells.Add(tc);
                tblparent.Rows.Add(tr);
                tr = new TableRow();
                tc = new TableCell();
                tc.Controls.Add(BookingRoomDetailsformail(iBookingId, ""));
                tr.Cells.Add(tc);
                tblparent.Rows.Add(tr);
                PnlMailformat.Controls.Add(tblparent);
            }
            if ((status == "waitlisted" || status == "booked") && updated == true)
            {
                lblSubject.Text = "Booking Updated";
                BookingDTMail[] obd = SessionServices.RetrieveSession("BookDetMail") as BookingDTMail[];
                BookingRoomReportsDTO[] orrbd = SessionServices.RetrieveSession("BookroomDetmail") as BookingRoomReportsDTO[];

                Table tblparent = new Table();
                TableRow tr;
                TableCell tc;

                tr = new TableRow();
                tc = new TableCell();
                tc.Controls.Add(PrepareBookindDetailsMail(obd, "Previous Details"));
                tr.Cells.Add(tc);

                //   tr = new TableRow();
                tc = new TableCell();
                tc.Controls.Add(BookDetailsformail(iBookingId, "Updated Details"));
                tr.Cells.Add(tc);
                tblparent.Rows.Add(tr);
                
                //PrepareBookindDetailsMail(obd, "Previous Details");
                //BookDetailsformail(iBookingId, "Updated Details");

                tr = new TableRow();
                tc = new TableCell();
                tc.Style.Add("vertical-align", "top");
                tc.Controls.Add(PrepareRoomBookingReport(orrbd, "Previous Details"));
                tr.Cells.Add(tc);
                tc = new TableCell();
                tc.Style.Add("vertical-align", "top");
                tc.Controls.Add(BookingRoomDetailsformail(iBookingId, "Updated Details"));

                tr.Cells.Add(tc);

                tblparent.Rows.Add(tr);

                PnlMailformat.Controls.Add(tblparent);
            }

            if (status == "cancelled")
            {
                lblSubject.Text = "Booking cancelled";

                Table tblparent = new Table();
                TableRow tr;
                TableCell tc;

                tr = new TableRow();
                tc = new TableCell();
                tc.Controls.Add(BookDetailsformail(iBookingId, ""));
                tr.Cells.Add(tc);
                tblparent.Rows.Add(tr);
                PnlMailformat.Controls.Add(tblparent);
            }

            ShowReleasedRooms(iBookingId);
            ShowOtherBookingsWithinCurrentBookingDates(iBookingId);
            ClearSessions();
        }
    }

    #region Status Manager
    private void DisplayBookingResult(int BookingId, string status)
    {
        if (BookingId > 0)
        {
            lblBookingStatus.Style[HtmlTextWriterStyle.FontSize] = "x-small";
            switch (status)
            {
                case "booked":
                    Status_Booked(BookingId);
                    break;
                case "waitlisted":
                    Status_Waitlisted(BookingId);
                    break;
                case "confirmed":
                    Status_Confirmed(BookingId);
                    break;
                case "cancelled":
                    Status_Cancelled(BookingId);
                    break;
                case "confirmation_cancelled":
                    Status_Confirmation_Cancelled(BookingId);
                    break;
                case "deleted":
                    Status_Deleted(BookingId);
                    break;
            }
        }
        else
        {
            lblBookingStatus.Text = "The booking has not been updated successfully";
            //lblBookingStatus.ForeColor = System.Drawing.Color.Green;
        }
    }

    private void Status_Booked(int iBookingId)
    {
        StringBuilder strComment = new StringBuilder();
        lblBookingStatus.Text = "The booking has been updated successfully.";
        lblBookingStatus.ForeColor = System.Drawing.Color.Orange;
        lblBookingDetails.Text = "";
        //        strComment.Append("You can perform following operations");
        strComment.Append("<br><a href='Booking.aspx?bid=" + Convert.ToString(iBookingId) + "'>View/Edit </a>");
        strComment.Append(" | <a href='bookingconfirmation.aspx?bid=" + Convert.ToString(iBookingId) + "'>Confirm</a>");
        //strComment.Append("<br> <a href='touristDetails.aspx?bid=" + Convert.ToString(_iBookingId) + "'>Add Tourists</a><br>");
        //strComment.Append("<br> <a href='ViewTourists.aspx?bid=" + Convert.ToString(_iBookingId) + "'>View Tourists</a><br>");
        strComment.Append(" | <a href='Booking.aspx'>New Reservation</a>");
        strComment.Append(" | <a href='ViewBookings.aspx'>Current Reservations</a>");
        lblComment.Text = Convert.ToString(strComment);
        strComment = null;
    }

    private void Status_Confirmed(int iBookingId)
    {
        StringBuilder strComment = new StringBuilder();
        lblBookingStatus.Text = "The booking has been confirmed successfully. The booking is in CONFIRMED state.";
        lblBookingStatus.ForeColor = System.Drawing.Color.Green;
        lblBookingDetails.Text = "";
        strComment.Append("<br><a href='touristDetails.aspx?bid=" + Convert.ToString(iBookingId) + "'>Add Tourists</a>");
        strComment.Append("| <a href='ViewTourists.aspx?bid=" + Convert.ToString(iBookingId) + "'>View Tourists</a>");
        strComment.Append("| <a href='Booking.aspx'>Add New Booking</a><br>");
        strComment.Append(" | <a href='ViewBookings.aspx'>Current Reservations</a>");
        lblComment.Text = Convert.ToString(strComment);
        strComment = null;
    }

    private void Status_Waitlisted(int iBookingId)
    {
        StringBuilder strComment = new StringBuilder();
        lblBookingStatus.Text = "The booking has been updated successfully. The booking is in WAIT LISTED state.";
        lblBookingStatus.ForeColor = System.Drawing.Color.Orange;
        lblBookingDetails.Text = "";
        //strComment.Append("You can perform following operations");
        strComment.Append("<br> <br><a href='Booking.aspx?bid=" + Convert.ToString(iBookingId) + "'>View/Edit Booking</a>");
        //strComment.Append("| <a href='bookingconfirmation.aspx?bid=" + Convert.ToString(iBookingId) + "'>Confirm Booking</a>");
        //strComment.Append("<br> <a href='touristDetails.aspx?bid=" + Convert.ToString(_iBookingId) + "'>Add Tourists</a><br>");
        //strComment.Append("<br> <a href='ViewTourists.aspx?bid=" + Convert.ToString(_iBookingId) + "'>View Tourists</a><br>");
        strComment.Append("| <a href='Booking.aspx'>Add New Booking</a>");
        strComment.Append(" | <a href='ViewBookings.aspx'>Current Reservations</a>");
        lblComment.Text = Convert.ToString(strComment);
        strComment = null;
    }

    private void Status_Cancelled(int iBookingId)
    {
        StringBuilder strComment = new StringBuilder();
        lblBookingStatus.Text = "The booking has been cancelled successfully. The booking is in CANCELLED state.";
        lblBookingStatus.ForeColor = System.Drawing.Color.Green;
        lblBookingDetails.Text = "";
        //strComment.Append("You can perform following operations");
        // strComment.Append("<br> <br><a href='Booking.aspx?bid=" + Convert.ToString(_iBookingId) + "'>View/Edit Booking</a><br>");
        //strComment.Append("<br> <a href='bookingconfirmation.aspx?bid=" + Convert.ToString(iBookingId) + "'>Confirm Booking</a>");
        //strComment.Append(" | <a href='touristDetails.aspx?bid=" + Convert.ToString(iBookingId) + "'>Add Tourists</a>");
        //strComment.Append(" | <a href='ViewTourists.aspx?bid=" + Convert.ToString(iBookingId) + "'>View Tourists</a>");
        strComment.Append(" | <a href='Booking.aspx'>Add New Booking</a><br>");
        strComment.Append(" | <a href='ViewBookings.aspx'>Current Reservations</a>");
        lblComment.Text = Convert.ToString(strComment);
        strComment = null;
        Table tbl = PrepareBlockedBookingData(iBookingId);
        if (tbl != null)
            AddControlToPanel(tbl);
    }

    private void Status_Confirmation_Cancelled(int iBookingId)
    {
        StringBuilder strComment = new StringBuilder();
        lblBookingStatus.Text = "The confirmation has been cancelled successfully. ";
        lblBookingStatus.ForeColor = System.Drawing.Color.Blue;
        lblBookingDetails.Text = "";
        strComment.Append("You can perform following operations");
        strComment.Append("<br><a href='Booking.aspx?bid=" + Convert.ToString(iBookingId) + "'>View/Edit Booking</a>");
        strComment.Append(" | <a href='bookingconfirmation.aspx?bid=" + Convert.ToString(iBookingId) + "'>Confirm Booking</a>");
        strComment.Append(" | <a href='touristDetails.aspx?bid=" + Convert.ToString(iBookingId) + "'>Add Tourists</a>");
        strComment.Append(" | <a href='ViewBookings.aspx'>Current Reservations</a>");
        //strComment.Append("<br> <a href='ViewTourists.aspx?bid=" + Convert.ToString(_iBookingId) + "'>View Tourists</a><br>");
        //strComment.Append("<br> <a href='Booking.aspx'>Add New Booking</a><br>");
        lblComment.Text = Convert.ToString(strComment);
        strComment = null;
        Table tbl = PrepareBlockedBookingData(iBookingId);
        if (tbl != null)
            AddControlToPanel(tbl);
    }
    #endregion Status Manager

    #region Event Handler Methods
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

    #endregion

    #region ShowReleasedRooms
    private void ShowReleasedRooms(int BookingId)
    {
        BookingServices BookingManager = new BookingServices();
        Accomodation accomodation;
        accomodation = BookingManager.GetReleasedRooms(BookingId);

        #region Add Released Rooms to Page
        HtmlGenericControl divCategory = new HtmlGenericControl("div");
        HtmlGenericControl divRoomType = new HtmlGenericControl("div");
        HtmlGenericControl divRoom = new HtmlGenericControl("div");
        HtmlGenericControl divRooms = new HtmlGenericControl("div");
        HtmlGenericControl divClearingDiv = new HtmlGenericControl("div");

        HtmlGenericControl divWaitListedBookings = new HtmlGenericControl("div");

        if (accomodation != null && accomodation.Categories != null)
        {
            foreach (AccomodationRoomCategory Category in accomodation.Categories)
            {
                divCategory = new HtmlGenericControl("div");
                divCategory.Controls.Add(AddCategory(Category));
                if (Category.RoomTypes != null)
                {
                    foreach (AccomodationRoomType RoomType in Category.RoomTypes)
                    {
                        divRoomType = new HtmlGenericControl("div");
                        divRoomType.Controls.Add(AddRoomType(RoomType));
                        if (RoomType.Rooms != null)
                        {
                            divRooms = new HtmlGenericControl("div");
                            divRooms.Controls.Add(AddRooms("Released Rooms: "));
                            foreach (AccomodationRoom Room in RoomType.Rooms)
                            {
                                divRoom = AddRoom(Room);
                                divRooms.Controls.Add(divRoom);
                            }
                            divClearingDiv = AddClearingDiv();
                            divRooms.Controls.Add(divClearingDiv);
                            divRoomType.Controls.Add(divRooms);
                        }
                        divWaitListedBookings = GetWaitListedBookingsDiv(BookingId, Category.RoomCategory.RoomCategoryId, RoomType.RoomType.RoomTypeId);
                        divCategory.Controls.Add(divRoomType);
                        divCategory.Controls.Add(divWaitListedBookings);
                        AddToReleasedRoomsPanel(divCategory);
                    }
                }
            }
        }
        #endregion
    }


    private HtmlGenericControl GetWaitListedBookingsDiv(int BookingId, int RoomCategoryId, int RoomTypeId)
    {
        BookingServices BookingManager = new BookingServices();
        BookingDTO[] BookingDTO = null;
        HtmlGenericControl divWaitListedBookings = new HtmlGenericControl("div");
        HtmlGenericControl divWaitListedBooking;
        string sLink;
        BookingDTO = BookingManager.GetWaitlistedBookingsForReleasedCatType(BookingId, RoomCategoryId, RoomTypeId);

        if (BookingDTO != null && BookingDTO.Length > 0)
        {
            divWaitListedBookings.Controls.Add(AddWaitListedBookings());
            for (int i = 0; i < BookingDTO.Length; i++)
            {
                sLink = "<a href='waitListmanagement.aspx";
                sLink += "?bid=" + BookingDTO[i].BookingId.ToString();
                sLink += "&cid=" + BookingDTO[i].StartDate.Year.ToString("0000") + BookingDTO[i].StartDate.Month.ToString("00") + BookingDTO[i].StartDate.Day.ToString("00");
                sLink += "&cod=" + BookingDTO[i].EndDate.Year.ToString("0000") + BookingDTO[i].EndDate.Month.ToString("00") + BookingDTO[i].EndDate.Day.ToString("00");
                sLink += "&accomid=" + BookingDTO[i].AccomodationId.ToString();
                sLink += "'>" + BookingDTO[i].BookingReference + "</a>";
                divWaitListedBooking = new HtmlGenericControl("div");
                divWaitListedBooking.Attributes.Add("class", "waitListedBooking");
                divWaitListedBooking.InnerHtml = sLink;
                divWaitListedBookings.Controls.Add(divWaitListedBooking);
            }
            divWaitListedBookings.Controls.Add(AddClearingDiv());
        }
        return divWaitListedBookings;
    }

    private HtmlGenericControl AddWaitListedBookings()
    {
        HtmlGenericControl divWaitListedBookings = new HtmlGenericControl("div");
        divWaitListedBookings.Attributes.Add("class", "waitListedBookings");
        divWaitListedBookings.InnerText = "Wait Listed bookings: Following booking(s) can use these released rooms. Click to go to waitlist management.";
        return divWaitListedBookings;
    }

    private HtmlGenericControl AddCategory(AccomodationRoomCategory Category)
    {
        HtmlGenericControl div = new HtmlGenericControl("div");
        if (Category != null && Category.RoomCategory != null)
        {
            div.InnerText = "Category: " + Category.RoomCategory.RoomCategory;
            div.Attributes.Add("class", "category");
            //AddToReleasedRoomsPanel(div);
        }
        return div;
    }

    private HtmlGenericControl AddRoomType(AccomodationRoomType RoomType)
    {
        HtmlGenericControl div = new HtmlGenericControl("div");
        if (RoomType != null && RoomType.RoomType != null)
        {
            div.InnerText = "Room Type: " + RoomType.RoomType.RoomType;
            div.Attributes.Add("class", "roomType");
            //AddToReleasedRoomsPanel(div);
        }
        return div;
    }

    private HtmlGenericControl AddRooms(string RelasedRooms)
    {
        HtmlGenericControl div = new HtmlGenericControl("div");
        div.InnerText = RelasedRooms;
        div.Attributes.Add("class", "rooms");
        //AddToReleasedRoomsPanel(div);
        return div;
    }

    private HtmlGenericControl AddRoom(AccomodationRoom Room)
    {
        HtmlGenericControl div = new HtmlGenericControl("div");
        if (Room != null && Room.RoomDetail != null)
        {
            div.InnerText = Room.RoomDetail.RoomNo;
            div.Attributes.Add("class", "room");
            //AddToReleasedRoomsPanel(div);
        }
        return div;
    }

    private HtmlGenericControl AddClearingDiv()
    {
        HtmlGenericControl div = new HtmlGenericControl("div");
        div.Attributes.Add("class", "clearingDiv");
        //AddToReleasedRoomsPanel(div);
        return div;
    }

    private void AddToReleasedRoomsPanel(HtmlGenericControl ctrl)
    {
        pnlReleasedRooms.Controls.Add(ctrl);
    }
    #endregion

    #region Private Helper Methods
    private BookingWaitListDTO[] GetBlockedBookings(int BookingId)
    {
        BookingWaitListDTO[] oBlockedBooking = null;
        BookingServices oBookingManager = new BookingServices();
        oBlockedBooking = oBookingManager.GetBlockedBookings(BookingId);
        return oBlockedBooking;
    }

    private Table PrepareBlockedBookingData(int BookingId)
    {
        BookingWaitListDTO[] oBlockedBooking = null;
        oBlockedBooking = GetBlockedBookings(BookingId);
        Table tblMain = new Table();
        tblMain.Width = 600;
        Table tblRoomDetails = new Table();
        tblRoomDetails.Width = 600;
        int iPrevBookingID = 0;
        string pnlID = "";
        bool bWritten = false;

        TableRow trMain = null;
        TableCell tc = null;
        for (int i = 0; i < oBlockedBooking.Length; i++)
        {
            //if (oBlockedBooking[i].BookingType == 'X')
            {
                // prepare a row of the current record showing its bookingref, category, 
                //roomtype and no_of_rooms_freed by this id
                pnlID = oBlockedBooking[i].RoomCategory + "" + oBlockedBooking[i].RoomType;
                if (oBlockedBooking[i].BookingId != iPrevBookingID)
                {
                    if (tblRoomDetails.Rows.Count > 0)
                    {
                        Panel p = new Panel();
                        p.ID = pnlID;
                        p = AddTableToPanel(p, tblRoomDetails);
                        trMain = new TableRow();
                        tc = new TableCell();
                        tc.Controls.Add(p);
                        trMain.Cells.Add(tc);
                        tblMain.Rows.Add(trMain);
                    }
                    //if (oBlockedBooking[i].BookingId == BookingId)
                    {
                        if (oBlockedBooking[i].BookingType == 'X')
                        {
                            trMain = new TableRow();
                            tc = SetCellData("Booking Details Of Cancelled Booking");
                            tc.ColumnSpan = 6;
                            trMain.Cells.Add(tc);
                            tblMain.Rows.Add(trMain);
                        }
                    }
                    //else if (oBlockedBooking[i].BookingId != BookingId)
                    {
                        if (oBlockedBooking[i].BookingType == Constants.WAITLISTED)
                        {
                            if (bWritten == false)
                            {
                                trMain = new TableRow();
                                tc = SetCellData("Bookings that can be confirmed in lieu of the cancelled booking");
                                tc.ColumnSpan = 6;
                                trMain.Cells.Add(tc);
                                tblMain.Rows.Add(trMain);
                                bWritten = true;
                            }
                        }
                    }
                    trMain = new TableRow();
                    tc = SetCellData("Booking Reference : " + "<a href='#'>" + oBlockedBooking[i].BookingRef.ToString() + "</a>");
                    tc.ColumnSpan = 6;
                    trMain.Cells.Add(tc);
                    //tc = SetCellData("<a href='#'>" + oBlockedBooking[i].BookingRef.ToString() + "</a>");
                    //trMain.Cells.Add(tc);
                    tblMain.Rows.Add(trMain);
                    iPrevBookingID = oBlockedBooking[i].BookingId;

                    tblRoomDetails = new Table();
                }
                trMain = new TableRow();
                trMain.Cells.Add(SetCellData(oBlockedBooking[i].RoomCategory.ToString()));
                trMain.Cells.Add(SetCellData(oBlockedBooking[i].RoomType.ToString()));
                trMain.Cells.Add(SetCellData(oBlockedBooking[i].No_Of_RoomsWaitListed.ToString()));
                tblRoomDetails.Rows.Add(trMain);
            }
        }
        Panel p1 = new Panel();
        p1.ID = pnlID;
        p1 = AddTableToPanel(p1, tblRoomDetails);
        trMain = new TableRow();
        tc = new TableCell();
        tc.Controls.Add(p1);
        trMain.Cells.Add(tc);
        tblMain.Rows.Add(trMain);

        return tblMain;
    }
    private void AddControlToPanel(Table tbl)
    {
        pnlBlockedBookings.Controls.Clear();
        pnlBlockedBookings.Controls.Add(tbl);
    }
    private Panel AddTableToPanel(Panel p, Table tbl)
    {
        p.Controls.Clear();
        p.Controls.Add(tbl);
        return p;
    }
    private void Status_Deleted(int iBookingId)
    {
        StringBuilder strComment = new StringBuilder();
        lblBookingStatus.Text = "The booking has been deleted successfully. The booking is in DELETED state.";
        lblBookingStatus.ForeColor = System.Drawing.Color.Green;
        lblBookingDetails.Text = "";
        strComment.Append("You can perform following operations");
        strComment.Append("<br> <a href='Booking.aspx'>Add New Booking</a><br>");
        lblComment.Text = Convert.ToString(strComment);
        strComment = null;
    }
    private void ClearSessions()
    {
        SessionServices.DeleteSession(Constants._Booking_TotalNights);
        SessionServices.DeleteSession(Constants._Booking_AccomodationData);
        SessionServices.DeleteSession(Constants._Booking_AllRoomsData);
    }

    private TableCell SetCellData(string strData)
    {
        TableCell tc = new TableCell();
        tc.Width = 200;
        tc.Text = strData;
        return tc;
    }
    private Panel AddRowsToPanel(TableRow tr, string PanelID)
    {
        Panel p = new Panel();
        p.ID = PanelID;
        Table tbl = new Table();
        tbl.Rows.Add(tr);
        p.Controls.Add(tbl);
        return p;
    }

    private void ShowBookingDetails(int BookingId)
    {
        BookingRoomReportsDTO[] oBRRD = null;
        BookingReportServices oBRM = new BookingReportServices();
        oBRRD = oBRM.GetDetailedBookingDetails(BookingId);
        if (oBRRD != null)
        {
            if (oBRRD.Length > 0)
            {
                PrepareBookingReport(oBRRD);
            }
        }
    }

    private Table Bookconfirmformail(int bookingId, string st)
    {
        BookingDTO[] obd = null;
        BookingServices oBRM = new BookingServices();
        obd = oBRM.GetConfirmMailDetails(bookingId);
        if (obd != null)
        {
            if (obd != null)
            {
                return PrepareConfirmMail(obd, st);
            }
        }
        return null;

    }

    private Table PrepareConfirmMail(BookingDTO[] obd, string st)
    {
        Table tblMain = new Table();
        tblMain.Style.Add("border-collapse", "collapse");
        TableRow tr;
        TableCell tc;
        if (st != "")
        {


            tr = new TableRow();
            //if (st == "Previous Details")
            //{
            //    tc = new TableCell();
            //    tc.Text = "Details";
            //    tr.Cells.Add(tc);
            //}



            tc = new TableCell();
            tc.ColumnSpan = 6;
            tc.Text = st;
            tc.Style[HtmlTextWriterStyle.FontWeight] = "Bold";
            tr.Width = 500;
            tr.Cells.Add(tc);
            tblMain.Rows.Add(tr);

        }


        tblMain.Width = 700;
        tblMain.Attributes.Add("Border", "1px solid black");
        tr = new TableRow();
        tc = new TableCell();
        tc.Text = "Booking Id:";
        tc.Style[HtmlTextWriterStyle.FontWeight] = "Bold";
        tc.Width = 80;
        tr.Cells.Add(tc);
        tc = new TableCell();
        tc.Text = obd[0].BookingCode;
        tc.Width = 80;
        tr.Cells.Add(tc);
        tc = new TableCell();
        tc.Text = "Voucher No.";
        tc.Width = 80;
        tr.Cells.Add(tc);
        tc = new TableCell();
        tc.Text = obd[0].ExchangeOrderNo;
        tc.Width = 80;
        tr.Cells.Add(tc);

        tc = new TableCell();
        tc.Text = "Tour No:";
        tc.Width = 80;
        tr.Cells.Add(tc);
        tc = new TableCell();
        tc.Text = obd[0].TourId;
        tc.Width = 80;
        tr.Cells.Add(tc);
        tblMain.Rows.Add(tr);

        tr = new TableRow();
        tc = new TableCell();
        tc.Text = "Arrival Details";
        tc.ColumnSpan = 2;
        tc.Style[HtmlTextWriterStyle.FontWeight] = "Bold";
        tc.Width = 250;
        tr.Cells.Add(tc);



        tc = new TableCell();
        tc.Text = "Departure Details";
        tc.ColumnSpan = 4;
        tc.Style[HtmlTextWriterStyle.FontWeight] = "Bold";
        tc.Width = 250;
        tr.Cells.Add(tc);
        tblMain.Rows.Add(tr);

        tr = new TableRow();
        tc = new TableCell();
        tc.Text = "Date Time";

        tc.Width = 125;
        tr.Cells.Add(tc);
        tc = new TableCell();

        tc.Text = obd[0].ArrivalDateTime.ToString("dd-MMM-yyyy");

        tc.Width = 125;
        tr.Cells.Add(tc);
        tc = new TableCell();
        tc.Text = "Date Time";

        tc.Width = 125;
        tr.Cells.Add(tc);
        tc = new TableCell();
        tc.ColumnSpan = 3;
        tc.Text = obd[0].DepartureDateTime.ToString("dd-MMM-yyyy");

        tc.Width = 125;
        tr.Cells.Add(tc);
        tblMain.Rows.Add(tr);


        tr = new TableRow();
        tc = new TableCell();
        tc.Text = "City:";

        tc.Width = 125;
        tr.Cells.Add(tc);
        tc = new TableCell();
        tc.Text = obd[0].ArrivalCity.ToString();

        tc.Width = 125;
        tr.Cells.Add(tc);
        tc = new TableCell();
        tc.Text = "City:";

        tc.Width = 125;
        tr.Cells.Add(tc);
        tc = new TableCell();
        tc.ColumnSpan = 3;
        tc.Text = obd[0].DepartureCity.ToString();

        tc.Width = 125;
        tr.Cells.Add(tc);
        tblMain.Rows.Add(tr);





        tr = new TableRow();
        tc = new TableCell();
        tc.Text = "Transport Mode:";

        tc.Width = 125;
        tr.Cells.Add(tc);
        tc = new TableCell();

        tc.Text = obd[0].ArrivalTransport == null ? "" : obd[0].ArrivalTransport.ToString();

        tc.Width = 125;
        tr.Cells.Add(tc);
        tc = new TableCell();
        tc.Text = "Transport Mode:";

        tc.Width = 125;
        tr.Cells.Add(tc);
        tc = new TableCell();
        tc.ColumnSpan = 3;
        tc.Text = obd[0].DepartureTransport == null ? "" : obd[0].DepartureTransport.ToString();

        tc.Width = 125;
        tr.Cells.Add(tc);
        tblMain.Rows.Add(tr);



        tr = new TableRow();
        tc = new TableCell();
        tc.Text = "Vehicle No.";

        tc.Width = 125;
        tr.Cells.Add(tc);
        tc = new TableCell();
        tc.Text = obd[0].ArrivalVehicleNo == null ? "" : obd[0].ArrivalVehicleNo.ToString();

        tc.Width = 125;
        tr.Cells.Add(tc);
        tc = new TableCell();
        tc.Text = "Vehicle No.";

        tc.Width = 125;
        tr.Cells.Add(tc);
        tc = new TableCell();
        tc.ColumnSpan = 3;
        tc.Text = obd[0].DepartureVehicleNo == null ? "" : obd[0].DepartureVehicleNo.ToString();

        tc.Width = 125;
        tr.Cells.Add(tc);
        tblMain.Rows.Add(tr);



        tr = new TableRow();
        tc = new TableCell();
        tc.Text = "Vehicle Name/Type:";

        tc.Width = 125;
        tr.Cells.Add(tc);
        tc = new TableCell();
        tc.Text = obd[0].ArrivalVehicleNameType == null ? "" : obd[0].ArrivalVehicleNameType.ToString();

        tc.Width = 125;
        tr.Cells.Add(tc);
        tc = new TableCell();
        tc.Text = "Vehicle Name/Type:";

        tc.Width = 125;
        tr.Cells.Add(tc);
        tc = new TableCell();
        tc.ColumnSpan = 3;
        tc.Text = obd[0].DepartureVehicleNameType == null ? "" : obd[0].DepartureVehicleNameType.ToString();

        tc.Width = 125;
        tr.Cells.Add(tc);
        tblMain.Rows.Add(tr);


        tr = new TableRow();
        tc = new TableCell();
        tc.Text = "Transport Company:";

        tc.Width = 125;
        tr.Cells.Add(tc);
        tc = new TableCell();
        tc.Text = obd[0].ArrivalTransportCompany == null ? "" : obd[0].ArrivalTransportCompany.ToString();

        tc.Width = 125;
        tr.Cells.Add(tc);
        tc = new TableCell();
        tc.Text = "Transport Company:";

        tc.Width = 125;
        tr.Cells.Add(tc);
        tc = new TableCell();
        tc.ColumnSpan = 3;
        tc.Text = obd[0].DepartureTransportCompany == null ? "" : obd[0].DepartureTransportCompany.ToString();

        tc.Width = 125;
        tr.Cells.Add(tc);
        tblMain.Rows.Add(tr);



        tr = new TableRow();
        tc = new TableCell();
        tc.Text = "Transport Phone No.:";

        tc.Width = 125;
        tr.Cells.Add(tc);
        tc = new TableCell();
        tc.Text = obd[0].ArrivalTransportCompanyPhoneNo == null ? "" : obd[0].ArrivalTransportCompanyPhoneNo.ToString();

        tc.Width = 125;
        tr.Cells.Add(tc);
        tc = new TableCell();
        tc.Text = "Transport Phone No.:";

        tc.Width = 125;
        tr.Cells.Add(tc);
        tc = new TableCell();
        tc.ColumnSpan = 3;
        tc.Text = obd[0].DepartureTransportCompanyPhoneNo == null ? "" : obd[0].DepartureTransportCompanyPhoneNo.ToString();

        tc.Width = 125;
        tr.Cells.Add(tc);
        tblMain.Rows.Add(tr);



        tr = new TableRow();
        tc = new TableCell();
        tc.Text = "Driver Phone No.:";

        tc.Width = 125;
        tr.Cells.Add(tc);
        tc = new TableCell();
        tc.Text = obd[0].ArrivalDriverPhoneNo == null ? "" : obd[0].ArrivalDriverPhoneNo.ToString();

        tc.Width = 125;
        tr.Cells.Add(tc);
        tc = new TableCell();
        tc.Text = "Driver Phone No.:";

        tc.Width = 125;
        tr.Cells.Add(tc);
        tc = new TableCell();
        tc.ColumnSpan = 3;
        tc.Text = obd[0].DepartureDriverPhoneNo == null ? "" : obd[0].DepartureDriverPhoneNo.ToString();

        tc.Width = 125;
        tr.Cells.Add(tc);
        tblMain.Rows.Add(tr);

        return tblMain;
        // PnlMailformat.Controls.Add(tblMain);
    }


    private Table BookDetailsformail(int bookingId, string st)
    {
        BookingDTMail[] obd = null;
        BookingReportServices oBRM = new BookingReportServices();
        obd = oBRM.GetBookingDetailsMail(bookingId);
        if (obd != null)
        {
            if (obd.Length > 0)
            {
                return PrepareBookindDetailsMail(obd, st);
            }
        }
        return null;

    }

    private Table BookingRoomDetailsformail(int bookingid, string st)
    {
        BookingRoomReportsDTO[] oBRRD = null;
        BookingReportServices oBRM = new BookingReportServices();
        oBRRD = oBRM.GetroomBookingDetailsMail(bookingid);
        if (oBRRD != null)
        {
            if (oBRRD.Length > 0)
            {
                return PrepareRoomBookingReport(oBRRD, st);
            }
        }
        return null;
    }

    private Table BookingConfirmRoomDetailsformail(int bookingid, string st)
    {
        BookingRoomReportsDTO[] oBRRD = null;
        BookingReportServices oBRM = new BookingReportServices();
        oBRRD = oBRM.GetroomBookingDetailsMail(bookingid);
        if (oBRRD != null)
        {
            if (oBRRD.Length > 0)
            {
                return PrepareconfirmRoomBookingReport(oBRRD, st);
            }
        }
        return null;
    }
        
    private Table PrepareRoomBookingReport(BookingRoomReportsDTO[] BookingRoomReportsDTO, string st)
    {
        Table tblMain;
        if (st == "Updated Details")
        {
            tblMain = new Table();
            tblMain.Style.Add("border-collapse", "collapse");

            TableRow tr;
            TableCell tc;
            tblMain.Width = 340;
            tblMain.Attributes.Add("Border", "1px solid black");


            tr = new TableRow();
            tc = new TableCell();
            tc.Text = "Room Category";
            tc.Style[HtmlTextWriterStyle.FontWeight] = "Bold";
            tc.Width = 80;
            tr.Cells.Add(tc);

            tc = new TableCell();
            tc.Text = "Room Type";
            tc.Style[HtmlTextWriterStyle.FontWeight] = "Bold";
            tc.Width = 80;
            tr.Cells.Add(tc);
            int b = 0;
            int w = 0;
            int p = 0;

            for (int i = 0; i < BookingRoomReportsDTO.Length; i++)
            {
                if (BookingRoomReportsDTO[i].TotalBooked > 0)
                {
                    b++;


                }
                if (BookingRoomReportsDTO[i].TotalWaitlisted > 0)
                {
                    w++;


                }
                if (BookingRoomReportsDTO[i].Proposed > 0)
                {
                    p++;



                }


            }

            if (b > 0)
            {
                tc = new TableCell();
                tc.Text = "Booked";
                tc.Style[HtmlTextWriterStyle.FontWeight] = "Bold";
                tc.Width = 80;
                tr.Cells.Add(tc);
            }
            if (w > 0)
            {

                tc = new TableCell();
                tc.Text = "Wait-Listed";
                tc.Style[HtmlTextWriterStyle.FontWeight] = "Bold";
                tc.Width = 80;
                tr.Cells.Add(tc);
            }

            if (p > 0)
            {

                tc = new TableCell();
                tc.Text = "Proposed";
                tc.Style[HtmlTextWriterStyle.FontWeight] = "Bold";
                tc.Width = 80;
                tr.Cells.Add(tc);
            }

            tblMain.Rows.Add(tr);






            for (int i = 0; i < BookingRoomReportsDTO.Length; i++)
            {
                tr = new TableRow();
                tc = new TableCell();
                tc.Text = BookingRoomReportsDTO[i].RoomCategory;

                tc.Width = 80;
                tr.Cells.Add(tc);

                tc = new TableCell();
                tc.Text = BookingRoomReportsDTO[i].RoomType;
                tc.Width = 80;
                tr.Cells.Add(tc);
                tblMain.Rows.Add(tr);
                if (b > 0)
                {
                    tc = new TableCell();
                    tc.Text = BookingRoomReportsDTO[i].TotalBooked.ToString();
                    tc.Width = 80;
                    tr.Cells.Add(tc);
                    tblMain.Rows.Add(tr);
                }

                if (w > 0)
                {

                    tc = new TableCell();
                    tc.Text = BookingRoomReportsDTO[i].TotalWaitlisted.ToString();
                    tc.Width = 80;
                    tr.Cells.Add(tc);
                    tblMain.Rows.Add(tr);
                }
                if (p > 0)
                {
                    tc = new TableCell();
                    tc.Text = BookingRoomReportsDTO[i].Proposed.ToString();
                    tc.Width = 80;
                    tr.Cells.Add(tc);

                }

                tblMain.Rows.Add(tr);

            }

            pnlmailbookedrms.Controls.Add(tblMain);
        }

        else
        {

            tblMain = new Table();
            tblMain.Style.Add("border-collapse", "collapse");
            TableRow tr;
            TableCell tc;
            tblMain.Width = 340;
            tblMain.Attributes.Add("Border", "1px solid black");



            tr = new TableRow();
            tc = new TableCell();
            tc.Text = "Room Category";
            tc.Style[HtmlTextWriterStyle.FontWeight] = "Bold";
            tc.Width = 80;
            tr.Cells.Add(tc);

            tc = new TableCell();
            tc.Text = "Room Type";
            tc.Style[HtmlTextWriterStyle.FontWeight] = "Bold";
            tc.Width = 80;
            tr.Cells.Add(tc);
            int b = 0;
            int w = 0;
            int p = 0;

            for (int i = 0; i < BookingRoomReportsDTO.Length; i++)
            {
                if (BookingRoomReportsDTO[i].TotalBooked > 0)
                {
                    b++;


                }
                if (BookingRoomReportsDTO[i].TotalWaitlisted > 0)
                {
                    w++;


                }
                if (BookingRoomReportsDTO[i].Proposed > 0)
                {
                    p++;



                }


            }

            if (b > 0)
            {
                tc = new TableCell();
                tc.Text = "Booked";
                tc.Style[HtmlTextWriterStyle.FontWeight] = "Bold";
                tc.Width = 80;
                tr.Cells.Add(tc);
            }
            if (w > 0)
            {

                tc = new TableCell();
                tc.Text = "Wait-Listed";
                tc.Style[HtmlTextWriterStyle.FontWeight] = "Bold";
                tc.Width = 80;
                tr.Cells.Add(tc);
            }

            if (p > 0)
            {

                tc = new TableCell();
                tc.Text = "Proposed";
                tc.Style[HtmlTextWriterStyle.FontWeight] = "Bold";
                tc.Width = 80;
                tr.Cells.Add(tc);
            }

            tblMain.Rows.Add(tr);






            for (int i = 0; i < BookingRoomReportsDTO.Length; i++)
            {
                tr = new TableRow();
                tc = new TableCell();
                tc.Text = BookingRoomReportsDTO[i].RoomCategory;

                tc.Width = 80;
                tr.Cells.Add(tc);

                tc = new TableCell();
                tc.Text = BookingRoomReportsDTO[i].RoomType;
                tc.Width = 80;
                tr.Cells.Add(tc);
                tblMain.Rows.Add(tr);
                if (b > 0)
                {
                    tc = new TableCell();
                    tc.Text = BookingRoomReportsDTO[i].TotalBooked.ToString();
                    tc.Width = 80;
                    tr.Cells.Add(tc);
                    tblMain.Rows.Add(tr);
                }

                if (w > 0)
                {

                    tc = new TableCell();
                    tc.Text = BookingRoomReportsDTO[i].TotalWaitlisted.ToString();
                    tc.Width = 80;
                    tr.Cells.Add(tc);
                    tblMain.Rows.Add(tr);
                }
                if (p > 0)
                {
                    tc = new TableCell();
                    tc.Text = BookingRoomReportsDTO[i].Proposed.ToString();
                    tc.Width = 80;
                    tr.Cells.Add(tc);

                }

                tblMain.Rows.Add(tr);

            }

            //  pnlmailbookedrms.Controls.Add(tblMain);
        }
        return tblMain;

    }

    private Table PrepareconfirmRoomBookingReport(BookingRoomReportsDTO[] BookingRoomReportsDTO, string st)
    {
        Table tblMain;
        if (st == "Updated Details")
        {
            tblMain = new Table();
            tblMain.Style.Add("border-collapse", "collapse");

            TableRow tr;
            TableCell tc;
            tblMain.Width = 340;
            tblMain.Attributes.Add("Border", "1px solid black");


            tr = new TableRow();
            tc = new TableCell();
            tc.Text = "Room Category";
            tc.Style[HtmlTextWriterStyle.FontWeight] = "Bold";
            tc.Width = 80;
            tr.Cells.Add(tc);

            tc = new TableCell();
            tc.Text = "Room Type";
            tc.Style[HtmlTextWriterStyle.FontWeight] = "Bold";
            tc.Width = 80;
            tr.Cells.Add(tc);
            int b = 0;
            int w = 0;
            int p = 0;

            for (int i = 0; i < BookingRoomReportsDTO.Length; i++)
            {
                if (BookingRoomReportsDTO[i].TotalBooked > 0)
                {
                    b++;


                }
                if (BookingRoomReportsDTO[i].TotalWaitlisted > 0)
                {
                    w++;


                }
                if (BookingRoomReportsDTO[i].Proposed > 0)
                {
                    p++;



                }


            }

            if (b > 0)
            {
                tc = new TableCell();
                tc.Text = "Confirmed";
                tc.Style[HtmlTextWriterStyle.FontWeight] = "Bold";
                tc.Width = 80;
                tr.Cells.Add(tc);
            }
            if (w > 0)
            {

                tc = new TableCell();
                tc.Text = "Wait-Listed";
                tc.Style[HtmlTextWriterStyle.FontWeight] = "Bold";
                tc.Width = 80;
                tr.Cells.Add(tc);
            }

            if (p > 0)
            {

                tc = new TableCell();
                tc.Text = "Proposed";
                tc.Style[HtmlTextWriterStyle.FontWeight] = "Bold";
                tc.Width = 80;
                tr.Cells.Add(tc);
            }

            tblMain.Rows.Add(tr);






            for (int i = 0; i < BookingRoomReportsDTO.Length; i++)
            {
                tr = new TableRow();
                tc = new TableCell();
                tc.Text = BookingRoomReportsDTO[i].RoomCategory;

                tc.Width = 80;
                tr.Cells.Add(tc);

                tc = new TableCell();
                tc.Text = BookingRoomReportsDTO[i].RoomType;
                tc.Width = 80;
                tr.Cells.Add(tc);
                tblMain.Rows.Add(tr);
                if (b > 0)
                {
                    tc = new TableCell();
                    tc.Text = BookingRoomReportsDTO[i].TotalBooked.ToString();
                    tc.Width = 80;
                    tr.Cells.Add(tc);
                    tblMain.Rows.Add(tr);
                }

                if (w > 0)
                {

                    tc = new TableCell();
                    tc.Text = BookingRoomReportsDTO[i].TotalWaitlisted.ToString();
                    tc.Width = 80;
                    tr.Cells.Add(tc);
                    tblMain.Rows.Add(tr);
                }
                if (p > 0)
                {
                    tc = new TableCell();
                    tc.Text = BookingRoomReportsDTO[i].Proposed.ToString();
                    tc.Width = 80;
                    tr.Cells.Add(tc);

                }

                tblMain.Rows.Add(tr);

            }

            pnlmailbookedrms.Controls.Add(tblMain);
        }

        else
        {

            tblMain = new Table();
            tblMain.Style.Add("border-collapse", "collapse");
            TableRow tr;
            TableCell tc;
            tblMain.Width = 340;
            tblMain.Attributes.Add("Border", "1px solid black");



            tr = new TableRow();
            tc = new TableCell();
            tc.Text = "Room Category";
            tc.Style[HtmlTextWriterStyle.FontWeight] = "Bold";
            tc.Width = 80;
            tr.Cells.Add(tc);

            tc = new TableCell();
            tc.Text = "Room Type";
            tc.Style[HtmlTextWriterStyle.FontWeight] = "Bold";
            tc.Width = 80;
            tr.Cells.Add(tc);
            int b = 0;
            int w = 0;
            int p = 0;

            for (int i = 0; i < BookingRoomReportsDTO.Length; i++)
            {
                if (BookingRoomReportsDTO[i].TotalBooked > 0)
                {
                    b++;


                }
                if (BookingRoomReportsDTO[i].TotalWaitlisted > 0)
                {
                    w++;


                }
                if (BookingRoomReportsDTO[i].Proposed > 0)
                {
                    p++;



                }


            }

            if (b > 0)
            {
                tc = new TableCell();
                tc.Text = "Confirmed";
                tc.Style[HtmlTextWriterStyle.FontWeight] = "Bold";
                tc.Width = 80;
                tr.Cells.Add(tc);
            }
            if (w > 0)
            {

                tc = new TableCell();
                tc.Text = "Wait-Listed";
                tc.Style[HtmlTextWriterStyle.FontWeight] = "Bold";
                tc.Width = 80;
                tr.Cells.Add(tc);
            }

            if (p > 0)
            {

                tc = new TableCell();
                tc.Text = "Proposed";
                tc.Style[HtmlTextWriterStyle.FontWeight] = "Bold";
                tc.Width = 80;
                tr.Cells.Add(tc);
            }

            tblMain.Rows.Add(tr);






            for (int i = 0; i < BookingRoomReportsDTO.Length; i++)
            {
                tr = new TableRow();
                tc = new TableCell();
                tc.Text = BookingRoomReportsDTO[i].RoomCategory;

                tc.Width = 80;
                tr.Cells.Add(tc);

                tc = new TableCell();
                tc.Text = BookingRoomReportsDTO[i].RoomType;
                tc.Width = 80;
                tr.Cells.Add(tc);
                tblMain.Rows.Add(tr);
                if (b > 0)
                {
                    tc = new TableCell();
                    tc.Text = BookingRoomReportsDTO[i].TotalBooked.ToString();
                    tc.Width = 80;
                    tr.Cells.Add(tc);
                    tblMain.Rows.Add(tr);
                }

                if (w > 0)
                {

                    tc = new TableCell();
                    tc.Text = BookingRoomReportsDTO[i].TotalWaitlisted.ToString();
                    tc.Width = 80;
                    tr.Cells.Add(tc);
                    tblMain.Rows.Add(tr);
                }
                if (p > 0)
                {
                    tc = new TableCell();
                    tc.Text = BookingRoomReportsDTO[i].Proposed.ToString();
                    tc.Width = 80;
                    tr.Cells.Add(tc);

                }

                tblMain.Rows.Add(tr);

            }

            //  pnlmailbookedrms.Controls.Add(tblMain);
        }
        return tblMain;

    }

    private Table PrepareBookindDetailsMail(BookingDTMail[] BookingDTMail, string st)
    {

        Table tblMain;

        if (lblSubject.Text == "Booking Updated")
        {
            if (st == "Updated Details")
            {
                lblSubject.Text = lblSubject.Text + "-" + BookingDTMail[0].Bookingcode + "-" + BookingDTMail[0].Accomodation + "(Ref:-" + BookingDTMail[0].Bookingref + ")";
                tblMain = new Table();
                tblMain.Style.Add("border-collapse", "collapse");
                TableRow tr;
                TableCell tc;
                tblMain.Width = 340;
                if (st != "")
                {
                    tr = new TableRow();
                    tc = new TableCell();
                    tc.Text = st;
                    tc.Style[HtmlTextWriterStyle.FontWeight] = "Bold";
                    tr.Width = 500;
                    tr.Cells.Add(tc);
                    tblMain.Rows.Add(tr);
                    tblMain.Attributes.Add("Border", "1px solid black");
                    //  tblMain.Attributes.Add("style", "float:right;width:50%");

                }
                for (int i = 0; i < BookingDTMail.Length; i++)
                {
                    tr = new TableRow();


                    tc = new TableCell();
                    tc.Text = BookingDTMail[i].Bookingcode;
                    tc.Width = 250;
                    tr.Cells.Add(tc);
                    tblMain.Rows.Add(tr);

                    tr = new TableRow();

                    tc = new TableCell();
                    tc.Text = BookingDTMail[i].AgentName;
                    tc.Width = 250;
                    tr.Cells.Add(tc);
                    tblMain.Rows.Add(tr);

                    tr = new TableRow();


                    tc = new TableCell();
                    tc.Text = BookingDTMail[i].Accomodation;
                    tc.Width = 250;
                    tr.Cells.Add(tc);
                    tblMain.Rows.Add(tr);

                    tr = new TableRow();


                    tc = new TableCell();
                    tc.Text = string.Format("{0:dd-MMM-yyyy}", BookingDTMail[i].checkin);
                    tc.Width = 250;
                    tr.Cells.Add(tc);
                    tblMain.Rows.Add(tr);

                    tr = new TableRow();


                    tc = new TableCell();
                    tc.Text = string.Format("{0:dd-MMM-yyyy}", BookingDTMail[i].checkout);
                    tc.Width = 250;
                    tr.Cells.Add(tc);
                    tblMain.Rows.Add(tr);

                    tr = new TableRow();

                    tc = new TableCell();
                    if (BookingDTMail[i].chartered == true)
                    {
                        tc.Text = BookingDTMail[i].bookingstatus + "(Chartered)";
                    }
                    else
                    {
                        tc.Text = BookingDTMail[i].bookingstatus;
                    }
                    tc.Width = 250;
                    tr.Cells.Add(tc);
                    tblMain.Rows.Add(tr);

                    tr = new TableRow();


                    tc = new TableCell();
                    tc.Text = BookingDTMail[i].pax.ToString();
                    tc.Width = 250;
                    tr.Cells.Add(tc);
                    tblMain.Rows.Add(tr);

                    tr = new TableRow();


                    tc = new TableCell();
                    tc.Text = BookingDTMail[i].Nights.ToString();
                    tc.Width = 250;
                    tr.Cells.Add(tc);
                    tblMain.Rows.Add(tr);
                }

                // PnlMailformat.Controls.Add(tblMain);

            }
            else
            {



                tblMain = new Table();
                tblMain.Style.Add("border-collapse", "collapse");
                TableRow tr;
                TableCell tc;
                tblMain.Width = 340;
                if (st != "")
                {



                    tr = new TableRow();
                    tc = new TableCell();
                    tc.Text = "Details";
                    tc.Style[HtmlTextWriterStyle.FontWeight] = "Bold";
                    tr.Cells.Add(tc);

                    tc = new TableCell();
                    tc.Text = st;
                    tc.Style[HtmlTextWriterStyle.FontWeight] = "Bold";
                    tr.Width = 500;
                    tr.Cells.Add(tc);
                    tblMain.Rows.Add(tr);
                    tblMain.Attributes.Add("Border", "1px solid black");
                    //  tblMain.Attributes.Add("style", "float:left;width:50%");


                }
                for (int i = 0; i < BookingDTMail.Length; i++)
                {
                    tr = new TableRow();
                    tc = new TableCell();
                    tc.Text = "Booking Id:";
                    //tc.Style[HtmlTextWriterStyle.FontWeight] = "Bold";
                    tc.Width = 250;
                    tr.Cells.Add(tc);

                    tc = new TableCell();
                    tc.Text = BookingDTMail[i].Bookingcode;
                    tc.Width = 250;
                    tr.Cells.Add(tc);
                    tblMain.Rows.Add(tr);

                    tr = new TableRow();
                    tc = new TableCell();
                    tc.Text = "Agent Name:";
                    //tc.Style[HtmlTextWriterStyle.FontWeight] = "Bold";
                    tc.Width = 250;
                    tr.Cells.Add(tc);

                    tc = new TableCell();
                    tc.Text = BookingDTMail[i].AgentName;
                    tc.Width = 250;
                    tr.Cells.Add(tc);
                    tblMain.Rows.Add(tr);

                    tr = new TableRow();
                    tc = new TableCell();
                    tc.Text = "Accommodation:";
                    //tc.Style[HtmlTextWriterStyle.FontWeight] = "Bold";
                    tc.Width = 250;
                    tr.Cells.Add(tc);

                    tc = new TableCell();
                    tc.Text = BookingDTMail[i].Accomodation;
                    tc.Width = 250;
                    tr.Cells.Add(tc);
                    tblMain.Rows.Add(tr);

                    tr = new TableRow();
                    tc = new TableCell();
                    tc.Text = "Check in:";
                    //tc.Style[HtmlTextWriterStyle.FontWeight] = "Bold";
                    tc.Width = 250;
                    tr.Cells.Add(tc);

                    tc = new TableCell();
                    tc.Text = string.Format("{0:dd-MMM-yyyy}", BookingDTMail[i].checkin);
                    tc.Width = 250;
                    tr.Cells.Add(tc);
                    tblMain.Rows.Add(tr);

                    tr = new TableRow();
                    tc = new TableCell();
                    tc.Text = "Check Out:";
                    //tc.Style[HtmlTextWriterStyle.FontWeight] = "Bold";
                    tc.Width = 250;
                    tr.Cells.Add(tc);

                    tc = new TableCell();
                    tc.Text = string.Format("{0:dd-MMM-yyyy}", BookingDTMail[i].checkout);
                    tc.Width = 250;
                    tr.Cells.Add(tc);
                    tblMain.Rows.Add(tr);

                    tr = new TableRow();
                    tc = new TableCell();
                    tc.Text = "Booking Status:";
                    //tc.Style[HtmlTextWriterStyle.FontWeight] = "Bold";
                    tc.Width = 250;
                    tr.Cells.Add(tc);

                    tc = new TableCell();

                    if (BookingDTMail[i].chartered == true)
                    {
                        tc.Text = BookingDTMail[i].bookingstatus + "(Chartered)";
                    }
                    else
                    {
                        tc.Text = BookingDTMail[i].bookingstatus;
                    }
                    tc.Width = 250;
                    tr.Cells.Add(tc);
                    tblMain.Rows.Add(tr);

                    tr = new TableRow();
                    tc = new TableCell();
                    tc.Text = "PAX:";
                    //tc.Style[HtmlTextWriterStyle.FontWeight] = "Bold";
                    tc.Width = 250;
                    tr.Cells.Add(tc);

                    tc = new TableCell();
                    tc.Text = BookingDTMail[i].pax.ToString();
                    tc.Width = 250;
                    tr.Cells.Add(tc);
                    tblMain.Rows.Add(tr);

                    tr = new TableRow();
                    tc = new TableCell();
                    tc.Text = "Nights:";
                    //tc.Style[HtmlTextWriterStyle.FontWeight] = "Bold";
                    tc.Width = 250;
                    tr.Cells.Add(tc);

                    tc = new TableCell();
                    tc.Text = BookingDTMail[i].Nights.ToString();
                    tc.Width = 250;
                    tr.Cells.Add(tc);
                    tblMain.Rows.Add(tr);


                }

                // PnlMailformat.Controls.Add(tblMain);
            }


        }
        else
        {
            lblSubject.Text = lblSubject.Text + "-" + BookingDTMail[0].Bookingcode + "-" + BookingDTMail[0].Accomodation + "(Ref:-" + BookingDTMail[0].Bookingref + ")";

            tblMain = new Table();
            tblMain.Style.Add("border-collapse", "collapse");
            TableRow tr;
            TableCell tc;
            tblMain.Width = 340;
            if (st != "")
            {

                tr = new TableRow();
                tc = new TableCell();
                tc.Text = st;
                tc.Style[HtmlTextWriterStyle.FontWeight] = "Bold";
                tr.Width = 500;
                tr.Cells.Add(tc);
                tblMain.Rows.Add(tr);
                tblMain.Attributes.Add("Border", "1px solid black");
            }
            for (int i = 0; i < BookingDTMail.Length; i++)
            {
                tr = new TableRow();
                tc = new TableCell();
                tc.Text = "Booking Id:";
                //tc.Style[HtmlTextWriterStyle.FontWeight] = "Bold";
                tc.Width = 250;
                tr.Cells.Add(tc);

                tc = new TableCell();
                tc.Text = BookingDTMail[i].Bookingcode;
                tc.Width = 250;
                tr.Cells.Add(tc);
                tblMain.Rows.Add(tr);

                tr = new TableRow();
                tc = new TableCell();
                tc.Text = "Agent Name:";
                //tc.Style[HtmlTextWriterStyle.FontWeight] = "Bold";
                tc.Width = 250;
                tr.Cells.Add(tc);

                tc = new TableCell();
                tc.Text = BookingDTMail[i].AgentName;
                tc.Width = 250;
                tr.Cells.Add(tc);
                tblMain.Rows.Add(tr);

                tr = new TableRow();
                tc = new TableCell();
                tc.Text = "Accommodation:";
                //tc.Style[HtmlTextWriterStyle.FontWeight] = "Bold";
                tc.Width = 250;
                tr.Cells.Add(tc);

                tc = new TableCell();
                tc.Text = BookingDTMail[i].Accomodation;
                tc.Width = 250;
                tr.Cells.Add(tc);
                tblMain.Rows.Add(tr);

                tr = new TableRow();
                tc = new TableCell();
                tc.Text = "Check in:";
                //tc.Style[HtmlTextWriterStyle.FontWeight] = "Bold";
                tc.Width = 250;
                tr.Cells.Add(tc);

                tc = new TableCell();
                tc.Text = string.Format("{0:dd-MMM-yyyy}", BookingDTMail[i].checkin);
                tc.Width = 250;
                tr.Cells.Add(tc);
                tblMain.Rows.Add(tr);

                tr = new TableRow();
                tc = new TableCell();
                tc.Text = "Check Out:";
                //tc.Style[HtmlTextWriterStyle.FontWeight] = "Bold";
                tc.Width = 250;
                tr.Cells.Add(tc);

                tc = new TableCell();
                tc.Text = string.Format("{0:dd-MMM-yyyy}", BookingDTMail[i].checkout);
                tc.Width = 250;
                tr.Cells.Add(tc);
                tblMain.Rows.Add(tr);

                tr = new TableRow();
                tc = new TableCell();
                tc.Text = "Booking Status:";
                //tc.Style[HtmlTextWriterStyle.FontWeight] = "Bold";
                tc.Width = 250;
                tr.Cells.Add(tc);

                tc = new TableCell();
                if (BookingDTMail[i].chartered == true)
                {
                    tc.Text = BookingDTMail[i].bookingstatus + "(Chartered)";
                }
                else
                {
                    tc.Text = BookingDTMail[i].bookingstatus;
                }
                tc.Width = 250;
                tr.Cells.Add(tc);
                tblMain.Rows.Add(tr);

                tr = new TableRow();
                tc = new TableCell();
                tc.Text = "PAX:";
                //tc.Style[HtmlTextWriterStyle.FontWeight] = "Bold";
                tc.Width = 250;
                tr.Cells.Add(tc);

                tc = new TableCell();
                tc.Text = BookingDTMail[i].pax.ToString();
                tc.Width = 250;
                tr.Cells.Add(tc);
                tblMain.Rows.Add(tr);

                tr = new TableRow();
                tc = new TableCell();
                tc.Text = "Nights:";
                //tc.Style[HtmlTextWriterStyle.FontWeight] = "Bold";
                tc.Width = 250;
                tr.Cells.Add(tc);

                tc = new TableCell();
                tc.Text = BookingDTMail[i].Nights.ToString();
                tc.Width = 250;
                tr.Cells.Add(tc);
                tblMain.Rows.Add(tr);
            }

            // PnlMailformat.Controls.Add(tblMain);
        }
        return tblMain;

    }

    private void PrepareBookingReport(BookingRoomReportsDTO[] BookingRoomReportsDTO)
    {
        Table tblMain = new Table();
        TableRow tr;
        TableCell tc;

        tblMain.Width = 340;
        tblMain.Style[HtmlTextWriterStyle.FontSize] = "x-small";

        for (int i = 0; i < BookingRoomReportsDTO.Length; i++)
        {
            #region Category
            tr = new TableRow();
            tc = new TableCell();
            tc.Text = "1. Room Category";
            //tc.Style[HtmlTextWriterStyle.FontWeight] = "Bold";
            tc.Width = 125;
            tr.Cells.Add(tc);

            tc = new TableCell();
            tc.Text = BookingRoomReportsDTO[i].RoomCategory;
            tr.Cells.Add(tc);
            tblMain.Rows.Add(tr);
            #endregion

            #region Type
            tr = new TableRow();
            tc = new TableCell();
            tc.Text = "2. Room Type";
            //tc.Style[HtmlTextWriterStyle.FontWeight] = "Bold";
            tc.Width = 125;
            tr.Cells.Add(tc);

            tc = new TableCell();
            tc.Text = BookingRoomReportsDTO[i].RoomType;
            tr.Cells.Add(tc);
            tblMain.Rows.Add(tr);
            #endregion

            #region Booked
            tr = new TableRow();
            tc = new TableCell();
            tc.Text = "3. Booked";
            //tc.Style[HtmlTextWriterStyle.FontWeight] = "Bold";
            tc.Width = 75;
            tr.Cells.Add(tc);

            tc = new TableCell();
            tc.Text = BookingRoomReportsDTO[i].TotalBooked.ToString();
            tc.HorizontalAlign = HorizontalAlign.Center;
            tr.Cells.Add(tc);
            tblMain.Rows.Add(tr);
            #endregion

            #region WaitListed
            tr = new TableRow();
            tc = new TableCell();
            tc.Text = "4. Waitlisted";
            //tc.Style[HtmlTextWriterStyle.FontWeight] = "Bold";
            tc.Width = 75;
            tr.Cells.Add(tc);
            tc = new TableCell();
            tc.Text = BookingRoomReportsDTO[i].TotalWaitlisted.ToString();
            tc.HorizontalAlign = HorizontalAlign.Center;
            tr.Cells.Add(tc);
            tblMain.Rows.Add(tr);
            #endregion WaitListed
        }
        pnlBookingDetails.Controls.Add(tblMain);
    }

    private void ShowOtherBookingsWithinCurrentBookingDates(int BookingId)
    {
        BookingRoomReportsDTO[] oBRRD = null;
        BookingReportServices oBRM = new BookingReportServices();
        oBRRD = oBRM.GetBookingWithinCurrentBookingDates(BookingId);
        if (oBRRD != null)
        {
            if (oBRRD.Length > 0)
            {
                PrepareRestBookingsReport(oBRRD);
            }
        }
    }

    private void PrepareRestBookingsReport(BookingRoomReportsDTO[] BookingRoomReportDTO)
    {
        Table tbl = new Table();
        tbl.Width = 500;
        TableRow tr = new TableRow();
        TableCell tc = null;
        for (int i = 0; i < BookingRoomReportDTO.Length; i++)
        {
            tc = new TableCell();
            tc.Text = "<a href=Booking.aspx?bid=" + BookingRoomReportDTO[i].BookingId.ToString() + ">" + BookingRoomReportDTO[i].BookingRef.ToString() + "</a>";
            tc.Width = 150;
            tr.Cells.Add(tc);

            if ((i % 3 == 0 && i != 0) || (i == BookingRoomReportDTO.Length - 1))
            {
                tbl.Rows.Add(tr);
                tr = new TableRow();
            }
        }
        pnlOtherBookings.Controls.Add(tbl);
    }
    #endregion
}
