using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using FarHorizon.Reservations.BusinessServices.Online.BAL;
using FarHorizon.Reservations.BusinessServices.Online.DAL;
using FarHorizon.Reservations.Common;
using FarHorizon.DataSecurity;
using FarHorizon.Reservations.Common.DataEntities.Masters;

public partial class Cruise_booking_agentLogin : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void Login1_Authenticate(object sender, AuthenticateEventArgs e)
    {
        BALLogin bllog = new BALLogin();
        DALLogin dllog = new DALLogin();

        try
        {
            if (Login1.UserName.Trim().ToString() == "" || Login1.Password.Trim().ToString() == "")
                e.Authenticated = false;

            bllog.EmailId = Login1.UserName.Trim().ToString();
            bllog.Password = Login1.Password.Trim().ToString();                       

            AgentDTO agent = dllog.AgentLogin(bllog);
            if (agent != null)
            {
                Session.Add("UserName", agent.AgentName);
                Session.Add("UserCode", agent.AgentId);
                Session.Add("AgentMailId", Login1.UserName.Trim().ToString());
                Session.Add("Password", Login1.Password.Trim().ToString());
                Response.Redirect("SearchProperty.aspx?Aid=" + agent.AgentId.ToString() + " ");
            }
        }
        catch (Exception exp)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Showstatus", "javascript:alert('" + exp.Message + "')", true);            
            throw exp;            
        }
    }

}