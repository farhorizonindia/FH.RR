using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using FarHorizon.Reservations.Common.DataEntities.Masters;
using FarHorizon.Reservations.Common;
using FarHorizon.Reservations.DataBaseManager;

namespace FarHorizon.Reservations.UserManager
{
    internal class UserRightsHandler
    {
        #region UserRights Functions

        #region Add Method(s)
        public bool AddUserRights(UserRightsData UserRights)
        {
            DatabaseManager oDB = new DatabaseManager();
            string sProcName = "up_SaveUserRights";
            oDB.DbCmd = oDB.GetStoredProcCommand(sProcName);
            oDB.DbDatabase.AddInParameter(oDB.DbCmd, "@iUserId", DbType.Int32, UserRights.UserID);
            oDB.DbDatabase.AddInParameter(oDB.DbCmd, "@iAccomTypeID", DbType.Int32, UserRights.AccomTypeID);
            oDB.DbDatabase.AddInParameter(oDB.DbCmd, "@iAccomID", DbType.Int32, UserRights.AccomID);
            try
            {
                oDB.ExecuteNonQuery(oDB.DbCmd);            
            }
            catch (Exception e)
            {
                GF.LogError("clsUserMaster.AddUserRights", e.Message);
                return false;
            }
            finally
            {
                oDB = null;
            }
            return true;

        }
        #endregion

        #region Update Method(s)
        public bool UpdateUserRights(UserRightsData UserRights)
        {
            return AddUserRights(UserRights);
        }
        #endregion

        #region Delete Method(s)
        public bool DeleteUserRights(UserRightsData UserRights)
        {
            DatabaseManager oDB = new DatabaseManager();
            string sProcName = "up_DeleteUserRights";
            try
            {
                oDB.DbCmd = oDB.GetStoredProcCommand(sProcName);
                oDB.DbDatabase.AddInParameter(oDB.DbCmd, "@iUserId", DbType.Int32, UserRights.UserID);
                oDB.ExecuteNonQuery(oDB.DbCmd);
            }
            catch (Exception e)
            {
                GF.LogError("clsUserMaster.DeleteUserRights", e.Message);
                return false;
            }
            finally
            {
                oDB = null;
            }
            return true;

        }
        #endregion

        #region Get Method(s)
        public UserRightsData[] GetUserRights(int UserID)
        {
            DatabaseManager oDB = new DatabaseManager();
            UserRightsData[] oUserRightsData = null;
            string sProcName = "up_GetUserRights";
            oDB.DbCmd = oDB.GetStoredProcCommand(sProcName);
            oDB.DbDatabase.AddInParameter(oDB.DbCmd, "@iUserId", DbType.Int32, UserID);
            try
            {
                DataSet ds = oDB.ExecuteDataSet(oDB.DbCmd);
                if (ds != null)
                {
                    int iCount = ds.Tables[0].Rows.Count;
                    if (iCount > 0)
                    {
                        oUserRightsData = new UserRightsData[iCount];
                        for (int i = 0; i < iCount; i++)
                        {
                            oUserRightsData[i] = new UserRightsData();
                            oUserRightsData[i].UserID = Convert.ToInt32(ds.Tables[0].Rows[i][0].ToString());
                            oUserRightsData[i].AccomTypeID = Convert.ToInt32(ds.Tables[0].Rows[i][1].ToString());
                            oUserRightsData[i].AccomID = Convert.ToInt32(ds.Tables[0].Rows[i][2].ToString());
                        }
                    }
                }            
            }
            catch (Exception e)
            {
                GF.LogError("clsUserRightsData[].GetUserRights", e.Message);
                return null;
            }
            finally
            {
                oDB = null;
            }
            return oUserRightsData;
        }
        #endregion
        #endregion UserRights Functions
    }
}
