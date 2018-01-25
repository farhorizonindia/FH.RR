using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using FarHorizon.Reservations.DataBaseManager;
using FarHorizon.Reservations.Common;
using FarHorizon.Reservations.Common.DataEntities.Masters;
using FarHorizon.Reservations.Common.DataEntities.Client;
using FarHorizon.Reservations.BusinessTier.BusinessLogic;
using FarHorizon.Reservations.MasterServices;
using FarHorizon.DataSecurity;

namespace FarHorizon.Reservations.BusinessTier.BusinessLogic.BookingChart
{
    internal class BookingChartViewHandler
    {
        #region Public Methods
        public RegionDTO[] GetRegionDetails()
        {
            RegionMaster oRegionMaster;
            oRegionMaster = new RegionMaster();
            return oRegionMaster.GetData();
        }

        public RoomBookingDateWiseDTO[] GetBookingDataForChart(int AccomodationTypeId, int RegionId, int AccomodationId, DateTime FromDate, DateTime ToDate)
        {
            return GetBookingDataForChart(AccomodationTypeId, RegionId, AccomodationId, "", FromDate, ToDate);
        }


        public BookingChartDTO[] GetmaintenanceDataForChart(int AccomodationTypeId, int RegionId, int AccomodationId, DateTime FromDate, DateTime ToDate)
        {
            return GetRoomDetmaintenance(AccomodationTypeId, RegionId, AccomodationId, "", FromDate, ToDate);
        }




