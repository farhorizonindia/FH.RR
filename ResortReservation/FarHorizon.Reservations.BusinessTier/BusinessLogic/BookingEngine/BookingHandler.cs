using System;
using System.Collections.Generic;
using System.Text;
using FarHorizon.Reservations.DataBaseManager;
using System.Data;
using System.Data.SqlClient;
using FarHorizon.Reservations.BusinessTier.BusinessLogic;
using FarHorizon.Reservations.Common;
using FarHorizon.Reservations.Common.DataEntities.Masters;
using FarHorizon.Reservations.Common.DataEntities.Client;
using FarHorizon.Reservations.Common.DataEntities.InputOutput;
using FarHorizon.DataSecurity;

namespace FarHorizon.Reservations.BusinessTier.BusinessLogic.BookingEngine
{
    internal class BookingHandler
    {
        DatabaseManager oDB;

        #region IBookingManager Members

        public bool AddBooking(BookingDTO oBookingData, BookedRooms[] oBookedRooms, BookingWaitListDTO[] oBookingWaitListData, out int BookingId)
        {
            int iBKID = 0;
            bool bActionCompleted = false;
            BookingRoomHandler bookingRoomHandler;
            BookingWaitListHandler bookingWaitListHandler;

            bActionCompleted = AddBooking(oBookingData, out iBKID);
            if (bActionCompleted == true)
            {
                if (oBookedRooms != null)
                {
                    for (int i = 0; i < oBookedRooms.Length; i++)
                    {
                        if (oBookedRooms[i] != null)
                            oBookedRooms[i].BookingId = iBKID;
                    }
                }

                if (oBookingWaitListData != null)
                {
                    for (int i = 0; i < oBookingWaitListData.Length; i++)
                    {
                        if (oBookingWaitListData[i] != null)
                            oBookingWaitListData[i].BookingId = iBKID;
                    }
                }
                if (oBookedRooms != null)
                {
                    bookingRoomHandler = new BookingRoomHandler();
                    bActionCompleted = bookingRoomHandler.AddBookingRooms(oBookedRooms);
                }
                if (bActionCompleted == true)
                {
                    if (oBookingWaitListData != null)
                    {
                        bookingWaitListHandler = new BookingWaitListHandler();
                        bActionCompleted = bookingWaitListHandler.AddBookingWaitList(oBookingWaitListData);
                    }
                }
            }
            else
            {
                iBKID = 0;
            }
            BookingId = iBKID;
            return bActionCompleted;

        }

        private bool AddBooking(BookingDTO objBooking, out int BookingId)
        {
            int iBookingID;

            try
            {
                if (oDB == null)
                    oDB = new DatabaseManager();
                string sProcName = "up_Ins_Booking";
                oDB.DbCmd = oDB.GetStoredProcCommand(sProcName);
                oDB.DbDatabase.AddInParameter(oDB.DbCmd, "@sBookingRef", DbType.String, objBooking.BookingReference.Trim());
                oDB.DbDatabase.AddInParameter(oDB.DbCmd, "@dtStartDate", DbType.DateTime, objBooking.StartDate);
                oDB.DbDatabase.AddInParameter(oDB.DbCmd, "@dtEndDate", DbType.DateTime, objBooking.EndDate);
                oDB.DbDatabase.AddInParameter(oDB.DbCmd, "@iAccomTypeId", DbType.Int32, objBooking.AccomodationTypeId);
                oDB.DbDatabase.AddInParameter(oDB.DbCmd, "@iAccomId", DbType.Int32, objBooking.AccomodationId);
                oDB.DbDatabase.AddInParameter(oDB.DbCmd, "@iAgentId", DbType.Int32, objBooking.AgentId);
                oDB.DbDatabase.AddInParameter(oDB.DbCmd, "@iNights", DbType.Int32, objBooking.NoOfNights);
                oDB.DbDatabase.AddInParameter(oDB.DbCmd, "@iPersons", DbType.Int32, objBooking.NoOfPersons);
                oDB.DbDatabase.AddInParameter(oDB.DbCmd, "@BookingStatusId", DbType.Int32, objBooking.BookingStatusId);
                oDB.DbDatabase.AddInParameter(oDB.DbCmd, "@SeriesId", DbType.Int32, objBooking.SeriesId);
                oDB.DbDatabase.AddInParameter(oDB.DbCmd, "@proposedBooking", DbType.Boolean, objBooking.ProposedBooking);
                oDB.DbDatabase.AddInParameter(oDB.DbCmd, "@chartered", DbType.Boolean, objBooking.Chartered);


                iBookingID = Convert.ToInt32(oDB.ExecuteScalar(oDB.DbCmd));
                BookingId = iBookingID;
            }
            catch (Exception exp)
            {
                objBooking = null;
                oDB = null;
                GF.LogError("clsBookingHandler.AddBooking", exp.Message);
                BookingId = 0;
                return false;
            }
            finally
            {
                objBooking = null;
                oDB = null;
            }
            return true;
        }

        public int GetBookingReferenceCount(BookingDTO objBooking)
        {
            int iBookingReferenceCount;

            try
            {
                if (oDB == null)
                    oDB = new DatabaseManager();
                string sProcName = "up_GetBookingReferenceCount";
                oDB.DbCmd = oDB.GetStoredProcCommand(sProcName);
                oDB.DbDatabase.AddInParameter(oDB.DbCmd, "@sBookingRef", DbType.String, objBooking.BookingReference);
                oDB.DbDatabase.AddInParameter(oDB.DbCmd, "@dtStartDate", DbType.DateTime, objBooking.StartDate);
                oDB.DbDatabase.AddInParameter(oDB.DbCmd, "@dtEndDate", DbType.DateTime, objBooking.EndDate.ToString());
                oDB.DbDatabase.AddInParameter(oDB.DbCmd, "@iAccomTypeId", DbType.Int32, objBooking.AccomodationTypeId);
                oDB.DbDatabase.AddInParameter(oDB.DbCmd, "@iAccomId", DbType.Int32, objBooking.AccomodationId);
                oDB.DbDatabase.AddInParameter(oDB.DbCmd, "@iBookingId", DbType.Int32, objBooking.BookingId);

                iBookingReferenceCount = Convert.ToInt32(oDB.ExecuteScalar(oDB.DbCmd));
            }
            catch (Exception exp)
            {
                objBooking = null;
                oDB = null;
                GF.LogError("clsBookingHandler.GetBookingReferenceCount", exp.Message);
                iBookingReferenceCount = 0;
                return 0;
            }
            finally
            {
                objBooking = null;
                oDB = null;
            }
            return iBookingReferenceCount;
        }

