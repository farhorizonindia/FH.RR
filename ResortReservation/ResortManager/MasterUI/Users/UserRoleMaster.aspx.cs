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
using FarHorizon.Reservations.Common.DataEntities.Masters;
using FarHorizon.Reservations.UserManager;
using FarHorizon.Reservations.MasterServices;
using FarHorizon.Reservations.BusinessServices;

using FarHorizon.Reservations.Bases;
using FarHorizon.Reservations.Bases.BasePages;

public partial class MasterUI_Users_UserRoleMaster : MasterBasePage
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
        txtUserRoleName.Text = "";

        btnAddNew.Enabled = false;
        btnCancel.Enabled = true;
        btnDelete.Enabled = false;
        btnEdit.Enabled = false;
        btnSave.Enabled = true;
        lblStatus.Text = "Add New action initiated";
        txtUserRoleName.Focus();
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
        txtUserRoleName.Text = "";
        hfId.Value = "";
        //SessionHandler"UserRoleID"] = null;
        lblStatus.Text = "Action Cancelled";
    }
    protected void dgUserRoles_SelectedIndexChanged(object sender, EventArgs e)
    {
        int iUserRoleID = Convert.ToInt32(dgUserRoles.DataKeys[dgUserRoles.SelectedIndex].ToString());
        hfId.Value = iUserRoleID.ToString();
        //SessionHandler"UserRoleID"] = iUserRoleID;
        UserRoleMaster oUserRoleMaster = new UserRoleMaster();
        UserRoleDTO oUserRoleData = oUserRoleMaster.GetUserRole(iUserRoleID);
        if (oUserRoleData != null)
        {
            txtUserRoleName.Text = oUserRoleData.UserRoleName.ToString();
        }
        oUserRoleMaster = null;
        oUserRoleData = null;

        //btnAddNew.Enabled = false;
        //btnCancel.Enabled = true;
        btnCancel.Visible = true;
        btnDelete.Enabled = true;
        btnEdit.Text = "Update";
        //btnSave.Enabled = false;
        lblStatus.Text = "";
    }
    protected void dgUserRoles_DeleteCommand(object source, DataGridCommandEventArgs e)
    {
        int iUserRoleID = Convert.ToInt32(dgUserRoles.DataKeys[e.Item.ItemIndex].ToString());
        UserRoleMaster oUserRoleMaster = new UserRoleMaster();
        UserRoleDTO oUserRoleData = new UserRoleDTO();
        oUserRoleData.UserRoleId = iUserRoleID;
        oUserRoleMaster.Delete(oUserRoleData);
        txtUserRoleName.Text = string.Empty;
        RefreshGrid();
        oUserRoleData = null;
        oUserRoleMaster = null;
    }

    #endregion ControlsEvents

    #region UserDefinedFunctions
    private void Save()
    {
        bool bActionCompleted = false;
        if (!base.ValidateIfCommandAllowed(Request.Url.AbsoluteUri, ENums.PageCommand.Add))
            return;

        if (!ValidateValues())
            return;

        UserRoleDTO oUserRoleData = new UserRoleDTO();
        oUserRoleData.UserRoleName = Convert.ToString(txtUserRoleName.Text);
        UserRoleMaster oUserRoleMaster = new UserRoleMaster();
        bActionCompleted = oUserRoleMaster.Insert(oUserRoleData);
        if (bActionCompleted == true)
        {
            base.DisplayAlert("The record has been inserted successfully");
            txtUserRoleName.Text = string.Empty;
        }
        else
            lblStatus.Text = "Error Occured while insertion: Please refer to the error log.";

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
        UserRoleDTO oUserRoleData = new UserRoleDTO();
        oUserRoleData.UserRoleId = Id;
        oUserRoleData.UserRoleName = txtUserRoleName.Text.ToString();
        UserRoleMaster oUserRoleMaster = new UserRoleMaster();
        bActionCompleted = oUserRoleMaster.Update(oUserRoleData);
        if (bActionCompleted == true)
        {
            base.DisplayAlert("The record has been updated successfully");
            txtUserRoleName.Text = "";
        }
        else
            lblStatus.Text = "Error Occured while updation: Please refer to the error log.";
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
        UserRoleMaster oUserRoleMaster = new UserRoleMaster();
        UserRoleDTO oUserRoleData = new UserRoleDTO();
        oUserRoleData.UserRoleId = Id;
        /*
        * 
        * CHECK IF THE UserRole WHICH IS TO BE DELETED HAS ANY ASSOCIATED RECORDS...IF YES, MOVE OUT OF THE FUNCTION ELSE PROCEED
        * IF THE OUTPUT OF sMessage IS "", THEN RECORD CAN BE DELETED, ELSE NOT
        * 
        */
        string sMessage = "";
        GF.HasRecords(Convert.ToString(Id), "UserRole", out sMessage);
        if (sMessage != "")
        {
            base.DisplayAlert(sMessage);
            btnDelete.Enabled = true;
        }
        else
        {
            bActionCompleted = oUserRoleMaster.Delete(oUserRoleData);
            if (bActionCompleted == true)
            {
                base.DisplayAlert("The record has been deleted successfully");
                txtUserRoleName.Text = string.Empty;
                hfId.Value = string.Empty;
            }
            else
            {
                base.DisplayAlert("Error Occured while deletion: Please refer to the error log.");
            }
            //lblStatus.Text = "Error Occured while deletion: Please refer to the error log.";
        }
        RefreshGrid();
        oUserRoleData = null;
        oUserRoleMaster = null;
    }

    private void RefreshGrid()
    {
        UserRoleMaster oUserRoleMaster = new UserRoleMaster();
        UserRoleDTO[] oUserRoleData = oUserRoleMaster.GetUserRoles();
        if (oUserRoleData != null && oUserRoleData.Length > 0)
        {
            dgUserRoles.DataSource = oUserRoleData;
            dgUserRoles.DataBind();
        }
        else
        {
            dgUserRoles.DataSource = null;
            dgUserRoles.DataBind();
        }
        txtUserRoleName.Text = "";
        oUserRoleData = null;
        oUserRoleMaster = null;
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
        if (txtUserRoleName.Text.Trim() == "")
        {
            lblStatus.Text = "Please enter User Role Name.";
            return false;
        }
        return true;
    }
    #endregion UserDefinedFunctions
}
