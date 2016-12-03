using System;
using FarHorizon.Reservations.Common.DataEntities.Client;

namespace FarHorizon.Reservations.BusinessServices
{
    interface ITouristServices
    {
        bool AddBookingTourist(BookingTouristDTO oBookingTouristDTO, out int TouristNo);
        bool DeleteBookingTourist(int BookingId, int TouristNo);
        void DeleteBookingTourist(int BookingId);
        BookingTouristDTO[] GetBookingTouristDetails(int BookingId);
        BookingTouristDTO GetBookingTouristDetails(int BookingId, int TouristNo);
        BookingTouristDTO[] GetBookingTourists(int BookingId);
        bool UpdateBookingTourist(BookingTouristDTO oBookingTouristDTO);
    }
}
