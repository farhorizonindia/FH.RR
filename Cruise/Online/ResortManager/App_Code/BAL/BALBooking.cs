using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
public class BALBooking
{
    public BALBooking()
    {
    }


    public string action { get; set; }
    public int accomId { get; set; }
    public int totpax { get; set; }
    public string RoomId { get; set; }
    public int AgentId { get; set; }
    public string PackageId { get; set; }
    public int DepartureId { get; set; }

    public int roomcatid { get; set; }


    #region Parent Booking Table
    public string _sBookingRef { get; set; }
    public DateTime _dtStartDate { get; set; }
    public DateTime _dtEndDate { get; set; }
    public int _iAccomTypeId { get; set; }
    public int _iAccomId { get; set; }
    public int _iAgentId { get; set; }
    public int _iNights { get; set; }
    public int _iPersons { get; set; }
    public int _BookingStatusId { get; set; }
    public int _SeriesId { get; set; }
    public bool _proposedBooking { get; set; }
    public bool _chartered { get; set; }
    #endregion

    #region ParentTable1 (Room Detail)
    public int _iBookingId { get; set; }
    public string _sRoomNo { get; set; }
    public int _iPaxStaying { get; set; }
    public bool _bConvertTo_Double_Twin { get; set; }
    public string _cRoomStatus { get; set; }
    public decimal _Amt { get; set; }
    public double _Paid { get; set; }

    public string PaymentId { get; set; }

    public string CustomerId { get; set; }



    #endregion

}