        /// <summary>
        /// This method should not be here, it is incorrectly placed
        /// </summary>
        /// <param name="SeriesBookingDTO"></param>
        /// <param name="BookingId"></param>
        /// <returns></returns>
        private bool AddSeriesBooking(clsSeriesBookingDTO SeriesBookingDTO, out int BookingId)
        {
            int iBookingID;
            try
            {
                if (oDB == null)
                    oDB = new DatabaseManager();
                string sProcName = "up_Ins_SeriesBooking";
                oDB.DbCmd = oDB.GetStoredProcCommand(sProcName);
                oDB.DbDatabase.AddInParameter(oDB.DbCmd, "@sSeriesName", DbType.String, SeriesBookingDTO.SeriesName);
                oDB.DbDatabase.AddInParameter(oDB.DbCmd, "@iAccomTypeId", DbType.Int32, SeriesBookingDTO.AccomTypeID);
                oDB.DbDatabase.AddInParameter(oDB.DbCmd, "@iAccomId", DbType.Int32, SeriesBookingDTO.AccomodationID);
                oDB.DbDatabase.AddInParameter(oDB.DbCmd, "@iNights", DbType.Int32, SeriesBookingDTO.NoOfNights);
                oDB.DbDatabase.AddInParameter(oDB.DbCmd, "@iGap", DbType.Int32, SeriesBookingDTO.Gap);
                oDB.DbDatabase.AddInParameter(oDB.DbCmd, "@iNoOfDepartures", DbType.Int32, SeriesBookingDTO.NoOfDepartures);
                oDB.DbDatabase.AddInParameter(oDB.DbCmd, "@dtStartDate", DbType.DateTime, SeriesBookingDTO.StartDate);

                iBookingID = Convert.ToInt32(oDB.ExecuteScalar(oDB.DbCmd));
                BookingId = iBookingID;
            }
            catch (Exception exp)
            {
                SeriesBookingDTO = null;
                oDB = null;
                GF.LogError("clsBookingHandler.AddSeriesBooking", exp.Message);
                BookingId = 0;
                return false;
            }
            finally
            {
                SeriesBookingDTO = null;
                oDB = null;
            }
            return true;
        }

        public bool UpdateBooking(BookingDTO oBookingData, BookedRooms[] oBookedRooms, BookingWaitListDTO[] oBookingWaitListData, BookedRooms[] TotallyRemovedRoomCategoryAndType)
        {
            bool bActionCompleted = false;
            //int iActionFinished = 0;
            if (oBookingData != null)
                bActionCompleted = UpdateBooking(oBookingData);
            if (bActionCompleted == true)
            {
                if (oBookedRooms != null)
                    bActionCompleted = UpdateBookingRooms(oBookedRooms);
                if (oBookingWaitListData != null && oBookingWaitListData.Length > 0)
                    bActionCompleted = UpdateWaitListedRooms(oBookingWaitListData);
                else if (oBookingWaitListData == null || oBookingWaitListData.Length == 0)
                    bActionCompleted = DeleteBookingWaitList(oBookingData.BookingId);
            }
            return bActionCompleted;
        }

        public bool UpdateBooking(BookingDTO objBooking)
        {
            try
            {
                if (oDB == null)
                    oDB = new DatabaseManager();
                string sProcName = "up_Update_Booking";
                oDB.DbCmd = oDB.GetStoredProcCommand(sProcName);

                oDB.DbDatabase.AddInParameter(oDB.DbCmd, "@iBookingID", DbType.Int32, objBooking.BookingId);
                oDB.DbDatabase.AddInParameter(oDB.DbCmd, "@sBookingCode", DbType.String, objBooking.BookingCode);
                oDB.DbDatabase.AddInParameter(oDB.DbCmd, "@sBookingRef", DbType.String, objBooking.BookingReference);
                oDB.DbDatabase.AddInParameter(oDB.DbCmd, "@dtStartDate", DbType.DateTime, objBooking.StartDate);
                oDB.DbDatabase.AddInParameter(oDB.DbCmd, "@dtEndDate", DbType.DateTime, objBooking.EndDate);
                oDB.DbDatabase.AddInParameter(oDB.DbCmd, "@iAccomTypeId", DbType.Int32, objBooking.AccomodationTypeId);
                oDB.DbDatabase.AddInParameter(oDB.DbCmd, "@iAccomId", DbType.Int32, objBooking.AccomodationId);
                oDB.DbDatabase.AddInParameter(oDB.DbCmd, "@iNights", DbType.Int32, objBooking.NoOfNights);
                oDB.DbDatabase.AddInParameter(oDB.DbCmd, "@iPersons", DbType.Int32, objBooking.NoOfPersons);
                oDB.DbDatabase.AddInParameter(oDB.DbCmd, "@iAgentId", DbType.Int32, objBooking.AgentId);
                oDB.DbDatabase.AddInParameter(oDB.DbCmd, "@iBookingStatusId", DbType.Int32, objBooking.BookingStatusId);
                oDB.DbDatabase.AddInParameter(oDB.DbCmd, "@proposedBooking", DbType.Boolean, objBooking.ProposedBooking);
                oDB.DbDatabase.AddInParameter(oDB.DbCmd, "@charteredbooking", DbType.Boolean, objBooking.Chartered);
                oDB.ExecuteNonQuery(oDB.DbCmd);

            }
            catch (Exception exp)
            {
                objBooking = null;
                oDB = null;
                GF.LogError("clsBookingHandler.AddBooking", exp.Message);
            }
            finally
            {
                objBooking = null;
                oDB = null;
            }
            return true;
        }

