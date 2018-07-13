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
using FarHorizon.Reservations.BusinessServices;

using FarHorizon.Reservations.MasterServices;
using FarHorizon.Reservations.Bases;
using FarHorizon.Reservations.Bases.BasePages;
using System.IO;
using FarHorizon.Reservations.DataBaseManager;
using System.Collections.Generic;

public partial class MasterUI_RoomMaster : MasterBasePage
{
    #region Controls Functions
    protected void Page_Load(object sender, EventArgs e)
    {
        AddAttributes();
        if (!IsPostBack)
        {
            FillAccomodationType();
            FillFloors();
            FillRoomCategories();
            FillRoomType();
            FillConvertCombo();
            FillExtraBeds();
            if (SessionServices.RoomMaster_OperationMode == null || SessionServices.RoomMaster_OperationMode == string.Empty)
            {
                if (ddlAccomType.Items.Count > 1)
                    ddlAccomType.SelectedIndex = 1;
                ddlAccomType_SelectedIndexChanged(sender, e);
                if (ddlAccomodation.Items.Count > 1)
                    ddlAccomodation.SelectedIndex = 1;
                btnSearch_Click(sender, e);
                EnableNewButton();
            }
        }
        //else
        //{
        //    ClearControls();
        //}
        //EnableNewButton();
    }
    protected void btnAddNew_Click(object sender, EventArgs e)
    {
        ClearControls();
        btnAddNew.Enabled = false;
        btnCancel.Enabled = true;
        btnDelete.Enabled = false;
        btnEdit.Enabled = false;
        btnSave.Enabled = true;
        SessionServices.RoomMaster_OperationMode = "NEW";
        txtRoomNo.Focus();
        lblStatus.Text = "Add New action initiated";
    }
    protected void btnEdit_Click(object sender, EventArgs e)
    {
        if (ValidateValues() == false)
            return;
        int Id = 0;
        int.TryParse(hfAccomId.Value, out Id);
        if (Id == 0 && hfRoomNo.Value == "")
        {
            lblStatus.Text = "Add action initiated.";
            Save();
        }
        else if (Id != 0 && hfRoomNo.Value != "")
        {
            lblStatus.Text = "Update action initiated.";
            Update();
        }
        hfRoomNo.Value = "";
        hfAccomId.Value = "";

        SessionServices.RoomMaster_OperationMode = "";
        ClearControls();
        btnSearch_Click(sender, e);
        EnableNewButton();
    }


