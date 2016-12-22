using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using FarHorizon.Reservations.Common;
using FarHorizon.Reservations.Common.DataEntities.Masters;
using FarHorizon.Reservations.Common.DataEntities.Client;
using FarHorizon.Reservations.BusinessTier.BusinessLogic;
using FarHorizon.Reservations.BusinessTier.BusinessLogic.BookingEngine;
using FarHorizon.Reservations.DataBaseManager;

namespace FarHorizon.Reservations.BusinessTier.BusinessLogic.BookingEngine
{
    internal class BookingRoomHandler
    {
        public bool AddBookingRooms(BookedRooms[] oBookedRooms)
        {
            DatabaseManager oDB;
            try
            {
                oDB = new DatabaseManager();
                string sProcName = "up_Ins_BookingRoom";
                if (oBookedRooms != null)
                {                        
                    for (int i = 0; i < oBookedRooms.Length; i++)
                    {
                        if (oBookedRooms[i] != null)
                        {
                            oDB.DbCmd = oDB.GetStoredProcCommand(sProcName);
                            oDB.DbDatabase.AddInParameter(oDB.DbCmd, "@iBookingId", DbType.Int32);
                            oDB.DbDatabase.AddInParameter(oDB.DbCmd, "@iAccomId", DbType.Int32);
                            oDB.DbDatabase.AddInParameter(oDB.DbCmd, "@sRoomNo", DbType.String);
                            oDB.DbDatabase.AddInParameter(oDB.DbCmd, "@dtStartDate", DbType.DateTime);
                            oDB.DbDatabase.AddInParameter(oDB.DbCmd, "@dtEndDate", DbType.DateTime);
                            oDB.DbDatabase.AddInParameter(oDB.DbCmd, "@iPaxStaying", DbType.Int32);
                            oDB.DbDatabase.AddInParameter(oDB.DbCmd, "@bConvertTo_Double_Twin", DbType.Byte);
                            oDB.DbDatabase.AddInParameter(oDB.DbCmd, "@cRoomStatus", DbType.String);

                            oDB.DbDatabase.AddInParameter(oDB.DbCmd, "@Action", DbType.String);
                            oDB.DbDatabase.AddInParameter(oDB.DbCmd, "@Amt", DbType.Double);
                            oDB.DbDatabase.AddInParameter(oDB.DbCmd, "@PaymentId", DbType.String);
                            oDB.DbDatabase.AddInParameter(oDB.DbCmd, "@paidAmt", DbType.Double);

                            oDB.DbCmd.Parameters[0].Value = oBookedRooms[i].BookingId;
                            oDB.DbCmd.Parameters[1].Value = oBookedRooms[i].AccomodationId;
                            oDB.DbCmd.Parameters[2].Value = oBookedRooms[i].RoomNo;
                            oDB.DbCmd.Parameters[3].Value = GF.HandleMaxMinDates(oBookedRooms[i].StartDate, false);
                            oDB.DbCmd.Parameters[4].Value = GF.HandleMaxMinDates(oBookedRooms[i].EndDate, false);

                            oDB.DbCmd.Parameters[5].Value = oBookedRooms[i].PaxStaying;


                            if (oBookedRooms[i].ConvertTo_Double_Twin == true)
                                oDB.DbCmd.Parameters[6].Value = 1;
                            else if(oBookedRooms[i].ConvertTo_Double_Twin ==false)
                                oDB.DbCmd.Parameters[6].Value = 0;
                            oDB.DbCmd.Parameters[7].Value = oBookedRooms[i].RoomStatus;

                            oDB.DbCmd.Parameters[8].Value = oBookedRooms[i].action;
                            oDB.DbCmd.Parameters[9].Value = oBookedRooms[i].Price;
                            oDB.DbCmd.Parameters[10].Value = oBookedRooms[i].PaymentId;
                            oDB.DbCmd.Parameters[11].Value = oBookedRooms[i].Amount;

                            oDB.ExecuteNonQuery(oDB.DbCmd);
                            oDB.DbCmd.Parameters.Clear();
                        }
                    }
                }
            }
            catch (Exception exp)
            {
                oDB = null;
                oBookedRooms = null;
                GF.LogError("clsBookingRoomHandler.AddBookingRooms", exp.Message);
                return false;   
            }
            finally
            {
                oDB = null;
                oBookedRooms = null;
            }
            return true;
        }

