using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;

public partial class Rate_PackageMaster : System.Web.UI.Page
{
    BALPackageMaster blPackage = new BALPackageMaster();
    DALPackageMaster dlPakage = new DALPackageMaster();
    DataTable dtGetReturnedData;
    int getQueryResponse = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            this.BindGridPackages();
            this.LoadHotels();
            this.LoadDestinations();
        }
    }
   
    #region UDF
    private void AddPackageNights(string PackageId)
    {
        for (int LoopCounter = 0; LoopCounter < GridCityEachNight.Rows.Count; LoopCounter++)
        {
            try
            {
                Label Night = (Label)GridCityEachNight.Rows[LoopCounter].Cells[1].FindControl("lbNights");
                DropDownList ddlCity = (DropDownList)GridCityEachNight.Rows[LoopCounter].Cells[2].FindControl("ddlcity");
                RadioButton rbCheckInYes = (RadioButton)GridCityEachNight.Rows[LoopCounter].Cells[3].FindControl("rbCheckInYes");
                RadioButton rbcheckInNo = (RadioButton)GridCityEachNight.Rows[LoopCounter].Cells[3].FindControl("rbcheckInNo");
                RadioButton rbCheckOutYes = (RadioButton)GridCityEachNight.Rows[LoopCounter].Cells[4].FindControl("rbCheckOutYes");
                RadioButton rbcheckOutNo = (RadioButton)GridCityEachNight.Rows[LoopCounter].Cells[4].FindControl("rbcheckOutNo");
                int PackCityId = Convert.ToInt32(ddlCity.SelectedItem.Value);
                int packHotelId = Convert.ToInt32(ddlHotel.SelectedItem.Value);
                blPackage._Action = "AddPackageNights";
                blPackage._packageId = PackageId.ToString();
                blPackage._night = Night.Text.ToString();
                blPackage._cityId = PackCityId;
                if (rbCheckInYes.Checked == true)
                    blPackage._AllowCheckIn = true;
                else
                    blPackage._AllowCheckIn = false;

                if (rbCheckOutYes.Checked == true)
                    blPackage._AllowCheckOut = true;
                else
                    blPackage._AllowCheckOut = false;

                getQueryResponse = dlPakage.AddPackageNights(blPackage);
                if (getQueryResponse > 0)
                {
                }
            }
            catch (Exception sqe)
            {

            }
        }

    }
    private void BindGridPackages()
    {
        try
        {
            blPackage._Action = "BindGridPackages";
            dtGetReturnedData = dlPakage.BindControls(blPackage);
            if (dtGetReturnedData.Rows.Count > 0)
            {
                GridPackages.DataSource = dtGetReturnedData;
                GridPackages.DataBind();
            }
            else
            {
                GridPackages.DataSource = null;
                GridPackages.DataBind();
            }
        }
        catch
        {
            GridPackages.DataSource = null;
            GridPackages.DataBind();
        }
    }
    private void LoadHotels()
    {
        try
        {
           
            blPackage._Action = "GetAllHotels";
            dtGetReturnedData = dlPakage.BindControls(blPackage);
            if (dtGetReturnedData.Rows.Count > 0)
            {
                ddlHotel.DataSource = dtGetReturnedData;
                ddlHotel.DataTextField = "AccomName";
                ddlHotel.DataValueField = "AccomId";
                ddlHotel.DataBind();
                ddlHotel.Items.Insert(0, "-Select Hotel-");
            }
            else
            {
                ddlHotel.Items.Clear();
                ddlHotel.Items.Insert(0, "-No Hotel-");
            }

        }
        catch

        {

        }
    }
    private void LoadDestinations()
    {
        try
        {

            blPackage._Action = "GetAllDestinations";
            dtGetReturnedData = dlPakage.BindControls(blPackage);
            if (dtGetReturnedData.Rows.Count > 0)
            {
                ddlBoardingFrom.DataSource = dtGetReturnedData;
                ddlBoardingFrom.DataTextField = "LocationName";
                ddlBoardingFrom.DataValueField = "LocationId";
                ddlBoardingFrom.DataBind();
                ddlBoardingFrom.Items.Insert(0, "-Select-");

                ddlBoardingTo.DataSource = dtGetReturnedData;
                ddlBoardingTo.DataTextField = "LocationName";
                ddlBoardingTo.DataValueField = "LocationId";
                ddlBoardingTo.DataBind();
                ddlBoardingTo.Items.Insert(0, "-Select-");
            }
            else
            {
                ddlBoardingFrom.Items.Clear();
                ddlBoardingFrom.Items.Insert(0, "-No City-");
                ddlBoardingTo.Items.Clear();
                ddlBoardingTo.Items.Insert(0, "-No City-");
            }
        }
        catch
        {

        }
    }
    private void ClearControls()
    {
        ddlMasterPackage.SelectedItem.Text = "-Select-";
        GridCityEachNight.DataSource = null;
        GridCityEachNight.DataBind();
        ddlPackageType.DataSource = null;
        ddlPackageType.DataBind();
        txtPackageName.Text = string.Empty;
        ddlnights.SelectedIndex = -1;
        ddlBoardingFrom.SelectedIndex = -1;
        ddlBoardingTo.SelectedIndex = -1;
        ddlHotel.SelectedIndex = -1;


    }
    #endregion

    #region Click events
    protected void btnSbmit_Click(object sender, EventArgs e)
    {
        try
        {
            blPackage._Action = "GetCountPackages";
            dtGetReturnedData = dlPakage.BindControls(blPackage);
            int CountId = Convert.ToInt32(dtGetReturnedData.Rows[0]["count"]);
            string NewPackageId = "Pack" + (CountId + 1).ToString();
            blPackage._packageId = NewPackageId.ToString();
            blPackage._Action = "AddNewPackage";
            blPackage._creationDate = System.DateTime.Now;
            blPackage._packageName = txtPackageName.Text.ToString();
            blPackage._NoOfNights = Convert.ToInt32(ddlnights.SelectedItem.Text);
            blPackage._pakageType = ddlPackageType.SelectedItem.Text.ToString();
            if (ddlPackageType.SelectedItem.Text == "Child Package")
                blPackage._MasterPackageId = ddlMasterPackage.SelectedItem.Value;
            else
                blPackage._MasterPackageId = null;
            blPackage._HotelId = Convert.ToInt32(ddlHotel.SelectedItem.Value);
            blPackage._BoardingFrom = Convert.ToInt32(ddlBoardingFrom.SelectedItem.Value);
            blPackage._BoardingTo = Convert.ToInt32(ddlBoardingTo.SelectedItem.Value);
            getQueryResponse = dlPakage.AddNewPackage(blPackage);
            if (getQueryResponse > 0)
            {
                this.AddPackageNights(NewPackageId);
                this.BindGridPackages();
                this.ClearControls();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Showstatus", "javascript:alert('Package Added Successfully')", true);
            }
            else
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Showstatus", "javascript:alert('Not  Added')", true);
        }
        catch (Exception sqe)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Showstatus", "javascript:alert('Please check entries')", true);

        }
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.RawUrl);
    }
    protected void ddlNights_changeEvent(object sender, EventArgs e)
    {
        try
        {
            if (ddlnights.SelectedItem.Text == "-Select-")
            {
                GridCityEachNight.DataSource = null;
                GridCityEachNight.DataBind();
            }
            else
            {
                DataTable dtNights = new DataTable();
                dtNights.Columns.Add("Sn");
                dtNights.Columns.Add("Nights");
                for (int LoopCount = 0; LoopCount < Convert.ToInt32(ddlnights.SelectedItem.Text); LoopCount++)
                {
                    DataRow dr = dtNights.NewRow();
                    dr["Sn"] = (LoopCount + 1).ToString();
                    dr["Nights"] = "Night " + (LoopCount + 1).ToString();
                    dtNights.Rows.Add(dr);
                }
                GridCityEachNight.DataSource = dtNights;
                GridCityEachNight.DataBind();

            }


        }
        catch (Exception sqe)
        {
        }
    }
    protected void ddlpackageType_selectChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlPackageType.SelectedItem.Text == "Child Package")
            {
                ddlMasterPackage.Items.Clear();
                ddlMasterPackage.Enabled = true;
                reqfddlMasterPackage.Enabled = true;
                blPackage._Action = "GetAllPackages";
                dtGetReturnedData = dlPakage.BindControls(blPackage);
                if (dtGetReturnedData.Rows.Count > 0)
                {
                    ddlMasterPackage.DataSource = dtGetReturnedData;
                    ddlMasterPackage.DataTextField = "PackageName";
                    ddlMasterPackage.DataValueField = "PackageId";
                    ddlMasterPackage.DataBind();
                    ddlMasterPackage.Items.Insert(0, "-Master Package-");
                }
                else
                {
                    ddlMasterPackage.Items.Clear();
                    ddlMasterPackage.Items.Insert(0, "-No package-");
                }
            }
            else if (ddlPackageType.SelectedItem.Text == "-Select-" || ddlPackageType.SelectedItem.Text == "Master Package")
            {

                ddlMasterPackage.Items.Clear();
                ddlMasterPackage.Enabled = false;
                reqfddlMasterPackage.Enabled = false;
            }

        }
        catch (Exception sqe)
        {

        }
    }
    protected void GridNights_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            DropDownList ddlcity = (DropDownList)e.Row.Cells[2].FindControl("ddlcity");
            blPackage._Action = "GetAllDestinations";
            dtGetReturnedData = dlPakage.BindControls(blPackage);
            if (dtGetReturnedData.Rows.Count > 0)
            {
                ddlcity.DataSource = dtGetReturnedData;
                ddlcity.DataTextField = "LocationName";
                ddlcity.DataValueField = "LocationId";
                ddlcity.DataBind();
                ddlcity.Items.Insert(0, "-Select City-");
            }
            else
            {
                ddlcity.Items.Clear();
                ddlcity.Items.Insert(0, "-No City-");
            }


           

        }
        else
        {

        }
    }
    #endregion

}