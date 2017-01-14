using System;
using System.Collections.Generic;
using System.Text;
using FarHorizon.Reservations.Common.DataEntities.Masters;

namespace FarHorizon.Reservations.Common.DataEntities.Client
{
    [Serializable]
    public class BookingChartTreeDTO
    {
        RegionDTO _oRegion;
        AccomTypeDTO _oAccomodationType;
        AccomodationDTO _oAccomodation;

        public RegionDTO Region
        {
            get { return _oRegion; }
            set { _oRegion = value; }
        }        

        public AccomTypeDTO AccomodationType
        {
            get { return _oAccomodationType; }
            set { _oAccomodationType = value; }
        }       

        public AccomodationDTO Accomodation
        {
            get { return _oAccomodation; }
            set { _oAccomodation = value; }
        }
    }
}
