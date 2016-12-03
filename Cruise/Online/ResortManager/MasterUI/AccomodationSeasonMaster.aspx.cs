using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using FarHorizon.Reservations.Bases.BasePages;
using FarHorizon.Reservations.Common;
using FarHorizon.Reservations.Common.DataEntities.Masters;
using FarHorizon.Reservations.MasterServices;

public partial class MasterUI_AccomodationSeasonMaster : MasterBasePage
{
    #region ControlsEvents
    protected void Page_Load(object sender, EventArgs e)
    {
        AddAddributes();
        //Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "Script", "<script language='javascript'>alert ('Record Updated Successfully') </script>");
        if (!IsPostBack)
        {
            FillAccomodations();
            EnableNewButton();
        }
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
    }
    protected void btnEdit_Click(object sender, EventArgs e)
    {
        if (String.IsNullOrEmpty(hfOldSeasonStartDate.Value) || String.IsNullOrEmpty(hfOldSeasonEndDate.Value))
        {
            lblStatus.Text = "Add action initiated.";
            Save();
        }
        else
        {
            lblStatus.Text = "Update action initiated.";
            Update();
        }
        EnableNewButton();
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        ////Validation happens in the javasacript.
        //lblStatus.Text = "Save action initiated";
        //Save();
        //btnSearch_Click(sender, e);
        //EnableNewButton();
    }
    protected void btnDelete_Click(object sender, EventArgs e)
    {
        //lblStatus.Text = "Delete Action initiated";
        Delete();
        EnableNewButton();
        RefreshGrid();
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        EnableNewButton();
        ClearControls();
        lblStatus.Text = "Action Cancelled";
    }
    protected void dgAccomodationSeasons_SelectedIndexChanged(object sender, EventArgs e)
    {
        int iAccomID = Convert.ToInt32(dgAccomodationSeasons.DataKeys[dgAccomodationSeasons.SelectedIndex]);
        hfId.Value = iAccomID.ToString();

        DateTime dt;

        DateTime.TryParse(dgAccomodationSeasons.SelectedItem.Cells[1].Text.ToString().Trim(), out dt);
        hfOldSeasonStartDate.Value = txtSeasonStartDate.Text = GF.GetDD_MMM_YYYY(dt, false);

        DateTime.TryParse(dgAccomodationSeasons.SelectedItem.Cells[2].Text.ToString().Trim(), out dt);
        hfOldSeasonEndDate.Value = txtSeasonEndDate.Text = GF.GetDD_MMM_YYYY(dt, false);

        btnCancel.Visible = true;
        btnDelete.Enabled = true;
        btnEdit.Text = "Update";
        lblStatus.Text = "";
    }
    protected void tvAccomodations_SelectedNodeChanged(object sender, EventArgs e)
    {
        hfId.Value = tvAccomodations.SelectedNode.Value;
        RefreshGrid();
        txtAccomodationName.Text = tvAccomodations.SelectedNode.Text;
    }
    #endregion ControlsEvents

    #region UserDefinedFuntions
    private void FillAccomodations()
    {
        AccomodationMaster accomodationMaster = new AccomodationMaster();
        List<AccomodationDTO> accomodationsList = new List<AccomodationDTO>(accomodationMaster.GetData());
        tvAccomodations.Nodes.Clear();

        TreeNode rootNode = new TreeNode("Accomodations");
        TreeNode accomodationNode = null;

        if (accomodationsList != null)
        {
            foreach (AccomodationDTO accomodation in accomodationsList)
            {
                accomodationNode = new TreeNode(accomodation.AccomodationName, accomodation.AccomodationId.ToString());
                rootNode.ChildNodes.Add(accomodationNode);
            }
        }
        tvAccomodations.Nodes.Add(rootNode);
        tvAccomodations.ExpandAll();
    }

    private void AddAddributes()
    {
        btnDelete.Attributes.Add("onclick", "return confirm('Are you sure you want to delete this record?')");
        btnEdit.Attributes.Add("onclick", "return validateSeasonDates();");
    }

    private void Save()
    {
        if (!base.ValidateIfCommandAllowed(Request.Url.AbsoluteUri, ENums.PageCommand.Add))
            return;

        if (!ValidateValues())
            return;

        AccomodationSeasonDTO accomodationSeasonDto = new AccomodationSeasonDTO();
        AccomodationMaster oAccomMaster = new AccomodationMaster();
        bool bActionCompleted;

        accomodationSeasonDto = MapControlsToObject();
        bActionCompleted = oAccomMaster.InsertAccomodationSeason(accomodationSeasonDto);
        if (bActionCompleted == true)
        {
            base.DisplayAlert("The record has been insert successfully");
            ClearControls();
            lblStatus.Text = "Saved";
        }
        else
        {
            lblStatus.Text = "Error Occured while saving: Please refer to the error log.";
            return;
        }
        RefreshGrid();
        accomodationSeasonDto = null;
        oAccomMaster = null;
    }
    private void Update()
    {
        if (!base.ValidateIfCommandAllowed(Request.Url.AbsoluteUri, ENums.PageCommand.Update))
            return;

        if (!ValidateValues())
            return;

        bool bActionCompleted;
        AccomodationSeasonDTO accomodationOldSeason = GetSeasonOldDates();
        AccomodationSeasonDTO accomodationNewSeason = MapControlsToObject();

        AccomodationMaster oAccomMaster = new AccomodationMaster();
        bActionCompleted = oAccomMaster.UpdateAccomodationSeason(accomodationOldSeason, accomodationNewSeason);
        if (bActionCompleted == true)
        {
            base.DisplayAlert("The record has been updated successfully");
            ClearControls();
            lblStatus.Text = "Updated Successfully";
        }
        else
            lblStatus.Text = "Error Occured while updating: Please refer to the error log.";

        RefreshGrid();
        accomodationOldSeason = null;
        accomodationNewSeason = null;
        oAccomMaster = null;
    }

