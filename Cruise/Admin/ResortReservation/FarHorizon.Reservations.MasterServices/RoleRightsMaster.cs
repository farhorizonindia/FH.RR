using System;
using System.Collections.Generic;
using System.Text;
using FarHorizon.Reservations.DataBaseManager;
using System.Data;
using FarHorizon.Reservations.Common;
using FarHorizon.Reservations.Common.DataEntities.Masters;

namespace FarHorizon.Reservations.MasterServices
{
    public class RoleRightsMaster 
    {
        #region IMaster Members

        public bool Insert(List<RoleRightsDTO> roleRights)
        {
            string sProcName;
            DatabaseManager oDB;
            try
            {
                if (roleRights != null && roleRights.Count > 0)
                {
                    Delete(roleRights[0]);                 
                }

                oDB = new DatabaseManager();
                sProcName = "up_Ins_RoleRightsMaster";

                foreach (RoleRightsDTO roleRight in roleRights)
                {
                    oDB.DbCmd = oDB.GetStoredProcCommand(sProcName);
                    oDB.DbDatabase.AddInParameter(oDB.DbCmd, "@roleId", DbType.String, roleRight.RoleId);
                    oDB.DbDatabase.AddInParameter(oDB.DbCmd, "@roleName", DbType.String, roleRight.RoleName);
                    oDB.DbDatabase.AddInParameter(oDB.DbCmd, "@screenName", DbType.String, roleRight.ScreenName);
                    oDB.DbDatabase.AddInParameter(oDB.DbCmd, "@rightName", DbType.String, roleRight.RightName);
                    oDB.DbDatabase.AddInParameter(oDB.DbCmd, "@rightKey", DbType.String, roleRight.RightKey);
                    oDB.ExecuteNonQuery(oDB.DbCmd);
                }
            }
            catch (Exception exp)
            {
                oDB = null;
                GF.LogError("RoleRightsMaster.Insert", exp.Message);
                throw exp;
            }
            finally
            {
                oDB = null;
            }
            return true;
        }

        public bool Update(List<RoleRightsDTO> roleRights)
        {
            try
            {
                if (roleRights != null && roleRights.Count > 0)
                {
                    Delete(roleRights[0]);
                    Insert(roleRights);
                }
            }
            catch (Exception exp)
            {
                throw exp;
            }
            return true;
        }

        public bool Delete(RoleRightsDTO roleRights)
        {
            string sProcName;
            DatabaseManager oDB;
            try
            {
                oDB = new DatabaseManager();

                sProcName = "up_Del_RoleRightsMaster";
                oDB.DbCmd = oDB.GetStoredProcCommand(sProcName);
                oDB.DbDatabase.AddInParameter(oDB.DbCmd, "@roleId", DbType.Int32, roleRights.RoleId);
                oDB.ExecuteNonQuery(oDB.DbCmd);
            }
            catch (Exception exp)
            {
                oDB = null;
                GF.LogError("RoleRightsMaster.Delete", exp.Message);
                throw exp;
            }
            finally
            {
                oDB = null;
            }
            return true;
        }

        public List<RoleRightsDTO> GetRoleRights(int roleId)
        {
            return GetData(roleId);
        }

        public List<RoleRightsDTO> GetData(int roleId)
        {
            List<RoleRightsDTO> roleRightsList = null;
            RoleRightsDTO roleRight;
            DataSet ds;

            string query = "select RoleId, RoleName, ScreenName, RightName, RightKey from tblRoleRightsMaster where 1=1 ";
            if (roleId != 0)
            {
                query += " and RoleId =" + roleId;
            }
            query += " order by RoleName, ScreenName, RightName";

            try
            {
                ds = GetDataFromDB(query);
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    roleRightsList = new List<RoleRightsDTO>();
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        roleRight = new RoleRightsDTO();
                        roleRight.RoleId = Convert.ToInt32(ds.Tables[0].Rows[i][0]);
                        roleRight.RoleName = Convert.ToString(ds.Tables[0].Rows[i][1]);
                        roleRight.ScreenName = Convert.ToString(ds.Tables[0].Rows[i][2]);
                        roleRight.RightName = Convert.ToString(ds.Tables[0].Rows[i][3]);
                        roleRight.RightKey = Convert.ToString(ds.Tables[0].Rows[i][4]);
                        roleRightsList.Add(roleRight);
                    }
                }
            }
            catch (Exception exp)
            {
                throw exp;
            }
            return roleRightsList;
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
                GF.LogError("RoleRightsMaster.GetDataFromDB", exp.Message);
                oDB = null;
                ds = null;
            }
            return ds;
        }

        #endregion
    }
}
