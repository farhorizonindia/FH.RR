using FarHorizon.DataSecurity;
using FarHorizon.Reservations.BusinessServices.Online.BAL;
using FarHorizon.Reservations.Common.DataEntities.Masters;
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace FarHorizon.Reservations.BusinessServices.Online.DAL
{
    /// <summary>
    /// Summary description for DALLogin
    /// </summary>
    public class DALLogin
    {
        string strCon = string.Empty;
        public DALLogin()
        {
            strCon = ConfigurationManager.ConnectionStrings["ReservationConnectionString"].ConnectionString;
        }

        #region Login

        public AgentDTO AgentLogin(BALLogin obj)
        {
            try
            {
                SqlConnection cn = new SqlConnection(strCon);
                string query = "select AgentId, AgentCode, AgentName, AgentEmailId, Password from tblAgentMaster where AgentEmailId='" + DataSecurityManager.Encrypt(obj.EmailId) + "' and [password]='" + DataSecurityManager.Encrypt(obj.Password) + "'";
                                
                SqlCommand cmd = new SqlCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = query;
                cmd.Connection = cn;
                cn.Open();

                AgentDTO agent = null;
                SqlDataReader reader = cmd.ExecuteReader();

                DataTable dt = new DataTable();
                dt.Load(reader);

                if (dt != null && dt.Rows.Count > 0)
                {
                    agent = new AgentDTO();
                    agent.AgentId = dt.Rows[0]["AgentId"] != null ? Convert.ToInt32(dt.Rows[0]["AgentId"]) : -1;
                    agent.AgentCode = dt.Rows[0]["AgentCode"] != null ? dt.Rows[0]["AgentCode"].ToString() : string.Empty;
                    agent.AgentName = dt.Rows[0]["AgentName"] != null ? DataSecurityManager.Decrypt(dt.Rows[0]["AgentName"].ToString()) : string.Empty;
                    agent.EmailId = dt.Rows[0]["AgentEmailId"] != null ? DataSecurityManager.Decrypt(dt.Rows[0]["AgentEmailId"].ToString()) : string.Empty;
                    agent.Password = dt.Rows[0]["Password"] != null ? DataSecurityManager.Decrypt(dt.Rows[0]["Password"].ToString()) : string.Empty;
                }
                reader.Close();               
                cn.Close();

                return agent;
            }
            catch (Exception exp)
            {
                throw exp;                
            }
        }
        #endregion
    }
}