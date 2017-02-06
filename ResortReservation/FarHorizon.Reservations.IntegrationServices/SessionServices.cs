using FarHorizon.Reservations.Common.DataEntities.Client;
using FarHorizon.Reservations.Common.DataEntities.Masters;
using FarHorizon.Reservations.SessionManager;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;

namespace FarHorizon.Reservations.BusinessServices
{
    public static class SessionServices
    {
        public static string LoginId_Key
        {
            get { return SessionHelper.LoginId_Key; }
        }

        public static int LoginId
        {
            get { return SessionHelper.LoginId; }
            set { SessionHelper.LoginId = value; }
        }

        #region Client Sessions
        #region BookingChart
        #region Booking Chart Session Properties
        public static BookingChartTreeDTO[] BookingChart_TreeDTO
        {
            get { return SessionHelper.BookingChart_TreeDTO; }
            set { SessionHelper.BookingChart_TreeDTO = value; }
        }
        public static string BookingChart_TreeType
        {
            get { return SessionHelper.BookingChart_TreeType; }
            set { SessionHelper.BookingChart_TreeType = value; }
        }
        public static string BookingChart_TreeArrangeBy
        {
            get { return SessionHelper.BookingChart_TreeArrangeBy; }
            set { SessionHelper.BookingChart_TreeArrangeBy = value; }
        }        
        public static int BookingChart_RegionId
        {
            get { return SessionHelper.BookingChart_RegionId; }
            set { SessionHelper.BookingChart_RegionId = value; }
        }
        public static int BookingChart_AccomodTypeId
        {
            get { return SessionHelper.BookingChart_AccomodTypeId; }
            set { SessionHelper.BookingChart_AccomodTypeId = value; }
        }
        public static int BookingChart_AccomId
        {
            get { return SessionHelper.BookingChart_AccomId; }
            set { SessionHelper.BookingChart_AccomId = value; }
        }
        #endregion
        #endregion BookingChart

        #region Booking
        #region Booking Session Properties
        public static int Booking_BookingId
        {
            get { return SessionHelper.Booking_BookingId; }
            set { SessionHelper.Booking_BookingId = value; }
        }
        public static BookedRooms[] Booking_AllRoomsData
        {
            get { return SessionHelper.Booking_AllRoomsData; }
            set { SessionHelper.Booking_AllRoomsData = value; }
        }
        public static BookedRooms[] Booking_AllRoomsDataPAX
        {
            get { return SessionHelper.Booking_AllRoomsDataPAX; }
            set { SessionHelper.Booking_AllRoomsDataPAX = value; }
        }
        public static AccomTypeDTO[] Booking_AccomodationData
        {
            get { return SessionHelper.Booking_AccomodationData; }
            set { SessionHelper.Booking_AccomodationData = value; }
        }
        public static RoomBookingDateWiseDTO[] Booking_RoomBookingDateWiseDTO
        {
            get { return SessionHelper.Booking_RoomBookingDateWiseDTO; }
            set { SessionHelper.Booking_RoomBookingDateWiseDTO = value; }
        }
        public static int Booking_TotalNights
        {
            get { return SessionHelper.Booking_TotalNights; }
            set { SessionHelper.Booking_TotalNights = value; }
        }
        public static Object Booking_DdlSelectedIndexes
        {
            get { return SessionHelper.Booking_DdlSelectedIndexes; }
            set { SessionHelper.Booking_DdlSelectedIndexes = value; }
        }
        public static Object Booking_RoomsStatus
        {
            get { return SessionHelper.Booking_RoomsStatus; }
            set { SessionHelper.Booking_RoomsStatus = value; }
        }
        #endregion
        #endregion

        #region Booking ChangeRoomPax
        #region Booking Session Properties
        public static SortedList BookingChangeRoomPax_DdlSelectedIndexes
        {
            get { return SessionHelper.BookingChangeRoomPax_DdlSelectedIndexes; }
            //set { SessionHelper.BookingChangeRoomPax_DdlSelectedIndexes = value; }
        }
        #endregion
        #endregion

