using System;
using System.Collections.Generic;
using System.Text;
using FarHorizon.Reservations.Common.DataEntities.Masters;

namespace FarHorizon.Reservations.Common.DataEntities.Client
{
    public class BookingRoomDTO
    {
        #region Variables
        private int _iBookingId;
        private string _sBookingCode;        
        private int _iAccomId;
        private string _sRoomNo;
        private RoomTypeDTO _oRoomTypeData;
        private RoomCategoryDTO _oRoomCategoryData;
        private DateTime _dStartDate;
        private DateTime _dEndDate;

        #endregion Variables

        #region Properties
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
        public DateTime StartDate
        {
            get { return _dStartDate; }
            set { _dStartDate = value; }
        }
        public DateTime EndDate
        {
            get { return _dEndDate; }
            set { _dEndDate = value; }
        }
        public RoomTypeDTO RoomTypeData
        {
            get { return _oRoomTypeData; }
            set { _oRoomTypeData = value; }
        }
        public RoomCategoryDTO RoomCategoryData
        {
            get { return _oRoomCategoryData; }
            set { _oRoomCategoryData = value; }
        }

        #endregion Properties
    }
}
