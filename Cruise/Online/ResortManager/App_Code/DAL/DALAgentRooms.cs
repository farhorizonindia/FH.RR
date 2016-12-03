using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Globalization;


public class DALAgentRooms
{
    string strCon = string.Empty;
	public DALAgentRooms()
    {
        strCon = ConfigurationManager.ConnectionStrings["ReservationConnectionString"].ConnectionString;

	} 
    #region Get Data

    public DataTable BindControls(BALAgentRooms obj)
    {
        try
        {
            SqlConnection cn = new SqlConnection(strCon);
            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = new SqlCommand("[dbo].[sp_agentMaxRooms]", cn);
            da.SelectCommand.Parameters.Clear();
            da.SelectCommand.Parameters.AddWithValue("@Action", obj._Action);
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
    public DataTable GetConfirmation(BALAgentRooms obj)
    {
        try
        {
            SqlConnection cn = new SqlConnection(strCon);
            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = new SqlCommand("[dbo].[sp_agentMaxRooms]", cn);
            da.SelectCommand.Parameters.Clear();
            da.SelectCommand.Parameters.AddWithValue("@Action", obj._Action);
            da.SelectCommand.Parameters.AddWithValue("@AccomId", obj._AccomId);
            da.SelectCommand.Parameters.AddWithValue("@maxRooms", obj._maxRooms);
            string dateTime = DateTime.Now.ToString();
            string createddate = Convert.ToDateTime(dateTime).ToString("yyyy-MM-dd");
            DateTime dt = DateTime.ParseExact(createddate, "yyyy-MM-dd", CultureInfo.InvariantCulture);
            da.SelectCommand.Parameters.AddWithValue("@date", dt);
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
    public DataTable GetExists(BALAgentRooms obj)
    {
        try
        {
            SqlConnection cn = new SqlConnection(strCon);
            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = new SqlCommand("[dbo].[sp_agentMaxRooms]", cn);
            da.SelectCommand.Parameters.Clear();
            da.SelectCommand.Parameters.AddWithValue("@Action", obj._Action);
            da.SelectCommand.Parameters.AddWithValue("@AccomId", obj._AccomId);
            da.SelectCommand.Parameters.AddWithValue("@AgentId", obj._AgentId);
            string dateTime = DateTime.Now.ToString();
            string createddate = Convert.ToDateTime(dateTime).ToString("yyyy-MM-dd");
            DateTime dt = DateTime.ParseExact(createddate, "yyyy-MM-dd", CultureInfo.InvariantCulture);
            da.SelectCommand.Parameters.AddWithValue("@date", dt);
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

    #region Insert/Update Data

    public int AddAgentRoom(BALAgentRooms obj)
    {
        try
        {
            SqlConnection cn = new SqlConnection(strCon);
            SqlDataAdapter da = new SqlDataAdapter();
            da.InsertCommand = new SqlCommand("[dbo].[sp_agentMaxRooms]", cn);
            da.InsertCommand.Parameters.AddWithValue("@Action", obj._Action);
            da.InsertCommand.Parameters.AddWithValue("@AccomId", obj._AccomId);
            da.InsertCommand.Parameters.AddWithValue("@AgentId", obj._AgentId);
            da.InsertCommand.Parameters.AddWithValue("@maxRooms", obj._maxRooms);
            da.InsertCommand.Parameters.AddWithValue("@date", obj._Date);
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
    public int UpdateRooms(BALAgentRooms obj)
    {
        try
        {
            SqlConnection cn = new SqlConnection(strCon);
            SqlDataAdapter da = new SqlDataAdapter();
            da.UpdateCommand = new SqlCommand("[dbo].[sp_agentMaxRooms]", cn);
            da.UpdateCommand.Parameters.AddWithValue("@Action", obj._Action);
            da.UpdateCommand.Parameters.AddWithValue("@AccomId", obj._AccomId);
            da.UpdateCommand.Parameters.AddWithValue("@AgentId", obj._AgentId);
            da.UpdateCommand.Parameters.AddWithValue("@maxRooms", obj._maxRooms);
            da.UpdateCommand.Parameters.AddWithValue("@date", obj._Date);
            da.UpdateCommand.CommandType = CommandType.StoredProcedure;
            cn.Open();
            int Status = da.UpdateCommand.ExecuteNonQuery();
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