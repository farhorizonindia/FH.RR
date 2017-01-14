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

public partial class MasterUI_AgentMaster : MasterBasePage
{
    BALAgentPayment blAgentpayment = new BALAgentPayment();
    DALAgentPayment dlAgentpayment = new DALAgentPayment();
    int GetQueryResponse = 0;
    DataTable dtGetReturenedData;

    BALLinks blLinks = new BALLinks();
    DALLinks dlLinks = new DALLinks();
    DataTable dtGetReturnedData;


    #region ControlsEvents

    protected void Page_Load(object sender, EventArgs e)
    {
        btnDelete.Attributes.Add("onclick", "return confirm('Are you sure you want to delete this record?')");
        if (!IsPostBack)
            RefreshGrid();
        EnableNewButton();
    }
    protected void btnAddNew_Click(object sender, EventArgs e)
    {
        ClearControls();
        btnAddNew.Enabled = false;
        btnCancel.Enabled = true;
        btnDelete.Enabled = false;
        btnEdit.Enabled = false;
        btnSave.Enabled = true;
        lblStatus.Text = "Add New action initiated";
        txtAgentName.Focus();
    }

    private void ClearControls()
    {
        txtAgentName.Text = String.Empty;
        txtAgentCode.Text = String.Empty;
        txtAgentEmailId.Text = String.Empty;
        txtPassword.Text = String.Empty;

    }

    protected void btnEdit_Click(object sender, EventArgs e)
    {
        int Id = 0;
        if (ValidateValues() == false)
            return;
        int.TryParse(hfId.Value, out Id);
        if (Id == 0)
        {
            lblStatus.Text = "Add action initiated.";
            Save();
        }
        else
        {
            lblStatus.Text = "Update action initiated.";
            Update();
        }
        hfId.Value = "";
        RefreshGrid();
        EnableNewButton();
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        lblStatus.Text = "Save action initiated";
        if (ValidateValues() == false)
            return;
        Save();
        RefreshGrid();
        EnableNewButton();
    }
    protected void btnDelete_Click(object sender, EventArgs e)
    {
        //lblStatus.Text = "Delete Action initiated";
        Delete();
        EnableNewButton();
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        EnableNewButton();
        ClearControls();
        hfId.Value = "";
        //SessionHandler"AgentID"] = null;
        lblStatus.Text = "Action Cancelled";
    }
    protected void dgAgents_SelectedIndexChanged(object sender, EventArgs e)
    {
        int iAgentID = Convert.ToInt32(dgAgents.DataKeys[dgAgents.SelectedIndex].ToString());
        hfId.Value = iAgentID.ToString();
        //SessionHandler"AgentID"] = iAgentID;
        AgentMaster oAgentMaster = new AgentMaster();
        AgentDTO[] oAgentData = oAgentMaster.GetData(iAgentID);
        if (oAgentData.Length > 0)
        {
            txtAgentCode.Text = oAgentData[0].AgentCode.ToString();
            txtAgentName.Text = oAgentData[0].AgentName.ToString();
            txtAgentEmailId.Text = oAgentData[0].EmailId.ToString();
            //  txtPassword.TextMode =TextBoxMode.SingleLine ;
            txtPassword.Text = oAgentData[0].Password.ToString();

            //   txtPassword.TextMode = TextBoxMode.Password;
        }
        oAgentMaster = null;
        oAgentData = null;

        #region fillingPaymentInfo
        dtGetReturenedData = new DataTable();
        blAgentpayment.agentid = iAgentID;
        blAgentpayment._Action = "getPaymentInfo";
        dtGetReturenedData = dlAgentpayment.GetAgentPaymentInfo(blAgentpayment);
        if (dtGetReturenedData != null)
        {
            if (dtGetReturenedData.Rows.Count > 0)
            {
                txtBillingAddress.Text = dtGetReturenedData.Rows[0]["BillingAddress"].ToString();
                txtCreditLimit.Text = dtGetReturenedData.Rows[0]["CreditLimit"].ToString();
                chkOnCredit.Checked = Convert.ToBoolean(dtGetReturenedData.Rows[0]["OnCredit"].ToString() == "" ? "false" : dtGetReturenedData.Rows[0]["OnCredit"].ToString());
                ddlpaymentMethod.SelectedValue = dtGetReturenedData.Rows[0]["PaymentMethod"].ToString();
                txtPhone.Text = dtGetReturenedData.Rows[0]["phone"].ToString();
            }
            else
            {
                txtBillingAddress.Text = "";
                txtCreditLimit.Text = "";
                chkOnCredit.Checked = false;
                ddlpaymentMethod.ClearSelection();
            }

        }
        #endregion
        //btnAddNew.Enabled = false;
        //btnCancel.Enabled = true;
        btnDelete.Enabled = true;
        btnCancel.Visible = true;
        btnEdit.Text = "Update";
        //btnSave.Enabled = false;
        lblStatus.Text = "";



    }
    protected void dgAgents_DeleteCommand(object source, DataGridCommandEventArgs e)
    {
        int iAgentID = Convert.ToInt32(dgAgents.DataKeys[e.Item.ItemIndex].ToString());
        AgentMaster oAgentMaster = new AgentMaster();
        AgentDTO oAgentData = new AgentDTO();
        oAgentData.AgentId = iAgentID;
        oAgentMaster.Delete(oAgentData);
        ClearControls();
        RefreshGrid();
        oAgentData = null;
        oAgentMaster = null;
    }

