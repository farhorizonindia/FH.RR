using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using FarHorizon.Reservations.Common.DataEntities.Client;
using FarHorizon.Reservations.BusinessTier.BusinessLogic.BookingEngine;
using FarHorizon.Reservations.DataBaseManager;
using FarHorizon.Reservations.Common;
using FarHorizon.Reservations.Common.DataEntities.Masters;
using FarHorizon.Reservations.MasterServices;
namespace FarHorizon.Reservations.BusinessTier.BusinessLogic.Reports
{
    internal class BookingCFormReportHandler
    {
        public CFormReportDTO GetCFormData(int bookingId)
        {
            return GetCFormData(bookingId, true, true);
        }

        public CFormReportDTO GetCFormDataForForeignNationals(int bookingId)
        {
            return GetCFormData(bookingId, true, false);
        }

        public CFormReportDTO GetCFormDataForIndiaNationals(int bookingId)
        {
            return GetCFormData(bookingId, false, true);
        }

        private CFormReportDTO GetCFormData(int bookingId, bool foreignNationals, bool indianNationals)
        {
            CFormReportDTO cFormReportDto;
            BookingDTO bookingDetailsDto;
            BookingTouristDTO[] bookingTouristDetailsDto;
            BookingHandler bookingHandler;
            BookingTouristHandler touristHandler;
            try
            {
                bookingHandler = new BookingHandler();
                touristHandler = new BookingTouristHandler();
                cFormReportDto = new CFormReportDTO();

                GenerateCFormNumbers(bookingId);

                bookingDetailsDto = bookingHandler.GetBookingDetails(bookingId);
                cFormReportDto.CFormNo = GetBookingCFormNo(bookingDetailsDto, foreignNationals, indianNationals);

                cFormReportDto.BookingDetails = bookingDetailsDto;

                bookingTouristDetailsDto = touristHandler.GetAllTouristDetails(bookingId);

                if (foreignNationals && indianNationals)
                {
                    cFormReportDto.BookingTouristDetails = bookingTouristDetailsDto;
                }
                else if (foreignNationals)
                {
                    cFormReportDto.BookingTouristDetails = GetForeignNationalsData(bookingTouristDetailsDto);
                }
                else if (indianNationals)
                {
                    cFormReportDto.BookingTouristDetails = GetIndianNationalsData(bookingTouristDetailsDto);
                }

            }
            catch (Exception exp)
            {
                throw exp;
            }
            return cFormReportDto;
        }

        private BookingTouristDTO[] GetIndianNationalsData(BookingTouristDTO[] bookingTouristDetailsDto)
        {
            List<BookingTouristDTO> bookingTouristList = new List<BookingTouristDTO>();
            BookingTouristDTO bookingTouristDTO;
            for (int i = 0; i < bookingTouristDetailsDto.Length; i++)
            {
                if (bookingTouristDetailsDto[i].NationalityId == GF.GetIndianNatinalityId())
                {
                    bookingTouristDTO = new BookingTouristDTO();
                    bookingTouristDTO = bookingTouristDetailsDto[i];
                    bookingTouristList.Add(bookingTouristDTO);
                }
            }
            return bookingTouristList.ToArray();
        }

        private BookingTouristDTO[] GetForeignNationalsData(BookingTouristDTO[] bookingTouristDetailsDto)
        {
            List<BookingTouristDTO> bookingTouristList = new List<BookingTouristDTO>();
            BookingTouristDTO bookingTouristDTO;
            if (bookingTouristDetailsDto != null)
            {
                for (int i = 0; i < bookingTouristDetailsDto.Length; i++)
                {
                    if (bookingTouristDetailsDto[i].NationalityId != GF.GetIndianNatinalityId())
                    {
                        bookingTouristDTO = new BookingTouristDTO();
                        bookingTouristDTO = bookingTouristDetailsDto[i];
                        bookingTouristList.Add(bookingTouristDTO);
                    }
                }
            }
            return bookingTouristList.ToArray();
        }