        public bool AddSeriesBookingRooms1(clsSeriesBookingDTO[] SeriesBokingDTO)
        {
            DatabaseManager oDB;
            try
            {
                oDB = new DatabaseManager();
                string sProcName = "up_ins_SeriesBookingRooms";
                if (SeriesBokingDTO != null)
                {
                    for (int i = 0; i < SeriesBokingDTO.Length; i++)
                    {
                        if (SeriesBokingDTO[i] != null)
                        {
                            oDB.DbCmd = oDB.GetStoredProcCommand(sProcName);
                            oDB.DbDatabase.AddInParameter(oDB.DbCmd, "@iBookingId", DbType.Int32);
                            oDB.DbDatabase.AddInParameter(oDB.DbCmd, "@iAccomId", DbType.Int32);
                            oDB.DbDatabase.AddInParameter(oDB.DbCmd, "@iRoomCategoryID", DbType.Int32);
                            oDB.DbDatabase.AddInParameter(oDB.DbCmd, "@iRoomTypeID", DbType.Int32);
                            oDB.DbDatabase.AddInParameter(oDB.DbCmd, "@dtStartDate", DbType.DateTime);
                            oDB.DbDatabase.AddInParameter(oDB.DbCmd, "@dtEndDate", DbType.DateTime);
                            oDB.DbDatabase.AddInParameter(oDB.DbCmd, "@iNoOfRooms", DbType.Int32);

                            oDB.DbCmd.Parameters[0].Value = SeriesBokingDTO[i].BookingID;
                            oDB.DbCmd.Parameters[1].Value = SeriesBokingDTO[i].AccomodationID;
                            oDB.DbCmd.Parameters[2].Value = SeriesBokingDTO[i].RoomCategoryID;
                            oDB.DbCmd.Parameters[3].Value = SeriesBokingDTO[i].RoomTypeID;
                            oDB.DbCmd.Parameters[4].Value = GF.HandleMaxMinDates(SeriesBokingDTO[i].StartDate, false);
                            oDB.DbCmd.Parameters[5].Value = GF.HandleMaxMinDates(SeriesBokingDTO[i].EndDate, false);
                            oDB.DbCmd.Parameters[6].Value = SeriesBokingDTO[i].NoOfRooms;
                            
                            oDB.ExecuteNonQuery(oDB.DbCmd);
                            oDB.DbCmd.Parameters.Clear();
                        }
                    }
                }
            }
            catch (Exception exp)
            {
                oDB = null;
                SeriesBokingDTO = null;
                GF.LogError("clsBookingRoomHandler.AddSeriesBookingRooms", exp.Message);
                return false;
            }
            finally
            {
                oDB = null;
                SeriesBokingDTO = null;
            }
            return true;
        }

        public bool AddSeriesBookingRooms(clsSeriesBookingDTO[] SeriesBokingDTO)
        {
            DatabaseManager oDB;
            try
            {
                oDB = new DatabaseManager();
                string sProcName = "up_SaveSeriesBookingRooms";
                if (SeriesBokingDTO != null)
                {
                    for (int i = 0; i < SeriesBokingDTO.Length; i++)
                    {
                        if (SeriesBokingDTO[i] != null)
                        {
                            oDB.DbCmd = oDB.GetStoredProcCommand(sProcName);
                            oDB.DbDatabase.AddInParameter(oDB.DbCmd, "@dtStartDate", DbType.Int32);
                            oDB.DbDatabase.AddInParameter(oDB.DbCmd, "@dtEndDate", DbType.Int32);
                            oDB.DbDatabase.AddInParameter(oDB.DbCmd, "@iAcomID", DbType.Int32);
                            oDB.DbDatabase.AddInParameter(oDB.DbCmd, "@iRoomCategoryID", DbType.Int32);
                            oDB.DbDatabase.AddInParameter(oDB.DbCmd, "@iRoomTypeID", DbType.DateTime);
                            oDB.DbDatabase.AddInParameter(oDB.DbCmd, "@iRequired", DbType.DateTime);
                            oDB.DbDatabase.AddInParameter(oDB.DbCmd, "@iNoOfRooms", DbType.Int32);

                            oDB.DbCmd.Parameters[0].Value = SeriesBokingDTO[i].BookingID;
                            oDB.DbCmd.Parameters[1].Value = SeriesBokingDTO[i].AccomodationID;
                            oDB.DbCmd.Parameters[2].Value = SeriesBokingDTO[i].RoomCategoryID;
                            oDB.DbCmd.Parameters[3].Value = SeriesBokingDTO[i].RoomTypeID;
                            oDB.DbCmd.Parameters[4].Value = GF.HandleMaxMinDates(SeriesBokingDTO[i].StartDate, false);
                            oDB.DbCmd.Parameters[5].Value = GF.HandleMaxMinDates(SeriesBokingDTO[i].EndDate, false);
                            oDB.DbCmd.Parameters[6].Value = SeriesBokingDTO[i].NoOfRooms;

                            oDB.ExecuteNonQuery(oDB.DbCmd);
                            oDB.DbCmd.Parameters.Clear();
                        }
                    }
                }
            }
            catch (Exception exp)
            {
                oDB = null;
                SeriesBokingDTO = null;
                GF.LogError("clsBookingRoomHandler.AddSeriesBookingRooms", exp.Message);
                return false;
            }
            finally
            {
                oDB = null;
                SeriesBokingDTO = null;
            }
            return true;
        }
        
