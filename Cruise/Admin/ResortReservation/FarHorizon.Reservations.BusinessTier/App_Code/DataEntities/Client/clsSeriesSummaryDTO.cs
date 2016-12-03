using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessTier.App_Code.DataEntities.Client
{
    public class clsSeriesSummaryDTO
    {
        private string _SeriesName;
        private int _SeriesId;
        private DateTime _CheckIn;
        private DateTime _CheckOut;
        private string _RoomCategory;
        private string _RoomType;
        private int _RoomCategoryId;
        private int _RoomTypeId;
        private int _NoOfRoomsRequested;
        private int _NoOfRoomsAvailable;      
        
        public string SeriesName
        {
            get { return _SeriesName; }
            set { _SeriesName = value; }
        }       
        public int SeriesId
        {
            get { return _SeriesId; }
            set { _SeriesId = value; }
        }       
        public DateTime CheckIn
        {
            get { return _CheckIn; }
            set { _CheckIn = value; }
        }       
        public DateTime CheckOut
        {
            get { return _CheckOut; }
            set { _CheckOut = value; }
        }
        public string RoomCategory
        {
            get { return _RoomCategory; }
            set { _RoomCategory = value; }
        }
        public string RoomType
        {
            get { return _RoomType; }
            set { _RoomType = value; }
        }        
        public int NoOfRoomsRequested
        {
            get { return _NoOfRoomsRequested; }
            set { _NoOfRoomsRequested = value; }
        }
        public int NoOfRoomsAvailable
        {
            get { return _NoOfRoomsAvailable; }
            set { _NoOfRoomsAvailable = value; }
        }
        private int _NoOfRoomsWaitlisted;
        public int NoOfRoomsWaitlisted
        {
            get { return _NoOfRoomsWaitlisted; }
            set { _NoOfRoomsWaitlisted = value; }
        }
        public int RoomCategoryId
        {
            get { return _RoomCategoryId; }
            set { _RoomCategoryId = value; }
        }

        public int RoomTypeId
        {
            get { return _RoomTypeId; }
            set { _RoomTypeId = value; }
        }
    }
}
