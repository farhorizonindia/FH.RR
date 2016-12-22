using System;
using System.Collections.Generic;
using System.Text;
using BusinessTier.App_Code.DataEntities.Interfaces;

namespace BusinessTier.App_Code.DataEntities.Masters
{
    public class clsRoomTypeDTO : IRoomTypeDTO
    {
        int _iRoomTypeId;
        string _sRoomType;
        int _iDefaultNoOfBeds;       

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

        public int DefaultNoOfBeds
        {
            get { return _iDefaultNoOfBeds; }
            set { _iDefaultNoOfBeds = value; }
        }
    }
}
