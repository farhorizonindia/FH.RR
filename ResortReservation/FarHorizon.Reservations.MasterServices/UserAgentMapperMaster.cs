using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using FarHorizon.Reservations.DataBaseManager;
using FarHorizon.Reservations.Common.DataEntities.Masters;
using FarHorizon.Reservations.Common;
using FarHorizon.DataSecurity;

namespace FarHorizon.Reservations.MasterServices
{
    public class UserAgentMapperMaster
    {
        #region IMaster Members

        public bool Insert(UserAgentMapperDTO userAgentData)
        {
            string sProcName;
            DatabaseManager oDB;
            try
            {                
                sProcName = "[up_Ins_UserAgentMaster]";
                oDB = new DatabaseManager();                

                foreach (AgentDTO agent in userAgentData.AgentList)
                {
                    oDB.DbCmd = null;
                    oDB.DbCmd = oDB.GetStoredProcCommand(sProcName);
                    oDB.DbDatabase.AddInParameter(oDB.DbCmd, "@UserId", DbType.String, userAgentData.UserId);
                    oDB.DbDatabase.AddInParameter(oDB.DbCmd, "@AgentId", DbType.Int32, agent.AgentId);
                    oDB.ExecuteNonQuery(oDB.DbCmd);
                }
            }
            catch (Exception exp)
            {
                oDB = null;
                userAgentData = null;
                GF.LogError("clsUserAgentMapperMaster.Insert", exp.Message);
                return false;
            }
            finally
            {
                oDB = null;
                userAgentData = null;
            }
            return true;
        }
        
        public bool Delete(UserAgentMapperDTO userAgentData)
        {
            string sProcName;
            DatabaseManager oDB;
            try
            {
                oDB = new DatabaseManager();
                sProcName = "up_Del_UserAgentMaster";

                foreach (AgentDTO agent in userAgentData.AgentList)
                {
                    oDB.DbCmd = oDB.GetStoredProcCommand(sProcName);
                    oDB.DbDatabase.AddInParameter(oDB.DbCmd, "@UserId", DbType.String, userAgentData.UserId);
                    oDB.DbDatabase.AddInParameter(oDB.DbCmd, "@AgentId", DbType.Int32, agent.AgentId);
                    oDB.ExecuteNonQuery(oDB.DbCmd);
                }
            }
            catch (Exception exp)
            {
                oDB = null;
                userAgentData = null;
                GF.LogError("UserAgentMaster.Delete", exp.Message);
                return false;
            }
            finally
            {
                oDB = null;
            }
            return true;
        }

        public UserAgentMapperDTO GetUnHookedAgents(String userId)
        {
            String query = "select u.UserId, a.AgentId, a.AgentName from tblUserMaster u " +
                           " left join tblUserAgentMapperMaster m on u.UserId = m.UserId " +
                           " right join tblAgentMaster a on m.AgentId = a.AgentId " +
                           " where (m.UserId != '" + userId.Trim() + "' or m.UserId is null) order by a.AgentName ";
            return GetAgents(query);
        }

        public UserAgentMapperDTO GetHookedAgents(String userId)
        {
            String query = "select u.UserId, a.AgentId, a.AgentName from tblUserMaster u " +
                           " left join tblUserAgentMapperMaster m on u.UserId = m.UserId " +
                           " right join tblAgentMaster a on m.AgentId = a.AgentId " +
                           " where (m.UserId = '" + userId.Trim() + "') order by a.AgentName ";
            return GetAgents(query);
        }

        public UserDTO[] Getusers()
        {
            UserMaster userMaster = new UserMaster();
            return userMaster.GetUsers();
        }