        private bool UpdateBookingRooms(BookedRooms[] oBookingRoomData)
        {
            try
            {
                BookingRoomHandler oBRoomHandler = new BookingRoomHandler();
                if (oBRoomHandler == null)
                    oBRoomHandler = new BookingRoomHandler();
                return oBRoomHandler.UpdateBookingRooms(oBookingRoomData);
            }
            catch (Exception exp)
            {
                throw exp;
            }
        }

        private bool UpdateWaitListedRooms(BookingWaitListDTO[] oBookingWaitListData)
        {
            try
            {
                BookingWaitListHandler oBookingWaitListHandler = new BookingWaitListHandler();
                if (oBookingWaitListHandler == null)
                    oBookingWaitListHandler = new BookingWaitListHandler();
                return oBookingWaitListHandler.UpdateBookingWaitList(oBookingWaitListData);
            }
            catch (Exception exp)
            {
                throw exp;
            }
        }

        private bool DeleteBookingWaitList(int bookingId)
        {
            try
            {
                BookingWaitListHandler bookingWaitListHandler = new BookingWaitListHandler();
                return bookingWaitListHandler.DeleteBookingWaitList(bookingId);
            }
            catch (Exception exp)
            {
                throw exp;
            }
        }

        public bool DeleteBooking(int BookingId)
        {
            try
            {
                if (oDB == null)
                    oDB = new DatabaseManager();
                string sProcName = "up_Del_Booking";
                oDB.DbCmd = oDB.GetStoredProcCommand(sProcName);
                oDB.DbDatabase.AddInParameter(oDB.DbCmd, "@iBookingID", DbType.Int32, BookingId);
                oDB.ExecuteNonQuery(oDB.DbCmd);
            }
            catch (Exception exp)
            {
                oDB = null;
                GF.LogError("clsBookingHandler.DeleteBooking", exp.Message);
                return false;
            }
            finally
            {
                oDB = null;
            }
            return true;
        }

        public bool CancelBooking(int BookingId)
        {
            try
            {
                if (oDB == null)
                    oDB = new DatabaseManager();
                string sProcName = "up_Booking_Cancel";
                oDB.DbCmd = oDB.GetStoredProcCommand(sProcName);
                oDB.DbDatabase.AddInParameter(oDB.DbCmd, "@iBookingID", DbType.Int32, BookingId);
                oDB.ExecuteNonQuery(oDB.DbCmd);
            }
            catch (Exception exp)
            {
                oDB = null;
                GF.LogError("clsBookingHandler.CancelBooking", exp.Message);
                return false;
            }
            finally
            {
                oDB = null;
            }
            return true;
        }

        public BookingDTO GetBookingDetails(int BookingId)
        {
            BookingDTO[] oBookingData;
            string WhereClause = ""; ;
            if (BookingId != 0)
            {
                WhereClause += " and BookingId = " + BookingId;
            }
            oBookingData = GetBookingDetails(WhereClause);
            if (oBookingData != null)
            {
                if (oBookingData.Length > 0)
                    return oBookingData[0];
                else
                {
                    return null;
                }
            }
            else
                return null;
        }

        public DataSet CheckifCharteredBookingExists(DateTime frm, DateTime to)
        {
            try
            {
                if (oDB == null)
                    oDB = new DatabaseManager();
                string sProcName = "CheckifCharteredbooking";
                oDB.DbCmd = oDB.GetStoredProcCommand(sProcName);
                oDB.DbDatabase.AddInParameter(oDB.DbCmd, "@fromdt", DbType.Date, frm);
                oDB.DbDatabase.AddInParameter(oDB.DbCmd, "@todt", DbType.Date, to);
                DataSet dtdt = oDB.ExecuteDataSet(oDB.DbCmd);
            }
            catch (Exception exp)
            {
                oDB = null;
                //  GF.LogError("clsBookingHandler.CancelBooking", exp.Message);
                return null;
            }
            finally
            {
                oDB = null;
            }
            return null;
        }

        public BookingDTO[] GetConfirmMail(int BookingId)
        {
            BookingDTO[] oBookingData;

            oBookingData = GetConfirmMailDetails(BookingId);
            if (oBookingData != null)
            {
                if (oBookingData != null)
                    return oBookingData;
                else
                {
                    return null;
                }
            }
            else
                return null;
        }

        public BookingDTO[] GetBookingDetails(DateTime FromDate, DateTime ToDate)
        {
            string WhereClause = ""; ;
            if (FromDate != DateTime.MinValue && ToDate != DateTime.MaxValue)
            {
                WhereClause += " and StartDate between '" + FromDate.Year + "-" + FromDate.Month + "-" + FromDate.Day + "'" +
                            " and '" + ToDate.Year + "-" + ToDate.Month + "-" + ToDate.Day + "'";
            }
            return GetBookingDetails(WhereClause);
        }

        public BookingDTO[] GetBookingDetails()
        {
            string WhereClause = ""; ;
            return GetBookingDetails(WhereClause);
        }

