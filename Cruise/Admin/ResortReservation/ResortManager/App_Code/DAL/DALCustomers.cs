using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

/// <summary>
/// Summary description for DALCustomers
/// </summary>
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
            da.InsertCommand.Parameters.AddWithValue("@Address1", obj.Address1);
            da.InsertCommand.Parameters.AddWithValue("@Address2", obj.Address2);
            da.InsertCommand.Parameters.AddWithValue("@City", obj.City);
            da.InsertCommand.Parameters.AddWithValue("@CountryId", obj.CountryId);
            da.InsertCommand.Parameters.AddWithValue("@Password", obj.Password);
            da.InsertCommand.Parameters.AddWithValue("@Email", obj.Email);
            da.InsertCommand.Parameters.AddWithValue("@FirstName", obj.FirstName);
            da.InsertCommand.Parameters.AddWithValue("@LastName", obj.LastName);
            da.InsertCommand.Parameters.AddWithValue("@PostalCode", obj.PostalCode);
            da.InsertCommand.Parameters.AddWithValue("@State", obj.State);
            da.InsertCommand.Parameters.AddWithValue("@Telephone", obj.Telephone);
            da.InsertCommand.Parameters.AddWithValue("@Title", obj.Title);
            da.InsertCommand.Parameters.AddWithValue("@PaymentMethod", obj.PaymentMethod);
 
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
            da.SelectCommand.Parameters.AddWithValue("@Email", obj.Email);
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
}