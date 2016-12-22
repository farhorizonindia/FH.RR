using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Data;
using FarHorizon.Reservations.Common.DataEntities.Masters;
using FarHorizon.Reservations.Common.DataEntities.Client;
using FarHorizon.Reservations.BusinessTier;
using FarHorizon.Reservations.Common;
using FarHorizon.Reservations.BusinessTier.BusinessLogic.BookingEngine;
using FarHorizon.Reservations.Common.DataEntities;
using FarHorizon.Reservations.Common.DataEntities.InputOutput;

namespace FarHorizon.Reservations.BusinessTier.Helpers
{
    public class BookingHelper
    {
        BookingHandler bookingHandler;
        BookingRoomHandler bookingRoomHandler;
        BookingConfirmationHandler bookingConfirmationHandler;
        BookingMealPlanHandler bookingMealPlanHandler;
        BookingWaitListHandler bookingWaitListHandler;
        BookingActivitiesHandler bookingActivitiesHandler;
        BookingRoomReleaseHandler bookingRoomReleaseHandler;

        #region Add Methods
        public bool AddBooking(BookingDTO oBookingData, BookedRooms[] oBookedRooms, BookingWaitListDTO[] oBookingWaitListData, out int BookingId)
        {
            try
            {
                if (bookingHandler == null)
                    bookingHandler = new BookingHandler();
                return bookingHandler.AddBooking(oBookingData, oBookedRooms, oBookingWaitListData, out BookingId);
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
                if (bookingMealPlanHandler == null)
                    bookingMealPlanHandler = new BookingMealPlanHandler();
                return bookingMealPlanHandler.AddBookingMealPlan(oBookingMealPlanDTO);
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
                if (bookingActivitiesHandler == null)
                    bookingActivitiesHandler = new BookingActivitiesHandler();
                return bookingActivitiesHandler.AddBookingActivity(oBookingActivityDTO);
            }
            catch (Exception exp)
            {
                throw exp;
            }
        }

        #endregion

        #region Update Methods
        public bool UpdateBooking(BookingDTO oBookingData, BookedRooms[] oBookedRooms, BookingWaitListDTO[] oBookingWaitListData, BookedRooms[] TotallyRemovedRoomCategoryAndType)
        {
            try
            {
                if (bookingHandler == null)
                    bookingHandler = new BookingHandler();
                return bookingHandler.UpdateBooking(oBookingData, oBookedRooms, oBookingWaitListData, TotallyRemovedRoomCategoryAndType);
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
                if (bookingMealPlanHandler == null)
                    bookingMealPlanHandler = new BookingMealPlanHandler();
                return bookingMealPlanHandler.UpdateBookingMealPlan(oBookingMealPlanDTO);
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
                if (bookingActivitiesHandler == null)
                    bookingActivitiesHandler = new BookingActivitiesHandler();
                return bookingActivitiesHandler.UpdateBookingActivity(oBookingActivityDTO);
            }
            catch (Exception exp)
            {
                throw exp;
            }
        }
        #endregion

        #region Delete Methods

        public bool DeleteBooking(int BookingId)
        {
            try
            {
                if (bookingHandler == null)
                    bookingHandler = new BookingHandler();
                return bookingHandler.DeleteBooking(BookingId);
            }
            catch (Exception exp)
            {
                throw exp;
            }
        }

        public bool DeleteBookingRooms(int BookingId)
        {
            try
            {
                if (bookingRoomHandler == null)
                    bookingRoomHandler = new BookingRoomHandler();
                return bookingRoomHandler.DeleteBookingRooms(BookingId);
            }
            catch (Exception exp)
            {
                throw exp;
            }
        }

        public bool DeleteBookingRooms(int BookingId, int AccomodationId)
        {
            try
            {
                if (bookingRoomHandler == null)
                    bookingRoomHandler = new BookingRoomHandler();
                return bookingRoomHandler.DeleteBookingRooms(BookingId, AccomodationId);
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
                if (bookingRoomHandler == null)
                    bookingRoomHandler = new BookingRoomHandler();
                return bookingRoomHandler.DeleteBookingRooms(BookingId, AccomodationId, RoomNo);
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
                if (bookingMealPlanHandler == null)
                    bookingMealPlanHandler = new BookingMealPlanHandler();
                return bookingMealPlanHandler.DeleteBookingMealPlan(BookingId);
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
                if (bookingActivitiesHandler == null)
                    bookingActivitiesHandler = new BookingActivitiesHandler();
                return bookingActivitiesHandler.DeleteBookingActivities(BookingId);
            }
            catch (Exception exp)
            {
                throw exp;
            }
        }

