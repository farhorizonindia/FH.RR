using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FarHorizon.Reservations.Common.Models
{
    [Serializable]
    public class CruiseLocation
    {
        public DateTime CheckInDate { get; set; }
        public string PackageId { get; set; }
        public string StartLocation { get; set; }
        public string EndLocation { get; set; }
    }
}
