using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Data;
using FarHorizon.Reservations.Common;
using FarHorizon.Reservations.Common.DataEntities.Masters;
using FarHorizon.Reservations.Common.DataEntities.Client;
using FarHorizon.Reservations.BusinessTier.BusinessLogic.BookingEngine;
using FarHorizon.Reservations.Common.DataEntities;
using FarHorizon.Reservations.BusinessTier.Helpers;
using FarHorizon.Reservations.Common.DataEntities.InputOutput;

namespace FarHorizon.Reservations.BusinessServices
{
    public class BookingServices
    {
        BookingHelper bookingHelper;

        #region Add Method(s)
        public bool AddBooking(BookingDTO oBookingData, BookedRooms[] oBookedRooms, BookingWaitListDTO[] oBookingWaitListData, out int BookingId)
        {
            try
            {
                if (bookingHelper == null)
                    bookingHelper = new BookingHelper();
                return bookingHelper.AddBooking(oBookingData, oBookedRooms, oBookingWaitListData, out BookingId);
            }
            catch (Exception exp)
            {
                throw exp;
            }
        }

        public bool AddBookingMealPlan(BookingMealPlanDTO[] oBookingMealPlanDTO)
        {
            try
            {
                if (bookingHelper == null)
                    bookingHelper = new BookingHelper();
                bookingHelper.AddBookingMealPlan(oBookingMealPlanDTO);
                return true;
            }
            catch (Exception exp)
            {
                throw exp;
            }

        }

        public bool AddBookingActivities(BookingActivityDTO[] oBookingActivityDTO)
        {
            try
            {
                if (bookingHelper == null)
                    bookingHelper = new BookingHelper();
                return bookingHelper.AddBookingActivities(oBookingActivityDTO);
            }
            catch (Exception exp)
            {
                throw exp;
            }
        }

        #endregion

        #region Update Method(s)
        public bool UpdateBooking(BookingDTO oBookingData, BookedRooms[] oBookedRooms, BookingWaitListDTO[] oBookingWaitListData, BookedRooms[] TotallyRemovedRoomCategoryAndType)
        {
            try
            {
                if (bookingHelper == null)
                    bookingHelper = new BookingHelper();
                return bookingHelper.UpdateBooking(oBookingData, oBookedRooms, oBookingWaitListData, TotallyRemovedRoomCategoryAndType);
            }
            catch (Exception exp)
            {
                throw exp;
            }
        }

        public bool UpdateBookingMealPlan(BookingMealPlanDTO[] oBookingMealPlanDTO)
        {
            try
            {
                if (bookingHelper == null)
                    bookingHelper = new BookingHelper();
                return bookingHelper.UpdateBookingMealPlan(oBookingMealPlanDTO);
            }
            catch (Exception exp)
            {
                throw exp;
            }
        }

        public bool UpdateBookingActivites(BookingActivityDTO[] oBookingActivityDTO)
        {
            try
            {
                if (bookingHelper == null)
                    bookingHelper = new BookingHelper();
                return bookingHelper.UpdateBookingActivites(oBookingActivityDTO);
            }
            catch (Exception exp)
            {
                throw exp;
            }
        }
        #endregion

        #region Delete Method(s)

        public bool DeleteBooking(int BookingId)
        {
            try
            {
                if (bookingHelper == null)
                    bookingHelper = new BookingHelper();
                return bookingHelper.DeleteBooking(BookingId);
            }
            catch (Exception exp)
            {
                throw exp;
            }
        }

        public void DeleteBookingRooms(int BookingId)
        {
            try
            {
                DeleteBookingRooms(BookingId, 0);
            }
            catch (Exception exp)
            {
                throw exp;
            }
        }

        public void DeleteBookingRooms(int BookingId, int AccomodationId)
        {
            try
            {
                DeleteBookingRooms(BookingId, AccomodationId, "");
            }
            catch (Exception exp)
            {
                throw exp;
            }
        }

        public bool DeleteBookingRooms(int BookingId, int AccomodationId, string RoomNo)
        {
            try
            {
                if (bookingHelper == null)
                    bookingHelper = new BookingHelper();
                return bookingHelper.DeleteBookingRooms(BookingId, AccomodationId, RoomNo);
            }
            catch (Exception exp)
            {
                throw exp;
            }
        }

