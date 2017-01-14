using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using FarHorizon.Reservations.BusinessServices.Online.BAL;
using FarHorizon.Reservations.BusinessServices.Online.DAL;

public partial class Cruise_booking_AgentPayment : System.Web.UI.Page
{
    BALAgentPayment blAgentpayment = new BALAgentPayment();
    DALAgentPayment dlAgentpayment = new DALAgentPayment();
    int GetQueryResponse=0;
    DataTable dtGetReturenedData;

    BALLinks blLinks = new BALLinks();
    DALLinks dlLinks = new DALLinks();
    DataTable dtGetReturnedData;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            this.BindAgentDD();
        }
    }
    protected void btnRefresh_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.RawUrl);
    }
    protected void btnRegister_Click(object sender, EventArgs e)
    {
        try
        {
            dtGetReturenedData = new DataTable();
            dtGetReturenedData = dlAgentpayment.getagentmasterinfo(Convert.ToInt32(ddlAgent.SelectedValue));
            if (dtGetReturenedData != null)
            {

                if (dtGetReturenedData.Rows[0]["Password"] != null && dtGetReturenedData.Rows[0]["Password"].ToString().Length>0)
                {
                    blAgentpayment._Action = "AddDetails";
                    blAgentpayment._FirstName = dtGetReturenedData.Rows[0]["AgentName"].ToString();
                    blAgentpayment._LastName = "";
                    blAgentpayment._PaymentMethod = ddlpaymentMethod.SelectedItem.Text;
                    blAgentpayment._EmailId = dtGetReturenedData.Rows[0]["AgentEmailId"].ToString();
                    blAgentpayment._AgentCode =Convert.ToInt32( dtGetReturenedData.Rows[0]["AgentId"].ToString()) ;
                    blAgentpayment._Password = dtGetReturenedData.Rows[0]["Password"].ToString();
                    

                    blAgentpayment._BillingAddress = txtBillingAddress.Text.Trim().ToString();
                    blAgentpayment.OnCredit = chkOnCredit.Checked;
                    blAgentpayment.CreditLimit = Convert.ToDecimal(txtCreditLimit.Text.Trim() == "" ? "0" : txtCreditLimit.Text.Trim());


                    GetQueryResponse = dlAgentpayment.AddpaymentDetails(blAgentpayment);

                    if (GetQueryResponse > 0)
                    {
                        ClearAllControls();
                        lbStatus.Text = "Payment details saved successfully.";
                        lbStatus.ForeColor = System.Drawing.Color.Green;

                    }
                    else
                    {
                        lbStatus.Text = "Not Saved.. Please check ur entries onces";
                        lbStatus.ForeColor = System.Drawing.Color.Red;
                    }
                }
                else
                {
                    lbStatus.Text = "First Create a Password for the Agent on the AgentMaster page";
                    lbStatus.ForeColor = System.Drawing.Color.Red;
                }
            }

        }
        catch(Exception sqe)
        {
            lbStatus.Text = "Unexpected error found . please check ur entries.";
            lbStatus.ForeColor = System.Drawing.Color.Red;
        }

    }


    private void ClearAllControls()
    {
        try
        {
            txtBillingAddress.Text = string.Empty;
         //   txtConfirmPassword.Text = string.Empty;
            ddlAgent.SelectedIndex = 0;
         //   txtMailId.Text = string.Empty;
          //  txtpassword.Text = string.Empty;

        }
        catch
        {

        }
        finally
        {
 
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

}