        #endregion

        #region Get Methods

        public BookingDTO GetBookingDetails(int BookingId)
        {
            try
            {
                if (bookingHandler == null)
                    bookingHandler = new BookingHandler();
                return bookingHandler.GetBookingDetails(BookingId);
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
                if (bookingHandler == null)
                    bookingHandler = new BookingHandler();
                return bookingHandler.CheckifCharteredBookingExists(frm, to);
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
                if (bookingHandler == null)
                    bookingHandler = new BookingHandler();
                return bookingHandler.GetConfirmMail(BookingId);
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
                if (bookingHandler == null)
                    bookingHandler = new BookingHandler();
                return bookingHandler.GetRoomBookings(RoomNo);
            }
            catch (Exception exp)
            {
                throw exp;
            }
            finally
            {
                bookingHandler = null;
            }
        }

        //public List<BookingDTO> GetBookings(DateTime FromDate, DateTime ToDate)
        //{
        //    try
        //    {
        //        if (bookingHandler == null)
        //            bookingHandler = new BookingHandler();
        //        return bookingHandler.GetBookings(FromDate, ToDate);
        //    }
        //    catch (Exception exp)
        //    {
        //        throw exp;
        //    }
        //}

        //public List<BookingDTO> GetBookings(DateTime FromDate, DateTime ToDate, ENums.BookingStatusTypes BookingStatusType)
        //{
        //    try
        //    {
        //        if (bookingHandler == null)
        //            bookingHandler = new BookingHandler();
        //        return bookingHandler.GetBookings(FromDate, ToDate, BookingStatusType);
        //    }
        //    catch (Exception exp)
        //    {
        //        throw exp;
        //    }
        //}

