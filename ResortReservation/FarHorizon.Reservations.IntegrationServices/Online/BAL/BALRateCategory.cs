using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FarHorizon.Reservations.BusinessServices.Online.BAL
{
    public class BALRateCategory
    {
        public BALRateCategory()
        {
        }

        public string _Action { get; set; }
        public string _categoryId { get; set; }
        public string _CategoryName { get; set; }
        public string _AltName { get; set; }
        public string _Remark { get; set; }
        public bool _Status { get; set; }
    }
}