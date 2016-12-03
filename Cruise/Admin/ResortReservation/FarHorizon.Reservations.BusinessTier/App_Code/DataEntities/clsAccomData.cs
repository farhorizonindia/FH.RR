using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessTier.App_Code.DataEntities
{
    public class clsAccomData
    {
        int _AccomId;
        int _iAccomTypeId;
        string _sAccom;
        int _RegionId;
        clsRoomData[] _oRoomData;
        
        public int AccomodationId
        {
            get { return _AccomId; }
            set { _AccomId = value; }
        }

        public int AccomodationTypeId
        {
            get { return _iAccomTypeId; }
            set { _iAccomTypeId = value; }
        }
        
        public string AccomodationName
        {
            get { return _sAccom; }
            set { _sAccom = value; }
        }

        public int RegionId
        {
            get { return _RegionId; }
            set { _RegionId = value; }
        }

        public clsRoomData[] RoomData
        {
            get { return _oRoomData; }
            set { _oRoomData = value; }
        }
    }
}
