using FarHorizon.DataSecurity;
using FarHorizon.Reservations.Common.DataEntities.Masters;
using FarHorizon.Reservations.DataBaseManager;
using FarHorizon.Reservations.MasterServices;
using FarHorizon.Reservations.SessionManager;
using System;
using System.Collections.Generic;
using System.Data;

namespace FarHorizon.Reservations.UserManager
{
    internal class UserHandler
    {
        #region Other Method(s)
        public int ValidateUser(UserDTO oUserData)
        {
            DatabaseManager oDB;
            int iLoginId = 0;
            oDB = new DatabaseManager();
            try
            {                
                string sProcName = "up_ValidateUser";
                oDB.DbCmd = oDB.GetStoredProcCommand(sProcName);
                oDB.DbDatabase.AddInParameter(oDB.DbCmd, "@sUserId", DbType.String, oUserData.UserId);
                oDB.DbDatabase.AddInParameter(oDB.DbCmd, "@sPwd", DbType.String, DataSecurityManager.Encrypt(oUserData.Password));
                DataSet ds = oDB.ExecuteDataSet(oDB.DbCmd);
                if (ds != null)
                {
                    if (ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
                    {
                        iLoginId = Convert.ToInt32(ds.Tables[0].Rows[0][0].ToString());
                    }
                }
                if (iLoginId != 0)
                {
                    SaveUserInfoToSession(oUserData.UserId);
                }
            }
            catch (Exception exp)
            {
                throw exp;
            }
            return iLoginId;
        }

        private void SaveUserInfoToSession(string userId)
        {
            LoggedInUser LoggedInUser = new LoggedInUser();
            UserMaster userMaster = new UserMaster();
            UserDTO user;
            user = userMaster.GetUser(userId);

            LoggedInUser.User = user;
            if (user != null)
            {
                RoleRightsMaster roleRightsMaster = new RoleRightsMaster();
                List<RoleRightsDTO> roleRightsList = null;
                if (user.UserRoleData != null && user.UserRoleData.UserRoleId != 0)
                    roleRightsList = roleRightsMaster.GetRoleRights(user.UserRoleData.UserRoleId);
                LoggedInUser.RoleRigthsList = roleRightsList;
            }
            SessionHelper.LoggedInUser = LoggedInUser;
        }
        #endregion
    }
}
