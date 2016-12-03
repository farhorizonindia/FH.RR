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
        ClearControls();
        txtRoomCategory.Focus();
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
        Save();
        ClearControls();
        lblStatus.Text = "Saved";
        RefreshGrid();
        EnableNewButton();
    }
    protected void btnEdit_Click(object sender, EventArgs e)
    {        
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
        ClearControls();
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
        ClearControls();
    }
    protected void dgRoomCategory_SelectedIndexChanged(object sender, EventArgs e)
    {
        int iRoomCategoryId = 0;
        int.TryParse(Convert.ToString(dgRoomCategory.DataKeys[dgRoomCategory.SelectedIndex]),out iRoomCategoryId);
        hfId.Value = iRoomCategoryId.ToString();
        RoomCategoryMaster oRoomCategoryMaster = new RoomCategoryMaster();
        RoomCategoryDTO[] oRoomCategoryData = oRoomCategoryMaster.GetData(iRoomCategoryId);
        if (oRoomCategoryData.Length > 0)
        {
            txtRoomCategory.Text = oRoomCategoryData[0].RoomCategory;
            txtCategoryAlias.Text = oRoomCategoryData[0].CategoryAlias;
        }
        oRoomCategoryMaster = null;
        oRoomCategoryData = null;

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

    private void AddAttributes()
    {
        btnEdit.Attributes.Add("onclick", "return validateSave();");
        btnDelete.Attributes.Add("onclick", "return validateSave();");
    }

    private void ClearControls()
    {
        hfId.Value = "";
        txtRoomCategory.Text = String.Empty;
        txtCategoryAlias.Text = String.Empty;
    }

    private void Save()
    {
        if (!base.ValidateIfCommandAllowed(Request.Url.AbsoluteUri, ENums.PageCommand.Add))
            return;

        bool bActionCompleted = false;
        RoomCategoryDTO oRoomCategoryData = new RoomCategoryDTO();
        oRoomCategoryData.RoomCategory = txtRoomCategory.Text.Trim();
        oRoomCategoryData.CategoryAlias = txtCategoryAlias.Text.Trim();
        RoomCategoryMaster oRoomCategoryMaster = new RoomCategoryMaster();
        bActionCompleted = oRoomCategoryMaster.Insert(oRoomCategoryData);
        if (bActionCompleted == true)
        {
            base.DisplayAlert("The record has been inserted successfully");
            ClearControls();
            lblStatus.Text = "Saved";
        }
        else
            lblStatus.Text = "Error Occured while insertion: Please refer to the error log.";                
               
        oRoomCategoryData = null;
        oRoomCategoryMaster = null;
    }
    private void Update()
    {
        if (!base.ValidateIfCommandAllowed(Request.Url.AbsoluteUri, ENums.PageCommand.Update))
            return;

        bool bActionCompleted = false;
        int Id = 0;
        int.TryParse(hfId.Value, out Id);
        if (Id == 0)
        {
            lblStatus.Text = "Please click on edit button again.";
            return;
        }        
        RoomCategoryDTO oRoomCategoryData = new RoomCategoryDTO();
        oRoomCategoryData.RoomCategoryId = Id;
        oRoomCategoryData.RoomCategory = txtRoomCategory.Text.Trim();
        oRoomCategoryData.CategoryAlias = txtCategoryAlias.Text.Trim();
        RoomCategoryMaster oRoomCategoryMaster = new RoomCategoryMaster();
        bActionCompleted = oRoomCategoryMaster.Update(oRoomCategoryData);
        if (bActionCompleted == true)
        {
            base.DisplayAlert("The record has been updated successfully");
            ClearControls();
            lblStatus.Text = "Updated";
        }
        else
            lblStatus.Text = "Error Occured while updation: Please refer to the error log.";                
        
        oRoomCategoryData = null;
        oRoomCategoryMaster = null;
    }
    private void Delete()
    {
        if (!base.ValidateIfCommandAllowed(Request.Url.AbsoluteUri, ENums.PageCommand.Delete))
            return;

        bool bActionCompleted = false;
        int Id = 0;
        int.TryParse(hfId.Value, out Id);
        if (Id == 0)
        {
            lblStatus.Text = "Please click on edit button again.";
            return;
        }
        RoomCategoryDTO oRoomCategoryData = new RoomCategoryDTO();
       //oRoomCategoryData.RoomCategoryId = Convert.ToInt32(SessionHandler"RoomCategoryId"]);
        oRoomCategoryData.RoomCategoryId = Id;
        RoomCategoryMaster oRoomCategoryMaster = new RoomCategoryMaster();
        /*
         * ADDED BY VIJAY
         * CHECK IF THE ROOMCATEGORY WHICH IS TO BE DELETED HAS ANY ASSOCIATED RECORDS...IF YES, MOVE OUT OF THE FUNCTION ELSE PROCEED
         * IF THE OUTPUT OF sMessage IS "", THEN RECORD CAN BE DELETED, ELSE NOT
         * 
         */
        string sMessage = "";
        GF.HasRecords(Convert.ToString(Id), "roomcategory", out sMessage);
        if (sMessage != "")
        {
            base.DisplayAlert(sMessage);
            btnDelete.Enabled = true;
        }
        else
        {
            bActionCompleted = oRoomCategoryMaster.Delete(oRoomCategoryData);

            if (bActionCompleted == true)
            {
                base.DisplayAlert("The record has been deleted successfully");
                ClearControls();
                RefreshGrid();
            }
            else
            {
                base.DisplayAlert("Error Occured while deletion: Please refer to the error log.");
            }
        }
        oRoomCategoryData = null;
        oRoomCategoryMaster = null;
    }
    private void RefreshGrid()
    {
        RoomCategoryMaster oRoomCategoryMaster;
        RoomCategoryDTO[] oRoomCategoryData;
        oRoomCategoryMaster = new RoomCategoryMaster();
        oRoomCategoryData = oRoomCategoryMaster.GetData();
        if (oRoomCategoryData != null)
        {
            if (oRoomCategoryData.Length > 0)
            {
                dgRoomCategory.DataSource = oRoomCategoryData;
                dgRoomCategory.DataBind();
            }
        }
        else
        {
            dgRoomCategory.DataSource = null;
            dgRoomCategory.DataBind();
        }
        ClearControls();
        oRoomCategoryMaster = null;
        oRoomCategoryData = null;
    }
    private void EnableNewButton()
    {
        //btnAddNew.Enabled = true;
        //btnCancel.Enabled = false;
        btnCancel.Visible = true;
        btnDelete.Enabled = false;
        btnEdit.Text = "Add";
        //btnEdit.Enabled = false;
        //btnSave.Enabled = false;
    }    
    #endregion UserDefinedFunctions   
}
