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

public partial class MasterUI_AccomodationContactMaster : MasterBasePage
{
    #region ControlsEvents
    protected void Page_Load(object sender, EventArgs e)
    {
        btnDelete.Attributes.Add("onclick", "return confirm('Are you sure you want to delete this record?')");
        if (!IsPostBack)
        {
            FillAccomodations();
            //RefreshGrid();            
            EnableNewButton();
        }

    }

    protected void ddlAccomodation_SelectedIndexChanged1(object sender, EventArgs e)
    {
        RefreshGrid();
    }

    protected void btnAddNew_Click(object sender, EventArgs e)
    {
        ClearPageControls();
        //ManageButtons(false);

        btnAddNew.Enabled = false;
        btnCancel.Enabled = true;
        btnDelete.Enabled = false;
        //btnEdit.Enabled = false;
        btnSave.Enabled = true;
        btnSave.Text = "Save";
        lblStatus.Text = "Add New action initiated";
    }

    private void ClearPageControls()
    {
        hfId.Value = "";
        txtContactName.Text = "";
        txtToIds.Text = string.Empty;
        txtCCIds.Text = string.Empty;
        txtBCCIds.Text = string.Empty;

        rdBookingYes.Checked = false;
        rdBookingNo.Checked = false;
        rdBookingUpdatYes.Checked = false;
        rdBookingUpdatNo.Checked = false;
        rdConfirmationYes.Checked = false;
        rdConfirmationNo.Checked = false;
        rdConfirmationUpdateYes.Checked = false;
        rdConfirmationUpdateNo.Checked = false;
        rdCancellationYes.Checked = false;
        rdConfirmationNo.Checked = false;
        rdDeletionYes.Checked = false;
        rdDeletionNo.Checked = false;

        txtContactName.Focus();
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (ValidateValues() == false)
            return;

        Save();
        ClearPageControls();
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
        ClearPageControls();
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
        ClearPageControls();
        EnableNewButton();
        hfId.Value = "";
    }
    protected void dgAccomodationContacts_SelectedIndexChanged(object sender, EventArgs e)
    {
        int iContactID = 0;
        int.TryParse(Convert.ToString(dgAccomodationContacts.DataKeys[dgAccomodationContacts.SelectedIndex]), out iContactID);
        hfId.Value = iContactID.ToString();       
        
        AccomodationContactsMaster oAccomodationContactMaster = new AccomodationContactsMaster();
        AccomContactDTO[] oAccomodationContactData = oAccomodationContactMaster.GetAccomodationContacts(0, iContactID);
        if (oAccomodationContactData.Length > 0)
        {
            ddlAccomodation.SelectedIndex = oAccomodationContactData[0].AccomodationId;
            txtContactName.Text = oAccomodationContactData[0].ContactName;
            txtToIds.Text = oAccomodationContactData[0].ToId;
            txtCCIds.Text = oAccomodationContactData[0].CCId;
            txtBCCIds.Text = oAccomodationContactData[0].BCCId;

            if (oAccomodationContactData[0].MailOnBooking)
                rdBookingYes.Checked = true;
            else
                rdBookingNo.Checked = true;

            if (oAccomodationContactData[0].MailOnBookingUpdate)
                rdBookingUpdatYes.Checked = true;
            else
                rdBookingUpdatNo.Checked = true;

            if (oAccomodationContactData[0].MailOnBookingConfirmation)
                rdConfirmationYes.Checked = true;
            else
                rdConfirmationNo.Checked = true;

            if (oAccomodationContactData[0].MailOnBookingConfirmationUpdate)
                rdConfirmationUpdateYes.Checked = true;
            else
                rdConfirmationUpdateNo.Checked = true;

            if (oAccomodationContactData[0].MailOnCancellation)
                rdCancellationYes.Checked = true;
            else
                rdCancellationNo.Checked = true;

            if (oAccomodationContactData[0].MailOnDeletion)
                rdDeletionYes.Checked = true;
            else
                rdDeletionNo.Checked = true;


        }
        oAccomodationContactMaster = null;
        oAccomodationContactData = null;

        btnAddNew.Enabled = false;
        btnCancel.Enabled = true;
        btnDelete.Enabled = true;
        //btnEdit.Text = "Update";
        btnSave.Text = "Update";
        btnSave.Enabled = true;
        //btnEdit.Enabled = true;
        //btnSave.Enabled = false;
        lblStatus.Text = "";
    }
    protected void ddlAccomodation_SelectedIndexChanged(object sender, EventArgs e)
    {
        RefreshGrid();
    }

