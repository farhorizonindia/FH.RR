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
using FarHorizon.Reservations.BusinessServices.Online.DAL;
using FarHorizon.Reservations.BusinessServices.Online.BAL;
using FarHorizon.Reservations.DataBaseManager;

public partial class MasterUI_AccomMaster : MasterBasePage
{
    DALBooking dal = new DALBooking();
    BALBooking bal = new BALBooking();
    #region ControlsEvents
    protected void Page_Load(object sender, EventArgs e)
    {
        AddAddributes();
        //Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "Script", "<script language='javascript'>alert ('Record Updated Successfully') </script>");
        if (!IsPostBack)
        {
            FillAccomodationType();
            FillRegions();
            if (ddlAccomTypeId.Items.Count > 1)
                ddlAccomTypeId.SelectedIndex = 1;
            if (ddlRegion.Items.Count > 1)
                ddlRegion.SelectedIndex = 1;
            btnSearch_Click(sender, e);
            EnableNewButton();
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
        txtAccomName.Focus();
        lblStatus.Text = "Add New action initiated";
    }
    protected void btnEdit_Click(object sender, EventArgs e)
    {
        //Validation happens in the Javascript
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
        btnSearch_Click(sender, e);
        EnableNewButton();
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        ////Validation happens in the javasacript.
        //lblStatus.Text = "Save action initiated";
        //Save();
        //btnSearch_Click(sender, e);
        //EnableNewButton();
    }
    protected void btnDelete_Click(object sender, EventArgs e)
    {
        //lblStatus.Text = "Delete Action initiated";
        Delete();
        hfId.Value = "";
        btnSearch_Click(sender, e);
        EnableNewButton();
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        EnableNewButton();
        ClearControls();
        hfId.Value = "";
        lblStatus.Text = "Action Cancelled";
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        int AccomTypeId = 0;
        int RegionId = 0;
        AccomTypeId = Convert.ToInt32(ddlAccomTypeId.SelectedValue);
        RegionId = Convert.ToInt32(ddlRegion.SelectedValue);
        RefreshGrid(AccomTypeId, RegionId);
    }
    protected void dgAccomodations_SelectedIndexChanged(object sender, EventArgs e)
    {
        //DateTime dt;
        int iAccomID = Convert.ToInt32(dgAccomodations.DataKeys[dgAccomodations.SelectedIndex]);
        Session["accomid"] = iAccomID;
        hfId.Value = iAccomID.ToString();
        txtAccomName.Text = dgAccomodations.SelectedItem.Cells[1].Text.ToString().Trim();
        ddlAccomTypeId.SelectedValue = Convert.ToString(dgAccomodations.SelectedItem.Cells[2].Text).Trim();
        ddlRegion.SelectedValue = Convert.ToString(dgAccomodations.SelectedItem.Cells[4].Text).Trim();
        txtAccomInitial.Text = Convert.ToString(dgAccomodations.SelectedItem.Cells[6].Text).Trim();
        txtAccomPolicyUrl.Text= Convert.ToString(dgAccomodations.SelectedItem.Cells[7].Text).Trim();
        btnInactive.Visible = true;
        bal.accomId = iAccomID;
        DataTable dt = dal.getdetailsbyaccomid(bal);
        if (dt != null && dt.Rows.Count > 0)
        {
            if (dt.Rows[0]["ActiveStatus"].ToString() == "")
            {
                btnInactive.Text = "Inactive";
            }
            if (dt.Rows[0]["ActiveStatus"].ToString() == "True")
            {
                btnInactive.Text = "Active";
            }
        }
        //dt = DateTime.MinValue;
        //DateTime.TryParse(dgAccomodations.SelectedItem.Cells[7].Text, out dt);
        //txtSeasonStartDate.Text = dt != DateTime.MinValue ? GF.Handle19000101(dt, false) : "";

        //dt = DateTime.MinValue;
        //DateTime.TryParse(dgAccomodations.SelectedItem.Cells[8].Text, out dt);
        //txtSeasonEndDate.Text = dt != DateTime.MinValue ? GF.Handle19000101(dt, false) : "";

        //btnAddNew.Enabled = false;
        //btnCancel.Enabled = true;
        btnCancel.Visible = true;
        btnDelete.Enabled = true;
        btnEdit.Text = "Update";
        //btnEdit.Enabled = true;
        //btnSave.Enabled = false;
        lblStatus.Text = "";
    }

    #endregion ControlsEvents

    #region UserDefinedFuntions
    private void FillAccomodationType()
    {
        AccomodationTypeMaster oAccomTypeMaster = new AccomodationTypeMaster();
        AccomTypeDTO[] oAccomTypeData = oAccomTypeMaster.GetData();
        ddlAccomTypeId.Items.Clear();
        SortedList slAccomMaster = new SortedList();
        slAccomMaster.Add("0", "Choose Accomodation Type");
        if (oAccomTypeData != null)
        {
            for (int i = 0; i < oAccomTypeData.Length; i++)
            {
                slAccomMaster.Add(Convert.ToString(oAccomTypeData[i].AccomodationTypeId), Convert.ToString(oAccomTypeData[i].AccomodationType));
            }
        }
        ddlAccomTypeId.DataSource = slAccomMaster;
        ddlAccomTypeId.DataTextField = "value";
        ddlAccomTypeId.DataValueField = "key";
        ddlAccomTypeId.DataBind();
        oAccomTypeData = null;
        oAccomTypeMaster = null;
    }
    private void FillRegions()
    {
        RegionMaster oRegionMaster = new RegionMaster();
        RegionDTO[] oRegionData = oRegionMaster.GetData();
        ddlRegion.Items.Clear();
        SortedList slRegionMaster = new SortedList();
        slRegionMaster.Add("0", "Choose Region");
        for (int i = 0; i < oRegionData.Length; i++)
        {
            slRegionMaster.Add(Convert.ToString(oRegionData[i].RegionId), Convert.ToString(oRegionData[i].RegionName));
        }
        ddlRegion.DataSource = slRegionMaster;
        ddlRegion.DataTextField = "value";
        ddlRegion.DataValueField = "key";
        ddlRegion.DataBind();
    }

    private void AddAddributes()
    {
        btnDelete.Attributes.Add("onclick", "return confirm('Are you sure you want to delete this record?')");
        btnEdit.Attributes.Add("onclick", "return validateSave()");
    }

    private void Save()
    {
        if (!base.ValidateIfCommandAllowed(Request.Url.AbsoluteUri, ENums.PageCommand.Add))
            return;

        if (!ValidateValues("save"))
            return;

        AccomodationDTO oAccomData = new AccomodationDTO();
        AccomodationMaster oAccomMaster = new AccomodationMaster();
        bool bActionCompleted;

        oAccomData = MapControlsToObject();
        bActionCompleted = oAccomMaster.Insert(oAccomData);
        if (bActionCompleted == true)
        {
            base.DisplayAlert("The record has been insert successfully");
            ClearControls();
            lblStatus.Text = "Saved";
        }
        else
            lblStatus.Text = "Error Occured while saving: Please refer to the error log.";
        oAccomData = null;
        oAccomMaster = null;
    }
    private void Update()
    {
        if (!base.ValidateIfCommandAllowed(Request.Url.AbsoluteUri, ENums.PageCommand.Update))
            return;

        if (!ValidateValues("update"))
            return;

        bool bActionCompleted;

        AccomodationDTO oAccomData = null;
        AccomodationMaster oAccomMaster = new AccomodationMaster();

        oAccomData = MapControlsToObject();

        bActionCompleted = oAccomMaster.Update(oAccomData);
        if (bActionCompleted == true)
        {
            base.DisplayAlert("The record has been updated successfully");
            ClearControls();
            lblStatus.Text = "Updated Successfully";
        }
        else
            lblStatus.Text = "Error Occured while updating: Please refer to the error log.";
        oAccomData = null;
        oAccomMaster = null;

        //lblStatus.Text = "Updated";

    }
    private void Delete()
    {
        if (!base.ValidateIfCommandAllowed(Request.Url.AbsoluteUri, ENums.PageCommand.Delete))
            return;

        bool bActionCompleted;
        string sMessage = "";
        AccomodationDTO oAccomData = new AccomodationDTO();
        AccomodationMaster oAccomMaster = new AccomodationMaster();

        int Id = 0;
        int.TryParse(hfId.Value, out Id);
        if (Id == 0)
        {
            lblStatus.Text = "Please click on edit again.";
            return;
        }
        oAccomData.AccomodationId = Id;
        /*
         * 
         * CHECK IF THE ACCOMODATION WHICH IS TO BE DELETED HAS ANY ASSOCIATED RECORDS...IF YES, MOVE OUT OF THE FUNCTION ELSE PROCEED
         * IF THE OUTPUT OF sMessage IS "", THEN RECORD CAN BE DELETED, ELSE NOT
         * 
         */
        GF.HasRecords(Convert.ToString(Id), "accomodation", out sMessage);
        if (sMessage != "")
        {
            base.DisplayAlert(sMessage);
            btnDelete.Enabled = true;
        }
        else
        {
            bActionCompleted = oAccomMaster.Delete(oAccomData);
            if (bActionCompleted == true)
            {
                base.DisplayAlert("The record has been deleted successfully.");
                ClearControls();
                //lblStatus.Text = "Deleted";
            }
            else
            {
                base.DisplayAlert("Error Occured while deletion: Please refer to the error log.");
            }
            //lblStatus.Text = "Error Occured while updating: Please refer to the error log.";

        }
        oAccomMaster = null;
        oAccomData = null;
    }
    private bool ValidateValues(string type)
    {
        int Id = 0;
        int.TryParse(hfId.Value, out Id);
        if (Id == 0 && type.ToLower()!="save")
        {
            lblStatus.Text = "Please click on edit again.";
            return false;
        }
        if (txtAccomName.Text.Trim() == "")
        {
            lblStatus.Text = "Please enter the Accomodation.";
            return false;
        }
        return true;
    }

    private AccomodationDTO MapControlsToObject()
    {
        AccomodationDTO oAccomData = new AccomodationDTO();
        int id = 0;
        int.TryParse(hfId.Value, out id);
        oAccomData.AccomodationId = id;
        oAccomData.AccomodationName = txtAccomName.Text.ToString();
        oAccomData.AccomodationTypeId = Convert.ToInt32(ddlAccomTypeId.SelectedValue.ToString());
        oAccomData.RegionId = Convert.ToInt32(ddlRegion.SelectedValue.ToString());
        oAccomData.AccomInitial = txtAccomInitial.Text.ToString();
        oAccomData.AccomPolicyUrl = txtAccomPolicyUrl.Text.ToString();
        return oAccomData;
    }

    private void RefreshGrid(int AccomodationTypeId, int RegionId)
    {
        AccomodationMaster oAccomMaster = new AccomodationMaster();
        AccomodationDTO[] oAccomData = oAccomMaster.GetData1(RegionId, AccomodationTypeId, 0);
        if (oAccomData != null)
        {
            if (oAccomData.Length > 0)
            {
                dgAccomodations.DataSource = oAccomData;
                dgAccomodations.DataBind();
            }
        }
        else
        {
            dgAccomodations.DataSource = null;
            dgAccomodations.DataBind();
        }
        ClearControls();
    }

    private void EnableNewButton()
    {
        btnDelete.Enabled = false;
        btnCancel.Visible = false;
        btnEdit.Text = "Add";
    }

    private void ClearControls()
    {
        txtAccomName.Text = "";
        txtAccomInitial.Text = "";
        txtAccomPolicyUrl.Text = "";
    }

    #endregion UserDefinedFuntions


    protected void btnInactive_Click(object sender, EventArgs e)
    {
        AccomodationMaster oAccomMaster = new AccomodationMaster();
        AccomodationDTO oAccomData = new AccomodationDTO();


        int accomid = 0;
        if (Session["accomid"] != null)
        {
            accomid = Convert.ToInt32(Session["accomid"].ToString());
            oAccomData.AccomodationId = accomid;
        }


        if (btnInactive.Text == "Inactive")
        {
            bool n = updateStatus(oAccomData);
            if (n == true)
            {
                lblStatus.Text = "Successfully Inctive";
                lblStatus.ForeColor = System.Drawing.Color.Green; 
                btnInactive.Text = "Active";
            }
        }
        else
        {
            bool n = updateStatus(oAccomData);
            if (n == true)
            {
                lblStatus.Text = "Successfully Active";
                lblStatus.ForeColor = System.Drawing.Color.Green;
                btnInactive.Text = "Inactive";
            }
        }

        //ScriptManager.RegisterStartupScript(this, this.GetType(), "showPopup", "if (!confirm('Do you want to continue?')) return false;", true);


    }
    public bool updateStatus(AccomodationDTO oAccomData)
    {
        string sProcName;
        DatabaseManager oDB;
        try
        {
            oDB = new DatabaseManager();

            sProcName = "up_Upd_Activate";
            oDB.DbCmd = oDB.GetStoredProcCommand(sProcName);
            oDB.DbDatabase.AddInParameter(oDB.DbCmd, "@iAccomId", DbType.Int32, oAccomData.AccomodationId);
            oDB.ExecuteNonQuery(oDB.DbCmd);
        }
        catch (Exception exp)
        {
            GF.LogError("clsAccomodationMaster.updateStatus", exp.Message.ToString());
            oDB = null;
            return false;
        }
        finally
        {
            oDB = null;
        }
        return true;
    }
}
