using FarHorizon.Reservations.Bases.BasePages;
using FarHorizon.Reservations.BusinessServices.Online.BAL;
using FarHorizon.Reservations.BusinessServices.Online.DAL;
using FarHorizon.Reservations.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Web.UI.WebControls;

public partial class Rate_MapAgentstoMarket : MasterBasePage
{
    BALLinks blLinks = new BALLinks();
    DALLinks dlLinks = new DALLinks();
    DataTable dtGetReturnedData;
    int getQueryResponse = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindMarketddl();
            BindAllagentmarket();

        }
    }

    #region UDF

    private void BindMarketddl()
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
    private void BindAllagentmarket()
    {
        #region BindCategotyGrid
        blLinks._Action = "GetAllAgentMarket";
        blLinks._MarketId = ddlMarket.SelectedItem.Value;
        List<AgentMarket> agentMarkets = dlLinks.BindControlsAgentmarket(blLinks);

        if (agentMarkets != null)
        {            
            gridAgents.DataSource = agentMarkets;
            gridAgents.DataBind();
        }
        else
        {
            gridAgents.DataSource = null;
            gridAgents.DataBind();

        }
        #endregion


    }

    #endregion

    #region Control Events

    protected void gridAgents_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gridAgents.PageIndex = e.NewPageIndex;
        BindAllagentmarket();
    }

    protected void cbStatus_CheckedChanged(object sender, EventArgs e)
    {
        try
        {
            CheckBox cbStatus = (CheckBox)sender;
            GridViewRow row = (GridViewRow)cbStatus.Parent.Parent;
            HiddenField hdnfCategoryId = (HiddenField)gridAgents.Rows[row.RowIndex].Cells[0].FindControl("hdnfAgentId");
            blLinks._MarketId = ddlMarket.SelectedItem.Value;
            blLinks._Agent = hdnfCategoryId.Value;
            if (cbStatus.Checked == true)
            {
                blLinks._Action = "AddRelationagentmarket";
                getQueryResponse = dlLinks.Insertagentmarketmapper(blLinks);
                this.BindAllagentmarket();
            }
            else
            {
                blLinks._Action = "RemoveRelationAgentmarket";
                getQueryResponse = dlLinks.RemoveAgentMarketMapper(blLinks);
                this.BindAllagentmarket();
            }
        }
        catch (Exception sqe)
        {
        }
    }
    protected void gridAgents_RowDataBound(object sender, GridViewRowEventArgs e)
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
        this.BindAllagentmarket();
    }

    #endregion

}