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
using FarHorizon.Reservations.DataBaseManager;

public partial class MasterUI_EventMessageMaster : MasterBasePage
{
    #region Event ControlsEvent
    protected void Page_Load(object sender, EventArgs e)
    {
        btnDelete.Attributes.Add("onclick", "return confirm('Are you sure you want to delete this record?')");
        if (!IsPostBack)
            RefreshGrid();
        EnableNewButton();
    }
    protected void btnAddNew_Click(object sender, EventArgs e)
    {
        txtEventName.Text = "";
        txtEventMesssage.Text = "";
        txtEventSubject.Text = "";
        txtEventName.Focus();
        //ManageButtons(false);

        btnAddNew.Enabled = false;
        btnCancel.Enabled = true;
        btnDelete.Enabled = false;
        btnEdit.Enabled = false;
        btnSave.Enabled = true;
        lblStatus.Text = "Add New action initiated";
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (ValidateValues() == false)
            return;

        Save();
        txtEventName.Text = "";
        lblStatus.Text = "Saved";
        RefreshGrid();
        EnableNewButton();
    }
    protected void btnEdit_Click(object sender, EventArgs e)
    {
        if (ValidateValues() == false)
            return;
        int Id = 0;
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
        txtEventName.Text = "";
        RefreshGrid();
        EnableNewButton();
    }
    protected void btnDelete_Click(object sender, EventArgs e)
    {
        Delete();
        RefreshGrid();
        EnableNewButton();
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        EnableNewButton();
        txtEventName.Text = "";
        hfId.Value = "";
    }
    protected void dgEventMessages_SelectedIndexChanged(object sender, EventArgs e)
    {
        int iEventID = 0;
        int.TryParse(Convert.ToString(dgEventMessages.DataKeys[dgEventMessages.SelectedIndex]), out iEventID);
        hfId.Value = iEventID.ToString();
        EventMessageMaster oEventMaster = new EventMessageMaster();
        EventMessageDTO[] oEventData = oEventMaster.GetEventMessage(iEventID);
        if (oEventData.Length > 0)
        {
            txtEventName.Text = oEventData[0].EventName;
            txtEventMesssage.Text = oEventData[0].EventMessage;
            txtEventSubject.Text = oEventData[0].EventSubject;
            chkAllowMails.Checked = oEventData[0].MailAllowed;
        }
        oEventMaster = null;
        oEventData = null;

        //btnAddNew.Enabled = false;
        //btnCancel.Enabled = true;
        btnCancel.Visible = true;
        btnDelete.Enabled = true;
        btnEdit.Text = "Update";
        //btnEdit.Enabled = true;
        //btnSave.Enabled = false;
        lblStatus.Text = "";
    }

    #endregion ControlsEvent

