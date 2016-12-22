using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

/// <summary>
/// Summary description for DALSearch
/// </summary>
public class DALSearch
{
    string strCon = string.Empty;
	public DALSearch()
	{
        strCon = ConfigurationManager.ConnectionStrings["ReservationConnectionString"].ConnectionString;
	}


    public DataTable GetSearchResultsPackages(BALSearch obj)
    {
        try
        {
            SqlConnection cn = new SqlConnection(strCon);
            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = new SqlCommand("[dbo].[sp_getsearchresults]", cn);
            da.SelectCommand.Parameters.Clear();
            da.SelectCommand.Parameters.AddWithValue("@action", obj.action);
            da.SelectCommand.Parameters.AddWithValue("@startdate", obj.StartDate);
            da.SelectCommand.Parameters.AddWithValue("@enddate", obj.EndDate);
            da.SelectCommand.Parameters.AddWithValue("@countryId", obj.CountryId);
            da.SelectCommand.Parameters.AddWithValue("@riverId", obj.RiverId);


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

    public DataTable GetResultBasedOnPackage(BALSearch obj)
    {
        try
        {
            SqlConnection cn = new SqlConnection(strCon);
            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = new SqlCommand("[dbo].[sp_getsearchresults]", cn);
            da.SelectCommand.Parameters.Clear();
            da.SelectCommand.Parameters.AddWithValue("@action", obj.action);
            da.SelectCommand.Parameters.AddWithValue("@packageId", obj.PackageId);

            da.SelectCommand.Parameters.AddWithValue("@startdate", obj.StartDate);
            da.SelectCommand.Parameters.AddWithValue("@enddate", obj.EndDate);


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


    public DataTable GetCruiseOpenDatesPackage(BALSearch obj)
    {
        try
        {
            SqlConnection cn = new SqlConnection(strCon);
            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = new SqlCommand("[dbo].[sp_getsearchresults]", cn);
            da.SelectCommand.Parameters.Clear();
            da.SelectCommand.Parameters.AddWithValue("@action", obj.action);
            da.SelectCommand.Parameters.AddWithValue("@packageId", obj.PackageId);

            da.SelectCommand.Parameters.AddWithValue("@startdate", obj.StartDate);
            da.SelectCommand.Parameters.AddWithValue("@enddate", obj.EndDate);
            da.SelectCommand.Parameters.AddWithValue("@AgentId", obj.AgentId);


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

    public DataTable getRoomrateCategoryWise(BALSearch obj)
    {
        try
        {
            SqlConnection cn = new SqlConnection(strCon);
            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = new SqlCommand("[dbo].[sp_getsearchresults]", cn);
            da.SelectCommand.Parameters.Clear();
            da.SelectCommand.Parameters.AddWithValue("@action", obj.action);
            da.SelectCommand.Parameters.AddWithValue("@packageId", obj.PackageId);


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


    public DataTable getPackageDescription(BALSearch obj)
    {
        try
        {
            SqlConnection cn = new SqlConnection(strCon);
            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = new SqlCommand("[dbo].[sp_getsearchresults]", cn);
            da.SelectCommand.Parameters.Clear();
            da.SelectCommand.Parameters.AddWithValue("@action", obj.action);
            da.SelectCommand.Parameters.AddWithValue("@packageId", obj.PackageId);


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