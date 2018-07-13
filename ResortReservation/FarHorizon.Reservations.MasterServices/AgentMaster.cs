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
                oDB.DbDatabase.AddInParameter(oDB.DbCmd, "@sAgentName", DbType.String, oAgentData.AgentName);
                oDB.DbDatabase.AddInParameter(oDB.DbCmd, "@AgentEmailId", DbType.String, DataSecurityManager.Encrypt(oAgentData.EmailId));
                oDB.DbDatabase.AddInParameter(oDB.DbCmd, "@Password", DbType.String, DataSecurityManager.Encrypt(oAgentData.Password));
                oDB.DbDatabase.AddInParameter(oDB.DbCmd, "@Category", DbType.String, DataSecurityManager.Encrypt(oAgentData.category));
                oDB.DbDatabase.AddInParameter(oDB.DbCmd, "@Country", DbType.String, DataSecurityManager.Encrypt(oAgentData.country));
                oDB.DbDatabase.AddInParameter(oDB.DbCmd, "@localAgent", DbType.Byte, oAgentData.localagent);
<<<<<<< HEAD

                oDB.DbDatabase.AddInParameter(oDB.DbCmd, "@CssPath", DbType.String, oAgentData.CssPath);
                oDB.DbDatabase.AddInParameter(oDB.DbCmd, "@RedirectURL", DbType.String, oAgentData.RedirectURL);
                oDB.DbDatabase.AddInParameter(oDB.DbCmd, "@IsPaymentBypass",DbType.Boolean,(oAgentData.IsPaymentBypass));

                oDB.DbDatabase.AddInParameter(oDB.DbCmd, "@AgentURL", DbType.String, (oAgentData.AgentURL));
