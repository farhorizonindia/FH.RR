using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;



public partial class Rate_PackageRateCard : System.Web.UI.Page
{
    BALPackageRateCard blRate = new BALPackageRateCard();
    DALPackageRateCard dlRate = new DALPackageRateCard();
    DataTable dtGetReturnedData;
    int getQueryResponse = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            this.BindPackages();
            this.BindRateCategory();
            this.BindLocation();
            this.bindGrid();
            this.BindRateCardIdddl();
        }
    }

    #region UDF
    private void BindPackages()
    {
        try
        {
            blRate._Action = "GetAllPackages";
            dtGetReturnedData = dlRate.BindControls(blRate);
            if (dtGetReturnedData.Rows.Count > 0)
            {
                ddlpackage.Items.Clear();
                ddlpackage.DataSource = dtGetReturnedData;
                ddlpackage.DataTextField = "PackageName";
                ddlpackage.DataValueField = "PackageId";
                ddlpackage.DataBind();
                ddlpackage.Items.Insert(0, "-Select-");
            }
            else
            {
                ddlpackage.Items.Clear();
                ddlpackage.Items.Insert(0, "-No packages-");
            }
        }
        catch
        {
        }
    }
    private void BindRateCategory()
    {
        try
        {
            blRate._Action = "GetAllRateCategories";
            dtGetReturnedData = dlRate.BindControls(blRate);
            if (dtGetReturnedData.Rows.Count > 0)
            {
                ddlRatecategory.Items.Clear();
                ddlRatecategory.DataSource = dtGetReturnedData;
                ddlRatecategory.DataTextField = "RateName";
                ddlRatecategory.DataValueField = "RateId";
                ddlRatecategory.DataBind();
                ddlRatecategory.Items.Insert(0, "-Select-");
            }
            else
            {
                ddlRatecategory.Items.Clear();
                ddlRatecategory.Items.Insert(0, "-No ratecategory-");
            }
        }
        catch
        {
        }
    }
    private void BindLocation()
    {
        try
        {
            blRate._Action = "GetAllLocation";
            dtGetReturnedData = dlRate.BindControls(blRate);
            if (dtGetReturnedData.Rows.Count > 0)
            {
                ddlLocation.Items.Clear();
                ddlLocation.DataSource = dtGetReturnedData;
                ddlLocation.DataTextField = "LocationName";
                ddlLocation.DataValueField = "LocationId";
                ddlLocation.DataBind();
                ddlLocation.Items.Insert(0, "-Select-");
            }
            else
            {
                ddlLocation.Items.Clear();
                ddlLocation.Items.Insert(0, "-No Location-");
            }
        }
        catch
        {
        }
    }
    private void bindGrid()
    {
        try
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("Sn");

            for (int a = 0; a < 3; a++)
            {
                DataRow dr = dt.NewRow();
                dr["Sn"] = (a + 1).ToString();
                dt.Rows.Add(dr);
            }

            GridRateSheet.DataSource = dt;
            GridRateSheet.DataBind();

        }
        catch
        {

        }
    }
    private void BindRateCardIdddl()
    {
        try
        {

            try
            {
                blRate._Action = "GetRatecardIds";
                dtGetReturnedData = new DataTable();
                dtGetReturnedData = dlRate.BindControls(blRate);
                if (dtGetReturnedData.Rows.Count > 0)
                {
                    ddlRateCardId.Items.Clear();
                    ddlRateCardId.DataSource = dtGetReturnedData;
                    ddlRateCardId.DataTextField = "RateCardId";
                    ddlRateCardId.DataValueField = "RateCardId";
                    ddlRateCardId.DataBind();
                    ddlRateCardId.Items.Insert(0, "-Select-");
                }
                else
                {
                    ddlRateCardId.Items.Clear();
                    ddlRateCardId.Items.Insert(0, "-No Rate Cards-");
                }
            }
            catch
            {
            }

        }
        catch
        {

        }
    }

    private void AddRoomRates(string RateCardId)
    {
        try
        {
            for (int LoopCounter = 0; LoopCounter < GridRateSheet.Rows.Count; LoopCounter++)
            {
                TextBox txtvalfrom = (TextBox)GridRateSheet.Rows[LoopCounter].Cells[1].FindControl("txtvalfrom");
                TextBox txtValTo = (TextBox)GridRateSheet.Rows[LoopCounter].Cells[2].FindControl("txtValTo");
                DropDownList ddlRoomCat = (DropDownList)GridRateSheet.Rows[LoopCounter].Cells[3].FindControl("ddlRoomCat");
                TextBox txtFromPax = (TextBox)GridRateSheet.Rows[LoopCounter].Cells[4].FindControl("txtFromPax");
                TextBox txtToPax = (TextBox)GridRateSheet.Rows[LoopCounter].Cells[5].FindControl("txtToPax");
                TextBox txtBcPP = (TextBox)GridRateSheet.Rows[LoopCounter].Cells[6].FindControl("txtBcPP");
                TextBox txtBcSrs = (TextBox)GridRateSheet.Rows[LoopCounter].Cells[7].FindControl("txtBcSrs");
                CheckBox chktaxInc = (CheckBox)GridRateSheet.Rows[LoopCounter].Cells[8].FindControl("chktaxInc");
                TextBox txtBcTaxValue = (TextBox)GridRateSheet.Rows[LoopCounter].Cells[9].FindControl("txtBcTaxValue");
                TextBox txtNcPP = (TextBox)GridRateSheet.Rows[LoopCounter].Cells[10].FindControl("txtNcPP");
                TextBox txtNcSrs = (TextBox)GridRateSheet.Rows[LoopCounter].Cells[11].FindControl("txtNcSrs");

                blRate._valFrom = Convert.ToDateTime(txtvalfrom.Text.Trim().ToString());
                blRate._ValTo = Convert.ToDateTime(txtValTo.Text.Trim().ToString());
                blRate._RoomCategoryId = Convert.ToInt32(ddlRoomCat.SelectedItem.Value.ToString());
                blRate._fromPax = Convert.ToInt32(txtFromPax.Text.Trim().ToString());
                blRate._ToPax = Convert.ToInt32(txtToPax.Text.Trim().ToString());
                blRate._ppBc = Convert.ToDecimal(txtBcPP.Text.Trim().ToString());
                blRate._SRSBc = Convert.ToDecimal(txtBcSrs.Text.Trim().ToString());
                if (chktaxInc.Checked == true)
                    blRate._taxEx = true;
                else
                    blRate._taxEx = false;

                blRate._taxValue = Convert.ToDecimal(txtBcTaxValue.Text.Trim().ToString());

                blRate._PPNc = Convert.ToDecimal(txtNcPP.Text.Trim().ToString());
                blRate._SRSNc = Convert.ToDecimal(txtNcSrs.Text.Trim().ToString());

                if (btnSbmit.Text == "Submit")
                {
                    blRate._Action = "AddPackageRateDetails";
                    getQueryResponse = dlRate.AddRoomRate(blRate);
                }
                else
                {
                    blRate._Action = "UpdateRCardDetails";
                    blRate._fromPaxup = Convert.ToInt32(hdnFrmPax.Value);
                    blRate._ToPaxup = Convert.ToInt32(hdnToPax.Value);
                    blRate._RoomCategoryIdup=Convert.ToInt32(hdnRoomCtid.Value);
                    blRate._RateCardId = ddlRateCardId.SelectedValue;
                    getQueryResponse = dlRate.UpdateRoomRate(blRate);
                }

                if (getQueryResponse > 0)
                {

                }
            }
        }
        catch (Exception sqe)
        {

        }
    }
    private void clearcontrols()
    {
        txtcarruency.Text = string.Empty;
        ddlLocation.SelectedIndex = -1;
        ddlpackage.SelectedIndex = -1;
        ddlRatecategory.SelectedIndex = -1;
        txtcarruency.Text = string.Empty;
        txtSupplier.Text = string.Empty;
        txtTaxPercent.Text = string.Empty;
        txtValFrom.Text = string.Empty;
        txtValto.Text = string.Empty;
        this.bindGrid();

    }


    #endregion

    #region Control Events
    protected void btnSbmit_Click(object sender, EventArgs e)
    {
        try
        {



            string RateId = "R" + DateTime.Now.ToString("MMddhhmmss");

            blRate._Date = System.DateTime.Now;
            blRate._packageId = ddlpackage.SelectedItem.Value.ToString();
            blRate._LocationId = Convert.ToInt32(ddlLocation.SelectedItem.Value);
            blRate._valFrom = Convert.ToDateTime(txtValFrom.Text.Trim().ToString());
            blRate._ValTo = Convert.ToDateTime(txtValto.Text.Trim().ToString());
            blRate._Tax = Convert.ToDecimal(txtTaxPercent.Text.Trim().ToString());
            blRate._Currency = txtcarruency.Text.Trim().ToString();
            blRate._RateCategory = ddlRatecategory.SelectedItem.Value.ToString();
            blRate._SupplierName = txtSupplier.Text.Trim().ToString();
            if (btnSbmit.Text == "Submit")
            {
                blRate._RateCardId = RateId.ToString();
                blRate._Action = "AddNewPackageRate";
                getQueryResponse = dlRate.AddNewPackageRateCard(blRate);
            }
            else
            {
                blRate._RateCardId = ddlRateCardId.SelectedValue;
                blRate._Action = "UpdateRateCard";
                getQueryResponse = dlRate.updatenewPackageRateCard(blRate);
            }


            if (getQueryResponse > 0)
            {
                Page.ClientScript.RegisterStartupScript(GetType(), "msgbox", "alert('Package rate card Created ')", true);
                AddRoomRates(RateId);
                // clearcontrols();
            }
            else
            {
                Page.ClientScript.RegisterStartupScript(GetType(), "msgbox", "alert('Package rate card could not be Created ')", true);
            }




        }
        catch (Exception sqe)
        {
            Page.ClientScript.RegisterStartupScript(GetType(), "msgbox", "alert('Package rate card could not be Created ')", true);

        }

    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.RawUrl);
    }
    protected void GridRateSheetRowDataBound(Object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            DropDownList ddlRoomCategory = (DropDownList)e.Row.FindControl("ddlRoomCat");
            blRate._Action = "GetRoomCategories";
            dtGetReturnedData = dlRate.BindControls(blRate);
            if (dtGetReturnedData.Rows.Count > 0)
            {
                ddlRoomCategory.Items.Clear();
                ddlRoomCategory.DataSource = dtGetReturnedData;
                ddlRoomCategory.DataTextField = "RoomCategory";
                ddlRoomCategory.DataValueField = "RoomCategoryId";
                ddlRoomCategory.DataBind();
                ddlRoomCategory.Items.Insert(0, "-Select-");
            }
            else
            {
                ddlRoomCategory.Items.Clear();
                ddlRoomCategory.Items.Insert(0, "-No RoomCategory-");
            }
        }
    }
    #endregion
    protected void ddlRateCardId_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillPackageMaster();
        fillPackageDetails();
        btnSbmit.Text = "Update";
    }

    public void FillPackageMaster()
    {
        try
        {
            blRate._RateCardId = ddlRateCardId.SelectedValue;
            blRate._Action = "GetRateCardmaster";
            dtGetReturnedData = new DataTable();
            dtGetReturnedData = dlRate.getPackageData(blRate);
            if (dtGetReturnedData != null)
            {

                ddlpackage.SelectedValue = dtGetReturnedData.Rows[0]["PackageId"].ToString();
                ddlRatecategory.SelectedValue = dtGetReturnedData.Rows[0]["RateCategory"].ToString();
                txtSupplier.Text = dtGetReturnedData.Rows[0]["SupplierName"].ToString();
                ddlLocation.SelectedValue = dtGetReturnedData.Rows[0]["LocationId"].ToString();
                txtValFrom.Text = dtGetReturnedData.Rows[0]["VaildFrom"].ToString();
                txtValto.Text = dtGetReturnedData.Rows[0]["ValidTo"].ToString();
                txtTaxPercent.Text = dtGetReturnedData.Rows[0]["tax"].ToString();
                txtcarruency.Text = dtGetReturnedData.Rows[0]["Currency"].ToString();

            }

        }

        catch
        {
        }
    }

    public void fillPackageDetails()
    {
        try
        {

            blRate._Action = "getRateCardDetails";
            blRate._RateCardId = ddlRateCardId.SelectedValue;
            dtGetReturnedData = new DataTable();
            dtGetReturnedData = dlRate.getPackageData(blRate);
            if (dtGetReturnedData != null)
            {
                int i = 0;
                foreach (GridViewRow Grow in GridRateSheet.Rows)
                {
                    if (Grow.RowType == DataControlRowType.DataRow)
                    {
                        TextBox txtValfrm = (TextBox)Grow.FindControl("txtvalfrom");
                        TextBox txtValTo = (TextBox)Grow.FindControl("txtValTo");
                        DropDownList ddlRmCat = (DropDownList)Grow.FindControl("ddlRoomCat");
                        TextBox txtPaxFrm = (TextBox)Grow.FindControl("txtFromPax");
                        TextBox txtPaxTo = (TextBox)Grow.FindControl("txtToPax");
                        TextBox txtbcpp = (TextBox)Grow.FindControl("txtBcPP");
                        TextBox txtbcsrs = (TextBox)Grow.FindControl("txtBcSrs");
                        CheckBox chk = (CheckBox)Grow.FindControl("chktaxInc");

                        TextBox txtBcTx = (TextBox)Grow.FindControl("txtBcTaxValue");
                        TextBox txtncpp = (TextBox)Grow.FindControl("txtNcPP");
                        TextBox txtncsrs = (TextBox)Grow.FindControl("txtNcSrs");

                        txtValfrm.Text = Convert.ToDateTime(dtGetReturnedData.Rows[i]["ValFrom"]).ToString("MM/dd/yyyy");
                        txtValTo.Text = Convert.ToDateTime(dtGetReturnedData.Rows[i]["Valto"]).ToString("MM/dd/yyyy");
                        hdnFrmPax.Value = txtPaxFrm.Text = dtGetReturnedData.Rows[i]["FromPax"].ToString();
                        hdnToPax.Value = txtPaxTo.Text = dtGetReturnedData.Rows[i]["ToPax"].ToString();
                        txtbcpp.Text = dtGetReturnedData.Rows[i]["ppBc"].ToString();
                        txtbcsrs.Text = dtGetReturnedData.Rows[i]["SRSBc"].ToString();
                        txtBcTx.Text = dtGetReturnedData.Rows[i]["Taxvalue"].ToString();
                        txtncpp.Text = dtGetReturnedData.Rows[i]["PPNc"].ToString();
                        txtncsrs.Text = dtGetReturnedData.Rows[i]["SRSNc"].ToString();
                        chk.Checked = Convert.ToBoolean(dtGetReturnedData.Rows[i]["TaxEnclusive"].ToString());
                        hdnRoomCtid.Value = ddlRmCat.SelectedValue = dtGetReturnedData.Rows[i]["RoomCategoryId"].ToString();

                        i++;

                    }
                }
            }


        }
        catch
        {
        }
    }




















}