    public string rename(string fullpath)
    {
        try
        {
            int count = 1;

            string fileNameOnly = Path.GetFileNameWithoutExtension(fullpath);
            string extension = Path.GetExtension(fullpath);
            string path = Path.GetDirectoryName(fullpath);
            string newFullPath = fullpath;

            while (File.Exists(newFullPath))
            {
                string tempFileName = string.Format("{0}({1})", fileNameOnly, count++);
                newFullPath = Path.Combine(path, tempFileName + extension);
            }
            return newFullPath;
        }
        catch
        {
            return null;
        }

    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        lblStatus.Text = "Save action initiated";
        if (ValidateValues() == false)
            return;
        Save();
        SessionServices.RoomMaster_OperationMode = "";
        ClearControls();
        btnSearch_Click(sender, e);
        EnableNewButton();
    }
    protected void btnDelete_Click(object sender, EventArgs e)
    {
        SessionServices.RoomMaster_OperationMode = "";
        //lblStatus.Text = "Delete Action initiated";
        Delete();
        btnSearch_Click(sender, e);
        EnableNewButton();
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        SessionServices.RoomMaster_OperationMode = "";
        EnableNewButton();
        ClearControls();
        hfRoomNo.Value = "";
        hfAccomId.Value = "";
        //SessionHandler"AccomodationID"] = null;
        //SessionHandler"RoomNo"] = null;
        lblStatus.Text = "Action Cancelled";
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        //int AccomTypeId = 0;
        //int RoomTypeId = 0;
        int AccomodationId = 0;
        //AccomTypeId = Convert.ToInt32(ddlAccomTypeId.SelectedValue);
        //RoomTypeId = Convert.ToInt32(ddlRoomType.SelectedValue);
        AccomodationId = Convert.ToInt32(ddlAccomodation.SelectedValue);
        RefreshGrid(AccomodationId);
    }
    protected void dgRooms_SelectedIndexChanged(object sender, EventArgs e)
    {
        //dgRooms.Items[0].Cells[0].Text
        string sRoomNo = dgRooms.SelectedItem.Cells[0].Text;
        int iAccomID = 0;
        int.TryParse(Convert.ToString(dgRooms.SelectedItem.Cells[1].Text), out iAccomID);
<<<<<<< HEAD
        int iroomcatID = 0;
        int.TryParse(Convert.ToString(dgRooms.SelectedItem.Cells[2].Text), out iroomcatID);
        hfRoomNo.Value = sRoomNo;
        hfAccomId.Value = iAccomID.ToString();
        hfroomcatid.Value = iroomcatID.ToString();
        SessionServices.RoomMaster_OperationMode = "EDIT";

        RoomMaster oRoomMaster = new RoomMaster();
        RoomDTO[] oAccomRoomData = oRoomMaster.GetData(iAccomID, sRoomNo,iroomcatID);
=======
        hfRoomNo.Value = sRoomNo;
        hfAccomId.Value = iAccomID.ToString();
        SessionServices.RoomMaster_OperationMode = "EDIT";

        RoomMaster oRoomMaster = new RoomMaster();
        RoomDTO[] oAccomRoomData = oRoomMaster.GetData(iAccomID, sRoomNo);
>>>>>>> 06df147e7f6e76b3ddcb27473f8305164d96b955

        ddlAccomodation.SelectedValue = Convert.ToString(oAccomRoomData[0].AccomodationId);
        ddlFloors.SelectedValue = Convert.ToString(oAccomRoomData[0].FloorId);
        ddlRoomType.SelectedValue = Convert.ToString(oAccomRoomData[0].RoomTypeId);
        ddlRoomType_SelectedIndexChanged(sender, e);
        ddlRoomCategory.SelectedValue = Convert.ToString(oAccomRoomData[0].RoomCategoryId);
        txtRoomNo.Text = Convert.ToString(oAccomRoomData[0].RoomNo);
        txtNoOfBeds.Text = Convert.ToString(oAccomRoomData[0].No_of_Beds);
        txtDescription.Text = Convert.ToString(oAccomRoomData[0].Description);
        ddlExtraBeds.Text = Convert.ToString(oAccomRoomData[0].ExtraBeds);
        txtExtraBedRate.Text = Convert.ToString(oAccomRoomData[0].ExtraBedRate);

        // chkMainten.Checked = oAccomRoomData[0].Status==false?true:false;
        if (txtExtraBedRate.Text != "")
            txtExtraBedRate.Enabled = true;
        else
            txtExtraBedRate.Enabled = false;

        if (oAccomRoomData[0].Convertable == true)
            ddlConvert.SelectedIndex = 1;
        else
            ddlConvert.SelectedIndex = 0;
        CalcTotalNoOfBeds();
        //if (oAccomRoomData[0].ExtraBeds == true)
        //{
        //    rbYes.Checked = true;
        //    txtExtraBedRate.Text = Convert.ToString(oAccomRoomData[0].ExtraBedRate);
        //}
        //else
        //{
        //    rbNo.Checked = true;
        //    txtExtraBedRate.Text = "";
        //}

        //btnAddNew.Enabled = false;
        //btnCancel.Enabled = true;
        btnCancel.Visible = true;
        btnDelete.Enabled = true;
        btnEdit.Text = "Update";
        //  chkMainten.Visible = true;
        //btnEdit.Enabled = true;
        //btnSave.Enabled = false;
        lblStatus.Text = "";
    }
    protected void dgRooms_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
    {
        dgRooms.CurrentPageIndex = e.NewPageIndex;
        int AccomodationId = 0;
        AccomodationId = Convert.ToInt32(ddlAccomodation.SelectedValue);
        RefreshGrid(AccomodationId);
        //RefreshGrid();
    }
    protected void ddlAccomType_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlAccomType.SelectedValue != null)
        {
            dgRooms.DataSource = null;
            dgRooms.DataBind();
            FillAccomodations(Convert.ToInt32(ddlAccomType.SelectedValue));
        }

        //btnAddNew.Enabled = false;
        //btnCancel.Enabled = true;
        //btnDelete.Enabled = true;
        //btnEdit.Enabled = true;
        //btnSave.Enabled = false;
        //lblStatus.Text = "";    
    }
    protected void ddlRoomType_SelectedIndexChanged(object sender, EventArgs e)
    {
        string[] sDefNoOfBeds;
        sDefNoOfBeds = ddlRoomType.SelectedItem.Text.Split('-');
        if (sDefNoOfBeds.Length > 1)
            txtDefaultNoOfBeds.Text = sDefNoOfBeds[1].Trim();
        else
            txtDefaultNoOfBeds.Text = "0";

        lblConvert.Visible = true;
        ddlConvert.Visible = true;
        lblConvert.Text = "";
        if (sDefNoOfBeds.Length >= 1)
        {
            /*
             * 
             * BELOW LINES HAVE BEEN COMMENTED BY VIJAY AFTER THE USER REQUIREMENT CHANGED
             * 
             * 
             */
            //if (string.Compare(sDefNoOfBeds[0].Trim(), "Double", true) == 0)
            //    lblConvert.Text = "Convertable to Twin:";
            if (string.Compare(sDefNoOfBeds[0].Trim(), "Twin", true) == 0)
                lblConvert.Text = "Convertable to Double:";
            else
            {
                lblConvert.Visible = false;
                ddlConvert.Visible = false;
                ddlConvert.SelectedIndex = 0;
            }
        }
        CalcTotalNoOfBeds();
    }
    protected void ddlExtraBeds_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlExtraBeds.Text == "0" || ddlExtraBeds.Text == "Choose")
        {
            txtExtraBedRate.Text = "";
            txtExtraBedRate.Enabled = false;
        }
        else
            txtExtraBedRate.Enabled = true;

        CalcTotalNoOfBeds();
    }
    protected void ddlRoomCategory_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    //protected void rbYes_CheckedChanged(object sender, EventArgs e)
    //{
    //    txtExtraBedRate.Enabled = true;
    //}
    //protected void r0_CheckedChanged(object sender, EventArgs e)
    //{
    //    txtExtraBedRate.Enabled = false;
    //}

    #endregion Controls Functions

    #region UserDefined Functions
    private void AddAttributes()
    {
        btnEdit.Attributes.Add("onclick", "return validateSave();");
        btnDelete.Attributes.Add("onclick", "return confirm('Are you sure you want to delete this record?')");
        txtExtraBedRate.Attributes.Add("onchange", "return allowNumberWithDecimal();");
        txtExtraBedRate.Attributes.Add("onkeyup", "return allowNumberWithDecimal();");
        txtExtraBedRate.Attributes.Add("onkeypress", "return allowNumberWithDecimal();");
        //ddlRoomType.Attributes.Add("onchange", "testddlChange();");
    }
    private void FillAccomodationType()
    {
        AccomodationTypeMaster oAccomTypeMaster = new AccomodationTypeMaster();
        AccomTypeDTO[] oAccomTypeData = oAccomTypeMaster.GetData();
        if (oAccomTypeData.Length > 0)
        {
            SortedList slAccomTypes = new SortedList();
            slAccomTypes.Add("0", "Choose Accomodation Type");
            for (int i = 0; i < oAccomTypeData.Length; i++)
            {
                slAccomTypes.Add(Convert.ToString(oAccomTypeData[i].AccomodationTypeId), Convert.ToString(oAccomTypeData[i].AccomodationType));
            }
            ddlAccomType.DataSource = slAccomTypes;
            ddlAccomType.DataTextField = "value";
            ddlAccomType.DataValueField = "key";
            ddlAccomType.DataBind();
        }
    }
    private void FillFloors()
    {
        FloorMaster oFloorMaster = new FloorMaster();
        FloorDTO[] oFloorData = oFloorMaster.GetData();
        if (oFloorData.Length > 0)
        {
            //SortedList slFloors = new SortedList();
            //slFloors.Add("0", "Choose Floor");

            ListItem l = null;
            for (int i = 0; i < oFloorData.Length; i++)
            {
                l = new ListItem();
                l.Text = Convert.ToString(oFloorData[i].Floor);
                l.Value = Convert.ToString(oFloorData[i].FloorId);
                ddlFloors.Items.Insert(i, l);
                //slFloors.Add(Convert.ToString(oFloorData[i].FloorId), Convert.ToString(oFloorData[i].Floor));
            }
            //ddlFloors.DataSource = slFloors;
            //ddlFloors.DataTextField = "value";
            //ddlFloors.DataValueField = "key";
            //ddlFloors.DataBind();
        }
    }
    public AccomodationDTO[] GetData(int RegionId, int AccomodationTypeId, int AccomodationId)
    {
        DataSet ds;
        AccomodationDTO[] AccomData;
        AccomData = null;
        ds = null;
        string sProcName;
        DatabaseManager oDB;
        try
        {
            oDB = new DatabaseManager();

            sProcName = "up_Get_Accomodations";
            oDB.DbCmd = oDB.GetStoredProcCommand(sProcName);
            oDB.DbDatabase.AddInParameter(oDB.DbCmd, "@iAccomTypeId", DbType.Int32, AccomodationTypeId);
            oDB.DbDatabase.AddInParameter(oDB.DbCmd, "@iAccomId", DbType.Int32, AccomodationId);
            oDB.DbDatabase.AddInParameter(oDB.DbCmd, "@iRegionId", DbType.Int32, RegionId);
            ds = oDB.ExecuteDataSet(oDB.DbCmd);
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                AccomData = new AccomodationDTO[ds.Tables[0].Rows.Count];
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    AccomData[i] = new AccomodationDTO();
                    AccomData[i].AccomodationTypeId = Convert.ToInt32(ds.Tables[0].Rows[i][0]);
                    AccomData[i].AccomodationType = Convert.ToString(ds.Tables[0].Rows[i][1]);
                    AccomData[i].AccomodationId = Convert.ToInt32(ds.Tables[0].Rows[i][2]);
                    AccomData[i].AccomodationName = Convert.ToString(ds.Tables[0].Rows[i][3]);
                    AccomData[i].RegionId = Convert.ToInt32(ds.Tables[0].Rows[i][4]);
                    AccomData[i].Region = Convert.ToString(ds.Tables[0].Rows[i][5]);
                    AccomData[i].AccomInitial = Convert.ToString(ds.Tables[0].Rows[i][6]);
                    //AccomData[i].AccomodationSeasonList = GetAccomodationSeasonDates(AccomData[i].AccomodationId);
                }
            }
        }
        catch (Exception exp)
        {
            GF.LogError("clsAccomodationMaster.Update", exp.Message.ToString());
            oDB = null;
        }
        finally
        {
            oDB = null;
        }
        return AccomData;
    }

    private List<AccomodationSeasonDTO> GetAccomodationSeasonDates(int accomodationId)
    {
        throw new NotImplementedException();
    }

    private void FillAccomodations(int iAccomTypeID)
    {
        try
        {
            AccomodationMaster oAccomMaster = new AccomodationMaster();
            AccomodationDTO[] oAccomData = GetData(0, iAccomTypeID, 0);
            if (oAccomData.Length > 0 && oAccomData != null)
            {
                SortedList slAccomMaster = new SortedList();
                slAccomMaster.Add("0", "Choose Accomodation");
                for (int i = 0; i < oAccomData.Length; i++)
                {
                    slAccomMaster.Add(Convert.ToString(oAccomData[i].AccomodationId), Convert.ToString(oAccomData[i].AccomodationName));
                }
                ddlAccomodation.DataSource = slAccomMaster;
                ddlAccomodation.DataTextField = "value";
                ddlAccomodation.DataValueField = "key";
                ddlAccomodation.DataBind();
            }
           
        }
        catch

        {
            SortedList slAccomMaster1 = new SortedList();
           
            slAccomMaster1.Remove("0");
            slAccomMaster1.Add("0", "Choose Accomodation");
           
            ddlAccomodation.DataSource = slAccomMaster1;
            ddlAccomodation.DataTextField = "value";
            ddlAccomodation.DataValueField = "key";
            ddlAccomodation.DataBind();
        }
    }
    private void FillRoomType()
    {
        RoomTypeMaster oRoomTypeMaster = new RoomTypeMaster();
        RoomTypeDTO[] oRoomTypeData = oRoomTypeMaster.GetData();

        if (oRoomTypeData.Length > 0)
        {
            ListItem l = null;
            l = new ListItem();
            l.Value = "0";
            l.Text = "Choose Room Type";
            ddlRoomType.Items.Insert(0, l);
            for (int i = 0; i < oRoomTypeData.Length; i++)
            {
                l = new ListItem();
                l.Text = oRoomTypeData[i].RoomType.Trim() + " - " + oRoomTypeData[i].DefaultNoOfBeds.ToString();
                l.Value = oRoomTypeData[i].RoomTypeId.ToString();
                ddlRoomType.Items.Insert(i + 1, l);
            }
        }
    }
    private void FillConvertCombo()
    {
        ddlConvert.Items.Insert(0, "No");
        ddlConvert.Items.Insert(1, "Yes");
    }
    private void CalcTotalNoOfBeds()
    {
        int iTotalBeds = 0, iBeds = 0;
        if (ddlExtraBeds.Text != "-1")
        {
            int.TryParse(ddlExtraBeds.Text, out iBeds);
            iTotalBeds += iBeds;
        }

        int.TryParse(txtDefaultNoOfBeds.Text, out iBeds);
        iTotalBeds += iBeds;

        txtNoOfBeds.Text = iTotalBeds.ToString();
    }
    private void FillRoomCategories()
    {
        RoomCategoryMaster oRoomCategoryMaster = new RoomCategoryMaster();
        RoomCategoryDTO[] oRoomCategoryData = oRoomCategoryMaster.GetData();
        if (oRoomCategoryData != null)
        {
            if (oRoomCategoryData.Length > 0)
            {
                SortedList slRoomCategoryMaster = new SortedList();
                slRoomCategoryMaster.Add("0", "Choose Room Category");
                for (int i = 0; i < oRoomCategoryData.Length; i++)
                {
                    slRoomCategoryMaster.Add(Convert.ToString(oRoomCategoryData[i].RoomCategoryId), Convert.ToString(oRoomCategoryData[i].RoomCategory));
                }
                ddlRoomCategory.DataSource = slRoomCategoryMaster;
                ddlRoomCategory.DataTextField = "value";
                ddlRoomCategory.DataValueField = "key";
                ddlRoomCategory.DataBind();
            }
        }
    }
    private void FillExtraBeds()
    {
        ListItem l = null;
        l = new ListItem("Choose", "-1");
        ddlExtraBeds.Items.Insert(0, l);

        for (int i = 0; i < 5; i++)
        {
            l = new ListItem(i.ToString(), i.ToString());
            ddlExtraBeds.Items.Insert(i + 1, l);
        }
        ddlExtraBeds.SelectedIndex = 0;
    }
    private void Save()
    {
        if (!base.ValidateIfCommandAllowed(Request.Url.AbsoluteUri, ENums.PageCommand.Add))
            return;

        bool bActionCompleted = false;
        RoomDTO oAccomRoomData = new RoomDTO();
        RoomMaster oRoomMaster = new RoomMaster();
        int iExtraBeds;
        oAccomRoomData.RoomNo = Convert.ToString(txtRoomNo.Text);
        oAccomRoomData.AccomodationId = Convert.ToInt32(ddlAccomodation.SelectedItem.Value.ToString());
        oAccomRoomData.FloorId = Convert.ToInt32(ddlFloors.SelectedItem.Value.ToString());
        oAccomRoomData.Description = txtDescription.Text.ToString();
        if (ddlExtraBeds.Text != "Choose")
        {
            int.TryParse(ddlExtraBeds.Text, out iExtraBeds);
            oAccomRoomData.ExtraBeds = iExtraBeds;
        }
        if (txtExtraBedRate.Text.Trim() != "")
            oAccomRoomData.ExtraBedRate = Convert.ToDouble(txtExtraBedRate.Text.ToString());// != "" ? txtExtraBedRate.Text.ToString() : "";
        oAccomRoomData.No_of_Beds = Convert.ToInt32(txtNoOfBeds.Text.ToString());
        oAccomRoomData.RoomTypeId = Convert.ToInt32(ddlRoomType.SelectedItem.Value.ToString());
        oAccomRoomData.RoomCategoryId = Convert.ToInt32(ddlRoomCategory.SelectedItem.Value.ToString());
        //oRoomData.Sequence = 1;
        //oRoomData.FloorId = 1;
        //oRoomData.TelExtnNo = "12345";     

        bActionCompleted = oRoomMaster.Insert(oAccomRoomData);
        if (bActionCompleted == true)
        {
            base.DisplayAlert("The record has been inserted successfully");
            ClearControls();
            RefreshGrid(Convert.ToInt32(ddlAccomodation.SelectedItem.Value.ToString()));
            lblStatus.Text = "Saved";
        }
        else
            lblStatus.Text = "Error Occured while insertion: Please refer to the error log.";

        oAccomRoomData = null;
        oRoomMaster = null;

    }
    private void Update()
    {
        if (!base.ValidateIfCommandAllowed(Request.Url.AbsoluteUri, ENums.PageCommand.Add))
            return;

        int iExtraBeds = 0;
        if (ValidateValues() == false)
            return;
        bool bActionCompleted = false;
        int Id = 0; string sRoomNo = "";
        int.TryParse(hfAccomId.Value, out Id);
        sRoomNo = hfRoomNo.Value;
        if (Id == 0 || sRoomNo == "")
        {
            lblStatus.Text = "Please click on edit button again.";
            return;
        }
        RoomDTO oAccomRoomData = new RoomDTO();
        oAccomRoomData.RoomNo = Convert.ToString(txtRoomNo.Text);
        oAccomRoomData.AccomodationId = Id;
        oAccomRoomData.FloorId = Convert.ToInt32(ddlFloors.SelectedItem.Value.ToString());
        oAccomRoomData.Description = txtDescription.Text.ToString();
        if (ddlExtraBeds.Text != "Choose")
        {
            int.TryParse(ddlExtraBeds.Text, out iExtraBeds);
            oAccomRoomData.ExtraBeds = iExtraBeds;
        }
        if (txtExtraBedRate.Text.Trim() != "")
            oAccomRoomData.ExtraBedRate = Convert.ToDouble(txtExtraBedRate.Text.ToString());// != "" ? txtExtraBedRate.Text.ToString() : "";
        oAccomRoomData.No_of_Beds = Convert.ToInt32(txtNoOfBeds.Text.ToString());
        oAccomRoomData.RoomTypeId = Convert.ToInt32(ddlRoomType.SelectedItem.Value.ToString());
        oAccomRoomData.RoomCategoryId = Convert.ToInt32(ddlRoomCategory.SelectedItem.Value.ToString());
        //  oAccomRoomData.Status = chkMainten.Checked?false:true;
        if (ddlConvert.SelectedIndex == 1)
            oAccomRoomData.Convertable = true;

        RoomMaster oRoomMaster = new RoomMaster();


        bActionCompleted = oRoomMaster.Update(oAccomRoomData);
        if (bActionCompleted == true)
        {
            base.DisplayAlert("The record has been updated successfully");
            ClearControls();
            RefreshGrid(Id);
            lblStatus.Text = "Updated";
        }
        else
            lblStatus.Text = "Error Occured while updation: Please refer to the error log.";

        oAccomRoomData = null;
        oRoomMaster = null;
    }
    private void Delete()
    {
        if (!base.ValidateIfCommandAllowed(Request.Url.AbsoluteUri, ENums.PageCommand.Add))
            return;

        if (ValidateValues() == false)
            return;
        bool bActionCompleted = false;
        int Id = 0; string sRoomNo = "";
        int.TryParse(hfAccomId.Value, out Id);
        sRoomNo = hfRoomNo.Value;
        if (Id == 0 || sRoomNo == "")
        {
            lblStatus.Text = "Please click on edit button again.";
            return;
        }
        string sMessage = "";
        RoomDTO oAccomRoomData = new RoomDTO();
        oAccomRoomData.AccomodationId = Id;
        oAccomRoomData.RoomNo = sRoomNo;
        RoomMaster oRoomMaster = new RoomMaster();
        /*
         * 
         * CHECK IF THE ROOM NO WHICH IS TO BE DELETED HAS ANY ASSOCIATED RECORDS...IF YES, MOVE OUT OF THE FUNCTION ELSE PROCEED
         * IF THE OUTPUT OF sMessage IS "", THEN RECORD CAN BE DELETED, ELSE NOT
         * 
         */

        GF.HasRecords(sRoomNo, "room", out sMessage);
        if (sMessage != "")
        {
            lblStatus.Text = sMessage;
            base.DisplayAlert(sMessage);
            btnDelete.Enabled = true;
        }
        else
        {
            bActionCompleted = oRoomMaster.Delete(oAccomRoomData);
            if (bActionCompleted == true)
            {
                base.DisplayAlert("The record has been deleted successfully");
                ClearControls();
                lblStatus.Text = "Deleted";
                RefreshGrid(Id);
            }
            else
            {
                base.DisplayAlert("Error Occured while deleted: Please refer to the error log.");
            }
        }


        oRoomMaster = null;
        oAccomRoomData = null;
    }
    private void RefreshGrid(int AccomodationId)
    {
        RoomMaster oRoomMaster = new RoomMaster();
        RoomDTO[] oAccomRoomData = null;
        if (AccomodationId != 0)
            oAccomRoomData = oRoomMaster.GetAllRooms(AccomodationId);
        if (oAccomRoomData != null)
        {
            if (oAccomRoomData.Length > 0)
            {
                dgRooms.DataSource = oAccomRoomData;
                dgRooms.DataBind();
            }
        }
        else
        {
            dgRooms.DataSource = null;
            dgRooms.DataBind();
        }
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
        if (txtRoomNo.Text.Trim() == "")
        {
            lblStatus.Text = "Please enter Room No.";
            return false;
        }
        if (ddlAccomType.SelectedValue == "0")
        {
            lblStatus.Text = "Please select Accomodation Type";
            return false;
        }
        if (ddlAccomodation.SelectedValue == "0")
        {
            lblStatus.Text = "Please select Accomodation Name";
            return false;
        }
        if (ddlRoomType.SelectedValue == "0")
        {
            lblStatus.Text = "Please select Room Type";
            return false;
        }
        if (ddlRoomCategory.SelectedValue == "0")
        {
            lblStatus.Text = "Please select Room Category";
            return false;
        }
        return true;
    }
    private void ClearControls()
    {
        txtRoomNo.Text = "";
        txtNoOfBeds.Text = "";
        txtExtraBedRate.Text = "";
        txtDescription.Text = "";
        txtDefaultNoOfBeds.Text = "";

        //ddlAccomType.SelectedIndex = 0;
        //ddlAccomodation.SelectedIndex = 0;
        ddlFloors.SelectedIndex = 0;
        ddlRoomType.SelectedIndex = 0;
        ddlRoomCategory.SelectedIndex = 0;
        ddlExtraBeds.SelectedIndex = 0;
        ddlConvert.SelectedIndex = 0;
    }
    #endregion UserDefined Functions

    protected void btnHide_Click(object sender, EventArgs e)
    {

    }

    protected void dgRooms_ItemCommand(object source, DataGridCommandEventArgs e)
    {
        //if (e.CommandName == "Select" || e.CommandArgument!="")
        //{
        string roomno = e.CommandArgument.ToString();
        // int acomid = Convert.ToInt32(e.CommandName.ToString());
        if (e.Item.Cells[0].Text != "")
        {
            int acomid = Convert.ToInt32(e.Item.Cells[1].Text);
<<<<<<< HEAD

            int roomcatid = Convert.ToInt32(e.Item.Cells[2].Text);

            RoomDTO rdto = new RoomDTO();
            rdto.RoomNo = roomno;
            rdto.AccomodationId = acomid;
            rdto.roomcategoryid = roomcatid;
=======
            RoomDTO rdto = new RoomDTO();
            rdto.RoomNo = roomno;
            rdto.AccomodationId = acomid;
>>>>>>> 06df147e7f6e76b3ddcb27473f8305164d96b955
            RoomMaster oRoomMaster = new RoomMaster();
            bool n = oRoomMaster.updatestatus(rdto);
            if (n == true)
            {
                RefreshGrid(acomid);
            }
        }
        //}

    }
}
