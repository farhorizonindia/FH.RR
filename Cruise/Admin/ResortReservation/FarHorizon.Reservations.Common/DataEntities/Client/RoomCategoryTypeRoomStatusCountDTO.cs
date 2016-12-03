using System;
using System.Collections.Generic;
using System.Text;

namespace FarHorizon.Reservations.Common.DataEntities.Client
{
    public class RoomCategoryTypeRoomStatusCountDTO
    {
        int _RoomCategoryId;
        string _RoomCategory;
        int _RoomTypeId;
        string _RoomType;
        int _TotalRooms;
        int _Booked;
        int _WaitListed;
        int _TotalPax;
        int _Maintained;
        double _TotalPriceCategory;

        public double TotalPriceCategory
        {
            get { return _TotalPriceCategory; }
            set { _TotalPriceCategory = value; }
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

        public int TotalRooms
        {
            get { return _TotalRooms; }
            set { _TotalRooms = value; }
        }

        public int Booked
        {
            get { return _Booked; }
            set { _Booked = value; }
        }

        public int WaitListed
        {
            get { return _WaitListed; }
            set { _WaitListed = value; }
        }

        public int Maintained
        {
            get { return _Maintained; }
            set { _Maintained = value; }
        }

        public int TotalPax
        {
            get { return _TotalPax; }
            set { _TotalPax = value; }
        }
    }
}
