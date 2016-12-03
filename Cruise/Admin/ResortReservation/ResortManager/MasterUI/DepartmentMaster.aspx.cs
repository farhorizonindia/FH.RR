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

public partial class MasterUI_DepartmentMaster : MasterBasePage
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
        txtDepartmentName.Text = "";
        txtDepartmentCode.Text = "";

        btnAddNew.Enabled = false;
        btnCancel.Enabled = true;
        btnDelete.Enabled = false;
        btnEdit.Enabled = false;
        btnSave.Enabled = true;
        lblStatus.Text = "Add New action initiated";
        txtDepartmentName.Focus();
    }
    protected void btnEdit_Click(object sender, EventArgs e)
    {
        if (ValidateValues() == false)
            return;
        int Id = 0;
        int.TryParse(hfId.Value, out Id);
        if(Id==0)
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
        lblStatus.Text = "Delete Action initiated";
        Delete();
        EnableNewButton();
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        EnableNewButton();
        txtDepartmentCode.Text = "";
        txtDepartmentName.Text = "";
        hfId.Value = "";
        //SessionHandler"DepartmentID"] = null;
        lblStatus.Text = "Action Cancelled";
    }
    protected void dgDepartment_SelectedIndexChanged(object sender, EventArgs e)
    {
        int iDepartmentID = 0;
        int.TryParse(Convert.ToString(dgDepartment.DataKeys[dgDepartment.SelectedIndex]), out iDepartmentID);        
        hfId.Value = iDepartmentID.ToString();
        DepartmentMaster oDepartmentMaster = new DepartmentMaster();
        DepartmentDTO[] oDepartmentData = oDepartmentMaster.GetData(iDepartmentID);
        if (oDepartmentData.Length > 0)
        {
            txtDepartmentCode.Text = oDepartmentData[0].DepartmentCode.ToString();
            txtDepartmentName.Text = oDepartmentData[0].DepartmentName.ToString();
        }
        oDepartmentMaster = null;
        oDepartmentData = null;

        //btnAddNew.Enabled = false;
        //btnCancel.Enabled = true;
        btnCancel.Visible = true;
        btnDelete.Enabled = true;
        btnEdit.Text = "Update";
        //btnEdit.Enabled = true;
        //btnSave.Enabled = false;
        lblStatus.Text = "";
    }
    protected void dgDepartment_DeleteCommand(object source, DataGridCommandEventArgs e)
    {
        int iDepartmentID = Convert.ToInt32(dgDepartment.DataKeys[e.Item.ItemIndex].ToString());
        DepartmentMaster oDepartmentMaster = new DepartmentMaster();
        DepartmentDTO oDepartmentData = new DepartmentDTO();
        oDepartmentData.DepartmentId = iDepartmentID;
        oDepartmentMaster.Delete(oDepartmentData);
        txtDepartmentName.Text = "";
        txtDepartmentCode.Text = "";
        RefreshGrid();
        oDepartmentData = null;
        oDepartmentMaster = null;
    }
    
    #endregion ControlsEvents

    #region UserDefinedFunctions
    private void Save()
    {
        if (!base.ValidateIfCommandAllowed(Request.Url.AbsoluteUri, ENums.PageCommand.Add))
            return;

        bool bActionCompleted = false;
        DepartmentDTO oDepartmentData = new DepartmentDTO();
        oDepartmentData.DepartmentCode = Convert.ToString(txtDepartmentCode.Text);
        oDepartmentData.DepartmentName = Convert.ToString(txtDepartmentName.Text);
        DepartmentMaster oDepartmentMaster = new DepartmentMaster();
        bActionCompleted= oDepartmentMaster.Insert(oDepartmentData);
        if (bActionCompleted == true)
        {
            base.DisplayAlert("The record has been inserted successfully");
            txtDepartmentName.Text = "";
            txtDepartmentCode.Text = "";
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
        DepartmentDTO oDepartmentData = new DepartmentDTO();
        oDepartmentData.DepartmentId = Id;
        oDepartmentData.DepartmentName = txtDepartmentName.Text.ToString();
        oDepartmentData.DepartmentCode = txtDepartmentCode.Text.ToString();
        DepartmentMaster oDepartmentMaster = new DepartmentMaster();
        bActionCompleted = oDepartmentMaster.Update(oDepartmentData);
        if (bActionCompleted == true)
        {
            base.DisplayAlert("The record has been updated successfully");
            txtDepartmentCode.Text = "";
            txtDepartmentName.Text = "";
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
        DepartmentMaster oDepartmentMaster = new DepartmentMaster();
        DepartmentDTO oDepartmentData = new DepartmentDTO();        
        oDepartmentData.DepartmentId = Id;
        bActionCompleted = oDepartmentMaster.Delete(oDepartmentData);
        if (bActionCompleted == true)
        {
            base.DisplayAlert("The record has been deleted successfully");
            txtDepartmentName.Text = "";
            txtDepartmentCode.Text = "";
            lblStatus.Text = "Deleted";
        }
        else
            lblStatus.Text = "Error Occured while deletion: Please refer to the error log.";

        RefreshGrid();
        oDepartmentData = null;
        oDepartmentMaster = null;        
    }
    private void RefreshGrid()
    {
        DepartmentMaster oDepartmentMaster = new DepartmentMaster();
        DepartmentDTO[] oDepartmentData = oDepartmentMaster.GetData();
        if (oDepartmentData != null)
        {
            if (oDepartmentData.Length > 0)
            {
                dgDepartment.DataSource = oDepartmentData;
                dgDepartment.DataBind();
            }
        }
        else
        {
            dgDepartment.DataSource = null;
            dgDepartment.DataBind();
        }
        txtDepartmentCode.Text = "";
        txtDepartmentName.Text = "";
        oDepartmentData = null;
        oDepartmentMaster = null;
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
        if (txtDepartmentName.Text.Trim() == "")
        {
            lblStatus.Text = "Please enter Department Name.";
            return false;
        }
        if (txtDepartmentCode.Text.Trim() == "")
        {
            lblStatus.Text = "Please enter Department Code.";
            return false;
        }
        return true;
    }
    #endregion UserDefinedFunctions

}
