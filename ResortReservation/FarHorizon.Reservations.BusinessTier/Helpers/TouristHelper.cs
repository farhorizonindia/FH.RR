using System;
using System.Collections.Generic;
using System.Text;
using FarHorizon.Reservations.Common.DataEntities.Client;
using FarHorizon.Reservations.BusinessTier.BusinessLogic.BookingEngine;

namespace FarHorizon.Reservations.BusinessTier.Helpers
{
    public class TouristHelper
    {
        BookingTouristHandler touristHandler;
        public bool AddBookingTourist(BookingTouristDTO oBookingTouristDTO, out int TouristNo)
        {
            try
            {
                if (touristHandler == null)
                    touristHandler = new BookingTouristHandler();
                return touristHandler.InsertTouristDetails(oBookingTouristDTO, out TouristNo);
            }
            catch (Exception exp)
            {
                throw exp;
            }
        }
        public bool Addtouristentry(BookingTouristDTO oBookingTouristDTO, out int TouristNo)
        {
            try
            {
                if (touristHandler == null)
                    touristHandler = new BookingTouristHandler();
                return touristHandler.InsertTouristentry(oBookingTouristDTO, out TouristNo);
            }
            catch (Exception exp)
            {
                throw exp;
            }
        }
        public bool UpdateBookingTourist(BookingTouristDTO oBookingTouristDTO)
        {
            try
            {
                if (touristHandler == null)
                    touristHandler = new BookingTouristHandler();
                return touristHandler.UpdateTouristDetails(oBookingTouristDTO);
            }
            catch (Exception exp)
            {
                throw exp;
            }
        }

        public void DeleteBookingTourist(int BookingId)
        {
            try
            {
                if (touristHandler == null)
                    touristHandler = new BookingTouristHandler();
                touristHandler.DeleteTourist(BookingId);
            }
            catch (Exception exp)
            {
                throw exp;
            }
        }

        public bool DeleteBookingTourist(int BookingId, int TouristNo)
        {
            try
            {
                if (touristHandler == null)
                    touristHandler = new BookingTouristHandler();
                return touristHandler.DeleteTourist(BookingId, TouristNo);
            }
            catch (Exception exp)
            {
                throw exp;
            }
        }

        public BookingTouristDTO[] GetBookingTourists(int BookingId)
        {
            try
            {
                if (touristHandler == null)
                    touristHandler = new BookingTouristHandler();
                return touristHandler.GetTourists(BookingId);

            }
            catch (Exception exp)
            {
                throw exp;
            }
        }

        public BookingTouristDTO[] GetBookingTouristsftr(string bookingCode,string email,string passpno,string name,DateTime chkin,DateTime chkout,int acommid)
        {
            try
            {
                if (touristHandler == null)
                    touristHandler = new BookingTouristHandler();
                return touristHandler.GetTouristsftr(bookingCode,email,passpno,name,chkin,chkout,acommid);

            }
            catch (Exception exp)
            {
                throw exp;
            }
        }

        public BookingTouristDTO[] GetAllTouristDetails(int BookingId)
        {
            try
            {
                if (touristHandler == null)
                    touristHandler = new BookingTouristHandler();
                return touristHandler.GetAllTouristDetails(BookingId);
            }
            catch (Exception exp)
            {
                throw exp;
            }
        }

        public BookingTouristDTO GetBookingTouristDetails(int BookingId, int TouristNo)
        {
            try
            {
                if (touristHandler == null)
                    touristHandler = new BookingTouristHandler();
                return touristHandler.GetTouristDetails(BookingId, TouristNo);
            }
            catch (Exception exp)
            {
                throw exp;
            }
        }

        public BookingTouristDTO[] GetTourists(string FirstName, string LastName, string PassportNo, int NationalityID)
        {
            try
            {
                if (touristHandler == null)
                    touristHandler = new BookingTouristHandler();
                return touristHandler.SearchTourists(FirstName, LastName, PassportNo, NationalityID);
            }
            catch (Exception exp)
            {
                throw exp;
            }
        }
    }
}
