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

public partial class MasterUI_CityMaster : MasterBasePage
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
        txtCityName.Text = "";
        txtCityCode.Text = "";

        btnAddNew.Enabled = false;
        btnCancel.Enabled = true;
        btnDelete.Enabled = false;
        btnEdit.Enabled = false;
        btnSave.Enabled = true;
        lblStatus.Text = "Add New action initiated";
        txtCityName.Focus();
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
        txtCityCode.Text = "";
        txtCityName.Text = "";
        hfId.Value = "";
        //SessionHandler"CityID"] = null;
        lblStatus.Text = "Action Cancelled";
    }
    protected void dgCitys_SelectedIndexChanged(object sender, EventArgs e)
    {
        int iCityID = Convert.ToInt32(dgCitys.DataKeys[dgCitys.SelectedIndex].ToString());
        hfId.Value = iCityID.ToString();
        //SessionHandler"CityID"] = iCityID;
        CityMaster oCityMaster = new CityMaster();
        CityDTO[] oCityData = oCityMaster.GetData(iCityID);
        if (oCityData.Length > 0)
        {
            txtCityCode.Text = oCityData[0].CityCode.ToString();
            txtCityName.Text = oCityData[0].CityName.ToString();
        }
        oCityMaster = null;
        oCityData = null;

        //btnAddNew.Enabled = false;
        //btnCancel.Enabled = true;
        btnCancel.Visible = true;
        btnDelete.Enabled = true;
        btnEdit.Text = "Update";
        //btnSave.Enabled = false;
        lblStatus.Text = "";
    }
    protected void dgCitys_DeleteCommand(object source, DataGridCommandEventArgs e)
    {
        int iCityID = Convert.ToInt32(dgCitys.DataKeys[e.Item.ItemIndex].ToString());
        CityMaster oCityMaster = new CityMaster();
        CityDTO oCityData = new CityDTO();
        oCityData.CityId = iCityID;
        oCityMaster.Delete(oCityData);
        txtCityName.Text = "";
        txtCityCode.Text = "";
        RefreshGrid();
        oCityData = null;
        oCityMaster = null;
    }

    #endregion ControlsEvents

    #region UserDefinedFunctions
    private void Save()
    {
        if (!base.ValidateIfCommandAllowed(Request.Url.AbsoluteUri, ENums.PageCommand.Add))
            return;

        bool bActionCompleted = false;
        CityDTO oCityData = new CityDTO();
        oCityData.CityCode = Convert.ToString(txtCityCode.Text);
        oCityData.CityName = Convert.ToString(txtCityName.Text);
        CityMaster oCityMaster = new CityMaster();
        bActionCompleted = oCityMaster.Insert(oCityData);
        if (bActionCompleted == true)
        {
            base.DisplayAlert("The record has been inserted successfully");
            txtCityName.Text = "";
            txtCityCode.Text = "";
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
        CityDTO oCityData = new CityDTO();
        oCityData.CityId = Id;
        oCityData.CityName = txtCityName.Text.ToString();
        oCityData.CityCode = txtCityCode.Text.ToString();
        CityMaster oCityMaster = new CityMaster();
        bActionCompleted = oCityMaster.Update(oCityData);
        if (bActionCompleted == true)
        {
            base.DisplayAlert("The record has been updated successfully");
            txtCityName.Text = "";
            txtCityCode.Text = "";
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
        CityMaster oCityMaster = new CityMaster();
        CityDTO oCityData = new CityDTO();
        oCityData.CityId = Id;
        /*
        * 
        * CHECK IF THE CITY WHICH IS TO BE DELETED HAS ANY ASSOCIATED RECORDS...IF YES, MOVE OUT OF THE FUNCTION ELSE PROCEED
        * IF THE OUTPUT OF sMessage IS "", THEN RECORD CAN BE DELETED, ELSE NOT
        * 
        */
        string sMessage = "";
        GF.HasRecords(Convert.ToString(Id), "city", out sMessage);
        if (sMessage != "")
        {
            base.DisplayAlert(sMessage);
            btnDelete.Enabled = true;
        }
        else
        {
            bActionCompleted = oCityMaster.Delete(oCityData);
            if (bActionCompleted == true)
            {
                sMessage = "The record has been deleted successfully.";
                base.DisplayAlert(sMessage);
                txtCityName.Text = "";
                txtCityCode.Text = "";
                //lblStatus.Text = "Deleted";
            }
            else
            {
                sMessage = "Error Occured while deletion: Please refer to the error log.";
                base.DisplayAlert(sMessage);                
            }
            //lblStatus.Text = "Error Occured while deletion: Please refer to the error log.";
        }
        RefreshGrid();
        oCityData = null;
        oCityMaster = null;
    }
    private void RefreshGrid()
    {
        CityMaster oCityMaster = new CityMaster();
        CityDTO[] oCityData = oCityMaster.GetData();
        if (oCityData != null)
        {
            if (oCityData.Length > 0)
            {
                dgCitys.DataSource = oCityData;
                dgCitys.DataBind();
            }
        }
        else
        {
            dgCitys.DataSource = null;
            dgCitys.DataBind();
        }
        txtCityCode.Text = "";
        txtCityName.Text = "";
        oCityData = null;
        oCityMaster = null;
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
        if (txtCityCode.Text.Trim() == "")
        {
            lblStatus.Text = "Please enter City Code.";
            return false;
        }
        if (txtCityName.Text.Trim() == "")
        {
            lblStatus.Text = "Please enter City Name.";
            return false;
        }
        return true;
    }
    #endregion UserDefinedFunctions

}
