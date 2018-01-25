using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for BALBookingLock
/// </summary>
/// 

namespace FarHorizon.Reservations.BusinessServices.Online.BAL
{
    [Serializable]
    public class BALBookingLock
    {
        public int AccomId { get; set; }
        public string LockIdentifier { get; set; }
        public DateTime LockExpireAt { get; set; }
        public string rooms { get; set; }
        public int roocatid { get; set; }
        public List<LockRoom> LockRooms { get; set; }
    }

    [Serializable]
    public class LockRoom
    {
        public int RoomCategoryId { get; set; }
        public string RoomNo { get; set; }
    }
}