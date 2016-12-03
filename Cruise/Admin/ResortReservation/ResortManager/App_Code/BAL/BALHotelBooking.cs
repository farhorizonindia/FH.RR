using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for BALHotelBooking
/// </summary>
public class BALHotelBooking
{
    public BALHotelBooking()
    {
        //
        // TODO: Add constructor logic here
        //
    }


    public string action { get; set; }
    public int Accomid { get; set; }

    public int RoomCateId { get; set; }
    public string roomno { get; set; }
    public int agentid { get; set; }
    public int TotPax { get; set; }

    public int iBookingId { get; set; }
    public int iMealPlanId { get; set; }
    public DateTime dtMealDate { get; set; }
    public bool bWelcomeDrink { get; set; }
    public bool bBreakfast { get; set; }
    public bool bLunch { get; set; }
    public bool bEveSnacks { get; set; }
    public bool bDinner { get; set; }

    public DateTime checkin { get; set; }
    public DateTime Checkout { get; set; }
    public int Reqnoofrooms { get; set; }

    public int RoomTypeId { get; set; }
    public string roomstring { get; set; }

    public bool Convertible_To_Double { get; set; }


}