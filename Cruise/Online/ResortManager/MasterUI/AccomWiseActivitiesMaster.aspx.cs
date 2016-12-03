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
using FarHorizon.Reservations.BusinessServices;
using FarHorizon.Reservations.MasterServices;
using FarHorizon.Reservations.Common.DataEntities.Masters;

using FarHorizon.Reservations.Bases;
using FarHorizon.Reservations.Common;
using FarHorizon.Reservations.Bases.BasePages;

public partial class MasterUI_AccomWiseActivitiesMaster : MasterBasePage
{
    #region ControlsEvents
    protected void Page_Load(object sender, EventArgs e)
    {
        btnDelete.Attributes.Add("onclick", "return confirm('Are you sure you want to delete this record?')");
        btnEdit.Attributes.Add("onclick", "return ValidateForm()");
        if (!IsPostBack)
        {
            FillAccomodations();            
            EnableNewButton();
        }
    }
    protected void btnAddNew_Click(object sender, EventArgs e)
    {
        //SessionHandler"AccomodationID"] = null;
        //ddlAccomTypeId.SelectedIndex = 0;
        //ddlRegion.SelectedIndex = 0;
        ClearControls();

        btnAddNew.Enabled = false;
        btnCancel.Enabled = true;
        btnDelete.Enabled = false;
        btnEdit.Enabled = false;
        btnSave.Enabled = true;
        txtActivityName.Focus();
        lblStatus.Text = "Add New action initiated";
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
        btnSearch_Click(sender, e);
        EnableNewButton();
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        lblStatus.Text = "Save action initiated";
        if (ValidateValues() == false)
            return;
        Save();
        btnSearch_Click(sender, e);
        EnableNewButton();
    }
    protected void btnDelete_Click(object sender, EventArgs e)
    {
        lblStatus.Text = "Delete Action initiated";
        Delete();
        hfId.Value = "";
        btnSearch_Click(sender, e);
        EnableNewButton();
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        EnableNewButton();
        ClearControls();
        hfId.Value = "";
        //SessionHandler"AccomodationID"] = null;
        lblStatus.Text = "Action Cancelled";
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        int AccomId = 0;
        AccomId = Convert.ToInt32(ddlAccomodations.SelectedValue);
        hfAccomId.Value = AccomId.ToString();
        RefreshGrid();
    }
    protected void dgAccomWiseActivities_SelectedIndexChanged(object sender, EventArgs e)
    {
        //DateTime dt;
        int iActivityId = 0;
        iActivityId = Convert.ToInt32(dgAccomWiseActivities.DataKeys[dgAccomWiseActivities.SelectedIndex]);
        int iAccomID = Convert.ToInt32(dgAccomWiseActivities.Items[dgAccomWiseActivities.SelectedIndex].Cells[0].Text);
        hfAccomId.Value = iAccomID.ToString();
        hfId.Value = iActivityId.ToString();
        //SessionHandler"AccomodationID"] = iAccomID;
        //clsAccomodationMaster oAccomMaster = new clsAccomodationMaster();
        //clsAccomData[] oAccomData = oAccomMaster.GetData(0, 0, iAccomID);
        txtActivityName.Text = dgAccomWiseActivities.SelectedItem.Cells[3].Text.ToString().Trim();
        txtActivityDesc.Text = dgAccomWiseActivities.SelectedItem.Cells[4].Text.ToString().Trim();

        btnCancel.Visible = true;
        btnDelete.Enabled = true;
        btnEdit.Text = "Update";
        lblStatus.Text = "";
    }
    
    #endregion ControlsEvents

    #region UserDefinedFuntions
    private void FillAccomodations()
    {
        AccomodationMaster oAccomMaster = new AccomodationMaster();
        AccomodationDTO[] oAccomData = null; 
        ddlAccomodations.Items.Clear();

        ListItem l;
        l = new ListItem();
        l.Text = "Choose";
        l.Value = "-1";
        ddlAccomodations.Items.Insert(0, l);

        l = new ListItem();
        l.Text = "All";
        l.Value = "0";
        ddlAccomodations.Items.Insert(1, l);

        oAccomData = oAccomMaster.GetData();
        if (oAccomData != null)
        {
            for (int i = 0; i < oAccomData.Length; i++)
            {
                l = new ListItem();
                l.Text = oAccomData[i].AccomodationName;
                l.Value = oAccomData[i].AccomodationId.ToString();
                ddlAccomodations.Items.Insert(i + 2, l);
            }
        }
        
        oAccomData = null;
        oAccomMaster = null;
    }
    
