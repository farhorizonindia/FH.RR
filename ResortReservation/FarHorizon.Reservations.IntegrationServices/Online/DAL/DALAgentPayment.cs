using FarHorizon.DataSecurity;
using FarHorizon.Reservations.BusinessServices.Online.BAL;
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace FarHorizon.Reservations.BusinessServices.Online.DAL
{
    public class DALAgentPayment
    {
        string strCon = string.Empty;
        public DALAgentPayment()
        {
            strCon = ConfigurationManager.ConnectionStrings["ReservationConnectionString"].ConnectionString;
        }

        #region Get Data
        public DataTable BindControls(BALAgentPayment obj)
        {
            try
            {
                SqlConnection cn = new SqlConnection(strCon);
                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = new SqlCommand("[dbo].[sp_agentPayement]", cn);
                da.SelectCommand.Parameters.Clear();
                da.SelectCommand.Parameters.AddWithValue("@Action", obj._Action);
                da.SelectCommand.Parameters.AddWithValue("@EmailId", obj._EmailId);

                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                cn.Open();
                da.SelectCommand.ExecuteReader();
                DataTable dtReturnData = new DataTable();
                cn.Close();
                da.Fill(dtReturnData);                

                if (dtReturnData != null)
                    return dtReturnData;
                else
                    return null;
            }
            catch (Exception)
            {
                return null;
            }
        }

        #endregion

        #region Insert / Update Data

        public int AddpaymentDetails(BALAgentPayment obj)
        {
            try
            {
                SqlConnection cn = new SqlConnection(strCon);
                SqlDataAdapter da = new SqlDataAdapter();
                da.InsertCommand = new SqlCommand("[dbo].[sp_agentPayement]", cn);
                da.InsertCommand.Parameters.AddWithValue("@Action", obj._Action);
                da.InsertCommand.Parameters.AddWithValue("@firstName", obj._FirstName);
                da.InsertCommand.Parameters.AddWithValue("@lastName", obj._LastName);
                da.InsertCommand.Parameters.AddWithValue("@EmailId", obj._EmailId);
                da.InsertCommand.Parameters.AddWithValue("@Password", obj._Password);
                da.InsertCommand.Parameters.AddWithValue("@AgentCode", obj._AgentCode);
                da.InsertCommand.Parameters.AddWithValue("@billingAddress", obj._BillingAddress);
                da.InsertCommand.Parameters.AddWithValue("@PaymentMethod", obj._PaymentMethod);
                da.InsertCommand.Parameters.AddWithValue("@OnCredit", obj.OnCredit == true ? 1 : 0);
                da.InsertCommand.Parameters.AddWithValue("@CreditLimit", obj.CreditLimit);
                da.InsertCommand.Parameters.AddWithValue("@phoneNumber", obj.Phone);
                da.InsertCommand.CommandType = CommandType.StoredProcedure;
                cn.Open();
                int Status = da.InsertCommand.ExecuteNonQuery();
                cn.Close();
                if (Status > 0)
                    return Status;
                else
                    return 0;
            }
            catch (Exception exp)
            {
                throw exp;
                //return 0;
            }
        }


        public int UpdatepaymentDetails(BALAgentPayment obj)
        {
            try
            {
                SqlConnection cn = new SqlConnection(strCon);
                SqlDataAdapter da = new SqlDataAdapter();
                SqlCommand cmd = new SqlCommand("[dbo].[sp_agentPayement]", cn);
                cmd.Parameters.AddWithValue("@Action", obj._Action);
                cmd.Parameters.AddWithValue("@firstName", obj._FirstName);
                cmd.Parameters.AddWithValue("@lastName", obj._LastName);
                cmd.Parameters.AddWithValue("@EmailId", obj._EmailId);
                cmd.Parameters.AddWithValue("@Password", obj._Password);
                cmd.Parameters.AddWithValue("@AgentCode", obj._AgentCode);
                cmd.Parameters.AddWithValue("@billingAddress", obj._BillingAddress);
                cmd.Parameters.AddWithValue("@PaymentMethod", obj._PaymentMethod);
                cmd.Parameters.AddWithValue("@OnCredit", obj.OnCredit);
                cmd.Parameters.AddWithValue("@CreditLimit", obj.CreditLimit);
                cmd.Parameters.AddWithValue("@phoneNumber", obj.Phone);

                cmd.CommandType = CommandType.StoredProcedure;
                cn.Open();
                int Status = cmd.ExecuteNonQuery();
                cn.Close();
                if (Status > 0)
                    return Status;
                else
                    return 0;
            }
            catch (Exception exp)
            {
                throw exp;
            }
        }

        #endregion

        #region getagentmasterinfo
        public DataTable getagentmasterinfo(int AgentId)
        {

            try
            {
                SqlDataReader dr; ;
                SqlConnection cn = new SqlConnection(strCon);

                string query = "select AgentId, AgentCode, AgentName, AgentEmailId,Password from tblAgentMaster where 1=1 ";
                if (AgentId != 0)
                {
                    query += " and AgentId=" + AgentId;
                }
                query += " order by AgentName";
                SqlDataAdapter sda = new SqlDataAdapter(query, cn);

                cn.Open();

                sda.SelectCommand.ExecuteReader();
                cn.Close();
                DataTable dt = new DataTable();
                sda.Fill(dt);


                if (dt != null)
                {

                    if (dt.Rows.Count > 0)
                    {
                        return dt;
                    }
                    else
                    {
                        return null;
                    }
                }
                else
                {
                    return null;
                }
            }
            catch
            {
                return null;
            }
        }

        #endregion

        public DataTable AgentInfo(BALAgentPayment obj)
        {
            try
            {
                SqlConnection cn = new SqlConnection(strCon);
                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = new SqlCommand("[dbo].[sp_agentPayement]", cn);
                da.SelectCommand.Parameters.Clear();
                da.SelectCommand.Parameters.AddWithValue("@Action", obj._Action);
                da.SelectCommand.Parameters.AddWithValue("@marketid", obj.MarketId);

                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                cn.Open();
                da.SelectCommand.ExecuteReader();
                DataTable dtReturnData = new DataTable();
                cn.Close();
                da.Fill(dtReturnData);

                if (dtReturnData != null && dtReturnData.Rows.Count > 0)
                {
                    foreach (DataRow row in dtReturnData.Rows)
                    {
                        row["AgentName"] = DataSecurityManager.Decrypt(row["AgentName"].ToString());
                        row["AgentEmailId"] = DataSecurityManager.Decrypt(row["AgentEmailId"].ToString());
                    }
                }

                if (dtReturnData != null)
                    return dtReturnData;
                else
                    return null;
            }
            catch (Exception exp)
            {
                return null;
            }
        }


        public DataTable GetAgentPaymentInfo(BALAgentPayment obj)
        {
            try
            {
                SqlConnection cn = new SqlConnection(strCon);
                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = new SqlCommand("[dbo].[sp_agentPayement]", cn);
                da.SelectCommand.Parameters.Clear();
                da.SelectCommand.Parameters.AddWithValue("@Action", obj._Action);
                da.SelectCommand.Parameters.AddWithValue("@AgentCode", obj.agentid);

                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                cn.Open();
                da.SelectCommand.ExecuteReader();
                DataTable dtReturnData = new DataTable();
                cn.Close();
                da.Fill(dtReturnData);
                if (dtReturnData != null)
                    return dtReturnData;
                else
                    return null;
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}