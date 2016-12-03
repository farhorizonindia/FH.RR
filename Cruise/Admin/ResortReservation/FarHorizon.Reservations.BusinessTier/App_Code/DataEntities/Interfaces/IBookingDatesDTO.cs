using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessTier.App_Code.DataEntities.Interfaces
{
    interface IBookingDatesDTO
    {
        DateTime CheckInDate { get; set; }
        DateTime CheckOutDate { get; set; }
    }
}
