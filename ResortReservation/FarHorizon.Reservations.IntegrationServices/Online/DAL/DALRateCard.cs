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

    public class DALRateCard
    {
        string strCon = string.Empty;
        public DALRateCard()
        {
            strCon = ConfigurationManager.ConnectionStrings["ReservationConnectionString"].ConnectionString;

        }
        #region GetData
        public DataTable BindControls(BALRateCard obj)
        {
            try
            {
                SqlConnection cn = new SqlConnection(strCon);

                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = new SqlCommand("[dbo].[sp_cardMaster]", cn);
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

        public DataTable GetRoomCatbyAccomid(BALRateCard obj)
        {
            try
            {
                SqlConnection cn = new SqlConnection(strCon);

                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = new SqlCommand("[dbo].[sp_cardMaster]", cn);
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

        public DataTable GetAccom(BALRateCard obj)
        {
            try
            {
                SqlConnection cn = new SqlConnection(strCon);

                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = new SqlCommand("[dbo].[sp_cardMaster]", cn);
                da.SelectCommand.Parameters.Clear();
                da.SelectCommand.Parameters.AddWithValue("@Action", obj._Action);
                da.SelectCommand.Parameters.AddWithValue("@AccomTypeId", obj._AccomTypeId);
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
        public DataTable GetAccomname(BALRateCard obj)
        {
            try
            {
                SqlConnection cn = new SqlConnection(strCon);

                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = new SqlCommand("[dbo].[sp_cardMaster]", cn);
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
        public DataTable GetallAccomname(BALRateCard obj)
        {
            try
            {
                SqlConnection cn = new SqlConnection(strCon);

                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = new SqlCommand("[dbo].[sp_cardMaster]", cn);
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
        public DataTable GetRoomsTypes(BALRateCard obj)
        {
            try
            {
                SqlConnection cn = new SqlConnection(strCon);

                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = new SqlCommand("[dbo].[sp_cardMaster]", cn);
                da.SelectCommand.Parameters.Clear();
                da.SelectCommand.Parameters.AddWithValue("@Action", obj._Action);
                da.SelectCommand.Parameters.AddWithValue("@AccomTypeId", obj._AccomTypeId);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                cn.Open();
                da.SelectCommand.ExecuteReader();
                DataTable dtReturnData = new DataTable();
                cn.Close();
                da.Fill(dtReturnData);
                if (dtReturnData != null)
                {
                    //DataTable dtUpdated = new DataTable();
                    //dtUpdated = dtAddNewRows(dtReturnData);
                    return dtReturnData;
                }
                else
                    return null;
            }
            catch (Exception)
            {
                return null;
            }
        }
        public DataTable dtAddNewRows(DataTable dtPrimary)
        {
            DataTable dtNew = new DataTable();
            dtNew = dtPrimary;
            DataRow dr = dtNew.NewRow();
            dr["RoomType"] = "Quad";
            dr["RoomTypeId"] = "00";
            dtNew.Rows.Add(dr);
            DataRow dr1 = dtNew.NewRow();
            dr1["RoomType"] = "Extra Bed";
            dr1["RoomTypeId"] = "00";
            dtNew.Rows.Add(dr1);
            return dtNew;
        }
        #endregion

        #region Insert/Update Data

        public int AddParentRateCard(BALRateCard obj)
        {
            try
            {
                SqlConnection cn = new SqlConnection(strCon);
                SqlDataAdapter da = new SqlDataAdapter();
                da.InsertCommand = new SqlCommand("[dbo].[sp_cardMaster]", cn);
                da.InsertCommand.Parameters.AddWithValue("@Action", obj._Action);
                da.InsertCommand.Parameters.AddWithValue("@RateCardId", obj._RateCardId);
                da.InsertCommand.Parameters.AddWithValue("@RateCategoryId", obj._RateCategoryId);
                da.InsertCommand.Parameters.AddWithValue("@AccomTypeId", obj._AccomTypeId);
                da.InsertCommand.Parameters.AddWithValue("@AccomId", obj._AccomId);
                da.InsertCommand.Parameters.AddWithValue("@RoomCategoryId", obj._RoomCategoryId);
                da.InsertCommand.Parameters.AddWithValue("@ValFrom", obj._ValFrom);
                da.InsertCommand.Parameters.AddWithValue("@ValTo", obj._ValTo);
                da.InsertCommand.Parameters.AddWithValue("@Season", obj._Season);
                da.InsertCommand.Parameters.AddWithValue("@minNights", obj._minNights);
                da.InsertCommand.Parameters.AddWithValue("@OperatingDays", obj._OperatingDays);
                da.InsertCommand.Parameters.AddWithValue("@AlloExtraBed", obj._AlloExtraBed);
                da.InsertCommand.Parameters.AddWithValue("@WebEnabled", obj._WebEnabled);
                da.InsertCommand.Parameters.AddWithValue("@TaxInclusive", obj._TaxInclusive);
                da.InsertCommand.Parameters.AddWithValue("@CommissionEnabled", obj._CommissionEnabled);
                da.InsertCommand.Parameters.AddWithValue("@RateTypeId", obj._RateTypeId);
                da.InsertCommand.Parameters.AddWithValue("@Currency", obj._Currency);
                da.InsertCommand.Parameters.AddWithValue("@Remark", obj._Remark);
                da.InsertCommand.Parameters.AddWithValue("@GITPaxFrom", obj.GITPaxFrom);
                da.InsertCommand.Parameters.AddWithValue("@TaxPct", obj.TaxPct);
                da.InsertCommand.Parameters.AddWithValue("@Description", obj.Description);
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
        public int AddFitGitRoomRate(BALRateCard obj)
        {
            try
            {
                SqlConnection cn = new SqlConnection(strCon);
                SqlDataAdapter da = new SqlDataAdapter();
                da.InsertCommand = new SqlCommand("[dbo].[sp_cardMaster]", cn);
                da.InsertCommand.Parameters.AddWithValue("@Action", obj._Action);
                da.InsertCommand.Parameters.AddWithValue("@RateCardId", obj._RateCardId);
                da.InsertCommand.Parameters.AddWithValue("@FitOrGit", obj._FitOrGit);
                da.InsertCommand.Parameters.AddWithValue("@RoomTypeId", obj._RoomTypeId);
                da.InsertCommand.Parameters.AddWithValue("@Amt", obj._Amt);
                da.InsertCommand.Parameters.AddWithValue("@agentid", obj.agentid);
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
        public int AddFitGitMealRate(BALRateCard obj)
        {
            try
            {
                SqlConnection cn = new SqlConnection(strCon);
                SqlDataAdapter da = new SqlDataAdapter();
                da.InsertCommand = new SqlCommand("[dbo].[sp_cardMaster]", cn);
                da.InsertCommand.Parameters.AddWithValue("@Action", obj._Action);
                da.InsertCommand.Parameters.AddWithValue("@WelcomeDrink", obj.WelcomeDrink);
                da.InsertCommand.Parameters.AddWithValue("@Breakfast", obj.Breakfast);
                da.InsertCommand.Parameters.AddWithValue("@Lunch", obj.Lunch);
                da.InsertCommand.Parameters.AddWithValue("@EveSnacks", obj.EveSnacks);
                da.InsertCommand.Parameters.AddWithValue("@Dinner", obj.Dinner);
                da.InsertCommand.Parameters.AddWithValue("@FitOrGit", obj._FitOrGit);
                da.InsertCommand.Parameters.AddWithValue("@RatecardId", obj._RateCardId);
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

        public int AddFitGitQuad(BALRateCard obj)
        {
            try
            {
                SqlConnection cn = new SqlConnection(strCon);
                SqlDataAdapter da = new SqlDataAdapter();
                da.InsertCommand = new SqlCommand("[dbo].[sp_cardMaster]", cn);
                da.InsertCommand.Parameters.AddWithValue("@Action", obj._Action);
                da.InsertCommand.Parameters.AddWithValue("@RateCardId", obj._RateCardId);
                da.InsertCommand.Parameters.AddWithValue("@FitOrGit", obj._FitOrGit);
                da.InsertCommand.Parameters.AddWithValue("@Quad", obj._Quad);
                da.InsertCommand.Parameters.AddWithValue("@ExtraBed", obj._ExtraBed);
                da.InsertCommand.CommandType = CommandType.StoredProcedure;
                cn.Open();
                int InsertStatus = da.InsertCommand.ExecuteNonQuery();
                cn.Close();
                if (InsertStatus > 0)
                    return InsertStatus;
                else
                    return 0;
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public DataTable getAllRatecards(BALRateCard obj)
        {
            try
            {
                SqlConnection cn = new SqlConnection(strCon);

                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = new SqlCommand("[dbo].[sp_cardMaster]", cn);
                da.SelectCommand.Parameters.Clear();
                da.SelectCommand.Parameters.AddWithValue("@action", obj._Action);

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


        public DataTable getRatecardbyId(BALRateCard obj)
        {
            try
            {
                SqlConnection cn = new SqlConnection(strCon);

                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = new SqlCommand("[dbo].[sp_cardMaster]", cn);
                da.SelectCommand.Parameters.Clear();
                da.SelectCommand.Parameters.AddWithValue("@action", obj._Action);
                da.SelectCommand.Parameters.AddWithValue("@RateCardId", obj._RateCardId);
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


        public int UpdateParentRateCard(BALRateCard obj)
        {
            try
            {
                SqlConnection cn = new SqlConnection(strCon);
                SqlDataAdapter da = new SqlDataAdapter();
                da.UpdateCommand = new SqlCommand("[dbo].[sp_cardMaster]", cn);
                da.UpdateCommand.Parameters.AddWithValue("@Action", obj._Action);
                da.UpdateCommand.Parameters.AddWithValue("@RateCardId", obj._RateCardId);
                da.UpdateCommand.Parameters.AddWithValue("@RateCategoryId", obj._RateCategoryId);
                da.UpdateCommand.Parameters.AddWithValue("@AccomTypeId", obj._AccomTypeId);
                da.UpdateCommand.Parameters.AddWithValue("@AccomId", obj._AccomId);
                da.UpdateCommand.Parameters.AddWithValue("@RoomCategoryId", obj._RoomCategoryId);
                da.UpdateCommand.Parameters.AddWithValue("@ValFrom", obj._ValFrom);
                da.UpdateCommand.Parameters.AddWithValue("@ValTo", obj._ValTo);
                da.UpdateCommand.Parameters.AddWithValue("@Season", obj._Season);
                da.UpdateCommand.Parameters.AddWithValue("@minNights", obj._minNights);
                da.UpdateCommand.Parameters.AddWithValue("@OperatingDays", obj._OperatingDays);
                da.UpdateCommand.Parameters.AddWithValue("@AlloExtraBed", obj._AlloExtraBed);
                da.UpdateCommand.Parameters.AddWithValue("@WebEnabled", obj._WebEnabled);
                da.UpdateCommand.Parameters.AddWithValue("@TaxInclusive", obj._TaxInclusive);
                da.UpdateCommand.Parameters.AddWithValue("@CommissionEnabled", obj._CommissionEnabled);
                da.UpdateCommand.Parameters.AddWithValue("@RateTypeId", obj._RateTypeId);
                da.UpdateCommand.Parameters.AddWithValue("@Currency", obj._Currency);
                da.UpdateCommand.Parameters.AddWithValue("@Remark", obj._Remark);
                da.UpdateCommand.Parameters.AddWithValue("@GITPaxFrom", obj.GITPaxFrom);
                da.UpdateCommand.Parameters.AddWithValue("@TaxPct", obj.TaxPct);
                da.UpdateCommand.Parameters.AddWithValue("@Description", obj.Description);
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


        public int UpdateFitGitRoomRate(BALRateCard obj)
        {
            try
            {
                SqlConnection cn = new SqlConnection(strCon);
                SqlDataAdapter da = new SqlDataAdapter();
                da.UpdateCommand = new SqlCommand("[dbo].[sp_cardMaster]", cn);
                da.UpdateCommand.Parameters.AddWithValue("@Action", obj._Action);
                da.UpdateCommand.Parameters.AddWithValue("@RateCardId", obj._RateCardId);
                da.UpdateCommand.Parameters.AddWithValue("@FitOrGit", obj._FitOrGit);
                da.UpdateCommand.Parameters.AddWithValue("@RoomTypeId", obj._RoomTypeId);
                da.UpdateCommand.Parameters.AddWithValue("@Amt", obj._Amt);
                da.UpdateCommand.Parameters.AddWithValue("@agentid", obj.agentid);
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

        public int UpdateFitGitMealRate(BALRateCard obj)
        {
            try
            {
                SqlConnection cn = new SqlConnection(strCon);
                SqlDataAdapter da = new SqlDataAdapter();
                da.UpdateCommand = new SqlCommand("[dbo].[sp_cardMaster]", cn);
                da.UpdateCommand.Parameters.AddWithValue("@Action", obj._Action);
                da.UpdateCommand.Parameters.AddWithValue("@WelcomeDrink", obj.WelcomeDrink);
                da.UpdateCommand.Parameters.AddWithValue("@Breakfast", obj.Breakfast);
                da.UpdateCommand.Parameters.AddWithValue("@Lunch", obj.Lunch);
                da.UpdateCommand.Parameters.AddWithValue("@EveSnacks", obj.EveSnacks);
                da.UpdateCommand.Parameters.AddWithValue("@Dinner", obj.Dinner);
                da.UpdateCommand.Parameters.AddWithValue("@FitOrGit", obj._FitOrGit);
                da.UpdateCommand.Parameters.AddWithValue("@RatecardId", obj._RateCardId);
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

        public int updateFitGitQuad(BALRateCard obj)
        {
            try
            {
                SqlConnection cn = new SqlConnection(strCon);
                SqlDataAdapter da = new SqlDataAdapter();
                da.UpdateCommand = new SqlCommand("[dbo].[sp_cardMaster]", cn);
                da.UpdateCommand.Parameters.AddWithValue("@Action", obj._Action);
                da.UpdateCommand.Parameters.AddWithValue("@RateCardId", obj._RateCardId);
                da.UpdateCommand.Parameters.AddWithValue("@FitOrGit", obj._FitOrGit);
                da.UpdateCommand.Parameters.AddWithValue("@Quad", obj._Quad);
                da.UpdateCommand.Parameters.AddWithValue("@ExtraBed", obj._ExtraBed);
                da.UpdateCommand.CommandType = CommandType.StoredProcedure;
                cn.Open();
                int InsertStatus = da.UpdateCommand.ExecuteNonQuery();
                cn.Close();
                if (InsertStatus > 0)
                    return InsertStatus;
                else
                    return 0;
            }
            catch (Exception)
            {
                return 0;
            }
        }



        public int DeleteHotelRateCard(BALRateCard obj)
        {
            try
            {
                SqlConnection cn = new SqlConnection(strCon);
                SqlDataAdapter da = new SqlDataAdapter();
                da.DeleteCommand = new SqlCommand("[dbo].[sp_cardMaster]", cn);
                da.DeleteCommand.Parameters.AddWithValue("@Action", obj._Action);
                da.DeleteCommand.Parameters.AddWithValue("@RateCardId", obj._RateCardId);

                da.DeleteCommand.CommandType = CommandType.StoredProcedure;
                cn.Open();
                int InsertStatus = da.DeleteCommand.ExecuteNonQuery();
                cn.Close();
                if (InsertStatus > 0)
                    return InsertStatus;
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