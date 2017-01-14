using FarHorizon.Reservations.BusinessServices.Online.BAL;
using FarHorizon.Reservations.BusinessServices.Online.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Cruise_booking_ViewBookings : System.Web.UI.Page
{
    BALAgentPayment blAgentPayment = new BALAgentPayment();
    DALAgentPayment dlAgentPayment = new DALAgentPayment();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
 
        }
    }

    private void BindSummeriseGrid()
    {
        try
        {

        }
        catch(Exception sqe)
        {
 
        }
    }
}