using System;
using System.Collections.Generic;
using System.Text;

namespace FarHorizon.Reservations.Common.DataEntities.Client
{
    public class SeriesDTO
    {
        private int _iBookingId;
        private DateTime _dtStartDate;
        private DateTime _dtEndDate;

        private string _sRoomCategory;
        private string _sRoomType;
        private int _iTotalRooms;
        private int _iRoomCategoryId;
        private int _iRoomTypeId;
        private string _sSeriesName;
        
        private int _iNoOfNights;
        private int _iGAP;
        private int _iSeriesId;
        private int _iAccomodationID;
        private int _iAccomTypeID;
        private DateTime dtSeriesStartDate;
        private int _iNoOfDeps;
        private string _sAccomodation;
        private int _AgentId;

        public int AgentId
        {
            get { return _AgentId; }
            set { _AgentId = value; }
        }

              

        public int SeriesId
        {
            get { return _iSeriesId; }
            set { _iSeriesId = value; }
        }
        public int BookingId
        {
            get { return _iBookingId; }
            set { _iBookingId = value; }
        }
        public string SeriesName
        {
            get { return _sSeriesName; }
            set { _sSeriesName = value; }
        }
        public int NoOfNights
        {
            get { return _iNoOfNights; }
            set { _iNoOfNights = value; }
        }
        public int GAP
        {
            get { return _iGAP; }
            set { _iGAP = value; }
        }
        public int NoOfDepartures
        {
            get { return _iNoOfDeps; }
            set { _iNoOfDeps = value; }
        }
        /*
         * 
         * SERIES STARTDATE REFERS TO THE STARTDATE OF A SERIES AND HAS NOTHING TO DO WITH THE 
         * STARTDATE OF INDIVIDUAL BOOKINGS IN A SERIES AS THISE DATES HAVE BEEN 
         * HANDLED THROUGH TWO DIFFERENT PROPERTIES BELOW
         * 
         * */
        public DateTime SeriesStartDate
        {
            get { return dtSeriesStartDate; }
            set { dtSeriesStartDate = value; }
        }
        /*
         * 
         *THE STARTDATE AND ENDDATE PROPERTIES DEFINED BELOW REFLECT THE STARTDATE AND ENDDATE OF 
         *INDIVIDUAL BOOKINGS IN A SERIES 
         * 
         */
        public DateTime StartDate
        {
            get { return _dtStartDate; }
            set { _dtStartDate = value; }
        }
        public DateTime EndDate
        {
            get { return _dtEndDate; }
            set { _dtEndDate = value; }
        }
        public string RoomCategory
        {
            get { return _sRoomCategory; }
            set { _sRoomCategory = value; }
        }
        public string RoomType
        {
            get { return _sRoomType; }
            set { _sRoomType = value; }
        }
        public int TotalRooms
        {
            get { return _iTotalRooms; }
            set { _iTotalRooms = value; }
        }
        public int RoomCategoryId
        {
            get { return _iRoomCategoryId; }
            set { _iRoomCategoryId = value; }
        }
        public int RoomTypeId
        {
            get { return _iRoomTypeId; }
            set { _iRoomTypeId = value; }
        }
        public int AccomTypeID
        {
            get { return _iAccomTypeID; }
            set { _iAccomTypeID = value; }
        }
        public int AccomodationID
        {
            get { return _iAccomodationID; }
            set { _iAccomodationID = value; }
        }
        public string Accomodation
        {
            get { return _sAccomodation; }
            set { _sAccomodation = value; }
        }
    }
    public class SeriesBookingDates
    {
        private DateTime _dtStartDate;

        public DateTime StartDate
        {
            get { return _dtStartDate; }
            set { _dtStartDate = value; }
        }
        private DateTime _dtEndDate;

        public DateTime EndDate
        {
            get { return _dtEndDate; }
            set { _dtEndDate = value; }
        }
        private int _iAccomId;

        public int AccomodationId
        {
            get { return _iAccomId; }
            set { _iAccomId = value; }
        }        
    }

    public class clsSeriesBookingDTO
    {
        #region Data Members

        private int _iBookingID;
        private DateTime dtStartDate;
        private DateTime dtEndDate;
        private int iNoOfRooms;
        private int iRoomCategoryID;
        private int iRoomTypeID;
        private int iAccomodationID;
        private int _iAccomTypeID;
        private string sSeriesName;
        private int _iNoOfNights;
        private int _iGap;
        private int _iNoOfDepartures;

        #endregion Data Members

        #region Member Properties
        public int BookingID
        {
            get { return _iBookingID; }
            set { _iBookingID = value; }
        }
        public int AccomTypeID
        {
            get { return _iAccomTypeID; }
            set { _iAccomTypeID = value; }
        }
        public int NoOfDepartures
        {
            get { return _iNoOfDepartures; }
            set { _iNoOfDepartures = value; }
        }
        public int Gap
        {
            get { return _iGap; }
            set { _iGap = value; }
        }
        public int NoOfNights
        {
            get { return _iNoOfNights; }
            set { _iNoOfNights = value; }
        }
        public DateTime StartDate
        {
            get { return dtStartDate; }
            set { dtStartDate = value; }
        }
        public DateTime EndDate
        {
            get { return dtEndDate; }
            set { dtEndDate = value; }
        }
        public int RoomCategoryID
        {
            get { return iRoomCategoryID; }
            set { iRoomCategoryID = value; }
        }
        public int RoomTypeID
        {
            get { return iRoomTypeID; }
            set { iRoomTypeID = value; }
        }
        public int NoOfRooms
        {
            get { return iNoOfRooms; }
            set { iNoOfRooms = value; }
        }
        public int AccomodationID
        {
            get { return iAccomodationID; }
            set { iAccomodationID = value; }
        }
        public string SeriesName
        {
            get { return sSeriesName; }
            set { sSeriesName = value; }
        }

        #endregion  Member Properties
    }
}
