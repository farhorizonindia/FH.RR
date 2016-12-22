using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
public class DALLinks
{
    string strCon = string.Empty;
	public DALLinks()
	{
        strCon = ConfigurationManager.ConnectionStrings["ReservationConnectionString"].ConnectionString;

	}

    #region GetData
    public DataTable BindControls(BALLinks obj)
    {
        try
        {
            SqlConnection cn = new SqlConnection(strCon);
           
            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = new SqlCommand("[dbo].[sp_Links]", cn);
            da.SelectCommand.Parameters.Clear();
            da.SelectCommand.Parameters.AddWithValue("@Action", obj._Action);
            da.SelectCommand.Parameters.AddWithValue("@marketId", obj._MarketId);
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
    public DataTable BindControlsAgent(BALLinks obj)
    {
        try
        {
            SqlConnection cn = new SqlConnection(strCon);

            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = new SqlCommand("[dbo].[sp_Links]", cn);
            da.SelectCommand.Parameters.Clear();
            da.SelectCommand.Parameters.AddWithValue("@Action", obj._Action);
            da.SelectCommand.Parameters.AddWithValue("@AgentID", obj._Agent);
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


    #region insert/Update Data
    public int InsertMappedRelation(BALLinks obj)
    {
        try
        {
            SqlConnection cn = new SqlConnection(strCon);
            SqlDataAdapter da = new SqlDataAdapter();
            da.InsertCommand = new SqlCommand("[dbo].[sp_Links]", cn);
            da.InsertCommand.Parameters.AddWithValue("@Action", obj._Action);
            da.InsertCommand.Parameters.AddWithValue("@marketId", obj._MarketId);
            da.InsertCommand.Parameters.AddWithValue("@RateCategory", obj._CateId);
            da.InsertCommand.Parameters.AddWithValue("@Discount", obj._discount);
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
    public int InsertMappedRelationAgent(BALLinks obj)
    {
        try
        {
            SqlConnection cn = new SqlConnection(strCon);
            SqlDataAdapter da = new SqlDataAdapter();
            da.InsertCommand = new SqlCommand("[dbo].[sp_Links]", cn);
            da.InsertCommand.Parameters.AddWithValue("@Action", obj._Action);
            da.InsertCommand.Parameters.AddWithValue("@AgentID", obj._Agent);
            da.InsertCommand.Parameters.AddWithValue("@RateCategory", obj._CateId);
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

    public int Insertagentmarketmapper(BALLinks obj)
    {
        try
        {
            SqlConnection cn = new SqlConnection(strCon);
            SqlDataAdapter da = new SqlDataAdapter();
            da.InsertCommand = new SqlCommand("[dbo].[sp_Links]", cn);
            da.InsertCommand.Parameters.AddWithValue("@Action", obj._Action);
            da.InsertCommand.Parameters.AddWithValue("@AgentID", obj._Agent);
            da.InsertCommand.Parameters.AddWithValue("@marketid", obj._MarketId);
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

    public int RemoveAgentMarketMapper(BALLinks obj)
    {
        try
        {
            SqlConnection cn = new SqlConnection(strCon);
            SqlDataAdapter da = new SqlDataAdapter();
            da.DeleteCommand = new SqlCommand("[dbo].[sp_Links]", cn);
            da.DeleteCommand.Parameters.AddWithValue("@Action", obj._Action);
            da.DeleteCommand.Parameters.AddWithValue("@AgentID", obj._Agent);
            da.DeleteCommand.Parameters.AddWithValue("@marketid", obj._MarketId);
            da.DeleteCommand.CommandType = CommandType.StoredProcedure;
            cn.Open();
            int Status = da.DeleteCommand.ExecuteNonQuery();
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

    public DataTable BindControlsAgentmarket(BALLinks obj)
    {
        try
        {
            SqlConnection cn = new SqlConnection(strCon);

            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = new SqlCommand("[dbo].[sp_Links]", cn);
            da.SelectCommand.Parameters.Clear();
            da.SelectCommand.Parameters.AddWithValue("@Action", obj._Action);
            da.SelectCommand.Parameters.AddWithValue("@marketid", obj._MarketId);
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






    public int RemoveMappedRelation(BALLinks obj)
    {
        try
        {
            SqlConnection cn = new SqlConnection(strCon);
            SqlDataAdapter da = new SqlDataAdapter();
            da.DeleteCommand = new SqlCommand("[dbo].[sp_Links]", cn);
            da.DeleteCommand.Parameters.AddWithValue("@Action", obj._Action);
            da.DeleteCommand.Parameters.AddWithValue("@marketId", obj._MarketId);
            da.DeleteCommand.Parameters.AddWithValue("@RateCategory", obj._CateId);
            da.DeleteCommand.CommandType = CommandType.StoredProcedure;
            cn.Open();
            int Status = da.DeleteCommand.ExecuteNonQuery();
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
    public int RemoveMappedRelationAgent(BALLinks obj)
    {
        try
        {
            SqlConnection cn = new SqlConnection(strCon);
            SqlDataAdapter da = new SqlDataAdapter();
            da.DeleteCommand = new SqlCommand("[dbo].[sp_Links]", cn);
            da.DeleteCommand.Parameters.AddWithValue("@Action", obj._Action);
            da.DeleteCommand.Parameters.AddWithValue("@AgentID", obj._Agent);
            da.DeleteCommand.Parameters.AddWithValue("@RateCategory", obj._CateId);
            da.DeleteCommand.CommandType = CommandType.StoredProcedure;
            cn.Open();
            int Status = da.DeleteCommand.ExecuteNonQuery();
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