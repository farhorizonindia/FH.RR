using System;
using System.Collections.Generic;
using System.Text;
using BusinessTier.App_Code.DataEntities.Masters;

namespace BusinessTier.App_Code.DataEntities.Client
{
    public class clsBookingChartTreeDTO
    {
        clsRegionDTO _oRegion;
        clsAccomTypeDTO _oAccomodationType;
        clsAccomodationDTO _oAccomodation;

        public clsRegionDTO Region
        {
            get { return _oRegion; }
            set { _oRegion = value; }
        }        

        public clsAccomTypeDTO AccomodationType
        {
            get { return _oAccomodationType; }
            set { _oAccomodationType = value; }
        }       

        public clsAccomodationDTO Accomodation
        {
            get { return _oAccomodation; }
            set { _oAccomodation = value; }
        }
    }
}
