using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using FarHorizon.Reservations.BusinessServices.Online.BAL;
using FarHorizon.Reservations.BusinessServices.Online.DAL;
using System.Data;
using FarHorizon.Reservations.Bases.BasePages;

public partial class MasterUI_AgentContractingmaster : MasterBasePage
{
    BALAgentPayment blAgentpayment = new BALAgentPayment();
    DALAgentPayment dlAgentpayment = new DALAgentPayment();
    int GetQueryResponse = 0;
    DataTable dtGetReturenedData;

    BALLinks blLinks = new BALLinks();
    DALLinks dlLinks = new DALLinks();
    DataTable dtGetReturnedData;
    protected void Page_Load(object sender, EventArgs e)
    {
       
        if (!IsPostBack)
        {
            BindAgentDD();
        }
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
            ddlAgent.Items.Insert(0, new ListItem("-Select-", "0"));

        }
        else
        {
            ddlAgent.Items.Clear();
            ddlAgent.DataSource = null;
            ddlAgent.DataBind();
            ddlAgent.Items.Insert(0, new ListItem("-Select-", "0"));

        }
        #endregion
    }
    private void load(string agentid)
    {
        DataTable dt = dlAgentpayment.fetchbyagentid(agentid);
        if (dt != null && dt.Rows.Count > 0)
        {
            txtBookingpaymentPolicy.Text = dt.Rows[0]["booking"].ToString();
            txtCancel.Text = dt.Rows[0]["Cancellation"].ToString();
        }
        else
        {
            txtBookingpaymentPolicy.Text = "";
            txtCancel.Text = "";
        }
    }
    protected void btnsave_Click(object sender, EventArgs e)
    {
        if (ddlAgent.SelectedIndex > 0)
        {
            int n = dlAgentpayment.Saveagentcontracting(ddlAgent.SelectedValue.ToString(), txtCancel.Text, txtBookingpaymentPolicy.Text);
            if (n == 1)
            {
                load(ddlAgent.SelectedValue.ToString());
            }
        }
    }

    protected void ddlAgent_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlAgent.SelectedIndex > 0)
        {
            load(ddlAgent.SelectedValue.ToString());
        }

    }
}