        #region Booking Confirmation
        #region Booking Confirmation Session Properties
        public static BookedRoomsTotal[] BookingConfirmation_TotalRoomsBooked
        {
            get { return SessionHelper.BookingConfirmation_TotalRoomsBooked; }
            set { SessionHelper.BookingConfirmation_TotalRoomsBooked = value; }
        }
        public static BookingMealPlanDTO[] BookingConfirmation_BookingMealPlanDTO
        {
            get { return SessionHelper.BookingConfirmation_BookingMealPlanDTO; }
            set { SessionHelper.BookingConfirmation_BookingMealPlanDTO = value; }
        }
        public static BookingActivityDTO[] BookingConfirmation_BookingActivityData
        {
            get { return SessionHelper.BookingConfirmation_BookingActivityData; }
            set { SessionHelper.BookingConfirmation_BookingActivityData = value; }
        }
        public static AccomActivityDTO[] BookingConfirmation_AccomodationActivityData
        {
            get { return SessionHelper.BookingConfirmation_AccomodationActivityData; }
            set { SessionHelper.BookingConfirmation_AccomodationActivityData = value; }
        }
        public static MealPlanDTO[] BookingConfirmation_MealPlanData
        {
            get { return SessionHelper.BookingConfirmation_MealPlanData; }
            set { SessionHelper.BookingConfirmation_MealPlanData = value; }
        }
        #endregion
        #endregion

        #region View Booking
        #region View Booking Session Properties
        public static List<ViewBookingDTO> ViewBooking_BookingData
        {
            get { return SessionHelper.ViewBooking_BookingData; }
            set { SessionHelper.ViewBooking_BookingData = value; }
        }
        public static string ViewBooking_SelectedCheckInDate
        {
            get { return Convert.ToString(SessionHelper.ViewBooking_SelectedCheckInDate); }
            set { SessionHelper.ViewBooking_SelectedCheckInDate = value; }
        }
        public static string ViewBooking_SelectedCheckOutDate
        {
            get { return Convert.ToString(SessionHelper.ViewBooking_SelectedCheckOutDate); }
            set { SessionHelper.ViewBooking_SelectedCheckOutDate = value; }
        }
        public static string ViewBooking_SelectedBookingStatus
        {
            get { return Convert.ToString(SessionHelper.ViewBooking_SelectedBookingStatus); }
            set { SessionHelper.ViewBooking_SelectedBookingStatus = value; }
        }
        public static string ViewBooking_SelectedAccomodationType
        {
            get { return Convert.ToString(SessionHelper.ViewBooking_SelectedAccomodationType); }
            set { SessionHelper.ViewBooking_SelectedAccomodationType = value; }
        }
        #endregion
        #endregion

        #region View Series
        #region View Series Session Properties
        public static List<SeriesDTO> ViewSeries_Data
        {
            get { return SessionHelper.ViewSeries_Data; }
            set { SessionHelper.ViewSeries_Data = value; }
        }
        public static DateTime ViewSeries_StartSeriesDate
        {
            get { return Convert.ToDateTime(SessionHelper.ViewSeries_StartSeriesDate); }
            set { SessionHelper.ViewSeries_StartSeriesDate = value; }
        }
        public static string ViewSeries_SelectedAccomodation
        {
            get { return Convert.ToString(SessionHelper.ViewSeries_SelectedAccomodation); }
            set { SessionHelper.ViewSeries_SelectedAccomodation = value; }
        }
        #endregion
        #endregion

        #region Tourist Details
        #region Tourist Details Session Properties
        public static int TouristDetails_TouristNo
        {
            get { return SessionHelper.TouristDetails_TouristNo; }
            set { SessionHelper.TouristDetails_TouristNo = value; }
        }
        public static int TouristDetails_BookingNo
        {
            get { return SessionHelper.TouristDetails_BookingNo; }
            set { SessionHelper.TouristDetails_BookingNo = value; }
        }
        #endregion
        #endregion

