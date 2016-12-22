using FarHorizon.Reservations.Bases.BasePages;
using FarHorizon.Reservations.Common;
using FarHorizon.Reservations.Common.DataEntities.Masters;
using FarHorizon.Reservations.MasterServices;
using System;
using System.Web.UI.WebControls;

public partial class MasterUI_AgentMaster : MasterBasePage
{
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
        }
        oAgentMaster = null;
        oAgentData = null;

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

        bool bActionCompleted;
        AgentDTO oAgentData = new AgentDTO();
        oAgentData.AgentCode = Convert.ToString(txtAgentCode.Text.Trim());
        oAgentData.AgentName = Convert.ToString(txtAgentName.Text.Trim());
        oAgentData.EmailId = txtAgentEmailId.Text.Trim();
        AgentMaster oAgentMaster = new AgentMaster();
        bActionCompleted = oAgentMaster.Insert(oAgentData);
        if (bActionCompleted == true)
        {
            base.DisplayAlert("The record has been inserted successfully");
            ClearControls();
        }
        else
            lblStatus.Text = "Error Occured while insertion: Please refer to the error log.";
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
        AgentMaster oAgentMaster = new AgentMaster();
        bActionCompleted = oAgentMaster.Update(oAgentData);
        if (bActionCompleted == true)
        {
            base.DisplayAlert("The record has been updated successfully");
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
}
