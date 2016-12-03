using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessTier.App_Code.DataEntities.Masters
{   
    public class clsBookingChartDTO
    {
        int _iAccomTypeId;
        string _sAccomType;
        int _iAccomId;
        string _sAccomName;
        string _sAccomInitial;        
        int _iRegionId;
        string _sRegionName;
        string _sRoomNo;

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
    }
}
