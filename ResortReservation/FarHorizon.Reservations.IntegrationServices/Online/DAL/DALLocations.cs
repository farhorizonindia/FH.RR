using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using FarHorizon.Reservations.BusinessServices.Online.BAL;

/// <summary>
/// Summary description for DALLocations
/// </summary>
namespace FarHorizon.Reservations.BusinessServices.Online.DAL
{

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
                da.InsertCommand.Parameters.AddWithValue("@CountryId", obj.countryid);
                da.InsertCommand.Parameters.AddWithValue("@description", obj.Description);
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


        public DataTable getLocationById(BALLocation obj)
        {
            try
            {
                SqlConnection cn = new SqlConnection(strCon);
                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = new SqlCommand("[dbo].[sp_cruiseLocations]", cn);
                da.SelectCommand.Parameters.AddWithValue("@action", obj.action);
                da.SelectCommand.Parameters.AddWithValue("@LocationId", obj.LocationId);

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

        public int UpdateLocation(BALLocation obj)
        {
            try
            {
                SqlConnection cn = new SqlConnection(strCon);
                SqlDataAdapter da = new SqlDataAdapter();
                da.UpdateCommand = new SqlCommand("[dbo].[sp_cruiseLocations]", cn);
                da.UpdateCommand.Parameters.AddWithValue("@action", obj.action);
                da.UpdateCommand.Parameters.AddWithValue("@LocationName", obj.LocationName);
                da.UpdateCommand.Parameters.AddWithValue("@CountryId", obj.countryid);
                da.UpdateCommand.Parameters.AddWithValue("@LocationId", obj.LocationId);
                da.UpdateCommand.Parameters.AddWithValue("@description", obj.Description);
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

        public int DeleteLocation(BALLocation obj)
        {
            try
            {
                SqlConnection cn = new SqlConnection(strCon);
                SqlDataAdapter da = new SqlDataAdapter();
                da.DeleteCommand = new SqlCommand("[dbo].[sp_cruiseLocations]", cn);
                da.DeleteCommand.Parameters.AddWithValue("@action", obj.action);

                da.DeleteCommand.Parameters.AddWithValue("@LocationId", obj.LocationId);
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





        public int InsertCountry(BALLocation obj)
        {
            try
            {
                SqlConnection cn = new SqlConnection(strCon);
                SqlDataAdapter da = new SqlDataAdapter();
                da.InsertCommand = new SqlCommand("[dbo].[sp_cruiseLocations]", cn);
                da.InsertCommand.Parameters.AddWithValue("@action", obj.action);
                da.InsertCommand.Parameters.AddWithValue("@CountryName", obj.countryname);

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



        public DataTable getallCountries(BALLocation obj)
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

        public DataTable getcountrybyId(BALLocation obj)
        {
            try
            {
                SqlConnection cn = new SqlConnection(strCon);
                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = new SqlCommand("[dbo].[sp_cruiseLocations]", cn);
                da.SelectCommand.Parameters.AddWithValue("@action", obj.action);
                da.SelectCommand.Parameters.AddWithValue("@CountryId", obj.countryid);

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

        public int UpdateCountry(BALLocation obj)
        {
            try
            {
                SqlConnection cn = new SqlConnection(strCon);
                SqlDataAdapter da = new SqlDataAdapter();
                da.UpdateCommand = new SqlCommand("[dbo].[sp_cruiseLocations]", cn);
                da.UpdateCommand.Parameters.AddWithValue("@action", obj.action);
                da.UpdateCommand.Parameters.AddWithValue("@CountryName", obj.countryname);
                da.UpdateCommand.Parameters.AddWithValue("@CountryId", obj.countryid);
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

        public int DeleteCountry(BALLocation obj)
        {
            try
            {
                SqlConnection cn = new SqlConnection(strCon);
                SqlDataAdapter da = new SqlDataAdapter();
                da.DeleteCommand = new SqlCommand("[dbo].[sp_cruiseLocations]", cn);
                da.DeleteCommand.Parameters.AddWithValue("@action", obj.action);

                da.DeleteCommand.Parameters.AddWithValue("@CountryId", obj.countryid);
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
}