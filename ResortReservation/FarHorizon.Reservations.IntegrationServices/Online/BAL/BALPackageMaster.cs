﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for BALPackageMaster
/// </summary>
namespace FarHorizon.Reservations.BusinessServices.Online.BAL
{
    [Serializable]
    public class BALPackageMaster
    {
        public BALPackageMaster()
        {

        }
        public string _Action { get; set; }
        public string _packageId { get; set; }
        public string _packageName { get; set; }
        public int _NoOfNights { get; set; }
        public string _pakageType { get; set; }
        public string _MasterPackageId { get; set; }
        public int _BoardingFrom { get; set; }
        public int _BoardingTo { get; set; }
        public int _HotelId { get; set; }
        public DateTime _creationDate { get; set; }
        public string _night { get; set; }
        public int _cityId { get; set; }
        public bool _AllowCheckIn { get; set; }
        public bool _AllowCheckOut { get; set; }
        public string Direction { get; set; }
        public string ImagePath { get; set; }
        public string PackageDescription { get; set; }

        public string ItineraryLink { get; set; }
        public DateTime Checkin { get; set; }
        public DateTime checkout { get; set; }
<<<<<<< HEAD

        public bool IsActive { get; set; }
=======
>>>>>>> 06df147e7f6e76b3ddcb27473f8305164d96b955
    }
}