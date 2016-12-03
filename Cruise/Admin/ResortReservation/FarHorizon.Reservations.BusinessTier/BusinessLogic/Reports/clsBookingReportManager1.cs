using System;
using System.Collections.Generic;
using System.Text;
using BusinessTier.App_Code.DataEntities.Client;
using BusinessTier.App_Code.BusinessLogic.BookingEngine;

namespace BusinessTier.App_Code.BusinessLogic.BookingEngine.Reports
{
    public class clsBookingReportManager1
    {
        clsBookingRoomReportsHandler1 oBookingRoomReportsHandler = null;

        public clsBookingRoomReportsData[] GetDetailedBookingDetails(int BookingId)
        {
            if (oBookingRoomReportsHandler == null)
                oBookingRoomReportsHandler = new clsBookingRoomReportsHandler1();
            return oBookingRoomReportsHandler.GetDetailedBookingDetails(BookingId);
        }
        
    }
}
