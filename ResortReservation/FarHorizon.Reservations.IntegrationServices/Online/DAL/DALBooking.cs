using FarHorizon.Reservations.BusinessServices.Online.BAL;
using FarHorizon.Reservations.Common;
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace FarHorizon.Reservations.BusinessServices.Online.DAL
{

    public class DALBooking
    {

        string strCon = string.Empty;
        public DALBooking()
        {
            strCon = ConfigurationManager.ConnectionStrings["ReservationConnectionString"].ConnectionString;
        }

        #region  Get Data

        public int GetBookingReferenceCount(BALBooking obj)
        {
            int iBookingReferenceCount;

            try
            {
                SqlConnection cn = new SqlConnection(strCon);

                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = new SqlCommand("[dbo].[up_GetBookingReferenceCount]", cn);
                da.SelectCommand.Parameters.Clear();


                da.SelectCommand.Parameters.AddWithValue("@sBookingRef", obj._sBookingRef);
                da.SelectCommand.Parameters.AddWithValue("@dtStartDate", obj._dtStartDate);
                da.SelectCommand.Parameters.AddWithValue("@dtEndDate", obj._dtEndDate);
                da.SelectCommand.Parameters.AddWithValue("@iAccomTypeId", obj._iAccomTypeId);
                da.SelectCommand.Parameters.AddWithValue("@iAccomId", obj._iAccomId);
                da.SelectCommand.Parameters.AddWithValue("@iBookingId", obj._iBookingId);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                cn.Open();
                da.SelectCommand.ExecuteReader();
                cn.Close();
                DataTable dt = new DataTable();
                da.Fill(dt);
                iBookingReferenceCount = Convert.ToInt32(da.SelectCommand.ExecuteScalar());




            }
            catch (Exception exp)
            {
                obj = null;
                iBookingReferenceCount = 0;
                return 0;
            }
            finally
            {

            }
            return iBookingReferenceCount;
        }

        public DataTable GetRoomCategoryWiseRates(BALBooking obj)
        {
            try
            {
                SqlConnection cn = new SqlConnection(strCon);

                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = new SqlCommand("[dbo].[sp_CruiseBooking]", cn);
                da.SelectCommand.Parameters.Clear();
                da.SelectCommand.Parameters.AddWithValue("@action", obj.action);
                da.SelectCommand.Parameters.AddWithValue("@PackageId", obj.PackageId);
                da.SelectCommand.Parameters.AddWithValue("@AgentId", obj.AgentId);
                da.SelectCommand.Parameters.AddWithValue("@AgentIdRef", obj.AgentIdRef);
                da.SelectCommand.Parameters.AddWithValue("@totpax", obj.totpax);
                da.SelectCommand.Parameters.AddWithValue("@startdate", GF.HandleMaxMinDates(obj._dtStartDate, false));

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
            catch (Exception exp)
            {
                Console.Write(exp.Message);
                return null;
            }
        }

        public double TotalAmount(BALBooking obj)
        {
            int iBookingReferenceCount;

            try
            {
                SqlConnection cn = new SqlConnection(strCon);

                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = new SqlCommand("[dbo].[sp_CruiseBooking]", cn);
                da.SelectCommand.Parameters.Clear();


                da.SelectCommand.Parameters.AddWithValue("@sBookingRef", obj._sBookingRef);
                da.SelectCommand.Parameters.AddWithValue("@dtStartDate", obj._dtStartDate);
                da.SelectCommand.Parameters.AddWithValue("@dtEndDate", obj._dtEndDate);
                da.SelectCommand.Parameters.AddWithValue("@iAccomTypeId", obj._iAccomTypeId);
                da.SelectCommand.Parameters.AddWithValue("@iAccomId", obj._iAccomId);
                da.SelectCommand.Parameters.AddWithValue("@iBookingId", obj._iBookingId);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                cn.Open();
                da.SelectCommand.ExecuteReader();
                cn.Close();
                DataTable dt = new DataTable();
                da.Fill(dt);
                iBookingReferenceCount = Convert.ToInt32(da.SelectCommand.ExecuteScalar());




            }
            catch (Exception exp)
            {
                obj = null;
                iBookingReferenceCount = 0;
                return 0;
            }
            finally
            {

            }
            return iBookingReferenceCount;
        }

        public DataTable RevenueReport(BALBooking obj)
        {
            try
            {
                SqlConnection cn = new SqlConnection(strCon);

                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = new SqlCommand("[dbo].[RevenueReport]", cn);
                da.SelectCommand.Parameters.AddWithValue("@fromdate", obj._dtStartDate);
                da.SelectCommand.Parameters.AddWithValue("@todate", obj._dtEndDate);
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
            catch (Exception exp)
            {
                Console.Write(exp.Message);
                return null;
            }
        }
        public DataTable bookingSummery(BALBooking obj)
        {
            try
            {
                SqlConnection cn = new SqlConnection(strCon);

                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = new SqlCommand("[dbo].[bookingSummery]", cn);

                da.SelectCommand.Parameters.Clear();

                da.SelectCommand.Parameters.AddWithValue("@FromDate", obj._dtStartDate);
                da.SelectCommand.Parameters.AddWithValue("@ToDate", obj._dtEndDate);
                da.SelectCommand.Parameters.AddWithValue("@AccomId", obj.accomId);
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
            catch (Exception exp)
            {
                Console.Write(exp.Message);
                return null;
            }
        }
        public DataTable getbookinposition(BALBooking obj)
        {
            try
            {
                SqlConnection cn = new SqlConnection(strCon);

                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = new SqlCommand("[dbo].[bookingSummery]", cn);

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
            catch (Exception exp)
            {
                Console.Write(exp.Message);
                return null;
            }
        }
        public DataTable bookingDetailsforCruise(BALBooking obj)
        {
            try
            {
                SqlConnection cn = new SqlConnection(strCon);

                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = new SqlCommand("[dbo].[bookingDetailsforCruise]", cn);

                da.SelectCommand.Parameters.Clear();

                da.SelectCommand.Parameters.AddWithValue("@FromDate", obj._dtStartDate);
                da.SelectCommand.Parameters.AddWithValue("@ToDate", obj._dtEndDate);
                da.SelectCommand.Parameters.AddWithValue("@AccomId", obj.accomId);
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
            catch (Exception exp)
            {
                Console.Write(exp.Message);
                return null;
            }
        }
        public DataTable bookingDetailsforhotel(BALBooking obj)
        {
            try
            {
                SqlConnection cn = new SqlConnection(strCon);

                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = new SqlCommand("[dbo].[bookingDetailsforhotel]", cn);

                da.SelectCommand.Parameters.Clear();

                da.SelectCommand.Parameters.AddWithValue("@FromDate", obj._dtStartDate);
                da.SelectCommand.Parameters.AddWithValue("@ToDate", obj._dtEndDate);
                da.SelectCommand.Parameters.AddWithValue("@AccomId", obj.accomId);
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
            catch (Exception exp)
            {
                Console.Write(exp.Message);
                return null;
            }
        }
        public DataTable getPaymentRegister(BALBooking obj)
        {
            try
            {
                SqlConnection cn = new SqlConnection(strCon);

                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = new SqlCommand("[dbo].[getPaymentRegister]", cn);
                da.SelectCommand.Parameters.Clear();

                da.SelectCommand.Parameters.AddWithValue("@FromDate", obj._dtStartDate);
                da.SelectCommand.Parameters.AddWithValue("@ToDate", obj._dtEndDate);
                da.SelectCommand.Parameters.AddWithValue("@AccomId", obj.accomId);
                da.SelectCommand.Parameters.AddWithValue("@AgentId", obj.AgentId);
                da.SelectCommand.Parameters.AddWithValue("@PaymentFromDate", obj._dtPaymentFDate);
                da.SelectCommand.Parameters.AddWithValue("@PaymentToDate", obj._dtPaymentTDate);


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
            catch (Exception exp)
            {
                Console.Write(exp.Message);
                return null;
            }
        }


        public DataTable getmonthlyrevenueAll(BALBooking obj)
        {
            try
            {
                SqlConnection cn = new SqlConnection(strCon);

                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = new SqlCommand("[dbo].[sp_getBookingPositionNew]", cn);
                da.SelectCommand.Parameters.Clear();
              //  da.SelectCommand.Parameters.AddWithValue("@action", obj.action);
                da.SelectCommand.Parameters.AddWithValue("@Boardingdate", obj._dtStartDate);
                da.SelectCommand.Parameters.AddWithValue("@Deboardingdate", obj._dtEndDate);

             //   da.SelectCommand.Parameters.AddWithValue("@Packageid", obj.PackageId);
                //da.SelectCommand.Parameters.AddWithValue("@Deboardingdate", obj._dtEndDate);

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
            catch (Exception exp)
            {
                Console.Write(exp.Message);
                return null;
            }
        }
        public DataTable getmonthlyrevenue(BALBooking obj)
        {
            try
            {
                SqlConnection cn = new SqlConnection(strCon);

                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = new SqlCommand("[dbo].[sp_getBookingPosition]", cn);
                da.SelectCommand.Parameters.Clear();
                da.SelectCommand.Parameters.AddWithValue("@action", obj.action);
                da.SelectCommand.Parameters.AddWithValue("@Boardingdate", obj._dtStartDate);
                da.SelectCommand.Parameters.AddWithValue("@Deboardingdate", obj._dtEndDate);

                da.SelectCommand.Parameters.AddWithValue("@Packageid", obj.PackageId);
                //da.SelectCommand.Parameters.AddWithValue("@Deboardingdate", obj._dtEndDate);

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
            catch (Exception exp)
            {
                Console.Write(exp.Message);
                return null;
            }
        }
        public DataTable getUpstreamCalculation(BALBooking obj)
        {
            try
            {
                SqlConnection cn = new SqlConnection(strCon);

                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = new SqlCommand("[dbo].[Up_UpstreamCalculation]", cn);
                da.SelectCommand.Parameters.Clear();
                da.SelectCommand.Parameters.AddWithValue("@action", obj.action);
                da.SelectCommand.Parameters.AddWithValue("@Boardingdate", obj._dtStartDate);
                da.SelectCommand.Parameters.AddWithValue("@Deboardingdate", obj._dtEndDate);

                da.SelectCommand.Parameters.AddWithValue("@Packageid", obj.PackageId);
                //da.SelectCommand.Parameters.AddWithValue("@Deboardingdate", obj._dtEndDate);

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
            catch (Exception exp)
            {
                Console.Write(exp.Message);
                return null;
            }
        }
        public DataTable getDownstreamCalculation(BALBooking obj)
        {
            try
            {
                SqlConnection cn = new SqlConnection(strCon);

                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = new SqlCommand("[dbo].[Up_DownstreamCalculation]", cn);
                da.SelectCommand.Parameters.Clear();
                da.SelectCommand.Parameters.AddWithValue("@action", obj.action);
                da.SelectCommand.Parameters.AddWithValue("@Boardingdate", obj._dtStartDate);
                da.SelectCommand.Parameters.AddWithValue("@Deboardingdate", obj._dtEndDate);

                da.SelectCommand.Parameters.AddWithValue("@Packageid", obj.PackageId);
                //da.SelectCommand.Parameters.AddWithValue("@Deboardingdate", obj._dtEndDate);

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
            catch (Exception exp)
            {
                Console.Write(exp.Message);
                return null;
            }
        }
        public DataTable getUpstreamPackage(BALBooking obj)
        {
            try
            {
                SqlConnection cn = new SqlConnection(strCon);

                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = new SqlCommand("[dbo].[up_UpstreamPackage]", cn);
                da.SelectCommand.Parameters.Clear();
                //da.SelectCommand.Parameters.AddWithValue("@action", obj.action);
                da.SelectCommand.Parameters.AddWithValue("@Boardingdate", obj._dtStartDate);
                da.SelectCommand.Parameters.AddWithValue("@Deboardingdate", obj._dtEndDate);

                da.SelectCommand.Parameters.AddWithValue("@Packageid", obj.PackageId);
                //da.SelectCommand.Parameters.AddWithValue("@Deboardingdate", obj._dtEndDate);

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
            catch (Exception exp)
            {
                Console.Write(exp.Message);
                return null;
            }
        }
        public DataTable getDownstreamPackage(BALBooking obj)
        {
            try
            {
                SqlConnection cn = new SqlConnection(strCon);

                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = new SqlCommand("[dbo].[up_DownstreamPackage]", cn);
                da.SelectCommand.Parameters.Clear();
                //da.SelectCommand.Parameters.AddWithValue("@action", obj.action);
                da.SelectCommand.Parameters.AddWithValue("@Boardingdate", obj._dtStartDate);
                da.SelectCommand.Parameters.AddWithValue("@Deboardingdate", obj._dtEndDate);

                da.SelectCommand.Parameters.AddWithValue("@Packageid", obj.PackageId);
                //da.SelectCommand.Parameters.AddWithValue("@Deboardingdate", obj._dtEndDate);

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
            catch (Exception exp)
            {
                Console.Write(exp.Message);
                return null;
            }
        }
        public DataTable Profitability(BALBooking obj)
        {
            try
            {
                SqlConnection cn = new SqlConnection(strCon);

                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = new SqlCommand("[dbo].[ProfitabilityReport]", cn);

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
            catch (Exception exp)
            {
                Console.Write(exp.Message);
                return null;
            }
        }
        public DataTable agentproductivity(BALBooking obj)
        {
            try
            {
                SqlConnection cn = new SqlConnection(strCon);

                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = new SqlCommand("[dbo].[agentproductivity]", cn);

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
            catch (Exception exp)
            {
                Console.Write(exp.Message);
                return null;
            }
        }
        public DataTable getdetailsbyaccomid(BALBooking obj)
        {
            try
            {
                SqlConnection cn = new SqlConnection(strCon);

                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = new SqlCommand("[up_Get_detailsbyaccomidd]", cn);
                da.SelectCommand.Parameters.Clear();

                da.SelectCommand.Parameters.AddWithValue("@iAccomId", obj.accomId);


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
            catch (Exception exp)
            {
                Console.Write(exp.Message);
                return null;
            }
        }
        public DataTable getpackaageid(string packagename, DateTime startdate)
        {
            try
            {
                SqlConnection cn = new SqlConnection(strCon);
                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = new SqlCommand("[dbo].[sp_CruiseBooking]", cn);
                da.SelectCommand.Parameters.Clear();
                da.SelectCommand.Parameters.AddWithValue("@action", "getpackaageid");
                da.SelectCommand.Parameters.AddWithValue("@startdate", startdate);
                da.SelectCommand.Parameters.AddWithValue("@packagename", packagename);

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
        public DataTable getid(string packagename)
        {
            try
            {
                SqlConnection cn = new SqlConnection(strCon);
                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = new SqlCommand("[dbo].[sp_CruiseBooking]", cn);
                da.SelectCommand.Parameters.Clear();
                da.SelectCommand.Parameters.AddWithValue("@action", "packaageid");

                da.SelectCommand.Parameters.AddWithValue("@packagename", packagename);

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
        public DataTable GetTokenDetails()
        {
            try
            {
                SqlConnection cn = new SqlConnection(strCon);

                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = new SqlCommand("up_Tokenlist", cn);

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
            catch (Exception exp)
            {
                Console.Write(exp.Message);
                return null;
            }
        }
        public DataTable GetCruiseRooms(BALBooking obj)
        {
            try
            {
                SqlConnection cn = new SqlConnection(strCon);
                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = new SqlCommand("[dbo].[sp_CruiseBooking]", cn);
                da.SelectCommand.Parameters.Clear();
                da.SelectCommand.Parameters.AddWithValue("@action", obj.action);
                da.SelectCommand.Parameters.AddWithValue("@PackageId", obj.PackageId);
                da.SelectCommand.Parameters.AddWithValue("@DepartureId", obj.DepartureId);
                da.SelectCommand.Parameters.AddWithValue("@AgentId", obj._iAgentId);
                da.SelectCommand.Parameters.AddWithValue("@AgentIdRef", obj._iAgentIdRef);
                da.SelectCommand.Parameters.AddWithValue("@roomcategoryid", obj.roomcatid);
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
        public DataTable getchilid(BALBooking obj)
        {
            try
            {
                SqlConnection cn = new SqlConnection(strCon);
                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = new SqlCommand("[dbo].[sp_CruiseBooking]", cn);
                da.SelectCommand.Parameters.Clear();
                da.SelectCommand.Parameters.AddWithValue("@action", obj.action);
                da.SelectCommand.Parameters.AddWithValue("@PackageId", obj.PackageId);
               
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
        public DataTable getdepartureid(BALBooking obj)
        {
            try
            {
                SqlConnection cn = new SqlConnection(strCon);
                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = new SqlCommand("[dbo].[sp_CruiseBooking]", cn);
                da.SelectCommand.Parameters.Clear();
                da.SelectCommand.Parameters.AddWithValue("@action", obj.action);
                da.SelectCommand.Parameters.AddWithValue("@PackageId", obj.PackageId);
                da.SelectCommand.Parameters.AddWithValue("@startdate", obj._dtStartDate);
                da.SelectCommand.Parameters.AddWithValue("@enddate", obj._dtEndDate);
              
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
        public DataTable GetcruiseRoomsbydates(BALBooking obj)
        {
            try
            {
                SqlConnection cn = new SqlConnection(strCon);
                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = new SqlCommand("[dbo].[sp_CruiseBooking]", cn);
                da.SelectCommand.Parameters.Clear();
                da.SelectCommand.Parameters.AddWithValue("@action", obj.action);
                da.SelectCommand.Parameters.AddWithValue("@PackageId", obj.PackageId);
                da.SelectCommand.Parameters.AddWithValue("@DepartureId", obj.DepartureId);
                da.SelectCommand.Parameters.AddWithValue("@AgentId", obj._iAgentId);
                da.SelectCommand.Parameters.AddWithValue("@AgentIdRef", obj._iAgentIdRef);
                da.SelectCommand.Parameters.AddWithValue("@roomcategoryid", obj.roomcatid);
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
        public DataTable GetDepartureDetails(BALBooking obj)
        {
            try
            {
                SqlConnection cn = new SqlConnection(strCon);

                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = new SqlCommand("[dbo].[sp_CruiseBooking]", cn);
                da.SelectCommand.Parameters.Clear();
                da.SelectCommand.Parameters.AddWithValue("@action", obj.action);
                da.SelectCommand.Parameters.AddWithValue("@PackageId", obj.PackageId);
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
        public DataTable GetPackageDetails(BALBooking obj)
        {
            try
            {
                SqlConnection cn = new SqlConnection(strCon);

                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = new SqlCommand("[dbo].[sp_CruiseBooking]", cn);
                da.SelectCommand.Parameters.Clear();
                da.SelectCommand.Parameters.AddWithValue("@action", obj.action);
                da.SelectCommand.Parameters.AddWithValue("@PackageId", obj.PackageId);
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

        public DataTable GetMaxBookingId(BALBooking obj)
        {
            try
            {
                SqlConnection cn = new SqlConnection(strCon);
                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = new SqlCommand("[dbo].[sp_CruiseBooking]", cn);
                da.SelectCommand.Parameters.Clear();
                da.SelectCommand.Parameters.AddWithValue("@action", obj.action);
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

        public bool UpdateBookingStatus(int bookingId, BookingStatusTypes status)
        {
            try
            {
                SqlConnection cn = new SqlConnection(strCon);

                string query = string.Empty;
                int proposed = status == BookingStatusTypes.PROPOSED ? 1 : 0;
                query = string.Format("update tblBooking set BookingStatusId = {0}, ProposedBooking = {1} where BookingId = {2}", (int)status, proposed, bookingId);

                SqlCommand cmd = new SqlCommand(query, cn);
                cmd.CommandType = CommandType.Text;
                cn.Open();
                int rowsEffected = cmd.ExecuteNonQuery();
                cn.Close();

                return true;
            }
            catch (Exception exp)
            {
                throw exp;
            }
        }

        public BALBooking GetBookingDetails(int bookingId)
        {
            try
            {
                SqlConnection cn = new SqlConnection(strCon);
                SqlCommand cmd = new SqlCommand(string.Format("select BookingID, BookingCode, StartDate, EndDate, NoOfPersons, AccomId from tblBooking where BookingId = {0}", bookingId), cn);
                //SqlCommand cmd = new SqlCommand(string.Format("select b.BookingID, b.BookingCode, b.StartDate, b.EndDate, b.NoOfPersons, b.AccomId,r.PaxStaying from tblBooking b inner join tblBookingRoom r on b.BookingId=r.bookingid where b.BookingId = {0}", bookingId), cn);
                cmd.CommandType = CommandType.Text;
                cn.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                BALBooking booking = null;
                if (reader.HasRows)
                {
                    reader.Read();
                    booking = new BALBooking();
                    booking._iBookingId = reader.GetValue(0) == DBNull.Value ? -1 : int.Parse(reader.GetValue(0).ToString());
                    booking.BookingCode = reader.GetValue(1) == DBNull.Value ? string.Empty : reader.GetValue(1).ToString();
                    booking._dtStartDate = reader.GetValue(2) == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(reader.GetValue(2).ToString());
                    booking._dtEndDate = reader.GetValue(3) == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(reader.GetValue(3).ToString());
                    //  booking._iPaxStaying = reader.GetValue(6) == DBNull.Value ? -1 : int.Parse(reader.GetValue(6).ToString());
                    //booking._iPaxStaying= Convert.ToInt32(Session["pax"]);
                    booking._iAccomId = reader.GetValue(5) == DBNull.Value ? -1 : int.Parse(reader.GetValue(5).ToString());
                }
                reader.Close();
                cn.Close();
                return booking;
            }
            catch (Exception exp)
            {
                throw exp;
            }
        }

        public int getRoomCategory(BALBooking obj)
        {
            try
            {
                SqlConnection cn = new SqlConnection(strCon);

                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = new SqlCommand("[dbo].[sp_CruiseBooking]", cn);
                da.SelectCommand.Parameters.Clear();
                da.SelectCommand.Parameters.AddWithValue("@action", obj.action);
                da.SelectCommand.Parameters.AddWithValue("@RoomId", obj.RoomId);
                da.SelectCommand.Parameters.AddWithValue("@packageid", obj.PackageId);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                cn.Open();
                da.SelectCommand.ExecuteReader();
                DataTable dtReturnData = new DataTable();
                cn.Close();
                da.Fill(dtReturnData);
                if (dtReturnData != null)
                {
                    int RoomcateId = Convert.ToInt32(dtReturnData.Rows[0]["RoomCategoryId"].ToString());
                    return RoomcateId;
                }
                else
                    return 0;
            }
            catch (Exception)
            {
                return 0;
            }
        }
        public DataTable fetchbybookingid(BALBooking obj)
        {
            try
            {
                SqlConnection cn = new SqlConnection(strCon);
                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = new SqlCommand("[dbo].[sp_CruiseBooking]", cn);
                da.SelectCommand.Parameters.Clear();
                da.SelectCommand.Parameters.AddWithValue("@action", obj.action);
                da.SelectCommand.Parameters.AddWithValue("@bookingId", obj._iBookingId);



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
        public DataTable getMaxRoomsBookable(BALBooking obj)
        {
            try
            {
                SqlConnection cn = new SqlConnection(strCon);
                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = new SqlCommand("[dbo].[sp_CruiseBooking]", cn);
                da.SelectCommand.Parameters.Clear();
                da.SelectCommand.Parameters.AddWithValue("@action", obj.action);
                da.SelectCommand.Parameters.AddWithValue("@accomid", obj.accomId);

                da.SelectCommand.Parameters.AddWithValue("@AgentId", obj._iAgentId);

                da.SelectCommand.Parameters.AddWithValue("@startdate", obj._dtStartDate);

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
        public DataTable paymentreminder(BALBooking obj)
        {
            try
            {
                SqlConnection cn = new SqlConnection(strCon);
                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = new SqlCommand("[dbo].[paymentremidermail]", cn);
                da.SelectCommand.Parameters.Clear();
                da.SelectCommand.Parameters.AddWithValue("@bookingId", obj._iBookingId);



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
        public DataTable countgeuset(BALBooking obj)
        {
            try
            {
                SqlConnection cn = new SqlConnection(strCon);
                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = new SqlCommand("[dbo].[paymentremidermail]", cn);
                da.SelectCommand.Parameters.Clear();
                da.SelectCommand.Parameters.AddWithValue("@bookingId", obj._iBookingId);



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
        public DataTable finalpaymentremidermail(BALBooking obj)
        {
            try
            {
                SqlConnection cn = new SqlConnection(strCon);
                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = new SqlCommand("[dbo].[finalpaymentremidermail]", cn);
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
        public DataTable getaccomtypeid(BALBooking obj)
        {
            try
            {
                SqlConnection cn = new SqlConnection(strCon);
                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = new SqlCommand("[dbo].[getaccomtypeid]", cn);
                da.SelectCommand.Parameters.Clear();
                da.SelectCommand.Parameters.AddWithValue("@accomid", obj.accomId);
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
        #region Insert/Update Data
        public int AddParentBookingDetail(BALBooking obj)
        {
            try
            {
                SqlConnection cn = new SqlConnection(strCon);
                SqlDataAdapter da = new SqlDataAdapter();
                da.InsertCommand = new SqlCommand("[dbo].[up_Ins_Booking]", cn);
                da.InsertCommand.Parameters.AddWithValue("@sBookingRef", obj._sBookingRef);
                da.InsertCommand.Parameters.AddWithValue("@dtStartDate", obj._dtStartDate);
                da.InsertCommand.Parameters.AddWithValue("@dtEndDate", obj._dtEndDate);
                da.InsertCommand.Parameters.AddWithValue("@iAccomTypeId", obj._iAccomTypeId);
                da.InsertCommand.Parameters.AddWithValue("@iAccomId", obj._iAccomId);
                da.InsertCommand.Parameters.AddWithValue("@iAgentId", obj._iAgentId);
                da.InsertCommand.Parameters.AddWithValue("@iAgentIdRef", obj._iAgentIdRef);
                da.InsertCommand.Parameters.AddWithValue("@iNights", obj._iNights);
                da.InsertCommand.Parameters.AddWithValue("@iPersons", obj._iPersons);
                da.InsertCommand.Parameters.AddWithValue("@BookingStatusId", obj._BookingStatusId);
                da.InsertCommand.Parameters.AddWithValue("@SeriesId", obj._SeriesId);
                da.InsertCommand.Parameters.AddWithValue("@proposedBooking", obj._proposedBooking);
                da.InsertCommand.Parameters.AddWithValue("@chartered", obj._chartered);
                da.InsertCommand.Parameters.AddWithValue("@CustomerId", obj.CustomerId);
                da.InsertCommand.Parameters.AddWithValue("@agentcommision", obj.agentcommission);
                da.InsertCommand.Parameters.AddWithValue("@packageid", obj.PackageId);

                da.InsertCommand.CommandType = CommandType.StoredProcedure;
                cn.Open();
                //int Status = da.InsertCommand.ExecuteNonQuery();
                var returnValue = da.InsertCommand.ExecuteScalar();
                cn.Close();

                int bookingId = -1;
                if (returnValue != null)
                    int.TryParse(returnValue.ToString(), out bookingId);

                return bookingId;
            }
            catch (Exception)
            {
                return 0;
            }
        }
        public int adddueamount(string email, decimal amount, string paymentmethod)
        {
            try
            {
                SqlConnection cn = new SqlConnection(strCon);
                SqlDataAdapter da = new SqlDataAdapter();
                da.InsertCommand = new SqlCommand("[dbo].[sp_DueAmount]", cn);
                da.InsertCommand.Parameters.AddWithValue("@action", "Save");
                da.InsertCommand.Parameters.AddWithValue("@EmailId", email);
                da.InsertCommand.Parameters.AddWithValue("@Dueamount", amount);
                da.InsertCommand.Parameters.AddWithValue("@PaymentMthod", paymentmethod);


                da.InsertCommand.CommandType = CommandType.StoredProcedure;
                cn.Open();
                //int Status = da.InsertCommand.ExecuteNonQuery();
                var returnValue = da.InsertCommand.ExecuteScalar();
                cn.Close();

                int bookingId = -1;
                if (returnValue != null)
                    int.TryParse(returnValue.ToString(), out bookingId);

                return bookingId;
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public int AddRoomBookingDetails(BALBooking obj)
        {
            try
            {
                SqlConnection cn = new SqlConnection(strCon);
                SqlDataAdapter da = new SqlDataAdapter();
                da.InsertCommand = new SqlCommand("[dbo].[up_Ins_BookingRoom]", cn);
                da.InsertCommand.Parameters.AddWithValue("@iBookingId", obj._iBookingId);
                da.InsertCommand.Parameters.AddWithValue("@dtStartDate", obj._dtStartDate);
                da.InsertCommand.Parameters.AddWithValue("@dtEndDate", obj._dtEndDate);
                da.InsertCommand.Parameters.AddWithValue("@iAccomId", obj._iAccomId);
                da.InsertCommand.Parameters.AddWithValue("@sRoomNo", obj._sRoomNo);
                da.InsertCommand.Parameters.AddWithValue("@iPaxStaying", obj._iPaxStaying);
                da.InsertCommand.Parameters.AddWithValue("@bConvertTo_Double_Twin", obj._bConvertTo_Double_Twin);
                da.InsertCommand.Parameters.AddWithValue("@cRoomStatus", obj._cRoomStatus);
                da.InsertCommand.Parameters.AddWithValue("@Amt", obj._Amt);
                da.InsertCommand.Parameters.AddWithValue("@Action", obj.action);
                da.InsertCommand.Parameters.AddWithValue("@paidAmt", obj._PaidAmount);
                da.InsertCommand.Parameters.AddWithValue("@PaymentId", obj.PaymentId);
                da.InsertCommand.Parameters.AddWithValue("@RoomCategoryId", obj.roomcatid);
                da.InsertCommand.Parameters.AddWithValue("@TaxableAmount", obj.taxableamount);
                da.InsertCommand.Parameters.AddWithValue("@TaxPercentage", obj.taxpercentage);
                da.InsertCommand.Parameters.AddWithValue("@TaxAmount", obj.taxamount);
                da.InsertCommand.Parameters.AddWithValue("@DiscountPercent", obj.Discount);
                da.InsertCommand.Parameters.AddWithValue("@DiscountAmount", obj.DiscountPrice);
                da.InsertCommand.Parameters.AddWithValue("@PricePerPerson", obj.priceperperson);
                da.InsertCommand.Parameters.AddWithValue("@Gross", obj.ToTal);
                da.InsertCommand.Parameters.AddWithValue("@bconfig", obj.bedconfig);
                da.InsertCommand.Parameters.AddWithValue("@invoiceno", obj.InvoiceNo);
                da.InsertCommand.Parameters.AddWithValue("@invoiceSequence", obj.InvoiceSequence);
                da.InsertCommand.CommandType = CommandType.StoredProcedure;
                cn.Open();
                int Status = da.InsertCommand.ExecuteNonQuery();
                cn.Close();
                if (Status > 0)
                    return Status;
                else
                    return 0;
            }
            catch (Exception exp)
            {
                throw exp;
            }
        }

        public void UpdatePaymentDetails(BALBooking balBookingPayment)
        {
            try
            {
                SqlConnection cn = new SqlConnection(strCon);
                cn.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cn;
                cmd.CommandType = CommandType.Text;

                string query = string.Format("update tblPayment set PaidAmt = {0}, PaymentDate = '{1}' where BookingId = {2} and paymentId = '{3}'",
                    balBookingPayment._PaidAmount,
                    DateTime.Now,
                    balBookingPayment._iBookingId,
                    balBookingPayment.PaymentId);

                cmd.CommandText = query;
                cmd.ExecuteNonQuery();
                cn.Close();
            }
            catch (Exception exp)
            {
                throw exp;
            }
        }
        #endregion
    }
}