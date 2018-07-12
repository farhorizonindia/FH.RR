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
    internal class BookingWaitListHandler
    {
        DatabaseManager oDB;
        public bool AddBookingWaitList(BookingWaitListDTO[] oBookingWaitListData)
        {
            bool bActionCompleted;
            bActionCompleted = Insert(oBookingWaitListData);
            oBookingWaitListData = null;
            return bActionCompleted;
        }
        public bool UpdateBookingWaitList(BookingWaitListDTO[] oBookingWaitListData)
        {
            bool bActionCompleted;
            if (oBookingWaitListData != null && oBookingWaitListData.Length > 0)
                bActionCompleted = DeleteBookingWaitList(oBookingWaitListData[0].BookingId);
            bActionCompleted = Insert(oBookingWaitListData);
            oBookingWaitListData = null;
            return bActionCompleted;
        }
        private bool Insert(BookingWaitListDTO[] oBookingWaitListData)
        {
            string sProcName = string.Empty;
            try
            {
                if (oBookingWaitListData != null && oBookingWaitListData.Length > 0)
                {
                    if (oDB == null)
                        oDB = new DatabaseManager();

                    //sProcName = "up_Ins_BookingWaitList2";
                    sProcName = "up_Ins_BookingWaitList3";
                    for (int i = 0; i < oBookingWaitListData.Length; i++)
                    {
                        if (oBookingWaitListData[i] != null)
                        {
                            oDB.DbCmd = oDB.GetStoredProcCommand(sProcName);
                            oDB.DbDatabase.AddInParameter(oDB.DbCmd, "@iBookingId", DbType.Int32, oBookingWaitListData[i].BookingId);
                            oDB.DbDatabase.AddInParameter(oDB.DbCmd, "@iAccomId", DbType.Int32, oBookingWaitListData[i].AccomId);
                            oDB.DbDatabase.AddInParameter(oDB.DbCmd, "@iRoomCategoryId", DbType.Int32, oBookingWaitListData[i].RoomCategoryId);
                            oDB.DbDatabase.AddInParameter(oDB.DbCmd, "@iRoomTypeId", DbType.Int32, oBookingWaitListData[i].RoomTypeId);
                            oDB.DbDatabase.AddInParameter(oDB.DbCmd, "@iNo_Of_Rooms_Waitlisted", DbType.Int32, oBookingWaitListData[i].No_Of_RoomsWaitListed);
                            oDB.DbDatabase.AddInParameter(oDB.DbCmd, "@pax", DbType.Int32, oBookingWaitListData[i].paxstying);
                            oDB.ExecuteNonQuery(oDB.DbCmd);
                            oDB.DbCmd.Parameters.Clear();
                        }
                    }
                }
            }
            catch (Exception exp)
            {
                oDB = null;
                oBookingWaitListData = null;
                GF.LogError("clsBookingWaitListHandler.SaveData", exp.Message);
                return false;
            }
            finally
            {
                oDB = null;
                oBookingWaitListData = null;
            }
            return true;
        }

        public bool AllocateRoomsToWaitListedBooking(int BookingId, string RoomList, int RoomCategoryId, int RoomTypeId)
        {
            try
            {
                if (oDB == null)
                    oDB = new DatabaseManager();

                string sProcName = "up_AllocateRooms_To_WaitListedLBookings";
                oDB.DbCmd = oDB.GetStoredProcCommand(sProcName);
                oDB.DbDatabase.AddInParameter(oDB.DbCmd, "@BookingId", DbType.Int32, BookingId);
                oDB.DbDatabase.AddInParameter(oDB.DbCmd, "@RoomNoList", DbType.String, RoomList);
                oDB.DbDatabase.AddInParameter(oDB.DbCmd, "@RoomCategoryId", DbType.Int32, RoomCategoryId);
                oDB.DbDatabase.AddInParameter(oDB.DbCmd, "@RoomTypeId", DbType.Int32, RoomTypeId);
                oDB.ExecuteNonQuery(oDB.DbCmd);
                oDB.DbCmd.Parameters.Clear();
            }
            catch (Exception exp)
            {
                oDB = null;
                GF.LogError("clsBookingWaitListHandler.AllocateRoomsToWaitListedBooking", exp.Message);
                return false;
            }
            finally
            {
                oDB = null;
            }
            return true;
        }
        public bool DeleteBookingWaitList(int BookingId)
        {
            return DeleteBookingWaitList(BookingId, 0, 0, 0);
        }
        public bool DeleteBookingWaitList(BookingWaitListDTO[] oBookingWaitListData)
        {
            bool bDone = false;
            if (oBookingWaitListData != null)
            {
                for (int i = 0; i < oBookingWaitListData.Length; i++)
                {
                    bDone = DeleteBookingWaitList(oBookingWaitListData[i].BookingId, oBookingWaitListData[i].RoomCategoryId, oBookingWaitListData[i].RoomTypeId, oBookingWaitListData[i].AccomId);
                }
            }
            return bDone;
        }

        private bool DeleteBookingWaitList(int BookingId, int RoomCategoryId, int RoomTypeId, int AccomId)
        {
            if (oDB == null)
                oDB = new DatabaseManager();
            string sProcName = "up_DeleteBookingWaitlistRooms";
            try
            {
                oDB.DbCmd = oDB.GetStoredProcCommand(sProcName);
                oDB.DbDatabase.AddInParameter(oDB.DbCmd, "@BookingId", DbType.Int32, BookingId);
                oDB.DbDatabase.AddInParameter(oDB.DbCmd, "@RoomCategoryId", DbType.Int32, RoomCategoryId);
                oDB.DbDatabase.AddInParameter(oDB.DbCmd, "@RoomTypeId", DbType.Int32, RoomTypeId);
                oDB.DbDatabase.AddInParameter(oDB.DbCmd, "@AccomId", DbType.Int32, AccomId);
                oDB.ExecuteNonQuery(oDB.DbCmd);
            }
            catch (Exception exp)
            {
                oDB = null;
                GF.LogError("clsBookingWaitListHandler.DeleteBookingWaitList", exp.Message);
                return false;
            }
            return true;
        }
        public BookingWaitListDTO[] GetBookingWaitList(int BookingId)
        {
            return GetBookingWaitList(BookingId, 0);
        }
        public BookingWaitListDTO[] GetBookingWaitList(int BookingId, int RoomCategoryId)
        {
            return GetBookingWaitList(BookingId, RoomCategoryId, 0);
        }
        public BookingWaitListDTO[] GetBookingWaitList(int BookingId, int RoomCategoryId, int RoomtypeId)
        {
            DataSet dsBookingWaitListData;
            DataRow dr;
            BookingWaitListDTO[] oBookingWaitListData;
            string sProcName;
            DatabaseManager oDB;

            dsBookingWaitListData = null;
            oBookingWaitListData = null;
            try
            {
                oDB = new DatabaseManager();
                sProcName = "up_Get_BookingWaitList";
                oDB.DbCmd = oDB.GetStoredProcCommand(sProcName);
                oDB.DbDatabase.AddInParameter(oDB.DbCmd, "@iBookingId", DbType.Int32, BookingId);
                oDB.DbDatabase.AddInParameter(oDB.DbCmd, "@iRoomCategoryId", DbType.Int32, RoomCategoryId);
                oDB.DbDatabase.AddInParameter(oDB.DbCmd, "@iRoomTypeId", DbType.Int32, RoomtypeId);

                dsBookingWaitListData = oDB.ExecuteDataSet(oDB.DbCmd);
                oDB = null;
            }
            catch
            {
                oDB = null;
                dsBookingWaitListData = null;
            }

            if (dsBookingWaitListData != null)
            {
                if (dsBookingWaitListData.Tables[0].Rows.Count > 0)
                {
                    oBookingWaitListData = new BookingWaitListDTO[dsBookingWaitListData.Tables[0].Rows.Count];
                    for (int i = 0; i < dsBookingWaitListData.Tables[0].Rows.Count; i++)
                    {
                        oBookingWaitListData[i] = new BookingWaitListDTO();
                        dr = dsBookingWaitListData.Tables[0].Rows[i];
                        if (dr.ItemArray.GetValue(0) != DBNull.Value)
                            oBookingWaitListData[i].BookingId = Convert.ToInt32(dr.ItemArray.GetValue(0));
                        if (dr.ItemArray.GetValue(1) != DBNull.Value)
                            oBookingWaitListData[i].RoomCategoryId = Convert.ToInt32(dr.ItemArray.GetValue(1));
                        if (dr.ItemArray.GetValue(2) != DBNull.Value)
                            oBookingWaitListData[i].RoomCategory = Convert.ToString(dr.ItemArray.GetValue(2));
                        if (dr.ItemArray.GetValue(3) != DBNull.Value)
                            oBookingWaitListData[i].RoomTypeId = Convert.ToInt32(dr.ItemArray.GetValue(3));
                        if (dr.ItemArray.GetValue(4) != DBNull.Value)
                            oBookingWaitListData[i].RoomType = Convert.ToString(dr.ItemArray.GetValue(4));
                        if (dr.ItemArray.GetValue(5) != DBNull.Value)
                            oBookingWaitListData[i].No_Of_RoomsWaitListed = Convert.ToInt32(dr.ItemArray.GetValue(5));
                    }
                }
            }
            return oBookingWaitListData;
        }
        public RoomBookingDateWiseDTO[] GetWaitListedBookings(DateTime StartDate, DateTime EndDate, int notThisBookingId, int AccomId)
        {
            RoomBookingDateWiseDTO[] oBookingRoomDataDateWise;
            DataSet dsRoomOtherBookings;
            DataRow dr;
            string sProcName;
            DatabaseManager oDB;

            dsRoomOtherBookings = null;
            oBookingRoomDataDateWise = null;
            try
            {
                oDB = new DatabaseManager();
                sProcName = "up_Get_WaitListedBookings";
                oDB.DbCmd = oDB.GetStoredProcCommand(sProcName);
                oDB.DbDatabase.AddInParameter(oDB.DbCmd, "@StartDate", DbType.Date, StartDate);
                oDB.DbDatabase.AddInParameter(oDB.DbCmd, "@EndDate", DbType.Date, EndDate);
                oDB.DbDatabase.AddInParameter(oDB.DbCmd, "@notThisBookingId", DbType.Int32, notThisBookingId);
                oDB.DbDatabase.AddInParameter(oDB.DbCmd, "@AccomId", DbType.Int32, AccomId);
                dsRoomOtherBookings = oDB.ExecuteDataSet(oDB.DbCmd);
                oDB = null;
            }
            catch (Exception exp)
            {
                oDB = null;
                dsRoomOtherBookings = null;
                GF.LogError("clsBookingRoomHandler.GetRoomOtherBookings", exp.Message);
            }
            if (dsRoomOtherBookings != null && dsRoomOtherBookings.Tables.Count > 0)
            {
                if (dsRoomOtherBookings.Tables[0].Rows.Count > 0)
                {
                    oBookingRoomDataDateWise = new RoomBookingDateWiseDTO[dsRoomOtherBookings.Tables[0].Rows.Count];
                    for (int i = 0; i < dsRoomOtherBookings.Tables[0].Rows.Count; i++)
                    {
                        dr = dsRoomOtherBookings.Tables[0].Rows[i];
                        oBookingRoomDataDateWise[i] = new RoomBookingDateWiseDTO();
                        oBookingRoomDataDateWise[i].BookingId = Convert.ToInt32(dr.ItemArray.GetValue(0));
                        oBookingRoomDataDateWise[i].BookingCode = Convert.ToString(dr.ItemArray.GetValue(1)).Trim();
                        oBookingRoomDataDateWise[i].AccomodationId = Convert.ToInt32(dr.ItemArray.GetValue(2));
                        oBookingRoomDataDateWise[i].Startdate = Convert.ToDateTime(dr.ItemArray.GetValue(3).ToString());
                        oBookingRoomDataDateWise[i].Enddate = Convert.ToDateTime(dr.ItemArray.GetValue(4).ToString());
                        oBookingRoomDataDateWise[i].BookingReference = Convert.ToString(dr.ItemArray.GetValue(5)).Trim();
                    }
                }
            }
            return oBookingRoomDataDateWise;
        }

        public BookingWaitListDTO[] GetBlockedBookings(int BookingId)
        {
            DataSet dsBlockedBooking;
            DataRow dr;
            BookingWaitListDTO[] oBlockedBooking;
            string sProcName;
            DatabaseManager oDB;

            dsBlockedBooking = null;
            oBlockedBooking = null;
            try
            {
                oDB = new DatabaseManager();
                sProcName = "up_GetWaitlistedBookings";
                oDB.DbCmd = oDB.GetStoredProcCommand(sProcName);
                oDB.DbDatabase.AddInParameter(oDB.DbCmd, "@iBookingId", DbType.Int32, BookingId);
                dsBlockedBooking = oDB.ExecuteDataSet(oDB.DbCmd);
                oDB = null;
            }
            catch
            {
                oDB = null;
                dsBlockedBooking = null;
            }

            if (dsBlockedBooking != null)
            {
                if (dsBlockedBooking.Tables[0].Rows.Count > 0)
                {
                    oBlockedBooking = new BookingWaitListDTO[dsBlockedBooking.Tables[0].Rows.Count];
                    for (int i = 0; i < dsBlockedBooking.Tables[0].Rows.Count; i++)
                    {
                        oBlockedBooking[i] = new BookingWaitListDTO();
                        dr = dsBlockedBooking.Tables[0].Rows[i];

                        if (dr.ItemArray.GetValue(0) != DBNull.Value)
                            oBlockedBooking[i].BookingType = Convert.ToChar(dr.ItemArray.GetValue(0));
                        if (dr.ItemArray.GetValue(1) != DBNull.Value)
                            oBlockedBooking[i].BookingId = Convert.ToInt32(dr.ItemArray.GetValue(1));
                        if (dr.ItemArray.GetValue(2) != DBNull.Value)
                            oBlockedBooking[i].BookingRef = Convert.ToString(dr.ItemArray.GetValue(2));
                        if (dr.ItemArray.GetValue(3) != DBNull.Value)
                            oBlockedBooking[i].RoomCategoryId = Convert.ToInt32(dr.ItemArray.GetValue(3));
                        if (dr.ItemArray.GetValue(4) != DBNull.Value)
                            oBlockedBooking[i].RoomCategory = Convert.ToString(dr.ItemArray.GetValue(4));
                        if (dr.ItemArray.GetValue(5) != DBNull.Value)
                            oBlockedBooking[i].RoomTypeId = Convert.ToInt32(dr.ItemArray.GetValue(5));
                        if (dr.ItemArray.GetValue(6) != DBNull.Value)
                            oBlockedBooking[i].RoomType = Convert.ToString(dr.ItemArray.GetValue(6));
                        if (dr.ItemArray.GetValue(7) != DBNull.Value)
                            oBlockedBooking[i].No_Of_RoomsWaitListed = Convert.ToInt32(dr.ItemArray.GetValue(7));
                    }
                }
            }
            return oBlockedBooking;
        }
        private DataSet GetDataFromDB(string Query)
        {
            DatabaseManager oDB = new DatabaseManager();
            DataSet ds = null;
            try
            {
                oDB.DbCmd = oDB.GetSqlStringCommand(Query);
                //DataSet ds = oDB.FetchRecords("tblBookingDetails", Query);
                ds = oDB.ExecuteDataSet(oDB.DbCmd);
            }
            catch (Exception exp)
            {
                oDB = null;
                ds = null;
                GF.LogError("clsBookingWaitListMaster.GetDataFromDB", exp.Message);
            }
            return ds;
        }
    }
}
