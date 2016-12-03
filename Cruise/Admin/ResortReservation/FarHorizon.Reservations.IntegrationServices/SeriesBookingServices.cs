using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using FarHorizon.Reservations.Common.DataEntities.Client;
using FarHorizon.Reservations.Common.DataEntities.Masters;
using FarHorizon.Reservations.BusinessTier.BusinessLogic.BookingEngine;
using FarHorizon.Reservations.BusinessTier.Helpers;

namespace FarHorizon.Reservations.BusinessServices
{
    public class SeriesBookingServices
    {
        SeriesBookingHelper seriesBookingHelper;

        #region Add Method(s)
        public bool AddSeriesBooking(SeriesDTO SeriesDTO, List<SeriesDetailDTO> oSeriesDetailDatas, List<BookingOfSeriesDTO> oBookingOfSeriesDTO, out int SeriesId)
        {
            try
            {
                if (seriesBookingHelper == null)
                    seriesBookingHelper = new SeriesBookingHelper();
                return seriesBookingHelper.AddSeriesBooking(SeriesDTO, oSeriesDetailDatas, oBookingOfSeriesDTO, out SeriesId);
            }
            catch (Exception exp)
            {
                throw exp;
            }
        }
        #endregion

        #region Delete Method(s)
        public bool DeleteSeries(int SeriesId)
        {
            return true;
        }
        #endregion

        #region Get Method(s)
        public List<SeriesDTO> GetSeriesOfAccomodation(int AccomodationId)
        {
            try
            {
                if (seriesBookingHelper == null)
                    seriesBookingHelper = new SeriesBookingHelper();
                return seriesBookingHelper.GetSeriesOfAccomodation(AccomodationId);
            }
            catch (Exception exp)
            {
                throw exp;
            }
        }

        public SeriesDTO GetSeries(int SeriesId)
        {
            try
            {
                if (seriesBookingHelper == null)
                    seriesBookingHelper = new SeriesBookingHelper();
                return seriesBookingHelper.GetSeries(SeriesId);
            }
            catch (Exception exp)
            {
                throw exp;
            }
        }

        public List<SeriesDTO> GetSeries(DateTime SeriesStartDate)
        {
            try
            {
                if (seriesBookingHelper == null)
                    seriesBookingHelper = new SeriesBookingHelper();
                return seriesBookingHelper.GetSeries(SeriesStartDate);
            }
            catch (Exception exp)
            {
                throw exp;
            }
        }

        public List<SeriesDTO> GetSeries(int AccomodationId, DateTime SeriesStartDate)
        {
            try
            {
                if (seriesBookingHelper == null)
                    seriesBookingHelper = new SeriesBookingHelper();
                return seriesBookingHelper.GetSeries(AccomodationId, SeriesStartDate);
            }
            catch (Exception exp)
            {
                throw exp;
            }
        }

        public List<SeriesDetailDTO> GetSeriesDetail(int SeriesId)
        {
            try
            {
                if (seriesBookingHelper == null)
                    seriesBookingHelper = new SeriesBookingHelper();
                return seriesBookingHelper.GetSeriesDetail(SeriesId);
            }
            catch (Exception exp)
            {
                throw exp;
            }
        }

        public SeriesDTO[] GetRoomsForSeries(int AccomodationId)
        {
            try
            {
                if (seriesBookingHelper == null)
                    seriesBookingHelper = new SeriesBookingHelper();
                return seriesBookingHelper.GetRoomsForSeries(AccomodationId);
            }
            catch (Exception exp)
            {
                throw exp;
            }
        }
        #endregion
    }
}