        //public List<BookingDTO> GetBookings(DateTime FromDate, DateTime ToDate, ENums.BookingStatusTypes BookingStatusType, int AccomTypeId)
        //{
        //    try
        //    {
        //        if (bookingHandler == null)
        //            bookingHandler = new BookingHandler();
        //        return bookingHandler.GetBookings(FromDate, ToDate, BookingStatusType, AccomTypeId, 0);
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
        //        if (bookingHandler == null)
        //            bookingHandler = new BookingHandler();
        //        return bookingHandler.GetBookings(FromDate, ToDate, BookingStatusType, AccomTypeId, AccomId);
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
                if (bookingHandler == null)
                    bookingHandler = new BookingHandler();
                return bookingHandler.GetBookings(getBookingsInput);
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
                if (bookingHandler == null)
                    bookingHandler = new BookingHandler();
                return bookingHandler.GetBookingsFH(getBookingsInput);
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
                if (bookingHandler == null)
                    bookingHandler = new BookingHandler();
                return bookingHandler.GetBookingsCruiseFH(getBookingsInput);
            }
            catch (Exception exp)
            {
                throw exp;
            }
        }



        public BookingRoomDTO[] GetBookingRoomDetails(int BookingId)
        {
            try
            {
                if (bookingRoomHandler == null)
                    bookingRoomHandler = new BookingRoomHandler();
                return bookingRoomHandler.GetBookingRoomDetails(BookingId);
            }
            catch (Exception exp)
            {
                throw exp;
            }
        }

        public BookingRoomDTO[] GetBookingRoomDetails(int BookingId, int AccomodationId)
        {
            try
            {
                if (bookingRoomHandler == null)
                    bookingRoomHandler = new BookingRoomHandler();
                return bookingRoomHandler.GetBookingRoomDetails(BookingId);
            }
            catch (Exception exp)
            {
                throw exp;
            }
        }

        public BookingRoomDTO GetBookingRoomDetails(int BookingId, int AccomodationId, string RoomNo)
        {
            try
            {
                if (bookingRoomHandler == null)
                    bookingRoomHandler = new BookingRoomHandler();
                return bookingRoomHandler.GetBookingRoomDetails(BookingId, AccomodationId, RoomNo);
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
                if (bookingRoomHandler == null)
                    bookingRoomHandler = new BookingRoomHandler();
                return bookingRoomHandler.GetBookingRoomDetails(StartDate, EndDate);
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
                if (bookingHandler == null)
                    bookingHandler = new BookingHandler();
                return bookingHandler.GetBookingDetails(FromDate, ToDate);
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
                if (bookingMealPlanHandler == null)
                    bookingMealPlanHandler = new BookingMealPlanHandler();
                return bookingMealPlanHandler.GetBookingMealPlan(BookingId);
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
                if (bookingActivitiesHandler == null)
                    bookingActivitiesHandler = new BookingActivitiesHandler();
                return bookingActivitiesHandler.GetBookingActivities(BookingId);
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
                if (bookingActivitiesHandler == null)
                    bookingActivitiesHandler = new BookingActivitiesHandler();
                return bookingActivitiesHandler.GetBookingActivities(BookingId, ActivityId);
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
                if (bookingRoomHandler == null)
                    bookingRoomHandler = new BookingRoomHandler();
                return bookingRoomHandler.GetAvailableRoomNos(iRoomTypeID, StartDate, iAccomId);
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
                if (bookingRoomHandler == null)
                    bookingRoomHandler = new BookingRoomHandler();
                return bookingRoomHandler.GetAllRooms(dtStartDate, EndDate, iAccomId, BookingId);
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
                if (bookingRoomHandler == null)
                    bookingRoomHandler = new BookingRoomHandler();
                return bookingRoomHandler.GetAllRoomspgload(dtStartDate, EndDate, iAccomId, BookingId);
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
                if (bookingWaitListHandler == null)
                    bookingWaitListHandler = new BookingWaitListHandler();
                return bookingWaitListHandler.GetBlockedBookings(BookingId);
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
                if (bookingRoomHandler == null)
                    bookingRoomHandler = new BookingRoomHandler();
                return bookingRoomHandler.GetRoomOtherBookings(StartDate, EndDate, notThisBookingId, AccomTypeId, AccomId, RoomNo);
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
                if (bookingWaitListHandler == null)
                    bookingWaitListHandler = new BookingWaitListHandler();
                return bookingWaitListHandler.GetWaitListedBookings(StartDate, EndDate, notThisBookingId, AccomId);
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
                if (bookingHandler == null)
                    bookingHandler = new BookingHandler();
                return bookingHandler.GetBookingReferenceCount(BookingData);
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
                if (bookingRoomReleaseHandler == null)
                    bookingRoomReleaseHandler = new BookingRoomReleaseHandler();
                return bookingRoomReleaseHandler.GetReleasedRooms(BookingId);
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
                if (bookingRoomReleaseHandler == null)
                    bookingRoomReleaseHandler = new BookingRoomReleaseHandler();
                return bookingRoomReleaseHandler.GetWaitlistedBookingsForReleasedCatType(BookingId, RoomCategoryId, RoomTypeId);
            }
            catch (Exception exp)
            {
                throw exp;
            }
        }
        #endregion

        #region Other Helper Methods

        public bool AllocateRoomsToWaitListedBooking(int BookingId, string RoomList, int RoomCategoryId, int RoomTypeId)
        {
            try
            {
                //RoomList is comma-seperated list of all the room no's.
                if (bookingWaitListHandler == null)
                    bookingWaitListHandler = new BookingWaitListHandler();
                return bookingWaitListHandler.AllocateRoomsToWaitListedBooking(BookingId, RoomList, RoomCategoryId, RoomTypeId);
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
                if (bookingConfirmationHandler == null)
                    bookingConfirmationHandler = new BookingConfirmationHandler();
                return bookingConfirmationHandler.ConfirmBooking(oBookingData);
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
                if (bookingHandler == null)
                    bookingHandler = new BookingHandler();
                return bookingHandler.CancelBooking(BookingId);
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
                if (bookingRoomHandler == null)
                    bookingRoomHandler = new BookingRoomHandler();
                return bookingRoomHandler.DeleteRemovedRoomCategoryAndType(TotallyRemovedRCRT);
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
                if (bookingConfirmationHandler == null)
                    bookingConfirmationHandler = new BookingConfirmationHandler();
                return bookingConfirmationHandler.CancelBookingConfirmation(BookingId);
            }
            catch (Exception exp)
            {
                throw exp;
            }
        }

        #endregion
    }
}