        public bool DeleteBookingMealPlan(int BookingId)
        {
            try
            {
                if (bookingHelper == null)
                    bookingHelper = new BookingHelper();
                return bookingHelper.DeleteBookingMealPlan(BookingId);
            }
            catch (Exception exp)
            {
                throw exp;
            }
        }

        public bool DeleteBookingActivities(int BookingId)
        {
            try
            {
                if (bookingHelper == null)
                    bookingHelper = new BookingHelper();
                return bookingHelper.DeleteBookingActivities(BookingId);
            }
            catch (Exception exp)
            {
                throw exp;
            }
        }
        #endregion

        #region Get Method(s)

        public BookingDTO GetBookingDetails(int BookingId)
        {
            try
            {
                if (bookingHelper == null)
                    bookingHelper = new BookingHelper();
                return bookingHelper.GetBookingDetails(BookingId);
            }
            catch (Exception exp)
            {
                throw exp;
            }
        }

        public DataSet CheckifCharteredBookingExists(DateTime frm, DateTime to)
        {
            try
            {
                if (bookingHelper == null)
                    bookingHelper = new BookingHelper();
                return bookingHelper.CheckifCharteredBookingExists(frm,to);
            }
            catch (Exception exp)
            {
                throw exp;
            }
        }


        


        public BookingDTO[] GetConfirmMailDetails(int BookingId)
        {
            try
            {
                if (bookingHelper == null)
                    bookingHelper = new BookingHelper();
                return bookingHelper.GetConfirmMailDetails(BookingId);
            }
            catch (Exception exp)
            {
                throw exp;
            }
        }

        public BookingDTO[] GetRoomBookings(string RoomNo)
        {
            try
            {
                if (bookingHelper == null)
                    bookingHelper = new BookingHelper();
                return bookingHelper.GetRoomBookings(RoomNo);
            }
            catch (Exception exp)
            {
                throw exp;
            }
            finally
            {
                bookingHelper = null;
            }
        }

        //public List<BookingDTO> GetBookings(DateTime FromDate, DateTime ToDate)
        //{
        //    try
        //    {
        //        return GetBookings(FromDate, ToDate, ENums.BookingStatusTypes.NONE);
        //    }
        //    catch (Exception exp)
        //    {
        //        throw exp;
        //    }
        //}

        public List<ViewBookingDTO> GetBookings(cdtGetBookingsInput getBookingsInput)
        {
            try
            {
                if (bookingHelper == null)
                    bookingHelper = new BookingHelper();
                return bookingHelper.GetBookings(getBookingsInput);
            }
            catch (Exception exp)
            {
                throw exp;
            }
        }


        public List<ViewBookingDTO> GetBookingsFH(cdtGetBookingsInput getBookingsInput)
        {
            try
            {
                if (bookingHelper == null)
                    bookingHelper = new BookingHelper();
                return bookingHelper.GetBookingsFH(getBookingsInput);
            }
            catch (Exception exp)
            {
                throw exp;
            }
        }

        public DataSet GetBookingsCruiseFH(cdtGetBookingsInput getBookingsInput)
        {
            try
            {
                if (bookingHelper == null)
                    bookingHelper = new BookingHelper();
                return bookingHelper.GetBookingsCruiseFH(getBookingsInput);
            }
            catch (Exception exp)
            {
                throw exp;
            }
        }

        //public List<BookingDTO> GetBookings(DateTime FromDate, DateTime ToDate, ENums.BookingStatusTypes BookingStatusType, int AccomTypeId)
        //{
        //    try
        //    {
        //        if (bookingHelper == null)
        //            bookingHelper = new BookingHelper();
        //        return bookingHelper.GetBookings(FromDate, ToDate, BookingStatusType, AccomTypeId, 0);
        //    }
        //    catch (Exception exp)
        //    {
        //        throw exp;
        //    }
        //}

        //public List<BookingDTO> GetBookings(DateTime FromDate, DateTime ToDate, ENums.BookingStatusTypes BookingStatusType, int AccomTypeId, int AccomId)
        //{
        //    try
        //    {
        //        if (bookingHelper == null)
        //            bookingHelper = new BookingHelper();
        //        return bookingHelper.GetBookings(FromDate, ToDate, BookingStatusType, AccomTypeId, AccomId);
        //    }
        //    catch (Exception exp)
        //    {
        //        throw exp;
        //    }
        //}

