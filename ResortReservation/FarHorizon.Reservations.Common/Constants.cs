using System;
using System.Collections.Generic;
using System.Text;

namespace FarHorizon.Reservations.Common
{
    public static class Constants
    {
        #region Control Ids Constants
        public const string BUTTON = "btn";
        public const string CELL = "cell";
        public const string CHECKBOX = "chk";
        public const string DROPDOWN = "ddl";
        public const string HIDDENFIELD = "hf";
        public const string IMAGE = "img";
        public const string PANEL = "pnl";
        public const string TEXTBOX = "txt";
        public const string DIV = "div";

        public const string SEPERATOR = "sep";

        #region Booking Screen

        public const string LABEL_PRICE = "lblCatWisePrice*";
        public const string LABEL_ROOMS_BOOKED = "lblRoomsBooked*";
        public const string LABEL_TOTAL_ROOMS_BOOKED = "lblTotalRoomsBooked*";
        public const string LABEL_PAX = "lblPax*";
        public const string DROPDOWNLIST_ROOMS = "ddl*";
        public const string DROPDOWNLIST_PAX = "ddlpax*";
        public const string DROPDOWNLIST_ROOMCONVERT = "ddlroomconvert*";
        public const string LABEL_ROOMS_AVAILABLE = "lblRoomsAvailable*";
        public const string LABEL_ROOMS_WAITLISTED = "lblRoomsWaitlisted*";
        public const string CHECKBOX_ROOM_NO = "chkRoomNo*";
        public const string PANEL_ROOMS = "pnlRooms";
        public const string DIV_ROOMNO = "divRoomNo";
        #endregion Booking Screen

        public const string CELL_ROOMNO = "cellRoomNo";


        #region Booking Confirmation Screen
        public const string CHECKBOX_WELCOMEDRINK = "chk*W*";
        public const string CHECKBOX_BREAKFAST = "chk*B*";
        public const string CHECKBOX_LUNCH = "chk*L*";
        public const string CHECKBOX_EVENINGSNACKS = "chk*E*";
        public const string CHECKBOX_DINNER = "chk*D*";

        public const string CHECKBOX_ACTIVITY = "chk*A*";

        public const string PANEL_MEAL_ACTIVITY = "pnlmealactivity";

        public const string ROOM_REQUIRED_CELL = "R*"; //Rooms Required
        public const string ROOM_AVAILABLE_CELL = "A*"; //Rooms Available
        public const string ROOM_WAITLISTED_CELL = "W*"; //Rooms Waitlisted
        public const string ROOM_BOOKED_CELL = "B*"; //Rooms Booked
        #endregion Booking Confirmation Screen

        #region Booking Chart
        public const string CHART_ROOM_ROW = "roomRow";
        public const string CHART_ROOM_CELL = "roomCell";
        public const string CHART_ROOM_CELL_DIV_MAIN = "roomCellDivMain";
        public const string CHART_ROOM_CELL_DIV_HEADER = "roomCellDivHeader";
        public const string CHART_ROOM_CELL_DIV_DETAIL = "roomCellDivDetail";

        #endregion Booking Chart

        #region waitlistManagement
        public const string PANEL_HEADER = "pnlHeader";
        public const string PANEL_DETAIL = "pnlDetail";
        #endregion waitlistManagement

        #region SeriesBooking
        public const string CHECKBOX_CAT_TYPE = "chkCatType";
        public const string CHECKBOX_BOOKING = "chkBooking";
        public const string CHECKBOX_PROPOSED_BOOKING = "chkPropsedBooking";
        public const string DROPDOWN_CAT_TYPE = "ddlCatType";

        public const string TEXTBOX_CHECKINDATE = "txtCheckIndate";
        public const string TEXTBOX_CHECKOUTDATE = "txtCheckOutdate";
        public const string BUTTON_CHECKINDATE = "btnCheckIndate";
        public const string BUTTON_CHECKOUTDATE = "btnCheckOutdate";
        #endregion SeriesBooking
        #endregion

        #region Room Status Constants
        public const char AVAILABLE = 'A';
        public const char BOOKED = 'B';
        public const char BOOKED_NOW = 'N';
        public const char BOOKED_EARLIER = 'E';
        public const char CONFIRMED = 'C';
        public const char WAITLISTED = 'W';
        public const char WAITLISTED_WITH_OTHER_BOOKING = 'O';
        public const char BOOKED_WITH_OTHER_BOOKING = 'Y';
        public const char CANCELLED = 'X';
        public const char Maintainence = 'M';
        #endregion

        #region MealPlan
        public const string MEALPLAN = "mp*";

