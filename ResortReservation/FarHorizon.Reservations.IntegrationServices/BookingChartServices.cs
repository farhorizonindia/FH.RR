using System;
using System.Collections.Generic;
using System.Text;
using FarHorizon.Reservations.Common.DataEntities.Masters;
using FarHorizon.Reservations.Common.DataEntities.Client;
using FarHorizon.Reservations.BusinessTier.Helpers;

namespace FarHorizon.Reservations.BusinessServices
{
    public class BookingChartServices
    {
        #region Booking Chart
        public RegionDTO[] GetRegionDetails()
        {
            try
            {
                BookingChartHelper bookingChartProxy;
                bookingChartProxy = new BookingChartHelper();
                return bookingChartProxy.GetRegionDetails();
            }
            catch (Exception exp)
            {
                throw exp;
            }
        }

        public RoomBookingDateWiseDTO[] GetBookingDataForChart(int AccomodationTypeId, int RegionId, int AccomodationId, DateTime FromDate, DateTime ToDate)
        {
            try
            {
                BookingChartHelper bookingChartProxy;
                bookingChartProxy = new BookingChartHelper();
                return bookingChartProxy.GetBookingDataForChart(AccomodationTypeId, RegionId, AccomodationId, FromDate, ToDate);
            }
            catch (Exception exp)
            {
                throw exp;
            }
        }

        public AccomTypeDTO[] GetBookingChart(int RegionId, int AccomodationTypeId, int AccomodationId, DateTime BookingFromDate, DateTime BookingToDate)
        {
            try
            {
                BookingChartHelper bookingChartProxy;
                bookingChartProxy = new BookingChartHelper();
                return bookingChartProxy.GetBookingChart(RegionId, AccomodationTypeId, AccomodationId, BookingFromDate, BookingToDate);
            }
            catch (Exception exp)
            {
                throw exp;
            }
        }

        public BookingChartDTO[] GetRoomDetailsNew(int RegionId, int AccomodationTypeId, int AccomodationId, DateTime sdate)
        {
            try
            {
                BookingChartHelper bookingChartProxy;
                bookingChartProxy = new BookingChartHelper();
                return bookingChartProxy.GetRoomDetailsNew(RegionId, AccomodationTypeId, AccomodationId,sdate);
            }
            catch (Exception exp)
            {
                throw exp;
            }
        }

        public BookingChartDTO[] GetRoomDetmaintenance(int AccomodationTypeId, int RegionId, int AccomodationId, DateTime FromDate, DateTime ToDate)
        {
            try
            {
                BookingChartHelper bookingChartProxy;
                bookingChartProxy = new BookingChartHelper();
                return bookingChartProxy.GetRoomDetmaintenance(RegionId, AccomodationTypeId, AccomodationId,FromDate,ToDate);
            }
            catch (Exception exp)
            {
                throw exp;
            }
        }
        #endregion

        #region Accomodation Tree
        public BookingChartTreeDTO[] GetTreeData(out string DefaultTreeTypeCode)
        {
            try
            {
                BookingChartHelper bookingChartProxy = new BookingChartHelper();
                return bookingChartProxy.GetTreeData(out DefaultTreeTypeCode);
            }
            catch (Exception exp)
            {
                throw exp;
            }
        }

        public string GetDefaultTreeType()
        {
            try
            {
                BookingChartHelper bookingChartProxy = new BookingChartHelper();
                return bookingChartProxy.GetDefaultTreeType();
            }
            catch (Exception exp)
            {
                throw exp;
            }
        }

        public TreeTypeDTO[] GetTreeTypes()
        {
            try
            {
                BookingChartHelper bookingChartProxy = new BookingChartHelper();
                return bookingChartProxy.GetTreeTypes();
            }
            catch (Exception exp)
            {
                throw exp;
            }
        }

        public void SetDefaultTreeType(int TreeTypeId)
        {
            try
            {
                BookingChartHelper bookingChartProxy = new BookingChartHelper();
                bookingChartProxy.SetDefaultTreeType(TreeTypeId);
            }
            catch (Exception exp)
            {
                throw exp;
            }
        }
        #endregion
    }
}