    #endregion ControlsEvents

    #region UserDefinedFunctions
    private void Save()
    {
        if (!base.ValidateIfCommandAllowed(Request.Url.AbsoluteUri, ENums.PageCommand.Add))
            return;

        int agentId;
        AgentDTO oAgentData = new AgentDTO();
        oAgentData.AgentCode = Convert.ToString(txtAgentCode.Text.Trim());
        oAgentData.AgentName = Convert.ToString(txtAgentName.Text.Trim());
        oAgentData.EmailId = txtAgentEmailId.Text.Trim();
        oAgentData.Password = txtPassword.Text.Trim();
        AgentMaster oAgentMaster = new AgentMaster();
        agentId = oAgentMaster.Insert(oAgentData);

        if (agentId > -1)
        {
            base.DisplayAlert("The record has been inserted successfully");
            PaymentInfo(agentId);
            ClearControls();
        }
        else
            lblStatus.Text = "Error Occured while insertion: Please refer to the error log.";
    }

    private void PaymentInfo(int agentId)
    {
        try
        {
            //dtGetReturenedData = new DataTable();
            //dtGetReturenedData = dlAgentpayment.getagentmasterinfo(Convert.ToInt32(ddlAgent.SelectedValue));

            blAgentpayment._Action = "AddDetails";
            blAgentpayment._FirstName = txtAgentName.Text;
            blAgentpayment._LastName = "";
            blAgentpayment._AgentCode = agentId;
            blAgentpayment._PaymentMethod = ddlpaymentMethod.SelectedItem.Text;
            blAgentpayment._EmailId = txtAgentEmailId.Text.Trim();
            //   blAgentpayment._AgentCode = Convert.ToInt32(dtGetReturenedData.Rows[0]["AgentId"].ToString());
            blAgentpayment._Password = txtPassword.Text.Trim();
            blAgentpayment.Phone = txtPhone.Text.Trim();

            blAgentpayment._BillingAddress = txtBillingAddress.Text.Trim().ToString();
            blAgentpayment.OnCredit = chkOnCredit.Checked;
            blAgentpayment.CreditLimit = Convert.ToDecimal(txtCreditLimit.Text.Trim() == "" ? "0" : txtCreditLimit.Text.Trim());

            GetQueryResponse = dlAgentpayment.AddpaymentDetails(blAgentpayment);
            if (GetQueryResponse > 0)
            {
                //  ClearAllControls();
                lbStatus.Text = "Payment details saved successfully.";
                lbStatus.ForeColor = System.Drawing.Color.Green;

            }
            else
            {
                lbStatus.Text = "Not Saved.. Please check ur entries once";
                lbStatus.ForeColor = System.Drawing.Color.Red;
            }
        }
        catch (Exception sqe)
        {
            lbStatus.Text = "Unexpected error found . please check ur entries.";
            lbStatus.ForeColor = System.Drawing.Color.Red;
        }

    }

