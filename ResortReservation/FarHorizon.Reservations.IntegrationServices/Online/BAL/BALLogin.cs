﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for BALLogin
/// </summary>

namespace FarHorizon.Reservations.BusinessServices.Online.BAL
{
    [Serializable]
    public class BALLogin
    {
        public BALLogin()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        public string EmailId { get; set; }
        public string Password { get; set; }
        public int BookingId { get; set; }
    }
}