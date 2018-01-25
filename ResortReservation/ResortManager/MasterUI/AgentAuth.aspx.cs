using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

using FarHorizon.Reservations.Common;
using FarHorizon.Reservations.BusinessServices;
using FarHorizon.Reservations.MasterServices;
using FarHorizon.Reservations.Common.DataEntities.Masters;

using FarHorizon.Reservations.Bases;
using FarHorizon.Reservations.Bases.BasePages;
using FarHorizon.Reservations.BusinessServices.Online.BAL;
using FarHorizon.Reservations.BusinessServices.Online.DAL;
using FarHorizon.DataSecurity;
using System.Text.RegularExpressions;
using System.Text;


public partial class MasterUI_AgentAuth : MasterBasePage
{
    DALBooking dlsr = new DALBooking();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            FillAgents();
            BindGrid();
        }
        lblError.Text = "";
       
    }

    private void FillAgents()
    {
        try
        {
            AgentMaster oAgentMaster = new AgentMaster();
            AgentDTO[] oAgentData = oAgentMaster.GetData();

            ListItemCollection li = new ListItemCollection();
            ListItem l = new ListItem("Choose Agent", "0");
            ddlAgent.Items.Insert(0, l);
            if (oAgentData != null)
            {
                for (int i = 0; i < oAgentData.Length; i++)
                {
                    l = new ListItem(oAgentData[i].AgentName.ToString(), oAgentData[i].AgentId.ToString());
                    ddlAgent.Items.Insert(i + 1, l);
                }
            }
        }
        catch { }
    }

    
    private void BindGrid()
    {

       
        DataTable dt = dlsr.GetTokenDetails();  




        gvTokenlist.DataSource = dt;
        gvTokenlist.DataBind();
    }

    protected void gvTokenlist_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvTokenlist.PageIndex = e.NewPageIndex;
        BindGrid();
       

    }
    protected void gvTokenlist_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            TextBox txtTokenNo = (TextBox)e.Row.FindControl("txtTokenNo");
            txtTokenNo.Width = 300;
            TextBox txtAgentName = (TextBox)e.Row.FindControl("txtAgentName");
            txtAgentName.Width = 300;
        }
    }

    protected void btnGenarateToken_Click(object sender, EventArgs e)
    {
        lblError.Text = "";
        AgentMaster oAgentMaster = new AgentMaster();
        AgentDTO[] oAgentData = oAgentMaster.GetData(Convert.ToInt32(ddlAgent.SelectedValue));
        string AgentCode = oAgentData[0].AgentCode.ToString();
        string AgentPassword = oAgentData[0].Password.ToString();
        if (AgentCode != "" && AgentPassword != "")
        {
            
            string Token = Convert.ToBase64String(ASCIIEncoding.ASCII.GetBytes(AgentCode + ":" + AgentPassword));

            // byte[] byt = Encoding.ASCII.GetBytes(Token);
            // byte[] data1 = Convert.FromBase64String(Token);

            //string value = Encoding.ASCII.GetString(byt);

            AgentDTO oAgentSaveData = new AgentDTO();
            oAgentSaveData.AgentId = Convert.ToInt32(ddlAgent.SelectedValue);
            oAgentSaveData.TokenNo = Token;
            bool Agent = oAgentMaster.ApiAuthInsert(oAgentSaveData);
            if (Agent)
            {
                
                lblError.Text = "Token Genarate Successfully";
            }
            else
            {
               
                lblError.Text = "This Agent Is Already Genarate Token";
            }
        }
        else
        {
           
            lblError.Text = "This Agent Can't Genarate Token,Password or AgentCode Missing";
        }
        BindGrid();
    }
}