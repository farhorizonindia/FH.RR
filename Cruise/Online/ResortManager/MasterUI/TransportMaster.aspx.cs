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

public partial class MasterUI_TransportMaster : MasterBasePage
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
        txtTransport.Text = "";
        txtTransport.Focus();
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
        txtTransport.Text = "";
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
        txtTransport.Text = "";
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
        txtTransport.Text = "";
        hfId.Value = "";
    }
    protected void dgTransport_SelectedIndexChanged(object sender, EventArgs e)
    {
        int iTransportId = 0;
        int.TryParse(Convert.ToString(dgTransport.DataKeys[dgTransport.SelectedIndex]), out iTransportId);
        hfId.Value = iTransportId.ToString();
        //SessionHandler"TransportId"] = iTransportId;
        TransportMaster oTransportMaster = new TransportMaster();
        TransportDTO[] oTransportData = oTransportMaster.GetData(iTransportId);
        if (oTransportData.Length > 0)
        {
            txtTransport.Text = oTransportData[0].TransportName.ToString();
        }
        oTransportMaster = null;
        oTransportData = null;

        //btnAddNew.Enabled = false;
        //btnCancel.Enabled = true;
        btnCancel.Visible = true;
        btnDelete.Enabled = true;
        btnEdit.Text = "Update";
        //btnEdit.Enabled = true;
        //btnSave.Enabled = false;
        lblStatus.Text = "";
    }
    protected void dgTransport_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
    {
        dgTransport.CurrentPageIndex = e.NewPageIndex;
        RefreshGrid();
    }

    #endregion ControlsEvent

    #region UserDefinedFunctions
    private void Save()
    {
        if (!base.ValidateIfCommandAllowed(Request.Url.AbsoluteUri, ENums.PageCommand.Add))
        {
            DisplayAlert("You don't have rights to " + ENums.PageCommand.Add.ToString());
            return;
        }

        bool bActionCompleted = false;
        TransportDTO oTransportData = new TransportDTO();
        oTransportData.TransportName = txtTransport.Text.Trim();
        TransportMaster oTransportMaster = new TransportMaster();
        bActionCompleted = oTransportMaster.Insert(oTransportData);
        if (bActionCompleted == true)
        {
            base.DisplayAlert("The record has been inserted successfully.");
            txtTransport.Text = "";
            lblStatus.Text = "Saved";
        }
        else
            lblStatus.Text = "Error Occured while insertion: Please refer to the error log.";
        oTransportData = null;
        oTransportMaster = null;
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
        TransportDTO oTransportData = new TransportDTO();
        oTransportData.TransportId = Id;
        oTransportData.TransportName = txtTransport.Text.Trim();
        TransportMaster oTransportMaster = new TransportMaster();
        bActionCompleted = oTransportMaster.Update(oTransportData);
        if (bActionCompleted == true)
        {
            base.DisplayAlert("The record has been updated successfully.");
            txtTransport.Text = "";
            lblStatus.Text = "Updated";
        }
        else
        {
            base.DisplayAlert("Error Occured while updation: Please refer to the error log.");
            lblStatus.Text = "Error Occured while updation: Please refer to the error log.";
        }

        oTransportData = null;
        oTransportMaster = null;
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
        TransportDTO oTransportData = new TransportDTO();
        oTransportData.TransportId = Id;
        TransportMaster oTransportMaster = new TransportMaster();
        /*
        * ADDED BY VIJAY
        * CHECK IF THE Transport WHICH IS TO BE DELETED HAS ANY ASSOCIATED RECORDS...IF YES, MOVE OUT OF THE FUNCTION ELSE PROCEED
        * IF THE OUTPUT OF sMessage IS "", THEN RECORD CAN BE DELETED, ELSE NOT
        * 
        */
        string sMessage = "";
        GF.HasRecords(Convert.ToString(Id), "Transport", out sMessage);
        if (sMessage != "")
        {
            base.DisplayAlert(sMessage);
            btnDelete.Enabled = true;
        }
        else
        {
            bActionCompleted = oTransportMaster.Delete(oTransportData);

            if (bActionCompleted == true)
            {
                sMessage = "The record has been deleted successfully";
                base.DisplayAlert(sMessage);
                txtTransport.Text = "";
                //lblStatus.Text = "Deleted";
                RefreshGrid();
            }
            else
            {
                sMessage = "Error Occured while deletion: Please refer to the error log.";
                base.DisplayAlert(sMessage);
            }            
        }
        oTransportData = null;
        oTransportMaster = null;
    }
    private void RefreshGrid()
    {
        TransportMaster oTransportMaster;
        TransportDTO[] oTransportData;
        oTransportMaster = new TransportMaster();
        oTransportData = oTransportMaster.GetData();
        if (oTransportData != null)
        {
            if (oTransportData.Length > 0)
            {
                dgTransport.DataSource = oTransportData;
                dgTransport.DataBind();
            }
        }
        else
        {
            dgTransport.DataSource = null;
            dgTransport.DataBind();
        }
        txtTransport.Text = "";
        oTransportMaster = null;
        oTransportData = null;
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
        if (txtTransport.Text.Trim() == "")
        {
            lblStatus.Text = "Please enter Transport no.";
            return false;
        }
        return true;
    }

    //protected override void DisplayAlert(string message)
    //{
    //    base.DisplayAlert(btnEdit, btnEdit.GetType(), "Access Right",
    //                    string.Format("{0}');", message.Replace("'", @"\'")), true);

    //}
    #endregion UserDefinedFunctions
}
