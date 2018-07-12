using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using FarHorizon.Reservations.Common.DataEntities.Client;
using FarHorizon.Reservations.Common.DataEntities.Masters;
using FarHorizon.Reservations.SessionManager;
using FarHorizon.Reservations.Common;
using System.Collections;

namespace FarHorizon.Reservations.SessionManager
{
    public static class SessionHelper
    {
        public static string LoginId_Key
        {
            get { return Constants._LoginId; }
        }
        public static int LoginId
        {
            get { return Convert.ToInt32(SessionManager.RetrieveSession(Constants._LoginId)); }
            set { SessionManager.SaveSession(Constants._LoginId, value); }
        }

        #region Client Sessions
        #region BookingChart
        #region Booking Chart Session Keys
        public static string BookingChart_TreeData_Key
        {
            get { return Constants._BookingChart_TreeDTO; }
        }
        public static string BookingChart_TreeType_Key
        {
            get { return Constants._BookingChart_TreeType; }
        }
        public static string BookingChart_TreeArrangeBy_Key
        {
            get { return Constants._BookingChart_TreeArrangeBy; }
        }
        public static string BookingChart_TableMonthChart_Key
        {
            get { return Constants._BookingChart_TableMonthChart; }
        }
        public static string BookingChart_TableBookingTable_Key
        {
            get { return Constants._BookingChart_TableBookingTable; }
        }
        public static string BookingChart_RegionId_Key
        {
            get { return Constants._BookingChart_RegionId; }
        }
        public static string BookingChart_AccomodTypeId_Key
        {
            get { return Constants._BookingChart_AccomodTypeId; }
        }
        public static string BookingChart_AccomId_Key
        {
            get { return Constants._BookingChart_AccomId; }
        }
        #endregion

        #region Booking Chart Session Properties
        public static BookingChartTreeDTO[] BookingChart_TreeDTO
        {
            get
            {
                Object O = SessionManager.RetrieveSession(Constants._BookingChart_TreeDTO);
                if (O == null)
                    return null;
                else
                    return (BookingChartTreeDTO[])O;
            }
            set { SessionManager.SaveSession(Constants._BookingChart_TreeDTO, value); }
        }
        public static string BookingChart_TreeType
        {
            get
            {
                Object O = SessionManager.RetrieveSession(Constants._BookingChart_TreeType);
                if (O == null)
                    return "";
                else
                    return Convert.ToString(O);
            }
            set { SessionManager.SaveSession(Constants._BookingChart_TreeType, value); }
        }
        public static string PropsedBook
        {
            get
            {
                Object O = SessionManager.RetrieveSession(Constants.Proposd);
                if (O == null)
                    return "";
                else
                    return Convert.ToString(O);
            }
            set { SessionManager.SaveSession(Constants.Proposd, value); }
        }
        public static string BookingChart_TreeArrangeBy
        {
            get
            {
                Object O = SessionManager.RetrieveSession(Constants._BookingChart_TreeArrangeBy);
                if (O == null)
                    return "";
                else
                    return Convert.ToString(O);
            }
            set { SessionManager.SaveSession(Constants._BookingChart_TreeArrangeBy, value); }
        }
        public static Object BookingChart_TableMonthChart
        {
            get
            {
                Object O = SessionManager.RetrieveSession(Constants._BookingChart_TableMonthChart);
                if (O == null)
                    return null;
                else
                    return O;
            }
            set { SessionManager.SaveSession(Constants._BookingChart_TableMonthChart, value); }
        }
        public static Object BookingChart_TableBookingTable
        {
            get
            {
                Object O = SessionManager.RetrieveSession(Constants._BookingChart_TableBookingTable);
                if (O == null)
                    return null;
                else
                    return O;
            }
            set { SessionManager.SaveSession(Constants._BookingChart_TableBookingTable, value); }
        }
        public static int BookingChart_RegionId
        {
            get
            {
                Object O = SessionManager.RetrieveSession(Constants._BookingChart_RegionId);
                if (O == null)
                    return -1;
                else
                    return Convert.ToInt32(O);
            }
            set { SessionManager.SaveSession(Constants._BookingChart_RegionId, value); }
        }
        public static int BookingChart_AccomodTypeId
        {
            get
            {
                Object O = SessionManager.RetrieveSession(Constants._BookingChart_AccomodTypeId);
                if (O == null)
                    return -1;
                else
                    return Convert.ToInt32(O);
            }
            set { SessionManager.SaveSession(Constants._BookingChart_AccomodTypeId, value); }
        }
        public static int BookingChart_AccomId
        {
            get
            {
                Object O = SessionManager.RetrieveSession(Constants._BookingChart_AccomId);
                if (O == null)
                    return -1;
                else
                    return Convert.ToInt32(O);
            }
            set { SessionManager.SaveSession(Constants._BookingChart_AccomId, value); }
        }
        #endregion
        #endregion BookingChart

