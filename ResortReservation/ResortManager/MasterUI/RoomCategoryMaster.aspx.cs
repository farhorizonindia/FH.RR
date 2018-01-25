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
using System;

public partial class MasterUI_RoomCategoryMaster : MasterBasePage
{
    #region ControlsEvent
    protected void Page_Load(object sender, EventArgs e)
    {
        btnDelete.Attributes.Add("onclick", "return confirm('Are you sure you want to delete this record?')");
        if (!IsPostBack)
            RefreshGrid();
        EnableNewButton();
    }
    protected void btnAddNew_Click(object sender, EventArgs e)
    {
        txtRoomcategory.Text = "";
        txtRoomcategory.Focus();
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
        txtRoomcategory.Text = "";
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
        txtRoomcategory.Text = "";
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
        txtRoomcategory.Text = "";
        hfId.Value = "";
    }
    protected void dgNationality_SelectedIndexChanged1(object sender, EventArgs e)
    {
        int iNationalityId = 0;
        int.TryParse(Convert.ToString(dgNationality.DataKeys[dgNationality.SelectedIndex]), out iNationalityId);
        hfId.Value = iNationalityId.ToString();
        RoomCategoryMaster oNationalityMaster = new RoomCategoryMaster();
        RoomCategoryDTO[] oNationalityData = oNationalityMaster.GetData(iNationalityId);
        if (oNationalityData.Length > 0)
        {
            txtRoomcategory.Text = oNationalityData[0].RoomCategory;
            txtCategoryAlias.Text = oNationalityData[0].CategoryAlias;
            txtCategoryAlias.ReadOnly = true;
        }
        oNationalityMaster = null;
        oNationalityData = null;

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
        RoomCategoryDTO oNationalityData = new RoomCategoryDTO();
        oNationalityData.RoomCategory = txtRoomcategory.Text.ToString();
        oNationalityData.CategoryAlias = txtCategoryAlias.Text;
        RoomCategoryMaster oNationalityMaster = new RoomCategoryMaster();
        bActionCompleted = oNationalityMaster.Insert(oNationalityData);
        if (bActionCompleted == true)
        {
            base.DisplayAlert("The record has been inserted successfully");
            txtRoomcategory.Text = "";
            lblStatus.Text = "Data Saved Successfully";
            lblStatus.ForeColor = System.Drawing.Color.Green;
        }
        else
            lblStatus.Text = "Error Occured while insertion: Please refer to the error log.";
        lblStatus.ForeColor = System.Drawing.Color.Red;
        oNationalityData = null;
        oNationalityMaster = null;
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
        RoomCategoryDTO oNationalityData = new RoomCategoryDTO();
        oNationalityData.RoomCategoryId = Id;
        oNationalityData.RoomCategory = txtRoomcategory.Text.ToString();
        oNationalityData.CategoryAlias = txtCategoryAlias.Text;
        RoomCategoryMaster oNationalityMaster = new RoomCategoryMaster();
        bActionCompleted = oNationalityMaster.Update(oNationalityData);
        if (bActionCompleted == true)
        {
            base.DisplayAlert("The record has been updated successfully");
            txtRoomcategory.Text = "";
            txtCategoryAlias.ReadOnly = false;
            lblStatus.Text = "Updated";
        }
        else
            lblStatus.Text = "Error Occured while updation: Please refer to the error log.";
        oNationalityData = null;
        oNationalityMaster = null;
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
        RoomCategoryDTO oNationalityData = new RoomCategoryDTO();
        oNationalityData.RoomCategoryId = Id;
        RoomCategoryMaster oNationalityMaster = new RoomCategoryMaster();
        /*
         * ADDED BY VIJAY
         * CHECK IF THE NATIONALITY WHICH IS TO BE DELETED HAS ANY ASSOCIATED RECORDS...IF YES, MOVE OUT OF THE FUNCTION ELSE PROCEED
         * IF THE OUTPUT OF sMessage IS "", THEN RECORD CAN BE DELETED, ELSE NOT
         * 
         */
        string sMessage = "";
        GF.HasRecords(Convert.ToString(Id), "nationality", out sMessage);
        if (sMessage != "")
        {
            base.DisplayAlert(sMessage);
            btnDelete.Enabled = true;
        }
        else
        {
            bActionCompleted = oNationalityMaster.Delete(oNationalityData);
            if (bActionCompleted == true)
            {
                base.DisplayAlert("The record has been deleted successfully");
                txtRoomcategory.Text = "";
                //lblStatus.Text = "Deleted";
                RefreshGrid();
            }
            else
            {
                base.DisplayAlert("Error Occured while deletion: Please refer to the error log.");
            }
        }
        oNationalityData = null;
        oNationalityMaster = null;
    }
    private void RefreshGrid()
    {
        RoomCategoryMaster oNationalityMaster;
        RoomCategoryDTO[] oNationalityData;
        oNationalityMaster = new RoomCategoryMaster();
        oNationalityData = oNationalityMaster.GetData();
        if (oNationalityData != null)
        {
            if (oNationalityData.Length > 0)
            {
                dgNationality.DataSource = oNationalityData;
                dgNationality.DataBind();
            }
        }
        else
        {
            dgNationality.DataSource = null;
            dgNationality.DataBind();
        }
        txtRoomcategory.Text = "";
        txtCategoryAlias.Text = "";
        oNationalityMaster = null;
        oNationalityData = null;
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
        if (txtRoomcategory.Text.Trim() == "")
        {
            lblStatus.Text = "Please enter Category type.";
            return false;
        }
        return true;
    }
    #endregion UserDefinedFunctions

    protected void dgNationality_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
    {
        dgNationality.CurrentPageIndex = e.NewPageIndex;
        RefreshGrid();
    }
}