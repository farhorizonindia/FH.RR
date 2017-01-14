using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Configuration;
using System.Data.SqlClient;
using FarHorizon.Reservations.BusinessServices.Online.BAL;

namespace FarHorizon.Reservations.BusinessServices.Online.DAL
{

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


        public int UpdateMarket(BALmarket obj)
        {
            try
            {

                SqlConnection cn = new SqlConnection(strCon);
                SqlDataAdapter da = new SqlDataAdapter();
                da.UpdateCommand = new SqlCommand("[dbo].[sp_market]", cn);
                da.UpdateCommand.Parameters.AddWithValue("@Action", obj._Action);
                da.UpdateCommand.Parameters.AddWithValue("@MarketCode", obj._marketId);
                da.UpdateCommand.Parameters.AddWithValue("@MarketName", obj._marketName);
                da.UpdateCommand.Parameters.AddWithValue("@Region", obj._region);
                da.UpdateCommand.Parameters.AddWithValue("@specification", obj._specification);
                da.UpdateCommand.CommandType = CommandType.StoredProcedure;
                cn.Open();
                int Status = da.UpdateCommand.ExecuteNonQuery();
                cn.Close();
                if (Status > 0)
                    return Status;
                else
                    return 0;
            }


            catch
            {
                return 0;
            }
        }

        public DataTable getmarketbyid(BALmarket obj)
        {
            try
            {
                SqlConnection cn = new SqlConnection(strCon);

                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = new SqlCommand("[dbo].[sp_market]", cn);
                da.SelectCommand.Parameters.Clear();
                da.SelectCommand.Parameters.AddWithValue("@action", obj._Action);
                da.SelectCommand.Parameters.AddWithValue("@MarketCode", obj._marketId);
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





        public int DeleteMarket(BALmarket obj)
        {
            try
            {
                SqlConnection cn = new SqlConnection(strCon);
                SqlDataAdapter da = new SqlDataAdapter();
                da.DeleteCommand = new SqlCommand("[dbo].[sp_market]", cn);
                da.DeleteCommand.Parameters.AddWithValue("@Action", obj._Action);
                da.DeleteCommand.Parameters.AddWithValue("@MarketCode", obj._marketId);

                da.DeleteCommand.CommandType = CommandType.StoredProcedure;
                cn.Open();
                int response = da.UpdateCommand.ExecuteNonQuery();
                cn.Close();
                if (response > 0)
                {
                    return response;
                }
                else
                {
                    return 0;
                }
            }
            catch (Exception)
            {
                return 0;
            }
        }


        #endregion update data

    }
}