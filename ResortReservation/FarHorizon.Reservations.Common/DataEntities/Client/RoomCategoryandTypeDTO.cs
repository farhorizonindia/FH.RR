using System;
using System.Collections.Generic;
using System.Text;

namespace FarHorizon.Reservations.Common.DataEntities.Client
{
    public class RoomCategoryandTypeDTO
    {
        int _RoomCategoryId;
        string _RoomCategory;
        int _RoomTypeId;
        string _RoomType;        

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
    }
}
