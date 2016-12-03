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
using FarHorizon.Reservations.Common.DataEntities.Client;
using FarHorizon.Reservations.BusinessServices;
using FarHorizon.Reservations.Bases;
using FarHorizon.Reservations.Bases.BasePages;

public partial class ClientUI_PopUp_OtherBookings : ClientBasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            DateTime dtSD = Convert.ToDateTime(Request.QueryString["sd"].ToString());
            DateTime dtED = Convert.ToDateTime(Request.QueryString["ed"].ToString());
            string sRoomNo = Convert.ToString(Request.QueryString["roomno"]);
            ShowOtherBookings(sRoomNo, dtSD, dtED);
        }
    }
    private void ShowOtherBookings(string RoomNo, DateTime StartDate, DateTime EndDate)
    {
        BookingRoomReportsDTO[] oBRRD = null;
        BookingReportServices oBRM = new BookingReportServices();
        oBRRD = oBRM.GetOtherBookingsOfThisRoom(RoomNo, StartDate, EndDate);
        if (oBRRD != null)
        {
            if (oBRRD.Length > 0)
            {
                PrepareBookingReport(oBRRD);
            }
        }
    }
    private void PrepareBookingReport(BookingRoomReportsDTO[] BookingRoomReportDTO)
    {
        Table tblMain = new Table();
        TableRow tr = new TableRow();
        tblMain.Width = 400;
        TableCell tc = new TableCell();
        tc.ColumnSpan = 3;
        tc.Text = "Other Bookings Of This Room Between " + Request.QueryString["sd"].ToString() + " And " + Request.QueryString["ed"];
        tr.Cells.Add(tc);
        tblMain.Rows.Add(tr);
        tblMain.Style[HtmlTextWriterStyle.FontSize] = "small";
        tblMain.Style[HtmlTextWriterStyle.FontFamily]="verdana";
        int colCount = 0;
        tr = new TableRow();
        for (int i = 0; i < BookingRoomReportDTO.Length; i++)
        {
            colCount = colCount + 1;
            tc = new TableCell();
            tc.Text ="<a href=Booking.aspx?bid=" +  BookingRoomReportDTO[i].BookingId.ToString() + ">"+  BookingRoomReportDTO[i].BookingRef + "</a>";
            tr.Cells.Add(tc);
            if (i % 3 == 0 && i != 0)
            {
                tblMain.Rows.Add(tr);
                tr = new TableRow();
            }
            else if (i < 3 && (i == BookingRoomReportDTO.Length - 1 || i == BookingRoomReportDTO.Length))
            {
                tblMain.Rows.Add(tr);                
            }
        }
        pnlOtherBookings.Controls.Add(tblMain);
    }
}

