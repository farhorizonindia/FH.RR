using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

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
            da.SelectCommand.Parameters.AddWithValue("@dtEndDate",obj._dtEndDate);
            da.SelectCommand.Parameters.AddWithValue("@iAccomTypeId",obj._iAccomTypeId);
            da.SelectCommand.Parameters.AddWithValue("@iAccomId", obj._iAccomId);
            da.SelectCommand.Parameters.AddWithValue("@iBookingId",obj._iBookingId);
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
            da.SelectCommand.Parameters.AddWithValue("@totpax", obj.totpax);
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
    #endregion

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
            da.InsertCommand.Parameters.AddWithValue("@iNights", obj._iNights);
            da.InsertCommand.Parameters.AddWithValue("@iPersons", obj._iPersons);
            da.InsertCommand.Parameters.AddWithValue("@BookingStatusId", obj._BookingStatusId);
            da.InsertCommand.Parameters.AddWithValue("@SeriesId", obj._SeriesId);
            da.InsertCommand.Parameters.AddWithValue("@proposedBooking", obj._proposedBooking);
            da.InsertCommand.Parameters.AddWithValue("@chartered", obj._chartered);
            da.InsertCommand.Parameters.AddWithValue("@CustomerId", obj.CustomerId);

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
            da.InsertCommand.Parameters.AddWithValue("@paidAmt", obj._Paid);
            da.InsertCommand.Parameters.AddWithValue("@PaymentId", obj.PaymentId);

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


}