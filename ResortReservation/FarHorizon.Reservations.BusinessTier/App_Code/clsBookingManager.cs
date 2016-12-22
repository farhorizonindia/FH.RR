using System;
using System.Collections.Generic;
using System.Text;
using BusinessTier;
using BusinessTier.App_Code;
using BusinessTier.App_Code.DataEntities;

namespace BusinessTier
{
    public class clsBookingManager : BusinessTier.App_Code.IBookingManager
    {
        clsBookingHandler oBHandler;
        clsBookingRoomHandler oBRoomHandler;
        clsBookingTouristHandler oBTouristHandler;

        public int AddBooking(clsBookingData oBookingData)
        {
            int iBookingId = 0;
            if(oBHandler == null)
                oBHandler = new clsBookingHandler();
            iBookingId = oBHandler.AddBooking(oBookingData);
            return iBookingId;
        }

        public bool AddBookingRooms(clsBookingRoomData[] oBookingRoomData)
        {
            bool booked;
            if (oBRoomHandler == null)
                oBRoomHandler = new clsBookingRoomHandler();
            booked = oBRoomHandler.AddBookingRooms(oBookingRoomData);
            return booked;
        }

        public bool AddBookingTourist(clsBookingTouristData oBookingTouristData)
        {
            bool booked;
            if(oBTouristHandler == null)
                oBTouristHandler = new clsBookingTouristHandler();
            booked = oBTouristHandler.InsertTouristDetails(oBookingTouristData);
            return booked;
        }

        public bool UpdateBooking(clsBookingData oBookingData)
        {
            bool updated;
            if (oBHandler == null)
                oBHandler = new clsBookingHandler();
            updated = oBHandler.UpdateBooking(oBookingData);
            return updated;
        }

        public bool UpdateBookingRooms(clsBookingRoomData[] oBookingRoomData)
        {
            bool updated;
            if (oBRoomHandler == null)
                oBRoomHandler = new clsBookingRoomHandler();
            updated = oBRoomHandler.UpdateBooking(oBookingRoomData);
            return updated; 
        }

        public bool UpdateBookingTourist(clsBookingTouristData oBookingTouristData)
        {
            bool updated;
            if (oBTouristHandler == null)
                oBTouristHandler = new clsBookingTouristHandler();
            updated = oBTouristHandler.UpdateTouristDetails(oBookingTouristData);
            return updated;
        }

        public bool DeleteBooking(int BookingId)
        {
            bool deleted;
            if (oBHandler == null)
                oBHandler = new clsBookingHandler();
            deleted = oBHandler.DeleteBooking(BookingId);
            return deleted;
        }

        public void DeleteBookingRooms(int BookingId)
        {
            DeleteBookingRooms(BookingId, 0);
        }

        public void DeleteBookingRooms(int BookingId, int AccomodationId)
        {
            DeleteBookingRooms(BookingId, AccomodationId,"");
        }

        public bool DeleteBookingRooms(int BookingId, int AccomodationId, string RoomNo)
        {
            bool deleted;
            if (oBRoomHandler == null)
                oBRoomHandler = new clsBookingRoomHandler();
            deleted =oBRoomHandler.DeleteBookingRooms(BookingId, AccomodationId, RoomNo);
            return deleted; 
        }

        public void DeleteBookingTourist(int BookingId)
        {
            DeleteBookingTourist(BookingId, 0);
        }

        public bool DeleteBookingTourist(int BookingId, int TouristNo)
        {
            bool deleted;
            if (oBTouristHandler == null)
                oBTouristHandler = new clsBookingTouristHandler();
            deleted = oBTouristHandler.DeleteTourist(BookingId, TouristNo);
            return deleted;
        }

        public clsBookingData GetBookingDetails(int BookingId)
        {
            clsBookingData oBookingData;
            if (oBHandler == null)
                oBHandler = new clsBookingHandler();
            oBookingData = oBHandler.GetBookingDetails(BookingId);
            oBHandler = null;
            return oBookingData;
        }

        public clsBookingRoomData[] GetBookingRoomDetails(int BookingId)
        {
            clsBookingRoomData[] oBookingRoomData;
            if (oBRoomHandler == null)
                oBRoomHandler = new clsBookingRoomHandler();
            oBookingRoomData = oBRoomHandler.GetBookingRoomDetails(BookingId);
            oBRoomHandler = null;
            return oBookingRoomData;
        }

        public clsBookingRoomData[] GetBookingRoomDetails(int BookingId, int AccomodationId)
        {
            clsBookingRoomData[] oBookingRoomData;
            if (oBRoomHandler == null)
                oBRoomHandler = new clsBookingRoomHandler();
            oBookingRoomData = oBRoomHandler.GetBookingRoomDetails(BookingId);
            oBRoomHandler = null;
            return oBookingRoomData;
        }

        public clsBookingRoomData GetBookingRoomDetails(int BookingId, int AccomodationId, string RoomNo)
        {
            clsBookingRoomData oBookingRoomData;
            if (oBRoomHandler == null)
                oBRoomHandler = new clsBookingRoomHandler();
            oBookingRoomData = oBRoomHandler.GetBookingRoomDetails(BookingId,AccomodationId,RoomNo);
            oBRoomHandler = null;
            return oBookingRoomData;
        }

        public clsBookingRoomData[] GetBookingRoomDetails(DateTime StartDate, DateTime EndDate)
        {
            clsBookingRoomData[] oBookingRoomData;
            if (oBRoomHandler == null)
                oBRoomHandler = new clsBookingRoomHandler();
            oBookingRoomData = oBRoomHandler.GetBookingRoomDetails(StartDate, EndDate);
            oBRoomHandler = null;
            return oBookingRoomData;
        }

        public clsBookingTouristData[] GetBookingTouristDetails(int BookingId)
        {
            clsBookingTouristData[] oBookingTouristData;
            if (oBTouristHandler == null)
                oBTouristHandler = new clsBookingTouristHandler();
            oBookingTouristData = oBTouristHandler.GetTouristDetails(BookingId);
            oBRoomHandler = null;
            return oBookingTouristData;
            
        }

        public clsBookingTouristData GetBookingTouristDetails(int BookingId, int TouristNo)
        {
            clsBookingTouristData oBookingTouristData;
            if (oBTouristHandler == null)
                oBTouristHandler = new clsBookingTouristHandler();
            oBookingTouristData = oBTouristHandler.GetTouristDetails(BookingId, TouristNo);
            oBRoomHandler = null;
            return oBookingTouristData;

        }

        
        
    }
}
