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
    /// Summary description for DALLogin
    /// </summary>
    public class DALLogin
    {
        string strCon = string.Empty;
        public DALLogin()
        {
            strCon = ConfigurationManager.ConnectionStrings["ReservationConnectionString"].ConnectionString;
        }

        #region Login

        public DataTable AgentLogin(BALLogin obj)
        {
            try
            {
                SqlConnection cn = new SqlConnection(strCon);

                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = new SqlCommand("[dbo].[sp_cruiseLogin]", cn);
                da.SelectCommand.Parameters.Clear();
                da.SelectCommand.Parameters.AddWithValue("@agentemailid", obj.EmailId);
                da.SelectCommand.Parameters.AddWithValue("@Password", obj.Password);

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
    }
}