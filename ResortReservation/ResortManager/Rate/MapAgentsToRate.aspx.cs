using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI.WebControls;
using FarHorizon.Reservations.Bases.BasePages;
using FarHorizon.Reservations.BusinessServices.Online.BAL;
using FarHorizon.Reservations.BusinessServices.Online.DAL;

public partial class Rate_MapAgentsToRate : MasterBasePage
{
    BALLinks blLinks = new BALLinks();
    DALLinks dlLinks = new DALLinks();
    DataTable dtGetReturnedData;
    int agentid = 0;
    int getQueryResponse = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            agentid = Convert.ToInt32(Request.QueryString["AgentId"]);
          //  this.BindAgentDD();
            this.BindAllCategories();
       
            
        }
    }
    #region User Define Controls
    private void BindAllCategories()
    {
        #region BindCategotyGrid
        agentid = Convert.ToInt32(Request.QueryString["AgentId"]);
        blLinks._Action = "GetAllRateCategoryAgent";
        blLinks._Agent = agentid.ToString();
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
 

    protected void MappCategory(object sender, EventArgs e)
    {
        try
        {
            agentid = Convert.ToInt32(Request.QueryString["AgentId"]);
            CheckBox cbStatus = (CheckBox)sender;
            GridViewRow row = (GridViewRow)cbStatus.Parent.Parent;
            HiddenField hdnfCategoryId = (HiddenField)Gridcategories.Rows[row.RowIndex].Cells[0].FindControl("hdnfCategoryId");
            blLinks._Agent = agentid.ToString();
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