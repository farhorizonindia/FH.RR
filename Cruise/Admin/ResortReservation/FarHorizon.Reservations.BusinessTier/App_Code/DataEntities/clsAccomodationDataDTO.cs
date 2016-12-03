using System;
using System.Collections.Generic;
using System.Text;
using BusinessTier.App_Code.DataEntities.Masters;
using BusinessTier.App_Code.DataEntities.Client;

namespace BusinessTier.App_Code.DataEntities
{
    public class Accomodation
    {
        #region Variables
        clsAccomodationDTO _AccomodationDetail;
        List<AccomodationRoomCategory> _Categories;
        #endregion

        #region Properties
        public clsAccomodationDTO AccomodationDetail
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
        clsRoomCategoryDTO _RoomCategory;
        List<AccomodationRoomType> _RoomTypes;
        #endregion

        #region Properties
        public clsRoomCategoryDTO RoomCategory
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
        clsRoomTypeDTO _RoomType;
        List<AccomodationRoom> _Rooms;
        #endregion

        #region Properties
        public clsRoomTypeDTO RoomType
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
        clsRoomDTO _RoomDetail;
        List<clsRoomBookingDateWiseDTO> _Bookings;
        #endregion

        #region Properties        
        public clsRoomDTO RoomDetail
        {
            get { return _RoomDetail; }
            set { _RoomDetail = value; }
        }        

        public List<clsRoomBookingDateWiseDTO> Bookings
        {
            get { return _Bookings; }
            set { _Bookings = value; }
        }
        #endregion
    }
}
