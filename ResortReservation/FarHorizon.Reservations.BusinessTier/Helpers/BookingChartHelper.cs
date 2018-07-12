using System;
using System.Collections.Generic;
using System.Text;
using FarHorizon.Reservations.Common.DataEntities.Masters;
using FarHorizon.Reservations.Common.DataEntities.Client;
using FarHorizon.Reservations.BusinessTier.BusinessLogic.BookingChart;

namespace FarHorizon.Reservations.BusinessTier.Helpers
{
    public class BookingChartHelper
    {
        #region Booking Chart
        public RegionDTO[] GetRegionDetails()
        {
            try
            {
                BookingChartViewHandler bookingChartViewManager;
                bookingChartViewManager = new BookingChartViewHandler();
                return bookingChartViewManager.GetRegionDetails();
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
                BookingChartViewHandler bookingChartViewManager;
                bookingChartViewManager = new BookingChartViewHandler();
                return bookingChartViewManager.GetBookingDataForChart(AccomodationTypeId, RegionId, AccomodationId, FromDate, ToDate);
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
                BookingChartViewHandler bookingChartViewManager;
                bookingChartViewManager = new BookingChartViewHandler();
                return bookingChartViewManager.GetBookingChart(RegionId, AccomodationTypeId, AccomodationId, BookingFromDate, BookingToDate);
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
                BookingChartViewHandler bookingChartViewManager;
                bookingChartViewManager = new BookingChartViewHandler();
                return bookingChartViewManager.GetRoomDetailsNew(RegionId, AccomodationTypeId, AccomodationId,sdate);
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
                BookingChartViewHandler bookingChartViewManager;
                bookingChartViewManager = new BookingChartViewHandler();
                return bookingChartViewManager.GetmaintenanceDataForChart(RegionId, AccomodationTypeId, AccomodationId, FromDate, ToDate);
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
                TreeViewHandler treeViewManager = new TreeViewHandler();
                return treeViewManager.GetTreeData(out DefaultTreeTypeCode);
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
                TreeViewHandler treeViewManager = new TreeViewHandler();
                return treeViewManager.GetDefaultTreeType();
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
                TreeViewHandler treeViewManager = new TreeViewHandler();
                return treeViewManager.GetTreeTypes();
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
                TreeViewHandler treeViewManager = new TreeViewHandler();
                treeViewManager.SetDefaultTreeType(TreeTypeId);
            }
            catch (Exception exp)
            {
                throw exp;
            }
        }
        #endregion
    }
}