        public BookingRoomDTO[] GetBookingRoomDetails(int BookingId)
        {            
            return GetBookingRoomDetails(BookingId,0,"",DateTime.MinValue,DateTime.MinValue);
        }

        public BookingRoomDTO[] GetBookingRoomDetails(int BookingId, int AccomodationId)
        {            
            return GetBookingRoomDetails(BookingId,AccomodationId,"",DateTime.MinValue, DateTime.MinValue);
        }

        public BookingRoomDTO GetBookingRoomDetails(int BookingId, int AccomodationId, string RoomNo)
        {
            BookingRoomDTO[] oBRD;
            oBRD = GetBookingRoomDetails(BookingId, AccomodationId, RoomNo,DateTime.MinValue,DateTime.MinValue);
            if (oBRD != null)
            {
                if (oBRD.Length > 0)
                    return oBRD[0];
            }
            else
            {
                oBRD = null;
            }
            return null;
        }

        public BookingRoomDTO[] GetBookingRoomDetails(DateTime FromDate, DateTime ToDate)
        {            
            return GetBookingRoomDetails(0,0,"",FromDate,ToDate);
        }

        private BookingRoomDTO[] GetBookingRoomDetails(int BookingId, int AccomodationId, string RoomNo, DateTime FromDate, DateTime ToDate)
        {
            BookingRoomDTO[] oBookingRoomDTO;
            DataSet dsBookingRoomData;
            DataRow dr;
            string sProcName;
            DatabaseManager oDB;

            dsBookingRoomData = null;
            oBookingRoomDTO = null;
            try
            {
                oDB = new DatabaseManager();
                sProcName = "up_Get_BookingRoomDetails";
                oDB.DbCmd = oDB.GetStoredProcCommand(sProcName);

                oDB.DbDatabase.AddInParameter(oDB.DbCmd, "@BookingId", DbType.Int32, BookingId);
                oDB.DbDatabase.AddInParameter(oDB.DbCmd, "@AccomodationId", DbType.Int32, AccomodationId);
                oDB.DbDatabase.AddInParameter(oDB.DbCmd, "@RoomNo", DbType.String, RoomNo);

                if (FromDate != DateTime.MinValue)
                    oDB.DbDatabase.AddInParameter(oDB.DbCmd, "@FromDate", DbType.Date, FromDate);
                else
                    oDB.DbDatabase.AddInParameter(oDB.DbCmd, "@FromDate", DbType.Date, "");

                if (ToDate != DateTime.MinValue)
                    oDB.DbDatabase.AddInParameter(oDB.DbCmd, "@ToDate", DbType.Date, ToDate);
                else
                    oDB.DbDatabase.AddInParameter(oDB.DbCmd, "@ToDate", DbType.Date, "");

                dsBookingRoomData = oDB.ExecuteDataSet(oDB.DbCmd);
                oDB = null;
            }
            catch(Exception exp)
            {
                oDB = null;
                dsBookingRoomData = null;
                GF.LogError("clsBookingRoomHandler.GetBookingRoomDetails", exp.Message);
            }
            if (dsBookingRoomData != null)
            {
                oBookingRoomDTO[dsBookingRoomData.Tables[0].Rows.Count] = new BookingRoomDTO();
                for (int i = 0; i < dsBookingRoomData.Tables[0].Rows.Count; i++)
                {
                    dr = dsBookingRoomData.Tables[0].Rows[i];
                    oBookingRoomDTO[i].BookingId = Convert.ToInt32(dr.ItemArray.GetValue(0));
                    oBookingRoomDTO[i].BookingCode = Convert.ToString(dr.ItemArray.GetValue(1));
                    oBookingRoomDTO[i].AccomodationId = Convert.ToInt32(dr.ItemArray.GetValue(2));
                    oBookingRoomDTO[i].RoomNo = Convert.ToString(dr.ItemArray.GetValue(3));
                    oBookingRoomDTO[i].StartDate = Convert.ToDateTime(dr.ItemArray.GetValue(4).ToString());
                    oBookingRoomDTO[i].EndDate = Convert.ToDateTime(dr.ItemArray.GetValue(5).ToString());
                    oBookingRoomDTO[i].RoomTypeData.RoomTypeId = Convert.ToInt32(dr.ItemArray.GetValue(6));
                    oBookingRoomDTO[i].RoomTypeData.RoomType = Convert.ToString(dr.ItemArray.GetValue(7));
                    oBookingRoomDTO[i].RoomCategoryData.RoomCategoryId = Convert.ToInt32(dr.ItemArray.GetValue(8));
                    oBookingRoomDTO[i].RoomCategoryData.RoomCategory = Convert.ToString(dr.ItemArray.GetValue(9));
                }
            }
            return oBookingRoomDTO;
        }

