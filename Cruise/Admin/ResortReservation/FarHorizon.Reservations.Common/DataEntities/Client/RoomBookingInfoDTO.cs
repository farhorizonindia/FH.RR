using System;
using System.Collections.Generic;
using System.Text;
using FarHorizon.Reservations.Common.DataEntities.Masters;

namespace FarHorizon.Reservations.Common.DataEntities.Client
{
    public struct RoomsAvailable
    {
        public RoomTypeDTO oRoomTypeData;
        public int NoOfRoomsAvailable;
    }
    public struct RoomTypewiseRooms
    {
        public int RoomTypeId;
        public string RoomType;
        public int NoOfRoomsBooked;
    }
    public struct AvailableRoomNos
    {
        public int RoomTypeID;
        public string RoomType;
        public int RoomNo;
    }
    public struct RoomsDeletionInfo
    {
        public int BookingID;
        public int AccomodationID;
        public int RoomTypeId;
    }
    public class RoomTypeDetails
    {
        private int _iRoomTypeId;

        public int RoomTypeId
        {
            get { return _iRoomTypeId; }
            set { _iRoomTypeId = value; }
        }
        private int _iRoomsBooked;

        public int RoomsBooked
        {
            get { return _iRoomsBooked; }
            set { _iRoomsBooked = value; }
        }
    }
    public class RoomBookingInfo
    {
        private int _iBookingID;

        public int BookingID
        {
            get { return _iBookingID; }
            set { _iBookingID = value; }
        }

        private RoomTypeDetails oRoomTypeDetails;

        public RoomTypeDetails RoomTypeDetails
        {
            get { return oRoomTypeDetails; }
            set { oRoomTypeDetails = value; }
        }
    }


    public class BookedRooms
    {
        #region Variables

        private int _iRoomCategoryId;
        private string _sRoomCategory;
        private int _iRoomTypeId;
        private string _sRoomType;
        private string _sRoomNo;
        private int _iAccomodationId;
        private int _iNoOfBeds;
        private int _iBookingId;
        private int _iDefaultNoOfBeds;
        private string _sStatus;
        private DateTime _dtStartDate;
        private DateTime _dtEndDate;
        private int _iPaxStaying;
        private int _bConvertTo_Double_Twin;
        private bool _bConvertable;
        private char _cRoomStatus;
        private int _iPrevBookingId;
        private int _iPrevPaxStaying;
        private char _cPrevRoomStatus;
        private int _iOriginalBookingId;
        private int _iOriginalPaxStaying;
        private char _cOriginalRoomStatus;
        private bool _chartered;

        private string _action;
        private double _Amt;
        private string _PaymentId;
        private double _Paid;

        private double _Price;








        #endregion Variables

        #region Properties

        public double Price
        {
            get { return _Price; }
            set { _Price = value; }
        }

        public string action
        {
            get { return _action; }
            set { _action = value; }
        }


        public double Amount
        {
            get { return _Amt; }
            set { _Amt = value; }
        }


        public string PaymentId
        {
            get { return _PaymentId; }
            set { _PaymentId = value; }
        }

        public double Paid
        {
            get { return _Paid; }
            set { _Paid = value; }
        }






        public bool Chartered
        {
            get { return _chartered; }
            set { _chartered = value; }
        }

        public int RoomCategoryId
        {
            get { return _iRoomCategoryId; }
            set { _iRoomCategoryId = value; }
        }
        public string RoomCategory
        {
            get { return _sRoomCategory; }
            set { _sRoomCategory = value; }
        }
        public int RoomTypeId
        {
            get { return _iRoomTypeId; }
            set { _iRoomTypeId = value; }
        }
        public string RoomType
        {
            get { return _sRoomType; }
            set { _sRoomType = value; }
        }
        public string RoomNo
        {
            get { return _sRoomNo; }
            set { _sRoomNo = value; }
        }
        public int AccomodationId
        {
            get { return _iAccomodationId; }
            set { _iAccomodationId = value; }
        }
        public int NoOfBeds
        {
            get { return _iNoOfBeds; }
            set { _iNoOfBeds = value; }
        }
        public int BookingId
        {
            get { return _iBookingId; }
            set { _iBookingId = value; }
        }
        public int DefaultNoOfBeds
        {
            get { return _iDefaultNoOfBeds; }
            set { _iDefaultNoOfBeds = value; }
        }
        public string Status
        {
            get { return _sStatus; }
            set { _sStatus = value; }
        }
        public DateTime StartDate
        {
            get { return _dtStartDate; }
            set { _dtStartDate = value; }
        }
        public DateTime EndDate
        {
            get { return _dtEndDate; }
            set { _dtEndDate = value; }
        }
        public int PaxStaying
        {
            get { return _iPaxStaying; }
            set { _iPaxStaying = value; }
        }
        public bool ConvertTo_Double_Twin
        {
            get
            {
                if (_bConvertTo_Double_Twin == 0)
                    return false;
                else
                    return true;
            }
            set
            {
                if (value == false)
                    _bConvertTo_Double_Twin = 0;
                else if (value == true)
                    _bConvertTo_Double_Twin = 1;
            }
        }
        public bool Convertable
        {
            get { return _bConvertable; }
            set { _bConvertable = value; }
        }
        public char RoomStatus
        {
            get { return _cRoomStatus; }
            set { _cRoomStatus = value; }
        }

        public int PrevBookingId
        {
            get { return _iPrevBookingId; }
            set { _iPrevBookingId = value; }
        }

        public int PrevPaxStaying
        {
            get { return _iPrevPaxStaying; }
            set { _iPrevPaxStaying = value; }
        }

        public char PrevRoomStatus
        {
            get { return _cPrevRoomStatus; }
            set { _cPrevRoomStatus = value; }
        }

        public int OriginalBookingId
        {
            get { return _iOriginalBookingId; }
            set { _iOriginalBookingId = value; }
        }

        public int OriginalPaxStaying
        {
            get { return _iOriginalPaxStaying; }
            set { _iOriginalPaxStaying = value; }
        }

        public char OriginalRoomStatus
        {
            get { return _cOriginalRoomStatus; }
            set { _cOriginalRoomStatus = value; }
        }

        #endregion Properties
    }

    public class BookedRoomsTotal
    {
        #region Variables

        private string _sRoomCategory;
        private string _sRoomType;
        private int _iRoomsBooked;

        #endregion Variables

        #region Properties
        public string RoomCategory
        {
            get { return _sRoomCategory; }
            set { _sRoomCategory = value; }
        }
        public string RoomType
        {
            get { return _sRoomType; }
            set { _sRoomType = value; }
        }

        public int RoomsBooked
        {
            get { return _iRoomsBooked; }
            set { _iRoomsBooked = value; }
        }

        #endregion Properties
    }

    public class RoomBookingInfoDTO
    {
        private RoomBookingInfo[] oRoomBookingInfo;

        public RoomBookingInfo[] RoomBookingInfo
        {
            get { return oRoomBookingInfo; }
            set { oRoomBookingInfo = value; }
        }

        private int _iTotalNoOfRoomsBooked;

        public int TotalNoOfRoomsBooked
        {
            get { return _iTotalNoOfRoomsBooked; }
            set { _iTotalNoOfRoomsBooked = value; }
        }

        private BookingDTO oBookingData;

        public BookingDTO BookingData
        {
            get { return oBookingData; }
            set { oBookingData = value; }
        }
    }
}
