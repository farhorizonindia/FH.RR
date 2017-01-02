using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using FarHorizon.Reservations.Common;
using FarHorizon.Reservations.Common.DataEntities.Masters;
using FarHorizon.Reservations.DataBaseManager;
using FarHorizon.Reservations.MasterServices;
using FarHorizon.DataSecurity;

namespace FarHorizon.Reservations.MasterServices
{
    public class UserMaster 
    {
        #region IMaster Members

        public bool Insert(UserDTO oUserData)
        {
            string sProcName;
            DatabaseManager oDB;
            try
            {
                oDB = new DatabaseManager();

                sProcName = "up_Ins_UserMaster";
                oDB.DbCmd = oDB.GetStoredProcCommand(sProcName);

                oDB.DbDatabase.AddInParameter(oDB.DbCmd, "@UserId", DbType.String, oUserData.UserId);
                oDB.DbDatabase.AddInParameter(oDB.DbCmd, "@sUserName", DbType.String, DataSecurityManager.Encrypt(oUserData.UserName));
                oDB.DbDatabase.AddInParameter(oDB.DbCmd, "@Password", DbType.String, DataSecurityManager.Encrypt(oUserData.Password));
                oDB.DbDatabase.AddInParameter(oDB.DbCmd, "@Active", DbType.Boolean, oUserData.Active);
                oDB.DbDatabase.AddInParameter(oDB.DbCmd, "@Administrator", DbType.Boolean, oUserData.Administrator);
                oDB.DbDatabase.AddInParameter(oDB.DbCmd, "@UserRoleId", DbType.Int32, oUserData.UserRoleData.UserRoleId);
                oDB.DbDatabase.AddInParameter(oDB.DbCmd, "@UserEmailId", DbType.String, DataSecurityManager.Encrypt(oUserData.EmailId));
                oDB.ExecuteNonQuery(oDB.DbCmd);
            }
            catch (Exception exp)
            {
                oDB = null;
                oUserData = null;
                GF.LogError("clsUserMaster.Insert", exp.Message);
                return false;
            }
            finally
            {
                oDB = null;
                oUserData = null;
            }
            return true;
        }

        public bool Update(UserDTO oUserData)
        {
            string sProcName;
            DatabaseManager oDB;
            try
            {
                oDB = new DatabaseManager();

                sProcName = "up_Upd_UserMaster";
                oDB.DbCmd = oDB.GetStoredProcCommand(sProcName);

                oDB.DbDatabase.AddInParameter(oDB.DbCmd, "@UserId", DbType.String, oUserData.UserId);
                oDB.DbDatabase.AddInParameter(oDB.DbCmd, "@sUserName", DbType.String, DataSecurityManager.Encrypt( oUserData.UserName));
                oDB.DbDatabase.AddInParameter(oDB.DbCmd, "@Password", DbType.String, DataSecurityManager.Encrypt(oUserData.Password));
                oDB.DbDatabase.AddInParameter(oDB.DbCmd, "@Active", DbType.Boolean, oUserData.Active);
                oDB.DbDatabase.AddInParameter(oDB.DbCmd, "@Administrator", DbType.Boolean, oUserData.Administrator);
                oDB.DbDatabase.AddInParameter(oDB.DbCmd, "@userRoleId", DbType.Int32, oUserData.UserRoleData.UserRoleId);
                oDB.DbDatabase.AddInParameter(oDB.DbCmd, "@UserEmailId", DbType.String, DataSecurityManager.Encrypt(oUserData.EmailId));

                oDB.ExecuteNonQuery(oDB.DbCmd);
            }
            catch (Exception exp)
            {
                oDB = null;
                oUserData = null;
                GF.LogError("clsUserMaster.Update", exp.Message);
                return false;
            }
            finally
            {
                oDB = null;
                oUserData = null;
            }
            return true;
        }

        public bool Delete(UserDTO oUserData)
        {
            string sProcName;
            DatabaseManager oDB;
            try
            {
                oDB = new DatabaseManager();

                sProcName = "up_Del_UserMaster";
                oDB.DbCmd = oDB.GetStoredProcCommand(sProcName);
                oDB.DbDatabase.AddInParameter(oDB.DbCmd, "@UserId", DbType.String, oUserData.UserId);
                oDB.ExecuteNonQuery(oDB.DbCmd);
            }
            catch (Exception exp)
            {
                oDB = null;
                oUserData = null;
                GF.LogError("clsUserMasterAmit.Delete", exp.Message);
                return false;
            }
            finally
            {
                oDB = null;
            }
            return true;
        }

        public UserDTO[] GetUsers()
        {
            UserDTO[] oUserData;
            oUserData = GetData("");
            return oUserData;
        }

        public UserDTO GetUser(string sUserId)
        {
            UserDTO[] oUserData;
            oUserData = GetData(sUserId);
            if (oUserData != null && oUserData.Length > 0)
            {
                return oUserData[0];
            }
            return null;
        }

        private UserDTO[] GetData(string sUserId)
        {
            UserDTO[] oUserData;
            oUserData = null;
            DataSet ds;
            string query = "select UserId, UserName, Password, Active, Administrator, UserRoleId, userEmailId from tbluserMaster where 1=1";
            if (sUserId.Trim() != "")
            {
                query += " and UserId='" + sUserId + "'";
                query += " order by UserId";
            }
            ds = GetDataFromDB(query);
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                oUserData = new UserDTO[ds.Tables[0].Rows.Count];
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    oUserData[i] = new UserDTO();
                    oUserData[i].UserId = Convert.ToString(ds.Tables[0].Rows[i][0]);
                    oUserData[i].UserName = DataSecurityManager.Decrypt(Convert.ToString(ds.Tables[0].Rows[i][1]));
                    oUserData[i].Password = DataSecurityManager.Decrypt(Convert.ToString(ds.Tables[0].Rows[i][2]));
                    oUserData[i].Active = Convert.ToBoolean(ds.Tables[0].Rows[i][3]);
                    oUserData[i].Administrator = Convert.ToBoolean(ds.Tables[0].Rows[i][4]);
                    oUserData[i].EmailId = ds.Tables[0].Rows[i][6] != DBNull.Value ? DataSecurityManager.Decrypt(Convert.ToString(ds.Tables[0].Rows[i][6])) : String.Empty;

                    UserRoleDTO userRoleDto = new UserRoleDTO();
                    if (ds.Tables[0].Rows[i][5] != DBNull.Value)
                        userRoleDto = GetUserRoleDetails(Convert.ToInt32(ds.Tables[0].Rows[i][5]));

                    oUserData[i].UserRoleData = userRoleDto;
                    oUserData[i].RoleIdForDisplay = userRoleDto.UserRoleId;
                    oUserData[i].RoleNameForDisplay = userRoleDto.UserRoleName;
                }
            }
            return oUserData;
        }

        private UserRoleDTO GetUserRoleDetails(int userRoleId)
        {
            UserRoleDTO userRoleDto = new UserRoleDTO();
            UserRoleMaster userRoleMaster = new UserRoleMaster();
            userRoleDto.UserRoleId = userRoleId;
            userRoleDto.UserRoleName = userRoleMaster.GetUserRoleName(userRoleId);
            return userRoleDto;
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
                GF.LogError("clsUserMasterAmit.Insert", exp.Message);
            }
            return ds;
        }

        #endregion
    }
}
