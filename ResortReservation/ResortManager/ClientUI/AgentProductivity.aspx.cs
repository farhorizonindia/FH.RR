using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using FarHorizon.Reservations.BusinessServices.Online.BAL;
using FarHorizon.Reservations.BusinessServices.Online.DAL;
using FarHorizon.Reservations.Bases.BasePages;
using System.Data;
public partial class ClientUI_AgentProductivity : MasterBasePage
{
    DALBooking dlbooking = new DALBooking();
    BALBooking blbooking = new BALBooking();
    protected void Page_Load(object sender, EventArgs e)
    {
       
        if (!IsPostBack)
        {
            load();
        }
    }
    private void load()
    {
        DataTable dt = dlbooking.agentproductivity(blbooking);
        if (dt != null && dt.Rows.Count > 0)
        {
            dgBookings.DataSource = dt;
            dgBookings.DataBind();
        }
    }

    protected void dgBookings_SelectedIndexChanged(object sender, EventArgs e)
    {

    }

    protected void dgBookings_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
    {
        dgBookings.CurrentPageIndex = e.NewPageIndex;
        load();
    }
}