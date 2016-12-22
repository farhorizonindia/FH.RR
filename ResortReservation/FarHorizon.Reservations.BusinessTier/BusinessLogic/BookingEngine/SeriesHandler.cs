using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using FarHorizon.Reservations.DataBaseManager;
using FarHorizon.Reservations.Common;
using FarHorizon.Reservations.Common.DataEntities.Client;
using FarHorizon.Reservations.Common.DataEntities.Masters;

namespace FarHorizon.Reservations.BusinessTier.BusinessLogic.BookingEngine
{
    internal class SeriesHandler
    {
        public bool AddSeriesBooking(SeriesDTO SeriesDTO, List<SeriesDetailDTO> oSeriesDetailList, List<BookingOfSeriesDTO> BookingsOfSeries, out int SeriesId)
        {
            int iSeriesId = 0;
            int iBookingId = 0;
            BookingHandler bookingHandler = new BookingHandler();
            SeriesDetailHandler oSeriesDetailHandler = new SeriesDetailHandler();
            //BookingDTO oBookingData;
            //BookedRooms oBookedRooms;
            //BookingWaitListDTO oBookingWaitListData;
            //BookingOfSeriesDTO FinalizedBookingDTO;
            List<BookingOfSeriesDTO> FinalizedBookingList = new List<BookingOfSeriesDTO>();
            
            #region Saving Series in Master and Detail
            AddSeries(SeriesDTO, out iSeriesId);

            foreach (SeriesDetailDTO var in oSeriesDetailList)
            {
                var.SeriesID = iSeriesId;
            }
            oSeriesDetailHandler.AddSeriesDetail(oSeriesDetailList);
            #endregion            

            #region Pulling EveryBooking and Saving It            
            foreach (BookingOfSeriesDTO FinalBooking in BookingsOfSeries)
            {
                FinalBooking.Booking.SeriesId = iSeriesId;
                bookingHandler.AddBooking(FinalBooking.Booking, FinalBooking.BookedRooms.ToArray(), FinalBooking.WaitListedRooms.ToArray(), out iBookingId);
            }
            #endregion Pulling EveryBooking and Saving It
            SeriesId = iSeriesId;
            return true;
        }        

        private bool AddSeries(SeriesDTO SeriesDTO, out int SeriesId)
        {
            DatabaseManager oDB;
            int sId = 0;
            bool Success = false;
            SeriesId = 0;
            try
            {
                oDB = new DatabaseManager();
                string sProcName = "up_Ins_Series";
                if (SeriesDTO != null)
                {
                    oDB.DbCmd = oDB.GetStoredProcCommand(sProcName);
                    oDB.DbDatabase.AddInParameter(oDB.DbCmd, "@SeriesName", DbType.String, SeriesDTO.SeriesName);
                    oDB.DbDatabase.AddInParameter(oDB.DbCmd, "@NoOfNights", DbType.Int32, SeriesDTO.NoOfNights);
                    oDB.DbDatabase.AddInParameter(oDB.DbCmd, "@GAP", DbType.Int32, SeriesDTO.GAP);
                    oDB.DbDatabase.AddInParameter(oDB.DbCmd, "@NoOfDeps", DbType.Int32, SeriesDTO.NoOfDepartures);
                    oDB.DbDatabase.AddInParameter(oDB.DbCmd, "@AccomId", DbType.Int32, SeriesDTO.AccomodationID);
                    oDB.DbDatabase.AddInParameter(oDB.DbCmd, "@AccomTypeId", DbType.Int32, SeriesDTO.AccomTypeID);
                    oDB.DbDatabase.AddInParameter(oDB.DbCmd, "@AgentId", DbType.Int32, SeriesDTO.AgentId);
                    oDB.DbDatabase.AddInParameter(oDB.DbCmd, "@SeriesStartDate", DbType.DateTime, SeriesDTO.SeriesStartDate);

                    sId = Convert.ToInt32(oDB.ExecuteScalar(oDB.DbCmd));
                    oDB.DbCmd.Parameters.Clear();
                    SeriesId = sId;
                    Success = true;
                }
            }
            catch (Exception exp)
            {
                oDB = null;
                GF.LogError("clsSeriesManager.AddSeriesBooking", exp.Message);
                Success = false;
            }
            finally
            {
                oDB = null;
                SeriesDTO = null;
            }
            return Success;
        }

