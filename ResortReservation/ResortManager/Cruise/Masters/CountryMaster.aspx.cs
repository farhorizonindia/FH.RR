using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using FarHorizon.Reservations.BusinessServices.Online.BAL;
using FarHorizon.Reservations.BusinessServices.Online.DAL;
using FarHorizon.Reservations.Bases.BasePages;
public partial class Cruise_Masters_CountryMaster : MasterBasePage
{

    DataTable dtgetreturneddata;
    BALLocation blloc = new BALLocation();
    DALLocations dlloc = new DALLocations();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            bindcountries();
        }
    }
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        try
        {
            if (btnAdd.Text == "Add")
            {
                blloc.action = "InsertCountries";
                blloc.countryname = txtLocation.Text;
                int res = dlloc.InsertCountry(blloc);
                if (res > 0)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Showstatus", "javascript:alert('Country Created')", true);
                    bindcountries();
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Showstatus", "javascript:alert('country could not be created ')", true);
                }
            }
            else
            {
                blloc.action = "UpdateCountry";
                blloc.countryname = txtLocation.Text;
                blloc.countryid=Convert.ToInt32(hfid.Value);

                int res = dlloc.UpdateCountry(blloc);
                if (res > 0)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Showstatus", "javascript:alert('country name updated ')", true);
                    bindcountries();
                    btnAdd.Text = "Submit";

                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Showstatus", "javascript:alert('country name could not be updated ')", true);
                }

            }

        }

        catch
        {
        }
    }

    public void bindcountries()
    {
        try
        {
            blloc.action = "GetallCountries";
            dtgetreturneddata = new DataTable();

            dtgetreturneddata = dlloc.getallCountries(blloc);
            if (dtgetreturneddata != null)
            {
                gdvlocations.DataSource = dtgetreturneddata;
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
    protected void gdvlocations_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Select")
        {
            try
            {
                LinkButton lnk = (LinkButton)e.CommandSource;
                GridViewRow grow = (GridViewRow)lnk.NamingContainer;
                int countryid = Convert.ToInt32(gdvlocations.DataKeys[grow.RowIndex].Value);
                hfid.Value = countryid.ToString();
                blloc.action = "getCountriesbyId";
                blloc.countryid = countryid;
                dtgetreturneddata = new DataTable();
                dtgetreturneddata = dlloc.getcountrybyId(blloc);
                if (dtgetreturneddata != null)
                {
                    txtLocation.Text = dtgetreturneddata.Rows[0]["CountryName"].ToString();
                    btnAdd.Text = "Update";

                   

                }
                else
                {

                    txtLocation.Text = "";
                }


               
            }

            catch
            {
            }
        }
    }
    protected void gdvlocations_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            int countryid = Convert.ToInt32(gdvlocations.DataKeys[e.RowIndex].Value);
            blloc.action = "DeleteCountry";
            blloc.countryid = countryid;
            int res = dlloc.DeleteCountry(blloc);
            if (res > 0)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Showstatus", "javascript:alert('country  Deleted')", true);
                bindcountries();
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Showstatus", "javascript:alert('country  could not be Deleted ')", true);

            }

        }

        catch
        {
        }
    }
}