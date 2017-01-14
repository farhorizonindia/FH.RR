using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FarHorizon.Reservations.BusinessServices.Online.BAL
{
    public class BALPackageRateCard
    {
        public BALPackageRateCard()
        {
        }



        public string _Action { get; set; }
        public string _packageId { get; set; }
        public string _RateCardId { get; set; }
        public DateTime _valFrom { get; set; }
        public DateTime _ValTo { get; set; }
        public decimal _ppBc { get; set; }
        public decimal _SRSBc { get; set; }
        public bool _taxEx { get; set; }
        public decimal _taxValue { get; set; }
        public decimal _PPNc { get; set; }
        public decimal _SRSNc { get; set; }
        public DateTime _Date { get; set; }
        public int _fromPax { get; set; }
        public int _ToPax { get; set; }

        public int _fromPaxup { get; set; }
        public int _ToPaxup { get; set; }
        public int _RoomCategoryIdup { get; set; }

        public string _RateCategory { get; set; }
        public string _SupplierName { get; set; }
        public int _RoomCategoryId { get; set; }
        public long _LocationId { get; set; }
        public decimal _Tax { get; set; }
        public string _Currency { get; set; }

        public int FrompaxMain { get; set; }
        public int ToPaxMain { get; set; }
    }
}