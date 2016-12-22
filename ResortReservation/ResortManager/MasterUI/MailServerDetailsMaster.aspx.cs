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
using FarHorizon.Reservations.Common.DataEntities.Masters;

using FarHorizon.Reservations.MasterServices;
using FarHorizon.Reservations.Bases;
using FarHorizon.Reservations.Bases.BasePages;

public partial class MasterUI_MailDetailsMaster : MasterBasePage
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
        ClearPageControls();

        btnAddNew.Enabled = false;
        btnCancel.Enabled = true;
        btnDelete.Enabled = false;
        btnEdit.Enabled = false;
        btnSave.Enabled = true;
        lblStatus.Text = "Add New action initiated";
        txtSMTPServer.Focus();
    }

    private void ClearPageControls()
    {
        txtSMTPServer.Text = string.Empty;
        txtFromDisplayName.Text = string.Empty;
        txtFromId.Text = string.Empty;
        txtPassword.Text = string.Empty;
        txtPort.Text = string.Empty;
        txtReplyToId.Text = string.Empty;
        txtUserId.Text = string.Empty;
        chkActive.Checked = false;
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
        ClearPageControls();
        hfId.Value = "";
        //SessionHandler"MailDetailsID"] = null;
        lblStatus.Text = "Action Cancelled";
    }
    protected void dgMailDetailss_SelectedIndexChanged(object sender, EventArgs e)
    {
        int iSMTPID = Convert.ToInt32(dgMailServerDetails.DataKeys[dgMailServerDetails.SelectedIndex].ToString());
        hfId.Value = iSMTPID.ToString();
        //SessionHandler"MailDetailsID"] = iMailDetailsID;
        SMTPMaster oSMTPMaster = new SMTPMaster();
        SMTPDTO[] oSMTPData = oSMTPMaster.GetSMTPDetails(iSMTPID);
        if (oSMTPData.Length > 0)
        {
            txtSMTPServer.Text = oSMTPData[0].SMTPServer;
            txtFromDisplayName.Text = oSMTPData[0].FromDisplayName;
            txtFromId.Text = oSMTPData[0].FromEmailId;
            txtPassword.Text = oSMTPData[0].SMTPPassword;
            txtPort.Text = oSMTPData[0].Port.ToString();
            txtReplyToId.Text = oSMTPData[0].ReplyToId;
            txtUserId.Text = oSMTPData[0].SMTPUserId;
            chkActive.Checked = oSMTPData[0].Active;
        }
        oSMTPMaster = null;
        oSMTPData = null;

        //btnAddNew.Enabled = false;
        //btnCancel.Enabled = true;
        btnDelete.Enabled = true;
        btnCancel.Visible = true;
        btnEdit.Text = "Update";
        //btnSave.Enabled = false;
        lblStatus.Text = "";
    }
    protected void dgMailDetailss_DeleteCommand(object source, DataGridCommandEventArgs e)
    {
        int iSMTPID = Convert.ToInt32(dgMailServerDetails.DataKeys[e.Item.ItemIndex].ToString());
        SMTPMaster oSMTPMaster = new SMTPMaster();
        SMTPDTO oSMTPData = new SMTPDTO();
        oSMTPData.SMTPId = iSMTPID;
        oSMTPMaster.Delete(oSMTPData);
        ClearPageControls();
        RefreshGrid();
        oSMTPData = null;
        oSMTPMaster = null;
    }

    #endregion ControlsEvents

    #region UserDefinedFunctions
    private void Save()
    {
        if (!base.ValidateIfCommandAllowed(Request.Url.AbsoluteUri, ENums.PageCommand.Add))
            return;

        bool bActionCompleted;

        SMTPMaster oSMTPMaster = new SMTPMaster();
        SMTPDTO oSMTPData = MapControlsToObject();        

        bActionCompleted = oSMTPMaster.Insert(oSMTPData);
        if (bActionCompleted == true)
        {
            base.DisplayAlert("The record has been inserted successfully");
            ClearPageControls();
        }
        else
            lblStatus.Text = "Error Occured while insertion: Please refer to the error log.";
    }
    private void Update()
    {
        if (!base.ValidateIfCommandAllowed(Request.Url.AbsoluteUri, ENums.PageCommand.Update))
            return;

        bool bActionCompleted;
        if (ValidateValues() == false)
            return;

        int Id = 0;
        int.TryParse(hfId.Value, out Id);
        if (Id == 0)
        {
            lblStatus.Text = "Please click on edit again.";
            return;
        }

        SMTPMaster oSMTPMaster = new SMTPMaster();
        SMTPDTO oSMTPData = MapControlsToObject();
        
        bActionCompleted = oSMTPMaster.Update(oSMTPData);
        if (bActionCompleted == true)
        {
            base.DisplayAlert("The record has been updated successfully");
            ClearPageControls();
        }
        else
            lblStatus.Text = "Error Occured while updation: Please refer to the error log.";
    }

    private SMTPDTO MapControlsToObject()
    {
        int Id = 0;
        int.TryParse(hfId.Value, out Id);

        SMTPDTO oSMTPData = new SMTPDTO();
        oSMTPData.SMTPId = Id;
        oSMTPData.SMTPServer = txtSMTPServer.Text;
        oSMTPData.FromDisplayName = txtFromDisplayName.Text;
        oSMTPData.FromEmailId = txtFromId.Text;
        oSMTPData.SMTPPassword = txtPassword.Text;
        oSMTPData.Port = Convert.ToInt32(txtPort.Text);
        oSMTPData.ReplyToId = txtReplyToId.Text;
        oSMTPData.SMTPUserId = txtUserId.Text;
        oSMTPData.Active = chkActive.Checked;
        return oSMTPData;
    }

    private void Delete()
    {
        int Id = 0;
        bool bActionCompleted;

        if (!base.ValidateIfCommandAllowed(Request.Url.AbsoluteUri, ENums.PageCommand.Delete))
            return;

        int.TryParse(hfId.Value, out Id);
        if (Id == 0)
        {
            lblStatus.Text = "Please click on edit again.";
            return;
        }
        if (txtSMTPServer.Text.Trim() == "")
        {
            lblStatus.Text = "Please enter the SMTP server.";
            return;
        }
        
        SMTPMaster oSMTPMaster = new SMTPMaster();
        SMTPDTO oSMTPData = MapControlsToObject();

        /*
         * 
         * CHECK IF THE MailDetails WHICH IS TO BE DELETED HAS ANY ASSOCIATED RECORDS...IF YES, MOVE OUT OF THE FUNCTION ELSE PROCEED
         * IF THE OUTPUT OF sMessage IS "", THEN RECORD CAN BE DELETED, ELSE NOT
         * 
         */
        string sMessage = "";
        GF.HasRecords(Convert.ToString(Id), "MailDetails", out sMessage);
        if (sMessage != "")
        {
            base.DisplayAlert(sMessage);
            btnDelete.Enabled = true;
        }
        else
        {
            bActionCompleted = oSMTPMaster.Delete(oSMTPData);
            if (bActionCompleted == true)
            {
                ClearPageControls();
                RefreshGrid();
                oSMTPData = null;
                oSMTPMaster = null;
                //lblStatus.Text = "Deleted";
                base.DisplayAlert("The record has been deleted successfully.");
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
        SMTPMaster oSMTPMaster = new SMTPMaster();
        SMTPDTO[] oSMTPData = oSMTPMaster.GetSMTPDetails();
        if (oSMTPData != null)
        {
            if (oSMTPData.Length > 0)
            {
                dgMailServerDetails.DataSource = oSMTPData;
                dgMailServerDetails.DataBind();
            }
        }
        else
        {
            dgMailServerDetails.DataSource = null;
            dgMailServerDetails.DataBind();
        }
        ClearPageControls();
        oSMTPData = null;
        oSMTPMaster = null;
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
        SMTPMaster SMTPMaster = new SMTPMaster();
        if (txtSMTPServer.Text.Trim() == "")
        {
            lblStatus.Text = "Please enter Smtp Server.";
            return false;
        }
        if (txtFromId.Text.Trim() == "")
        {
            lblStatus.Text = "Please enter From email id.";
            return false;
        }
        if(txtReplyToId.Text.Trim() == "")
        {
            lblStatus.Text = "Please enter Reply email id.";
            return false;
        }
        if (txtFromDisplayName.Text.Trim() == "")
        {
            lblStatus.Text = "Please enter From display name.";
            return false;
        }
        /*if (txtUserId.Text.Trim() == "")
        {
            lblStatus.Text = "Please enter SMTP user id.";
            return false;
        }
        if (txtPassword.Text.Trim() == "")
        {
            lblStatus.Text = "Please enter the SMTP password.";
        }*/
        if (txtPort.Text.Trim() == "")
        {
            lblStatus.Text = "Please enter the SMTP port.";
        }
        int port;
        if (!int.TryParse(txtPort.Text.Trim(), out port))
        {
            lblStatus.Text = "SMTP port can be numeric only.";
        }

        if (!SMTPMaster.IsValidEmailId(txtFromId.Text))
        {
            lblStatus.Text = "From Email Id is invalid.";
        }

        if (!SMTPMaster.IsValidEmailId(txtReplyToId.Text))
        {
            lblStatus.Text = "Reply Email Id is invalid.";
        }


        return true;
    }
    #endregion UserDefinedFunctions
}
