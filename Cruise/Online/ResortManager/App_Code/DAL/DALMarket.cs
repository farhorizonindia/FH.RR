using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Configuration;
using System.Data.SqlClient;
public class DALMarket
{
    string strCon = string.Empty;
	public DALMarket()
    {
        strCon = ConfigurationManager.ConnectionStrings["ReservationConnectionString"].ConnectionString;

	}


    #region getdata
    public DataTable GetAllmarkets(BALmarket obj)
    {
        try
        {
            SqlConnection cn = new SqlConnection(strCon);
            SqlDataAdapter da = new SqlDataAdapter("[dbo].[sp_market] '" + obj._Action + "'", cn);
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

    #region insert update data

    public int AddNewMarket(BALmarket obj)
    {
        try
        {
            SqlConnection cn = new SqlConnection(strCon);
            SqlDataAdapter da = new SqlDataAdapter();
            da.InsertCommand = new SqlCommand("[dbo].[sp_market]", cn);
            da.InsertCommand.Parameters.AddWithValue("@Action", obj._Action);
            da.InsertCommand.Parameters.AddWithValue("@MarketCode", obj._marketId);
            da.InsertCommand.Parameters.AddWithValue("@MarketName", obj._marketName);
            da.InsertCommand.Parameters.AddWithValue("@Region", obj._region);
            da.InsertCommand.Parameters.AddWithValue("@specification", obj._specification);
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
    //public int UpdatePreviousRate(BALRateMaster obj)
    //{
    //    try
    //    {
    //        SqlConnection cn = new SqlConnection(strCon);
    //        SqlDataAdapter da = new SqlDataAdapter();
    //        da.UpdateCommand = new SqlCommand("[dbo].[sp_RateMaster]", cn);
    //        da.UpdateCommand.Parameters.AddWithValue("@Action", obj.Action);
    //        da.UpdateCommand.Parameters.AddWithValue("@AccomId", obj.AccomId);
    //        da.UpdateCommand.Parameters.AddWithValue("@RoomCatId", obj.RoomCategoryId);
    //        da.UpdateCommand.Parameters.AddWithValue("@RoomTypeId", obj.RommTypeId);
    //        da.UpdateCommand.Parameters.AddWithValue("@RateCategoryId", obj.RateCategoryID);
    //        da.UpdateCommand.Parameters.AddWithValue("@UpstreamPrice", obj.UpstreamPrice);
    //        da.UpdateCommand.Parameters.AddWithValue("@DownStreamPrice", obj.DownStreamPrice);
    //        da.UpdateCommand.Parameters.AddWithValue("@PriceApartCruise", obj.PriceApartCruise);
    //        da.UpdateCommand.CommandType = CommandType.StoredProcedure;
    //        cn.Open();
    //        int response = da.UpdateCommand.ExecuteNonQuery();
    //        cn.Close();
    //        if (response > 0)
    //        {
    //            return response;
    //        }
    //        else
    //        {
    //            return 0;
    //        }
    //    }
    //    catch (Exception)
    //    {
    //        return 0;
    //    }
    //}
    #endregion insert update data

}