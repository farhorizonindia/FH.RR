using System;
using System.Collections.Generic;
using System.Text;
using FarHorizon.Reservations.Common.DataEntities.Masters;
using FarHorizon.Reservations.Common.DataEntities.Client;
using FarHorizon.Reservations.Common;
using FarHorizon.Reservations.DataBaseManager;
using System.Data;

namespace FarHorizon.Reservations.BusinessTier.BusinessLogic.BookingEngine
{
    internal class BookingActivitiesHandler
    {
        DatabaseManager oDB;
        public bool AddBookingActivity(BookingActivityDTO[] oBookingActivityDTO)
        {
            bool bActionCompleted = false;
            try
            {
                bActionCompleted = SaveData(oBookingActivityDTO);
            }
            catch (Exception exp)
            {
                throw exp;
            }
            finally
            {
                oBookingActivityDTO = null;
            }
            return bActionCompleted;
        }

        public bool UpdateBookingActivity(BookingActivityDTO[] oBookingActivityDTO)
        {
            bool bActionCompleted = false;
            try
            {
                bActionCompleted = SaveData(oBookingActivityDTO);
            }
            catch (Exception exp)
            {
                oBookingActivityDTO = null;
                throw exp;
            }
            finally
            {
                oBookingActivityDTO = null;
            }
            return bActionCompleted;
        }

        private bool SaveData(BookingActivityDTO[] oBookingActivityDTO)
        {
            bool actionCompleted = false;
            try
            {
                if (oBookingActivityDTO != null && oBookingActivityDTO.Length > 0)
                {
                    DeleteBookingActivities(oBookingActivityDTO[0].BookingId);
                    if (oDB == null)
                        oDB = new DatabaseManager();
                    string sProcName = "up_Ins_BookingActivity";
                    for (int i = 0; i < oBookingActivityDTO.Length; i++)
                    {
                        if (oBookingActivityDTO[i] != null)
                        {
                            oDB.DbCmd = oDB.GetStoredProcCommand(sProcName);
                            oDB.DbDatabase.AddInParameter(oDB.DbCmd, "@iBookingId", DbType.Int32, oBookingActivityDTO[i].BookingId);
                            oDB.DbDatabase.AddInParameter(oDB.DbCmd, "@iAccomId", DbType.Int32, oBookingActivityDTO[i].AccomId);
                            oDB.DbDatabase.AddInParameter(oDB.DbCmd, "@iActivityId", DbType.Int32, oBookingActivityDTO[i].ActivityId);
                            oDB.DbDatabase.AddInParameter(oDB.DbCmd, "@dtOperationDate", DbType.DateTime, oBookingActivityDTO[i].OperationDate);
                            oDB.ExecuteNonQuery(oDB.DbCmd);
                            oDB.DbCmd.Parameters.Clear();
                            actionCompleted = true;
                        }
                    }
                }
            }
            catch (Exception exp)
            {
                oDB = null;
                oBookingActivityDTO = null;
                throw exp;
            }
            finally
            {
                oDB = null;
                oBookingActivityDTO = null;
            }
            return actionCompleted;
        }

        public bool DeleteBookingActivities(int BookingId)
        {
            string sProcName = "up_Del_BookingActivity";

            try
            {
                if (oDB == null)
                    oDB = new DatabaseManager();

                oDB.DbCmd = oDB.GetStoredProcCommand(sProcName);
                oDB.DbDatabase.AddInParameter(oDB.DbCmd, "@iBookingId", DbType.Int32, BookingId);
                oDB.ExecuteNonQuery(oDB.DbCmd);
            }
            catch (Exception exp)
            {
                oDB = null;
                throw exp;
            }
            return true;
        }

        public BookingActivityDTO[] GetBookingActivities(int BookingId)
        {
            return GetBookingActivities(BookingId, 0);
        }

        public BookingActivityDTO[] GetBookingActivities(int BookingId, int ActivityId)
        {
            DataSet dsBookingActivityData;
            BookingActivityDTO[] oBookingActivityDTO;
            string sProcName;
            DatabaseManager oDB = null;

            dsBookingActivityData = null;
            oBookingActivityDTO = null;
            sProcName = "up_Get_BookingActivities";

            try
            {
                oDB = new DatabaseManager();
                oDB.DbCmd = oDB.GetStoredProcCommand(sProcName);
                oDB.DbDatabase.AddInParameter(oDB.DbCmd, "@BookingId", DbType.Int32, BookingId);
                oDB.DbDatabase.AddInParameter(oDB.DbCmd, "@ActivityId", DbType.Int32, ActivityId);

                dsBookingActivityData = oDB.ExecuteDataSet(oDB.DbCmd);
                oDB = null;

                oBookingActivityDTO = MapDBDataToObject(dsBookingActivityData);
            }
            catch (Exception exp)
            {
                oDB = null;
                dsBookingActivityData = null;
                throw exp;
            }
            return oBookingActivityDTO;
        }

        private BookingActivityDTO[] MapDBDataToObject(DataSet dsBookingActivityData)
        {
            DataRow dr;
            BookingActivityDTO[] BookingActivityList = null;
            try
            {
                if (dsBookingActivityData != null)
                {
                    BookingActivityList = new BookingActivityDTO[dsBookingActivityData.Tables[0].Rows.Count];
                    for (int i = 0; i < dsBookingActivityData.Tables[0].Rows.Count; i++)
                    {
                        BookingActivityList[i] = new BookingActivityDTO();
                        dr = dsBookingActivityData.Tables[0].Rows[i];
                        if (dr.ItemArray.GetValue(0) != DBNull.Value)
                            BookingActivityList[i].BookingId = Convert.ToInt32(dr.ItemArray.GetValue(0));
                        if (dr.ItemArray.GetValue(1) != DBNull.Value)
                            BookingActivityList[i].AccomId = Convert.ToInt32(dr.ItemArray.GetValue(1));
                        if (dr.ItemArray.GetValue(2) != DBNull.Value)
                            BookingActivityList[i].Accomodation = Convert.ToString(dr.ItemArray.GetValue(2));
                        if (dr.ItemArray.GetValue(3) != DBNull.Value)
                            BookingActivityList[i].ActivityId = Convert.ToInt32(dr.ItemArray.GetValue(3));
                        if (dr.ItemArray.GetValue(4) != DBNull.Value)
                            BookingActivityList[i].OperationDate = Convert.ToDateTime(dr.ItemArray.GetValue(4).ToString());
                    }
                }
            }
            catch (Exception exp)
            {
                throw exp;
            }
            return BookingActivityList;
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
                throw exp;
            }
            return ds;
        }
    }
}
