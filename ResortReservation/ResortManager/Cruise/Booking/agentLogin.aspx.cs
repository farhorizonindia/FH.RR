using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using FarHorizon.Reservations.BusinessServices.Online.BAL;
using FarHorizon.Reservations.BusinessServices.Online.DAL;

public partial class Cruise_booking_agentLogin : System.Web.UI.Page
{

    BALLogin bllog = new BALLogin();
    DALLogin dllog = new DALLogin();
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void Login1_Authenticate(object sender, AuthenticateEventArgs e)
    {
        try
        {
            if (Login1.UserName.Trim().ToString() == "" || Login1.Password.Trim().ToString() == "")
            e.Authenticated = false;
            bllog.EmailId = Login1.UserName.Trim().ToString();
            bllog.Password = Login1.Password.Trim().ToString();
            DataTable dt = dllog.AgentLogin(bllog);
            if (dt != null && dt.Rows.Count > 0)
            {
                Session.Add("UserName", dt.Rows[0]["AgentName"].ToString());
                Session.Add("UserCode", dt.Rows[0]["AgentId"].ToString());
                Session.Add("AgentMailId", Login1.UserName.Trim().ToString());
                Session.Add("Password", Login1.Password.Trim().ToString());
          Response.Redirect("SearchProperty.aspx?Aid=" + dt.Rows[0]["AgentId"].ToString() + " ");
            }
            else
            {


            }
        }
        catch
        {
        }
    }

}