    private void Save()
    {
        if (!base.ValidateIfCommandAllowed(Request.Url.AbsoluteUri, ENums.PageCommand.Add))
            return;

        AccomActivityDTO oAccomWiseActivityData = new AccomActivityDTO();
        AccomActivityMaster oAccomWiseActivityMaster = new AccomActivityMaster();
        bool bActionCompleted;
        oAccomWiseActivityData = MapControlsToObject();
        bActionCompleted = oAccomWiseActivityMaster.Insert(oAccomWiseActivityData);
        if (bActionCompleted == true)
        {
            base.DisplayAlert("The record has been insert successfully");
            ClearControls();
            lblStatus.Text = "Saved";
        }
        else
            lblStatus.Text = "Error Occured while saving: Please refer to the error log.";
        oAccomWiseActivityData = null;
        oAccomWiseActivityMaster = null;
    }
    private void Update()
    {
        if (!base.ValidateIfCommandAllowed(Request.Url.AbsoluteUri, ENums.PageCommand.Update))
            return;

        bool bActionCompleted;
        int Id = 0;
        int.TryParse(hfId.Value, out Id);
        if (Id == 0)
        {
            lblStatus.Text = "Please click on edit again.";
            return;
        }
        if (txtActivityName.Text.Trim() == "")
        {
            lblStatus.Text = "Please enter the Activity Name.";
            return;
        }
        if (txtActivityDesc.Text.Trim() == "")
        {
            lblStatus.Text = "Please enter the Activity Description.";
            return;
        }

        AccomActivityDTO oAccomWiseActivityData = null;
        AccomActivityMaster oAccomWiseActivityMaster = new AccomActivityMaster();

        oAccomWiseActivityData = MapControlsToObject();
        oAccomWiseActivityData.ActivityId = Id;
        bActionCompleted = oAccomWiseActivityMaster.Update(oAccomWiseActivityData);
        if (bActionCompleted == true)
        {
            base.DisplayAlert("The record has been updated successfully");
            ClearControls();
            lblStatus.Text = "Updated Successfully";
        }
        else
            lblStatus.Text = "Error Occured while updating: Please refer to the error log.";
        oAccomWiseActivityData = null;
        oAccomWiseActivityMaster = null;
    }
    private void Delete()
    {
        if (!base.ValidateIfCommandAllowed(Request.Url.AbsoluteUri, ENums.PageCommand.Delete))
            return;

        bool bActionCompleted;
        AccomActivityDTO oAccomWiseActivityData = new AccomActivityDTO();
        AccomActivityMaster oAccomWiseActivityMaster = new AccomActivityMaster();

        int Id = 0;
        int.TryParse(hfId.Value, out Id);
        if (Id == 0)
        {
            lblStatus.Text = "Please click on edit again.";
            return;
        }
        oAccomWiseActivityData.AccomodationId = Id;
        bActionCompleted = oAccomWiseActivityMaster.Delete(oAccomWiseActivityData);
        if (bActionCompleted == true)
        {
            base.DisplayAlert("The record has been deleted successfully");
            ClearControls();
            lblStatus.Text = "Deleted";
        }
        else
            lblStatus.Text = "Error Occured while updating: Please refer to the error log.";

        oAccomWiseActivityMaster = null;
        oAccomWiseActivityData = null;
    }

    private AccomActivityDTO MapControlsToObject()
    {
        AccomActivityDTO oAccomWiseActivityData = new AccomActivityDTO();
        //DateTime dt;
        int id = 0;
        int.TryParse(hfAccomId.Value, out id);
        oAccomWiseActivityData.AccomodationId = id;

        id = 0;
        int.TryParse(hfId.Value, out id);
        oAccomWiseActivityData.ActivityId = id;

        oAccomWiseActivityData.ActivityName = txtActivityName.Text.Trim();
        oAccomWiseActivityData.ActivityDesc = txtActivityDesc.Text.Trim();      

        return oAccomWiseActivityData;
    }
    

    private void RefreshGrid()
    {
        int AccomodationId = 0;
        int.TryParse(hfAccomId.Value, out AccomodationId);
        AccomActivityMaster oAccomWiseActivityMaster = new AccomActivityMaster();
        AccomActivityDTO[] oAccomWiseActivityData = oAccomWiseActivityMaster.GetData(AccomodationId);
        if (oAccomWiseActivityData != null)
        {
            if (oAccomWiseActivityData.Length > 0)
            {
                dgAccomWiseActivities.DataSource = oAccomWiseActivityData;
                dgAccomWiseActivities.DataBind();
            }
        }
        else
        {
            dgAccomWiseActivities.DataSource = null;
            dgAccomWiseActivities.DataBind();
        }
        ClearControls();
    }
    private void EnableNewButton()
    {
        //btnAddNew.Enabled = true;
        //btnCancel.Enabled = false;
        //btnDelete.Enabled = false;
        //btnEdit.Enabled = false;
        //btnSave.Enabled = false;
        btnDelete.Enabled = false;
        btnCancel.Visible = false;
        btnEdit.Text = "Add";
    }
    private bool ValidateValues()
    {
        if (txtActivityName.Text.Trim() == "")
        {
            lblStatus.Text = "Please enter Activity Name.";
            return false;
        }
        if (txtActivityDesc.Text.Trim() == "")
        {
            lblStatus.Text = "Please enter Activity Description.";
            return false;
        }
        if (Convert.ToInt32(ddlAccomodations.SelectedValue) <= 0)
        {
            lblStatus.Text = "Please select Accomodation Type";
            return false;
        }
        
        return true;
    }

    private void ClearControls()
    {
        txtActivityName.Text = "";
        txtActivityDesc.Text = "";    
    }

    #endregion UserDefinedFuntions
}
