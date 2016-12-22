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

public partial class MasterUI_MealPlanMaster : MasterBasePage
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
        ClearControls();
        btnAddNew.Enabled = false;
        btnCancel.Enabled = true;
        btnDelete.Enabled = false;
        btnEdit.Enabled = false;
        btnSave.Enabled = true;
        lblStatus.Text = "Add New action initiated";
        txtMealPlanName.Focus();
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
        txtMealPlanCode.Text = "";
        txtMealPlanDesc.Text = "";
        txtMealPlanName.Text = "";
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
        //SessionHandler"MealPlanID"] = null;
        hfId.Value = "";
        lblStatus.Text = "Action Cancelled";
    }
    protected void dgMealPlans_SelectedIndexChanged(object sender, EventArgs e)
    {
        int iMealPlanID = 0;
        int.TryParse(Convert.ToString(dgMealPlans.DataKeys[dgMealPlans.SelectedIndex]), out iMealPlanID);
        hfId.Value = iMealPlanID.ToString();
        //SessionHandler"MealPlanID"] = iMealPlanID;
        MealPlanMaster oMealPlanMaster = new MealPlanMaster();
        MealPlanDTO oMealPlanData = oMealPlanMaster.GetMealDetails(iMealPlanID);
        txtMealPlanCode.Text = oMealPlanData.MealPlanCode.ToString();
        txtMealPlanName.Text = oMealPlanData.MealPlan.ToString();

        if (oMealPlanData.MealPlanDesc != null)
            txtMealPlanDesc.Text = oMealPlanData.MealPlanDesc.ToString();
        else
            txtMealPlanDesc.Text = "";

        if (oMealPlanData.WelcomeDrink == true)
            chkWelcomeDrink.Checked = true;
        else
            chkWelcomeDrink.Checked = false;

        if (oMealPlanData.Breakfast == true)
            chkBreakfast.Checked = true;
        else
            chkBreakfast.Checked = false;

        if (oMealPlanData.Lunch == true)
            chkLunch.Checked = true;
        else
            chkLunch.Checked = false;

        if (oMealPlanData.EveningSnacks == true)
            chkEveSnacks.Checked = true;
        else
            chkEveSnacks.Checked = false;

        if (oMealPlanData.Dinner == true)
            chkDinner.Checked = true;
        else
            chkDinner.Checked = false;


        oMealPlanMaster = null;
        oMealPlanData = null;
        //btnAddNew.Enabled = false;
        //btnCancel.Enabled = true;
        btnCancel.Visible = true;
        btnDelete.Enabled = true;
        btnEdit.Text = "Update";
        //btnEdit.Enabled = true;
        //btnSave.Enabled = false;
        lblStatus.Text = "";
    }
    protected void dgMealPlans_DeleteCommand(object source, DataGridCommandEventArgs e)
    {
        int iMealPlanID = Convert.ToInt32(dgMealPlans.DataKeys[e.Item.ItemIndex].ToString());
        MealPlanMaster oMealPlanMaster = new MealPlanMaster();
        MealPlanDTO oMealPlanData = new MealPlanDTO();
        oMealPlanData.MealPlanId = iMealPlanID;
        oMealPlanMaster.Delete(oMealPlanData);
        txtMealPlanName.Text = "";
        txtMealPlanCode.Text = "";
        txtMealPlanDesc.Text = "";
        RefreshGrid();
        oMealPlanData = null;
        oMealPlanMaster = null;
    }

    #endregion ControlsEvents

    #region UserDefinedFunctions
    private void Save()
    {
        if (!base.ValidateIfCommandAllowed(Request.Url.AbsoluteUri, ENums.PageCommand.Add))
            return;

        bool bActionCompleted = false;
        MealPlanDTO oMealPlanData = MapControlsToObjects();
        MealPlanMaster oMealPlanMaster = new MealPlanMaster();
        bActionCompleted = oMealPlanMaster.Insert(oMealPlanData);
        if (bActionCompleted == true)
        {
            base.DisplayAlert("The record has been inserted successfully");
            ClearControls();
            lblStatus.Text = "Saved";
        }
        else
            lblStatus.Text = "Error Occured while insertion: Please refer to the error log.";
        oMealPlanData = null;
        oMealPlanMaster = null;

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
        MealPlanDTO oMealPlanData = MapControlsToObjects();
        oMealPlanData.MealPlanId = Id;
        MealPlanMaster oMealPlanMaster = new MealPlanMaster();
        bActionCompleted = oMealPlanMaster.Update(oMealPlanData);
        if (bActionCompleted == true)
        {
            base.DisplayAlert("The record has been updated successfully");
            ClearControls();
            lblStatus.Text = "Updated";
        }
        else
            lblStatus.Text = "Error Occured while updation: Please refer to the error log.";
        oMealPlanData = null;
        oMealPlanMaster = null;

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
        MealPlanMaster oMealPlanMaster = new MealPlanMaster();
        MealPlanDTO oMealPlanData = new MealPlanDTO();
        oMealPlanData.MealPlanId = Id;
        /*
         * ADDED BY VIJAY
         * CHECK IF THE FLOOR WHICH IS TO BE DELETED HAS ANY ASSOCIATED RECORDS...IF YES, MOVE OUT OF THE FUNCTION ELSE PROCEED
         * IF THE OUTPUT OF sMessage IS "", THEN RECORD CAN BE DELETED, ELSE NOT
         * 
         */
        string sMessage = "";
        GF.HasRecords(Convert.ToString(Id), "mealplan", out sMessage);
        if (sMessage != "")
        {
            base.DisplayAlert(sMessage);
            btnDelete.Enabled = true;
        }
        else
        {
            bActionCompleted = oMealPlanMaster.Delete(oMealPlanData);
            if (bActionCompleted == true)
            {
                base.DisplayAlert("The record has been deleted successfully");
                ClearControls();
                RefreshGrid();
                //lblStatus.Text = "Deleted";
            }
            else
            {
                base.DisplayAlert("Error Occured while deletion: Please refer to the error log.");
            }
        }
        oMealPlanData = null;
        oMealPlanMaster = null;
    }
    private void ClearControls()
    {
        txtMealPlanName.Text = "";
        txtMealPlanCode.Text = "";
        txtMealPlanDesc.Text = "";
        chkWelcomeDrink.Checked = false;
        chkBreakfast.Checked = false;
        chkLunch.Checked = false;
        chkEveSnacks.Checked = false;
        chkDinner.Checked = false;
    }
    private void RefreshGrid()
    {
        MealPlanMaster oMealPlanMaster = new MealPlanMaster();
        MealPlanDTO[] oMealPlanData = oMealPlanMaster.GetMeals();
        if (oMealPlanData != null)
        {
            if (oMealPlanData.Length > 0)
            {
                dgMealPlans.DataSource = oMealPlanData;
                dgMealPlans.DataBind();
            }
        }
        else
        {
            dgMealPlans.DataSource = null;
            dgMealPlans.DataBind();
        }
        ClearControls();
        oMealPlanData = null;
        oMealPlanMaster = null;
    }
    private void EnableNewButton()
    {
        //btnAddNew.Enabled = true;
        //btnCancel.Enabled = false;
        btnDelete.Enabled = false;
        btnEdit.Text = "Add";
        //btnEdit.Enabled = false;
        //btnSave.Enabled = false;
    }
    private MealPlanDTO MapControlsToObjects()
    {
        MealPlanDTO oMealPlanData = new MealPlanDTO();
        oMealPlanData.MealPlanCode = Convert.ToString(txtMealPlanCode.Text);
        oMealPlanData.MealPlan = Convert.ToString(txtMealPlanName.Text);
        oMealPlanData.MealPlanDesc = Convert.ToString(txtMealPlanDesc.Text);

        if (chkWelcomeDrink.Checked == true)
            oMealPlanData.WelcomeDrink = true;
        else
            oMealPlanData.WelcomeDrink = false;

        if (chkBreakfast.Checked == true)
            oMealPlanData.Breakfast = true;
        else
            oMealPlanData.Breakfast = false;

        if (chkLunch.Checked == true)
            oMealPlanData.Lunch = true;
        else
            oMealPlanData.Lunch = false;

        if (chkEveSnacks.Checked == true)
            oMealPlanData.EveningSnacks = true;
        else
            oMealPlanData.EveningSnacks = false;

        if (chkDinner.Checked == true)
            oMealPlanData.Dinner = true;
        else
            oMealPlanData.Dinner = false;
        return oMealPlanData;
    }
    private bool ValidateValues()
    {
        if (txtMealPlanCode.Text.Trim() == "")
        {
            lblStatus.Text = "Please enter MealPlan Code.";
            return false;
        }
        if (txtMealPlanName.Text.Trim() == "")
        {
            lblStatus.Text = "Please enter MealPlan Name.";
            return false;
        }
        return true;
    }
    #endregion UserDefinedFunctions

}
