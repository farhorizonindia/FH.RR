using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class Cruise_Booking_PackageSearch : System.Web.UI.Page
{

    DALOpenDates dlOpenDates = new DALOpenDates();
    BALOpenDates blOpenDates = new BALOpenDates();
    DataTable dtGetReturnedData;
    int getQueryResponse = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            LoadCountries();
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
                ddlDestination.DataSource = dtGetReturnedData;
                ddlDestination.DataTextField = "CountryName";
                ddlDestination.DataValueField = "CountryId";
                ddlDestination.DataBind();
                ddlDestination.Items.Insert(0, "-Select-");
            }
            else
            {
                ddlDestination.Items.Clear();
                ddlDestination.DataSource = null;
                ddlDestination.DataBind();
                ddlDestination.Items.Insert(0, "-No country-");
            }
        }
        catch (Exception sqe)
        {
            ddlDestination.Items.Clear();
            ddlDestination.DataSource = null;
            ddlDestination.DataBind();
            ddlDestination.Items.Insert(0, "-No Accom-");

        }
    }


    protected void btnSearch_Click(object sender, EventArgs e)
    {

    }
}