    private void UpdatePaymentInfo()
    {
        try
        {
            //dtGetReturenedData = new DataTable();
            //dtGetReturenedData = dlAgentpayment.getagentmasterinfo(Convert.ToInt32(ddlAgent.SelectedValue));
            int Id;
            int.TryParse(hfId.Value, out Id);

            blAgentpayment._Action = "updatepaymenInfo";
            blAgentpayment._FirstName = txtAgentName.Text;
            blAgentpayment._LastName = "";
            blAgentpayment._PaymentMethod = ddlpaymentMethod.SelectedItem.Text;
            blAgentpayment._EmailId = txtAgentEmailId.Text.Trim();
            blAgentpayment._AgentCode = Id;
            blAgentpayment.Phone = txtPhone.Text.Trim();
            if (txtPassword.Text != "")
            {
                blAgentpayment._Password = txtPassword.Text.Trim();
            }
            else
            {
                blAgentpayment._Password = null;
            }

            blAgentpayment._BillingAddress = txtBillingAddress.Text.Trim().ToString();
            blAgentpayment.OnCredit = chkOnCredit.Checked;
            blAgentpayment.CreditLimit = Convert.ToDecimal(txtCreditLimit.Text.Trim() == "" ? "0" : txtCreditLimit.Text.Trim());
            
            GetQueryResponse = dlAgentpayment.UpdatepaymentDetails(blAgentpayment);
            if (GetQueryResponse > 0)
            {
                //  ClearAllControls();
                lbStatus.Text = "Payment details saved successfully.";
                lbStatus.ForeColor = System.Drawing.Color.Green;
            }
            else
            {
                lbStatus.Text = "Not Saved.. Please check ur entries once";
                lbStatus.ForeColor = System.Drawing.Color.Red;
            }
        }
        catch (Exception sqe)
        {
            lbStatus.Text = "Unexpected error found . please check ur entries.";
            lbStatus.ForeColor = System.Drawing.Color.Red;
        }

    }


