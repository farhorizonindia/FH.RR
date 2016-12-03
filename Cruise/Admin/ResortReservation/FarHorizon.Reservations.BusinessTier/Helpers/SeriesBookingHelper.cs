using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using FarHorizon.Reservations.Common.DataEntities.Client;
using FarHorizon.Reservations.Common.DataEntities.Masters;
using FarHorizon.Reservations.BusinessTier.BusinessLogic.BookingEngine;

namespace FarHorizon.Reservations.BusinessTier.Helpers
{
    public class SeriesBookingHelper
    {
        SeriesHandler seriesHandler;
        SeriesDetailHandler seriesDetailHandler;
        public bool AddSeriesBooking(SeriesDTO SeriesDTO, List<SeriesDetailDTO> oSeriesDetailDatas, List<BookingOfSeriesDTO> oBookingOfSeriesDTO, out int SeriesId)
        {
            try
            {
                if (seriesHandler == null)
                    seriesHandler = new SeriesHandler();
                return seriesHandler.AddSeriesBooking(SeriesDTO, oSeriesDetailDatas, oBookingOfSeriesDTO, out SeriesId);
            }
            catch (Exception exp)
            {
                throw exp;
            }
        }

        public bool DeleteSeries(int SeriesId)
        {
            //TODO
            return true;
        }

        public List<SeriesDTO> GetSeriesOfAccomodation(int AccomodationId)
        {
            try
            {
                if (seriesHandler == null)
                    seriesHandler = new SeriesHandler();
                return seriesHandler.GetSeriesOfAccomodation(AccomodationId);
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
                if (seriesHandler == null)
                    seriesHandler = new SeriesHandler();
                return seriesHandler.GetSeries(SeriesId);
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
                if (seriesHandler == null)
                    seriesHandler = new SeriesHandler();
                return seriesHandler.GetSeries(SeriesStartDate);
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
                if (seriesHandler == null)
                    seriesHandler = new SeriesHandler();
                return seriesHandler.GetSeries(AccomodationId, 0, SeriesStartDate);
            }
            catch (Exception exp)
            {
                throw exp;
            }
        }

        private List<SeriesDTO> GetSeries(int AccomodationId, int SeriesId, DateTime SeriesStartDate)
        {
            try
            {
                if (seriesHandler == null)
                    seriesHandler = new SeriesHandler();
                return seriesHandler.GetSeries(AccomodationId, SeriesId, SeriesStartDate);
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
                if (seriesDetailHandler == null)
                    seriesDetailHandler = new SeriesDetailHandler();
                return seriesDetailHandler.GetSeriesDetail(SeriesId);
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
                if (seriesHandler == null)
                    seriesHandler = new SeriesHandler();
                return seriesHandler.GetRoomsForSeries(AccomodationId);
            }
            catch (Exception exp)
            {
                throw exp;
            }
        }

    }

}
