using System;
using System.Collections.Generic;
using System.Text;

namespace FarHorizon.Reservations.Common.DataEntities.Client
{
    public class SeriesDetailDTO
    {
        private int _SeriesID;
        private DateTime _CheckIn;
        private DateTime _CheckOut;
        private string _RoomCategory;
        private string _RoomType;
        private int _RoomCategoryId;
        private int _RoomTypeId;
        private int _Requested;
        private int _Available;
        private int _Waitlisted;
        private int _Confirmed;
        private int _BookingId;
        private bool _proposedBooking;
        String _bookingCode;
        String _bookingRef;
        
        public int SeriesID
        {
            get { return _SeriesID; }
            set { _SeriesID = value; }
        }

        public DateTime CheckIn
        {
            get { return _CheckIn; }
            set { _CheckIn = value; }
        }

        public DateTime CheckOut
        {
            get { return _CheckOut; }
            set { _CheckOut = value; }
        }

        public int Requested
        {
            get { return _Requested; }
            set { _Requested = value; }
        }

        public int Available
        {
            get { return _Available; }
            set { _Available = value; }
        }

        public int Waitlisted
        {
            get { return _Waitlisted; }
            set { _Waitlisted = value; }
        }

        public int Confirmed
        {
            get { return _Confirmed; }
            set { _Confirmed = value; }
        }
        
        public int RoomCategoryId
        {
            get { return _RoomCategoryId; }
            set { _RoomCategoryId = value; }
        }

        public string RoomCategory
        {
            get { return _RoomCategory; }
            set { _RoomCategory = value; }
        }

        public int RoomTypeId
        {
            get { return _RoomTypeId; }
            set { _RoomTypeId = value; }
        }

        public string RoomType
        {
            get { return _RoomType; }
            set { _RoomType = value; }
        }

        public int BookingId
        {
            get { return _BookingId; }
            set { _BookingId = value; }
        }

        public bool ProposedBooking
        {
            get { return _proposedBooking; }
            set { _proposedBooking = value; }
        }

        public String BookingCode
        {
            get { return _bookingCode; }
            set { _bookingCode = value; }
        }

        public String BookingRef
        {
            get { return _bookingRef; }
            set { _bookingRef = value; }
        }
        
    }
}
