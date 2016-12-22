using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;


public partial class Cruise_booking_ViewAllBookings : System.Web.UI.Page
{

    BALCustomers blcus = new BALCustomers();
    DALCustomers dlcus = new DALCustomers();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindGrid();
           
        }
    }

    public void BindGrid()
    {
        try
        {


            if (Session["UserCode"] != null)
            {
                blcus.AgentId = Convert.ToInt32(Session["UserCode"]);
                DataTable dt = dlcus.GetBookingByBookingId(blcus);
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        gdvAllBookings.DataSource = dt;
                        gdvAllBookings.DataBind();
                    }
                    else
                    {
                        gdvAllBookings.DataSource = null;
                        gdvAllBookings.DataBind();
                    }
                }
                else
                {
                    gdvAllBookings.DataSource = null;
                    gdvAllBookings.DataBind();

                }
            }
            else if (Session["CustomerCode"] != null)
            {
                blcus.CustId = Convert.ToInt32(Session["CustomerCode"]);
                DataTable dt = dlcus.GetBookingByCustId(blcus);
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {

                        gdvAllBookings.DataSource = dt;
                        gdvAllBookings.DataBind();
                    }
                    else
                    {
                        gdvAllBookings.DataSource = null;
                        gdvAllBookings.DataBind();
                    }
                }
                else
                {
                    gdvAllBookings.DataSource = null;
                    gdvAllBookings.DataBind();

                }


            }

            Removezeroes();


        }
        catch
        {

        }
    }

    public void Removezeroes()
    {
        try
        {
            foreach (GridViewRow grow in gdvAllBookings.Rows)
            {
                grow.Cells[12].Text = Convert.ToDouble(grow.Cells[12].Text).ToString("#.##");
            }
        }
        catch
        {
        }
    }



    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        Session.Clear();
        Session.Abandon();
        Session.RemoveAll();
        System.Web.Security.FormsAuthentication.SignOut();
        Response.Redirect("SearchProperty.aspx");
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        Response.Redirect("SearchProperty.aspx");
    }
    protected void gdvAllBookings_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gdvAllBookings.PageIndex = e.NewPageIndex;
        BindGrid();

    }
}