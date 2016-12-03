using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
public class DALOpenDates
{
    string strCon = string.Empty;

    public DALOpenDates()
    {
        strCon = ConfigurationManager.ConnectionStrings["ReservationConnectionString"].ConnectionString;

    }
    #region Get Data

    public DataTable BindControls(BALOpenDates obj)
    {
        try
        {
            SqlConnection cn = new SqlConnection(strCon);
            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = new SqlCommand("[dbo].[sp_OpenDates]", cn);
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

    public DataTable GetRiverLocation(BALOpenDates obj)
    {
        try
        {
            SqlConnection cn = new SqlConnection(strCon);
            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = new SqlCommand("[dbo].[sp_OpenDates]", cn);
            da.SelectCommand.Parameters.Clear();
            da.SelectCommand.Parameters.AddWithValue("@Action", obj._Action);
            da.SelectCommand.Parameters.AddWithValue("@CountryId", obj._CountryId);
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
    public DataTable GetNoOfNights(BALOpenDates obj)
    {
        try
        {
            SqlConnection cn = new SqlConnection(strCon);
            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = new SqlCommand("[dbo].[sp_OpenDates]", cn);
            da.SelectCommand.Parameters.Clear();
            da.SelectCommand.Parameters.AddWithValue("@Action", obj._Action);
            da.SelectCommand.Parameters.AddWithValue("@packageId", obj._PackageId);
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
    public int AddNewOpenDate(BALOpenDates obj)
    {
        try
        {
            SqlConnection cn = new SqlConnection(strCon);
            SqlDataAdapter da = new SqlDataAdapter();
            da.InsertCommand = new SqlCommand("[dbo].[sp_OpenDates]", cn);
            da.InsertCommand.Parameters.AddWithValue("@Action", obj._Action);
            da.InsertCommand.Parameters.AddWithValue("@CountryId", obj._CountryId);
            da.InsertCommand.Parameters.AddWithValue("@AccomID", obj._AccomId);
            da.InsertCommand.Parameters.AddWithValue("@RiverId", obj._RiverId);
            da.InsertCommand.Parameters.AddWithValue("@packageId", obj._PackageId);
            da.InsertCommand.Parameters.AddWithValue("@CheckinDate", obj._checkInDate);
            da.InsertCommand.Parameters.AddWithValue("@checkOutDate", obj._checkOutDate);
            da.InsertCommand.Parameters.AddWithValue("@Status", obj.Status);
            da.InsertCommand.CommandType = CommandType.StoredProcedure;
            cn.Open();
            int InsertStatus = da.InsertCommand.ExecuteNonQuery();
            cn.Close();
            if (InsertStatus > 0)
                return InsertStatus;
            else
                return 0;
        }
        catch (Exception)
        {
            return 0;
        }
    }



    #endregion


    #region GetNoOfNights
    public DataTable getSeasoninfo(BALOpenDates obj)
    {
        try
        {
            SqlConnection cn = new SqlConnection(strCon);
            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = new SqlCommand("[dbo].[sp_OpenDates]", cn);
            da.SelectCommand.Parameters.Clear();
            da.SelectCommand.Parameters.AddWithValue("@Action", obj._Action);
            da.SelectCommand.Parameters.AddWithValue("@checkin", obj._checkInDate);

            da.SelectCommand.Parameters.AddWithValue("@checkout", obj._checkOutDate);
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

    public DataTable getMonthsforddl(BALOpenDates obj)
    {
        try
        {
            SqlConnection cn = new SqlConnection(strCon);
            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = new SqlCommand("[dbo].[sp_OpenDates]", cn);
            da.SelectCommand.Parameters.Clear();
            da.SelectCommand.Parameters.AddWithValue("@Action", obj._Action);
            da.SelectCommand.Parameters.AddWithValue("@countryId", obj._CountryId);

      
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