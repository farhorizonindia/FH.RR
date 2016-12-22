using System;
using System.Collections.Generic;
using System.Text;
using FarHorizon.Reservations.Common.DataEntities.Masters;
using FarHorizon.Reservations.DataBaseManager;
using System.Data;
using FarHorizon.Reservations.Common;

namespace FarHorizon.Reservations.MasterServices
{
    public class UserRoleMaster 
    {
        #region IMaster Members

        public bool Insert(UserRoleDTO oUserRoleData)
        {
            string sProcName;
            DatabaseManager oDB;
            try
            {
                oDB = new DatabaseManager();

                sProcName = "up_Ins_UserRoleMaster";
                oDB.DbCmd = oDB.GetStoredProcCommand(sProcName);
                oDB.DbDatabase.AddInParameter(oDB.DbCmd, "@UserRoleName", DbType.String, oUserRoleData.UserRoleName);
                oDB.ExecuteNonQuery(oDB.DbCmd);
            }
            catch (Exception exp)
            {
                oDB = null;
                oUserRoleData = null;
                GF.LogError("clsUserRoleMaster.Insert", exp.Message);
                return false;
            }
            finally
            {
                oUserRoleData = null;
                oDB = null;
            }
            return true;
        }

        public bool Update(UserRoleDTO oUserRoleData)
        {
            string sProcName;
            DatabaseManager oDB;
            try
            {
                oDB = new DatabaseManager();
                sProcName = "up_Upd_UserRoleMaster";
                oDB.DbCmd = oDB.GetStoredProcCommand(sProcName);
                oDB.DbDatabase.AddInParameter(oDB.DbCmd, "@UserRoleId ", DbType.Int32, oUserRoleData.UserRoleId);
                oDB.DbDatabase.AddInParameter(oDB.DbCmd, "@UserRoleName", DbType.String, oUserRoleData.UserRoleName);
                oDB.ExecuteNonQuery(oDB.DbCmd);
            }
            catch (Exception exp)
            {
                oDB = null;
                oUserRoleData = null;
                GF.LogError("clsUserRoleMaster.Update", exp.Message);
                return false;
            }
            finally
            {
                oDB = null;
                oUserRoleData = null;
            }
            return true;
        }

        public bool Delete(UserRoleDTO oUserRoleData)
        {
            string sProcName;
            DatabaseManager oDB;
            try
            {
                oDB = new DatabaseManager();

                sProcName = "up_Del_UserRoleMaster";
                oDB.DbCmd = oDB.GetStoredProcCommand(sProcName);
                oDB.DbDatabase.AddInParameter(oDB.DbCmd, "@UserRoleId", DbType.Int32, oUserRoleData.UserRoleId);
                oDB.ExecuteNonQuery(oDB.DbCmd);
            }
            catch (Exception exp)
            {
                oDB = null;
                oUserRoleData = null;
                GF.LogError("clsUserRoleMaster.Delete", exp.Message);
                return false;
            }
            finally
            {
                oDB = null;
                oUserRoleData = null;
            }
            return true;
        }

        public UserRoleDTO[] GetUserRoles()
        {
            return GetData(0);
        }

        public UserRoleDTO GetUserRole(int UserRoleId)
        {
            UserRoleDTO[] userRoleDTO;
            userRoleDTO = GetData(UserRoleId);
            if (userRoleDTO != null && userRoleDTO.Length > 0)
            {
                return userRoleDTO[0];
            }
            return null;
        }

        private UserRoleDTO[] GetData(int UserRoleId)
        {
            UserRoleDTO[] oUserRoleData;
            oUserRoleData = null;
            DataSet ds;
            string query = "select UserRoleId, UserRoleName from tblUserRoleMaster where 1=1";
            if (UserRoleId != 0)
            {
                query += " and UserRoleId=" + UserRoleId;
                query += " order by UserRoleName";
            }
            ds = GetDataFromDB(query);
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                oUserRoleData = new UserRoleDTO[ds.Tables[0].Rows.Count];
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    oUserRoleData[i] = new UserRoleDTO();
                    oUserRoleData[i].UserRoleId = Convert.ToInt32(ds.Tables[0].Rows[i][0]);
                    oUserRoleData[i].UserRoleName = Convert.ToString(ds.Tables[0].Rows[i][1]);
                }
            }
            return oUserRoleData;
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
                GF.LogError("clsUserRoleMaster.GetDataFromDB", exp.Message);
            }
            return ds;
        }

        public string GetUserRoleName(int userRoleId)
        {
            string userRoleName = string.Empty;
            UserRoleDTO[] userRoleList = GetData(userRoleId);
            if (userRoleList != null && userRoleList.Length > 0)
            {
                userRoleName = userRoleList[0].UserRoleName;
            }
            return userRoleName;
        }

        #endregion
    }
}
