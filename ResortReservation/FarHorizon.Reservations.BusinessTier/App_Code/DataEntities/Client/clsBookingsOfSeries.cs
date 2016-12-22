using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessTier.App_Code.DataEntities.Client
{
    public class clsBookingOfSeriesDTO
    {
        clsBookingDTO _Booking;
        List<clsBookedRooms> _BookedRooms;
        List<clsBookingWaitListDTO> _WaitListedRooms;

        public clsBookingDTO Booking
        {
            get { return _Booking; }
            set { _Booking = value; }
        }       

        public List<clsBookedRooms> BookedRooms
        {
            get { return _BookedRooms; }
            set { _BookedRooms = value; }
        }

        public List<clsBookingWaitListDTO> WaitListedRooms
        {
            get { return _WaitListedRooms; }
            set { _WaitListedRooms = value; }
        }
    }
}
