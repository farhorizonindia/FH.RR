using System;
using System.Collections.Generic;
using System.Text;
using FarHorizon.Reservations.Common.DataEntities.Client;
using FarHorizon.Reservations.BusinessTier.BusinessLogic.Reports;
using FarHorizon.Reservations.BusinessTier.BusinessLogic.BookingEngine;
using FarHorizon.Reservations.Common;
using FarHorizon.Reservations.Common.DataEntities.Masters;

namespace FarHorizon.Reservations.BusinessTier.BusinessLogic.EventEmails
{
    public class EventEmailHelper
    {
        /// <summary>
        /// This Method will recive the booking Id, pull out the current status of the booking.
        /// and pull the email message and contacts and sent the email to the contacts.
        /// </summary>
        /// <param name="BookingId"></param>
        /// <returns></returns>
        public bool SendEventMail(int BookingId, ENums.EventName eventName)
        {
            try
            {
                EventEmailHandler eventEmailHandler = new EventEmailHandler();
                return eventEmailHandler.SendEventMail(BookingId, eventName);
            }
            catch (Exception exp)
            {
                //Swallowing the email exception.
                //throw exp;
                return false;
            }
        }
    }
}
