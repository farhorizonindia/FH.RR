using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using FarHorizon.Reservations.BusinessServices.Online.BAL;

namespace FarHorizon.Reservations.BusinessServices.Online.DAL
{

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
        public DataTable GetNightsDetail(BALPackageMaster obj)
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
                da.InsertCommand.Parameters.AddWithValue("@ItineraryLink", obj.ItineraryLink);


                da.InsertCommand.Parameters.AddWithValue("@PackageDesc", obj.PackageDescription);
                da.InsertCommand.Parameters.AddWithValue("@PackageImage", obj.ImagePath);
                da.InsertCommand.Parameters.AddWithValue("@IsActive", obj.IsActive);

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
        #endregion



        public DataTable getPackagebyid(BALPackageMaster obj)
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

        public DataTable getPackagebyBynights(BALPackageMaster obj)
        {
            try
            {
                SqlConnection cn = new SqlConnection(strCon);
                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = new SqlCommand("[dbo].[sp_Package]", cn);
                da.SelectCommand.Parameters.Clear();
                da.SelectCommand.Parameters.AddWithValue("@Action", obj._Action);
                da.SelectCommand.Parameters.AddWithValue("@NoOfNights", obj._NoOfNights);
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
        public DataTable Getpackagebydates(BALPackageMaster obj)
        {
            try
            {
                SqlConnection cn = new SqlConnection(strCon);
                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = new SqlCommand("[dbo].[sp_Package]", cn);
                da.SelectCommand.Parameters.Clear();
                da.SelectCommand.Parameters.AddWithValue("@Action", obj._Action);
                da.SelectCommand.Parameters.AddWithValue("@CheckInDate", obj.Checkin);
                da.SelectCommand.Parameters.AddWithValue("@CheckOutDate", obj.checkout);
                //da.SelectCommand.Parameters.AddWithValue("@NoOfNights", obj._NoOfNights);
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
        public int DeletePackage(BALPackageMaster obj)
        {
            try
            {
                SqlConnection cn = new SqlConnection(strCon);
                SqlDataAdapter da = new SqlDataAdapter();
                da.DeleteCommand = new SqlCommand("[dbo].[sp_Package]", cn);
                da.DeleteCommand.Parameters.AddWithValue("@Action", obj._Action);
                da.DeleteCommand.Parameters.AddWithValue("@packageId", obj._packageId);

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


        public DataTable checkChild(BALPackageMaster obj)
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

        public int UpdatePackage(BALPackageMaster obj)
        {
            try
            {
                SqlConnection cn = new SqlConnection(strCon);
                SqlDataAdapter da = new SqlDataAdapter();
                da.UpdateCommand = new SqlCommand("[dbo].[sp_Package]", cn);
                da.UpdateCommand.Parameters.AddWithValue("@Action", obj._Action);
                da.UpdateCommand.Parameters.AddWithValue("@packageId", obj._packageId);
                da.UpdateCommand.Parameters.AddWithValue("@packageName", obj._packageName);
                da.UpdateCommand.Parameters.AddWithValue("@PackageDesc", obj.PackageDescription);
                da.UpdateCommand.Parameters.AddWithValue("@PackageImage", obj.ImagePath);
                da.UpdateCommand.Parameters.AddWithValue("@ItineraryLink", obj.ItineraryLink);
                da.UpdateCommand.Parameters.AddWithValue("@IsActive", obj.IsActive);
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


    }
}