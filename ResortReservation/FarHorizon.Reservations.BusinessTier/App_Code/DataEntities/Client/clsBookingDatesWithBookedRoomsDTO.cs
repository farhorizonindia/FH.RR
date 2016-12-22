using System;
using System.Collections.Generic;
using System.Text;
using BusinessTier.App_Code;
using BusinessTier.App_Code.DataEntities.Interfaces;

namespace BusinessTier.App_Code.DataEntities.Client
{
    public class clsBookingDatesWithBookedRoomsDTO : IBookingDatesDTO
    {
        DateTime _CheckInDate = DateTime.MinValue;
        DateTime _CheckOutDate = DateTime.MinValue;
        List<clsBookedRooms> _BookedRooms = null;        

        #region IBookingDates Members

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

        public List<clsBookedRooms> BookedRooms
        {
            get { return _BookedRooms; }
            set { _BookedRooms = value; }
        }
        #endregion
    }
}