        public BookingRoomDTO[] GetBookingRoomDetails(int BookingId)
        {
            try
            {
                if (bookingHelper == null)
                    bookingHelper = new BookingHelper();
                return bookingHelper.GetBookingRoomDetails(BookingId);
            }
            catch (Exception exp)
            {
                throw exp;
            }
        }

        public BookingRoomDTO[] GetBookingRoomDetails(int BookingId, int AccomodationId)
        {
            BookingRoomDTO[] oBookingRoomDTO;
            if (bookingHelper == null)
                bookingHelper = new BookingHelper();
            oBookingRoomDTO = bookingHelper.GetBookingRoomDetails(BookingId);
            bookingHelper = null;
            return oBookingRoomDTO;
        }

        public BookingRoomDTO GetBookingRoomDetails(int BookingId, int AccomodationId, string RoomNo)
        {
            try
            {
                if (bookingHelper == null)
                    bookingHelper = new BookingHelper();
                return bookingHelper.GetBookingRoomDetails(BookingId, AccomodationId, RoomNo);
            }
            catch (Exception exp)
            {
                throw exp;
            }
        }

        public BookingRoomDTO[] GetBookingRoomDetails(DateTime StartDate, DateTime EndDate)
        {
            try
            {
                if (bookingHelper == null)
                    bookingHelper = new BookingHelper();
                return bookingHelper.GetBookingRoomDetails(StartDate, EndDate);
            }
            catch (Exception exp)
            {
                throw exp;
            }
        }

        public BookingDTO[] GetBookingDetails(DateTime FromDate, DateTime ToDate)
        {
            try
            {
                if (bookingHelper == null)
                    bookingHelper = new BookingHelper();
                return bookingHelper.GetBookingDetails(FromDate, ToDate);
            }
            catch (Exception exp)
            {
                throw exp;
            }
        }

        public BookingMealPlanDTO[] GetBookingMealPlanData(int BookingId)
        {
            try
            {
                if (bookingHelper == null)
                    bookingHelper = new BookingHelper();
                return bookingHelper.GetBookingMealPlanData(BookingId);
            }
            catch (Exception exp)
            {
                throw exp;
            }
        }

        public BookingActivityDTO[] GetBookingActivities(int BookingId)
        {
            try
            {
                if (bookingHelper == null)
                    bookingHelper = new BookingHelper();
                return bookingHelper.GetBookingActivities(BookingId);
            }
            catch (Exception exp)
            {
                throw exp;
            }
        }

        public BookingActivityDTO[] GetBookingActivities(int BookingId, int ActivityId)
        {
            try
            {
                if (bookingHelper == null)
                    bookingHelper = new BookingHelper();
                return bookingHelper.GetBookingActivities(BookingId, ActivityId);
            }
            catch (Exception exp)
            {
                throw exp;
            }
        }

        public AvailableRoomNos[] GetAvailableRoomNos(int iRoomTypeID, DateTime StartDate, int iAccomId)
        {
            try
            {
                if (bookingHelper == null)
                    bookingHelper = new BookingHelper();
                return bookingHelper.GetAvailableRoomNos(iRoomTypeID, StartDate, iAccomId);
            }
            catch (Exception exp)
            {
                throw exp;
            }
        }

        public BookedRooms[] GetAllRooms(DateTime dtStartDate, DateTime EndDate, int iAccomId, int BookingId)
        {
            try
            {
                if (bookingHelper == null)
                    bookingHelper = new BookingHelper();
                return bookingHelper.GetAllRooms(dtStartDate, EndDate, iAccomId, BookingId);
            }
            catch (Exception exp)
            {
                throw exp;
            }
        }

        public BookedRooms[] GetAllRoomspgload(DateTime dtStartDate, DateTime EndDate, int iAccomId, int BookingId)
        {
            try
            {
                if (bookingHelper == null)
                    bookingHelper = new BookingHelper();
                return bookingHelper.GetAllRoomspgload(dtStartDate, EndDate, iAccomId, BookingId);
            }
            catch (Exception exp)
            {
                throw exp;
            }
        }

