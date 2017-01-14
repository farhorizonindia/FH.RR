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
    /// Summary description for DALHotelBooking
    /// </summary>
    public class DALHotelBooking
    {

        string strCon = string.Empty;
        public DALHotelBooking()
        {
            strCon = ConfigurationManager.ConnectionStrings["ReservationConnectionString"].ConnectionString;
        }


        public DataTable GetHotelRates(BALHotelBooking obj)
        {
            try
            {
                SqlConnection cn = new SqlConnection(strCon);

                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = new SqlCommand("[dbo].[sp_hotelRates]", cn);
                da.SelectCommand.Parameters.Clear();
                da.SelectCommand.Parameters.AddWithValue("@action", obj.action);


                da.SelectCommand.Parameters.AddWithValue("@Accomid", obj.Accomid);
                da.SelectCommand.Parameters.AddWithValue("@AgentId", obj.agentid);
                da.SelectCommand.Parameters.AddWithValue("@totpax", obj.TotPax);
                da.SelectCommand.Parameters.AddWithValue("@Reqnoofrooms", obj.Reqnoofrooms);
                da.SelectCommand.Parameters.AddWithValue("@startdate", obj.checkin);
                da.SelectCommand.Parameters.AddWithValue("@enddate", obj.Checkout);

                da.SelectCommand.Parameters.AddWithValue("@roomtypeId", obj.RoomTypeId);
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

        public DataTable getrooms(BALHotelBooking obj)
        {
            try
            {
                SqlConnection cn = new SqlConnection(strCon);

                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = new SqlCommand("[dbo].[sp_hotelRates]", cn);
                da.SelectCommand.Parameters.Clear();
                da.SelectCommand.Parameters.AddWithValue("@action", obj.action);


                da.SelectCommand.Parameters.AddWithValue("@Accomid", obj.Accomid);

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

        public DataTable getmaxPax(BALHotelBooking obj)
        {
            try
            {
                SqlConnection cn = new SqlConnection(strCon);

                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = new SqlCommand("[dbo].[sp_hotelRates]", cn);
                da.SelectCommand.Parameters.Clear();
                da.SelectCommand.Parameters.AddWithValue("@action", obj.action);


                da.SelectCommand.Parameters.AddWithValue("@Accomid", obj.Accomid);
                da.SelectCommand.Parameters.AddWithValue("@roomno", obj.roomno);

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

        public DataTable getRoomCatId(BALHotelBooking obj)
        {

            try
            {
                SqlConnection cn = new SqlConnection(strCon);

                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = new SqlCommand("[dbo].[sp_hotelRates]", cn);
                da.SelectCommand.Parameters.Clear();
                da.SelectCommand.Parameters.AddWithValue("@action", obj.action);


                da.SelectCommand.Parameters.AddWithValue("@Accomid", obj.Accomid);
                da.SelectCommand.Parameters.AddWithValue("@roomno", obj.roomno);

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



        public DataTable GetMealPlans(BALHotelBooking obj)
        {
            try
            {
                SqlConnection cn = new SqlConnection(strCon);

                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = new SqlCommand("[dbo].[sp_hotelRates]", cn);
                da.SelectCommand.Parameters.Clear();
                da.SelectCommand.Parameters.AddWithValue("@action", obj.action);
                da.SelectCommand.Parameters.AddWithValue("@Accomid", obj.Accomid);
                da.SelectCommand.Parameters.AddWithValue("@AgentId", obj.agentid);
                da.SelectCommand.Parameters.AddWithValue("@totpax", obj.TotPax);




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
        public int AddBookingMealRates(BALHotelBooking obj)
        {
            try
            {
                SqlConnection cn = new SqlConnection(strCon);
                SqlDataAdapter da = new SqlDataAdapter();
                da.InsertCommand = new SqlCommand("[dbo].[up_Ins_BookingMealPlan]", cn);
                da.InsertCommand.Parameters.AddWithValue("@iBookingId", obj.iBookingId);
                da.InsertCommand.Parameters.AddWithValue("@iMealPlanId", obj.iMealPlanId);
                da.InsertCommand.Parameters.AddWithValue("@bBreakfast", obj.bBreakfast);
                da.InsertCommand.Parameters.AddWithValue("@bDinner", obj.bDinner);
                da.InsertCommand.Parameters.AddWithValue("@bEveSnacks", obj.bEveSnacks);
                da.InsertCommand.Parameters.AddWithValue("@bLunch", obj.bLunch);
                da.InsertCommand.Parameters.AddWithValue("@bWelcomeDrink", obj.bWelcomeDrink);
                da.InsertCommand.Parameters.AddWithValue("@dtMealDate", obj.dtMealDate);



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




        public DataTable GetRoomNosToBook(BALHotelBooking obj)
        {
            try
            {
                SqlConnection cn = new SqlConnection(strCon);

                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = new SqlCommand("[dbo].[sp_hotelRates]", cn);
                da.SelectCommand.Parameters.Clear();
                da.SelectCommand.Parameters.AddWithValue("@action", obj.action);
                da.SelectCommand.Parameters.AddWithValue("@startdate", obj.checkin);
                da.SelectCommand.Parameters.AddWithValue("@enddate", obj.Checkout);
                da.SelectCommand.Parameters.AddWithValue("@Accomid", obj.Accomid);

                da.SelectCommand.Parameters.AddWithValue("@RoomCateId", obj.RoomCateId);
                da.SelectCommand.Parameters.AddWithValue("@RoomTypeId", obj.RoomTypeId);
                da.SelectCommand.Parameters.AddWithValue("@Reqnoofrooms", obj.Reqnoofrooms);
                da.SelectCommand.Parameters.AddWithValue("@Convertible_To_Double", obj.Convertible_To_Double);
                da.SelectCommand.Parameters.AddWithValue("@roomstring", obj.roomstring);
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

        public DataTable getPaymentDetails(BALHotelBooking obj)
        {
            try
            {
                SqlConnection cn = new SqlConnection(strCon);

                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = new SqlCommand("[dbo].[sp_hotelRates]", cn);
                da.SelectCommand.Parameters.Clear();
                da.SelectCommand.Parameters.AddWithValue("@action", obj.action);


                da.SelectCommand.Parameters.AddWithValue("@bookingId", obj.iBookingId);


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