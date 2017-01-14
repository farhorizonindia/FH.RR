using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using FarHorizon.Reservations.BusinessServices.Online.BAL;
using FarHorizon.Reservations.BusinessServices.Online.DAL;

public partial class Cruise_Masters_LocationMaster : System.Web.UI.Page
{
    BALLocation balloc = new BALLocation();
    DALLocations dalloc = new DALLocations();

    BALOpenDates blOpenDates = new BALOpenDates();
    DALOpenDates dlOpenDates = new DALOpenDates();
    int Queryres = 0;

    DataTable dtGetReturnedData;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            bindlocation();
            this.LoadCountries();
        }
    }

    

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        try
        {

            balloc.LocationName = txtLocation.Text.Trim();
            balloc.countryid = Convert.ToInt32(ddlCountry.SelectedValue);
            balloc.Description = txtDesc.Text;
            if (btnAdd.Text == "Add")
            {
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
            else
            {
                balloc.action = "UpdateLoc";
                balloc.LocationId = Convert.ToInt32(hfid.Value);
               

                Queryres = dalloc.UpdateLocation(balloc);
                if (Queryres > 0)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Showstatus", "javascript:alert('location updated')", true);
                    txtLocation.Text = "";

                    bindlocation();
                    txtLocation.Text = "";
                    ddlCountry.SelectedIndex = 0;
                    btnAdd.Text = "Add";
                }

                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Showstatus", "javascript:alert('location could not be  updated')", true);
                }

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



    private void LoadCountries()
    {
        try
        {
            blOpenDates._Action = "GetCountry";
            dtGetReturnedData = dlOpenDates.BindControls(blOpenDates);
            if (dtGetReturnedData.Rows.Count > 0)
            {
                ddlCountry.DataSource = dtGetReturnedData;
                ddlCountry.DataTextField = "CountryName";
                ddlCountry.DataValueField = "CountryId";
                ddlCountry.DataBind();
                ddlCountry.Items.Insert(0, "-Select-");
            }
            else
            {
                ddlCountry.Items.Clear();
                ddlCountry.DataSource = null;
                ddlCountry.DataBind();
                ddlCountry.Items.Insert(0, "-No Accom-");
            }
        }
        catch (Exception sqe)
        {
            ddlCountry.Items.Clear();
            ddlCountry.DataSource = null;
            ddlCountry.DataBind();
            ddlCountry.Items.Insert(0, "-No Accom-");

        }
    }



    protected void gdvlocations_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            if (e.CommandName == "Select")
            {
                LinkButton lnk = (LinkButton)e.CommandSource;
                GridViewRow grow = (GridViewRow)lnk.NamingContainer;
                int locid = Convert.ToInt32(gdvlocations.DataKeys[grow.RowIndex].Value);
                hfid.Value = locid.ToString();
                balloc.LocationId = locid;
                balloc.action = "GetLocById";
                dtGetReturnedData = new DataTable();
                dtGetReturnedData = dalloc.getLocationById(balloc);
                if (dtGetReturnedData != null)
                {
                    txtLocation.Text = dtGetReturnedData.Rows[0]["LocationName"].ToString();
                    ddlCountry.SelectedValue = dtGetReturnedData.Rows[0]["CountryId"].ToString();
                    txtDesc.Text = dtGetReturnedData.Rows[0]["Description"].ToString();
                    btnAdd.Text = "Update";
                }
                else
                {
                    txtLocation.Text = "";
                    ddlCountry.SelectedIndex = 0;
                }


            }
        }
        catch
        {
        }
    }
    protected void gdvlocations_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            int locid = Convert.ToInt32(gdvlocations.DataKeys[e.RowIndex].Value);
            balloc.LocationId = locid;
            balloc.action = "DeleteLocation";
            int res = dalloc.DeleteLocation(balloc);
            if (res > 0)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Showstatus", "javascript:alert('location Deleted')", true);
                bindlocation();
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Showstatus", "javascript:alert('location could not be  Deleted')", true);
            }
        }

        catch
        {
        }
    }
}