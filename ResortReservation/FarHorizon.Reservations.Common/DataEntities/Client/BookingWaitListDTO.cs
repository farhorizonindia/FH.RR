using System;
using System.Collections.Generic;
using System.Text;

namespace FarHorizon.Reservations.Common.DataEntities.Client
{
    [Serializable]
    public class BookingWaitListDTO
    {
        char _cBookingType;        
        private int _iBookingId;
        private string _sBookingRef;        
        private int _iRoomCategoryId;
        private string _sRoomCategory;
        private int _iRoomTypeId;
        private string _sRoomType;
        private int _iAccomId;
        private int _iNo_Of_RoomsWaitListed;
        private int paxstaying;

        public int AccomId
        {
            get { return _iAccomId; }
            set { _iAccomId = value; }
        }
        public char BookingType
        {
            get { return _cBookingType; }
            set { _cBookingType = value; }
        }
        public int BookingId
        {
            get { return _iBookingId; }
            set { _iBookingId = value; }
        }

        public string BookingRef
        {
            get { return _sBookingRef; }
            set { _sBookingRef = value; }
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
        
        public int No_Of_RoomsWaitListed
        {
            get { return _iNo_Of_RoomsWaitListed; }
            set { _iNo_Of_RoomsWaitListed = value; }
        }
        public int paxstying
        {
            get { return paxstaying; }
            set { paxstaying = value; }
        }
    }
}
