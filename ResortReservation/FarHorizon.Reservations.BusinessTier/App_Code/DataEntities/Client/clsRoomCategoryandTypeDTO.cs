using System;
using System.Collections.Generic;
using System.Text;
using BusinessTier.App_Code.DataEntities.Interfaces;

namespace BusinessTier.App_Code.DataEntities.Client
{
    public class clsRoomCategoryandTypeDTO:IRoomCategoryDTO, IRoomTypeDTO
    {
        int _RoomCategoryId;
        string _RoomCategory;
        int _RoomTypeId;
        string _RoomType;
        #region IRoomCategoryDTO Members

        public int RoomCategoryId
        {
            get
            {
                return _RoomCategoryId;
            }
            set
            {
                _RoomCategoryId = value;
            }
        }

        public string RoomCategory
        {
            get
            {
                return _RoomCategory;
            }
            set
            {
                _RoomCategory = value;  
            }
        }

        #endregion

        #region IRoomTypeDTO Members

        public int RoomTypeId
        {
            get
            {
                return _RoomTypeId;
            }
            set
            {
                _RoomTypeId = value;
            }
        }

        public string RoomType
        {
            get
            {
                return _RoomType;
            }
            set
            {
                _RoomType = value;
            }
        }

        #endregion
    }
}
