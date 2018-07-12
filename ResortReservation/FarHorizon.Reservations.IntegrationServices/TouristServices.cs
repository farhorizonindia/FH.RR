using System;
using System.Collections.Generic;
using System.Text;
using FarHorizon.Reservations.Common.DataEntities.Client;
using FarHorizon.Reservations.BusinessTier.BusinessLogic.BookingEngine;
using FarHorizon.Reservations.BusinessTier.Helpers;

namespace FarHorizon.Reservations.BusinessServices
{
    public class TouristServices
    {
        TouristHelper touristHelper;

        #region Add Method(s)
        public bool AddBookingTourist(BookingTouristDTO oBookingTouristDTO, out int TouristNo)
        {
            try
            {
                if (touristHelper == null)
                    touristHelper = new TouristHelper();
                return touristHelper.AddBookingTourist(oBookingTouristDTO, out TouristNo);
            }
            catch (Exception exp)
            {
                throw exp;
            }
        }
        #endregion
      
        public bool AddBookingTouristentry(BookingTouristDTO oBookingTouristDTO, out int TouristNo)
        {
            try
            {
                if (touristHelper == null)
                    touristHelper = new TouristHelper();
                return touristHelper.Addtouristentry(oBookingTouristDTO, out TouristNo);
            }
            catch (Exception exp)
            {
                throw exp;
            }
        }
       
        #region Update Method(s)
        public bool UpdateBookingTourist(BookingTouristDTO oBookingTouristDTO)
        {
            try
            {
                if (touristHelper == null)
                    touristHelper = new TouristHelper();
                return touristHelper.UpdateBookingTourist(oBookingTouristDTO);
            }
            catch (Exception exp)
            {
                throw exp;
            }
        }
        #endregion

        #region Delete Method(s)
        public bool DeleteBookingTourist(int BookingId)
        {
            try
            {
                return DeleteBookingTourist(BookingId, 0);
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
                if (touristHelper == null)
                    touristHelper = new TouristHelper();
                return touristHelper.DeleteBookingTourist(BookingId, TouristNo);
            }
            catch (Exception exp)
            {
                throw exp;
            }
        }
        #endregion

        #region Get Method(s)
        public BookingTouristDTO[] GetBookingTourists(int BookingId)
        {
            try
            {
                if (touristHelper == null)
                    touristHelper = new TouristHelper();
                return touristHelper.GetBookingTourists(BookingId);
            }
            catch (Exception exp)
            {
                throw exp;
            }
        }

        public BookingTouristDTO[] GetBookingTouristsftr(string bookingCode, string email, string passpno, string name,DateTime chkin,DateTime chkout,int accomid)
        {
            try
            {
                if (touristHelper == null)
                    touristHelper = new TouristHelper();
                return touristHelper.GetBookingTouristsftr(bookingCode,email,passpno,name,chkin,chkout,accomid);
            }
            catch (Exception exp)
            {
                throw exp;
            }
        }


        public BookingTouristDTO[] GetBookingTouristDetails(int BookingId)
        {
            try
            {
                if (touristHelper == null)
                    touristHelper = new TouristHelper();
                return touristHelper.GetAllTouristDetails(BookingId);
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
                if (touristHelper == null)
                    touristHelper = new TouristHelper();
                return touristHelper.GetBookingTouristDetails(BookingId, TouristNo);
            }
            catch (Exception exp)
            {
                throw exp;
            }
        }

        public BookingTouristDTO[] GetTourists(string FirstName, string LastName, string PassportNo, int NationalityID)
        {
            BookingTouristDTO[] oBookingTouristDTO;
            if (touristHelper == null)
                touristHelper = new TouristHelper();
            oBookingTouristDTO = touristHelper.GetTourists(FirstName, LastName, PassportNo, NationalityID);
            touristHelper = null;
            return oBookingTouristDTO;
        }
        #endregion
    }
}
