using System;
using System.Collections.Generic;

namespace FarHorizon.Reservations.Common.DataEntities.Masters
{
    public class AccomodationDTO 
    {
        private int _AccomId;
        private string _sAccom;
        private int _iAccomTypeId;
        private string _sAccomType;
        private int _RegionId;
        private string _sRegion;
        private string _sAccomInitial;
        List<AccomodationSeasonDTO> _accomodationSeasonList;
        RoomDTO[] _oAccomRoomData;

        public int AccomodationId
        {
            get { return _AccomId; }
            set { _AccomId = value; }
        }

        public string AccomodationName
        {
            get { return _sAccom; }
            set { _sAccom = value; }
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

        public int RegionId
        {
            get { return _RegionId; }
            set { _RegionId = value; }
        }

        public string Region
        {
            get { return _sRegion; }
            set { _sRegion = value; }
        }

        public string AccomInitial
        {
            get { return _sAccomInitial; }
            set { _sAccomInitial = value; }
        }

        public List<AccomodationSeasonDTO> AccomodationSeasonList
        {
            get { return _accomodationSeasonList; }
            set { _accomodationSeasonList = value; }
        }        

        public RoomDTO[] RoomData
        {
            get { return _oAccomRoomData; }
            set { _oAccomRoomData = value; }
        }
    }

    public class AccomodationSeasonDTO
    {
        int _AccomodationId;
        DateTime _SeasonStartDate;
        DateTime _SeasonEndDate;

        public int AccomodationId
        {
            get { return _AccomodationId; }
            set { _AccomodationId = value; }
        }

        public DateTime SeasonStartDate
        {
            get { return _SeasonStartDate; }
            set { _SeasonStartDate = value; }
        }

        public DateTime SeasonEndDate
        {
            get { return _SeasonEndDate; }
            set { _SeasonEndDate = value; }
        }
    }
}