        #region Booking
        #region Booking Session Keys
        public static string Booking_BookingId_Key
        {
            get { return Constants._Booking_BookingId; }
        }
        public static string Booking_AllRoomsData_Key
        {
            get { return Constants._Booking_AllRoomsData; }
        }
        public static string Booking_AllRoomsDataPAX_Key
        {
            get { return Constants._Booking_AllRoomsDataPAX; }
        }
        public static string Booking_AccomodationData_Key
        {
            get { return Constants._Booking_AccomodationData; }
        }
        public static string Booking_TotalNights_Key
        {
            get { return Constants._Booking_TotalNights; }
        }
        public static string Booking_DdlSelectedIndexes_Key
        {
            get { return Constants._Booking_DdlSelectedIndexes; }
        }
        public static string Booking_RoomBookingDateWiseData_Key
        {
            get { return Constants._Booking_RoomBookingDateWiseData; }
        }
        public static string Booking_RoomsStatus_Key
        {
            get { return Constants._Booking_RoomsStatus; }
        }

        #endregion

        #region Booking Session Properties
        public static int Booking_BookingId
        {
            get
            {
                Object O = SessionManager.RetrieveSession(Constants._Booking_BookingId);
                if (O == null)
                    return -1;
                else
                    return Convert.ToInt32(O);
            }
            set { SessionManager.SaveSession(Constants._Booking_BookingId, value); }
        }
        public static BookedRooms[] Booking_AllRoomsData
        {
            get
            {
                Object O = SessionManager.RetrieveSession(Constants._Booking_AllRoomsData);
                if (O == null)
                    return null;
                else
                    return (BookedRooms[])O;

            }
            set { SessionManager.SaveSession(Constants._Booking_AllRoomsData, value); }
        }
        public static BookedRooms[] Booking_AllRoomsDataPAX
        {
            get
            {
                Object O = SessionManager.RetrieveSession(Constants._Booking_AllRoomsDataPAX);
                if (O == null)
                    return null;
                else
                    return (BookedRooms[])O;

            }
            set { SessionManager.SaveSession(Constants._Booking_AllRoomsDataPAX, value); }
        }
        public static AccomTypeDTO[] Booking_AccomodationData
        {
            get
            {
                Object O = SessionManager.RetrieveSession(Constants._Booking_AccomodationData);
                if (O == null)
                    return null;
                else
                    return (AccomTypeDTO[])O;
            }
            set { SessionManager.SaveSession(Constants._Booking_AccomodationData, value); }
        }
        public static RoomBookingDateWiseDTO[] Booking_RoomBookingDateWiseDTO
        {
            get
            {
                Object O = SessionManager.RetrieveSession(Constants._Booking_RoomBookingDateWiseData);
                if (O == null)
                    return null;
                else
                    return (RoomBookingDateWiseDTO[])O;
            }
            set { SessionManager.SaveSession(Constants._Booking_RoomBookingDateWiseData, value); }
        }
        public static int Booking_TotalNights
        {
            get
            {
                Object O = SessionManager.RetrieveSession(Constants._Booking_TotalNights);
                if (O == null)
                    return -1;
                else
                    return Convert.ToInt32(O);
            }
            set { SessionManager.SaveSession(Constants._Booking_TotalNights, value); }
        }
        public static Object Booking_DdlSelectedIndexes
        {
            get { return SessionManager.RetrieveSession(Constants._Booking_DdlSelectedIndexes); }
            set { SessionManager.SaveSession(Constants._Booking_DdlSelectedIndexes, value); }
        }
        public static Object Booking_RoomsStatus
        {
            get { return SessionManager.RetrieveSession(Constants._Booking_RoomsStatus); }
            set { SessionManager.SaveSession(Constants._Booking_RoomsStatus, value); }
        }
        #endregion
        #endregion

