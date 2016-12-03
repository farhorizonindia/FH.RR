using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

public class DALRateCategory
{
    string strCon = string.Empty;
	public DALRateCategory()
    {
        strCon = ConfigurationManager.ConnectionStrings["ReservationConnectionString"].ConnectionString;

	}

    #region insertdata
    public int AddNewMarket(BALRateCategory obj)
    {
        try
        {
            SqlConnection cn = new SqlConnection(strCon);
            SqlDataAdapter da = new SqlDataAdapter();
            da.InsertCommand = new SqlCommand("[dbo].[sp_RateCategory]", cn);
            da.InsertCommand.Parameters.AddWithValue("@Action", obj._Action);
            da.InsertCommand.Parameters.AddWithValue("@RateCateId", obj._categoryId);
            da.InsertCommand.Parameters.AddWithValue("@RateCateName", obj._CategoryName);
            da.InsertCommand.Parameters.AddWithValue("@AltName", obj._AltName);
            da.InsertCommand.Parameters.AddWithValue("@Remark", obj._Remark);
            da.InsertCommand.Parameters.AddWithValue("@status", obj._Status);
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


    #region GetData
    #region getdata
    public DataTable GetAllCategories(BALRateCategory obj)
    {
        try
        {
            SqlConnection cn = new SqlConnection(strCon);
            SqlDataAdapter da = new SqlDataAdapter("[dbo].[sp_RateCategory] '" + obj._Action + "'", cn);
            DataTable dtReturnData = new DataTable();
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







    #endregion


    public DataTable getCategoriesbyId(BALRateCategory obj)
    {
        try
        {
            SqlConnection cn = new SqlConnection(strCon);
            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = new SqlCommand("[dbo].[sp_RateCategory]", cn);
            da.SelectCommand.Parameters.Clear();
            da.SelectCommand.Parameters.AddWithValue("@Action", obj._Action);

            da.SelectCommand.Parameters.AddWithValue("@RateCateId", obj._categoryId);
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


    public int updateRateCategory(BALRateCategory obj)
    {
        try
        {
            SqlConnection cn = new SqlConnection(strCon);
            SqlDataAdapter da = new SqlDataAdapter();
            da.UpdateCommand = new SqlCommand("[dbo].[sp_RateCategory]", cn);
            da.UpdateCommand.Parameters.AddWithValue("@Action", obj._Action);
            da.UpdateCommand.Parameters.AddWithValue("@RateCateId", obj._categoryId);
            da.UpdateCommand.Parameters.AddWithValue("@RateCateName", obj._CategoryName);
            da.UpdateCommand.Parameters.AddWithValue("@AltName", obj._AltName);
            da.UpdateCommand.Parameters.AddWithValue("@Remark", obj._Remark);
            da.UpdateCommand.Parameters.AddWithValue("@status", obj._Status);
       
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


    public int DeleteRateCategory(BALRateCategory obj)
    {
        try
        {
            SqlConnection cn = new SqlConnection(strCon);
            SqlDataAdapter da = new SqlDataAdapter();
            da.DeleteCommand = new SqlCommand("[dbo].[sp_RateCategory]", cn);
            da.DeleteCommand.Parameters.AddWithValue("@Action", obj._Action);
            da.DeleteCommand.Parameters.AddWithValue("@RateCateId", obj._categoryId);
     
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





}