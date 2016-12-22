using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessTier.App_Code.DataEntities
{
    public class clsRegionData
    {
        int _iRegionId;
        string _sRegionName;
        clsAccomData[] _oAccomodationData;
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

        public clsAccomData[] Accomodation
        {
            get { return _oAccomodationData; }
            set { _oAccomodationData = value; }
        }
    }
}