        private string GetBookingCFormNo(BookingDTO bookingDetailsDto, bool foreignNationals, bool indianNationals)
        {
            StringBuilder cFormNo = new StringBuilder();
            String accomodationInitials = GetAccomodationInitials(bookingDetailsDto.AccomodationTypeId, bookingDetailsDto.AccomodationId);

            cFormNo.Append(accomodationInitials);
            cFormNo.Append("/C-F/");

            if (foreignNationals && indianNationals)
            {
                cFormNo.Append("FN/IN-");
                cFormNo.Append(bookingDetailsDto.ForeignNationalCFormNoStart.ToString("0000"));
                cFormNo.Append("-");
                cFormNo.Append("FN/IN-");
                cFormNo.Append(bookingDetailsDto.ForeignNationalCFormNoEnd.ToString("0000"));
            }
            else if (foreignNationals)
            {
                cFormNo.Append("FN-");
                cFormNo.Append(bookingDetailsDto.ForeignNationalCFormNoStart.ToString("0000"));
                cFormNo.Append("-");
                cFormNo.Append("FN-");
                cFormNo.Append(bookingDetailsDto.ForeignNationalCFormNoEnd.ToString("0000"));
            }
            else if (indianNationals)
            {
                cFormNo.Append("IN-");
                cFormNo.Append(bookingDetailsDto.IndianNationalCFormNoStart.ToString("0000"));
                cFormNo.Append("-");
                cFormNo.Append("IN-");
                cFormNo.Append(bookingDetailsDto.IndianNationalCFormNoEnd.ToString("0000"));
            }
            cFormNo.Append("/");

            String yearPostFix = GetYearsPostFix(bookingDetailsDto.ArrivalDateTime);

            cFormNo.Append(yearPostFix);
            return cFormNo.ToString();
        }

        private string GetYearsPostFix(DateTime arrivalDateTime)
        {
            #region Check Date Financial Year
            String yearPostFix = string.Empty;
            string newFinancialYearDate = "01-APRIL-" + arrivalDateTime.Year.ToString();
            if (DateTime.Compare(arrivalDateTime, DateTime.Parse(newFinancialYearDate)) >= 0)
            {
                yearPostFix = arrivalDateTime.Year.ToString() + "-" + arrivalDateTime.AddYears(1).Year.ToString();
            }
            else if (DateTime.Compare(arrivalDateTime, DateTime.Parse(newFinancialYearDate)) < 0)
            {
                yearPostFix = arrivalDateTime.AddYears(-1).Year.ToString() + "-" + arrivalDateTime.Year.ToString();
            }
            return yearPostFix;
            #endregion
        }

        private static string GetAccomodationInitials(int accomodationTypeId, int accomodationId)
        {
            String accomInitial = String.Empty;
            AccomodationDTO accomodation;
            AccomodationMaster accomodationMaster = new AccomodationMaster();

            accomodation = accomodationMaster.GetAccomodation(accomodationTypeId, accomodationId);
            if (accomodation != null)
            {
                accomInitial = accomodation.AccomInitial;
            }
            return accomInitial;
        }

        private bool GenerateCFormNumbers(int bookingId)
        {
            DatabaseManager oDB = null;
            try
            {
                if (oDB == null)
                    oDB = new DatabaseManager();
                string sProcName = "up_Upd_BookingCFormNos";
                oDB.DbCmd = oDB.GetStoredProcCommand(sProcName);
                oDB.DbDatabase.AddInParameter(oDB.DbCmd, "@bookingId", DbType.Int32, bookingId);
                oDB.ExecuteNonQuery(oDB.DbCmd);
            }
            catch (Exception exp)
            {
                oDB = null;
                GF.LogError("clsBCFormReport.GenerateCFormNumbers", exp.Message);
                return false;
            }
            finally
            {
                oDB = null;
            }
            return true;
        }
    }

}
