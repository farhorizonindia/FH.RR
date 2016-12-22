using System;
using System.Collections.Generic;
using System.Text;
using BusinessTier.App_Code.DataEntities.Interfaces;

namespace BusinessTier.App_Code.DataEntities.Masters
{
    public class clsRoomCategoryDTO : IRoomCategoryDTO
    {
        private int _iRoomCategoryID;
        private string _sRoomCategory;

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

    }
}
