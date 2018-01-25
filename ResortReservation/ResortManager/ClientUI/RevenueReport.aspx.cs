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
public partial class ClientUI_RevenueReport : MasterBasePage
{
    DALBooking dlbooking = new DALBooking();
    BALBooking blbooking = new BALBooking();
    protected void Page_Load(object sender, EventArgs e)
    {
       
        if (!IsPostBack)
        {
            getgross();
        }
    }
    private void getgross()
    {

        DataTable dt = dlbooking.RevenueReport(blbooking);
        if (dt != null && dt.Rows.Count > 0)
        {
            lblRevenue.Text = "INR" + dt.Rows[0]["totalincome"].ToString();
        }
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        lblMsg.Text = "";
        if (txtStartDate.Text != "" && txtEndDate.Text != "")
        {
            blbooking._dtStartDate = Convert.ToDateTime(txtStartDate.Text);
            blbooking._dtEndDate = Convert.ToDateTime(txtEndDate.Text.ToString());
            DataTable dt = dlbooking.RevenueReport(blbooking);
            if (dt != null && dt.Rows.Count > 0)
            {
                lblRevenue.Text = dt.Rows[0]["totalincome"].ToString();
            }
        }
        else
        {
            lblMsg.Text = "Please Enter Start date and End date";
            lblMsg.ForeColor = System.Drawing.Color.Red;
            return;
        }
    }
}