        public BookingWaitListDTO[] GetBlockedBookings(int BookingId)
        {
            try
            {
                if (bookingHelper == null)
                    bookingHelper = new BookingHelper();
                return bookingHelper.GetBlockedBookings(BookingId);
            }
            catch (Exception exp)
            {
                throw exp;
            }
        }

        public RoomBookingDateWiseDTO[] GetRoomOtherBookings(DateTime StartDate, DateTime EndDate, int notThisBookingId, int AccomTypeId, int AccomId, string RoomNo)
        {
            try
            {
                if (bookingHelper == null)
                    bookingHelper = new BookingHelper();
                return bookingHelper.GetRoomOtherBookings(StartDate, EndDate, notThisBookingId, AccomTypeId, AccomId, RoomNo);
            }
            catch (Exception exp)
            {
                throw exp;
            }
        }

        public RoomBookingDateWiseDTO[] GetWaitListedBookings(DateTime StartDate, DateTime EndDate, int notThisBookingId, int AccomId)
        {
            try
            {
                if (bookingHelper == null)
                    bookingHelper = new BookingHelper();
                return bookingHelper.GetWaitListedBookings(StartDate, EndDate, notThisBookingId, AccomId);
            }
            catch (Exception exp)
            {
                throw exp;
            }
        }

        public int GetBookingReferenceCount(BookingDTO BookingData)
        {
            try
            {
                if (bookingHelper == null)
                    bookingHelper = new BookingHelper();
                return bookingHelper.GetBookingReferenceCount(BookingData);
            }
            catch (Exception exp)
            {
                throw exp;
            }
        }

        public Accomodation GetReleasedRooms(int BookingId)
        {
            try
            {
                if (bookingHelper == null)
                    bookingHelper = new BookingHelper();
                return bookingHelper.GetReleasedRooms(BookingId);
            }
            catch (Exception exp)
            {
                throw exp;
            }
        }

        public BookingDTO[] GetWaitlistedBookingsForReleasedCatType(int BookingId, int RoomCategoryId, int RoomTypeId)
        {
            try
            {
                if (bookingHelper == null)
                    bookingHelper = new BookingHelper();
                return bookingHelper.GetWaitlistedBookingsForReleasedCatType(BookingId, RoomCategoryId, RoomTypeId);
            }
            catch (Exception exp)
            {
                throw exp;
            }
        }
        #endregion

        #region Other Helper Method(s)

        public bool AllocateRoomsToWaitListedBooking(int BookingId, string RoomList, int RoomCategoryId, int RoomTypeId)
        {
            try
            {
                //RoomList is comma-seperated list of all the room no's.
                if (bookingHelper == null)
                    bookingHelper = new BookingHelper();
                return bookingHelper.AllocateRoomsToWaitListedBooking(BookingId, RoomList, RoomCategoryId, RoomTypeId);
            }
            catch (Exception exp)
            {
                throw exp;
            }
        }

        public bool ConfirmBooking(BookingDTO oBookingData)
        {
            try
            {
                if (bookingHelper == null)
                    bookingHelper = new BookingHelper();
                return bookingHelper.ConfirmBooking(oBookingData);
            }
            catch (Exception exp)
            {
                throw exp;
            }
        }

        public bool CancelBooking(int BookingId)
        {
            try
            {
                if (bookingHelper == null)
                    bookingHelper = new BookingHelper();
                return bookingHelper.CancelBooking(BookingId);
            }
            catch (Exception exp)
            {
                throw exp;
            }
        }

        public bool DeleteRemovedRoomCategoryAndType(BookedRooms[] TotallyRemovedRCRT)
        {
            try
            {
                if (bookingHelper == null)
                    bookingHelper = new BookingHelper();
                return bookingHelper.DeleteRemovedRoomCategoryAndType(TotallyRemovedRCRT);
            }
            catch (Exception exp)
            {
                throw exp;
            }
        }

        public bool CancelBookingConfirmation(int BookingId)
        {
            try
            {
                if (bookingHelper == null)
                    bookingHelper = new BookingHelper();
                return bookingHelper.CancelBookingConfirmation(BookingId);
            }
            catch (Exception exp)
            {
                throw exp;
            }
        }
        #endregion
    }
}