        private BookingDTO[] GetBookingDetails(string WhereClause)
        {
            DataSet dsBookingData;
            DataRow dr;
            BookingDTO[] oBookingData;
            string sProcName;
            DatabaseManager oDB;

            dsBookingData = null;
            oBookingData = null;

            try
            {
                oDB = new DatabaseManager();
                sProcName = "up_Get_BookingDetails";
                oDB.DbCmd = oDB.GetStoredProcCommand(sProcName);
                oDB.DbDatabase.AddInParameter(oDB.DbCmd, "@sWhereClause", DbType.String, WhereClause); //Max. 2000
                dsBookingData = oDB.ExecuteDataSet(oDB.DbCmd);
            }
            catch (Exception exp)
            {
                oDB = null;
                dsBookingData = null;
                GF.LogError("clsBookingHandler.GetBookingDetails", exp.Message);
            }

            if (dsBookingData != null)
            {
                //oBookingData[dsBookingData.Tables[0].Rows.Count] = new clsBookingData();
                oBookingData = new BookingDTO[dsBookingData.Tables[0].Rows.Count];
                for (int i = 0; i < dsBookingData.Tables[0].Rows.Count; i++)
                {
                    oBookingData[i] = new BookingDTO();
                    dr = dsBookingData.Tables[0].Rows[i];
                    oBookingData[i].BookingId = Convert.ToInt32(dr.ItemArray.GetValue(0));
                    oBookingData[i].BookingCode = Convert.ToString(dr.ItemArray.GetValue(1));
                    oBookingData[i].BookingReference = Convert.ToString(dr.ItemArray.GetValue(2));
                    oBookingData[i].SDate = Convert.ToString(dr.ItemArray.GetValue(3));
                    oBookingData[i].EDate = Convert.ToString(dr.ItemArray.GetValue(4));
                    oBookingData[i].StartDate = Convert.ToDateTime(dr.ItemArray.GetValue(3).ToString());
                    oBookingData[i].EndDate = Convert.ToDateTime(dr.ItemArray.GetValue(4).ToString());
                    oBookingData[i].AccomodationType = Convert.ToString(dr.ItemArray.GetValue(5));
                    oBookingData[i].AccomodationName = Convert.ToString(dr.ItemArray.GetValue(6));
                    oBookingData[i].AccomodationTypeId = Convert.ToInt32(dr.ItemArray.GetValue(7));
                    oBookingData[i].AccomodationId = Convert.ToInt32(dr.ItemArray.GetValue(8));
                    oBookingData[i].NoOfNights = Convert.ToInt32(dr.ItemArray.GetValue(9));
                    oBookingData[i].NoOfPersons = Convert.ToInt32(dr.ItemArray.GetValue(10));
                    oBookingData[i].AgentId = Convert.ToInt32(dr.ItemArray.GetValue(11));
                    oBookingData[i].AgentName = DataSecurityManager.Decrypt(Convert.ToString(dr.ItemArray.GetValue(12)));
                    oBookingData[i].BookingStatusId = Convert.ToInt32(dr.ItemArray.GetValue(13));
                    oBookingData[i].ExchangeOrderNo = Convert.ToString(dr.ItemArray.GetValue(14));
                    if (dr.ItemArray.GetValue(15) != DBNull.Value)
                        oBookingData[i].VoucherDate = Convert.ToDateTime(dr.ItemArray.GetValue(15).ToString());
                    if (dr.ItemArray.GetValue(16) != DBNull.Value)
                        oBookingData[i].ArrivalDateTime = Convert.ToDateTime(dr.ItemArray.GetValue(16));
                    if (dr.ItemArray.GetValue(17) != DBNull.Value)
                        oBookingData[i].ArrivalTransport = Convert.ToString(dr.ItemArray.GetValue(17));
                    if (dr.ItemArray.GetValue(18) != DBNull.Value)
                        oBookingData[i].ArrivalCityId = Convert.ToInt32(dr.ItemArray.GetValue(18));
                    if (dr.ItemArray.GetValue(19) != DBNull.Value)
                        oBookingData[i].DepartureDateTime = Convert.ToDateTime(dr.ItemArray.GetValue(19));
                    if (dr.ItemArray.GetValue(20) != DBNull.Value)
                        oBookingData[i].DepartureTransport = Convert.ToString(dr.ItemArray.GetValue(20));
                    if (dr.ItemArray.GetValue(21) != DBNull.Value)
                        oBookingData[i].DepartureCityId = Convert.ToInt32(dr.ItemArray.GetValue(21));
                    if (dr.ItemArray.GetValue(22) != DBNull.Value)
                        oBookingData[i].TourId = Convert.ToString(dr.ItemArray.GetValue(22));
                    if (dr.ItemArray.GetValue(23) != DBNull.Value)
                        oBookingData[i].AccomodationId = Convert.ToInt32(dr.ItemArray.GetValue(23));
                    if (dr.ItemArray.GetValue(24) != DBNull.Value)
                        oBookingData[i].ArrivalVehicleNo = Convert.ToString(dr.ItemArray.GetValue(24));
                    if (dr.ItemArray.GetValue(25) != DBNull.Value)
                        oBookingData[i].ArrivalTransportCompany = Convert.ToString(dr.ItemArray.GetValue(25));
                    if (dr.ItemArray.GetValue(26) != DBNull.Value)
                        oBookingData[i].DepartureVehicleNo = Convert.ToString(dr.ItemArray.GetValue(26));
                    if (dr.ItemArray.GetValue(27) != DBNull.Value)
                        oBookingData[i].DepartureTransportCompany = Convert.ToString(dr.ItemArray.GetValue(27));
                    if (dr.ItemArray.GetValue(28) != DBNull.Value)
                        oBookingData[i].ArrivalTransportId = Convert.ToInt32(dr.ItemArray.GetValue(28));
                    if (dr.ItemArray.GetValue(29) != DBNull.Value)
                        oBookingData[i].DepartureTransportId = Convert.ToInt32(dr.ItemArray.GetValue(29));

                    if (dr.ItemArray.GetValue(30) != DBNull.Value)
                        oBookingData[i].ArrivalVehicleNameType = Convert.ToString(dr.ItemArray.GetValue(30));
                    if (dr.ItemArray.GetValue(31) != DBNull.Value)
                        oBookingData[i].ArrivalTransportCompanyPhoneNo = Convert.ToString(dr.ItemArray.GetValue(31));
                    if (dr.ItemArray.GetValue(32) != DBNull.Value)
                        oBookingData[i].ArrivalDriverPhoneNo = Convert.ToString(dr.ItemArray.GetValue(32));
                    if (dr.ItemArray.GetValue(33) != DBNull.Value)
                        oBookingData[i].DepartureVehicleNameType = Convert.ToString(dr.ItemArray.GetValue(33));
                    if (dr.ItemArray.GetValue(34) != DBNull.Value)
                        oBookingData[i].DepartureTransportCompanyPhoneNo = Convert.ToString(dr.ItemArray.GetValue(34));
                    if (dr.ItemArray.GetValue(35) != DBNull.Value)
                        oBookingData[i].DepartureDriverPhoneNo = Convert.ToString(dr.ItemArray.GetValue(35));

                    if (dr.ItemArray.GetValue(36) != DBNull.Value)
                        oBookingData[i].ForeignNationalCFormNoStart = Convert.ToInt32(dr.ItemArray.GetValue(36));
                    if (dr.ItemArray.GetValue(37) != DBNull.Value)
                        oBookingData[i].ForeignNationalCFormNoEnd = Convert.ToInt32(dr.ItemArray.GetValue(37));
                    if (dr.ItemArray.GetValue(38) != DBNull.Value)
                        oBookingData[i].IndianNationalCFormNoStart = Convert.ToInt32(dr.ItemArray.GetValue(38));
                    if (dr.ItemArray.GetValue(39) != DBNull.Value)
                        oBookingData[i].IndianNationalCFormNoEnd = Convert.ToInt32(dr.ItemArray.GetValue(39));
                    if (dr.ItemArray.GetValue(40) != DBNull.Value)
                        oBookingData[i].ProposedBooking = Convert.ToBoolean(dr.ItemArray.GetValue(40));

                    if (dr.ItemArray.GetValue(41) != DBNull.Value)
                        oBookingData[i].Chartered = Convert.ToBoolean(dr.ItemArray.GetValue(41));
                }
            }
            //}
            //catch(Exception e)
            //{
            //  throw new Exception("The method or operation is not implemented.");
            //}
            return oBookingData;
        }



