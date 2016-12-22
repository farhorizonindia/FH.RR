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

public partial class MasterUI_RegionMaster : MasterBasePage
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
        txtRegionName.Text = "";
        txtRegionName.Focus();
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
        txtRegionName.Text = "";
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
        txtRegionName.Text = "";
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
        txtRegionName.Text = "";
        hfId.Value = "";
    }
    protected void dgRegion_SelectedIndexChanged(object sender, EventArgs e)
    {
        int iRegionID = 0;
        int.TryParse(Convert.ToString(dgRegion.DataKeys[dgRegion.SelectedIndex]), out iRegionID);
        hfId.Value = iRegionID.ToString();
        RegionMaster oRegionMaster = new RegionMaster();
        RegionDTO[] oRegionData = oRegionMaster.GetData(iRegionID);
        if (oRegionData.Length > 0)
        {
            txtRegionName.Text = oRegionData[0].RegionName;
        }
        oRegionMaster = null;
        oRegionData = null;

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
        RegionDTO oRegionData = new RegionDTO();
        oRegionData.RegionName = txtRegionName.Text.ToString();
        RegionMaster oRegionMaster = new RegionMaster();
        bActionCompleted = oRegionMaster.Insert(oRegionData);
        if (bActionCompleted == true)
        {
            base.DisplayAlert("The record has been inserted successfully");
            txtRegionName.Text = "";
            lblStatus.Text = "Saved";
        }
        else
            lblStatus.Text = "Error Occured while insertion: Please refer to the error log.";                
        
        oRegionData = null;
        oRegionMaster = null;
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
        RegionDTO oRegionData = new RegionDTO();
        oRegionData.RegionId = Id;  
        oRegionData.RegionName = txtRegionName.Text.ToString();
        RegionMaster oRegionMaster = new RegionMaster();
        bActionCompleted = oRegionMaster.Update(oRegionData);
        if (bActionCompleted == true)
        {
            base.DisplayAlert("The record has been updated successfully");
            txtRegionName.Text = "";
            lblStatus.Text = "Updated";
        }
        else
            lblStatus.Text = "Error Occured while updation: Please refer to the error log.";                
        oRegionData = null;
        oRegionMaster = null;
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

        RegionDTO oRegionData = new RegionDTO();
        oRegionData.RegionId = Id;
        RegionMaster oRegionMaster = new RegionMaster();
        /*
         * ADDED BY VIJAY
         * CHECK IF THE REGION WHICH IS TO BE DELETED HAS ANY ASSOCIATED RECORDS...IF YES, MOVE OUT OF THE FUNCTION ELSE PROCEED
         * IF THE OUTPUT OF sMessage IS "", THEN RECORD CAN BE DELETED, ELSE NOT
         * 
         */
        string sMessage = "";
        GF.HasRecords(Convert.ToString(Id), "region", out sMessage);
        if (sMessage != "")
        {
            base.DisplayAlert(sMessage);
            btnDelete.Enabled = true;
        }
        else
        {
            bActionCompleted = oRegionMaster.Delete(oRegionData);

            if (bActionCompleted == true)
            {
                base.DisplayAlert("The record has been deleted successfully");
                txtRegionName.Text = "";
                //lblStatus.Text = "Deleted";
                RefreshGrid();
            }
            else
            {
                base.DisplayAlert("Error Occured while deletion: Please refer to the error log.");
            }
        }
        oRegionData = null;
        oRegionMaster = null;
    }
    private void RefreshGrid()
    {
        RegionMaster oRegionMaster;
        RegionDTO[] oRegionData;
        oRegionMaster = new RegionMaster();
        oRegionData = oRegionMaster.GetData();
        if (oRegionData != null)
        {
            if (oRegionData.Length > 0)
            {
                dgRegion.DataSource = oRegionData;
                dgRegion.DataBind();
            }
        }
        else
        {
            dgRegion.DataSource = null;
            dgRegion.DataBind();
        }
        txtRegionName.Text = "";
        oRegionMaster = null;
        oRegionData = null;
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
        if (txtRegionName.Text.Trim() == "")
        {
            lblStatus.Text = "Please enter accomodation type.";
            return false;
        }
        return true;
    }
    #endregion UserDefinedFunctions
    
}
