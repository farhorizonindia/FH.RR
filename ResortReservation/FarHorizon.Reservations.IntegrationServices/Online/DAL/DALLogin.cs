using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using FarHorizon.Reservations.BusinessServices.Online.BAL;
using FarHorizon.DataSecurity;

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
                string query = "select * from tblAgentMaster where AgentEmailId='" + DataSecurityManager.Encrypt(obj.EmailId) + "' and [password]='" + DataSecurityManager.Encrypt(obj.Password) + "'";

                SqlCommand cmd = new SqlCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = query;
                cmd.Connection = cn;

                //SqlDataAdapter da = new SqlDataAdapter();
                //da.SelectCommand = new SqlCommand("[dbo].[sp_cruiseLogin]", cn);
                //da.SelectCommand.Parameters.Clear();
                //da.SelectCommand.Parameters.AddWithValue("@agentemailid", DataSecurityManager.Encrypt(obj.EmailId));
                //da.SelectCommand.Parameters.AddWithValue("@Password", DataSecurityManager.Encrypt(obj.Password));

                //da.SelectCommand.CommandType = CommandType.StoredProcedure;
                cn.Open();

                SqlDataReader reader = cmd.ExecuteReader();
                
                //da.SelectCommand.ExecuteReader();
                DataTable dtReturnData = new DataTable();
                dtReturnData.Load(reader);

                cn.Close();
                //da.Fill(dtReturnData);
                if (dtReturnData != null)
                    return dtReturnData;
                else
                    return null;
            }
            catch (Exception exp)
            {
                throw exp;
                //return null;
            }
        }
        #endregion
    }
}