        #region Booking ChangeRoomPax
        #region Booking Session Keys        
        public static string BookingChangeRoomPax_DdlSelectedIndexes_Key
        {
            get { return Constants._BookingChangeRoomPax_DdlSelectedIndexes; }
        }
        #endregion

        public static SortedList BookingChangeRoomPax_DdlSelectedIndexes
        {

            get
            {
                object O = SessionManager.RetrieveSession(Constants._BookingChangeRoomPax_DdlSelectedIndexes);
                if (O == null)
                    return null;
                else
                    return (SortedList)O;

            }
        }
        #endregion

        #region Booking Confirmation
        #region Booking Cofirmation Session Keys
        public static string BookingConfirmation_TableSelectedMealPlan_Key
        {
            get { return Constants._BookingConfirmation_TableSelectedMealPlan; }
        }
        public static string BookingConfirmation_TableRoomsBooked_Key
        {
            get { return Constants._BookingConfirmation_TableRoomsBooked; }
        }
        public static string BookingConfirmation_TotalRoomsBooked_Key
        {
            get { return Constants._BookingConfirmation_TotalRoomsBooked; }
        }
        public static string BookingConfirmation_BookingMealPlanData_Key
        {
            get { return Constants._BookingConfirmation_BookingMealPlanData; }
        }
        public static string BookingConfirmation_BookingActivityData_Key
        {
            get { return Constants._BookingConfirmation_BookingActivityDTO; }
        }
        public static string BookingConfirmation_AccomodationActivityData_Key
        {
            get { return Constants._BookingConfirmation_AccomodationActivityData; }
        }
        public static string BookingConfirmation_MealPlanData_Key
        {
            get { return Constants._BookingConfirmation_MealPlanData; }
        }
        #endregion

        #region Booking Confirmation Session Properties
        public static BookedRoomsTotal[] BookingConfirmation_TotalRoomsBooked
        {
            get
            {
                Object O = SessionManager.RetrieveSession(Constants._BookingConfirmation_TotalRoomsBooked);
                if (O == null)
                    return null;
                else
                    return (BookedRoomsTotal[])O;
            }
            set { SessionManager.SaveSession(Constants._BookingConfirmation_TotalRoomsBooked, value); }
        }
        public static Object BookingConfirmation_TableSelectedMealPlan
        {
            get { return SessionManager.RetrieveSession(Constants._BookingConfirmation_TableSelectedMealPlan); }
            set { SessionManager.SaveSession(Constants._BookingConfirmation_TableSelectedMealPlan, value); }
        }
        public static Object BookingConfirmation_TableRoomsBooked
        {
            get { return SessionManager.RetrieveSession(Constants._BookingConfirmation_TableRoomsBooked); }
            set { SessionManager.SaveSession(Constants._BookingConfirmation_TableRoomsBooked, value); }
        }
        public static BookingMealPlanDTO[] BookingConfirmation_BookingMealPlanDTO
        {
            get
            {
                Object O = SessionManager.RetrieveSession(Constants._BookingConfirmation_BookingMealPlanData);
                if (O == null)
                    return null;
                else
                    return (BookingMealPlanDTO[])O;
            }
            set { SessionManager.SaveSession(Constants._BookingConfirmation_BookingMealPlanData, value); }
        }
        public static BookingActivityDTO[] BookingConfirmation_BookingActivityData
        {
            get
            {
                Object O = SessionManager.RetrieveSession(Constants._BookingConfirmation_BookingActivityDTO);
                if (O == null)
                    return null;
                else
                    return (BookingActivityDTO[])O;
            }
            set { SessionManager.SaveSession(Constants._BookingConfirmation_BookingActivityDTO, value); }
        }
        public static AccomActivityDTO[] BookingConfirmation_AccomodationActivityData
        {
            get
            {
                Object O = SessionManager.RetrieveSession(Constants._BookingConfirmation_AccomodationActivityData);
                if (O == null)
                    return null;
                else
                    return (AccomActivityDTO[])O;
            }
            set { SessionManager.SaveSession(Constants._BookingConfirmation_AccomodationActivityData, value); }
        }
        public static MealPlanDTO[] BookingConfirmation_MealPlanData
        {
            get
            {
                Object O = SessionManager.RetrieveSession(Constants._BookingConfirmation_MealPlanData);
                if (O == null)
                    return null;
                else
                    return (MealPlanDTO[])O;
            }
            set { SessionManager.SaveSession(Constants._BookingConfirmation_MealPlanData, value); }
        }
        #endregion
        #endregion