    private void Delete()
    {
        if (!base.ValidateIfCommandAllowed(Request.Url.AbsoluteUri, ENums.PageCommand.Delete))
            return;

        bool bActionCompleted;
        string sMessage = "";
        AccomodationSeasonDTO accomodationSeason = MapControlsToObject();
        AccomodationMaster oAccomMaster = new AccomodationMaster();

        int Id = 0;
        int.TryParse(hfId.Value, out Id);
        if (Id == 0)
        {
            lblStatus.Text = "Please click on edit again.";
            return;
        }

        GF.HasRecords(Convert.ToString(Id), "accomodation", out sMessage);
        if (sMessage != "")
        {
            base.DisplayAlert(sMessage);
            btnDelete.Enabled = true;
        }
        else
        {
            bActionCompleted = oAccomMaster.DeleteAccomodationSeason(accomodationSeason);
            if (bActionCompleted == true)
            {
                base.DisplayAlert("The record has been deleted successfully.");
                ClearControls();
            }
            else
            {
                base.DisplayAlert("Error Occured while deletion: Please refer to the error log.");
            }

        }
        oAccomMaster = null;
        accomodationSeason = null;
    }

    private bool ValidateValues()
    {
        int Id = 0;
        int.TryParse(hfId.Value, out Id);
        if (Id == 0)
        {
            lblStatus.Text = "Please click on edit again.";
            return false;
        }
        else if (txtSeasonStartDate.Text.Trim() == "")
        {
            lblStatus.Text = "Please enter the season start date.";
            return false;
        }
        else if (txtSeasonEndDate.Text.Trim() == "")
        {
            lblStatus.Text = "Please enter the season end date.";
            return false;
        }
        return true;
    }

    private AccomodationSeasonDTO GetSeasonOldDates()
    {
        AccomodationSeasonDTO accomodationOldSeason = new AccomodationSeasonDTO();
        int id = 0;
        int.TryParse(hfId.Value, out id);
        accomodationOldSeason.AccomodationId = id;

        DateTime dt;
        DateTime.TryParse(hfOldSeasonStartDate.Value, out dt);
        accomodationOldSeason.SeasonStartDate = dt;

        DateTime.TryParse(hfOldSeasonEndDate.Value, out dt);
        accomodationOldSeason.SeasonEndDate = dt;
        return accomodationOldSeason;
    }

    private AccomodationSeasonDTO MapControlsToObject()
    {
        DateTime dt;
        AccomodationSeasonDTO accomodationSeasonDto = new AccomodationSeasonDTO();
        int id = 0;
        int.TryParse(hfId.Value, out id);
        accomodationSeasonDto.AccomodationId = id;
        DateTime.TryParse(txtSeasonStartDate.Text, out dt);
        accomodationSeasonDto.SeasonStartDate = dt;

        DateTime.TryParse(txtSeasonEndDate.Text, out dt);
        accomodationSeasonDto.SeasonEndDate = dt;
        return accomodationSeasonDto;
    }

    private void RefreshGrid()
    {
        AccomodationMaster oAccomMaster = new AccomodationMaster();
        int accomodationId;
        int.TryParse(hfId.Value, out accomodationId);
        List<AccomodationSeasonDTO> accomodationSeasonList = oAccomMaster.GetAccomodationSeasonDates(accomodationId);
        if (accomodationSeasonList != null && accomodationSeasonList.Count > 0)
        {
            dgAccomodationSeasons.DataSource = accomodationSeasonList;
            dgAccomodationSeasons.DataBind();
        }
        else
        {
            dgAccomodationSeasons.DataSource = null;
            dgAccomodationSeasons.DataBind();
        }
        ClearControls();
    }

    private void EnableNewButton()
    {
        btnDelete.Enabled = false;
        btnCancel.Visible = false;
        btnEdit.Text = "Add";
    }

    private void ClearControls()
    {
        txtSeasonStartDate.Text = txtSeasonEndDate.Text = String.Empty;
        hfOldSeasonStartDate.Value = hfOldSeasonEndDate.Value = String.Empty;
    }

    #endregion UserDefinedFuntions
}