        public SeriesDTO GetSeries(int SeriesId)
        {
            List<SeriesDTO> SeriesList;
            SeriesList = GetSeries(0, SeriesId, DateTime.MinValue);
            if (SeriesList != null && SeriesList.Count > 0)
            {
                return SeriesList[0];
            }
            else
            {
                return null;
            }
        }

        
        public List<SeriesDTO> GetSeries(DateTime SeriesStartDate)
        {
            return GetSeries(0, 0, SeriesStartDate);
        }

        public List<SeriesDTO> GetSeriesOfAccomodation(int AccomodationId)
        {
            return GetSeries(AccomodationId, DateTime.MinValue);
        }
        public List<SeriesDTO> GetSeries(int AccomodationId, DateTime SeriesStartDate)
        {
            return GetSeries(AccomodationId, 0, SeriesStartDate);
        }

        public List<SeriesDTO> GetSeries(int AccomodationId, int SeriesId, DateTime SeriesStartDate)
        {            
            SeriesDTO oSeriesDTO = new SeriesDTO();
            oSeriesDTO.AccomodationID = AccomodationId;
            oSeriesDTO.SeriesId = SeriesId;
            oSeriesDTO.SeriesStartDate = SeriesStartDate;
            return GetSeries(oSeriesDTO);
        }

        public List<SeriesDTO> GetSeries(SeriesDTO SeriesDTO)
        {
            List<SeriesDTO> oSeriesList = new List<SeriesDTO>();
            SeriesDTO Series;
            DataSet dsSeries;
            DataRow dr;
            DatabaseManager oDB;

            string sProcName=string.Empty;
            try
            {
                oDB = new DatabaseManager();
                sProcName = "up_Get_Series";
                oDB.DbCmd = oDB.GetStoredProcCommand(sProcName);

                oDB.DbDatabase.AddInParameter(oDB.DbCmd, "@SeriesId", DbType.Int32, SeriesDTO.SeriesId);
                oDB.DbDatabase.AddInParameter(oDB.DbCmd, "@AccomId", DbType.Int32, SeriesDTO.AccomodationID);
                oDB.DbDatabase.AddInParameter(oDB.DbCmd, "@SeriesStartDate", DbType.Date, GF.HandleMaxMinDates(SeriesDTO.SeriesStartDate, false));

                dsSeries = oDB.ExecuteDataSet(oDB.DbCmd);
                oDB = null;
            }
            catch (Exception exp)
            {
                oDB = null;
                dsSeries = null;
                GF.LogError("clsSeriesHandler.GetSeries", exp.Message);
            }
            if (dsSeries != null)
            {
                //oBookingRoomDTO[dsSeries.Tables[0].Rows.Count] = new BookingRoomDTO();
                for (int i = 0; i < dsSeries.Tables[0].Rows.Count; i++)
                {                    
                    Series = new SeriesDTO();
                    dr = dsSeries.Tables[0].Rows[i];
                    Series.SeriesId = Convert.ToInt32(dr.ItemArray.GetValue(0));
                    Series.Accomodation = Convert.ToString(dr.ItemArray.GetValue(1));
                    Series.SeriesName = Convert.ToString(dr.ItemArray.GetValue(2));
                    Series.NoOfNights = Convert.ToInt32(dr.ItemArray.GetValue(3));
                    Series.GAP = Convert.ToInt32(dr.ItemArray.GetValue(4).ToString());
                    Series.NoOfDepartures = Convert.ToInt32(dr.ItemArray.GetValue(5));
                    Series.SeriesStartDate = Convert.ToDateTime(dr.ItemArray.GetValue(6));
                    Series.AccomodationID = Convert.ToInt32(dr.ItemArray.GetValue(7));
                    Series.AccomTypeID = Convert.ToInt32(dr.ItemArray.GetValue(8));
                    Series.AgentId = Convert.ToInt32(dr.ItemArray.GetValue(9));
                    oSeriesList.Add(Series);
                }
            }            
            return oSeriesList;
        }

