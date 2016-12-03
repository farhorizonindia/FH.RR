using System;
using FarHorizon.Reservations.Common.DataEntities.Masters;
using FarHorizon.Reservations.Common.DataEntities.Client;

namespace FarHorizon.Reservations.BusinessServices
{
    public interface IBookingServices
    {
        bool AddBooking(BookingDTO oBookingData, BookedRooms[] oBookedRooms, BookingWaitListDTO[] oBookingWaitListData,out int BookingId);
        bool UpdateBooking(BookingDTO oBookingData, BookedRooms[] oBookedRooms, BookingWaitListDTO[] oBookingWaitListData, BookedRooms[] TotallyRemovedRoomCategoryAndType);                
        bool DeleteBooking(int BookingId);

        bool DeleteBookingRooms(int BookingId, int AccomodationId, string RoomNo);
        void DeleteBookingRooms(int BookingId);
        void DeleteBookingRooms(int BookingId, int AccomodationId);        
        
        BookingDTO GetBookingDetails(int BookingId);
        BookingRoomDTO GetBookingRoomDetails(int BookingId, int AccomodationId, string RoomNo);
        BookingRoomDTO[] GetBookingRoomDetails(int BookingId, int AccomodationId);
        BookingRoomDTO[] GetBookingRoomDetails(int BookingId);
        BookingRoomDTO[] GetBookingRoomDetails(DateTime StartDate, DateTime EndDate);
        BookingWaitListDTO[] GetBlockedBookings(int BookingId);
    }
}
