using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;


public partial class Rate_MapRateCategoryToMarket : System.Web.UI.Page
{
    BALLinks blLinks = new BALLinks();
    DALLinks dlLinks = new DALLinks();
    DataTable dtGetReturnedData;
    int getQueryResponse = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            this.BindMarketDD();
            this.BindAllCategories();
        }
    }
    #region User Define Controls
    private void BindAllCategories()
    {
        #region BindCategotyGrid
        blLinks._Action = "GetAllRateCategory";
        blLinks._MarketId = ddlMarket.SelectedItem.Value;
        dtGetReturnedData = dlLinks.BindControls(blLinks);
        if (dtGetReturnedData != null)
        {
            Gridcategories.DataSource = dtGetReturnedData;
            Gridcategories.DataBind();

        }
        else
        {
            Gridcategories.DataSource = null;
            Gridcategories.DataBind();

        }
        #endregion


    }
    private void BindMarketDD()
    {
        #region Bind Market DD
        blLinks._Action = "GetAllGetAllMarkets";
        dtGetReturnedData = dlLinks.BindControls(blLinks);
        if (dtGetReturnedData != null)
        {
            ddlMarket.Items.Clear();
            ddlMarket.DataSource = dtGetReturnedData;
            ddlMarket.DataTextField = "Name";
            ddlMarket.DataValueField = "MarketCode";
            ddlMarket.DataBind();

        }
        else
        {
            ddlMarket.Items.Clear();
            ddlMarket.DataSource = null;
            ddlMarket.DataBind();

        }
        #endregion
    }

    protected void MappCategory(object sender, EventArgs e)
    {
        try
        {
            CheckBox cbStatus = (CheckBox)sender;
            GridViewRow row = (GridViewRow)cbStatus.Parent.Parent;
            HiddenField hdnfCategoryId = (HiddenField)Gridcategories.Rows[row.RowIndex].Cells[0].FindControl("hdnfCategoryId");
            blLinks._MarketId = ddlMarket.SelectedItem.Value;
            blLinks._CateId = hdnfCategoryId.Value;
            if (cbStatus.Checked == true)
            {
                blLinks._Action = "AddRelation";
                getQueryResponse = dlLinks.InsertMappedRelation(blLinks);
                this.BindAllCategories();
            }
            else
            {
                blLinks._Action = "RemoveRelation";
                getQueryResponse = dlLinks.RemoveMappedRelation(blLinks);
                this.BindAllCategories();
            }
        }
        catch (Exception sqe)
        {
        }
    }
    #endregion

    #region Control Events
    protected void Gridcategories_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label lbStatus = (Label)e.Row.FindControl("lbStatus");
            CheckBox cbStatus = (CheckBox)e.Row.FindControl("cbStatus");
            HiddenField hdnfStatus = (HiddenField)e.Row.FindControl("hdnfStatus");
            if (hdnfStatus != null && hdnfStatus.Value == "True")
            {
                cbStatus.Checked = true;
                lbStatus.Text = "Mapped";
                lbStatus.ForeColor = System.Drawing.Color.Green;
            }
            else
            {
                cbStatus.Checked = false;
                lbStatus.Text = "Not Mapped";
                lbStatus.ForeColor = System.Drawing.Color.Gray;
            }
        }

    }
    protected void ddlMarket_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindAllCategories();
    }
    #endregion

}