using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using FarHorizon.Reservations.BusinessServices.Online.BAL;
using FarHorizon.Reservations.BusinessServices.Online.DAL;

public partial class Cruise_booking_CustomerLogin : System.Web.UI.Page
{

     BALCustomers blcus = new BALCustomers();
    DALCustomers dlcus = new DALCustomers();

    DataTable dtGetReturnedData;
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void Login1_Authenticate(object sender, AuthenticateEventArgs e)
    {

        try
        {
            blcus.Email = Login1.UserName.Trim().ToString();
            blcus.Password = Login1.Password.Trim().ToString();
            blcus.action = "LoginCust";
            dtGetReturnedData = new DataTable();
            dtGetReturnedData = dlcus.checkDuplicateemail(blcus);
            if (dtGetReturnedData != null)
            {
                Session.Add("CustName", dtGetReturnedData.Rows[0]["FirstName"].ToString());
                Session.Add("CustomerCode", dtGetReturnedData.Rows[0]["CustId"].ToString());
                Session.Add("CustomerMailId", Login1.UserName.Trim().ToString());
                Session.Add("CustPassword", dtGetReturnedData.Rows[0]["Password"].ToString());
                Response.Redirect("SearchProperty.aspx");

            }
        }
        catch
        {
        }
    }
}