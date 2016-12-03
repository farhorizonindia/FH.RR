using System;
using System.Collections.Generic;
using System.Text;
using FarHorizon.Reservations.BusinessTier.BusinessLogic.EventEmails;
using FarHorizon.Reservations.Common;

namespace FarHorizon.Reservations.BusinessServices
{
    public class EventEmailServices
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
                EventEmailHelper eventEmailProxy = new EventEmailHelper();
                return eventEmailProxy.SendEventMail(BookingId, eventName);
            }
            catch (Exception exp)
            {
                throw exp;
            }
        }
    }
}
