using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class Hotel_HotelInvoice : System.Web.UI.Page
{
    DataTable Bookingdt;
    DALHotelBooking dlht = new DALHotelBooking();
    BALHotelBooking blht = new BALHotelBooking();
    DataTable retdt;

    double total = 0;

    protected void Page_Load(object sender, EventArgs e)
    {
        Bookingdt = new DataTable();
        Bookingdt = Session["Bookingdt"] as DataTable;
        gdvSelectedRooms.DataSource = Bookingdt;
        gdvSelectedRooms.DataBind();
        lblArrvDate.Text = Session["Chkin"].ToString();
        lblDepartDate.Text = Session["chkout"].ToString(); ;
        lblacm.Text = lblAccomName.Text = Session["AccomName"].ToString();

        CalcAmount();
        getbalance();


    }

    public void CalcAmount()
    {
        try
        {
            for (int i = 0; i < gdvSelectedRooms.Rows.Count; i++)
            {
                total = total + Convert.ToDouble(gdvSelectedRooms.Rows[i].Cells[6].Text);
            }
            lblTotoAmt.Text = total.ToString();
        }

        catch
        {
        }
    }

    public void getbalance()
    {
        try
        {
            blht.action = "GetBookDetails";
            blht.iBookingId = Convert.ToInt32(Session["maxBookId"]);
            DataTable dt1 = dlht.getPaymentDetails(blht);

            lblBalance.Text = dt1.Rows[0][0].ToString();
        }

        catch
        {
        }
    }





    protected void gdvSelectedRooms_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {

            try
            {
                retdt = new DataTable();
                retdt = Session["RoomInfo"] as DataTable;
                Label roomcatid = (Label)e.Row.FindControl("lblRoomCatId");

                DataView dv = new DataView(retdt);
                dv.RowFilter = "RoomCategoryId='" + roomcatid.Text + "' ";
                e.Row.Cells[7].Text = dv.ToTable().Rows[0]["TaxPct"].ToString();
                e.Row.Cells[8].Text = ((Convert.ToDouble(e.Row.Cells[7].Text) * Convert.ToDouble(e.Row.Cells[6].Text)) / 100).ToString();

            }
            catch
            {


            }

        }
    }
}