        private BookingDTO[] GetConfirmMailDetails(int bookingid)
        {
            DataSet dsBookingData;
            DataRow dr;
            BookingDTO[] oBookingData;
            string sProcName;
            DatabaseManager oDB;

            dsBookingData = null;
            oBookingData = null;

            try
            {
                oDB = new DatabaseManager();
                sProcName = "sp_bookingConfirmationmail";
                oDB.DbCmd = oDB.GetStoredProcCommand(sProcName);
                oDB.DbDatabase.AddInParameter(oDB.DbCmd, "@BookingId", DbType.Int32, bookingid);
                dsBookingData = oDB.ExecuteDataSet(oDB.DbCmd);
            }
            catch (Exception exp)
            {
                oDB = null;
                dsBookingData = null;
                //GF.LogError("clsBookingHandler.GetBookingDetails", exp.Message);
            }

            if (dsBookingData != null)
            {
                //oBookingData[dsBookingData.Tables[0].Rows.Count] = new clsBookingData();
                oBookingData = new BookingDTO[dsBookingData.Tables[0].Rows.Count];
                for (int i = 0; i < dsBookingData.Tables[0].Rows.Count; i++)
                {
                    oBookingData[i] = new BookingDTO();
                    dr = dsBookingData.Tables[0].Rows[i];

                    oBookingData[i].BookingCode = Convert.ToString(dr.ItemArray.GetValue(0));

                    if (dr.ItemArray.GetValue(1) != DBNull.Value)
                        oBookingData[i].VoucherDate = Convert.ToDateTime(dr.ItemArray.GetValue(1).ToString());
                    if (dr.ItemArray.GetValue(2) != DBNull.Value)
                        oBookingData[i].TourId = Convert.ToString(dr.ItemArray.GetValue(2));
                    if (dr.ItemArray.GetValue(3) != DBNull.Value)
                        oBookingData[i].ArrivalDateTime = Convert.ToDateTime(dr.ItemArray.GetValue(3));
                    if (dr.ItemArray.GetValue(4) != DBNull.Value)
                        oBookingData[i].ArrivalCity = Convert.ToString(dr.ItemArray.GetValue(4));
                    if (dr.ItemArray.GetValue(5) != DBNull.Value)
                        oBookingData[i].ArrivalTransport = Convert.ToString(dr.ItemArray.GetValue(5));
                    if (dr.ItemArray.GetValue(6) != DBNull.Value)
                        oBookingData[i].ArrivalVehicleNo = Convert.ToString(dr.ItemArray.GetValue(6));
                    if (dr.ItemArray.GetValue(7) != DBNull.Value)
                        oBookingData[i].ArrivalVehicleNameType = Convert.ToString(dr.ItemArray.GetValue(7));
                    if (dr.ItemArray.GetValue(8) != DBNull.Value)
                        oBookingData[i].ArrivalTransportCompany = Convert.ToString(dr.ItemArray.GetValue(8));
                    if (dr.ItemArray.GetValue(9) != DBNull.Value)
                        oBookingData[i].ArrivalTransportCompanyPhoneNo = Convert.ToString(dr.ItemArray.GetValue(9));
                    if (dr.ItemArray.GetValue(10) != DBNull.Value)
                        oBookingData[i].ArrivalDriverPhoneNo = Convert.ToString(dr.ItemArray.GetValue(10));
                    if (dr.ItemArray.GetValue(11) != DBNull.Value)
                        oBookingData[i].DepartureDateTime = Convert.ToDateTime(dr.ItemArray.GetValue(11));
                    if (dr.ItemArray.GetValue(12) != DBNull.Value)
                        oBookingData[i].DepartureCity = Convert.ToString(dr.ItemArray.GetValue(12));
                    if (dr.ItemArray.GetValue(13) != DBNull.Value)
                        oBookingData[i].DepartureTransport = Convert.ToString(dr.ItemArray.GetValue(13));
                    if (dr.ItemArray.GetValue(14) != DBNull.Value)
                        oBookingData[i].DepartureVehicleNo = Convert.ToString(dr.ItemArray.GetValue(14));
                    if (dr.ItemArray.GetValue(15) != DBNull.Value)
                        oBookingData[i].DepartureVehicleNameType = Convert.ToString(dr.ItemArray.GetValue(15));

                    if (dr.ItemArray.GetValue(16) != DBNull.Value)
                        oBookingData[i].DepartureTransportCompany = Convert.ToString(dr.ItemArray.GetValue(16));
                    if (dr.ItemArray.GetValue(17) != DBNull.Value)
                        oBookingData[i].DepartureTransportCompanyPhoneNo = Convert.ToString(dr.ItemArray.GetValue(17));
                    if (dr.ItemArray.GetValue(18) != DBNull.Value)
                        oBookingData[i].DepartureDriverPhoneNo = Convert.ToString(dr.ItemArray.GetValue(18));

                    if (dr.ItemArray.GetValue(19) != DBNull.Value)
                        oBookingData[i].ExchangeOrderNo = Convert.ToString(dr.ItemArray.GetValue(19));


                }
            }
            //}
            //catch(Exception e)
            //{
            //  throw new Exception("The method or operation is not implemented.");
            //}
            return oBookingData;
        }

