using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
public class DALPackageMaster
{
    string strCon = string.Empty;
	public DALPackageMaster()
    {
        strCon = ConfigurationManager.ConnectionStrings["ReservationConnectionString"].ConnectionString;

	}

    #region Get Data
    public DataTable BindControls(BALPackageMaster obj)
    {
        try
        {
            SqlConnection cn = new SqlConnection(strCon);
            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = new SqlCommand("[dbo].[sp_Package]", cn);
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
    #endregion


    #region Insert / Update Data
    public int AddNewPackage(BALPackageMaster obj)
    {
        try
        {
            SqlConnection cn = new SqlConnection(strCon);
            SqlDataAdapter da = new SqlDataAdapter();
            da.InsertCommand = new SqlCommand("[dbo].[sp_Package]", cn);
            da.InsertCommand.Parameters.AddWithValue("@Action", obj._Action);
            da.InsertCommand.Parameters.AddWithValue("@packageId", obj._packageId);
            da.InsertCommand.Parameters.AddWithValue("@packageName", obj._packageName);
            da.InsertCommand.Parameters.AddWithValue("@NoOfNights", obj._NoOfNights);
            da.InsertCommand.Parameters.AddWithValue("@pakageType", obj._pakageType);
            da.InsertCommand.Parameters.AddWithValue("@MasterpackageId", obj._MasterPackageId);
            da.InsertCommand.Parameters.AddWithValue("@boardingFrom", obj._BoardingFrom);
            da.InsertCommand.Parameters.AddWithValue("@BoardingTo", obj._BoardingTo);
            da.InsertCommand.Parameters.AddWithValue("@HotelId", obj._HotelId);
            da.InsertCommand.Parameters.AddWithValue("@creationDate", obj._creationDate);
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


    public int AddPackageNights(BALPackageMaster obj)
    {
        try
        {
            SqlConnection cn = new SqlConnection(strCon);
            SqlDataAdapter da = new SqlDataAdapter();
            da.InsertCommand = new SqlCommand("[dbo].[sp_Package]", cn);
            da.InsertCommand.Parameters.AddWithValue("@Action", obj._Action);
            da.InsertCommand.Parameters.AddWithValue("@packageId", obj._packageId);
            da.InsertCommand.Parameters.AddWithValue("@night", obj._night);
            da.InsertCommand.Parameters.AddWithValue("@cityId", obj._cityId);
            da.InsertCommand.Parameters.AddWithValue("@checkIn", obj._AllowCheckIn);
            da.InsertCommand.Parameters.AddWithValue("@checkOut", obj._AllowCheckOut);
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

    public DataTable getPackageItinerary(BALPackageMaster obj)
    {
        try
        {
            SqlConnection cn = new SqlConnection(strCon);
            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = new SqlCommand("[dbo].[sp_Package]", cn);
            da.SelectCommand.Parameters.Clear();
            da.SelectCommand.Parameters.AddWithValue("@Action", obj._Action);
            da.SelectCommand.Parameters.AddWithValue("@packageId", obj._packageId);
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
}