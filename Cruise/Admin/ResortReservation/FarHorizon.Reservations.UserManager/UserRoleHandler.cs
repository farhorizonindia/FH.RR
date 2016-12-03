using System;
using System.Collections.Generic;
using System.Text;
using FarHorizon.Reservations.DataBaseManager;
using System.Data;
using System.Data.SqlClient;
using FarHorizon.Reservations.Common;
using FarHorizon.Reservations.Common.DataEntities.Masters;

namespace FarHorizon.Reservations.UserManager
{
    internal class UserRoleHandler
    {
        #region Role Functions

        #region Add Method(s)
        public bool AddRole(UserRoleDTO UserRoleDTO)
        {
            DatabaseManager oDB;
            oDB = new DatabaseManager();
            string sProcName = "up_SaveRole";
            oDB.DbCmd = oDB.GetStoredProcCommand(sProcName);
            oDB.DbDatabase.AddInParameter(oDB.DbCmd, "@sRoleName", DbType.String, UserRoleDTO.UserRoleName);
            try
            {
                oDB.ExecuteNonQuery(oDB.DbCmd);
                //return true;
            }
            catch (Exception e)
            {
                GF.LogError("clsUserRoleHandler.SaveRole", e.Message);
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
        public bool UpdateRole(UserRoleDTO UserRoleDTO)
        {
            DatabaseManager oDB;
            oDB = new DatabaseManager();
            string sProcName = "up_UpdateRole";
            oDB.DbCmd = oDB.GetStoredProcCommand(sProcName);
            oDB.DbDatabase.AddInParameter(oDB.DbCmd, "@iRoleID", DbType.Int32, UserRoleDTO.UserRoleId);
            oDB.DbDatabase.AddInParameter(oDB.DbCmd, "@sRoleName", DbType.String, UserRoleDTO.UserRoleName);
            try
            {
                oDB.ExecuteNonQuery(oDB.DbCmd);
                //return true;
            }
            catch (Exception e)
            {
                GF.LogError("clsUserRoleHandler.UpdateRole", e.Message);
                return false;
            }
            finally
            {
                oDB = null;
            }
            return true;
        }
        #endregion

        #region Delete Method(s)
        public bool DeleteRole(UserRoleDTO UserRoleDTO)
        {
            DatabaseManager oDB;
            oDB = new DatabaseManager();
            string sProcName = "up_DeleteRole";
            oDB.DbCmd = oDB.GetStoredProcCommand(sProcName);
            oDB.DbDatabase.AddInParameter(oDB.DbCmd, "@iRoleID", DbType.Int32, UserRoleDTO.UserRoleId);
            try
            {
                oDB.ExecuteNonQuery(oDB.DbCmd);
                //return true;
            }
            catch (Exception e)
            {
                GF.LogError("clsUserRoleHandler.DeleteRole", e.Message);
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
        public UserRoleDTO[] GetRoles()
        {
            DatabaseManager oDB;
            UserRoleDTO[] oRole = null;
            oDB = new DatabaseManager();
            string sProcName = "up_GetRoles";
            oDB.DbCmd = oDB.GetStoredProcCommand(sProcName);
            try
            {
                DataSet ds = oDB.ExecuteDataSet(oDB.DbCmd);
                if (ds != null)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        oRole = new UserRoleDTO[ds.Tables[0].Rows.Count];
                        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                        {
                            oRole[i] = new UserRoleDTO();
                            oRole[i].UserRoleId = Convert.ToInt32(ds.Tables[0].Rows[i][0].ToString());
                            oRole[i].UserRoleName = Convert.ToString(ds.Tables[0].Rows[i][1].ToString());
                        }
                    }
                }
            }
            catch (Exception e)
            {
                GF.LogError("clsUserMaster.GetRoles", e.Message);
                return null;
            }
            finally
            {
                oDB = null;
            }
            return oRole;
        }

        public UserRoleDTO GetRole(int RoleID)
        {
            DatabaseManager oDB;
            UserRoleDTO oRole = null;
           
            oDB = new DatabaseManager();
            string sProcName = "up_GetRole";
            oDB.DbCmd = oDB.GetStoredProcCommand(sProcName);
            oDB.DbDatabase.AddInParameter(oDB.DbCmd, "@iRoleID", DbType.Int32, RoleID);
            try
            {
                DataSet ds = oDB.ExecuteDataSet(oDB.DbCmd);
                if (ds != null)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                        {
                            oRole = new UserRoleDTO();
                            oRole.UserRoleId = Convert.ToInt32(ds.Tables[0].Rows[i][0].ToString());
                            oRole.UserRoleName = Convert.ToString(ds.Tables[0].Rows[i][1].ToString());
                        }
                    }
                }
                //return oRole;

            }
            catch (Exception e)
            {
                GF.LogError("clsUserMaster.SaveRole", e.Message);
                return null;
            }
            finally
            {
                oDB = null;
            }
            return oRole;
        }
        #endregion

        #endregion Role Functions
    }
}
