using System;
using System.Collections.Generic;
using System.Text;

namespace FarHorizon.Reservations.Common.DataEntities.Masters
{
    public class AccomContactDTO 
    {
        int _ContactId;
        int _AccomodationId;
        string _ContactName;
        string _ToId;
        string _CCId;
        string _BCCId;
        bool _MailOnBooking;
        bool _MailOnBookingUpdate;
        bool _MailOnBookingConfirmation;
        bool _MailOnBookingConfirmationUpdate;
        bool _MailOnCancellation;
        bool _MailOnDeletion;

        public int ContactId
        {
            get { return _ContactId; }
            set { _ContactId = value; }
        }

        public int AccomodationId
        {
            get { return _AccomodationId; }
            set { _AccomodationId = value; }
        }

        public string ContactName
        {
            get { return _ContactName; }
            set { _ContactName = value; }
        }

        public string ToId
        {
            get { return _ToId; }
            set { _ToId = value; }
        }

        public string CCId
        {
            get { return _CCId; }
            set { _CCId = value; }
        }

        public string BCCId
        {
            get { return _BCCId; }
            set { _BCCId = value; }
        }

        public bool MailOnBooking
        {
            get { return _MailOnBooking; }
            set { _MailOnBooking = value; }
        }

        public bool MailOnBookingUpdate
        {
            get { return _MailOnBookingUpdate; }
            set { _MailOnBookingUpdate = value; }
        }

        public bool MailOnBookingConfirmation
        {
            get { return _MailOnBookingConfirmation; }
            set { _MailOnBookingConfirmation = value; }
        }

        public bool MailOnBookingConfirmationUpdate
        {
            get { return _MailOnBookingConfirmationUpdate; }
            set { _MailOnBookingConfirmationUpdate = value; }
        }

        public bool MailOnCancellation
        {
            get { return _MailOnCancellation; }
            set { _MailOnCancellation = value; }
        }

        public bool MailOnDeletion
        {
            get { return _MailOnDeletion; }
            set { _MailOnDeletion = value; }
        }

    }
}
