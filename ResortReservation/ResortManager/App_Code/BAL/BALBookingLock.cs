using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for BALBookingLock
/// </summary>
public class BALBookingLock
{
    public int AccomId { get; set; }
    public string LockIdentifier { get; set; }
    public DateTime LockExpireAt { get; set; }

    public List<LockRoom> LockRooms { get; set; }
}

public class LockRoom
{
    public int RoomCategoryId { get; set; }
    public string RoomNo { get; set; }
}