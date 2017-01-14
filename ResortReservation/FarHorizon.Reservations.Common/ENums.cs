using System;
using System.Collections.Generic;
using System.Text;

namespace FarHorizon.Reservations.Common
{
    [Serializable]
    public enum BookingStatusTypes
    {
        NONE = 0,
        BOOKED = 1,
        CONFIRMED = 2,
        WAITLISTED = 3,
        CANCELLED = 4,
        PROPOSED = 5
    }

    [Serializable]
    public static class ENums
    {
        public enum BookingStatusTypes
        {
            NONE = 0,
            BOOKED = 1,
            CONFIRMED = 2,
            WAITLISTED = 3,
            CANCELLED = 4,
            PROPOSED = 5
        }

        public enum PageCommand
        {
            Add = 0,
            Update,
            Delete,
            Cancel,
            Comfirmation,
            View
        }

        public enum UploadXMLType
        {
            Tourist = 0,
            ApplicationRights
        }

        public enum EventName
        {
            NONE = 0,
            BOOKING,
            BOOKINGUPDATED,
            CONFIRMATION,
            CONFIRMATIONUPDATED,
            CANCELLED,
            DELETED
        }
    }
}
