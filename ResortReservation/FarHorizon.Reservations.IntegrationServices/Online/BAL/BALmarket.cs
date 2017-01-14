using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FarHorizon.Reservations.BusinessServices.Online.BAL
{
    public class BALmarket
    {
        public BALmarket()
        {

        }
        public string _Action { get; set; }
        public string _marketName { get; set; }
        public string _marketId { get; set; }
        public string _region { get; set; }
        public string _specification { get; set; }
    }
}