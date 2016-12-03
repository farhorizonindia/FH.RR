using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessTier.App_Code.DataEntities.Masters
{
    public class clsAccomodationDTO
    {
        private int _AccomId;
        private string _sAccom;
        private int _iAccomTypeId;
        private string _sAccomType;        
        private int _RegionId;
        private string _sRegion;
        private string _sAccomInitial;
        private DateTime _dSeasonStartDate;
        private DateTime _dSeasonEndDate;
                
        clsRoomDTO[] _oAccomRoomData;
                
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

        public DateTime SeasonStartDate
        {
            get { return _dSeasonStartDate; }
            set { _dSeasonStartDate = value; }
        }


        public DateTime SeasonEndDate
        {
            get { return _dSeasonEndDate; }
            set { _dSeasonEndDate = value; }
        }


        public clsRoomDTO[] RoomData
        {
            get { return _oAccomRoomData; }
            set { _oAccomRoomData = value; }
        }
    }
}
