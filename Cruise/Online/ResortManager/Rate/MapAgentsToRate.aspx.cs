using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI.WebControls;

public partial class Rate_MapAgentsToRate : System.Web.UI.Page
{
    BALLinks blLinks = new BALLinks();
    DALLinks dlLinks = new DALLinks();
    DataTable dtGetReturnedData;
    int getQueryResponse = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            this.BindAgentDD();
            this.BindAllCategories();
        }
    }
    
    #region User Define Controls
    private void BindAllCategories()
    {
        #region BindCategotyGrid
        blLinks._Action = "GetAllRateCategoryAgent";
        blLinks._Agent = ddlAgent.SelectedItem.Value;
        dtGetReturnedData = dlLinks.BindControlsAgent(blLinks);
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
    private void BindAgentDD()
    {
        #region Bind Agent DD
        blLinks._Action = "GetAllGetAllAgents";
        dtGetReturnedData = dlLinks.BindControlsAgent(blLinks);
        if (dtGetReturnedData != null)
        {
            ddlAgent.Items.Clear();
            ddlAgent.DataSource = dtGetReturnedData;
            ddlAgent.DataTextField = "AgentName";
            ddlAgent.DataValueField = "AgentId";
            ddlAgent.DataBind();

        }
        else
        {
            ddlAgent.Items.Clear();
            ddlAgent.DataSource = null;
            ddlAgent.DataBind();

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
            blLinks._Agent = ddlAgent.SelectedItem.Value;
            blLinks._CateId = hdnfCategoryId.Value;
            if (cbStatus.Checked == true)
            {
                blLinks._Action = "AddRelationAgent";
                getQueryResponse = dlLinks.InsertMappedRelationAgent(blLinks);
                this.BindAllCategories();
            }
            else
            {
                blLinks._Action = "RemoveRelationAgent";
                getQueryResponse = dlLinks.RemoveMappedRelationAgent(blLinks);
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