=======
>>>>>>> 06df147e7f6e76b3ddcb27473f8305164d96b955

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
        public bool ApiAuthInsert(AgentDTO oAgentData)
        {
            string sProcName;
            DatabaseManager oDB;
            try
            {
                oDB = new DatabaseManager();

                sProcName = "up_Ins_ApiAuth";
                oDB.DbCmd = oDB.GetStoredProcCommand(sProcName);
                oDB.DbCmd = oDB.GetStoredProcCommand(sProcName);
                oDB.DbDatabase.AddInParameter(oDB.DbCmd, "@AgentId", DbType.String, oAgentData.AgentId);
                oDB.DbDatabase.AddInParameter(oDB.DbCmd, "@TokenNo", DbType.String, oAgentData.TokenNo);
                oDB.ExecuteNonQuery(oDB.DbCmd);
            }
            catch (Exception exp)
            {
                GF.LogError("clsApiAuth.Insert", exp.Message.ToString());
                oDB = null;
                return false;
            }
            finally
            {
                oDB = null;
            }
            return true;
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
                oDB.DbDatabase.AddInParameter(oDB.DbCmd, "@sAgentName", DbType.String, oAgentData.AgentName);
                oDB.DbDatabase.AddInParameter(oDB.DbCmd, "@AgentEmailId", DbType.String, DataSecurityManager.Encrypt(oAgentData.EmailId));
                oDB.DbDatabase.AddInParameter(oDB.DbCmd, "@Password", DbType.String, DataSecurityManager.Encrypt(oAgentData.Password));
                oDB.DbDatabase.AddInParameter(oDB.DbCmd, "@Category", DbType.String, DataSecurityManager.Encrypt(oAgentData.category));
                oDB.DbDatabase.AddInParameter(oDB.DbCmd, "@Country", DbType.String, DataSecurityManager.Encrypt(oAgentData.country));
                oDB.DbDatabase.AddInParameter(oDB.DbCmd, "@localAgent", DbType.Byte, oAgentData.localagent);
<<<<<<< HEAD

                oDB.DbDatabase.AddInParameter(oDB.DbCmd, "@CssPath", DbType.String, oAgentData.CssPath);
                oDB.DbDatabase.AddInParameter(oDB.DbCmd, "@RedirectURL", DbType.String, oAgentData.RedirectURL);
                oDB.DbDatabase.AddInParameter(oDB.DbCmd, "@IsPaymentBypass", DbType.Boolean, (oAgentData.IsPaymentBypass));

                oDB.DbDatabase.AddInParameter(oDB.DbCmd, "@AgentURL", DbType.String, (oAgentData.AgentURL));

=======
>>>>>>> 06df147e7f6e76b3ddcb27473f8305164d96b955
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



        public AgentDTO[] GetRefAgentData()
        {
            List<AgentDTO> oAgentDataList = null;
            DataSet ds;

            string query = "select AgentId, AgentCode, AgentName, AgentEmailId,Password,Category,Country from (select distinct agentreftypeid from tblBooking) as a inner join tblAgentMaster b on a.AgentRefTypeId=b.AgentId";
            
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
                    oAgentData.AgentCode = Convert.ToString(row[1]);

                    actions.Add(new Action(() => oAgentData.AgentName = DataSecurityManager.Decrypt(Convert.ToString(row[2]))));
                    actions.Add(new Action(() => oAgentData.EmailId = DataSecurityManager.Decrypt(Convert.ToString(row[3]))));
                    actions.Add(new Action(() => oAgentData.Password = DataSecurityManager.Decrypt(Convert.ToString(row[4]))));
                    actions.Add(new Action(() => oAgentData.category = DataSecurityManager.Decrypt(Convert.ToString(row[5]))));
                    actions.Add(new Action(() => oAgentData.country = DataSecurityManager.Decrypt(Convert.ToString(row[6]))));
                 //   actions.Add(new Action(() => oAgentData.localagent = Convert.ToByte(row[7])));

                    oAgentDataList.Add(oAgentData);
                }

                ParallelOptions po = new ParallelOptions();
                po.MaxDegreeOfParallelism = 100;

                Parallel.Invoke(po, actions.ToArray());
            }
            return oAgentDataList.OrderBy(a => a.AgentName).ToArray();
        }


        public AgentDTO[] GetData(int AgentId)
        {
            List<AgentDTO> oAgentDataList = null;
            DataSet ds;

<<<<<<< HEAD
            //string query = "select AgentId, AgentCode, AgentName, AgentEmailId,Password,Category,Country,'localAgent'=case when localAgent is null or localAgent=0 then '0' else 1 end  from tblAgentMaster where 1=1 ";
            string query = "select AgentId, AgentCode, AgentName, AgentEmailId,Password,Category,Country,'localAgent'=case when localAgent is null or localAgent=0 then '0' else 1 end ,csspath,redirecturl,'ispaymentbypass'=case when ispaymentbypass is null or ispaymentbypass=0 then '0' else 1 end,agenturl   from tblAgentMaster where 1=1 ";
=======
            string query = "select AgentId, AgentCode, AgentName, AgentEmailId,Password,Category,Country,'localAgent'=case when localAgent is null or localAgent=0 then '0' else 1 end  from tblAgentMaster where 1=1 ";
>>>>>>> 06df147e7f6e76b3ddcb27473f8305164d96b955
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
                    oAgentData.AgentCode = Convert.ToString(row[1]);

                    actions.Add(new Action(() => oAgentData.AgentName = DataSecurityManager.Decrypt(Convert.ToString(row[2]))));
                    actions.Add(new Action(() => oAgentData.EmailId = DataSecurityManager.Decrypt(Convert.ToString(row[3]))));
                    actions.Add(new Action(() => oAgentData.Password = DataSecurityManager.Decrypt(Convert.ToString(row[4]))));
                    actions.Add(new Action(() => oAgentData.category = DataSecurityManager.Decrypt(Convert.ToString(row[5]))));
                    actions.Add(new Action(() => oAgentData.country = DataSecurityManager.Decrypt(Convert.ToString(row[6]))));
                    actions.Add(new Action(() => oAgentData.localagent = Convert.ToByte(row[7])));
<<<<<<< HEAD

                    actions.Add(new Action(() => oAgentData.CssPath = Convert.ToString(row[8])));
                    actions.Add(new Action(() => oAgentData.RedirectURL = Convert.ToString(row[9])));
                    actions.Add(new Action(() => oAgentData.AgentURL = Convert.ToString(row[11])));
                    actions.Add(new Action(() => oAgentData.IsPaymentBypass = Convert.ToByte(row[10])));
                    //actions.Add(new Action(() => oAgentData.IsPaymentBypass = Convert.ToBoolean(row[10] == null ? false : Convert.ToBoolean(row[10]))));
=======
>>>>>>> 06df147e7f6e76b3ddcb27473f8305164d96b955

                    oAgentDataList.Add(oAgentData);
                }

                ParallelOptions po = new ParallelOptions();
                po.MaxDegreeOfParallelism = 100;

                Parallel.Invoke(po, actions.ToArray());
            }
            return oAgentDataList.OrderBy(a => a.AgentName).ToArray();
        }

        public AgentDTO[] GetApiAuth(int AgentId)
        {
            List<AgentDTO> oAgentDataList = null;
            DataSet ds;

            string query = "select AgentId,TokenNo from TblApiAuth where 1=1 ";
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
                    
                    actions.Add(new Action(() => oAgentData.TokenNo =Convert.ToString(row[1])));
                  

                    oAgentDataList.Add(oAgentData);
                }

                ParallelOptions po = new ParallelOptions();
                po.MaxDegreeOfParallelism = 100;

                Parallel.Invoke(po, actions.ToArray());
            }
            return oAgentDataList.OrderBy(a => a.TokenNo).ToArray();
        }


        public AgentDTO[] GetTokenDetails()
        {
            List<AgentDTO> oAgentDataList = null;
            DataSet ds;

            string query = "select auth.AgentId,agent.AgentName,TokenNo from TblApiAuth auth left join tblAgentMaster agent on auth.AgentId=agent.AgentId where 1=1 ";
           

            ds = GetDataFromDB(query);
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                List<Action> actions = new List<Action>();
                oAgentDataList = new List<AgentDTO>();

                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    AgentDTO oAgentData = new AgentDTO();
                    oAgentData.AgentId = Convert.ToInt32(row[0]);
                    oAgentData.AgentName= Convert.ToString(row[1]);
                    actions.Add(new Action(() => oAgentData.TokenNo = Convert.ToString(row[2])));


                    oAgentDataList.Add(oAgentData);
                }

                ParallelOptions po = new ParallelOptions();
                po.MaxDegreeOfParallelism = 100;

                Parallel.Invoke(po, actions.ToArray());
            }
            return oAgentDataList.OrderBy(a => a.TokenNo).ToArray();
        }

        //public AgentDTO[] GetData1()
        //{
        //    return GetData1(0);
        //}

        public AgentDTO[] GetData1(int AgentId)
        {
            List<AgentDTO> oAgentDataList = null;
            DataSet ds;

            // string query = "select AgentId, AgentCode, AgentName, AgentEmailId,Password,Category,Country,'localAgent'=case when localAgent is null or localAgent=0 then '0' else 1 end  from tblAgentMaster where 1=1 ";

            string query = "select Distinct ta.AgentId, AgentCode, AgentName, AgentEmailId,Password,Category,Country,'localAgent'=case when localAgent is null or localAgent=0 then '0' else 1 end,td.AgentIdRef  from tblPackageRateCardDetails td inner join tblAgentMaster  ta on td.AgentIdRef=ta.AgentId where td.AgentIdRef is not null and td.AgentId is  null";
            if (AgentId != 0)
            {
                query += " or td.AgentId=" + AgentId;
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
                    oAgentData.AgentCode = Convert.ToString(row[1]);

                    actions.Add(new Action(() => oAgentData.AgentName = DataSecurityManager.Decrypt(Convert.ToString(row[2]))));
                    actions.Add(new Action(() => oAgentData.EmailId = DataSecurityManager.Decrypt(Convert.ToString(row[3]))));
                    actions.Add(new Action(() => oAgentData.Password = DataSecurityManager.Decrypt(Convert.ToString(row[4]))));
                    actions.Add(new Action(() => oAgentData.category = DataSecurityManager.Decrypt(Convert.ToString(row[5]))));
                    actions.Add(new Action(() => oAgentData.country = DataSecurityManager.Decrypt(Convert.ToString(row[6]))));
                    actions.Add(new Action(() => oAgentData.localagent = Convert.ToByte(row[7])));

                    oAgentDataList.Add(oAgentData);
                }

                ParallelOptions po = new ParallelOptions();
                po.MaxDegreeOfParallelism = 100;

                Parallel.Invoke(po, actions.ToArray());
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
