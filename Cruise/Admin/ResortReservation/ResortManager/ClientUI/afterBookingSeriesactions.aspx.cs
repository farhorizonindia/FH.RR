using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using FarHorizon.Reservations.Bases;
using FarHorizon.Reservations.Bases.BasePages;
using FarHorizon.Reservations.BusinessServices;
using FarHorizon.Reservations.Common.DataEntities.Client;
using System.Collections.Generic;
using FarHorizon.Reservations.Common;

public partial class ClientUI_afterBookingSeriesactions : ClientBasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        int SeriesId = 0;
        if (Request.QueryString["sid"] != null)
            int.TryParse(Request.QueryString["sid"], out SeriesId);

        lblSeriesBookingDetails.Text = "Series Id: " + SeriesId.ToString();
        lblSeriesBookingDetails.Text += "\n <a href=SeriesBooking.aspx?>New Series</a>";
        lblSeriesBookingDetails.Text += "\n <a href=SeriesBooking.aspx?sid=" + SeriesId.ToString() + ">View Series</a>";

        FillSeriesBookings(SeriesId);
        //if (!IsPostBack)
        //{
        //    ENums.EventName eventName = GetEventName(BookingStatus, bookingUpdated);
        //    EventEmailServices eventEmailService = new EventEmailServices();
        //    eventEmailService.SendEventMail(BookingId, eventName);
        //    //Thread emailThread = new Thread(new ThreadStart(EventEmailManager.SendEventMail(BookingId, AccomodationId, EventName)));
        //    //emailThread.Priority = ThreadPriority.Highest;
        //    //emailThread.Start();
        //}
    }

    private void FillSeriesBookings(int seriesId)
    {
        SeriesBookingServices oSeriesManager = new SeriesBookingServices();
        List<SeriesDetailDTO> seriesDetailList;
        Hashtable bookingDisplayed = new Hashtable();

        seriesDetailList = oSeriesManager.GetSeriesDetail(seriesId);
        lblBookingDetails.Text = "Following are the bookings booked in this series";

        HtmlGenericControl bookingRow;

        pnlBookingList.Controls.Clear();
        foreach (SeriesDetailDTO booking in seriesDetailList)
        {
            if (!bookingDisplayed.ContainsKey(booking.BookingId))
            {
                bookingRow = new HtmlGenericControl("Div");
                HyperLink hl = new HyperLink();
                hl.Target = "_blank";
                hl.NavigateUrl = "Booking.aspx?bid=" + booking.BookingId.ToString();
                hl.Text = "Booking Code: " + booking.BookingCode + " - Booking Ref: " + booking.BookingRef;
                bookingRow.Controls.Add(hl);
                pnlBookingList.Controls.Add(bookingRow);
                bookingDisplayed.Add(booking.BookingId, booking.BookingCode);
            }
        }
    }    
}