    private void Update()
    {
        int Id = 0;
        bool bActionCompleted;

        if (!base.ValidateIfCommandAllowed(Request.Url.AbsoluteUri, ENums.PageCommand.Add))
            return;

        if (ValidateValues() == false)
            return;

        int.TryParse(hfId.Value, out Id);
        if (Id == 0)
        {
            lblStatus.Text = "Please click on edit again.";
            return;
        }

        AgentDTO oAgentData = new AgentDTO();
        oAgentData.AgentId = Id;
        oAgentData.AgentName = txtAgentName.Text.Trim();
        oAgentData.AgentCode = txtAgentCode.Text.Trim();
        oAgentData.EmailId = txtAgentEmailId.Text.Trim();
        if (txtPassword.Text != "")
        {
            oAgentData.Password = txtPassword.Text.Trim();
        }
        else
        {
            oAgentData.Password = null;
        }
        AgentMaster oAgentMaster = new AgentMaster();
        bActionCompleted = oAgentMaster.Update(oAgentData);
        if (bActionCompleted == true)
        {
            base.DisplayAlert("The record has been updated successfully");
            UpdatePaymentInfo();
            ClearControls();
        }
        else
            lblStatus.Text = "Error Occured while updation: Please refer to the error log.";
    }
    private void Delete()
    {
        int Id = 0;
        bool bActionCompleted;

        if (!base.ValidateIfCommandAllowed(Request.Url.AbsoluteUri, ENums.PageCommand.Add))
            return;

        int.TryParse(hfId.Value, out Id);
        if (Id == 0)
        {
            lblStatus.Text = "Please click on edit again.";
            return;
        }
        if (txtAgentName.Text.Trim() == "")
        {
            lblStatus.Text = "Please enter the Agent.";
            return;
        }

        if (txtAgentCode.Text.Trim() == "")
        {
            lblStatus.Text = "Please enter the Agent.";
            return;
        }
        AgentMaster oAgentMaster = new AgentMaster();
        AgentDTO oAgentData = new AgentDTO();
        oAgentData.AgentId = Id;

        /*
         * 
         * CHECK IF THE AGENT WHICH IS TO BE DELETED HAS ANY ASSOCIATED RECORDS...IF YES, MOVE OUT OF THE FUNCTION ELSE PROCEED
         * IF THE OUTPUT OF sMessage IS "", THEN RECORD CAN BE DELETED, ELSE NOT
         * 
         */
        string sMessage = "";
        GF.HasRecords(Convert.ToString(Id), "agent", out sMessage);
        if (sMessage != "")
        {
            base.DisplayAlert(sMessage);
            btnDelete.Enabled = true;
        }
        else
        {
            bActionCompleted = oAgentMaster.Delete(oAgentData);
            if (bActionCompleted == true)
            {
                ClearControls();
                RefreshGrid();
                oAgentData = null;
                oAgentMaster = null;
                //lblStatus.Text = "Deleted";
                base.DisplayAlert("The record has been deleted successfully");
            }
            else
            {
                base.DisplayAlert("Error Occured while deletion: Please refer to the error log.");
            }
            //lblStatus.Text = "Error Occured while deletion: Please refer to the error log.";
        }
    }
    private void RefreshGrid()
    {
        AgentMaster oAgentMaster = new AgentMaster();
        AgentDTO[] oAgentData = oAgentMaster.GetData();
        if (oAgentData != null)
        {
            if (oAgentData.Length > 0)
            {
                dgAgents.DataSource = oAgentData;
                dgAgents.DataBind();
            }
        }
        else
        {
            dgAgents.DataSource = null;
            dgAgents.DataBind();
        }
        ClearControls();
        oAgentData = null;
        oAgentMaster = null;
    }
    private void EnableNewButton()
    {
        //btnAddNew.Enabled = true;
        //btnCancel.Enabled = false;
        btnDelete.Enabled = false;
        btnCancel.Visible = false;
        btnEdit.Text = "Add";
        //btnEdit.Enabled = false;
        //btnSave.Enabled = false;
    }
    private bool ValidateValues()
    {
        if (txtAgentCode.Text.Trim() == "")
        {
            lblStatus.Text = "Please enter Agent Code.";
            return false;
        }
        if (txtAgentName.Text.Trim() == "")
        {
            lblStatus.Text = "Please enter Agent Name.";
            return false;
        }
        if (!String.IsNullOrEmpty(txtAgentEmailId.Text.Trim()))
        {
            if (!GF.ValidateEmailId(txtAgentEmailId.Text.Trim()))
            {
                lblStatus.Text = "Please enter correct email id.";
                return false;
            }
        }
        return true;
    }
    #endregion UserDefinedFunctions
    protected void dgAgents_ItemCommand(object source, DataGridCommandEventArgs e)
    {
        if (e.CommandName == "MaptoRate")
        {
            try
            {

                LinkButton lnk = (LinkButton)e.CommandSource;
                DataGridItem gitem = (DataGridItem)lnk.NamingContainer;
                int agentid = Convert.ToInt32(dgAgents.DataKeys[gitem.ItemIndex].ToString());

                string url = "../Rate/MapAgentsToRate.aspx?agentId=" + agentid;
                Response.Redirect(url);
                //Response.Write("<script> window.open('" + url + "','_blank'); </script>");
                // Page.ClientScript.RegisterStartupScript(this.GetType(), "popup", "<script language=javascript>window.open('" + url + "','','width=300px,height=200px').focus();</script>");
                //ScriptManager.RegisterStartupScript(this, this.GetType(), "myFunction", "<script language=javascript> window.open('" + url + "','','width=300px,height=200px').focus();</script>", true);
            }
            catch
            {
            }

        }
    }
}