        #region View Booking
        #region View Booking Session Keys
        public static string ViewBooking_BookingData_Key
        {
            get { return Constants._ViewBooking_BookingData; }
        }
        public static string ViewBooking_SelectedCheckInDate_Key
        {
            get { return Constants._ViewBooking_SelectedCheckInDate; }
        }
        public static string ViewBooking_SelectedCheckOutDate_Key
        {
            get { return Constants._ViewBooking_SelectedCheckOutDate; }
        }
        public static string ViewBooking_SelectedBookingStatus_Key
        {
            get { return Constants._ViewBooking_SelectedBookingStatus; }
        }
        public static string ViewBooking_SelectedAccomodationType_Key
        {
            get { return Constants._ViewBooking_SelectedAccomodationType; }
        }
        #endregion

        #region View Booking Session Properties
        public static List<ViewBookingDTO> ViewBooking_BookingData
        {
            get
            {
                Object O = SessionManager.RetrieveSession(Constants._ViewBooking_BookingData);
                if (O == null)
                    return null;
                else
                    return (List<ViewBookingDTO>)O;
            }
            set { SessionManager.SaveSession(Constants._ViewBooking_BookingData, value); }
        }
        public static string ViewBooking_SelectedCheckInDate
        {
            get { return Convert.ToString(SessionManager.RetrieveSession(Constants._ViewBooking_SelectedCheckInDate)); }
            set { SessionManager.SaveSession(Constants._ViewBooking_SelectedCheckInDate, value); }
        }
        public static string ViewBooking_SelectedCheckOutDate
        {
            get { return Convert.ToString(SessionManager.RetrieveSession(Constants._ViewBooking_SelectedCheckOutDate)); }
            set { SessionManager.SaveSession(Constants._ViewBooking_SelectedCheckOutDate, value); }
        }
        public static string ViewBooking_SelectedBookingStatus
        {
            get { return Convert.ToString(SessionManager.RetrieveSession(Constants._ViewBooking_SelectedBookingStatus)); }
            set { SessionManager.SaveSession(Constants._ViewBooking_SelectedBookingStatus, value); }
        }
        public static string ViewBooking_SelectedAccomodationType
        {
            get { return Convert.ToString(SessionManager.RetrieveSession(Constants._ViewBooking_SelectedAccomodationType)); }
            set { SessionManager.SaveSession(Constants._ViewBooking_SelectedAccomodationType, value); }
        }
        #endregion
        #endregion

        #region View Series
        #region View Series Session Keys
        public static string ViewSeries_Data_Key
        {
            get { return Constants._ViewSeries_Data; }
        }
        public static string ViewSeries_StartSeriesDate_Key
        {
            get { return Constants._ViewSeries_StartSeriesDate; }
        }
        public static string ViewSeries_SelectedAccomodation_Key
        {
            get { return Constants._ViewSeries_SelectedAccomodation; }
        }
        #endregion

