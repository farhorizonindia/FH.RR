using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;


public partial class Cruise_Masters_AgentInfo : System.Web.UI.Page
{
    DataTable dtGetReturnedata;

    BALAgentPayment blagent = new BALAgentPayment();
    DALAgentPayment dlagent = new DALAgentPayment();
    BALLinks blLinks = new BALLinks();
    DALLinks dlLinks = new DALLinks();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            bindAgentGrid();
            BindMarketddl();
        }
    }

    private void BindMarketddl()
    {
        #region Bind Market DD
        blLinks._Action = "GetAllGetAllMarkets";
        dtGetReturnedata = dlLinks.BindControls(blLinks);
        if (dtGetReturnedata != null)
        {
            ddlMarket.Items.Clear();
            ddlMarket.DataSource = dtGetReturnedata;
            ddlMarket.DataTextField = "Name";
            ddlMarket.DataValueField = "MarketCode";
            ddlMarket.DataBind();
            ddlMarket.Items.Insert(0, new ListItem("-All-", "0"));

        }
        else
        {
            ddlMarket.Items.Clear();
            ddlMarket.DataSource = null;
            ddlMarket.DataBind();
            ddlMarket.Items.Insert(0, new ListItem("-All-", "0"));

        }
        #endregion
    }


    public void bindAgentGrid()
    {
        try
        {
            blagent._Action = "AgentInfo";
            dtGetReturnedata = new DataTable();
            dtGetReturnedata = dlagent.AgentInfo(blagent);
            if (dtGetReturnedata != null)
            {
                dgAgents.DataSource = dtGetReturnedata;
                dgAgents.DataBind();

            }
            else
            {
                dgAgents.DataSource = null;
                dgAgents.DataBind();

            }
        }

        catch
        {

        }
    }







    protected void ddlMarket_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            blagent._Action="AgentInfo";
            if (ddlMarket.SelectedIndex > 0)
            {
                blagent.MarketId = ddlMarket.SelectedValue;
            }

            dtGetReturnedata = new DataTable();
            dtGetReturnedata = dlagent.AgentInfo(blagent);
            if (dtGetReturnedata != null)
            {
                dgAgents.DataSource = dtGetReturnedata;
                dgAgents.DataBind();

            }
            else
            {
                dgAgents.DataSource = null;
                dgAgents.DataBind();

            }
        }
        catch
        {
        }
    }
}