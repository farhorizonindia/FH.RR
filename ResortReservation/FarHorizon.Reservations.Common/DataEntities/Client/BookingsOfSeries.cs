using System;
using System.Collections.Generic;
using System.Text;

namespace FarHorizon.Reservations.Common.DataEntities.Client
{
    public class BookingOfSeriesDTO
    {
        BookingDTO _Booking;
        List<BookedRooms> _BookedRooms;
        List<BookingWaitListDTO> _WaitListedRooms;

        public BookingDTO Booking
        {
            get { return _Booking; }
            set { _Booking = value; }
        }       

        public List<BookedRooms> BookedRooms
        {
            get { return _BookedRooms; }
            set { _BookedRooms = value; }
        }

        public List<BookingWaitListDTO> WaitListedRooms
        {
            get { return _WaitListedRooms; }
            set { _WaitListedRooms = value; }
        }
    }
}