        #endregion

        #region Session Constants
        public static string _LoginId = "LoginId";
        #region Client Sessions
        #region BookingChart
        #region Booking Chart Session Variables
        public static string _BookingChart_TreeDTO = "BC_TD";
        public static string _BookingChart_TreeType = "BC_TT";
        public static string _BookingChart_TreeArrangeBy = "BC_TAB";
        public static string _BookingChart_TableMonthChart = "BC_MC";

        public static string _BookingChart_TableBookingTable = "BC_BT";
        public static string _BookingChart_RegionId = "BC_RID";
        public static string _BookingChart_AccomodTypeId = "BC_ATID";
        public static string _BookingChart_AccomId = "BC_AID";
        #endregion
        #endregion BookingChart

        #region Booking
        #region Booking Session Variables
        public static string _Booking_BookingId = "B_ID";
        public static string _Booking_AllRoomsData = "B_ARD";
        public static string _Booking_AllRoomsDataPAX = "B_ARDP";
        public static string _Booking_AccomodationData = "B_AD";
        public static string _Booking_TotalNights = "B_TN";
        public static string _Booking_DdlSelectedIndexes = "B_DSI";
        public static string _Booking_RoomBookingDateWiseData = "B_RBDWD";
        public static string _Booking_RoomsStatus = "B_RS";
        #endregion
        #endregion

        #region Booking ChangeRoomPax
        #region Booking Session Variables
        public static string _BookingChangeRoomPax_DdlSelectedIndexes = "BCRP_DSI";
        #endregion
        #endregion

        #region Booking Confirmation
        #region Booking Confirmation Session Variables
        public static string _BookingConfirmation_TableSelectedMealPlan = "BCONF_TSMP";
        public static string _BookingConfirmation_TableRoomsBooked = "BCONF_TRB";
        public static string _BookingConfirmation_TotalRoomsBooked = "BCONF_TRBOOKED";
        public static string _BookingConfirmation_BookingMealPlanData = "BCONF_BMPD";
        public static string _BookingConfirmation_BookingActivityDTO = "BCONF_BAD";
        public static string _BookingConfirmation_AccomodationActivityData = "BCONF_AAD";
        public static string _BookingConfirmation_MealPlanData = "BCONF_MPD";
        #endregion
        #endregion

        #region View Booking
        #region View Booking Session Variables
        public static string _ViewBooking_BookingData = "VB_BD";
        public static string _ViewBooking_SelectedCheckInDate = "VB_SCID";
        public static string _ViewBooking_SelectedCheckOutDate = "VB_SCOD";
        public static string _ViewBooking_SelectedBookingStatus = "VB_SBS";
        public static string _ViewBooking_SelectedAccomodationType = "VB_SAT";
        #endregion
        #endregion

        #region View Series
        #region View Series Session Variables
        public static string _ViewSeries_Data = "VS_D";
        public static string _ViewSeries_StartSeriesDate = "VB_SSD";
        public static string _ViewSeries_SelectedAccomodation = "VB_SA";
        #endregion
        #endregion

        #region Tourist Details
        #region Tourist Details Session Variables
        public static string _TouristDetails_TouristNo = "TD_TN";
        public static string _TouristDetails_BookingNo = "TD_BN";
        #endregion
        #endregion

        #region Tourist Count Booking
        #region Tourist Count Session Variables
        public static string _TouristCount_BookingData = "TC_BD";
        public static string _TouristCount_SelectedCheckInDate = "TC_SCID";
        public static string _TouristCount_SelectedCheckOutDate = "TC_SCOD";
        public static string _TouristCount_SelectedBookingStatus = "TC_SBS";
        public static string _TouristCount_SelectedAccomodationType = "TC_SAT";
        #endregion
        #endregion

        #region Series Booking
        #region Series Booking Session Variables
        public static string _SeriesBooking_TableTotalRoomCount = "SB_TTRC";
        public static string _SeriesBooking_TableSeries = "SB_TS";
        public static string _Series_AccomodationData = "S_AD";
        public static string _Series_BookedRooms_WithDates = "S_BRWD";

        #endregion
        #endregion

        #region Wait List Management
        #region Wait List Management Session Variables
        public static string _WaitListManagement_WaitListedBookings = "WLM_WLB";
        #endregion
        #endregion

        #region User Info
        public static string _LoggedInUser = "L_I_U";
        #endregion

        #endregion Client Sessions

        #region Master Sessions
        #region Master Sessions Variables
        public static string _RoomMaster_OperationMode = "RM_OM";
        #endregion
        #endregion Master Sessions

        #endregion
    }    
}