        //public List<BookingDTO> GetBookings(DateTime FromDate, DateTime ToDate)
        //{
        //    try
        //    {
        //        return GetBookings(FromDate, ToDate, ENums.BookingStatusTypes.NONE);
        //    }
        //    catch (Exception exp)
        //    {
        //        throw exp;
        //    }
        //}

        //internal List<BookingDTO> GetBookings(DateTime FromDate, DateTime ToDate, ENums.BookingStatusTypes BookingStatusType)
        //{
        //    try
        //    {
        //        cdtGetBookingsInput getBookingsInput = new cdtGetBookingsInput();
        //        return GetBookings(FromDate, ToDate, BookingStatusType, 0, 0);
        //    }
        //    catch (Exception exp)
        //    {
        //        throw exp;
        //    }
        //}

        public List<ViewBookingDTO> GetBookings(cdtGetBookingsInput getBookingsInput)
        {
            List<ViewBookingDTO> bookingList;
            ViewBookingDTO booking;
            DataRow dr;
            DataSet dsBookingData;
            string sProcName;
            dsBookingData = null;
            bookingList = null;

            if (getBookingsInput.FromDate == DateTime.MinValue || getBookingsInput.FromDate == DateTime.MaxValue)
                getBookingsInput.FromDate = GF.GetDate().AddYears(-10);
            if (getBookingsInput.ToDate == DateTime.MinValue || getBookingsInput.ToDate == DateTime.MaxValue)
                getBookingsInput.ToDate = GF.GetDate().AddYears(20);
            try
            {
                oDB = new DatabaseManager();
                sProcName = "up_Get_Bookings";
                oDB.DbCmd = oDB.GetStoredProcCommand(sProcName);
                oDB.DbDatabase.AddInParameter(oDB.DbCmd, "@FromDate", DbType.Date, getBookingsInput.FromDate);
                oDB.DbDatabase.AddInParameter(oDB.DbCmd, "@ToDate", DbType.Date, getBookingsInput.ToDate);
                oDB.DbDatabase.AddInParameter(oDB.DbCmd, "@BookingStatusTypeId", DbType.Int32, getBookingsInput.BookingStatusType);
                oDB.DbDatabase.AddInParameter(oDB.DbCmd, "@AccomTypeId", DbType.Int32, getBookingsInput.AccomTypeId);
                oDB.DbDatabase.AddInParameter(oDB.DbCmd, "@AccomId", DbType.Int32, getBookingsInput.AccomodationId);
                oDB.DbDatabase.AddInParameter(oDB.DbCmd, "@AgentId", DbType.Int32, getBookingsInput.AgentId);
                oDB.DbDatabase.AddInParameter(oDB.DbCmd, "@BookingCode", DbType.String, getBookingsInput.BookingCode);

                dsBookingData = oDB.ExecuteDataSet(oDB.DbCmd);
            }
            catch (Exception exp)
            {
                oDB = null;
                dsBookingData = null;
                GF.LogError("clsBookingHandler.GetBookings", exp.Message);
            }

            if (dsBookingData != null)
            {
                //oBookingData[dsBookingData.Tables[0].Rows.Count] = new clsBookingData();
                bookingList = new List<ViewBookingDTO>();
                for (int i = 0; i < dsBookingData.Tables[0].Rows.Count; i++)
                {
                    booking = new ViewBookingDTO();
                    dr = dsBookingData.Tables[0].Rows[i];
                    booking.BookingId = Convert.ToInt32(dr.ItemArray.GetValue(0));
                    booking.BookingCode = Convert.ToString(dr.ItemArray.GetValue(1));
                    booking.BookingReference = Convert.ToString(dr.ItemArray.GetValue(2));
                    booking.SDate = Convert.ToString(dr.ItemArray.GetValue(3));
                    booking.EDate = Convert.ToString(dr.ItemArray.GetValue(4));
                    booking.StartDate = Convert.ToDateTime(dr.ItemArray.GetValue(3).ToString());
                    booking.EndDate = Convert.ToDateTime(dr.ItemArray.GetValue(4).ToString());
                    booking.BookingStatus = Convert.ToString(dr.ItemArray.GetValue(5));
                    booking.AccomodationType = Convert.ToString(dr.ItemArray.GetValue(6));
                    if (dr.ItemArray.GetValue(7) != DBNull.Value)
                        booking.ProposedBooking = Convert.ToBoolean(dr.ItemArray.GetValue(7));
                    if (dr.ItemArray.GetValue(8) != DBNull.Value)
                    {
                        if (Convert.ToInt32(dr.ItemArray.GetValue(8)) > 0)
                        {
                            booking.HasForeignTourists = true;
                        }
                    }
                    if (dr.ItemArray.GetValue(9) != DBNull.Value)
                    {
                        if (Convert.ToInt32(dr.ItemArray.GetValue(9)) > 0)
                        {
                            booking.HasIndianTourists = true;
                        }
                    }

                    if (dr.ItemArray.GetValue(10) != DBNull.Value)
                        booking.CharteredBooking = Convert.ToBoolean(dr.ItemArray.GetValue(10));
                    if (dr.ItemArray.GetValue(11) != DBNull.Value)
                    booking.PaymentStatus = Convert.ToBoolean(dr.ItemArray.GetValue(11));

                    if (dr.ItemArray.GetValue(12) != DBNull.Value)
                        booking.PaidAmt = Convert.ToDouble(dr.ItemArray.GetValue(12));
                    if (dr.ItemArray.GetValue(13) != DBNull.Value)
                        booking.InvoiceAmount = Convert.ToDouble(dr.ItemArray.GetValue(13));

                    bookingList.Add(booking);
                }
            }
            return bookingList;
        }


