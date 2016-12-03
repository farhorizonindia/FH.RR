using System;
using System.Collections.Generic;
using System.Text;

namespace FarHorizon.Reservations.Common.DataEntities.Client
{
    public class BookingDatesWithBookedRoomsDTO
    {
        DateTime _CheckInDate = DateTime.MinValue;
        DateTime _CheckOutDate = DateTime.MinValue;
        List<BookedRooms> _BookedRooms = null;        

        public DateTime CheckInDate
        {
            get { return _CheckInDate; }
            set { _CheckInDate = value; }
        }

        public DateTime CheckOutDate
        {
            get { return _CheckOutDate;  }
            set { _CheckOutDate = value; }
        }

        public List<BookedRooms> BookedRooms
        {
            get { return _BookedRooms; }
            set { _BookedRooms = value; }
        }        
    }
}
