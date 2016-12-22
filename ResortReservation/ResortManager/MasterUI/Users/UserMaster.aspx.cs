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
using FarHorizon.Reservations.Common.DataEntities.Masters;
using FarHorizon.Reservations.Common;
using FarHorizon.Reservations.MasterServices;

using FarHorizon.Reservations.Bases.BasePages;

public partial class MasterUI_Users_UserMaster : MasterBasePage
{
    #region ControlsEvents
    protected void Page_Load(object sender, EventArgs e)
    {
        btnDelete.Attributes.Add("onclick", "return confirm('Are you sure you want to delete this record?')");
        if (!IsPostBack)
        {
            FillUserRoles();
            RefreshGrid();
        }
        EnableNewButton();
    }

    private void FillUserRoles()
    {
        ListItem listItem;
        UserRoleMaster userRoleMaster = new UserRoleMaster();
        UserRoleDTO[] userRoleDto;

        listItem = new ListItem("Choose", "0");
        ddlUserRoleList.Items.Add(listItem);

        userRoleDto = userRoleMaster.GetUserRoles();
        for (int i = 0; i < userRoleDto.Length; i++)
        {
            listItem = new ListItem(userRoleDto[i].UserRoleName, userRoleDto[i].UserRoleId.ToString());
            ddlUserRoleList.Items.Add(listItem);
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
        txtUserId.Focus();
    }
    protected void btnEdit_Click(object sender, EventArgs e)
    {
        if (ValidateValues() == false)
            return;

        string Id = hfId.Value;
        if (string.IsNullOrEmpty(Id))
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
        ClearControls();
        hfId.Value = "";
        //SessionHandler"UserID"] = null;
        lblStatus.Text = "Action Cancelled";
    }

    protected void dgUsers_SelectedIndexChanged(object sender, EventArgs e)
    {
        string userId = Convert.ToString(dgUsers.DataKeys[dgUsers.SelectedIndex].ToString());
        hfId.Value = userId.ToString();
        //SessionHandler"UserID"] = iUserID;
        UserMaster oUserMaster = new UserMaster();
        UserDTO oUserData = oUserMaster.GetUser(userId);
        if (oUserData != null)
        {
            txtUserId.Text = oUserData.UserId;
            txtUserName.Text = oUserData.UserName;
            txtPassword.Attributes.Add("value", oUserData.Password);
            chkActive.Checked = oUserData.Active;
            ddlUserRoleList.SelectedIndex = ddlUserRoleList.Items.IndexOf(ddlUserRoleList.Items.FindByValue(oUserData.UserRoleData.UserRoleId.ToString()));
            txtUserEmailId.Text = oUserData.EmailId;
        }
        oUserMaster = null;
        oUserData = null;

        //btnAddNew.Enabled = false;
        //btnCancel.Enabled = true;
        btnCancel.Visible = true;
        btnDelete.Enabled = true;
        btnEdit.Text = "Update";
        //btnSave.Enabled = false;
        lblStatus.Text = "";
    }
    protected void dgUsers_DeleteCommand(object source, DataGridCommandEventArgs e)
    {
        string iUserID = Convert.ToString(dgUsers.DataKeys[e.Item.ItemIndex].ToString());
        UserMaster oUserMaster = new UserMaster();
        UserDTO oUserData = new UserDTO();
        oUserData.UserId = iUserID;
        oUserMaster.Delete(oUserData);

        ClearControls();

        RefreshGrid();
        oUserData = null;
        oUserMaster = null;
    }

    private void ClearControls()
    {
        txtUserName.Text = string.Empty;
        txtUserId.Text = string.Empty;
        txtPassword.Text = string.Empty;
        txtPassword.Attributes.Remove("value");
        chkActive.Checked = false;
        ddlUserRoleList.SelectedIndex = 0;
        txtUserEmailId.Text = String.Empty;
    }

    #endregion ControlsEvents

    #region UserDefinedFunctions
    private void Save()
    {
        bool bActionCompleted = false;

        if (!base.ValidateIfCommandAllowed(Request.Url.AbsoluteUri, ENums.PageCommand.Add))
            return;

        UserDTO oUserData = MapControlsToObject();

        UserMaster oUserMaster = new UserMaster();
        bActionCompleted = oUserMaster.Insert(oUserData);
        if (bActionCompleted == true)
        {
            base.DisplayAlert("The record has been inserted successfully");
            ClearControls();
        }
        else
            lblStatus.Text = "Error Occured while insertion: Please refer to the error log.";

    }

    private void Update()
    {
        if (ValidateValues() == false)
            return;

        if (!base.ValidateIfCommandAllowed(Request.Url.AbsoluteUri, ENums.PageCommand.Update))
            return;

        bool bActionCompleted = false;
        string Id = hfId.Value;
        if (string.IsNullOrEmpty(Id))
        {
            lblStatus.Text = "Please click on edit button again.";
            return;
        }
        UserDTO oUserData = MapControlsToObject();

        UserMaster oUserMaster = new UserMaster();
        bActionCompleted = oUserMaster.Update(oUserData);
        if (bActionCompleted == true)
        {
            base.DisplayAlert("The record has been updated successfully");
            ClearControls();
        }
        else
            lblStatus.Text = "Error Occured while updation: Please refer to the error log.";
    }

    private void Delete()
    {
        if (ValidateValues() == false)
            return;

        if (!base.ValidateIfCommandAllowed(Request.Url.AbsoluteUri, ENums.PageCommand.Delete))
            return;

        bool bActionCompleted = false;
        string id = string.Empty;
        id = hfId.Value;
        if (string.IsNullOrEmpty(id))
        {
            lblStatus.Text = "Please click on edit button again.";
            return;
        }
        UserMaster oUserMaster = new UserMaster();
        UserDTO oUserData = new UserDTO();
        oUserData.UserId = id;
        /*
        * 
        * CHECK IF THE User WHICH IS TO BE DELETED HAS ANY ASSOCIATED RECORDS...IF YES, MOVE OUT OF THE FUNCTION ELSE PROCEED
        * IF THE OUTPUT OF sMessage IS "", THEN RECORD CAN BE DELETED, ELSE NOT
        * 
        */
        string sMessage = "";
        GF.HasRecords(Convert.ToString(id), "User", out sMessage);
        if (sMessage != "")
        {
            base.DisplayAlert(sMessage);
            btnDelete.Enabled = true;
        }
        else
        {
            bActionCompleted = oUserMaster.Delete(oUserData);
            if (bActionCompleted == true)
            {
                base.DisplayAlert("The record has been deleted successfully");
                ClearControls();
                hfId.Value = string.Empty;
            }
            else
            {
                base.DisplayAlert("Error Occured while deletion: Please refer to the error log.");
            }
            //lblStatus.Text = "Error Occured while deletion: Please refer to the error log.";
        }
        RefreshGrid();
        oUserData = null;
        oUserMaster = null;
    }

    private void RefreshGrid()
    {
        UserMaster oUserMaster = new UserMaster();
        UserDTO[] oUserData = oUserMaster.GetUsers();
        if (oUserData != null)
        {
            if (oUserData.Length > 0)
            {
                dgUsers.DataSource = oUserData;
                dgUsers.DataBind();
            }
        }
        else
        {
            dgUsers.DataSource = null;
            dgUsers.DataBind();
        }
        txtUserName.Text = "";
        oUserData = null;
        oUserMaster = null;
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
        if (txtUserName.Text.Trim() == "")
        {
            lblStatus.Text = "Please enter User  Name.";
            return false;
        }
        if (!String.IsNullOrEmpty(txtUserEmailId.Text))
        {
            if (!GF.ValidateEmailId(txtUserEmailId.Text.Trim()))
            {
                lblStatus.Text = "Please enter correct email id.";
                return false;
            }
        }
        return true;
    }

    private UserDTO MapControlsToObject()
    {
        UserDTO oUserData = new UserDTO();
        oUserData.UserName = txtUserName.Text.Trim();
        oUserData.UserId = txtUserId.Text.Trim();
        oUserData.Password = txtPassword.Text.Trim();
        oUserData.Active = chkActive.Checked;
        oUserData.EmailId = txtUserEmailId.Text.Trim();

        UserRoleDTO userRoleDto = new UserRoleDTO();
        userRoleDto.UserRoleId = Convert.ToInt32(ddlUserRoleList.SelectedValue);
        oUserData.UserRoleData = userRoleDto;
        return oUserData;
    }

    #endregion UserDefinedFunctions
}

