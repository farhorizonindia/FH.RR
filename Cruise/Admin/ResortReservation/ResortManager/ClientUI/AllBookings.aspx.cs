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
using FarHorizon.Reservations.Common.DataEntities.Client;
using FarHorizon.Reservations.BusinessServices;
using FarHorizon.Reservations.Bases;
using FarHorizon.Reservations.Bases.BasePages;

public partial class ClientUI_AllBookings : ClientBasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
            BindGrid();
    }
    
    private void BindGrid()
    {
        string sRoomNo = Convert.ToString(Request.QueryString["roomno"]);
        BookingServices oBookingManager = new BookingServices();
        BookingDTO[] oBookingData = oBookingManager.GetRoomBookings(sRoomNo);
        if (oBookingData != null)
        {
            if (oBookingData.Length > 0)
            {
                dgBookings.DataSource = oBookingData;
                dgBookings.DataBind();
            }
        }
    }
}