        #region View Series Session Properties
        public static List<SeriesDTO> ViewSeries_Data
        {
            get
            {
                Object O = SessionManager.RetrieveSession(Constants._ViewSeries_Data);
                if (O == null)
                    return null;
                else
                    return (List<SeriesDTO>)O;

            }
            set { SessionManager.SaveSession(Constants._ViewSeries_Data, value); }
        }
        public static DateTime ViewSeries_StartSeriesDate
        {
            get { return Convert.ToDateTime(SessionManager.RetrieveSession(Constants._ViewSeries_StartSeriesDate)); }
            set { SessionManager.SaveSession(Constants._ViewSeries_StartSeriesDate, value); }
        }
        public static string ViewSeries_SelectedAccomodation
        {
            get { return Convert.ToString(SessionManager.RetrieveSession(Constants._ViewSeries_SelectedAccomodation)); }
            set { SessionManager.SaveSession(Constants._ViewSeries_SelectedAccomodation, value); }
        }
        #endregion
        #endregion

        #region Tourist Details       
        #region Tourist Details Session Keys
        public static string TouristDetails_TouristNo_Key
        {
            get { return Constants._TouristDetails_TouristNo; }
        }
        public static string TouristDetails_BookingNo_Key
        {
            get { return Constants._TouristDetails_BookingNo; }
        }
        #endregion

        #region Tourist Details Session Properties
        public static int TouristDetails_TouristNo
        {
            get
            {
                Object O = SessionManager.RetrieveSession(Constants._TouristDetails_TouristNo);
                if (O == null)
                    return -1;
                else
                    return Convert.ToInt32(O);
            }
            set { SessionManager.SaveSession(Constants._TouristDetails_TouristNo, value); }
        }
        public static int TouristDetails_BookingNo
        {
            get
            {
                Object O = SessionManager.RetrieveSession(Constants._TouristDetails_BookingNo);
                if (O == null)
                    return -1;
                else
                    return Convert.ToInt32(O);
            }
            set { SessionManager.SaveSession(Constants._TouristDetails_BookingNo, value); }
        }
        #endregion
        #endregion

        #region Tourist Count Booking        
        #region View Booking Session Keys
        public static string TouristCount_BookingData_Key
        {
            get { return Constants._TouristCount_BookingData; }
        }
        public static string TouristCount_SelectedCheckInDate_Key
        {
            get { return Constants._TouristCount_SelectedCheckInDate; }
        }
        public static string TouristCount_SelectedCheckOutDate_Key
        {
            get { return Constants._TouristCount_SelectedCheckOutDate; }
        }
        public static string TouristCount_SelectedBookingStatus_Key
        {
            get { return Constants._TouristCount_SelectedBookingStatus; }
        }
        public static string TouristCount_SelectedAccomodationType_Key
        {
            get { return Constants._TouristCount_SelectedAccomodationType; }
        }
        #endregion

        #region View Booking Session Properties
        public static List<clsTouristCountDTO> TouristCount_BookingData
        {
            get
            {
                Object O = SessionManager.RetrieveSession(Constants._TouristCount_BookingData);
                if (O == null)
                    return null;
                else
                    return (List<clsTouristCountDTO>)O;
            }
            set { SessionManager.SaveSession(Constants._TouristCount_BookingData, value); }
        }
        public static string TouristCount_SelectedCheckInDate
        {
            get { return Convert.ToString(SessionManager.RetrieveSession(Constants._TouristCount_SelectedCheckInDate)); }
            set { SessionManager.SaveSession(Constants._TouristCount_SelectedCheckInDate, value); }
        }
        public static string TouristCount_SelectedCheckOutDate
        {
            get { return Convert.ToString(SessionManager.RetrieveSession(Constants._TouristCount_SelectedCheckOutDate)); }
            set { SessionManager.SaveSession(Constants._TouristCount_SelectedCheckOutDate, value); }
        }
        public static string TouristCount_SelectedBookingStatus
        {
            get { return Convert.ToString(SessionManager.RetrieveSession(Constants._TouristCount_SelectedBookingStatus)); }
            set { SessionManager.SaveSession(Constants._TouristCount_SelectedBookingStatus, value); }
        }
        public static string TouristCount_SelectedAccomodationType
        {
            get { return Convert.ToString(SessionManager.RetrieveSession(Constants._TouristCount_SelectedAccomodationType)); }
            set { SessionManager.SaveSession(Constants._TouristCount_SelectedAccomodationType, value); }
        }
        #endregion
        #endregion

