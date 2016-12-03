using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;


public partial class Cruise_Masters_CruiseOpenDatesMaster : System.Web.UI.Page
{
    DALOpenDates dlOpenDates = new DALOpenDates();
    BALOpenDates blOpenDates = new BALOpenDates();
    DataTable dtGetReturnedData;
    int getQueryResponse = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            this.LoadAccom();
            this.LoadCountries();
            this.LoadAllPackages();
            this.BindGridOpenDates();

        }
    }
    #region UDF
    private void LoadAccom()
    {
        try
        {
            blOpenDates._Action = "GetAllAccom";
            dtGetReturnedData = dlOpenDates.BindControls(blOpenDates);
            if (dtGetReturnedData.Rows.Count > 0)
            {
                ddlAccom.DataSource = dtGetReturnedData;
                ddlAccom.DataTextField = "AccomName";
                ddlAccom.DataValueField = "AccomId";
                ddlAccom.DataBind();
                ddlAccom.Items.Insert(0, "-Select-");
            }
            else
            {
                ddlAccom.Items.Clear();
                ddlAccom.DataSource = null;
                ddlAccom.DataBind();
                ddlAccom.Items.Insert(0, "-No Accom-");
            }
        }
        catch (Exception sqe)
        {
            ddlAccom.Items.Clear();
            ddlAccom.DataSource = null;
            ddlAccom.DataBind();
            ddlAccom.Items.Insert(0, "-No Accom-");

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

    private void LoadAllPackages()
    {
        try
        {
            blOpenDates._Action = "GetPackages";
            dtGetReturnedData = dlOpenDates.BindControls(blOpenDates);
            if (dtGetReturnedData.Rows.Count > 0)
            {
                ddlpackage.DataSource = dtGetReturnedData;
                ddlpackage.DataTextField = "PackageName";
                ddlpackage.DataValueField = "PackageId";
                ddlpackage.DataBind();
                ddlpackage.Items.Insert(0, "-Select-");
            }
            else
            {
                ddlpackage.Items.Clear();
                ddlpackage.DataSource = null;
                ddlpackage.DataBind();
                ddlpackage.Items.Insert(0, "-No Package-");
            }
        }
        catch (Exception sqe)
        {
            ddlpackage.Items.Clear();
            ddlpackage.DataSource = null;
            ddlpackage.DataBind();
            ddlpackage.Items.Insert(0, "-No Package-");

        }
    }

    private void BindGridOpenDates()
    {
        try
        {
            blOpenDates._Action = "GetAllOpenDates";
            dtGetReturnedData = dlOpenDates.BindControls(blOpenDates);
            if (dtGetReturnedData.Rows.Count > 0)
            {
                GridOpenDates.DataSource = dtGetReturnedData;
                GridOpenDates.DataBind();
            }
            else
            {
                GridOpenDates.DataSource = null;
                GridOpenDates.DataBind();
            }
        }
        catch (Exception sqe)
        {
            GridOpenDates.DataSource = null;
            GridOpenDates.DataBind();
        }
    }

    #endregion

    #region Control Click Events
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        try
        {
            blOpenDates._Action = "AddNewOpenDate";
            blOpenDates._checkInDate = Convert.ToDateTime(txtBoardingDate.Text.ToString().Trim());
            blOpenDates._checkOutDate = Convert.ToDateTime(txtDeBordingDate.Text.ToString().Trim());
            blOpenDates._RiverId = Convert.ToInt32(ddlRiver.SelectedItem.Value);
            blOpenDates._CountryId = Convert.ToInt32(ddlCountry.SelectedItem.Value);
            blOpenDates._PackageId = ddlpackage.SelectedItem.Value.ToString();
            blOpenDates._AccomId = Convert.ToInt32(ddlAccom.SelectedItem.Value);
            blOpenDates.Status = true;
            getQueryResponse = dlOpenDates.AddNewOpenDate(blOpenDates);
            if (getQueryResponse > 0)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Showstatus", "javascript:alert('Cruise OpenDate has been added successfully')", true);
            }
        }
        catch (Exception sqe)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Showstatus", "javascript:alert('cannot add this Open date. please see error log.')", true);
        }
    }
    protected void ddlCountry_SelectedIndexChanged(object sender, EventArgs e)
    {

        #region Bind RiverDD
        try
        {
            blOpenDates._Action = "GetRiver";
            blOpenDates._CountryId = Convert.ToInt32(ddlCountry.SelectedItem.Value);
            dtGetReturnedData = dlOpenDates.GetRiverLocation(blOpenDates);
            if (dtGetReturnedData.Rows.Count > 0)
            {
                ddlRiver.DataSource = dtGetReturnedData;
                ddlRiver.DataTextField = "RiverName";
                ddlRiver.DataValueField = "RiverId";
                ddlRiver.DataBind();
                ddlRiver.Items.Insert(0, "-Select-");
            }
            else
            {
                ddlRiver.Items.Clear();
                ddlRiver.DataSource = null;
                ddlRiver.DataBind();
                ddlRiver.Items.Insert(0, "-No River-");
            }
        }
        catch (Exception sqe)
        {
            ddlRiver.Items.Clear();
            ddlRiver.DataSource = null;
            ddlRiver.DataBind();
            ddlRiver.Items.Insert(0, "-No River-");

        }
        #endregion



    }
    protected void btnAdd0_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.RawUrl);
    }

    protected void txtBoardingDate_TextChanged(object sender, EventArgs e)
    {
        try
        {
            DateTime date = Convert.ToDateTime(txtBoardingDate.Text);
            int NoOfNights = Convert.ToInt32(hdnfNoOfRooms.Value);
            DateTime dtcalculatedChuckOut = date.AddDays(NoOfNights);
            txtDeBordingDate.Text = dtcalculatedChuckOut.ToShortDateString();
        }
        catch
        {
            txtDeBordingDate.Text = string.Empty;
            txtBoardingDate.Text = string.Empty;
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Showstatus", "javascript:alert('Please select a package first.')", true);

        }
    }
    protected void ddlpackage_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {

            blOpenDates._Action = "GetNoOfNights";
            blOpenDates._PackageId =ddlpackage.SelectedItem.Value;
            dtGetReturnedData = dlOpenDates.GetNoOfNights(blOpenDates);
            if (dtGetReturnedData.Rows.Count > 0)
            {
                hdnfNoOfRooms.Value = dtGetReturnedData.Rows[0]["NoOfNights"].ToString();
            }
            else
            {
                hdnfNoOfRooms.Value = string.Empty;
            }
            txtBoardingDate.Text = string.Empty;
            txtDeBordingDate.Text = string.Empty;

        }
        catch
        {


        }
    }
    #endregion



   
}