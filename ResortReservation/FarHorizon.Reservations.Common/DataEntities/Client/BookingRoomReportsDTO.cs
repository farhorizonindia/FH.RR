using System;
using System.Collections.Generic;
using System.Text;

namespace FarHorizon.Reservations.Common.DataEntities.Client
{
    [Serializable]
    public class BookingRoomReportsDTO
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

        private int _Proposed;
        public int Proposed
        {
            get { return _Proposed; }
            set { _Proposed = value; }
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

    [Serializable]
    public class BookingDTMail
    {
        private int _bookingid;

        public int BookingId
        {
            get { return _bookingid; }
            set { _bookingid = value; }
        }

        private string _BookingCode;
        public string Bookingcode
        {
            get { return _BookingCode; }
            set { _BookingCode = value; }
        }
        private string _agentname;

        public string AgentName
        {
            get { return _agentname; }
            set { _agentname = value; }
        }

        private string _refagentname;

        public string RefAgentName
        {
            get { return _refagentname; }
            set { _refagentname = value; }
        }

        private string _accomodation;
        public string Accomodation
        {
            get { return _accomodation; }
            set { _accomodation = value; }
        }

        private DateTime _checkin;
        public DateTime checkin
        {
            get { return _checkin; }
            set { _checkin = value; }
        }

        private DateTime _checkout;
        public DateTime checkout
        {
            get { return _checkout; }
            set { _checkout = value; }
        }

        private string _bookingstatus;
        public string bookingstatus
        {
            get { return _bookingstatus; }
            set { _bookingstatus = value; }
        }

        private int _pax;
        public int pax
        {
            get { return _pax; }
            set { _pax = value; }
        }

        private int _nights;
        public int Nights
        {
            get { return _nights; }
            set { _nights = value; }
        }

        private string _bookingref;

        public string Bookingref
        {
            get { return _bookingref; }
            set { _bookingref = value; }
        }
        private bool _chartered;
        public bool chartered
        {
            get { return _chartered; }
            set { _chartered = value; }
        }
        private string _packagename;
        public string packagename
        {
            get { return _packagename; }
            set { _packagename = value; }
        }


    }

  
}
