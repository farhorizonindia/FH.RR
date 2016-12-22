using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

namespace FarHorizon.Reservations.Common.DataEntities.Client
{
    public class ChartViewBookingDetail
    {
        public string key;
        public string bookingDetailHtml;
        //public string bookingStatusType;
        public BookingStatusTypes BookingStatusType;
    }
}
