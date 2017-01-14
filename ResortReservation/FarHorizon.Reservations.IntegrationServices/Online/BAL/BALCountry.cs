using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FarHorizon.Reservations.BusinessServices.Online.BAL
{
    [Serializable]
    public class BALCountry
    {
        public BALCountry()
        {
        }
        public string _Action { get; set; }
        public string _CountryName { get; set; }
    }
}