using System;
using System.Collections.Generic;
using System.Text;

namespace FarHorizon.Reservations.Common.DataEntities.Client
{
    public class BookingChartDTO
    {
        int _iAccomTypeId;
        string _sAccomType;
        int _iAccomId;
        string _sAccomName;
        string _sAccomInitial;
        int _iRegionId;
        string _sRegionName;
        string _sRoomNo;
        string _roomCategoryAlias;
        private DateTime _fromdt;
        private DateTime _todt;
        private string _reason;

        private string _sStartDate;
        private string _sEndDate;

        public string Reason
        {
            get { return _reason; }
            set { _reason = value; }
        }


        public DateTime FromDt
        {
            get { return _fromdt; }
            set
            {
                _fromdt = value;
                _sStartDate = String.Format("{0:dd-MMM-yyyy}", _fromdt);
            }
        }

        public DateTime Todt
        {
            get { return _todt; }
            set
            {
                _todt = value;
                _sEndDate = String.Format("{0:dd-MMM-yyyy}", _todt);
            }
        }

        public string StartdtFormatted
        {
            get { return _sStartDate; }
        }

        public string EndDtFormatted
        {
            get { return _sEndDate; }
        }



        public int AccomodationTypeId
        {
            get { return _iAccomTypeId; }
            set { _iAccomTypeId = value; }
        }
        public string AccomodationType
        {
            get { return _sAccomType; }
            set { _sAccomType = value; }
        }
        public int AccomodationId
        {
            get { return _iAccomId; }
            set { _iAccomId = value; }
        }
        public string AccomodationName
        {
            get { return _sAccomName; }
            set { _sAccomName = value; }
        }
        public string AccomInitial
        {
            get { return _sAccomInitial; }
            set { _sAccomInitial = value; }
        }
        public int RegionId
        {
            get { return _iRegionId; }
            set { _iRegionId = value; }
        }
        public string RegionName
        {
            get { return _sRegionName; }
            set { _sRegionName = value; }
        }
        public string RoomNo
        {
            get { return _sRoomNo; }
            set { _sRoomNo = value; }
        }

        public string RoomCategoryAlias
        {
            get { return _roomCategoryAlias; }
            set { _roomCategoryAlias = value; }
        }
    }
}
