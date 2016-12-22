using System;
using System.Collections.Generic;
using System.Text;

namespace FarHorizon.Reservations.Common.DataEntities.Masters
{
    public class RegionDTO 
    {
        int _iRegionId;
        string _sRegionName;
        AccomodationDTO[] _oAccomodationData;
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

        public AccomodationDTO[] Accomodation
        {
            get { return _oAccomodationData; }
            set { _oAccomodationData = value; }
        }
    }
}
