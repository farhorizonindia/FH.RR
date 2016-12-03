using System;
using System.Collections.Generic;
using System.Text;
using FarHorizon.Reservations.Common.DataEntities.Client;
using FarHorizon.Reservations.BusinessTier.BusinessLogic.Reports;
using FarHorizon.Reservations.BusinessTier.BusinessLogic.BookingEngine;
using FarHorizon.Reservations.BusinessTier.Helpers;

namespace FarHorizon.Reservations.BusinessServices
{
    public class BookingReportServices
    {
        BookingReportHelper bookingReportHelper = null;

        #region Get Method(s)
        public BookingRoomReportsDTO[] GetDetailedBookingDetails(int BookingId)
        {
            try
            {
                if (bookingReportHelper == null)
                    bookingReportHelper = new BookingReportHelper();
                return bookingReportHelper.GetDetailedBookingDetails(BookingId);
            }
            catch (Exception exp)
            {
                throw exp;
            }
        }


        public BookingRoomReportsDTO[] GetroomBookingDetailsMail(int BookingId)
        {
            try
            {
                if (bookingReportHelper == null)
                    bookingReportHelper = new BookingReportHelper();
                return bookingReportHelper.GetroomBookingDetailsmail(BookingId);
            }
            catch (Exception exp)
            {
                throw exp;
            }
        }

        public BookingDTMail[] GetBookingDetailsMail(int BookingId)
        {
            try
            {
               
                if (bookingReportHelper == null)
                    bookingReportHelper = new BookingReportHelper();
                return bookingReportHelper.GetBookingDetailsmail(BookingId);
            }
            catch (Exception exp)
            {
                throw exp;
            }
        }




        public BookingRoomReportsDTO[] GetBookingWithinCurrentBookingDates(int BookingiD)
        {
            try
            {
                if (bookingReportHelper == null)
                    bookingReportHelper = new BookingReportHelper();
                return bookingReportHelper.GetBookingWithinCurrentBookingDates(BookingiD);
            }
            catch (Exception exp)
            {
                throw exp;
            }
        }
        public BookingRoomReportsDTO[] GetOtherBookingsOfThisRoom(string RoomNo, DateTime StartDate, DateTime EndDate)
        {
            try
            {
                if (bookingReportHelper == null)
                    bookingReportHelper = new BookingReportHelper();
                return bookingReportHelper.GetOtherBookingsOfThisRoom(RoomNo, StartDate, EndDate);
            }
            catch (Exception exp)
            {
                throw exp;
            }
        }

        public CFormReportDTO GetCFormDataForForeignNationals(int bookingId)
        {
            try
            {
                if (bookingReportHelper == null)
                    bookingReportHelper = new BookingReportHelper();
                return bookingReportHelper.GetCFormDataForForeignNationals(bookingId);
            }
            catch (Exception exp)
            {
                throw exp;
            }
        }

        public CFormReportDTO GetCFormDataForIndianNationals(int bookingId)
        {
            try
            {
                if (bookingReportHelper == null)
                    bookingReportHelper = new BookingReportHelper();
                return bookingReportHelper.GetCFormDataForIndiaNationals(bookingId);
            }
            catch (Exception exp)
            {
                throw exp;
            }
        }

        public CFormReportDTO GetCFormData(int bookingId)
        {
            try
            {
                if (bookingReportHelper == null)
                    bookingReportHelper = new BookingReportHelper();
                return bookingReportHelper.GetCFormData(bookingId);
            }
            catch (Exception exp)
            {
                throw exp;
            }
        }

        private CFormReportDTO GetCFormDetails(int bookingId, bool foreignNationals, bool indianNationals)
        {
            CFormReportDTO cFormReportDto = null;
            try
            {
                BookingReportHelper BookingReportHelper = new BookingReportHelper();
                if (foreignNationals && indianNationals)
                {
                    cFormReportDto = BookingReportHelper.GetCFormData(bookingId);
                }
                else if (foreignNationals)
                {
                    cFormReportDto = BookingReportHelper.GetCFormDataForForeignNationals(bookingId);
                }
                else if (indianNationals)
                {
                    cFormReportDto = BookingReportHelper.GetCFormDataForIndiaNationals(bookingId);
                }
            }
            catch (Exception exp)
            {
                throw exp;
            }
            return cFormReportDto;
        }

        public List<clsTouristCountDTO> GetTouristCount(DateTime fromDate, DateTime toDate, int accomTypeId, int accomId)
        {
            try
            {
                BookingReportHelper BookingReportHelper = new BookingReportHelper();
                return BookingReportHelper.GetTouristCount(fromDate, toDate, accomTypeId, accomId);
            }
            catch (Exception exp)
            {
                throw exp;
            }
        }
        #endregion
    }
}