        private UserAgentMapperDTO GetAgents(String query)
        {
            UserAgentMapperDTO userAgentMapperDTO = new UserAgentMapperDTO();
            List<AgentDTO> hookedAgentList = new List<AgentDTO>();
            AgentDTO hookedAgent = new AgentDTO();
            DataSet ds;

            ds = GetDataFromDB(query);
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    hookedAgent = new AgentDTO();
                    hookedAgent.AgentId = ds.Tables[0].Rows[i][1] == DBNull.Value ? 0 : Convert.ToInt32(ds.Tables[0].Rows[i][1]);
                    hookedAgent.AgentName = ds.Tables[0].Rows[i][2] == DBNull.Value ? String.Empty : DataSecurityManager.Decrypt( Convert.ToString(ds.Tables[0].Rows[i][2]));
                    hookedAgentList.Add(hookedAgent);
                }
            }
            userAgentMapperDTO.AgentList = hookedAgentList;
            return userAgentMapperDTO;
        }

        public AgentUserMapperDTO GetAgentUserEmailIds(int bookingId)
        {
            AgentDTO agentDto =  GetBookingAgent(bookingId);
            UserDTO[] userDto  = null;
            if (agentDto != null)
            {
                userDto = GetAgentUsers(agentDto.AgentId);
            }

            AgentUserMapperDTO agentUserMapperDTO = new AgentUserMapperDTO();
            agentUserMapperDTO.Agent = agentDto;
            if (userDto != null)
            {
                agentUserMapperDTO.UserList = new List<UserDTO>(userDto);
            }
            return agentUserMapperDTO;
        }

        private UserDTO[] GetAgentUsers(int agentId)
        {
            UserDTO[] oUserData;
            oUserData = null;
            DataSet ds;
            String query = "select UserId, UserName, Password, Active, Administrator, UserRoleId, userEmailId from tbluserMaster where 1=1";
            query += " and RTrim(LTrim(UserId)) in (select RTrim(LTrim(UserId)) from tblUserAgentMapperMaster where AgentId = " + agentId + ")";
            query += " order by userEmailId";
            
            ds = GetDataFromDB(query);
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                oUserData = new UserDTO[ds.Tables[0].Rows.Count];
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    oUserData[i] = new UserDTO();
                    oUserData[i].UserId = Convert.ToString(ds.Tables[0].Rows[i][0]);
                    oUserData[i].UserName = Convert.ToString(ds.Tables[0].Rows[i][1]);
                    oUserData[i].Password = Convert.ToString(ds.Tables[0].Rows[i][2]);
                    oUserData[i].Active = Convert.ToBoolean(ds.Tables[0].Rows[i][3]);
                    oUserData[i].Administrator = Convert.ToBoolean(ds.Tables[0].Rows[i][4]);
                    oUserData[i].EmailId = ds.Tables[0].Rows[i][6] != DBNull.Value ? Convert.ToString(ds.Tables[0].Rows[i][6]) : String.Empty;                    
                }
            }
            return oUserData;
        }

        private AgentDTO GetBookingAgent(int bookingId)
        {
            AgentDTO[] oAgentData = null;            
            string query = "select AgentId, AgentCode, AgentName, AgentEmailId from tblAgentMaster where 1=1 ";
            query += " and AgentId = (select AgentId from tblBooking where BookingId = " + bookingId + ")";
            query += " order by AgentEmailId";

            DataSet ds;
            ds = GetDataFromDB(query);
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                oAgentData = new AgentDTO[ds.Tables[0].Rows.Count];
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    oAgentData[i] = new AgentDTO();
                    oAgentData[i].AgentId = Convert.ToInt32(ds.Tables[0].Rows[i][0]);
                    oAgentData[i].AgentCode = Convert.ToString(ds.Tables[0].Rows[i][1]);
                    oAgentData[i].AgentName = Convert.ToString(ds.Tables[0].Rows[i][2]);
                    oAgentData[i].EmailId = Convert.ToString(ds.Tables[0].Rows[i][3]);
                }
            }

            if (oAgentData != null && oAgentData.Length > 0)
            {
                return oAgentData[0];
            }
            else
            {
                return null;
            }
        }

        private DataSet GetDataFromDB(String query)
        {
            DatabaseManager oDB = new DatabaseManager();
            DataSet ds = null;
            try
            {
                oDB.DbCmd = oDB.GetSqlStringCommand(query);
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
