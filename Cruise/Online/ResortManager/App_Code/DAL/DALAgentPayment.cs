using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Configuration;
using System.Data.SqlClient;
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
            da.InsertCommand.CommandType = CommandType.StoredProcedure;
            cn.Open();
            int Status = da.InsertCommand.ExecuteNonQuery();
            cn.Close();
            if (Status > 0)
                return Status;
            else
                return 0;
        }
        catch (Exception)
        {
            return 0;
        }
    }
    #endregion
}