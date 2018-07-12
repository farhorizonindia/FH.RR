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
        public DataTable getbyfilter1(BALSearch obj)
        {
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = new SqlConnection(strCon);
                cmd.CommandText = "sp_getsearchresults";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandTimeout = 0;
                cmd.Parameters.AddWithValue("@action", "getbyfilter");
                cmd.Parameters.AddWithValue("@startdate", obj.StartDate);
                cmd.Parameters.AddWithValue("@enddate", obj.EndDate);
                cmd.Parameters.AddWithValue("@packageId", obj.PackageId);

                DataTable dt = new DataTable();
                if (cmd.Connection.State == ConnectionState.Open)
                {
                    cmd.Connection.Close();
                }
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                if (cmd.Connection.State == ConnectionState.Open)
                {
                    cmd.Connection.Close();
                }
                da.Fill(dt);

                return dt;
            }
            catch (SqlException ex)
            {
                return null;
            }

        }
        public DataTable getroomsdetails(int bookingid)
        {
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = new SqlConnection(strCon);
                cmd.CommandText = "sp_CruiseBooking";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandTimeout = 0;
                cmd.Parameters.AddWithValue("@action", "getroomsdetails");
                cmd.Parameters.AddWithValue("@bookingId", bookingid);
               

                DataTable dt = new DataTable();
                if (cmd.Connection.State == ConnectionState.Open)
                {
                    cmd.Connection.Close();
                }
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                if (cmd.Connection.State == ConnectionState.Open)
                {
                    cmd.Connection.Close();
                }
                da.Fill(dt);

                return dt;
            }
            catch (SqlException ex)
            {
                return null;
            }

        }
        //public DataTable getbyfilter(BALSearch obj)
        //{
        //    try
        //    {
        //        SqlConnection cn = new SqlConnection(strCon);
        //        SqlDataAdapter da = new SqlDataAdapter();
        //        da.SelectCommand = new SqlCommand("[dbo].[sp_getsearchresults]", cn);
        //        da.SelectCommand.Parameters.Clear();
        //        da.SelectCommand.Parameters.AddWithValue("@action", obj.action);
        //        da.SelectCommand.Parameters.AddWithValue("@startdate", obj.StartDate);
        //        da.SelectCommand.Parameters.AddWithValue("@enddate", obj.EndDate);
        //        da.SelectCommand.Parameters.AddWithValue("@packageId", obj.PackageId);



        //        da.SelectCommand.CommandType = CommandType.StoredProcedure;
        //        cn.Open();
        //        da.SelectCommand.ExecuteReader();
        //        DataTable dtReturnData = new DataTable();
        //        cn.Close();
        //        da.Fill(dtReturnData);
        //        if (dtReturnData != null)
        //            return dtReturnData;
        //        else
        //            return null;
        //    }
        //    catch (Exception)
        //    {
        //        return null;
        //    }
        //}

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
        public DataTable fetchall()
        {
            try
            {
                SqlConnection cn = new SqlConnection(strCon);
                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = new SqlCommand("[dbo].[sp_getsearchresults]", cn);
                da.SelectCommand.Parameters.Clear();
                da.SelectCommand.Parameters.AddWithValue("@action", "fetchall");
                da.SelectCommand.Parameters.AddWithValue("@packageId", "");

                da.SelectCommand.Parameters.AddWithValue("@startdate", DateTime.Now);
                da.SelectCommand.Parameters.AddWithValue("@enddate", DateTime.Now);


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




        public DataTable fetchallPackageHome()
        {
            try
            {
                SqlConnection cn = new SqlConnection(strCon);
                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = new SqlCommand("[dbo].[sp_getsearchresults]", cn);
                da.SelectCommand.Parameters.Clear();
                da.SelectCommand.Parameters.AddWithValue("@action", "fetchallpackageHome");
                da.SelectCommand.Parameters.AddWithValue("@packageId", "");

                da.SelectCommand.Parameters.AddWithValue("@startdate", DateTime.Now);
                da.SelectCommand.Parameters.AddWithValue("@enddate", DateTime.Now);


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


        public DataTable fetchFilterPackagewise(BALBooking obj)
        {
            try
            {
                SqlConnection cn = new SqlConnection(strCon);
                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = new SqlCommand("[dbo].[sp_getsearchresults]", cn);
                da.SelectCommand.Parameters.Clear();
                da.SelectCommand.Parameters.AddWithValue("@action", "fetchfilterpackagewise");
                da.SelectCommand.Parameters.AddWithValue("@packageId", obj.PackageId);

                da.SelectCommand.Parameters.AddWithValue("@startdate", DateTime.Now);
                da.SelectCommand.Parameters.AddWithValue("@enddate", DateTime.Now);


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

        public DataTable getavaialablemonthHome(DateTime sdate, DateTime edate)
        {
            try
            {
                SqlConnection cn = new SqlConnection(strCon);
                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = new SqlCommand("[dbo].[sp_getsearchresults]", cn);
                da.SelectCommand.Parameters.Clear();
                da.SelectCommand.Parameters.AddWithValue("@action", "getavaialablemonthhome");
                da.SelectCommand.Parameters.AddWithValue("@startdate", sdate);
                da.SelectCommand.Parameters.AddWithValue("@enddate", edate);


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
        public DataTable getavaialablemonth()
        {
            try
            {
                SqlConnection cn = new SqlConnection(strCon);
                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = new SqlCommand("[dbo].[sp_getsearchresults]", cn);
                da.SelectCommand.Parameters.Clear();
                da.SelectCommand.Parameters.AddWithValue("@action", "getavaialablemonth");
          


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


        public DataTable getavaialableYear()
        {
            try
            {
                SqlConnection cn = new SqlConnection(strCon);
                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = new SqlCommand("[dbo].[sp_getsearchresults]", cn);
                da.SelectCommand.Parameters.Clear();
                da.SelectCommand.Parameters.AddWithValue("@action", "getyear");




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


        public DataTable getavaialableMonthbyyearPackage(string package,DateTime sdate,DateTime edate)
        {
            try
            {
                SqlConnection cn = new SqlConnection(strCon);
                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = new SqlCommand("[dbo].[sp_getsearchresults]", cn);
                da.SelectCommand.Parameters.Clear();
                da.SelectCommand.Parameters.AddWithValue("@action", "getmonthbyyearandpackage");
                da.SelectCommand.Parameters.AddWithValue("@packageId", package);
                da.SelectCommand.Parameters.AddWithValue("@startdate", sdate);
                da.SelectCommand.Parameters.AddWithValue("@enddate", edate);

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
            catch (Exception ex)
            {
                return null;
            }
        }


        public DataTable fetchdiscountAll()
        {
            try
            {
                SqlConnection cn = new SqlConnection(strCon);
                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = new SqlCommand("[dbo].[sp_Discountmaster]", cn);
                da.SelectCommand.Parameters.Clear();
                da.SelectCommand.Parameters.AddWithValue("@action", "fetchdiscountall");
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

        public DataTable fetchdiscount(string packageid, DateTime bordingdate, DateTime debordingdate, decimal price)
        {
            try
            {
                SqlConnection cn = new SqlConnection(strCon);
                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = new SqlCommand("[dbo].[sp_Discountmaster]", cn);
                da.SelectCommand.Parameters.Clear();
                da.SelectCommand.Parameters.AddWithValue("@action", "fetchdiscount");
                da.SelectCommand.Parameters.AddWithValue("@Packageid", packageid);

                da.SelectCommand.Parameters.AddWithValue("@Boardingdate", bordingdate);
                da.SelectCommand.Parameters.AddWithValue("@Deboardingdate", debordingdate);
                da.SelectCommand.Parameters.AddWithValue("@price", price);





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
        public DataTable getbydate(DateTime bordingdate)
        {
            try
            {
                SqlConnection cn = new SqlConnection(strCon);
                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = new SqlCommand("[dbo].[sp_Discountmaster]", cn);
                da.SelectCommand.Parameters.Clear();
                da.SelectCommand.Parameters.AddWithValue("@action", "getbydate");
               

                da.SelectCommand.Parameters.AddWithValue("@Boardingdate", bordingdate);
              





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
        public DataTable fetchbydiscountid(int id)
        {
            try
            {
                SqlConnection cn = new SqlConnection(strCon);
                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = new SqlCommand("[dbo].[sp_Discountmaster]", cn);
                da.SelectCommand.Parameters.Clear();
                da.SelectCommand.Parameters.AddWithValue("@action", "getbyid");
                da.SelectCommand.Parameters.AddWithValue("@Id", id);
              







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
            catch (Exception ex)
            {
                return null;
            }
        }

        public DataTable GetCruiseOpenDatesPackageSearch(BALSearch obj)
        {
            try
            {
                SqlConnection cn = new SqlConnection(strCon);
                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = new SqlCommand("[dbo].[sp_getsearchresults]", cn);
                da.SelectCommand.Parameters.Clear();
                da.SelectCommand.Parameters.AddWithValue("@action", "GetOpenDatesCruiseSearch");
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
            catch (Exception ex)
            {
                return null;
            }
        }


        public DataTable GetCruiseallOpenDatesPackage(BALSearch obj)
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
            catch (Exception ex)
            {
                return null;
            }
        }
        public DataTable PackagewiseavailbilityReport(BALSearch obj)
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
                da.SelectCommand.Parameters.AddWithValue("@PackageType", obj.PackageType);
                da.SelectCommand.Parameters.AddWithValue("@Openclose", obj.Openclose);


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
            catch (Exception ex)
            {
                return null;
            }
        }
        public DataTable webserviceformahabaaahu(BALSearch obj)
        {
            try
            {
                SqlConnection cn = new SqlConnection(strCon);
                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = new SqlCommand("[dbo].[webserviceformahabaaahu]", cn);
                da.SelectCommand.Parameters.Clear();



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
}