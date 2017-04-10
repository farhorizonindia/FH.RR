using System;
using System.Linq;
using System.Collections;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using FarHorizon.Reservations.Common;
using FarHorizon.Reservations.DataBaseManager;
using FarHorizon.Reservations.Common.DataEntities.Masters;
using FarHorizon.DataSecurity;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FarHorizon.Reservations.MasterServices
{
    public class AgentMaster
    {
        #region IMaster Members

        public int Insert(AgentDTO oAgentData)
        {
            DatabaseManager oDB;
            int agentId = -1;
            try
            {                
                oDB = new DatabaseManager();             
                string sProcName = "up_Ins_AgentMaster";
                oDB.DbCmd = oDB.GetStoredProcCommand(sProcName);
                oDB.DbDatabase.AddInParameter(oDB.DbCmd, "@sAgentCode", DbType.String, oAgentData.AgentCode);
                //oDB.DbDatabase.AddInParameter(oDB.DbCmd, "@sAgentName", DbType.String, DataSecurityManager.Encrypt(oAgentData.AgentName));
                oDB.DbDatabase.AddInParameter(oDB.DbCmd, "@sAgentName", DbType.String, oAgentData.AgentName);
                oDB.DbDatabase.AddInParameter(oDB.DbCmd, "@AgentEmailId", DbType.String, DataSecurityManager.Encrypt(oAgentData.EmailId));
                oDB.DbDatabase.AddInParameter(oDB.DbCmd, "@Password", DbType.String, DataSecurityManager.Encrypt(oAgentData.Password));

                agentId = Convert.ToInt32(oDB.ExecuteScalar(oDB.DbCmd));
                //oDB.ExecuteNonQuery(oDB.DbCmd);
            }
            catch (Exception exp)
            {
                oDB = null;
                GF.LogError("clsAgentMaster.Insert", exp.Message);
                return -1;
            }
            finally
            {
                oDB = null;
            }
            return agentId;
        }

        public bool Update(AgentDTO oAgentData)
        {
            DatabaseManager oDB;
            try
            {
                oDB = new DatabaseManager();
                string sProcName = "up_Upd_AgentMaster";
                oDB.DbCmd = oDB.GetStoredProcCommand(sProcName);
                oDB.DbDatabase.AddInParameter(oDB.DbCmd, "@AgentId", DbType.Int32, oAgentData.AgentId);
                oDB.DbDatabase.AddInParameter(oDB.DbCmd, "@sAgentCode", DbType.String, oAgentData.AgentCode);
                //oDB.DbDatabase.AddInParameter(oDB.DbCmd, "@sAgentName", DbType.String, DataSecurityManager.Encrypt(oAgentData.AgentName));
                oDB.DbDatabase.AddInParameter(oDB.DbCmd, "@sAgentName", DbType.String, oAgentData.AgentName);
                oDB.DbDatabase.AddInParameter(oDB.DbCmd, "@AgentEmailId", DbType.String, DataSecurityManager.Encrypt(oAgentData.EmailId));
                oDB.DbDatabase.AddInParameter(oDB.DbCmd, "@Password", DbType.String, DataSecurityManager.Encrypt(oAgentData.Password));
                oDB.ExecuteNonQuery(oDB.DbCmd);
            }
            catch (Exception exp)
            {
                oDB = null;
                GF.LogError("clsAgentMaster.Update", exp.Message);
                return false;
            }
            finally
            {
                oDB = null;
            }
            return true;
        }

        public bool Delete(AgentDTO oAgentData)
        {
            string sProcName;
            DatabaseManager oDB;
            try
            {
                oDB = new DatabaseManager();

                sProcName = "up_Del_AgentMaster";
                oDB.DbCmd = oDB.GetStoredProcCommand(sProcName);
                oDB.DbDatabase.AddInParameter(oDB.DbCmd, "@AgentId", DbType.Int32, oAgentData.AgentId);
                oDB.ExecuteNonQuery(oDB.DbCmd);
            }
            catch (Exception exp)
            {
                oDB = null;
                GF.LogError("clsAgentMaster.Delete", exp.Message);
                return false;
            }
            finally
            {
                oDB = null;
            }
            return true;
        }

        public AgentDTO[] GetData()
        {
            return GetData(0);
        }

        public AgentDTO[] GetData(int AgentId)
        {
            List<AgentDTO> oAgentDataList = null;
            DataSet ds;

            //string query = "select AgentId, AgentCode, AgentName, AgentEmailId,Password from tblAgentMaster where 1=1 ";
            string query = "select AgentId, AgentName from tblAgentMaster where 1=1 ";
            if (AgentId != 0)
            {
                query += " and AgentId=" + AgentId;
            }
            //query += " order by AgentName";

            ds = GetDataFromDB(query);
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                List<Action> actions = new List<Action>();
                oAgentDataList = new List<AgentDTO>();

                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    AgentDTO oAgentData = new AgentDTO();
                    oAgentData.AgentId = Convert.ToInt32(row[0]);
                    //actions.Add(new Action(() => oAgentData.AgentName = DataSecurityManager.Decrypt(Convert.ToString(row[1]))));
                    oAgentData.AgentName = Convert.ToString(row[1]);
                    //oAgentData.AgentCode = Convert.ToString(row[1]);
                    //actions.Add(new Action(() => oAgentData.AgentName = DataSecurityManager.Decrypt(Convert.ToString(row[2]))));
                    //actions.Add(new Action(() => oAgentData.EmailId = DataSecurityManager.Decrypt(Convert.ToString(row[3]))));
                    //actions.Add(new Action(() => oAgentData.Password = DataSecurityManager.Decrypt(Convert.ToString(row[4]))));

                    oAgentDataList.Add(oAgentData);
                }

                //ParallelOptions po = new ParallelOptions();
                //po.MaxDegreeOfParallelism = 100;

                //Parallel.Invoke(po, actions.ToArray());                
            }
            return oAgentDataList.OrderBy(a => a.AgentName).ToArray();            
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
                GF.LogError("clsAgentMaster.GetDataFromDB", exp.Message);
                oDB = null;
                ds = null;
            }
            return ds;
        }
        #endregion
    }
}
