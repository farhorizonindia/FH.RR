using System;

namespace FarHorizon.Reservations.Common.DataEntities.Client
{
    [Serializable]
    public class ChartViewBookingDetail
    {
        public string key;
        public string bookingDetailHtml;
        //public string bookingStatusType;
        public BookingStatusTypes BookingStatusType;
    }
}