        #region Series Booking
        #region Series Booking Session Key
        public static string SeriesBooking_TableTotalRoomCount_Key
        {
            get { return Constants._SeriesBooking_TableTotalRoomCount; }
        }
        public static string SeriesBooking_TableSeries_Key
        {
            get { return Constants._SeriesBooking_TableSeries; }
        }
        public static string Series_AccomodationData_Key
        {
            get { return Constants._Series_AccomodationData; }
        }
        public static string Series_BookedRooms_WithDates_Key
        {
            get { return Constants._Series_BookedRooms_WithDates; }
        }
        #endregion

        #region Series Booking Session Properties
        public static Object SeriesBooking_TableTotalRoomCount
        {
            get { return SessionManager.RetrieveSession(Constants._SeriesBooking_TableTotalRoomCount); }
            set { SessionManager.SaveSession(Constants._SeriesBooking_TableTotalRoomCount, value); }
        }
        public static Object SeriesBooking_TableSeries
        {
            get { return SessionManager.RetrieveSession(Constants._SeriesBooking_TableSeries); }
            set { SessionManager.SaveSession(Constants._SeriesBooking_TableSeries, value); }
        }
        public static AccomTypeDTO[] Series_AccomodationData
        {
            get
            {
                Object O = SessionManager.RetrieveSession(Constants._Series_AccomodationData);
                if (O == null)
                    return null;
                else
                    return (AccomTypeDTO[])O;
            }
            set { SessionManager.SaveSession(Constants._Series_AccomodationData, value); }
        }
        public static List<BookingDatesWithBookedRoomsDTO> Series_BookedRooms_WithDates
        {
            get
            {
                Object O = SessionManager.RetrieveSession(Constants._Series_BookedRooms_WithDates);
                if (O == null)
                    return null;
                else
                    return (List<BookingDatesWithBookedRoomsDTO>)O;
            }
            set { SessionManager.SaveSession(Constants._Series_BookedRooms_WithDates, value); }
        }
        #endregion
        #endregion

        #region Wait List Management        
        #region Wait List Management Session Keys
        public static string WaitListManagement_WaitListedBookings_Key
        {
            get { return Constants._WaitListManagement_WaitListedBookings; }
        }
        #endregion
        #region  Wait List Management Session Properties
        public static RoomBookingDateWiseDTO[] WaitListManagement_WaitListedBookings
        {
            get
            {
                Object O = SessionManager.RetrieveSession(Constants._WaitListManagement_WaitListedBookings);
                if (O == null)
                    return null;
                else
                    return (RoomBookingDateWiseDTO[])O;
            }
            set { SessionManager.SaveSession(Constants._WaitListManagement_WaitListedBookings, value); }
        }
        #endregion
        #endregion

        #region User Info
        public static string LoggedInUser_Key
        {
            get { return Constants._LoggedInUser; }
        }

        public static LoggedInUser LoggedInUser
        {
            get
            {
                Object O = SessionManager.RetrieveSession(Constants._LoggedInUser);
                if (O == null)
                    return null;
                else
                    return (LoggedInUser)O;
            }
            set { SessionManager.SaveSession(Constants._LoggedInUser, value); }
        }
        #endregion        

        #endregion Client Sessions

        #region Master Sessions
        #region Master Sessions Keys
        public static string RoomMaster_OperationMode_Key
        {
            get { return Constants._RoomMaster_OperationMode; }
        }
        #endregion Master Sessions Keys

        #region Master Sessions Properties
        public static string RoomMaster_OperationMode
        {
            get { return Convert.ToString(SessionManager.RetrieveSession(Constants._RoomMaster_OperationMode)); }
            set { SessionManager.SaveSession(Constants._RoomMaster_OperationMode, value); }
        }
        #endregion Master Sessions Properties
        #endregion Master Sessions

        public static Boolean SaveSession(String key, Object value)
        {
            return SessionManager.SaveSession(key, value);
        }
        public static Object RetrieveSession(String key)
        {
            return SessionManager.RetrieveSession(key);
        }
        public static Boolean DeleteSession(String key)
        {
            return SessionManager.DeleteSession(key);
        }
        public static Boolean AbandonSession()
        {
            return SessionManager.AbandonSession();
        }
    }
}