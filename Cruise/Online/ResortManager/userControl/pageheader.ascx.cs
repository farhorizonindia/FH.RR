using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using FarHorizon.Reservations.Common;
using FarHorizon.Reservations.BusinessServices;

public partial class userControl_pageheader : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        lnkBooking.NavigateUrl = "~/ClientUI/Booking.aspx?bookingId=0";
        lnkBookingChartView.NavigateUrl = "~/ClientUI/BookingChartView.aspx";
        lnkViewBookings.NavigateUrl = "~/ClientUI/ViewBookings.aspx";
        lnkMainmenu.NavigateUrl = "~/mainmenu.aspx";

        FillLoggedInUserInfo();
    }

    private void userControl_pageheader_OnLoad(object sender, EventArgs evt)
    {

    }

    private void FillLoggedInUserInfo()
    {
        string userInfo = string.Empty;
        if (SessionServices.LoggedInUser != null)
        {
            if (SessionServices.LoggedInUser.User != null)
            {
                userInfo = "User:" + SessionServices.LoggedInUser.User.UserId;
                if (SessionServices.LoggedInUser.RoleRigthsList != null && SessionServices.LoggedInUser.RoleRigthsList.Count > 0)
                {
                    userInfo += "[Role: " + SessionServices.LoggedInUser.RoleRigthsList[0].RoleName + "]";
                }
            }
        }
        lblUserInfo.Text = userInfo;
    }

    public string PageTitle
    {
        set { lblpageTitle.Text = value; }
    }

    protected void btnLogout_Click(object sender, EventArgs e)
    {
        SessionServices.AbandonSession();        
        Response.Redirect("~/loggedout.aspx?op=logout");
    }
}
