using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessTier.App_Code.DataEntities.Client
{
    public class clsBookingRoomReportsDTO
    {
        private string _sRoomCategory;

        public string RoomCategory
        {
            get { return _sRoomCategory; }
            set { _sRoomCategory = value; }
        }
        private string _sRoomType;

        public string RoomType
        {
            get { return _sRoomType; }
            set { _sRoomType = value; }
        }
        private int _iTotalBooked;

        public int TotalBooked
        {
            get { return _iTotalBooked; }
            set { _iTotalBooked = value; }
        }
        private int _iTotalWaitlisted;

        public int TotalWaitlisted
        {
            get { return _iTotalWaitlisted; }
            set { _iTotalWaitlisted = value; }
        }

        private int _iBookingId;

        public int BookingId
        {
            get { return _iBookingId; }
            set { _iBookingId = value; }
        }
        
        private string _sBookingRef;

        public string BookingRef
        {
            get { return _sBookingRef; }
            set { _sBookingRef = value; }
        }
        


    }
}
