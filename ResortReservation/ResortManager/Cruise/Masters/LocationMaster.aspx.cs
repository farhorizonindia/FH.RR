using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class Cruise_Masters_LocationMaster : System.Web.UI.Page
{
    BALLocation balloc = new BALLocation();
    DALLocations dalloc = new DALLocations();
    int Queryres = 0;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            bindlocation();
        }
    }

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        try
        {
            balloc.LocationName = txtLocation.Text.Trim();
            balloc.action = "InsertLoc";
            Queryres = dalloc.InsertLocation(balloc);
            if (Queryres > 0)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Showstatus", "javascript:alert('location added')", true);
                txtLocation.Text = "";

                bindlocation();
            }

            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Showstatus", "javascript:alert('location could not be  added')", true);
            }



        }

        catch
        {
        }
    }

    public void bindlocation()
    {
        try
        {
            balloc.action = "getallloc";
            DataTable dt = dalloc.getLocation(balloc);
            if (dt != null)
            {
                gdvlocations.DataSource = dt;
                gdvlocations.DataBind();
            }
            else
            {
                gdvlocations.DataSource = null;
                gdvlocations.DataBind();
            }

           
        }
        catch
        {
        }
    }



}