        #region newgetbookingsFH
        public List<ViewBookingDTO> GetBookingsFH(cdtGetBookingsInput getBookingsInput)
        {
            List<ViewBookingDTO> bookingList;
            ViewBookingDTO booking;
            DataRow dr;
            DataSet dsBookingData;
            string sProcName;
            dsBookingData = null;
            bookingList = null;

            if (getBookingsInput.FromDate == DateTime.MinValue || getBookingsInput.FromDate == DateTime.MaxValue)
                getBookingsInput.FromDate = GF.GetDate().AddYears(-10);
            if (getBookingsInput.ToDate == DateTime.MinValue || getBookingsInput.ToDate == DateTime.MaxValue)
                getBookingsInput.ToDate = GF.GetDate().AddYears(20);
            try
            {
                oDB = new DatabaseManager();
                sProcName = "up_Get_BookingsFH";
                oDB.DbCmd = oDB.GetStoredProcCommand(sProcName);
                oDB.DbDatabase.AddInParameter(oDB.DbCmd, "@FromDate", DbType.Date, getBookingsInput.FromDate);
                oDB.DbDatabase.AddInParameter(oDB.DbCmd, "@ToDate", DbType.Date, getBookingsInput.ToDate);
                oDB.DbDatabase.AddInParameter(oDB.DbCmd, "@BookingStatusTypeId", DbType.Int32, getBookingsInput.BookingStatusType);
                oDB.DbDatabase.AddInParameter(oDB.DbCmd, "@AccomTypeId", DbType.Int32, getBookingsInput.AccomTypeId);
                oDB.DbDatabase.AddInParameter(oDB.DbCmd, "@AccomId", DbType.Int32, getBookingsInput.AccomodationId);
                oDB.DbDatabase.AddInParameter(oDB.DbCmd, "@AgentId", DbType.Int32, getBookingsInput.AgentId);
                oDB.DbDatabase.AddInParameter(oDB.DbCmd, "@BookingCode", DbType.String, getBookingsInput.BookingCode);

                dsBookingData = oDB.ExecuteDataSet(oDB.DbCmd);
            }
            catch (Exception exp)
            {
                oDB = null;
                dsBookingData = null;
                //  GF.LogError("clsBookingHandler.GetBookings", exp.Message);
            }

            if (dsBookingData != null)
            {
                //oBookingData[dsBookingData.Tables[0].Rows.Count] = new clsBookingData();
                bookingList = new List<ViewBookingDTO>();
                for (int i = 0; i < dsBookingData.Tables[0].Rows.Count; i++)
                {
                    booking = new ViewBookingDTO();
                    dr = dsBookingData.Tables[0].Rows[i];
                    booking.BookingId = Convert.ToInt32(dr.ItemArray.GetValue(0));
                    booking.StartDate = Convert.ToDateTime(dr.ItemArray.GetValue(1));
                    booking.EndDate = Convert.ToDateTime(dr.ItemArray.GetValue(2));
                    booking.SDate = Convert.ToString(dr.ItemArray.GetValue(1));
                    booking.EDate = Convert.ToString(dr.ItemArray.GetValue(2));
                    booking.unit = Convert.ToString(dr.ItemArray.GetValue(3).ToString());
                    booking.BookingCode = Convert.ToString(dr.ItemArray.GetValue(4).ToString());
                    booking.BookingReference = Convert.ToString(dr.ItemArray.GetValue(5));

                    booking.agentname = DataSecurityManager.Decrypt(Convert.ToString(dr.ItemArray.GetValue(6)));
                    booking.noofnights = Convert.ToInt32(dr.ItemArray.GetValue(7));
                    booking.SGL = Convert.ToInt32(dr.ItemArray.GetValue(8));
                    booking.TWN = Convert.ToInt32(dr.ItemArray.GetValue(9));
                    booking.DBL = Convert.ToInt32(dr.ItemArray.GetValue(10));

                    booking.Total = Convert.ToInt32(dr.ItemArray.GetValue(11));
                    booking.PAX = Convert.ToInt32(dr.ItemArray.GetValue(12));
                    booking.BookingStatus = Convert.ToString(dr.ItemArray.GetValue(13));
                    booking.BookingAmt = Convert.ToDouble((dr.ItemArray.GetValue(19) == null || dr.ItemArray.GetValue(19).ToString() == "") ? 0 : dr.ItemArray.GetValue(19));

                    if (dr.ItemArray.GetValue(14) != DBNull.Value)
                        booking.ProposedBooking = Convert.ToBoolean(dr.ItemArray.GetValue(14));
                    if (dr.ItemArray.GetValue(15) != DBNull.Value)
                    {
                        if (Convert.ToInt32(dr.ItemArray.GetValue(15)) > 0)
                        {
                            booking.HasForeignTourists = true;
                        }
                    }
                    if (dr.ItemArray.GetValue(16) != DBNull.Value)
                    {
                        if (Convert.ToInt32(dr.ItemArray.GetValue(16)) > 0)
                        {
                            booking.HasIndianTourists = true;
                        }
                    }

                    if (dr.ItemArray.GetValue(17) != DBNull.Value)
                        booking.CharteredBooking = Convert.ToBoolean(dr.ItemArray.GetValue(17));
                    if (dr.ItemArray.GetValue(18) != DBNull.Value)
                        booking.TRP = Convert.ToInt32(dr.ItemArray.GetValue(18));



                    bookingList.Add(booking);
                }
            }
            return bookingList;
        }


