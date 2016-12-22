using System;
using System.Collections.Generic;
using System.Text;

namespace FarHorizon.Reservations.Common.DataEntities.Masters
{
    public class RoomCategoryDTO
    {
        private int _iRoomCategoryID;
        private String _sRoomCategory;
        private String _categoryAlias;
        
        public int RoomCategoryId
        {
            get { return _iRoomCategoryID; }
            set { _iRoomCategoryID = value; }
        }
        public string RoomCategory
        {
            get { return _sRoomCategory; }
            set { _sRoomCategory = value; }
        }

        public String CategoryAlias
        {
            get { return _categoryAlias; }
            set { _categoryAlias = value; }
        }
    }
}
