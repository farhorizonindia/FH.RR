using System;
using System.Collections.Generic;
using System.Text;

namespace FarHorizon.Reservations.Common.DataEntities.Client
{
    public class CFormReportDTO
    {
        String _cFormNo;
        BookingDTO _bookingDetails;
        BookingTouristDTO[] _bookingTouristDetails;

        public String CFormNo
        {
            get { return _cFormNo; }
            set { _cFormNo = value; }
        }

        public BookingDTO BookingDetails
        {
            get { return _bookingDetails; }
            set { _bookingDetails = value; }
        }


        public BookingTouristDTO[] BookingTouristDetails
        {
            get { return _bookingTouristDetails; }
            set { _bookingTouristDetails = value; }
        }        
    }
}
