using System;
using System.Collections.Generic;
using System.Text;

namespace FarHorizon.Reservations.Common.DataEntities.InputOutput
{
    public class cdtGetBookingsInput
    {
        /// <summary>
        /// Optional Field
        /// </summary>
        public Int32 AccomTypeId;
        /// <summary>
        /// Mutually exclusive with From date and To date.
        /// </summary>
        public String BookingCode;
        /// <summary>
        /// Optional Field
        /// </summary>
        public DateTime FromDate;
        /// <summary>
        /// Optional Field
        /// </summary>
        public DateTime ToDate;
        /// <summary>
        /// Optional Field
        /// </summary>
        public ENums.BookingStatusTypes BookingStatusType;
        /// <summary>
        /// Optional Field
        /// </summary>
        public Int32 AccomodationId;
        /// <summary>
        /// Optional Field
        /// </summary>
        public Int32 AgentId;

        /// <summary>
        /// Optional Field
        /// </summary>
        public Int32 RefAgentId;

    }
}
