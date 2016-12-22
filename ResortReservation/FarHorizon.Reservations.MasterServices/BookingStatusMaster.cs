using System;
using System.Collections.Generic;
using System.Collections;
using System.Text;

using FarHorizon.Reservations.Common;


using FarHorizon.Reservations.DataBaseManager;
using System.Data;
using System.Data.SqlClient;
using FarHorizon.Reservations.Common.DataEntities.Masters;

namespace FarHorizon.Reservations.MasterServices
{
    public class BookingStatusMaster 
    {
        #region IMaster Members

        public bool Insert(BookingStatusDTO oBookingStatusData)
        {
            string sProcName;
            DatabaseManager oDB;
            try
            {
                oDB = new DatabaseManager();

                sProcName = "up_Ins_BookingStatusMaster";
                oDB.DbCmd = oDB.GetStoredProcCommand(sProcName);
                oDB.DbDatabase.AddInParameter(oDB.DbCmd, "@sBookingStatusType", DbType.String, oBookingStatusData.BookingStatusType);
                oDB.DbDatabase.AddInParameter(oDB.DbCmd, "@lBookingStatusColor", DbType.Int64, oBookingStatusData.BookingStatusColor);
                oDB.ExecuteNonQuery(oDB.DbCmd);
            }
            catch (Exception exp)
            {
                oDB = null;
                oBookingStatusData = null;
                GF.LogError("clsBookingStatusMaster.Insert", exp.Message);
                return false;
            }
            finally
            {
                oDB = null;
            }
            return true;
        }

        public bool Update(BookingStatusDTO oBookingStatusData)
        {
            string sProcName;
            DatabaseManager oDB;
            try
            {
                oDB = new DatabaseManager();

                sProcName = "up_Upd_BookingStatusMaster";
                oDB.DbCmd = oDB.GetStoredProcCommand(sProcName);
                oDB.DbDatabase.AddInParameter(oDB.DbCmd, "@iBookingStatusId", DbType.Int32, oBookingStatusData.BookingStatusId);
                oDB.DbDatabase.AddInParameter(oDB.DbCmd, "@sBookingStatusType", DbType.String, oBookingStatusData.BookingStatusType);
                oDB.DbDatabase.AddInParameter(oDB.DbCmd, "@iBookingStatusColor", DbType.Int64, oBookingStatusData.BookingStatusColor);
                oDB.ExecuteNonQuery(oDB.DbCmd);
            }
            catch (Exception exp)
            {
                oDB = null;
                oBookingStatusData = null;
                GF.LogError("clsBookingStatusMaster.Update", exp.Message);
                return false;
            }
            finally
            {
                oBookingStatusData = null;
                oDB = null;
            }
            return true;
        }

        public bool Delete(BookingStatusDTO oBookingStatusData)
        {
            string sProcName;
            DatabaseManager oDB;
            try
            {
                oDB = new DatabaseManager();
                sProcName = "up_Del_BookingStatusMaster";
                oDB.DbCmd = oDB.GetStoredProcCommand(sProcName);
                oDB.DbDatabase.AddInParameter(oDB.DbCmd, "@iBookingStatusId", DbType.Int32, oBookingStatusData.BookingStatusId);
                oDB.ExecuteNonQuery(oDB.DbCmd);
            }
            catch (Exception exp)
            {
                oDB = null;
                oBookingStatusData = null;
                GF.LogError("clsBookingStatusMaster.Delete", exp.Message);
                return false;
            }
            finally
            {
                oDB = null;
            }
            return true;
        }

        public BookingStatusDTO[] GetData()
        {
            return GetData(0);
        }

        public BookingStatusDTO[] GetData(int BookingStatusId)
        {
            BookingStatusDTO[] oBookingStatusData;
            oBookingStatusData = null;
            DataSet ds;
            string query = "select BookingStatusId, BookingStatusType, BookingStatusColor from " +
                " tblBookingStatusMaster where 1=1";
            if (BookingStatusId != 0)
            {
                query += " and BookingStatusId=" + BookingStatusId;
                query += " order by BookingStatusType";
            }
            ds = GetDataFromDB(query);
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                oBookingStatusData = new BookingStatusDTO[ds.Tables[0].Rows.Count];
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    oBookingStatusData[i] = new BookingStatusDTO();
                    oBookingStatusData[i].BookingStatusId = Convert.ToInt32(ds.Tables[0].Rows[i][0]);
                    oBookingStatusData[i].BookingStatusType = Convert.ToString(ds.Tables[0].Rows[i][1]);
                    //oBookingStatusData[i].BookingStatusColor = Convert.ToInt64(ds.Tables[0].Rows[i][2]);                    
                }
            }
            return oBookingStatusData;
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
                GF.LogError("clsBookingStatusMaster.GetDataFromDB", exp.Message);
            }
            return ds;
        }


        #endregion
    }
}