        public AccomTypeDTO[] GetBookingChart(int RegionId, int AccomodationTypeId, int AccomodationId, DateTime BookingFromDate, DateTime BookingToDate)
        {
            AccomTypeDTO[] oAccomTypeData;
            string sRoomNo;
            int iAccomId;
            oAccomTypeData = null;

            try
            {
                oAccomTypeData = GetRoomDetails(RegionId, AccomodationTypeId, AccomodationId);
                if (oAccomTypeData != null)
                {
                    for (int i = 0; i < oAccomTypeData.Length; i++)
                    {
                        if (oAccomTypeData[i].Accomodations != null)
                        {
                            for (int j = 0; j < oAccomTypeData[i].Accomodations.Length; j++)
                            {
                                if (oAccomTypeData[i].Accomodations[j].RoomData != null)
                                {
                                    for (int k = 0; k < oAccomTypeData[i].Accomodations[j].RoomData.Length; k++)
                                    {
                                        iAccomId = oAccomTypeData[i].Accomodations[j].AccomodationId;
                                        sRoomNo = oAccomTypeData[i].Accomodations[j].RoomData[k].RoomNo.ToString();
                                        oAccomTypeData[i].Accomodations[j].RoomData[k].RoomBookingData = GetBookingDataForChart(AccomodationTypeId, RegionId, iAccomId, sRoomNo, BookingFromDate, BookingToDate);
                                    }
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception exp)
            {
                throw exp;
            }
            return oAccomTypeData;
        }

        public BookingChartDTO[] GetRoomDetailsNew(int RegionId, int AccomodationTypeId, int AccomodationId)
        {
            DataSet dsBookingChartData;
            DataRow dr;
            BookingChartDTO[] oBookingChartDTO;
            string sProcName;
            DatabaseManager oDB;

            dsBookingChartData = null;
            oBookingChartDTO = null;

            try
            {
                oDB = new DatabaseManager();
                sProcName = "up_Get_BookingChart";
                oDB.DbCmd = oDB.GetStoredProcCommand(sProcName);
                oDB.DbDatabase.AddInParameter(oDB.DbCmd, "@iRegionId", DbType.Int32, RegionId);
                oDB.DbDatabase.AddInParameter(oDB.DbCmd, "@iAccomodationTypeId", DbType.Int32, AccomodationTypeId);
                oDB.DbDatabase.AddInParameter(oDB.DbCmd, "@iAccomodationId", DbType.Int32, AccomodationId);
                dsBookingChartData = oDB.ExecuteDataSet(oDB.DbCmd);

                if (dsBookingChartData != null)
                {
                    oBookingChartDTO = new BookingChartDTO[dsBookingChartData.Tables[0].Rows.Count];
                    for (int i = 0; i < dsBookingChartData.Tables[0].Rows.Count; i++)
                    {
                        oBookingChartDTO[i] = new BookingChartDTO();
                        dr = dsBookingChartData.Tables[0].Rows[i];
                        if (dr.ItemArray.GetValue(0) != DBNull.Value)
                            oBookingChartDTO[i].AccomodationTypeId = Convert.ToInt32(dr.ItemArray.GetValue(0));
                        if (dr.ItemArray.GetValue(1) != DBNull.Value)
                            oBookingChartDTO[i].AccomodationType = Convert.ToString(dr.ItemArray.GetValue(1));
                        if (dr.ItemArray.GetValue(2) != DBNull.Value)
                            oBookingChartDTO[i].AccomodationId = Convert.ToInt32(dr.ItemArray.GetValue(2));
                        if (dr.ItemArray.GetValue(3) != DBNull.Value)
                            oBookingChartDTO[i].AccomodationName = Convert.ToString(dr.ItemArray.GetValue(3));
                        if (dr.ItemArray.GetValue(4) != DBNull.Value)
                            oBookingChartDTO[i].RegionId = Convert.ToInt32(dr.ItemArray.GetValue(4));
                        if (dr.ItemArray.GetValue(5) != DBNull.Value)
                            oBookingChartDTO[i].RegionName = Convert.ToString(dr.ItemArray.GetValue(5));
                        if (dr.ItemArray.GetValue(6) != DBNull.Value)
                            oBookingChartDTO[i].RoomNo = Convert.ToString(dr.ItemArray.GetValue(6));
                        if (dr.ItemArray.GetValue(7) != DBNull.Value)
                            oBookingChartDTO[i].AccomInitial = Convert.ToString(dr.ItemArray.GetValue(7));
                        if (dr.ItemArray.GetValue(8) != DBNull.Value)
                            oBookingChartDTO[i].RoomCategoryAlias = Convert.ToString(dr.ItemArray.GetValue(8));
                    }
                }
            }
            catch (Exception exp)
            {
                oDB = null;
                dsBookingChartData = null;
                throw exp;
            }
            finally
            {
                oDB = null;
            }
            return oBookingChartDTO;
        }



        public BookingChartDTO[] GetRoomDetmaintenance(int AccomodationTypeId, int RegionId, int AccomodationId, string RoomNo, DateTime FromDate, DateTime ToDate)
        {
            DataSet dsBookingChartData;
            DataRow dr;
            BookingChartDTO[] oBookingChartDTO;
            string sProcName;
            DatabaseManager oDB;

            dsBookingChartData = null;
            oBookingChartDTO = null;

            try
            {
                oDB = new DatabaseManager();
                sProcName = "up_Get_BookingChartMaintenance";
                oDB.DbCmd = oDB.GetStoredProcCommand(sProcName);
                oDB.DbDatabase.AddInParameter(oDB.DbCmd, "@AccomodationTypeId", DbType.Int32, AccomodationTypeId);
                oDB.DbDatabase.AddInParameter(oDB.DbCmd, "@RegionId", DbType.Int32, RegionId);
                oDB.DbDatabase.AddInParameter(oDB.DbCmd, "@AccomodationId", DbType.Int32, AccomodationId);
                oDB.DbDatabase.AddInParameter(oDB.DbCmd, "@RoomNo", DbType.String, RoomNo);
                oDB.DbDatabase.AddInParameter(oDB.DbCmd, "@FromDate", DbType.Date, GF.HandleMaxMinDates(FromDate, false));
                oDB.DbDatabase.AddInParameter(oDB.DbCmd, "@ToDate", DbType.Date, GF.HandleMaxMinDates(ToDate, false));
                dsBookingChartData = oDB.ExecuteDataSet(oDB.DbCmd);

                if (dsBookingChartData != null)
                {
                    oBookingChartDTO = new BookingChartDTO[dsBookingChartData.Tables[0].Rows.Count];
                    for (int i = 0; i < dsBookingChartData.Tables[0].Rows.Count; i++)
                    {
                        oBookingChartDTO[i] = new BookingChartDTO();
                        dr = dsBookingChartData.Tables[0].Rows[i];
                        if (dr.ItemArray.GetValue(0) != DBNull.Value)
                            oBookingChartDTO[i].AccomodationTypeId = Convert.ToInt32(dr.ItemArray.GetValue(0));
                        if (dr.ItemArray.GetValue(1) != DBNull.Value)
                            oBookingChartDTO[i].AccomodationType = Convert.ToString(dr.ItemArray.GetValue(1));
                        if (dr.ItemArray.GetValue(2) != DBNull.Value)
                            oBookingChartDTO[i].AccomodationId = Convert.ToInt32(dr.ItemArray.GetValue(2));
                        if (dr.ItemArray.GetValue(3) != DBNull.Value)
                            oBookingChartDTO[i].AccomodationName = Convert.ToString(dr.ItemArray.GetValue(3));
                        if (dr.ItemArray.GetValue(4) != DBNull.Value)
                            oBookingChartDTO[i].RegionId = Convert.ToInt32(dr.ItemArray.GetValue(4));
                        if (dr.ItemArray.GetValue(5) != DBNull.Value)
                            oBookingChartDTO[i].RegionName = Convert.ToString(dr.ItemArray.GetValue(5));
                        if (dr.ItemArray.GetValue(6) != DBNull.Value)
                            oBookingChartDTO[i].RoomNo = Convert.ToString(dr.ItemArray.GetValue(6));
                        if (dr.ItemArray.GetValue(7) != DBNull.Value)
                            oBookingChartDTO[i].AccomInitial = Convert.ToString(dr.ItemArray.GetValue(7));
                        if (dr.ItemArray.GetValue(8) != DBNull.Value)
                            oBookingChartDTO[i].RoomCategoryAlias = Convert.ToString(dr.ItemArray.GetValue(8));

                        if (dr.ItemArray.GetValue(9) != DBNull.Value)
                            oBookingChartDTO[i].FromDt = Convert.ToDateTime(dr.ItemArray.GetValue(9));
                        if (dr.ItemArray.GetValue(10) != DBNull.Value)
                            oBookingChartDTO[i].Todt = Convert.ToDateTime(dr.ItemArray.GetValue(10));
                        if (dr.ItemArray.GetValue(11) != DBNull.Value)
                            oBookingChartDTO[i].Reason = Convert.ToString(dr.ItemArray.GetValue(11));
                    }
                }
            }
            catch (Exception exp)
            {
                oDB = null;
                dsBookingChartData = null;
                throw exp;
            }
            finally
            {
                oDB = null;
            }
            return oBookingChartDTO;
        }

        #endregion

        #region Helper Methods
        private AccomTypeDTO[] GetRoomDetails(int RegionId, int AccomodationTypeId, int AccomodationId)
        {
            AccomTypeDTO[] objAccomodationTypeData;
            AccomodationTypeMaster oAccomTypeMaster;
            AccomodationMaster oAccomMaster;
            RoomMaster oRoomMaster;

            try
            {
                oAccomTypeMaster = new AccomodationTypeMaster();
                oAccomMaster = new AccomodationMaster();
                oRoomMaster = new RoomMaster();

                objAccomodationTypeData = oAccomTypeMaster.GetData(AccomodationTypeId);

                if (objAccomodationTypeData != null)
                {
                    for (int i = 0; i < objAccomodationTypeData.Length; i++)
                    {
                        objAccomodationTypeData[i].Accomodations = oAccomMaster.GetData(RegionId, objAccomodationTypeData[i].AccomodationTypeId, AccomodationId);
                        if (objAccomodationTypeData[i].Accomodations != null)
                        {
                            for (int j = 0; j < objAccomodationTypeData[i].Accomodations.Length; j++)
                            {
                                objAccomodationTypeData[i].Accomodations[j].RoomData = oRoomMaster.GetData(objAccomodationTypeData[i].Accomodations[j].AccomodationId);
                            }
                        }
                    }
                }
            }
            catch (Exception exp)
            {
                throw exp;
            }
            return objAccomodationTypeData;
        }

        private RoomBookingDateWiseDTO[] GetBookingDataForChart(int AccomodationTypeId, int RegionId, int AccomodationId, string RoomNo, DateTime FromDate, DateTime ToDate)
        {
            DataSet ds;
            RoomBookingDateWiseDTO[] dateWiseBookingData;
            string sProcName;
            DatabaseManager oDB;
            ds = null;
            dateWiseBookingData = null;

            try
            {
                oDB = new DatabaseManager();
                sProcName = "up_Get_BookingDataForChart";
                oDB.DbCmd = oDB.GetStoredProcCommand(sProcName);
                oDB.DbDatabase.AddInParameter(oDB.DbCmd, "@AccomodationTypeId", DbType.Int32, AccomodationTypeId);
                oDB.DbDatabase.AddInParameter(oDB.DbCmd, "@RegionId", DbType.Int32, RegionId);
                oDB.DbDatabase.AddInParameter(oDB.DbCmd, "@AccomodationId", DbType.Int32, AccomodationId);
                oDB.DbDatabase.AddInParameter(oDB.DbCmd, "@RoomNo", DbType.String, RoomNo);
                oDB.DbDatabase.AddInParameter(oDB.DbCmd, "@FromDate", DbType.Date, GF.HandleMaxMinDates(FromDate, false));
                oDB.DbDatabase.AddInParameter(oDB.DbCmd, "@ToDate", DbType.Date, GF.HandleMaxMinDates(ToDate, false));
                ds = oDB.ExecuteDataSet(oDB.DbCmd);

                dateWiseBookingData = MapDBToObject(ds);
            }
            catch (Exception exp)
            {
                oDB = null;
                ds = null;
                throw exp;
            }
            return dateWiseBookingData;
        }

        private RoomBookingDateWiseDTO[] MapDBToObject(DataSet dbData)
        {
            List<RoomBookingDateWiseDTO> dateWiseBookingDataList = new List<RoomBookingDateWiseDTO>();
            RoomBookingDateWiseDTO dateWiseBookingDTO = null;
            DataRow dr;
            try
            {
                if (dbData != null && dbData.Tables[0].Rows.Count > 0)
                {
                    for (int i = 0; i < dbData.Tables[0].Rows.Count; i++)
                    {
                        dr = dbData.Tables[0].Rows[i];
                        dateWiseBookingDTO = new RoomBookingDateWiseDTO();
                        if (dr.ItemArray.GetValue(0) != DBNull.Value)
                            dateWiseBookingDTO.AccomodationId = Convert.ToInt32(dr.ItemArray.GetValue(0));
                        if (dr.ItemArray.GetValue(1) != DBNull.Value)
                            dateWiseBookingDTO.RoomNo = Convert.ToString(dr.ItemArray.GetValue(1));
                        if (dr.ItemArray.GetValue(2) != DBNull.Value)
                            dateWiseBookingDTO.BookingId = Convert.ToInt32(dr.ItemArray.GetValue(2));
                        if (dr.ItemArray.GetValue(3) != DBNull.Value)
                            dateWiseBookingDTO.BookingCode = Convert.ToString(dr.ItemArray.GetValue(3)).Trim();
                        if (dr.ItemArray.GetValue(4) != DBNull.Value)
                            dateWiseBookingDTO.TourId = Convert.ToString(dr.ItemArray.GetValue(4));
                        if (dr.ItemArray.GetValue(5) != DBNull.Value)
                            dateWiseBookingDTO.Startdate = Convert.ToDateTime(dr.ItemArray.GetValue(5).ToString());
                        if (dr.ItemArray.GetValue(6) != DBNull.Value)
                            dateWiseBookingDTO.Enddate = Convert.ToDateTime(dr.ItemArray.GetValue(6).ToString());
                        if (dr.ItemArray.GetValue(7) != DBNull.Value)
                            dateWiseBookingDTO.Noofnights = Convert.ToInt32(dr.ItemArray.GetValue(7));
                        if (dr.ItemArray.GetValue(8) != null && dr.ItemArray.GetValue(8) != DBNull.Value)
                            dateWiseBookingDTO.BookingStatusId = Convert.ToInt32(dr.ItemArray.GetValue(8));
                        else
                            dateWiseBookingDTO.BookingStatusId = 0;
                        if (dr.ItemArray.GetValue(9) != null && dr.ItemArray.GetValue(9) != DBNull.Value)
                            dateWiseBookingDTO.AgentName = DataSecurityManager.Decrypt(Convert.ToString(dr.ItemArray.GetValue(9)));
                        if (dr.ItemArray.GetValue(15) != null && dr.ItemArray.GetValue(15) != DBNull.Value)
                            dateWiseBookingDTO.RefAgentName = DataSecurityManager.Decrypt(Convert.ToString(dr.ItemArray.GetValue(15)));
                        if (dr.ItemArray.GetValue(10) != null && dr.ItemArray.GetValue(10) != DBNull.Value)
                            dateWiseBookingDTO.BookingReference = Convert.ToString(dr.ItemArray.GetValue(10));
                        if (dr.ItemArray.GetValue(11) != null && dr.ItemArray.GetValue(11) != DBNull.Value)
                            dateWiseBookingDTO.BookingStatusType = Convert.ToString(dr.ItemArray.GetValue(11));

                        if (dr.ItemArray.GetValue(12) != null && dr.ItemArray.GetValue(12) != DBNull.Value)
                            dateWiseBookingDTO.Chartered = Convert.ToBoolean(dr.ItemArray.GetValue(12));
                        else
                            dateWiseBookingDTO.Chartered = false;

                        if (dr.ItemArray.GetValue(13) != null && dr.ItemArray.GetValue(13) != DBNull.Value)
                            dateWiseBookingDTO.NoofRooms = Convert.ToInt32(dr.ItemArray.GetValue(13));

                        if (dr.ItemArray.GetValue(14) != null && dr.ItemArray.GetValue(14) != DBNull.Value)
                            dateWiseBookingDTO.paxStaying = Convert.ToInt32(dr.ItemArray.GetValue(14));

                        dateWiseBookingDataList.Add(dateWiseBookingDTO);
                    }
                }
            }
            catch (Exception exp)
            {
                throw exp;
            }
            return dateWiseBookingDataList.ToArray();
        }

        private DataSet GetDataFromDB(string Query)
        {
            DatabaseManager oDB = new DatabaseManager();
            DataSet ds = null;
            try
            {
                oDB.DbCmd = oDB.GetSqlStringCommand(Query);
                ds = oDB.ExecuteDataSet(oDB.DbCmd);
            }
            catch (Exception exp)
            {
                oDB = null;
                ds = null;
                throw exp;
            }
            return ds;
        }
        #endregion
    }
}
