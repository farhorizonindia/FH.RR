using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for BALSearch
/// </summary>

namespace FarHorizon.Reservations.BusinessServices.Online.BAL
{
    [Serializable]
    public class BALSearch
    {
        public BALSearch()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        public int CountryId { get; set; }
        public int RiverId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string action { get; set; }
        public string PackageId { get; set; }
        public int AgentId { get; set; }
        public string PackageType { get; set; }
        public int Openclose { get; set; }
    }
}