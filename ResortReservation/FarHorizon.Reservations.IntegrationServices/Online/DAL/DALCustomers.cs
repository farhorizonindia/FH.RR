﻿using FarHorizon.DataSecurity;
using FarHorizon.Reservations.BusinessServices.Online.BAL;
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

/// <summary>
/// Summary description for DALCustomers
/// </summary>

namespace FarHorizon.Reservations.BusinessServices.Online.DAL
{ 
    public class DALCustomers
    {
        string strCon = string.Empty;

        public DALCustomers()
        {
            strCon = ConfigurationManager.ConnectionStrings["ReservationConnectionString"].ConnectionString;
        }

        #region AddCustomers
        public int AddCustomers(BALCustomers obj)
        {
            try
            {
                SqlConnection cn = new SqlConnection(strCon);
                SqlDataAdapter da = new SqlDataAdapter();
                da.InsertCommand = new SqlCommand("[dbo].[sp_customers]", cn);
                da.InsertCommand.Parameters.AddWithValue("@action", obj.action);
                da.InsertCommand.Parameters.AddWithValue("@Address1", DataSecurityManager.Encrypt(obj.Address1));
                da.InsertCommand.Parameters.AddWithValue("@Address2", DataSecurityManager.Encrypt(obj.Address2));
                da.InsertCommand.Parameters.AddWithValue("@City", DataSecurityManager.Encrypt(obj.City));
                da.InsertCommand.Parameters.AddWithValue("@CountryId", obj.CountryId);
                da.InsertCommand.Parameters.AddWithValue("@Password", DataSecurityManager.Encrypt(obj.Password));
                da.InsertCommand.Parameters.AddWithValue("@Email", DataSecurityManager.Encrypt(obj.Email));
                da.InsertCommand.Parameters.AddWithValue("@FirstName", DataSecurityManager.Encrypt(obj.FirstName));
                da.InsertCommand.Parameters.AddWithValue("@LastName", DataSecurityManager.Encrypt(obj.LastName));
                da.InsertCommand.Parameters.AddWithValue("@PostalCode", DataSecurityManager.Encrypt(obj.PostalCode));
                da.InsertCommand.Parameters.AddWithValue("@State", DataSecurityManager.Encrypt(obj.State));
                da.InsertCommand.Parameters.AddWithValue("@Telephone", DataSecurityManager.Encrypt(obj.Telephone));
                da.InsertCommand.Parameters.AddWithValue("@Title", DataSecurityManager.Encrypt(obj.Title));
                da.InsertCommand.Parameters.AddWithValue("@PaymentMethod", DataSecurityManager.Encrypt(obj.PaymentMethod));

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

        public DataTable checkDuplicateemail(BALCustomers obj)
        {
            try
            {
                SqlConnection cn = new SqlConnection(strCon);

                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = new SqlCommand("[dbo].[sp_customers]", cn);
                da.SelectCommand.Parameters.Clear();
                da.SelectCommand.Parameters.AddWithValue("@action", obj.action);
                da.SelectCommand.Parameters.AddWithValue("@Email", DataSecurityManager.Encrypt(obj.Email));
                da.SelectCommand.Parameters.AddWithValue("@Password", DataSecurityManager.Encrypt(obj.Password));
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

        public DataTable GetBookingByBookingId(BALCustomers obj)
        {
            try
            {
                SqlConnection cn = new SqlConnection(strCon);

                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = new SqlCommand("up_Get_BookingsClient", cn);
                da.SelectCommand.Parameters.Clear();


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
            catch (Exception)
            {
                return null;
            }
        }

        public DataTable GetBookingByCustId(BALCustomers obj)
        {
            try
            {
                SqlConnection cn = new SqlConnection(strCon);

                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = new SqlCommand("up_Get_BookingsCust", cn);
                da.SelectCommand.Parameters.Clear();


                da.SelectCommand.Parameters.AddWithValue("@CustId", obj.CustId);
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

        public string GetBillingAddress(DataRow dataRow)
        {
            string address1;
            string address2;
            string city;
            string state;
            string postalCode;

            address1 = DataSecurityManager.Decrypt(dataRow["Address1"].ToString());
            address2 = DataSecurityManager.Decrypt(dataRow["Address2"].ToString());
            city = DataSecurityManager.Decrypt(dataRow["City"].ToString());
            state = DataSecurityManager.Decrypt(dataRow["State"].ToString());
            postalCode = DataSecurityManager.Decrypt(dataRow["PostalCode"].ToString());

            return string.Format("{0} {1}, {2} {3} {4}", address1, address2, city, state, postalCode);
        }
    }
}