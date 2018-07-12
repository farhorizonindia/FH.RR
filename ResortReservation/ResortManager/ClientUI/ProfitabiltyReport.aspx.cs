using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using FarHorizon.Reservations.BusinessServices.Online.BAL;
using FarHorizon.Reservations.BusinessServices.Online.DAL;
using System.Data;
using FarHorizon.Reservations.Bases.BasePages;

public partial class ClientUI_ProfitabiltyReport : MasterBasePage
{
    DALBooking dlbooking = new DALBooking();
    BALBooking blbooking = new BALBooking();
    double revenue = 0;
    double commission = 0;
    double Profitabilitya = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            getall();
        }
    }
    private void getall()
    {
        DataTable dt = dlbooking.Profitability(blbooking);
        if (dt != null && dt.Rows.Count > 0)
        {
            lblRevenue.Text = dt.Rows[0]["totalincome"].ToString();
            lblCommissionamount.Text = dt.Rows[0]["AgentCommision"].ToString();
            lblProfitability.Text = dt.Rows[0]["Profiability"].ToString();
        }
    }
}