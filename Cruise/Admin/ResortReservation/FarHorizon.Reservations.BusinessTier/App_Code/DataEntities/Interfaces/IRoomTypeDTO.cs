using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessTier.App_Code.DataEntities.Interfaces
{
    interface IRoomTypeDTO
    {
        int RoomTypeId { get; set; }
        string RoomType { get; set; }
    }
}
