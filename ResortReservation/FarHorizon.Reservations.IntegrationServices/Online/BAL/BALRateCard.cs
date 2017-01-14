using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FarHorizon.Reservations.BusinessServices.Online.BAL
{
    [Serializable]
    public class BALRateCard
    {
        public BALRateCard()
        {

        }
        public string _Action { get; set; }
        public int _AccomTypeId { get; set; }
        public string _RateCardId { get; set; }
        public string _RateCategoryId { get; set; }
        public int _AccomId { get; set; }
        public int _RoomCategoryId { get; set; }
        public DateTime _ValFrom { get; set; }
        public DateTime _ValTo { get; set; }
        public string _Season { get; set; }
        public int _minNights { get; set; }
        public string _OperatingDays { get; set; }
        public bool _AlloExtraBed { get; set; }
        public bool _WebEnabled { get; set; }
        public bool _TaxInclusive { get; set; }
        public bool _CommissionEnabled { get; set; }
        public int _RateTypeId { get; set; }
        public string _Currency { get; set; }
        public string _Remark { get; set; }
        public string _FitOrGit { get; set; }
        public int _RoomTypeId { get; set; }
        public decimal _Quad { get; set; }
        public decimal _ExtraBed { get; set; }
        public decimal _Amt { get; set; }
        public int _ServiceTypeId { get; set; }
        public string Description { get; set; }



        public double WelcomeDrink { get; set; }
        public double Breakfast { get; set; }
        public double Lunch { get; set; }
        public double Dinner { get; set; }
        public double EveSnacks { get; set; }

        public int GITPaxFrom { get; set; }
        public double TaxPct { get; set; }
    }
}