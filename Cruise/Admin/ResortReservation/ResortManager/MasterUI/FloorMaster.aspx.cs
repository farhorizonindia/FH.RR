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

public partial class MasterUI_FloorMaster : MasterBasePage
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
        txtFloor.Text = "";
        txtFloor.Focus();
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
        txtFloor.Text = "";
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
        txtFloor.Text = "";
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
        txtFloor.Text = "";
        hfId.Value = "";
    }
    protected void dgFloor_SelectedIndexChanged(object sender, EventArgs e)
    {
        int iFloorId = 0;
        int.TryParse(Convert.ToString(dgFloor.DataKeys[dgFloor.SelectedIndex]), out iFloorId);
        hfId.Value = iFloorId.ToString();
        //SessionHandler"FloorId"] = iFloorId;
        FloorMaster oFloorMaster = new FloorMaster();
        FloorDTO[] oFloorData = oFloorMaster.GetData(iFloorId);
        if (oFloorData.Length > 0)
        {
            txtFloor.Text = oFloorData[0].Floor.ToString();
        }
        oFloorMaster = null;
        oFloorData = null;

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
        FloorDTO oFloorData = new FloorDTO();
        oFloorData.Floor = Convert.ToInt32(txtFloor.Text.Trim().ToString());
        FloorMaster oFloorMaster = new FloorMaster();
        bActionCompleted = oFloorMaster.Insert(oFloorData);
        if (bActionCompleted == true)
        {
            base.DisplayAlert("The record has been inserted successfully.");
            txtFloor.Text = "";
            lblStatus.Text = "Saved";
        }
        else
            lblStatus.Text = "Error Occured while insertion: Please refer to the error log.";        
        oFloorData = null;
        oFloorMaster = null;
    }
    private void Update()
    {
        if (!base.ValidateIfCommandAllowed(Request.Url.AbsoluteUri, ENums.PageCommand.Add))
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
        FloorDTO oFloorData = new FloorDTO();
        oFloorData.FloorId = Id;
        oFloorData.Floor = Convert.ToInt32(txtFloor.Text.Trim().ToString());
        FloorMaster oFloorMaster = new FloorMaster();
        bActionCompleted= oFloorMaster.Update(oFloorData);
        if (bActionCompleted == true)
        {
            base.DisplayAlert("The record has been updated successfully.");
            txtFloor.Text = "";
            lblStatus.Text = "Updated";
        }
        else
            lblStatus.Text = "Error Occured while updation: Please refer to the error log.";
        
        oFloorData = null;
        oFloorMaster = null;
    }
    private void Delete()
    {
        if (!base.ValidateIfCommandAllowed(Request.Url.AbsoluteUri, ENums.PageCommand.Add))
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
        FloorDTO oFloorData = new FloorDTO();
        oFloorData.FloorId = Id;
        FloorMaster oFloorMaster = new FloorMaster();
         /*
         * ADDED BY VIJAY
         * CHECK IF THE FLOOR WHICH IS TO BE DELETED HAS ANY ASSOCIATED RECORDS...IF YES, MOVE OUT OF THE FUNCTION ELSE PROCEED
         * IF THE OUTPUT OF sMessage IS "", THEN RECORD CAN BE DELETED, ELSE NOT
         * 
         */
        string sMessage = "";
        GF.HasRecords(Convert.ToString(Id), "floor", out sMessage);
        if (sMessage != "")
        {
            base.DisplayAlert(sMessage);
            btnDelete.Enabled = true;
        }
        else
        {
            bActionCompleted = oFloorMaster.Delete(oFloorData);

            if (bActionCompleted == true)
            {
                base.DisplayAlert("The record has been deleted successfully");
                txtFloor.Text = "";
                //lblStatus.Text = "Deleted";
                RefreshGrid();
            }
            else
            {
                base.DisplayAlert("Error Occured while deletion: Please refer to the error log.");
            }
             //   lblStatus.Text = "Error Occured while updation: Please refer to the error log.";
        }               
        oFloorData = null;
        oFloorMaster = null;
    }
    private void RefreshGrid()
    {
        FloorMaster oFloorMaster;
        FloorDTO[] oFloorData;
        oFloorMaster = new FloorMaster();
        oFloorData = oFloorMaster.GetData();
        if (oFloorData != null)
        {
            if (oFloorData.Length > 0)
            {
                dgFloor.DataSource = oFloorData;
                dgFloor.DataBind();
            }
        }
        else
        {
            dgFloor.DataSource = null;
            dgFloor.DataBind();
        }
        txtFloor.Text = "";
        oFloorMaster = null;
        oFloorData = null;
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
        int result=0;
        if (txtFloor.Text.Trim() == "")
        {
            lblStatus.Text = "Please enter floor no.";
            return false;
        }

        if (int.TryParse(txtFloor.Text.Trim(), out result) == false)
        {
            lblStatus.Text = "Floor No. entered is not numeric, please change the floor no.";
            return false;
        }
        return true;
    }
    #endregion UserDefinedFunctions
    protected void dgFloor_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
    {
        dgFloor.CurrentPageIndex = e.NewPageIndex;
        RefreshGrid();
    }
}
