using System;
using System.Collections.Generic;
using System.Text;
using FarHorizon.Reservations.Common.DataEntities.Masters;
using FarHorizon.Reservations.Common.DataEntities.Client;

namespace FarHorizon.Reservations.Common.DataEntities.Client
{
    public class Accomodation
    {
        #region Variables
        AccomodationDTO _AccomodationDetail;
        List<AccomodationRoomCategory> _Categories;
        #endregion

        #region Properties
        public AccomodationDTO AccomodationDetail
        {
            get { return _AccomodationDetail; }
            set { _AccomodationDetail = value; }
        }        

        public List<AccomodationRoomCategory> Categories
        {
            get { return _Categories; }
            set { _Categories = value; }
        }        
        #endregion
    }

    public class AccomodationRoomCategory
    {
        #region Variables
        RoomCategoryDTO _RoomCategory;
        List<AccomodationRoomType> _RoomTypes;
        #endregion

        #region Properties
        public RoomCategoryDTO RoomCategory
        {
            get { return _RoomCategory; }
            set { _RoomCategory = value; }
        }

        public List<AccomodationRoomType> RoomTypes
        {
            get { return _RoomTypes; }
            set { _RoomTypes = value; }
        }
        
        #endregion
    }

    public class AccomodationRoomType
    {
        #region Variables
        RoomTypeDTO _RoomType;
        List<AccomodationRoom> _Rooms;
        #endregion

        #region Properties
        public RoomTypeDTO RoomType
        {
            get { return _RoomType; }
            set { _RoomType = value; }
        }        

        public List<AccomodationRoom> Rooms
        {
            get { return _Rooms; }
            set { _Rooms = value; }
        }
        #endregion
    }

    public class AccomodationRoom
    {
        #region Variables
        RoomDTO _RoomDetail;
        List<RoomBookingDateWiseDTO> _bookingList;
        #endregion

        #region Properties        
        public RoomDTO RoomDetail
        {
            get { return _RoomDetail; }
            set { _RoomDetail = value; }
        }        

        public List<RoomBookingDateWiseDTO> BookingList
        {
            get { return _bookingList; }
            set { _bookingList = value; }
        }
        #endregion
    }
}
