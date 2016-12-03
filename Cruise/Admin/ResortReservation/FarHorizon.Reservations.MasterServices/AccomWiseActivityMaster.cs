using System;
using System.Collections.Generic;
using System.Text;

using FarHorizon.Reservations.Common;

using FarHorizon.Reservations.DataBaseManager;
using System.Data;
using System.Data.SqlClient;
using FarHorizon.Reservations.Common.DataEntities.Masters;

namespace FarHorizon.Reservations.MasterServices
{
    public class AccomActivityMaster 
    {
        #region IMaster Members
        public bool Insert(AccomActivityDTO oAccomActivityData)
        {
            string sProcName;
            DatabaseManager oDB;
            try
            {
                oDB = new DatabaseManager();

                sProcName = "Up_Ins_AccomActivities";
                oDB.DbCmd = oDB.GetStoredProcCommand(sProcName);
                oDB.DbDatabase.AddInParameter(oDB.DbCmd, "@AccomId", DbType.Int32, oAccomActivityData.AccomodationId);
                oDB.DbDatabase.AddInParameter(oDB.DbCmd, "@ActivityName", DbType.String, oAccomActivityData.ActivityName);
                oDB.DbDatabase.AddInParameter(oDB.DbCmd, "@ActivityDesc", DbType.String, oAccomActivityData.ActivityDesc);
                oDB.ExecuteNonQuery(oDB.DbCmd);
            }
            catch (Exception exp)
            {
                GF.LogError("clsAccomWiseActivityMaster.Insert", exp.Message.ToString());
                oDB = null;
                return false;
            }
            finally
            {
                oDB = null;
            }
            return true;
        }
        public bool Update(AccomActivityDTO oAccomActivityData)
        {
            string sProcName;
            DatabaseManager oDB;
            try
            {
                oDB = new DatabaseManager();

                sProcName = "Up_Upd_AccomActivities";
                oDB.DbCmd = oDB.GetStoredProcCommand(sProcName);
                oDB.DbDatabase.AddInParameter(oDB.DbCmd, "@AccomId", DbType.Int32, oAccomActivityData.AccomodationId);
                oDB.DbDatabase.AddInParameter(oDB.DbCmd, "@ActivityId", DbType.Int32, oAccomActivityData.ActivityId);
                oDB.DbDatabase.AddInParameter(oDB.DbCmd, "@ActivityName", DbType.String, oAccomActivityData.ActivityName);
                oDB.DbDatabase.AddInParameter(oDB.DbCmd, "@ActivityDesc", DbType.String, oAccomActivityData.ActivityDesc);
                oDB.ExecuteNonQuery(oDB.DbCmd);
            }
            catch (Exception exp)
            {
                GF.LogError("clsAccomWiseActivityMaster.Update", exp.Message.ToString());
                oDB = null;
                return false;
            }
            finally
            {
                oDB = null;
            }
            return true;
        }
        public bool Delete(AccomActivityDTO oAccomActivityData)
        {
            string sProcName;
            DatabaseManager oDB;
            try
            {
                oDB = new DatabaseManager();

                sProcName = "up_Del_AccomActivityMaster";
                oDB.DbCmd = oDB.GetStoredProcCommand(sProcName);
                oDB.DbDatabase.AddInParameter(oDB.DbCmd, "@iAccomId", DbType.Int32, oAccomActivityData.AccomodationId);
                oDB.DbDatabase.AddInParameter(oDB.DbCmd, "@iActivityId", DbType.Int32, oAccomActivityData.ActivityId);
                oDB.ExecuteNonQuery(oDB.DbCmd);
            }
            catch (Exception exp)
            {
                GF.LogError("clsAccomWiseActivityMaster.Delete", exp.Message.ToString());
                oDB = null;
                return false;
            }
            finally
            {
                oDB = null;
            }
            return true;
        }
        public AccomActivityDTO[] GetData()
        {
            return GetData(0, 0);
        }

        public AccomActivityDTO[] GetData(int AccomodationId)
        {
            return GetData(AccomodationId, 0);
        }

        public AccomActivityDTO[] GetData(int AccomodationId, int ActivityId)
        {
            DataSet ds;
            AccomActivityDTO[] AccomWiseActivityData;
            AccomWiseActivityData = null;
            ds = null;
            string sProcName;
            DatabaseManager oDB;
            try
            {
                oDB = new DatabaseManager();

                sProcName = "Up_Get_AccomActivities";
                oDB.DbCmd = oDB.GetStoredProcCommand(sProcName);
                oDB.DbDatabase.AddInParameter(oDB.DbCmd, "@AccomId", DbType.Int32, AccomodationId);
                oDB.DbDatabase.AddInParameter(oDB.DbCmd, "@ActivityId", DbType.Int32, ActivityId);
                ds = oDB.ExecuteDataSet(oDB.DbCmd);
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    AccomWiseActivityData = new AccomActivityDTO[ds.Tables[0].Rows.Count];
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        AccomWiseActivityData[i] = new AccomActivityDTO();
                        AccomWiseActivityData[i].AccomodationId = ds.Tables[0].Rows[i][0] != DBNull.Value ? Convert.ToInt32(ds.Tables[0].Rows[i][0]) : 0;
                        AccomWiseActivityData[i].AccomodationName = ds.Tables[0].Rows[i][1] != DBNull.Value ? Convert.ToString(ds.Tables[0].Rows[i][1]) : "";
                        AccomWiseActivityData[i].ActivityId = ds.Tables[0].Rows[i][2] != DBNull.Value ? Convert.ToInt32(ds.Tables[0].Rows[i][2]) : 0;
                        AccomWiseActivityData[i].ActivityName = ds.Tables[0].Rows[i][3] != DBNull.Value ? Convert.ToString(ds.Tables[0].Rows[i][3]) : "";
                        AccomWiseActivityData[i].ActivityDesc = ds.Tables[0].Rows[i][4] != DBNull.Value ? Convert.ToString(ds.Tables[0].Rows[i][4]) : "";
                    }
                }
            }
            catch (Exception exp)
            {
                GF.LogError("clsAccomodationMaster.Update", exp.Message.ToString());
                oDB = null;
            }
            finally
            {
                oDB = null;
            }
            return AccomWiseActivityData;
        }
        private DataSet GetDataFromDB(string query)
        {
            DatabaseManager oDB = new DatabaseManager();
            DataSet ds = null;
            try
            {
                oDB.DbCmd = oDB.GetSqlStringCommand(query);
                //DataSet ds = oDB.FetchRecords("tblBookingDetails", Query);
                ds = oDB.ExecuteDataSet(oDB.DbCmd);
            }
            catch (Exception exp)
            {
                GF.LogError("clsAccomodationMaster.GetDataFromDB", exp.Message.ToString());
                ds = null;
            }
            finally
            {
                oDB = null;
            }
            return ds;
        }
        #endregion
    }
}
