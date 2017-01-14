using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FarHorizon.Reservations.BusinessServices.Online.BAL
{
    public class BALLinks
    {
        public BALLinks()
        {

        }
        public string _Action { get; set; }
        public string _CateId { get; set; }
        public string _MarketId { get; set; }
        public string _Agent { get; set; }

        public decimal _discount { get; set; }
    }
}