        public SeriesDTO[] GetRoomsForSeries(int AccomodationId)
        {
            SeriesDTO[] oSeriesDTO;
            DataSet dsSeriesData;
            DataRow dr;
            string sProcName;
            DatabaseManager oDB;

            dsSeriesData = null;
            oSeriesDTO = null;
            try
            {
                oDB = new DatabaseManager();
                sProcName = "up_Get_RoomsForSeries";
                oDB.DbCmd = oDB.GetStoredProcCommand(sProcName);
                oDB.DbDatabase.AddInParameter(oDB.DbCmd, "@AccomId", DbType.Int32, AccomodationId);
                dsSeriesData = oDB.ExecuteDataSet(oDB.DbCmd);
                oDB = null;
            }
            catch (Exception exp)
            {
                oDB = null;
                dsSeriesData = null;
                GF.LogError("clsBookingRoomHandler.GetBookingRoomsForSeries", exp.Message);
            }
            if (dsSeriesData != null)
            {
                oSeriesDTO = new SeriesDTO[dsSeriesData.Tables[0].Rows.Count];
                for (int i = 0; i < dsSeriesData.Tables[0].Rows.Count; i++)
                {
                    oSeriesDTO[i] = new SeriesDTO();
                    dr = dsSeriesData.Tables[0].Rows[i];
                    oSeriesDTO[i].RoomCategoryId = (dr.ItemArray.GetValue(0) == DBNull.Value ? 0 : Convert.ToInt32(dr.ItemArray.GetValue(0)));
                    oSeriesDTO[i].RoomCategory = (dr.ItemArray.GetValue(0) == DBNull.Value ? "" : Convert.ToString(dr.ItemArray.GetValue(1)));
                    oSeriesDTO[i].RoomTypeId = (dr.ItemArray.GetValue(0) == DBNull.Value ? 0 : Convert.ToInt32(dr.ItemArray.GetValue(2)));
                    oSeriesDTO[i].RoomType = (dr.ItemArray.GetValue(0) == DBNull.Value ? "" : Convert.ToString(dr.ItemArray.GetValue(3)));
                    oSeriesDTO[i].TotalRooms = (dr.ItemArray.GetValue(0) == DBNull.Value ? 0 : Convert.ToInt32(dr.ItemArray.GetValue(4)));
                }
            }
            return oSeriesDTO;
        }
        public bool AddSeriesBookingRooms(SeriesDTO SeriesDTO)
        {
            DatabaseManager oDB;
            //int SeriesID = 0;
            try
            {
                oDB = new DatabaseManager();
                string sProcName = "up_SaveSeriesBookingRooms";
                if (SeriesDTO != null)
                {

                    oDB.DbCmd = oDB.GetStoredProcCommand(sProcName);
                    oDB.DbDatabase.AddInParameter(oDB.DbCmd, "@iBookingID", DbType.Int32, SeriesDTO.BookingId);
                    oDB.DbDatabase.AddInParameter(oDB.DbCmd, "@iSeriesID", DbType.Int32, SeriesDTO.SeriesId);
                    oDB.DbDatabase.AddInParameter(oDB.DbCmd, "@dtStartDate", DbType.DateTime, Convert.ToDateTime(SeriesDTO.StartDate.ToString()));
                    oDB.DbDatabase.AddInParameter(oDB.DbCmd, "@dtEndDate", DbType.DateTime, Convert.ToDateTime(SeriesDTO.EndDate.ToString()));
                    oDB.DbDatabase.AddInParameter(oDB.DbCmd, "@iAccomID", DbType.Int32, SeriesDTO.AccomodationID);
                    oDB.DbDatabase.AddInParameter(oDB.DbCmd, "@iRoomCategoryID", DbType.Int32, SeriesDTO.RoomCategoryId);
                    oDB.DbDatabase.AddInParameter(oDB.DbCmd, "@iRoomTypeID", DbType.Int32, SeriesDTO.RoomTypeId);
                    oDB.DbDatabase.AddInParameter(oDB.DbCmd, "@iRequired", DbType.Int32, SeriesDTO.TotalRooms);
                    oDB.ExecuteNonQuery(oDB.DbCmd);
                    oDB.DbCmd.Parameters.Clear();
                }
            }
            catch (Exception exp)
            {
                oDB = null;
                GF.LogError("clsSeriesManager.AddSeriesBooking", exp.Message);
                return false;
            }
            finally
            {
                oDB = null;
                SeriesDTO = null;
            }
            return true;
        }
    }
}
