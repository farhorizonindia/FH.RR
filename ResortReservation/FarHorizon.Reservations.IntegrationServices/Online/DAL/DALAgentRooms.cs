using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Globalization;
using FarHorizon.Reservations.BusinessServices.Online.BAL;

namespace FarHorizon.Reservations.BusinessServices.Online.DAL
{

    public class DALAgentRooms
    {
        string strCon = string.Empty;
        public DALAgentRooms()
        {
            strCon = ConfigurationManager.ConnectionStrings["ReservationConnectionString"].ConnectionString;

        }
        #region Get Data

        public DataTable BindControls(BALAgentRooms obj)
        {
            try
            {
                SqlConnection cn = new SqlConnection(strCon);
                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = new SqlCommand("[dbo].[sp_agentMaxRooms]", cn);
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
        public DataTable GetConfirmation(BALAgentRooms obj)
        {
            try
            {
                SqlConnection cn = new SqlConnection(strCon);
                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = new SqlCommand("[dbo].[sp_agentMaxRooms]", cn);
                da.SelectCommand.Parameters.Clear();
                da.SelectCommand.Parameters.AddWithValue("@Action", obj._Action);
                da.SelectCommand.Parameters.AddWithValue("@AccomId", obj._AccomId);
                da.SelectCommand.Parameters.AddWithValue("@maxRooms", obj._maxRooms);

                da.SelectCommand.Parameters.AddWithValue("@date", obj._Date);
                da.SelectCommand.Parameters.AddWithValue("@ToDate", obj.toDate);
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
        public DataTable GetExists(BALAgentRooms obj)
        {
            try
            {
                SqlConnection cn = new SqlConnection(strCon);
                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = new SqlCommand("[dbo].[sp_agentMaxRooms]", cn);
                da.SelectCommand.Parameters.Clear();
                da.SelectCommand.Parameters.AddWithValue("@Action", obj._Action);
                da.SelectCommand.Parameters.AddWithValue("@AccomId", obj._AccomId);
                da.SelectCommand.Parameters.AddWithValue("@AgentId", obj._AgentId);

                da.SelectCommand.Parameters.AddWithValue("@date", obj._Date);
                da.SelectCommand.Parameters.AddWithValue("@ToDate", obj.toDate);
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

        #region Insert/Update Data

        public int AddAgentRoom(BALAgentRooms obj)
        {
            try
            {
                SqlConnection cn = new SqlConnection(strCon);
                SqlDataAdapter da = new SqlDataAdapter();
                da.InsertCommand = new SqlCommand("[dbo].[sp_agentMaxRooms]", cn);
                da.InsertCommand.Parameters.AddWithValue("@Action", obj._Action);
                da.InsertCommand.Parameters.AddWithValue("@AccomId", obj._AccomId);
                da.InsertCommand.Parameters.AddWithValue("@AgentId", obj._AgentId);
                da.InsertCommand.Parameters.AddWithValue("@maxRooms", obj._maxRooms);
                da.InsertCommand.Parameters.AddWithValue("@date", obj._Date);
                da.InsertCommand.Parameters.AddWithValue("@ToDate", obj.toDate);
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
        public int UpdateRooms(BALAgentRooms obj)
        {
            try
            {
                SqlConnection cn = new SqlConnection(strCon);
                SqlDataAdapter da = new SqlDataAdapter();
                da.UpdateCommand = new SqlCommand("[dbo].[sp_agentMaxRooms]", cn);
                da.UpdateCommand.Parameters.AddWithValue("@Action", obj._Action);
                da.UpdateCommand.Parameters.AddWithValue("@AccomId", obj._AccomId);
                da.UpdateCommand.Parameters.AddWithValue("@AgentId", obj._AgentId);
                da.UpdateCommand.Parameters.AddWithValue("@maxRooms", obj._maxRooms);
                da.UpdateCommand.Parameters.AddWithValue("@date", obj._Date);
                da.UpdateCommand.Parameters.AddWithValue("@ToDate", obj.toDate);

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
        #endregion

        public int AddRoomImages(BALAgentRooms obj)
        {
            try
            {
                SqlConnection cn = new SqlConnection(strCon);
                SqlDataAdapter da = new SqlDataAdapter();
                da.InsertCommand = new SqlCommand("sp_RoomCategoryImages", cn);
                da.InsertCommand.Parameters.AddWithValue("@Action", obj._Action);
                da.InsertCommand.Parameters.AddWithValue("@AccomId", obj._AccomId);
                da.InsertCommand.Parameters.AddWithValue("@RoomCategoryId", obj.RoomCategoryId);
                da.InsertCommand.Parameters.AddWithValue("@ImagePath", obj.ImagePath);

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

        public DataTable getallRoomImages(BALAgentRooms obj)
        {
            try
            {
                SqlConnection cn = new SqlConnection(strCon);
                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = new SqlCommand("sp_RoomCategoryImages", cn);
                da.SelectCommand.Parameters.Clear();
                da.SelectCommand.Parameters.AddWithValue("@Action", obj._Action);
                da.SelectCommand.Parameters.AddWithValue("@AccomId", obj._AccomId);
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

        public DataTable getImageById(BALAgentRooms obj)
        {

            try
            {
                SqlConnection cn = new SqlConnection(strCon);
                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = new SqlCommand("sp_RoomCategoryImages", cn);
                da.SelectCommand.Parameters.Clear();
                da.SelectCommand.Parameters.AddWithValue("@Action", obj._Action);
                da.SelectCommand.Parameters.AddWithValue("@Id", obj._id);
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

        public int UpdateRoomImages(BALAgentRooms obj)
        {
            try
            {
                SqlConnection cn = new SqlConnection(strCon);
                SqlDataAdapter da = new SqlDataAdapter();
                da.UpdateCommand = new SqlCommand("sp_RoomCategoryImages", cn);
                da.UpdateCommand.Parameters.AddWithValue("@Action", obj._Action);
                da.UpdateCommand.Parameters.AddWithValue("@Id", obj._id);
                da.UpdateCommand.Parameters.AddWithValue("@AccomId", obj._AccomId);
                da.UpdateCommand.Parameters.AddWithValue("@RoomCategoryId", obj.RoomCategoryId);
                da.UpdateCommand.Parameters.AddWithValue("@ImagePath", obj.ImagePath);

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

        public int DeleteRoomImages(BALAgentRooms obj)
        {
            try
            {
                SqlConnection cn = new SqlConnection(strCon);
                SqlDataAdapter da = new SqlDataAdapter();
                da.DeleteCommand = new SqlCommand("sp_RoomCategoryImages", cn);
                da.DeleteCommand.Parameters.AddWithValue("@Action", obj._Action);
                da.DeleteCommand.Parameters.AddWithValue("@Id", obj._id);
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

        public DataTable GetBookableRoomDetails(BALAgentRooms obj)
        {
            try
            {
                SqlConnection cn = new SqlConnection(strCon);
                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = new SqlCommand("[dbo].[sp_agentMaxRooms]", cn);
                da.SelectCommand.Parameters.Clear();
                da.SelectCommand.Parameters.AddWithValue("@Action", obj._Action);
                da.SelectCommand.Parameters.AddWithValue("@AgentId", obj._AgentId);
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

        public int DeletebookableInfo(BALAgentRooms obj)
        {
            try
            {
                SqlConnection cn = new SqlConnection(strCon);
                SqlDataAdapter da = new SqlDataAdapter();
                da.DeleteCommand = new SqlCommand("[dbo].[sp_agentMaxRooms]", cn);
                da.DeleteCommand.Parameters.AddWithValue("@Action", obj._Action);
                da.DeleteCommand.Parameters.AddWithValue("@Id", obj._id);
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