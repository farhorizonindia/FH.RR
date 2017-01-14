using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using FarHorizon.Reservations.BusinessServices.Online.BAL;
using FarHorizon.Reservations.BusinessServices.Online.DAL;

public partial class Cruise_DepartureMaster : System.Web.UI.Page
{
    BALDeparture bldeparture = new BALDeparture();
    DALDeparture dlDeparture = new DALDeparture();
    DataTable dtGetReturnedData;
    int getQueryResponse = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            this.GetAllCruiseAccom();
            this.GetPackages();
        }

    }

    #region UDF
    private void GetAllCruiseAccom()
    {
        try
        {
            bldeparture._Action = "GetAllCruise";
            dtGetReturnedData = dlDeparture.BindControls(bldeparture);
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
                ddlAccom.DataSource = null;
                ddlAccom.DataBind();
                ddlAccom.Items.Insert(0, "-No cruise-");
            }
        }
        catch(Exception sqe)
        {
            ddlAccom.DataSource = null;
            ddlAccom.DataBind();
            ddlAccom.Items.Insert(0, "-No cruise-");
        }
    }
    private void GetPackages()
    {
        try
        {
            bldeparture._Action = "getAllPackages";
            dtGetReturnedData = dlDeparture.BindControls(bldeparture);
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
                ddlpackage.DataSource = null;
                ddlpackage.DataBind();
                ddlpackage.Items.Insert(0, "-No cruise-");
            }
        }
        catch (Exception sqe)
        {
            ddlpackage.DataSource = null;
            ddlpackage.DataBind();
            ddlpackage.Items.Insert(0, "-No cruise-");
        }
    }
    #endregion

    #region Control Events
    protected void btnSbmit_Click(object sender, EventArgs e)
    {
        try
        {
            
        }
        catch(Exception sqe)
        {

        }

    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        try
        {
            Response.Redirect(Request.RawUrl);
        }
        catch(Exception sqe)
        {
 
        }
    }
    #endregion
    protected void ddlpackage_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
}