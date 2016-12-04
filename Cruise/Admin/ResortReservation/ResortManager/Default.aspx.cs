using FarHorizon.Reservations.Bases.BasePages;
using FarHorizon.Reservations.BusinessServices;
using FarHorizon.Reservations.Common.DataEntities.Masters;
using System;
using System.Diagnostics;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class _Default : ClientBasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Debugger.IsAttached)
        {
            Login1.UserName = "admin";
        }
    }

    protected void Login1_Authenticate(object sender, AuthenticateEventArgs e)
    {
        UserDTO userDto;
        UserServices userServices;
        int iLoginId;
        userDto = new UserDTO();
        userServices = new UserServices();
        try
        {
            if (Login1.UserName.Trim().ToString() == "" || Login1.Password.Trim().ToString() == "")
                e.Authenticated = false;

            userDto.UserId = Login1.UserName.Trim().ToString();
            userDto.Password = Login1.Password.Trim().ToString();
            iLoginId = userServices.ValidateUser(userDto);
            if (iLoginId > 0)
            {
                e.Authenticated = true;
                SessionServices.LoginId = iLoginId;
            }
            else if (iLoginId <= 0)
            {
                iLoginId = 0;
                //SessionHandler"LoginId"] = 0;
                SessionServices.LoginId = iLoginId;
                e.Authenticated = false;
            }
        }
        catch (Exception exp)
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "errorMsg", "" + exp.Message + "');");
            //Console.Write(exp.Message);
        }
        finally
        {
            userServices = null;
            userDto = null;
        }
    }

    protected void Login1_LoggedIn(object sender, EventArgs e)
    {
        Response.Redirect("mainmenu.aspx", true);
    }

    protected void Login1_LoginError(object sender, EventArgs e)
    {
        //Label1.Visible = true;
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        UserDTO userDto;
        UserServices userServices;
        int iLoginId;
        userDto = new UserDTO();
        userServices = new UserServices();
        userDto.UserId = TextBox1.Text.Trim().ToString();
        userDto.Password = TextBox2.Text.Trim().ToString();
        iLoginId = userServices.ValidateUser(userDto);

        if (iLoginId > 0)
        {
            // e.Authenticated = true;

            SessionServices.LoginId = iLoginId;
            Response.Redirect("mainmenu.aspx", true);
        }
        else if (iLoginId <= 0)
        {
            iLoginId = 0;
            //SessionHandler"LoginId"] = 0;
            SessionServices.LoginId = iLoginId;
            //  e.Authenticated = false;
        }
    }
}