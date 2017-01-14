using System;
using System.Text;
using FarHorizon.Reservations.Common;

namespace FarHorizon.Reservations.Common.DataEntities.Client
{
    [Serializable]
    public class RoomBookingDateWiseDTO : IComparable
    {
        private int _iAccomId;
        private string _sRoomNo;
        private int _iBookingId;
        private string _sBookingCode;
        private string _sTourId;
        private DateTime _dstartdate;
        private DateTime _denddate;
        private int _inoofnights;
        private int _iBookingStatusId;
        private string _sBookingRef;
        private string _sStartDate;
        private string _sEndDate;
        private string _sAgentName;
        private string _bookingStatusType;
        private int _noofrooms;
        private int _paxStaying;
        private bool _chartered;


        #region Properties

        public bool Chartered
        {
            get { return _chartered; }
            set { _chartered = value; }
        }
        public int NoofRooms
        {
            get { return _noofrooms; }
            set { _noofrooms = value; }

        }

        public int paxStaying
        {
            get { return _paxStaying; }
            set { _paxStaying = value; }
        }

        public string BookingStatusType
        {
            get { return _bookingStatusType; }
            set { _bookingStatusType = value; }
        }
        public string BookingReference
        {
            get { return _sBookingRef; }
            set { _sBookingRef = value; }
        }
        public int AccomodationId
        {
            get { return _iAccomId; }
            set { _iAccomId = value; }
        }
        public string RoomNo
        {
            get { return _sRoomNo; }
            set { _sRoomNo = value; }
        }
        public int BookingId
        {
            get { return _iBookingId; }
            set { _iBookingId = value; }
        }
        public string BookingCode
        {
            get { return _sBookingCode; }
            set { _sBookingCode = value; }
        }
        public string TourId
        {
            get { return _sTourId; }
            set { _sTourId = value; }
        }
        public DateTime Startdate
        {
            get { return _dstartdate; }
            set
            {
                _dstartdate = value;
               // _sStartDate = GF.GetDD_MM_YYYY(_dstartdate);
                _sStartDate = String.Format("{0:dd-MMM-yyyy}", _dstartdate);
            }
        }
        public DateTime Enddate
        {
            get { return _denddate; }
            set
            {
                _denddate = value;
                //_sEndDate = GF.GetDD_MM_YYYY(_denddate);
                _sEndDate = String.Format("{0:dd-MMM-yyyy}", _denddate);
            }
        }
        public int Noofnights
        {
            get { return _inoofnights; }
            set { _inoofnights = value; }
        }
        public int BookingStatusId
        {
            get { return _iBookingStatusId; }
            set { _iBookingStatusId = value; }
        }
        public string StartDateFormatted
        {
            get { return _sStartDate; }
        }
        public string EndDateFormatted
        {
            get { return _sEndDate; }
        }
        public string AgentName
        {
            get { return _sAgentName; }
            set { _sAgentName = value; }
        }
        #endregion

        #region IComparable Members

        public int CompareTo(object obj)
        {
            if (obj is RoomBookingDateWiseDTO)
            {
                RoomBookingDateWiseDTO roomBookingDateWiseDTO = (RoomBookingDateWiseDTO)obj;
                return Startdate.CompareTo(roomBookingDateWiseDTO.Startdate);
            }
            else
            {
                throw new ArgumentException("Object is not of type RoomBookingDateWiseDTO.");
            }
        }

        #endregion
    }
}