        #region Tourist Count Booking
        #region View Booking Session Properties
        public static List<clsTouristCountDTO> TouristCount_BookingData
        {
            get { return SessionHelper.TouristCount_BookingData; }
            set { SessionHelper.TouristCount_BookingData = value; }
        }
        public static string TouristCount_SelectedCheckInDate
        {
            get { return Convert.ToString(SessionHelper.TouristCount_SelectedCheckInDate); }
            set { SessionHelper.TouristCount_SelectedCheckInDate = value; }
        }
        public static string TouristCount_SelectedCheckOutDate
        {
            get { return Convert.ToString(SessionHelper.TouristCount_SelectedCheckOutDate); }
            set { SessionHelper.TouristCount_SelectedCheckOutDate = value; }
        }
        public static string TouristCount_SelectedBookingStatus
        {
            get { return Convert.ToString(SessionHelper.TouristCount_SelectedBookingStatus); }
            set { SessionHelper.TouristCount_SelectedBookingStatus = value; }
        }
        public static string TouristCount_SelectedAccomodationType
        {
            get { return Convert.ToString(SessionHelper.TouristCount_SelectedAccomodationType); }
            set { SessionHelper.TouristCount_SelectedAccomodationType = value; }
        }
        #endregion
        #endregion

        #region Series Booking
        #region Series Booking Session Properties
                public static AccomTypeDTO[] Series_AccomodationData
        {
            get { return SessionHelper.Series_AccomodationData; }
            set { SessionHelper.Series_AccomodationData = value; }
        }
        public static List<BookingDatesWithBookedRoomsDTO> Series_BookedRooms_WithDates
        {
            get { return SessionHelper.Series_BookedRooms_WithDates; }
            set { SessionHelper.Series_BookedRooms_WithDates = value; }
        }
        #endregion
        #endregion

        #region Wait List Management
        #region  Wait List Management Session Properties
        public static RoomBookingDateWiseDTO[] WaitListManagement_WaitListedBookings
        {
            get { return SessionHelper.WaitListManagement_WaitListedBookings; }
            set { SessionHelper.WaitListManagement_WaitListedBookings = value; }
        }
        #endregion
        #endregion

        #region User Info
        public static LoggedInUser LoggedInUser
        {
            get { return SessionHelper.LoggedInUser; }
            set { SessionHelper.LoggedInUser = value; }
        }
        #endregion 
        #endregion Client Sessions

        #region Master Sessions
        #region Master Sessions Properties
        public static string RoomMaster_OperationMode
        {
            get { return Convert.ToString(SessionHelper.RoomMaster_OperationMode); }
            set { SessionHelper.RoomMaster_OperationMode = value; }
        }
        #endregion Master Sessions Properties
        #endregion Master Sessions

        public static Boolean SaveSession(String key, Object value)
        {
            return SessionHelper.SaveSession(key, value);
        }
        public static Object RetrieveSession(String key)
        {
            return SessionHelper.RetrieveSession(key);
        }
        public static Boolean DeleteSession(String key)
        {
            return SessionHelper.DeleteSession(key);
        }
        public static Boolean AbandonSession()
        {
            return SessionHelper.AbandonSession();
        }

        public static void SaveSession<T>(string key, T value)
        {
            SaveSession<T>(key, value, false);
        }


        public static void SaveSession<T>(string key, T value, bool forceSerialize)
        {
            if (typeof(T) == typeof(DataTable) || forceSerialize)
            {
                string json = JsonConvert.SerializeObject(value);
                SessionHelper.SaveSession(key, json);
                return;
            }
            SessionHelper.SaveSession(key, value);
        }

        
        public static T RetrieveSession<T>(string key)
        {
            return RetrieveSession<T>(key, false);
        }

        public static T RetrieveSession<T>(string key, bool forceDeSerialize)
        {
            if (typeof(T) == typeof(DataTable) || forceDeSerialize)
            {
                var value = SessionHelper.RetrieveSession(key);
                if (value != null)
                {
                    string dataTableJsonString = Convert.ToString(SessionHelper.RetrieveSession(key));
                    return JsonConvert.DeserializeObject<T>(dataTableJsonString);
                }                
            }
            return (T)SessionHelper.RetrieveSession(key);
        }
    }
}