    protected void btnCopyContacts_Click(object sender, EventArgs e)
    {
        if (ddlAccomodation.SelectedItem.Text == "Choose Accomodation")
        {
            base.DisplayAlert("Please choose the accomodation.");
            return;
        }

        if (ddlDestinationAccomodation.SelectedItem.Text == "Choose Accomodation")
        {
            base.DisplayAlert("Please choose the destination.");
            return;
        }

        if (ddlAccomodation.SelectedItem.Text == ddlDestinationAccomodation.SelectedItem.Text)
        {
            base.DisplayAlert("Source and destination cannot be same.");
            return;
        }
        int SourceAccomodationId, DestinationAccomodationId;
        int.TryParse(ddlAccomodation.SelectedItem.Value, out SourceAccomodationId);
        int.TryParse(ddlDestinationAccomodation.SelectedItem.Value, out DestinationAccomodationId);

        AccomodationContactsMaster objAccomodationContactsMaster = new AccomodationContactsMaster();
        objAccomodationContactsMaster.CopyContacts(SourceAccomodationId, DestinationAccomodationId);
        base.DisplayAlert("Contacts are copied.");
        return;
    }
    #endregion ControlsAccomodationContact

    #region UserDefinedFunctions
    private void Save()
    {
        if (!base.ValidateIfCommandAllowed(Request.Url.AbsoluteUri, ENums.PageCommand.Add))
            return;

        if (ValidateValues() == false)
            return;

        bool bActionCompleted = false;

        AccomContactDTO oAccomodationContactData = MapControlsToObject();

        AccomodationContactsMaster oAccomodationContactMaster = new AccomodationContactsMaster();
        bActionCompleted = oAccomodationContactMaster.Insert(oAccomodationContactData);
        if (bActionCompleted == true)
        {
            base.DisplayAlert("The record has been inserted successfully");
            ClearPageControls();
            lblStatus.Text = "Saved";
        }
        else
            lblStatus.Text = "Error Occured while insertion: Please refer to the error log.";

        oAccomodationContactData = null;
        oAccomodationContactMaster = null;
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
        AccomContactDTO oAccomodationContactData = new AccomContactDTO();
        oAccomodationContactData.ContactId = Id;
        oAccomodationContactData.AccomodationId = Convert.ToInt32(ddlAccomodation.SelectedValue);
        oAccomodationContactData.ContactName = txtContactName.Text.ToString();
        oAccomodationContactData.ToId = txtToIds.Text;
        oAccomodationContactData.CCId = txtCCIds.Text;
        oAccomodationContactData.BCCId = txtBCCIds.Text;

        oAccomodationContactData.MailOnBooking = false;
        if (rdBookingYes.Checked)
            oAccomodationContactData.MailOnBooking = true;

        oAccomodationContactData.MailOnBookingUpdate = false;
        if (rdBookingUpdatYes.Checked)
            oAccomodationContactData.MailOnBookingUpdate = true;

        oAccomodationContactData.MailOnBookingConfirmation = false;
        if (rdConfirmationYes.Checked)
            oAccomodationContactData.MailOnBookingConfirmation = true;

        oAccomodationContactData.MailOnBookingConfirmationUpdate = false;
        if (rdConfirmationUpdateYes.Checked)
            oAccomodationContactData.MailOnBookingConfirmationUpdate = true;

        oAccomodationContactData.MailOnCancellation = false;
        if (rdCancellationYes.Checked)
            oAccomodationContactData.MailOnCancellation = true;

        oAccomodationContactData.MailOnDeletion = false;
        if (rdDeletionYes.Checked)
            oAccomodationContactData.MailOnDeletion = true;

        AccomodationContactsMaster oAccomodationContactMaster = new AccomodationContactsMaster();
        bActionCompleted = oAccomodationContactMaster.Update(oAccomodationContactData);
        if (bActionCompleted == true)
        {
            base.DisplayAlert("The record has been updated successfully");
            txtContactName.Text = "";
            lblStatus.Text = "Updated";
        }
        else
            lblStatus.Text = "Error Occured while updation: Please refer to the error log.";
        oAccomodationContactData = null;
        oAccomodationContactMaster = null;
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

        AccomContactDTO oAccomodationContactData = new AccomContactDTO();
        oAccomodationContactData.ContactId = Id;
        AccomodationContactsMaster oAccomodationContactMaster = new AccomodationContactsMaster();
        /*
         * ADDED BY VIJAY
         * CHECK IF THE AccomodationContact WHICH IS TO BE DELETED HAS ANY ASSOCIATED RECORDS...IF YES, MOVE OUT OF THE FUNCTION ELSE PROCEED
         * IF THE OUTPUT OF sMessage IS "", THEN RECORD CAN BE DELETED, ELSE NOT
         * 
         */
        string sMessage = "";
        GF.HasRecords(Convert.ToString(Id), "tblAccomodationContactsMaster", out sMessage);
        if (sMessage != "")
        {
            base.DisplayAlert(sMessage);
            btnDelete.Enabled = true;
        }
        else
        {
            bActionCompleted = oAccomodationContactMaster.Delete(oAccomodationContactData);

            if (bActionCompleted == true)
            {
                base.DisplayAlert("The record has been deleted successfully");
                ClearPageControls();
                //lblStatus.Text = "Deleted";
                RefreshGrid();
            }
            else
            {
                base.DisplayAlert("Error Occured while deletion: Please refer to the error log.");
            }
        }
        oAccomodationContactData = null;
        oAccomodationContactMaster = null;
    }

