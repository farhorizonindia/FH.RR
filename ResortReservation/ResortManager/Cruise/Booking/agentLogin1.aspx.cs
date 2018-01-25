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

public partial class Cruise_Booking_agentLogin1 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["CustName"] != null)
        {
            lblUsername.Text = "Hello " + Session["CustName"].ToString();
            //lnkCustomerRegis.Visible = false;
            navlogin.Visible = false;
            LinkButton1.Visible = true;
        }

        else if (Session["UserName"] != null)

        {
            lblUsername.Text = "Hello " + Session["UserName"].ToString();
            //lnkCustomerRegis.Visible = false;
            navlogin.Visible = false;
            LinkButton1.Visible = true;
        }
        else
        {
            //lnkCustomerRegis.Visible = false;
            navlogin.Visible = true;
            LinkButton1.Visible = false;
        }
    }

    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        Session.Clear();
        Session.Abandon();
        Session.RemoveAll();
        System.Web.Security.FormsAuthentication.SignOut();
        LinkButton1.Visible = false;
        Response.Redirect("SearchProperty1.aspx");
    }

    protected void btnCustLogin_Click(object sender, EventArgs e)
    {
        BALLogin bllog = new BALLogin();
        DALLogin dllog = new DALLogin();

        try
        {
            if (txtCustMailId.Value.Trim() == "" || txtCustPass.Value.Trim() == "")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Showstatus", "javascript:alert('please enter valid email or password')", true);
                return;
            }

            bllog.EmailId = txtCustMailId.Value.Trim();
            bllog.Password = txtCustPass.Value.Trim();
            bllog.BookingId = Convert.ToInt32(Request.QueryString["bid"]);

            AgentDTO agent = dllog.AgentLogin(bllog);
            if (agent != null)
            {
                Session.Add("UserName", agent.AgentName);
                Session.Add("UserCode", agent.AgentId);
                Session.Add("AgentMailId", txtCustMailId.Value.Trim());
                Session.Add("Password", txtCustPass.Value.Trim());
                
               
                {
                    Response.Redirect("SearchProperty1.aspx?Aid=" + agent.AgentId.ToString() + " ");
                }

            }
            else
            {
                txtCustMailId.Value = "";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Showstatus", "javascript:alert('please enter valid email or password')", true);
                return;
            }
        }
        catch (Exception exp)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Showstatus", "javascript:alert('" + exp.Message + "')", true);
            throw exp;
        }
    }

    protected void btnCustLogin_Click1(object sender, EventArgs e)
    {
        Response.Redirect("../Booking/ForgotPassword.aspx");
    }
}
