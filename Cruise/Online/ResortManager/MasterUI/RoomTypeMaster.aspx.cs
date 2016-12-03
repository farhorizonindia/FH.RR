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

public partial class MasterUI_RoomTypeMaster : MasterBasePage
{
    #region ControlsEvent
    protected void Page_Load(object sender, EventArgs e)
    {
        btnDelete.Attributes.Add("onclick", "return confirm('Are you sure you want to delete this record?')");
        if (!IsPostBack)
        {
            FillDefaultNoOfBeds();
            RefreshGrid();
        }
        AddAttributes();
        EnableNewButton();
    }
    protected void btnAddNew_Click(object sender, EventArgs e)
    {
        txtRoomType.Text = "";
        ddlDefaultNoOfBeds.SelectedIndex = 0;
        txtRoomType.Focus();
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
        txtRoomType.Text = "";
        ddlDefaultNoOfBeds.SelectedIndex = 0;
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
        txtRoomType.Text = "";
        ddlDefaultNoOfBeds.SelectedIndex = 0;
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
        hfId.Value = "";
        txtRoomType.Text = "";
        ddlDefaultNoOfBeds.SelectedIndex = 0;
    }
    protected void dgRoomType_SelectedIndexChanged(object sender, EventArgs e)
    {
        int iRoomTypeID = 0;
        int.TryParse(Convert.ToString(dgRoomType.DataKeys[dgRoomType.SelectedIndex]), out iRoomTypeID);
        hfId.Value = iRoomTypeID.ToString();
        txtRoomType.Text = dgRoomType.SelectedItem.Cells[0].Text.ToString();
        ListItem l;
        l = ddlDefaultNoOfBeds.Items.FindByText(dgRoomType.SelectedItem.Cells[1].Text.ToString());
        if (l != null)
            ddlDefaultNoOfBeds.SelectedIndex = ddlDefaultNoOfBeds.Items.IndexOf(l);

        //oRoomTypeMaster = null;
        //oRoomTypeData = null;

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
    }
    private void Save()
    {
        if (!base.ValidateIfCommandAllowed(Request.Url.AbsoluteUri, ENums.PageCommand.Add))
            return;

        bool bActionCompleted = false;
        RoomTypeDTO oRoomTypeData = new RoomTypeDTO();
        int DNOB = 0;
        oRoomTypeData.RoomType = txtRoomType.Text.ToString();
        if (ddlDefaultNoOfBeds.Text != "Choose")
        {
            int.TryParse(ddlDefaultNoOfBeds.Text, out DNOB);
            oRoomTypeData.DefaultNoOfBeds = DNOB;
        }
        else
            oRoomTypeData.DefaultNoOfBeds = 0;
        RoomTypeMaster oRoomTypeMaster = new RoomTypeMaster();
        bActionCompleted = oRoomTypeMaster.Insert(oRoomTypeData);
        if (bActionCompleted == true)
        {
            base.DisplayAlert("The record has been inserted successfully.");
            txtRoomType.Text = "";
            ddlDefaultNoOfBeds.SelectedIndex = 0;
            lblStatus.Text = "Saved";
        }
        else
            lblStatus.Text = "Error Occured while insertion: Please refer to the error log.";

        oRoomTypeData = null;
        oRoomTypeMaster = null;
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

        RoomTypeDTO oRoomTypeData = new RoomTypeDTO();
        int DNOB = 0;
        oRoomTypeData.RoomTypeId = Id;
        oRoomTypeData.RoomType = txtRoomType.Text.ToString();
        if (ddlDefaultNoOfBeds.Text != "Choose")
        {
            int.TryParse(ddlDefaultNoOfBeds.Text, out DNOB);
            oRoomTypeData.DefaultNoOfBeds = DNOB;
        }
        else
            oRoomTypeData.DefaultNoOfBeds = 0;

        RoomTypeMaster oRoomTypeMaster = new RoomTypeMaster();
        bActionCompleted = oRoomTypeMaster.Update(oRoomTypeData);
        if (bActionCompleted == true)
        {
            base.DisplayAlert("The record has been updated successfully.");
            txtRoomType.Text = "";
            ddlDefaultNoOfBeds.SelectedIndex = 0;
            lblStatus.Text = "Updated";
        }
        else
            lblStatus.Text = "Error Occured while updation: Please refer to the error log.";

        oRoomTypeData = null;
        oRoomTypeMaster = null;
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
        RoomTypeDTO oRoomTypeData = new RoomTypeDTO();
        //oRoomTypeData.RoomTypeId = Convert.ToInt32(SessionHandler"RoomTypeId"]);
        oRoomTypeData.RoomTypeId = Id;
        RoomTypeMaster oRoomTypeMaster = new RoomTypeMaster();
        /*
        * ADDED BY VIJAY
        * CHECK IF THE ROOM TYPE WHICH IS TO BE DELETED HAS ANY ASSOCIATED RECORDS...IF YES, MOVE OUT OF THE FUNCTION ELSE PROCEED
        * IF THE OUTPUT OF sMessage IS "", THEN RECORD CAN BE DELETED, ELSE NOT
        * 
        */
        string sMessage = "";
        GF.HasRecords(Convert.ToString(Id), "roomtype", out sMessage);
        if (sMessage != "")
        {
            base.DisplayAlert(sMessage);
            btnDelete.Enabled = true;
        }
        else
        {
            bActionCompleted = oRoomTypeMaster.Delete(oRoomTypeData);
            if (bActionCompleted == true)
            {
                base.DisplayAlert("The record has been deleted successfully.");
                txtRoomType.Text = "";
                ddlDefaultNoOfBeds.SelectedIndex = 0;
                lblStatus.Text = "Deleted";
                RefreshGrid();
            }
            else
            {
                base.DisplayAlert("Error Occured while deletion: Please refer to the error log.");
            }
        }
        oRoomTypeData = null;
        oRoomTypeMaster = null;
    }
    private void RefreshGrid()
    {
        RoomTypeMaster oRoomTypeMaster;
        RoomTypeDTO[] oRoomTypeData;
        oRoomTypeMaster = new RoomTypeMaster();
        oRoomTypeData = oRoomTypeMaster.GetData();
        if (oRoomTypeData != null)
        {
            if (oRoomTypeData.Length > 0)
            {
                dgRoomType.DataSource = oRoomTypeData;
                dgRoomType.DataBind();
            }
        }
        else
        {
            dgRoomType.DataSource = null;
            dgRoomType.DataBind();
        }
        txtRoomType.Text = "";
        ddlDefaultNoOfBeds.SelectedIndex = 0;
        oRoomTypeMaster = null;
        oRoomTypeData = null;
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
        if (txtRoomType.Text.Trim() == "")
        {
            lblStatus.Text = "Please enter accomodation type.";
            return false;
        }
        return true;
    }

    private void FillDefaultNoOfBeds()
    {
        ddlDefaultNoOfBeds.Items.Insert(0, "Choose");
        for (int i = 1; i <= 10; i++)
        {
            ddlDefaultNoOfBeds.Items.Insert(i, i.ToString());
        }
    }
    #endregion UserDefinedFunctions
}