    private AccomContactDTO MapControlsToObject()
    {
        AccomContactDTO oAccomodationContactData = new AccomContactDTO();
        oAccomodationContactData.ContactName = txtContactName.Text.ToString();
        oAccomodationContactData.ToId = txtToIds.Text;
        oAccomodationContactData.CCId = txtCCIds.Text;
        oAccomodationContactData.BCCId = txtBCCIds.Text;
        oAccomodationContactData.AccomodationId = Convert.ToInt32(ddlAccomodation.SelectedValue);

        oAccomodationContactData.MailOnBooking = rdBookingYes.Checked;
        oAccomodationContactData.MailOnBookingUpdate = rdBookingUpdatYes.Checked;
        oAccomodationContactData.MailOnBookingConfirmation = rdCancellationYes.Checked;
        oAccomodationContactData.MailOnBookingConfirmationUpdate = rdConfirmationUpdateYes.Checked;
        oAccomodationContactData.MailOnCancellation = rdConfirmationUpdateYes.Checked;
        oAccomodationContactData.MailOnDeletion = rdDeletionYes.Checked;
        return oAccomodationContactData;
    }    
    private void RefreshGrid()
    {
        int AccomodationId = Convert.ToInt32(ddlAccomodation.SelectedValue);        
        AccomodationContactsMaster oAccomodationContactMaster;
        AccomContactDTO[] oAccomodationContactData;
        oAccomodationContactMaster = new AccomodationContactsMaster();
        oAccomodationContactData = oAccomodationContactMaster.GetAccomodationContacts(AccomodationId);
        if (oAccomodationContactData != null)
        {
            if (oAccomodationContactData.Length > 0)
            {
                dgAccomodationContacts.DataSource = oAccomodationContactData;
                dgAccomodationContacts.DataBind();
            }
        }
        else
        {
            dgAccomodationContacts.DataSource = null;
            dgAccomodationContacts.DataBind();
        }
        ClearPageControls();
        oAccomodationContactMaster = null;
        oAccomodationContactData = null;
    }
    private void EnableNewButton()
    {
        btnAddNew.Enabled = true;
        btnCancel.Enabled = false;
        //btnCancel.Visible = false;
        btnDelete.Enabled = false;
        btnEdit.Text = "Add";
        btnEdit.Enabled = false;
        btnSave.Enabled = false;
    }
    private bool ValidateValues()
    {
        if (txtContactName.Text.Trim() == "")
        {
            lblStatus.Text = "Please enter contact name.";
            return false;
        }
        if (txtToIds.Text.Trim() == "")
        {
            lblStatus.Text = "Please enter To ids.";
            return false;
        }
        if (txtCCIds.Text.Trim() == "")
        {
            lblStatus.Text = "Please enter Cc ids.";
            return false;
        }
        if (ddlAccomodation.SelectedIndex <= 0)
        {
            lblStatus.Text = "Please select the accomodation.";
            return false;
        }
        return true;
    }

    private void FillAccomodations()
    {
        ddlAccomodation.Items.Clear();
        ddlDestinationAccomodation.Items.Clear();

        SortedList slAccomData = new SortedList();

        AccomodationMaster AccomodationMaster = new AccomodationMaster();
        AccomodationDTO[] oAccomData = AccomodationMaster.GetAccomodations();
        slAccomData.Add("0", "Choose Accomodation");
        if (oAccomData != null)
        {
            for (int i = 0; i < oAccomData.Length; i++)
            {
                slAccomData.Add(Convert.ToString(oAccomData[i].AccomodationId), Convert.ToString(oAccomData[i].AccomodationName));
            }
            ddlAccomodation.DataSource = slAccomData;
            ddlAccomodation.DataTextField = "value";
            ddlAccomodation.DataValueField = "key";
            ddlAccomodation.DataBind();

            ddlDestinationAccomodation.DataSource = slAccomData;
            ddlDestinationAccomodation.DataTextField = "value";
            ddlDestinationAccomodation.DataValueField = "key";
            ddlDestinationAccomodation.DataBind();
        }
        ddlAccomodation.SelectedIndex = 0;
    }
    #endregion UserDefinedFunctions    
}
