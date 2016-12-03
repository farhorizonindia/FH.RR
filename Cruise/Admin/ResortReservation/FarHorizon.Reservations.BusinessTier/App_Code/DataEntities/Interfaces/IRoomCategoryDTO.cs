using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessTier.App_Code.DataEntities.Interfaces
{
    interface IRoomCategoryDTO
    {
        int RoomCategoryId { get; set; }
        string RoomCategory { get; set; }
    }
}
