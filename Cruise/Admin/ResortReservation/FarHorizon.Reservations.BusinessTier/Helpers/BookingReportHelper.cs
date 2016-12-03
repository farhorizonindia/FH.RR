using System;
using System.Collections.Generic;
using System.Text;
using FarHorizon.Reservations.Common.DataEntities.Client;
using FarHorizon.Reservations.BusinessTier.BusinessLogic.Reports;
using FarHorizon.Reservations.BusinessTier.BusinessLogic.BookingEngine;

namespace FarHorizon.Reservations.BusinessTier.Helpers
{
    public class BookingReportHelper
    {
        BookingRoomReportsHandler oBookingRoomReportsHandler = null;
        BookingCFormReportHandler bookingCFormReportHandler = null;

        public BookingRoomReportsDTO[] GetDetailedBookingDetails(int BookingId)
        {
            try
            {
                if (oBookingRoomReportsHandler == null)
                    oBookingRoomReportsHandler = new BookingRoomReportsHandler();
                return oBookingRoomReportsHandler.GetDetailedBookingDetails(BookingId);
            }
            catch (Exception exp)
            {
                throw exp;
            }
        }

        public BookingRoomReportsDTO[] GetroomBookingDetailsmail(int BookingId)
        {
            try
            {
                if (oBookingRoomReportsHandler == null)
                    oBookingRoomReportsHandler = new BookingRoomReportsHandler();
                return oBookingRoomReportsHandler.GetroomBookingDetailsmail(BookingId);
            }
            catch (Exception exp)
            {
                throw exp;
            }
        }


        public BookingDTMail[] GetBookingDetailsmail(int BookingId)
        {
            try
            {
                if (oBookingRoomReportsHandler == null)
                    oBookingRoomReportsHandler = new BookingRoomReportsHandler();
                return oBookingRoomReportsHandler.GetBookingDetailsmail(BookingId);
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
                if (oBookingRoomReportsHandler == null)
                    oBookingRoomReportsHandler = new BookingRoomReportsHandler();
                return oBookingRoomReportsHandler.GetBookingWithinCurrentBookingDates(BookingiD);
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
                if (oBookingRoomReportsHandler == null)
                    oBookingRoomReportsHandler = new BookingRoomReportsHandler();
                return oBookingRoomReportsHandler.GetOtherBookingsOfThisRoom(RoomNo, StartDate, EndDate);
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
                if (bookingCFormReportHandler == null)
                    bookingCFormReportHandler = new BookingCFormReportHandler();
                return bookingCFormReportHandler.GetCFormData(bookingId);
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
                if (bookingCFormReportHandler == null)
                    bookingCFormReportHandler = new BookingCFormReportHandler();
                return bookingCFormReportHandler.GetCFormDataForForeignNationals(bookingId);
            }
            catch (Exception exp)
            {
                throw exp;
            }
        }

        public CFormReportDTO GetCFormDataForIndiaNationals(int bookingId)
        {
            try
            {
                if (bookingCFormReportHandler == null)
                    bookingCFormReportHandler = new BookingCFormReportHandler();
                return bookingCFormReportHandler.GetCFormDataForIndiaNationals(bookingId);
            }
            catch (Exception exp)
            {
                throw exp;
            }
        }

        public List<clsTouristCountDTO> GetTouristCount(DateTime fromDate, DateTime toDate, int accomTypeId, int accomId)
        {
            try
            {
                BookingTouristHandler bookingTouristHandler = new BookingTouristHandler();
                return bookingTouristHandler.GetTouristCount(fromDate, toDate, accomTypeId, accomId);
            }
            catch (Exception exp)
            {
                throw exp;
            }
        }
    }
}
