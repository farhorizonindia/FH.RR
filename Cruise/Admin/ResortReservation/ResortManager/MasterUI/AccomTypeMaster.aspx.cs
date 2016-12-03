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

public partial class MasterUI_AccomTypeMaster : MasterBasePage
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
        txtAccomType.Text = "";
        txtAccomType.Focus();
        //ManageButtons(false);

        //btnAddNew.Enabled = false;
        //btnCancel.Enabled = true;
        btnEdit.Text = "Add";
        btnDelete.Enabled = false;
        //btnEdit.Enabled = false;
        //btnSave.Enabled = true;
        lblStatus.Text = "Add New action initiated";
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        if(ValidateValues() ==false)        
            return;
        Save();
        txtAccomType.Text = "";
        lblStatus.Text = "Saved";
        RefreshGrid();
        EnableNewButton();
    }
    protected void btnEdit_Click(object sender, EventArgs e)
    {
        int Id=0;
        if (ValidateValues() == false)
            return;

        int.TryParse(hfId.Value, out Id);

        if(Id==0)
        {
            Save();
            lblStatus.Text = "Inserted";
        }
        else if (Id!=0)
        {
            Update();
            lblStatus.Text = "Updated";
        }
        txtAccomType.Text = "";
        hfId.Value = "";
        RefreshGrid();
        EnableNewButton();
    }
    protected void btnDelete_Click(object sender, EventArgs e)
    {
        Delete();
        hfId.Value = "";
        RefreshGrid();
        EnableNewButton();
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        EnableNewButton();
        txtAccomType.Text = "";
        hfId.Value = "";
    }
    protected void dgAccomodationType_SelectedIndexChanged(object sender, EventArgs e)
    {
        int iAccomTypeID = Convert.ToInt32(dgAccomodationType.DataKeys[dgAccomodationType.SelectedIndex]);
        hfId.Value = iAccomTypeID.ToString();
        //SessionHandler"AccomTypeID"] = iAccomTypeID;
        AccomodationTypeMaster oAccomTypeMaster = new AccomodationTypeMaster();
        AccomTypeDTO[] oAccomTypeData = oAccomTypeMaster.GetData(iAccomTypeID);
        if (oAccomTypeData.Length > 0)
        {
            txtAccomType.Text = oAccomTypeData[0].AccomodationType;
        }
        oAccomTypeMaster = null;
        oAccomTypeData = null;

        //btnAddNew.Enabled = false;
        //btnCancel.Enabled = true;
        btnDelete.Enabled = true;
        btnEdit.Enabled = true;
        btnCancel.Visible = true;        
        btnEdit.Text = "Update";
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

        AccomodationTypeMaster oAccomTypeMaster = new AccomodationTypeMaster();
        AccomTypeDTO oAccomTypeData = MapControlsToObject();

        bActionCompleted = oAccomTypeMaster.Insert(oAccomTypeData);

        if (bActionCompleted == true)
        {
            txtAccomType.Text = "";
            base.DisplayAlert("The record has been inserted successfully");
        }
        else if (bActionCompleted == false)
        {
            base.DisplayAlert("The record has been not been inserted successfully");
            lblStatus.Text = "Error in saving.";
        }    
        
        oAccomTypeData = null;
        oAccomTypeMaster = null;
    }

    private AccomTypeDTO  MapControlsToObject()
    {
        AccomTypeDTO accomTypeDto = new AccomTypeDTO();
        int Id;
        int.TryParse(hfId.Value, out Id);
        accomTypeDto.AccomodationTypeId = Id;
        accomTypeDto.AccomodationType = txtAccomType.Text.ToString();
        return accomTypeDto;
    }

    private void Update()
    {
        bool bActionCompleted = false;

        if (!base.ValidateIfCommandAllowed(Request.Url.AbsoluteUri, ENums.PageCommand.Update))
            return;

        int Id = 0;
        int.TryParse(hfId.Value, out Id);
        if (Id == 0)
        {
            lblStatus.Text = "Please click on edit again.";
            return;
        }
        if (txtAccomType.Text.Trim() == "")
        {
            lblStatus.Text = "Please enter the Accomodation Type.";
            return;
        }
        

        AccomodationTypeMaster oAccomTypeMaster = new AccomodationTypeMaster();
        AccomTypeDTO oAccomTypeData = MapControlsToObject();
        bActionCompleted =oAccomTypeMaster.Update(oAccomTypeData);
        if (bActionCompleted == true)
        {
            txtAccomType.Text = "";
            base.DisplayAlert("The record has been updated successfully");
        }
        else if (bActionCompleted == false)
        {
            base.DisplayAlert("The record has been not been updated successfully");
            lblStatus.Text = "Error in saving.";
        }    
            
        txtAccomType.Text = "";
        lblStatus.Text = "Updated";
        oAccomTypeData = null;
        oAccomTypeMaster = null;
    }
    private void Delete()
    {
        if (base.ValidateIfCommandAllowed(Request.Url.AbsoluteUri, ENums.PageCommand.Delete))
            return;

        int Id = 0;
        int.TryParse(hfId.Value, out Id);
        if (Id == 0)
        {
            lblStatus.Text = "Please click on edit again.";
            return;
        }
        if (txtAccomType.Text.Trim() == "")
        {
            lblStatus.Text = "Please enter the Accomodation Type.";
            return;
        }

        AccomTypeDTO oAccomTypeData = MapControlsToObject();
        AccomodationTypeMaster oAccomTypeMaster = new AccomodationTypeMaster();
        /*
         * 
         * CHECK IF THE ACCOMODATION TYPE WHICH IS TO BE DELETED HAS ANY ASSOCIATED RECORDS...IF YES, MOVE OUT OF THE FUNCTION ELSE PROCEED
         * IF THE OUTPUT OF sMessage IS "", THEN RECORD CAN BE DELETED, ELSE NOT
         * 
         */
        string sMessage = "";
        GF.HasRecords(Convert.ToString(Id), "accomodationtype", out sMessage);
        if (sMessage != "")
        {
            base.DisplayAlert(sMessage);
            btnDelete.Enabled = true;
        }
        else
        {
            bool bActionCompleted = oAccomTypeMaster.Delete(oAccomTypeData);
            if (bActionCompleted == true)
            {
                base.DisplayAlert("The record has been deleted successfully");
                txtAccomType.Text = "";
                //lblStatus.Text = "Deleted";
            }
            else
            {
                base.DisplayAlert("Error Occured while deletion: Please refer to the error log.");
            }
        }
        
        oAccomTypeData = null;
        oAccomTypeMaster = null;
    }
    private void RefreshGrid()
    {
        AccomodationTypeMaster oAccomTypeMaster;
        AccomTypeDTO[] oAccomTypeData;
        oAccomTypeMaster = new AccomodationTypeMaster();
        oAccomTypeData = oAccomTypeMaster.GetData();
        dgAccomodationType.DataSource = null;
        dgAccomodationType.DataBind();
        if (oAccomTypeData != null)
        {
            if (oAccomTypeData.Length > 0)
            {
                dgAccomodationType.DataSource = oAccomTypeData;
                dgAccomodationType.DataBind();
            }
        }
        txtAccomType.Text = "";
        oAccomTypeMaster = null;
        oAccomTypeData = null;
    }
    private void EnableNewButton()
    {
        //btnAddNew.Enabled = true;
        //btnCancel.Enabled = false;
        btnDelete.Enabled = false;
        btnCancel.Visible = false;  
        btnEdit.Text = "Add";        
        //btnEdit.Enabled = false;
        //btnSave.Enabled = false;
    }
    private bool ValidateValues()
    {
        if (txtAccomType.Text.Trim() == "")
        {
            lblStatus.Text = "Please enter accomodation type.";            
            return false;
        }
        return true;
    }
    #endregion UserDefinedFunctions
    
}
