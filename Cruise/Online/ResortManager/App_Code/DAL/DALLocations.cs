using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;


/// <summary>
/// Summary description for DALLocations
/// </summary>
public class DALLocations
{

    string strCon = string.Empty;
	public DALLocations()
	{
        strCon = ConfigurationManager.ConnectionStrings["ReservationConnectionString"].ConnectionString;
	}

    public int InsertLocation(BALLocation obj)
    {
        try
        {
            SqlConnection cn = new SqlConnection(strCon);
            SqlDataAdapter da = new SqlDataAdapter();
            da.InsertCommand = new SqlCommand("[dbo].[sp_cruiseLocations]", cn);
            da.InsertCommand.Parameters.AddWithValue("@action", obj.action);
            da.InsertCommand.Parameters.AddWithValue("@LocationName", obj.LocationName);
      
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
    public DataTable getLocation(BALLocation obj)
    {
        try
        {
            SqlConnection cn = new SqlConnection(strCon);
            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = new SqlCommand("[dbo].[sp_cruiseLocations]", cn);
            da.SelectCommand.Parameters.AddWithValue("@action", obj.action);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            cn.Open();
            da.SelectCommand.ExecuteReader();
            cn.Close();
            DataTable dt = new DataTable();
            da.Fill(dt);
            if (dt != null)
                return dt;
            else
                return null;
        }
        catch (Exception)
        {
            return null;
        }
    }



}