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

    public class DALDeparture
    {
        string strCon = string.Empty;
        public DALDeparture()
        {
            strCon = ConfigurationManager.ConnectionStrings["ReservationConnectionString"].ConnectionString;
        }



        #region Get Data
        public DataTable BindControls(BALDeparture obj)
        {
            try
            {
                SqlConnection cn = new SqlConnection(strCon);

                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = new SqlCommand("[dbo].[sp_Departure]", cn);
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
        #endregion

        #region Insert / Update Data
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

    }
}