using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using FarHorizon.Reservations.Bases.BasePages;
using FarHorizon.Reservations.BusinessServices.Online.BAL;
using FarHorizon.Reservations.BusinessServices.Online.DAL;

public partial class Rate_PackageRateCard : MasterBasePage
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
            //  this.BindLocation();
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
    //private void BindLocation()
    //{
    //    try
    //    {
    //        blRate._Action = "GetAllLocation";
    //        dtGetReturnedData = dlRate.BindControls(blRate);
    //        if (dtGetReturnedData.Rows.Count > 0)
    //        {
    //            ddlLocation.Items.Clear();
    //            ddlLocation.DataSource = dtGetReturnedData;
    //            ddlLocation.DataTextField = "LocationName";
    //            ddlLocation.DataValueField = "LocationId";
    //            ddlLocation.DataBind();
    //            ddlLocation.Items.Insert(0, "-Select-");
    //        }
    //        else
    //        {
    //            ddlLocation.Items.Clear();
    //            ddlLocation.Items.Insert(0, "-No Location-");
    //        }
    //    }
    //    catch
    //    {
    //    }
    //}
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
                blRate._Action = "GetRateCardsPackage";
                dtGetReturnedData = new DataTable();
                dtGetReturnedData = dlRate.BindControls(blRate);
                if (dtGetReturnedData.Rows.Count > 0)
                {

                    gdvRateCards.DataSource = dtGetReturnedData;
                    gdvRateCards.DataBind();
                    ViewState["RateCard"] = dtGetReturnedData;
                }
                else
                {
                    gdvRateCards.DataSource = null;
                    gdvRateCards.DataBind();

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
                HiddenField frompaxold = (HiddenField)GridRateSheet.Rows[LoopCounter].Cells[11].FindControl("hdnfrompaxold");
                HiddenField TopaxOld = (HiddenField)GridRateSheet.Rows[LoopCounter].Cells[11].FindControl("hdntopaxold");

                blRate._fromPaxup = Convert.ToInt32(txtFromPax.Text.Trim());
                blRate._ToPaxup = Convert.ToInt32(txtToPax.Text.Trim());
                blRate._valFrom = Convert.ToDateTime(txtValFrom.Text.Trim().ToString());
                blRate._ValTo = Convert.ToDateTime(txtValto.Text.Trim().ToString());

                blRate._RoomCategoryId = Convert.ToInt32(ddlRoomCat.SelectedItem.Value.ToString());

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
                    blRate._fromPax = Convert.ToInt32(frompaxold.Value);
                    blRate._ToPax = Convert.ToInt32(TopaxOld.Value);
                    blRate._Action = "UpdateRCardDetails";



                    blRate._RoomCategoryIdup = Convert.ToInt32(hdnRoomCtid.Value);

                    blRate._RateCardId = hfId.Value;
                    getQueryResponse = dlRate.UpdateRoomRate(blRate);
                }
                if (getQueryResponse > 0)
                {

                }

                if (LoopCounter == GridRateSheet.Rows.Count - 1)
                    Page.ClientScript.RegisterStartupScript(GetType(), "msgbox", "alert('Package rate card updated successfully.')", true);

            }
        }
        catch (Exception sqe)
        {

        }
    }
    private void clearcontrols()
    {

        // ddlLocation.SelectedIndex = -1;
        ddlpackage.SelectedIndex = -1;
        ddlRatecategory.SelectedIndex = -1;
        ddlCurrency.SelectedIndex = 0;
        txtSupplier.Text = string.Empty;
        txtTaxPercent.Text ="0";
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
            blRate._LocationId = 0;
            blRate._valFrom = Convert.ToDateTime(txtValFrom.Text.Trim().ToString());
            blRate._ValTo = Convert.ToDateTime(txtValto.Text.Trim().ToString());
            blRate._Tax = Convert.ToDecimal(txtTaxPercent.Text.Trim().ToString());
            blRate._Currency = ddlCurrency.SelectedItem.Text;
            blRate._RateCategory = ddlRatecategory.SelectedItem.Value.ToString();
            blRate._SupplierName = txtSupplier.Text.Trim().ToString();
            blRate.FrompaxMain = Convert.ToInt32(txtFrmPax.Text.Trim());
            blRate.ToPaxMain = Convert.ToInt32(txtToPax.Text.Trim());

            if (btnSbmit.Text == "Submit")
            {
                blRate._RateCardId = RateId.ToString();
                blRate._Action = "AddNewPackageRate";
                getQueryResponse = dlRate.AddNewPackageRateCard(blRate);
            }
            else
            {
                blRate._RateCardId = hfId.Value.ToString();
                blRate._Action = "UpdateRateCard";
                getQueryResponse = dlRate.updatenewPackageRateCard(blRate);
            }
            if (getQueryResponse > 0)
            {
                AddRoomRates(RateId);
                if (btnSbmit.Text == "Submit")
                {
                    lblStatus.Text = "Package Rate card created successfully.";
                    lblStatus.ForeColor = System.Drawing.Color.Green;
                }
                else
                {
                    lblStatus.Text = "Updated successfully.";
                    lblStatus.ForeColor = System.Drawing.Color.Green;
                    btnSbmit.Text = "Submit";
                    hfId.Value = null;
                    clearcontrols();
                }
            }
            else
            {
                lblStatus.Text = "please check your entries .";
                lblStatus.ForeColor = System.Drawing.Color.Red;
                Page.ClientScript.RegisterStartupScript(GetType(), "msgbox", "alert('Package rate card could not be Created ')", true);
            }
        }
        catch (Exception sqe)
        {
            Page.ClientScript.RegisterStartupScript(GetType(), "msgbox", "alert('Package rate card could not be Created ')", true);
            lblStatus.Text = "please check your entries carefully.";
            lblStatus.ForeColor = System.Drawing.Color.Red;
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
            blRate._Action = "GetcrRoomCategories";
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
    protected void ddlRateCardId_SelectedIndexChanged(object sender, EventArgs e)
    {
        //FillPackageMaster();
        //fillPackageDetails();
        //btnSbmit.Text = "Update";
        //lblStatus.Text = string.Empty;
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
                //   ddlLocation.SelectedValue = dtGetReturnedData.Rows[0]["LocationId"].ToString();
                txtValFrom.Text = dtGetReturnedData.Rows[0]["VaildFrom"].ToString();
                txtValto.Text = dtGetReturnedData.Rows[0]["ValidTo"].ToString();
                txtTaxPercent.Text = dtGetReturnedData.Rows[0]["tax"].ToString();
                ddlCurrency.SelectedValue = dtGetReturnedData.Rows[0]["Currency"].ToString();



            }

        }

        catch
        {
        }
    }
    public void fillPackageDetails(string rcardid)
    {
        try
        {

            blRate._Action = "getRateCardDetails";
            blRate._RateCardId = rcardid;
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
                        HiddenField frompaxold = (HiddenField)Grow.FindControl("hdnfrompaxold");
                        HiddenField TopaxOld = (HiddenField)Grow.FindControl("hdntopaxold");


                        txtValfrm.Text = Convert.ToDateTime(dtGetReturnedData.Rows[i]["ValFrom"]).ToString("MM/dd/yyyy");
                        txtValTo.Text = Convert.ToDateTime(dtGetReturnedData.Rows[i]["Valto"]).ToString("MM/dd/yyyy");
                        frompaxold.Value = txtPaxFrm.Text = dtGetReturnedData.Rows[i]["FromPax"].ToString();
                        TopaxOld.Value = txtPaxTo.Text = dtGetReturnedData.Rows[i]["ToPax"].ToString();
                        txtbcpp.Text = Convert.ToDouble(dtGetReturnedData.Rows[i]["ppBc"]).ToString("#.##");
                        txtbcsrs.Text = Convert.ToDouble(dtGetReturnedData.Rows[i]["SRSBc"]).ToString("#.##");
                        txtBcTx.Text = Convert.ToDouble(dtGetReturnedData.Rows[i]["Taxvalue"]).ToString("#.##");
                        txtncpp.Text = Convert.ToDouble(dtGetReturnedData.Rows[i]["PPNc"]).ToString("#.##");
                        txtncsrs.Text = Convert.ToDouble(dtGetReturnedData.Rows[i]["SRSNc"]).ToString("#.##");
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
    protected void btncancel_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.RawUrl);
    }
    #endregion

    protected void gdv_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            if (e.CommandName == "Select")
            {
                LinkButton lnk = (LinkButton)e.CommandSource;
                GridViewRow grow = (GridViewRow)lnk.NamingContainer;
                string rcardid = grow.Cells[0].Text;
                hfId.Value = rcardid;
                blRate._RateCardId = rcardid;
                blRate._Action = "GetRateCardsbyId";
                dtGetReturnedData = new DataTable();
                dtGetReturnedData = dlRate.getRateCardbyId(blRate);
                if (dtGetReturnedData != null)
                {
                    //   ddlLocation.SelectedValue = dtGetReturnedData.Rows[0]["LocationId"].ToString();
                    ddlpackage.SelectedValue = dtGetReturnedData.Rows[0]["PackageId"].ToString();
                    ddlRatecategory.SelectedValue = dtGetReturnedData.Rows[0]["RateCategory"].ToString();
                    ddlCurrency.SelectedValue = dtGetReturnedData.Rows[0]["Currency"].ToString();
                    txtSupplier.Text = dtGetReturnedData.Rows[0]["SupplierName"].ToString();
                    txtTaxPercent.Text = dtGetReturnedData.Rows[0]["tax"].ToString();
                    txtValFrom.Text = Convert.ToDateTime(dtGetReturnedData.Rows[0]["VaildFrom"]).ToString("MM/dd/yyyy");
                    txtValto.Text = Convert.ToDateTime(dtGetReturnedData.Rows[0]["ValidTo"]).ToString("MM/dd/yyyy");



                    fillPackageDetails(rcardid);
                    btnSbmit.Text = "Update";
                }



            }
        }

        catch
        {
        }
    }
    protected void gdv_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {

            string rcardid = gdvRateCards.DataKeys[e.RowIndex].Value.ToString();
            blRate._RateCardId = rcardid;
            blRate._Action = "DeleteRateCard";
            int res = dlRate.DeletePackageRateCard(blRate);
            if (res > 0)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Showstatus", "javascript:alert('Package rate card  Deleted ')", true);

                BindRateCardIdddl();


            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Showstatus", "javascript:alert('Package rate card could not be Deleted ')", true);


            }

        }
        catch
        {
        }





    }




    protected void txtBcPP_TextChanged(object sender, EventArgs e)
    {

        try
        {
            TextBox txtbcp = (TextBox)sender;
            GridViewRow grow = (GridViewRow)txtbcp.NamingContainer;
            TextBox txtNcPP = (TextBox)grow.FindControl("txtNcPP");
            TextBox txtNcSrs = (TextBox)grow.FindControl("txtNcSrs");
            TextBox taxPer = (TextBox)grow.FindControl("txtBcTaxValue");
            CheckBox chk = (CheckBox)grow.FindControl("chktaxInc");



            taxPer.Text = txtTaxPercent.Text.Trim();
           

                txtNcPP.Text = Math.Round((Convert.ToDouble(txtbcp.Text) + (Convert.ToDouble(txtbcp.Text) / 100) * Convert.ToDouble(txtTaxPercent.Text))).ToString();
           






        }

        catch
        {
        }
    }
    protected void txtBcSrs_TextChanged(object sender, EventArgs e)
    {

        try
        {
            TextBox txtBcSrs = (TextBox)sender;
            GridViewRow grow = (GridViewRow)txtBcSrs.NamingContainer;
            TextBox txtNcPP = (TextBox)grow.FindControl("txtNcPP");
            TextBox txtNcSrs = (TextBox)grow.FindControl("txtNcSrs");
            TextBox txtBcTaxValue = (TextBox)grow.FindControl("txtBcTaxValue");
            CheckBox chk = (CheckBox)grow.FindControl("chktaxInc");
           
                txtNcSrs.Text = Math.Round((Convert.ToDouble(txtBcSrs.Text) + (Convert.ToDouble(txtBcSrs.Text) / 100) * Convert.ToDouble(txtTaxPercent.Text))).ToString();
           
        }
        catch
        {
        }

    }
    protected void chktaxInc_CheckedChanged(object sender, EventArgs e)
    {
        try
        {
            CheckBox chk = (CheckBox)sender;
            GridViewRow grow = (GridViewRow)chk.NamingContainer;
            TextBox txtNcPP = (TextBox)grow.FindControl("txtNcPP");
            TextBox txtNcSrs = (TextBox)grow.FindControl("txtNcSrs");
            TextBox txtBcTaxValue = (TextBox)grow.FindControl("txtBcTaxValue");




            TextBox txtbcp = (TextBox)grow.FindControl("txtBcPP");
            TextBox txtbcsrs = (TextBox)grow.FindControl("txtBcSrs");

            if (chk.Checked)
            {
                txtNcSrs.Text = txtbcsrs.Text;
                txtNcPP.Text = txtbcp.Text;

               
            }
            else
            {
                txtNcSrs.Text = Math.Round((Convert.ToDouble(txtbcsrs.Text) + (Convert.ToDouble(txtbcsrs.Text) / 100) * Convert.ToDouble(txtTaxPercent.Text))).ToString();
                txtNcPP.Text = Math.Round((Convert.ToDouble(txtbcp.Text) + (Convert.ToDouble(txtbcp.Text) / 100) * Convert.ToDouble(txtTaxPercent.Text))).ToString();

            }
        }

        catch
        {

        }


    }
    protected void txtValto_TextChanged(object sender, EventArgs e)
    {
        try
        {
            dtGetReturnedData = ViewState["RateCard"] as DataTable;
            DataView dv = new DataView(dtGetReturnedData);

            if (txtValFrom.Text != "" && txtValto.Text != "")
            {
                if (Convert.ToInt32(txtValto.Text) >= Convert.ToInt32(txtValFrom.Text))
                {
                    dv.RowFilter = "#" + Convert.ToDateTime(txtValFrom.Text.Trim()) + "#<=VaildFrom   and #" + Convert.ToDateTime(txtValFrom.Text.Trim()) + "#>= ValidTo";
                }

            }


            if (dv.ToTable().Rows.Count > 0)
            {

                gdvRateCards.DataSource = dv;
                gdvRateCards.DataBind();
            }
            else
            {
                gdvRateCards.DataSource = null;
                gdvRateCards.DataBind();
            }
        }
        catch
        {
        }
    }
  
}