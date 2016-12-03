using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Diagnostics;
using FarHorizon.Reservations.BusinessServices;
using FarHorizon.Reservations.MasterServices;
using FarHorizon.Reservations.Common.DataEntities.Masters;
using FarHorizon.Reservations.UserManager;
using FarHorizon.Reservations.Bases;
using FarHorizon.Reservations.Bases.BasePages;

public partial class _Default : ClientBasePage 
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //if (Debugger.IsAttached)
        //{
        //    Login1.UserName = "admin";
        //}
        Response.Redirect("~/Cruise/booking/SearchProperty.aspx");
       
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
            
                SessionServices.LoginId = iLoginId; 
                e.Authenticated = false;
            }
        }
        catch (Exception exp)
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "errorMsg", "" + exp.Message + "');");
           
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
       
    }
}