    #region UserDefinedFunctions
    private void Save()
    {
        if (!base.ValidateIfCommandAllowed(Request.Url.AbsoluteUri, ENums.PageCommand.Add))
            return;

        bool bActionCompleted = false;
        EventMessageDTO oEventData = new EventMessageDTO();
        oEventData.EventName = txtEventName.Text.ToString();
        oEventData.EventMessage = txtEventMesssage.Text.ToString();
        oEventData.EventSubject = txtEventSubject.Text.ToString();
        oEventData.EventName = txtEventName.Text.ToString();
        oEventData.EventMessageDefault = "";
        if (chkAllowMails.Checked)
        {
            oEventData.MailAllowed = true;
        }
        else
        {
            oEventData.MailAllowed = false;
        }
        EventMessageMaster oEventMaster = new EventMessageMaster();
        bActionCompleted = oEventMaster.Insert(oEventData);
        if (bActionCompleted == true)
        {
            base.DisplayAlert("The record has been inserted successfully");
            txtEventName.Text = string.Empty;
            txtEventName.Text = string.Empty;
            txtEventMesssage.Text = string.Empty;
            txtEventSubject.Text = string.Empty;
            chkAllowMails.Checked = false;
            lblStatus.Text = "Saved";
        }
        else
            lblStatus.Text = "Error Occured while insertion: Please refer to the error log.";

        oEventData = null;
        oEventMaster = null;
    }
    public bool Insert(EventMessageDTO oEventMessageData)
    {
        string sProcName;
        DatabaseManager oDB;
        try
        {
            oDB = new DatabaseManager();

            sProcName = "up_Ins_EventMessage";
            oDB.DbCmd = oDB.GetStoredProcCommand(sProcName);
            oDB.DbDatabase.AddInParameter(oDB.DbCmd, "@EventName", DbType.String, oEventMessageData.EventName);
            oDB.DbDatabase.AddInParameter(oDB.DbCmd, "@EventMessage", DbType.String, oEventMessageData.EventMessage);
            oDB.DbDatabase.AddInParameter(oDB.DbCmd, "@EventSubject", DbType.String, oEventMessageData.EventSubject);
            oDB.DbDatabase.AddInParameter(oDB.DbCmd, "@EventMessageDefault", DbType.String, oEventMessageData.EventMessageDefault);
            oDB.DbDatabase.AddInParameter(oDB.DbCmd, "@MailAllowed", DbType.Boolean, oEventMessageData.MailAllowed);

            oDB.ExecuteNonQuery(oDB.DbCmd);
        }
        catch (Exception exp)
        {
            GF.LogError("clsEventMessageMaster.Insert", exp.Message.ToString());
            oDB = null;
            return false;
        }
        finally
        {
            oDB = null;
        }
        return true;
    }
    private void Update()
    {
        if (!base.ValidateIfCommandAllowed(Request.Url.AbsoluteUri, ENums.PageCommand.Update))
            return;

        if (ValidateValues() == false)
            return;
        bool bActionCompleted = false;
        int Id = 0;
        int.TryParse(hfId.Value, out Id);
        if (Id == 0)
        {
            lblStatus.Text = "Please click on edit button again.";
            return;
        }
        EventMessageDTO oEventData = new EventMessageDTO();
        oEventData.MessageId = Id;
        oEventData.EventName = txtEventName.Text.ToString();
        oEventData.EventMessage = txtEventMesssage.Text;
        oEventData.EventSubject = txtEventSubject.Text;
        oEventData.MailAllowed = chkAllowMails.Checked;

        EventMessageMaster oEventMaster = new EventMessageMaster();
        bActionCompleted = oEventMaster.Update(oEventData);
        if (bActionCompleted == true)
        {
            base.DisplayAlert("The record has been updated successfully");
            txtEventName.Text = "";
            lblStatus.Text = "Updated";
        }
        else
            lblStatus.Text = "Error Occured while updation: Please refer to the error log.";
        oEventData = null;
        oEventMaster = null;
    }
    private void Delete()
    {
        if (!base.ValidateIfCommandAllowed(Request.Url.AbsoluteUri, ENums.PageCommand.Delete))
            return;

        if (ValidateValues() == false)
            return;
        bool bActionCompleted = false;
        int Id = 0;
        int.TryParse(hfId.Value, out Id);
        if (Id == 0)
        {
            lblStatus.Text = "Please click on edit button again.";
            return;
        }

        EventMessageDTO oEventData = new EventMessageDTO();
        oEventData.MessageId = Id;
        EventMessageMaster oEventMaster = new EventMessageMaster();
        /*
         * ADDED BY VIJAY
         * CHECK IF THE Event WHICH IS TO BE DELETED HAS ANY ASSOCIATED RECORDS...IF YES, MOVE OUT OF THE FUNCTION ELSE PROCEED
         * IF THE OUTPUT OF sMessage IS "", THEN RECORD CAN BE DELETED, ELSE NOT
         * 
         */
        string sMessage = "";
        GF.HasRecords(Convert.ToString(Id), "tblEmailMessageMaster", out sMessage);
        if (sMessage != "")
        {
            base.DisplayAlert(sMessage);
            btnDelete.Enabled = true;
        }
        else
        {
            bActionCompleted = oEventMaster.Delete(oEventData);

            if (bActionCompleted == true)
            {
                base.DisplayAlert("The record has been deleted successfully.");
                txtEventName.Text = "";
                //lblStatus.Text = "Deleted";
                RefreshGrid();
            }
            else
            {
                base.DisplayAlert("Error Occured while deletion: Please refer to the error log.");
            }
        }
        oEventData = null;
        oEventMaster = null;
    }
    private void RefreshGrid()
    {
        EventMessageMaster oEventMaster;
        EventMessageDTO[] oEventData;
        oEventMaster = new EventMessageMaster();
        oEventData = oEventMaster.GetEventMessage();
        if (oEventData != null)
        {
            if (oEventData.Length > 0)
            {
                dgEventMessages.DataSource = oEventData;
                dgEventMessages.DataBind();
            }
        }
        else
        {
            dgEventMessages.DataSource = null;
            dgEventMessages.DataBind();
        }
        txtEventName.Text = "";
        txtEventSubject.Text = ""; ;
        txtEventMesssage.Text = "";
        chkAllowMails.Checked = false;
        oEventMaster = null;
        oEventData = null;
    }
    private void EnableNewButton()
    {
        //btnAddNew.Enabled = true;
        //btnCancel.Enabled = false;
        btnCancel.Visible = false;
        btnDelete.Enabled = false;
        btnEdit.Text = "Add";
        //btnEdit.Enabled = false;
        //btnSave.Enabled = false;
    }
    private bool ValidateValues()
    {
        if (txtEventName.Text.Trim() == "")
        {
            lblStatus.Text = "Please enter event name.";
            return false;
        }
        if (txtEventMesssage.Text.Trim() == "")
        {
            lblStatus.Text = "Please enter event message.";
            return false;
        }
        if (txtEventSubject.Text.Trim() == "")
        {
            lblStatus.Text = "Please enter event subject.";
            return false;
        }
        return true;
    }
    #endregion UserDefinedFunctions
}
