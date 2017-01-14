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

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["Usercode"] != null)
        {
            
        }
        else
        {
            Response.Redirect("agentLogin.aspx");
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

            blAgentpayment._Action = "AddDetails";
            blAgentpayment._FirstName = txtFirstName.Text.Trim().ToString();
            blAgentpayment._LastName = txtLastName.Text.Trim().ToString();
            blAgentpayment._PaymentMethod = ddlpaymentMethod.SelectedItem.Text;
            blAgentpayment._EmailId = txtMailId.Text.Trim().ToString();
            blAgentpayment._AgentCode = Convert.ToInt32(Session["UserCode"].ToString());
            blAgentpayment._Password = txtConfirmPassword.Text.Trim().ToString();
            blAgentpayment._BillingAddress = txtBillingAddress.Text.Trim().ToString();
            blAgentpayment.OnCredit = chkOnCredit.Checked;
            blAgentpayment.CreditLimit = Convert.ToDecimal(txtCreditLimit.Text.Trim());


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
            txtConfirmPassword.Text = string.Empty;
            txtFirstName.Text = string.Empty;
            txtLastName.Text = string.Empty;
            txtMailId.Text = string.Empty;
            txtpassword.Text = string.Empty;

        }
        catch
        {

        }
        finally
        {
 
        }

    }
}