        public DataSet GetBookingsCruiseFH(cdtGetBookingsInput getBookingsInput)
        {
            List<ViewBookingDTO> bookingList;
            ViewBookingDTO booking;
            DataRow dr;
            DataSet dsBookingData;
            string sProcName;
            dsBookingData = null;
            bookingList = null;

            if (getBookingsInput.FromDate == DateTime.MinValue || getBookingsInput.FromDate == DateTime.MaxValue)
                getBookingsInput.FromDate = GF.GetDate().AddYears(-10);
            if (getBookingsInput.ToDate == DateTime.MinValue || getBookingsInput.ToDate == DateTime.MaxValue)
                getBookingsInput.ToDate = GF.GetDate().AddYears(20);
            try
            {
                oDB = new DatabaseManager();
                sProcName = "up_Get_CruiseBookingsFH";
                oDB.DbCmd = oDB.GetStoredProcCommand(sProcName);
                oDB.DbDatabase.AddInParameter(oDB.DbCmd, "@FromDate", DbType.Date, getBookingsInput.FromDate);
                oDB.DbDatabase.AddInParameter(oDB.DbCmd, "@ToDate", DbType.Date, getBookingsInput.ToDate);
                oDB.DbDatabase.AddInParameter(oDB.DbCmd, "@BookingStatusTypeId", DbType.Int32, getBookingsInput.BookingStatusType);
                oDB.DbDatabase.AddInParameter(oDB.DbCmd, "@AccomTypeId", DbType.Int32, getBookingsInput.AccomTypeId);
                oDB.DbDatabase.AddInParameter(oDB.DbCmd, "@AccomId", DbType.Int32, getBookingsInput.AccomodationId);
                oDB.DbDatabase.AddInParameter(oDB.DbCmd, "@AgentId", DbType.Int32, getBookingsInput.AgentId);
                oDB.DbDatabase.AddInParameter(oDB.DbCmd, "@BookingCode", DbType.String, getBookingsInput.BookingCode);

                dsBookingData = oDB.ExecuteDataSet(oDB.DbCmd);
            }
            catch (Exception exp)
            {
                oDB = null;
                dsBookingData = null;
                //  GF.LogError("clsBookingHandler.GetBookings", exp.Message);
            }

            if (dsBookingData != null)
            {
                ////oBookingData[dsBookingData.Tables[0].Rows.Count] = new clsBookingData();
                //bookingList = new List<ViewBookingDTO>();
                //for (int i = 0; i < dsBookingData.Tables[0].Rows.Count; i++)
                //{
                //    booking = new ViewBookingDTO();
                //    dr = dsBookingData.Tables[0].Rows[i];
                //    booking.BookingId = Convert.ToInt32(dr.ItemArray.GetValue(0));
                //    booking.StartDate = Convert.ToDateTime(dr.ItemArray.GetValue(1));
                //    booking.EndDate = Convert.ToDateTime(dr.ItemArray.GetValue(2));
                //    booking.SDate = Convert.ToString(dr.ItemArray.GetValue(1));
                //    booking.EDate = Convert.ToString(dr.ItemArray.GetValue(2));
                //    booking.unit = Convert.ToString(dr.ItemArray.GetValue(3).ToString());
                //    booking.BookingCode = Convert.ToString(dr.ItemArray.GetValue(4).ToString());
                //    booking.BookingReference = Convert.ToString(dr.ItemArray.GetValue(5));

                //    booking.agentname = Convert.ToString(dr.ItemArray.GetValue(6));
                //    booking.noofnights = Convert.ToInt32(dr.ItemArray.GetValue(7));
                //    booking.SGL = Convert.ToInt32(dr.ItemArray.GetValue(8));
                //    booking.TWN = Convert.ToInt32(dr.ItemArray.GetValue(9));
                //    booking.DBL = Convert.ToInt32(dr.ItemArray.GetValue(10));

                //    booking.Total = Convert.ToInt32(dr.ItemArray.GetValue(11));
                //    booking.PAX = Convert.ToInt32(dr.ItemArray.GetValue(12));
                //    booking.BookingStatus = Convert.ToString(dr.ItemArray.GetValue(13));


                //    if (dr.ItemArray.GetValue(14) != DBNull.Value)
                //        booking.ProposedBooking = Convert.ToBoolean(dr.ItemArray.GetValue(14));
                //    if (dr.ItemArray.GetValue(15) != DBNull.Value)
                //    {
                //        if (Convert.ToInt32(dr.ItemArray.GetValue(15)) > 0)
                //        {
                //            booking.HasForeignTourists = true;
                //        }
                //    }
                //    if (dr.ItemArray.GetValue(16) != DBNull.Value)
                //    {
                //        if (Convert.ToInt32(dr.ItemArray.GetValue(16)) > 0)
                //        {
                //            booking.HasIndianTourists = true;
                //        }
                //    }

                //    if (dr.ItemArray.GetValue(17) != DBNull.Value)
                //        booking.CharteredBooking = Convert.ToBoolean(dr.ItemArray.GetValue(17));
                //    if (dr.ItemArray.GetValue(18) != DBNull.Value)
                //        booking.TRP = Convert.ToInt32(dr.ItemArray.GetValue(18));

                //    bookingList.Add(booking);
                //}
            }
            return dsBookingData;
        }


        #endregion




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
                GF.LogError("clsBookingHandler.GetDataFromDB", exp.Message);
            }
            return ds;
        }

        public BookingDTO[] GetRoomBookings(string RoomNo)
        {
            DataSet dsBookingData;
            DataRow dr;
            BookingDTO[] oBookingData;
            string sProcName;
            DatabaseManager oDB;

            dsBookingData = null;
            oBookingData = null;

            try
            {
                oDB = new DatabaseManager();
                sProcName = "up_GetRoomBookings";
                oDB.DbCmd = oDB.GetStoredProcCommand(sProcName);
                oDB.DbDatabase.AddInParameter(oDB.DbCmd, "@RoomNo", DbType.String, RoomNo);
                dsBookingData = oDB.ExecuteDataSet(oDB.DbCmd);
            }
            catch (Exception exp)
            {
                oDB = null;
                dsBookingData = null;
                GF.LogError("clsBookingHandler.GetBookingDetails", exp.Message);
            }

            if (dsBookingData != null)
            {
                oBookingData = new BookingDTO[dsBookingData.Tables[0].Rows.Count];
                for (int i = 0; i < dsBookingData.Tables[0].Rows.Count; i++)
                {

                    oBookingData[i] = new BookingDTO();
                    dr = dsBookingData.Tables[0].Rows[i];
                    oBookingData[i].BookingId = Convert.ToInt32(dr.ItemArray.GetValue(0));
                    oBookingData[i].BookingCode = Convert.ToString(dr.ItemArray.GetValue(1));
                    oBookingData[i].BookingReference = Convert.ToString(dr.ItemArray.GetValue(2));
                    oBookingData[i].SDate = Convert.ToString(dr.ItemArray.GetValue(3));
                    oBookingData[i].EDate = Convert.ToString(dr.ItemArray.GetValue(4));
                }
            }
            return oBookingData;
        }

        #endregion
    }
}