        public RoomBookingDateWiseDTO[] GetRoomOtherBookings(DateTime StartDate, DateTime EndDate, int notThisBookingId, int AccomTypeId, int AccomId, string RoomNo)
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
                sProcName = "up_Get_RoomOtherBookings";
                oDB.DbCmd = oDB.GetStoredProcCommand(sProcName);
                oDB.DbDatabase.AddInParameter(oDB.DbCmd, "@StartDate", DbType.Date, StartDate);
                oDB.DbDatabase.AddInParameter(oDB.DbCmd, "@EndDate", DbType.Date, EndDate);
                oDB.DbDatabase.AddInParameter(oDB.DbCmd, "@CurrentBookingId", DbType.Int32, notThisBookingId);
                oDB.DbDatabase.AddInParameter(oDB.DbCmd, "@AccomTypeId", DbType.Int32, AccomTypeId);
                oDB.DbDatabase.AddInParameter(oDB.DbCmd, "@AccomId", DbType.Int32, AccomId);
                oDB.DbDatabase.AddInParameter(oDB.DbCmd, "@RoomNo", DbType.String, RoomNo);                
                dsRoomOtherBookings = oDB.ExecuteDataSet(oDB.DbCmd);
                oDB = null;
            }
            catch (Exception exp)
            {
                oDB = null;
                dsRoomOtherBookings = null;
                GF.LogError("clsBookingRoomHandler.GetRoomOtherBookings", exp.Message);
            }
            if (dsRoomOtherBookings != null && dsRoomOtherBookings.Tables.Count>0)
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
                        oBookingRoomDataDateWise[i].RoomNo = Convert.ToString(dr.ItemArray.GetValue(3)).Trim();
                        oBookingRoomDataDateWise[i].Startdate = Convert.ToDateTime(dr.ItemArray.GetValue(4).ToString());
                        oBookingRoomDataDateWise[i].Enddate = Convert.ToDateTime(dr.ItemArray.GetValue(5).ToString());
                        oBookingRoomDataDateWise[i].BookingReference = Convert.ToString(dr.ItemArray.GetValue(6)).Trim();
                    }
                }
            }
            return oBookingRoomDataDateWise;
        }                

        public bool DeleteBookingRooms(int BookingId)
        {            
            return DeleteBookingRooms(BookingId,0);
        }

        public bool DeleteBookingRooms(int BookingId, int AccomodationId)
        {            
            return DeleteBookingRooms(BookingId,AccomodationId,"");
        }

        public bool DeleteBookingRooms(int BookingId, int AccomodationId, string RoomNo)
        {            
            DatabaseManager oDB;
            try
            {
                oDB = new DatabaseManager();
                string sProcName = "up_Del_BookingRoom";
                oDB.DbCmd = oDB.GetStoredProcCommand(sProcName);
                oDB.DbDatabase.AddInParameter(oDB.DbCmd, "@iBookingId", DbType.Int32, BookingId);
                oDB.DbDatabase.AddInParameter(oDB.DbCmd, "@iAccomID", DbType.Int32, AccomodationId);
                oDB.DbDatabase.AddInParameter(oDB.DbCmd, "@sRoomNo", DbType.String, RoomNo);
                oDB.ExecuteNonQuery(oDB.DbCmd);
                oDB.DbCmd.Parameters.Clear();
            }
            catch (Exception exp)
            {
                oDB = null;                
                GF.LogError("clsBookingRoomHandler.DeleteBookings", exp.Message);
                return false;
            }
            return true;
        }

        //private bool DeleteBookingRooms(string WhereClause)
        //{
        //    string sQuery;
        //    bool deleted;
        //    clsDatabaseManager oDB;
        //    oDB = new clsDatabaseManager();
        //    try
        //    {
        //        //It is deleted only the confirmed rooms the waitlisted rooms will be deleted in the waitlist handler.
        //        sQuery = "Delete from tblBookingRoom where 1=1 and RoomStatus='B' ";
        //        //sQuery = "Delete from tblBookingRoom where 1=1 ";
        //        if (WhereClause != string.Empty)
        //            sQuery += WhereClause;

        //        oDB.DbCmd = oDB.GetSqlStringCommand(sQuery);
        //        oDB.ExecuteNonQuery(oDB.DbCmd);
        //        deleted = true;
        //    }
        //    catch (Exception exp)
        //    {
        //        GF.LogError("clsBookingRoomHandler.DeleteBookingRooms", exp.Message);
        //        deleted = false;
        //    }
        //    //deleted = oDB.ExecuteQuery(sQuery);
        //    return deleted;
        //}

        public bool UpdateBookingRooms(BookedRooms[] objBookedRooms)
        {
            if (objBookedRooms !=null && objBookedRooms.Length >0 && objBookedRooms[0] != null)
            {
                DeleteBookingRooms(objBookedRooms[0].BookingId);
                return AddBookingRooms(objBookedRooms);
            }
            else
                return false;
        }

        public bool DeleteRemovedRoomCategoryAndType(BookedRooms[] TotallyRemovedRCRT)
        {
            DatabaseManager oDB;
            try
            {
                oDB = new DatabaseManager();
                string sProcName = "up_DeleteRemovedRoomCategoryAndType";
                for (int i = 0; i < TotallyRemovedRCRT.Length; i++)
                {
                    oDB.DbCmd = oDB.GetStoredProcCommand(sProcName);
                    oDB.DbDatabase.AddInParameter(oDB.DbCmd, "@BookingId", DbType.Int32,TotallyRemovedRCRT[i].BookingId);
                    oDB.DbDatabase.AddInParameter(oDB.DbCmd, "@AccomID", DbType.Int32,TotallyRemovedRCRT[i].AccomodationId);
                    oDB.DbDatabase.AddInParameter(oDB.DbCmd, "@RoomCategory", DbType.String,TotallyRemovedRCRT[i].RoomCategory);
                    oDB.DbDatabase.AddInParameter(oDB.DbCmd, "@RoomType", DbType.String,TotallyRemovedRCRT[i].RoomType);
                    oDB.ExecuteNonQuery(oDB.DbCmd);
                    oDB.DbCmd.Parameters.Clear();
                }
            }
            catch (Exception exp)
            {
                oDB = null;
                //oRoomsDeletion = null;
                GF.LogError("clsBookingRoomHandler.DeleteBookings", exp.Message);
                return false;
            }
            return true;
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
                GF.LogError("clsBookingRoomHandler.GetDataFromDB", exp.Message);
            }
            return ds;
        }

        public AvailableRoomNos[] GetAvailableRoomNos(int iRoomTypeID, DateTime dtStartDate, int iAccomID)
        {
            DatabaseManager oDB;
            AvailableRoomNos[] oAvailableRoomNos = null;
            try
            {
                oDB = new DatabaseManager();
                string sProcName = "";
                oDB = new DatabaseManager();
                sProcName = "up_GetAvailableRoomNos";
                oDB.DbCmd = oDB.GetStoredProcCommand(sProcName);

                oDB.DbDatabase.AddInParameter(oDB.DbCmd, "@iRoomTypeID", DbType.Int32, iRoomTypeID);
                oDB.DbDatabase.AddInParameter(oDB.DbCmd, "@dtStartDate", DbType.Date, dtStartDate);
                oDB.DbDatabase.AddInParameter(oDB.DbCmd, "@iAccomID", DbType.Int32, iAccomID);

                DataSet dsAvailableRoomNos = oDB.ExecuteDataSet(oDB.DbCmd);
                if (dsAvailableRoomNos != null)
                {
                    if (dsAvailableRoomNos.Tables[0].Rows.Count > 0)
                    {
                        oAvailableRoomNos = new AvailableRoomNos[dsAvailableRoomNos.Tables[0].Rows.Count];
                        for (int i = 0; i < oAvailableRoomNos.Length; i++)
                        {
                            oAvailableRoomNos[i] = new AvailableRoomNos();
                            oAvailableRoomNos[i].RoomTypeID = Convert.ToInt32(dsAvailableRoomNos.Tables[0].Rows[i][0].ToString());
                            oAvailableRoomNos[i].RoomType = Convert.ToString(dsAvailableRoomNos.Tables[0].Rows[i][1].ToString());
                            oAvailableRoomNos[i].RoomNo = Convert.ToInt32(dsAvailableRoomNos.Tables[0].Rows[i][2].ToString());
                        }
                    }
                }
            }
            catch (Exception e)
            {
                throw e;
            }
            return oAvailableRoomNos;
        }

        public AvailableRoomNos[] GetAvailableRoomNos(DateTime dtStartDate, int iAccomID)
        {
            return GetAvailableRoomNos(0, dtStartDate, iAccomID);
        }

        public BookedRooms[] GetAllRooms(DateTime dtStartDate, DateTime EndDate, int iAccomID,int BookingId)
        {
            DatabaseManager oDB;
            BookedRooms[] oTotalRooms = null;
            DateTime dt;
            
            try
            {             
                oDB = new DatabaseManager();
                string sProcName = "";
                oDB = new DatabaseManager();
                sProcName = "up_GetAllRooms";
                oDB.DbCmd = oDB.GetStoredProcCommand(sProcName);
                oDB.DbDatabase.AddInParameter(oDB.DbCmd, "@dtStartDate", DbType.Date, dtStartDate.ToString("yyyy-MM-dd"));
                oDB.DbDatabase.AddInParameter(oDB.DbCmd, "@dtEndDate", DbType.Date, EndDate.ToString("yyyy-MM-dd"));
                oDB.DbDatabase.AddInParameter(oDB.DbCmd, "@iAccomID", DbType.Int32, iAccomID);

                DataSet dsRooms = oDB.ExecuteDataSet(oDB.DbCmd);
                if (dsRooms != null)
                {
                    if (dsRooms.Tables[0].Rows.Count > 0)
                    {
                        oTotalRooms = new BookedRooms[dsRooms.Tables[0].Rows.Count];
                        for (int i = 0; i < oTotalRooms.Length; i++)
                        {
                            #region Get All Rooms
                            oTotalRooms[i] = new BookedRooms();
                            oTotalRooms[i].RoomCategoryId = Convert.ToInt32(dsRooms.Tables[0].Rows[i][0].ToString());
                            oTotalRooms[i].RoomCategory = Convert.ToString(dsRooms.Tables[0].Rows[i][1].ToString());
                            oTotalRooms[i].RoomTypeId = Convert.ToInt32(dsRooms.Tables[0].Rows[i][2].ToString());
                            oTotalRooms[i].RoomType = Convert.ToString(dsRooms.Tables[0].Rows[i][3].ToString());
                            oTotalRooms[i].RoomNo = Convert.ToString(dsRooms.Tables[0].Rows[i][4].ToString());
                            oTotalRooms[i].AccomodationId = Convert.ToInt32(dsRooms.Tables[0].Rows[i][5].ToString());
                            oTotalRooms[i].NoOfBeds = Convert.ToInt32(dsRooms.Tables[0].Rows[i][6].ToString());
                            oTotalRooms[i].BookingId = Convert.ToInt32(dsRooms.Tables[0].Rows[i][7].ToString());
                            oTotalRooms[i].DefaultNoOfBeds = Convert.ToInt32(dsRooms.Tables[0].Rows[i][8].ToString());
                            oTotalRooms[i].Status = Convert.ToString(dsRooms.Tables[0].Rows[i][9].ToString());
                            if (dsRooms.Tables[0].Rows[i][10] == DBNull.Value)
                                oTotalRooms[i].PaxStaying = 0;
                            else
                                oTotalRooms[i].PaxStaying = Convert.ToInt32(dsRooms.Tables[0].Rows[i][10].ToString());
                            if (dsRooms.Tables[0].Rows[i][11] == DBNull.Value)
                                oTotalRooms[i].ConvertTo_Double_Twin = false;
                            else if (Convert.ToInt32(dsRooms.Tables[0].Rows[i][11]) == 1)
                                oTotalRooms[i].ConvertTo_Double_Twin = true;
                            else if (Convert.ToInt32(dsRooms.Tables[0].Rows[i][11]) == 0)
                                oTotalRooms[i].ConvertTo_Double_Twin = false;

                            if (dsRooms.Tables[0].Rows[i][12] != DBNull.Value)
                                oTotalRooms[i].Convertable = Convert.ToBoolean(dsRooms.Tables[0].Rows[i][12]);
                            //if (dsRooms.Tables[0].Rows[i][13] != DBNull.Value)
                            oTotalRooms[i].RoomStatus = dsRooms.Tables[0].Rows[i][13] != DBNull.Value ? Convert.ToChar(dsRooms.Tables[0].Rows[i][13]) : Constants.AVAILABLE;

                            if (dsRooms.Tables[0].Rows[i][15] != DBNull.Value)
                            {
                                DateTime.TryParse(dsRooms.Tables[0].Rows[i][15].ToString(), out dt);
                                oTotalRooms[i].StartDate = dt;
                            }
                            if (dsRooms.Tables[0].Rows[i][16] != DBNull.Value)
                            {
                             DateTime.TryParse(dsRooms.Tables[0].Rows[i][16].ToString(), out dt);
                             oTotalRooms[i].EndDate = dt;
                            }

                            if (dsRooms.Tables[0].Rows[i][17] == DBNull.Value)
                                oTotalRooms[i].Price = 0;
                            else
                                oTotalRooms[i].Price = Convert.ToDouble(dsRooms.Tables[0].Rows[i][17].ToString());

                            if (dsRooms.Tables[0].Rows[i][18] == DBNull.Value)
                                oTotalRooms[i].Amount = 0;
                            else
                                oTotalRooms[i].Amount = Convert.ToDouble(dsRooms.Tables[0].Rows[i][18].ToString());

                          
                           


                            oTotalRooms[i].PrevBookingId = oTotalRooms[i].BookingId;
                            oTotalRooms[i].PrevPaxStaying = oTotalRooms[i].PaxStaying;
                            oTotalRooms[i].PrevRoomStatus = oTotalRooms[i].RoomStatus;

                            oTotalRooms[i].OriginalBookingId = oTotalRooms[i].BookingId;
                            oTotalRooms[i].OriginalPaxStaying = oTotalRooms[i].PaxStaying;
                            oTotalRooms[i].OriginalRoomStatus = oTotalRooms[i].RoomStatus;

                            


                            #endregion Get All Rooms
                        }
                        oTotalRooms = SetWaitListRooms(oTotalRooms, BookingId);
                    }
                }               

            }
            catch (Exception exp)
            {
                GF.LogError("clsBookingRoomHandler.GetAllRooms", exp.Message);
            }
            return oTotalRooms;
        }

        public BookedRooms[] GetAllRoomspgload(DateTime dtStartDate, DateTime EndDate, int iAccomID, int BookingId)
        {
            DatabaseManager oDB;
            BookedRooms[] oTotalRooms = null;
            DateTime dt;

            try
            {
                oDB = new DatabaseManager();
                string sProcName = "";
                oDB = new DatabaseManager();
                sProcName = "up_GetAllRoomsPgload";
                oDB.DbCmd = oDB.GetStoredProcCommand(sProcName);
                oDB.DbDatabase.AddInParameter(oDB.DbCmd, "@dtStartDate", DbType.Date, dtStartDate.ToString("yyyy-MM-dd"));
                oDB.DbDatabase.AddInParameter(oDB.DbCmd, "@dtEndDate", DbType.Date, EndDate.ToString("yyyy-MM-dd"));
                oDB.DbDatabase.AddInParameter(oDB.DbCmd, "@iAccomID", DbType.Int32, iAccomID);

                DataSet dsRooms = oDB.ExecuteDataSet(oDB.DbCmd);
                if (dsRooms != null)
                {
                    if (dsRooms.Tables[0].Rows.Count > 0)
                    {
                        oTotalRooms = new BookedRooms[dsRooms.Tables[0].Rows.Count];
                        for (int i = 0; i < oTotalRooms.Length; i++)
                        {
                            #region Get All Rooms
                            oTotalRooms[i] = new BookedRooms();
                            oTotalRooms[i].RoomCategoryId = Convert.ToInt32(dsRooms.Tables[0].Rows[i][0].ToString());
                            oTotalRooms[i].RoomCategory = Convert.ToString(dsRooms.Tables[0].Rows[i][1].ToString());
                            oTotalRooms[i].RoomTypeId = Convert.ToInt32(dsRooms.Tables[0].Rows[i][2].ToString());
                            oTotalRooms[i].RoomType = Convert.ToString(dsRooms.Tables[0].Rows[i][3].ToString());
                            oTotalRooms[i].RoomNo = Convert.ToString(dsRooms.Tables[0].Rows[i][4].ToString());
                            oTotalRooms[i].AccomodationId = Convert.ToInt32(dsRooms.Tables[0].Rows[i][5].ToString());
                            oTotalRooms[i].NoOfBeds = Convert.ToInt32(dsRooms.Tables[0].Rows[i][6].ToString());
                            oTotalRooms[i].BookingId = Convert.ToInt32(dsRooms.Tables[0].Rows[i][7].ToString());
                            oTotalRooms[i].DefaultNoOfBeds = Convert.ToInt32(dsRooms.Tables[0].Rows[i][8].ToString());
                            oTotalRooms[i].Status = Convert.ToString(dsRooms.Tables[0].Rows[i][9].ToString());
                            if (dsRooms.Tables[0].Rows[i][10] == DBNull.Value)
                                oTotalRooms[i].PaxStaying = 0;
                            else
                                oTotalRooms[i].PaxStaying = Convert.ToInt32(dsRooms.Tables[0].Rows[i][10].ToString());
                            if (dsRooms.Tables[0].Rows[i][11] == DBNull.Value)
                                oTotalRooms[i].ConvertTo_Double_Twin = false;
                            else if (Convert.ToInt32(dsRooms.Tables[0].Rows[i][11]) == 1)
                                oTotalRooms[i].ConvertTo_Double_Twin = true;
                            else if (Convert.ToInt32(dsRooms.Tables[0].Rows[i][11]) == 0)
                                oTotalRooms[i].ConvertTo_Double_Twin = false;

                            if (dsRooms.Tables[0].Rows[i][12] != DBNull.Value)
                                oTotalRooms[i].Convertable = Convert.ToBoolean(dsRooms.Tables[0].Rows[i][12]);
                            //if (dsRooms.Tables[0].Rows[i][13] != DBNull.Value)
                            oTotalRooms[i].RoomStatus = dsRooms.Tables[0].Rows[i][13] != DBNull.Value ? Convert.ToChar(dsRooms.Tables[0].Rows[i][13]) : Constants.AVAILABLE;

                            if (dsRooms.Tables[0].Rows[i][15] != DBNull.Value)
                            {
                                DateTime.TryParse(dsRooms.Tables[0].Rows[i][15].ToString(), out dt);
                                oTotalRooms[i].StartDate = dt;
                            }
                            if (dsRooms.Tables[0].Rows[i][16] != DBNull.Value)
                            {
                                DateTime.TryParse(dsRooms.Tables[0].Rows[i][16].ToString(), out dt);
                                oTotalRooms[i].EndDate = dt;
                            }

                            oTotalRooms[i].PrevBookingId = oTotalRooms[i].BookingId;
                            oTotalRooms[i].PrevPaxStaying = oTotalRooms[i].PaxStaying;
                            oTotalRooms[i].PrevRoomStatus = oTotalRooms[i].RoomStatus;

                            oTotalRooms[i].OriginalBookingId = oTotalRooms[i].BookingId;
                            oTotalRooms[i].OriginalPaxStaying = oTotalRooms[i].PaxStaying;
                            oTotalRooms[i].OriginalRoomStatus = oTotalRooms[i].RoomStatus;




                            #endregion Get All Rooms
                        }
                        oTotalRooms = SetWaitListRooms(oTotalRooms, BookingId);
                    }
                }

            }
            catch (Exception exp)
            {
                GF.LogError("clsBookingRoomHandler.GetAllRooms", exp.Message);
            }
            return oTotalRooms;
        }

        private BookedRooms[] SetWaitListRooms(BookedRooms[] oAllRooms, int BookingId)
        {
            BookingWaitListDTO[] oBookingWaitListData = null;
            BookingWaitListHandler oBookingWaitListHandler = null;
            int j=0, iWLRCounter = 0;
            if (BookingId != 0)
            {
                oBookingWaitListHandler = new BookingWaitListHandler();
                oBookingWaitListData = oBookingWaitListHandler.GetBookingWaitList(BookingId);
            }
            if (oBookingWaitListData != null)
            {
                for (j = 0; j < oBookingWaitListData.Length; j++)
                {
                    iWLRCounter = 0;
                    for (int k = 0; k < oAllRooms.Length; k++)
                    {
                        if (oBookingWaitListData[j].RoomCategoryId == oAllRooms[k].RoomCategoryId && oBookingWaitListData[j].RoomTypeId == oAllRooms[k].RoomTypeId)
                        {
                            if (oAllRooms[k].BookingId != BookingId && oAllRooms[k].BookingId != 0)
                            {
                                if (iWLRCounter < oBookingWaitListData[j].No_Of_RoomsWaitListed)
                                {
                                    oAllRooms[k].RoomStatus = Constants.WAITLISTED;
                                    iWLRCounter++;
                                }
                                if (iWLRCounter == oBookingWaitListData[j].No_Of_RoomsWaitListed)
                                    break;
                            }
                        }
                    }
                }                
            }
            return oAllRooms;
        }
    }   
}
