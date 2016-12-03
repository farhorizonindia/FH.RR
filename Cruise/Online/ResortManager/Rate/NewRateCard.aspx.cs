using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Data;
using System.Data.SqlClient;

using System.Web.UI.WebControls;

public partial class Rate_NewRateCard : System.Web.UI.Page
{
    BALRateCard blCard = new BALRateCard();
    DALRateCard dlcard = new DALRateCard();
    DataTable dtGetReturnedData;
    int getQueryResponse = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            this.BindOperatingDays();
            this.BindAccomType();
            this.BindRateCategorise();
            this.BindRoomCategories();
            this.BindRoomTypeGrids();
            SetInitialRow();
            //   this.BindRoomServiceGrids();
        }
    }


    private void SetInitialRow()
    {
        try
        {
            DataTable dtd = new DataTable();
            DataRow dr = null;

            dtd.Columns.Add(new DataColumn("Column1", typeof(string)));
            dtd.Columns.Add(new DataColumn("Column2", typeof(string)));

            dtd.Columns.Add(new DataColumn("Column3", typeof(string)));
            dtd.Columns.Add(new DataColumn("Column6", typeof(string)));


            dtd.Columns.Add(new DataColumn("Column4", typeof(string)));
            dtd.Columns.Add(new DataColumn("Column5", typeof(string)));
            dtd.Columns.Add(new DataColumn("Column9", typeof(string)));
            dtd.Columns.Add(new DataColumn("Column10", typeof(string)));
            dtd.Columns.Add(new DataColumn("Column11", typeof(string)));
            dr = dtd.NewRow();

            dr["Column1"] = string.Empty;
            dr["Column2"] = string.Empty;
            dr["Column3"] = string.Empty;
            dr["Column6"] = string.Empty;
            dr["Column4"] = string.Empty;
            dr["Column5"] = string.Empty;
            dr["Column9"] = string.Empty;
            dr["Column10"] = string.Empty;
            dr["Column11"] = string.Empty;
            dtd.Rows.Add(dr);


            //Store the DataTable in ViewState
            ViewState["CurrentTable"] = dtd;

            gdvMealRatesFIT.DataSource = dtd;
            gdvMealRatesFIT.DataBind();
            gdvMealRatesGIT.DataSource = dtd;
            gdvMealRatesGIT.DataBind();
        }
        catch
        {
        }
    }

    #region UserDefineControls
    private void BindOperatingDays()
    {
        DataTable dtOperatingDays = new DataTable();
        dtOperatingDays.Columns.Add("S.No.");
        dtOperatingDays.Columns.Add("Days");

        DataRow dr1 = dtOperatingDays.NewRow();
        dr1["S.No."] = "1";
        dr1["Days"] = "Monday";
        dtOperatingDays.Rows.Add(dr1);
        DataRow dr2 = dtOperatingDays.NewRow();
        dr2["S.No."] = "2";
        dr2["Days"] = "Tuesday";
        dtOperatingDays.Rows.Add(dr2);
        DataRow dr3 = dtOperatingDays.NewRow();
        dr3["S.No."] = "3";
        dr3["Days"] = "Wednesday";
        dtOperatingDays.Rows.Add(dr3);
        DataRow dr4 = dtOperatingDays.NewRow();
        dr4["S.No."] = "4";
        dr4["Days"] = "Thursday";
        dtOperatingDays.Rows.Add(dr4);
        DataRow dr5 = dtOperatingDays.NewRow();
        dr5["S.No."] = "5";
        dr5["Days"] = "Friday";
        dtOperatingDays.Rows.Add(dr5);
        DataRow dr6 = dtOperatingDays.NewRow();
        dr6["S.No."] = "6";
        dr6["Days"] = "Saturday";
        dtOperatingDays.Rows.Add(dr6);

        GridOperatingDays.DataSource = dtOperatingDays;
        GridOperatingDays.DataBind();

    }







    private void BindAccomType()
    {
        try
        {
            blCard._Action = "GetAllAcoomTypes";
            dtGetReturnedData = dlcard.BindControls(blCard);
            if (dtGetReturnedData.Rows.Count > 0)
            {
                ddlAccomType.Items.Clear();
                ddlAccomType.DataSource = dtGetReturnedData;
                ddlAccomType.DataTextField = "AccomType";
                ddlAccomType.DataValueField = "AccomTypeId";
                ddlAccomType.DataBind();
                ddlAccomType.Items.Insert(0, "-Select-");
            }
            else
            {
                ddlAccomType.Items.Clear();
                ddlAccomType.Items.Insert(0, "-No AccomType-");
            }
        }
        catch
        {
        }
    }
    private void BindRateCategorise()
    {
        try
        {
            blCard._Action = "GetAllRateCategories";
            dtGetReturnedData = dlcard.BindControls(blCard);
            if (dtGetReturnedData.Rows.Count > 0)
            {
                ddlRatecategory.DataSource = dtGetReturnedData;
                ddlRatecategory.DataTextField = "RateName";
                ddlRatecategory.DataValueField = "RateId";
                ddlRatecategory.DataBind();
                ddlRatecategory.Items.Insert(0, "-Select-");
            }
            else
            {
                ddlRatecategory.Items.Clear();
                ddlRatecategory.DataSource = null;
                ddlRatecategory.DataBind();
                ddlRatecategory.Items.Insert(0, "-No Rate Categories-");
            }
        }
        catch (Exception sqe)
        {
            ddlRatecategory.DataSource = null;
            ddlRatecategory.DataBind();
            ddlRatecategory.Items.Insert(0, "-No Rate Categories-");
        }
    }
    private void BindRoomCategories()
    {
        try
        {
            blCard._Action = "getAllRoomCategory";
            dtGetReturnedData = dlcard.BindControls(blCard);
            if (dtGetReturnedData.Rows.Count > 0)
            {
                ddlRoomCategory.DataSource = dtGetReturnedData;
                ddlRoomCategory.DataTextField = "RoomCategory";
                ddlRoomCategory.DataValueField = "RoomCategoryId";
                ddlRoomCategory.DataBind();
                ddlRoomCategory.Items.Insert(0, "-Select-");
            }
            else
            {
                ddlRoomCategory.Items.Clear();
                ddlRoomCategory.DataSource = null;
                ddlRoomCategory.DataBind();
                ddlRoomCategory.Items.Insert(0, "-No Room Category-");
            }
        }
        catch (Exception sqe)
        {
            ddlRoomCategory.DataSource = null;
            ddlRoomCategory.DataBind();
            ddlRoomCategory.Items.Insert(0, "-No Room Category-");
        }
    }
    private void BindRoomTypeGrids()
    {
        try
        {
            blCard._Action = "getAllRoomTypes";
            dtGetReturnedData = dlcard.GetRoomsTypes(blCard);
            if (dtGetReturnedData.Rows.Count > 0)
            {
                GridFITRooms.DataSource = dtGetReturnedData;
                GridFITRooms.DataBind();
                GridGITRooms.DataSource = dtGetReturnedData;
                GridGITRooms.DataBind();
            }
            else
            {
                GridFITRooms.DataSource = null;
                GridFITRooms.DataBind();
                GridGITRooms.DataSource = null;
                GridGITRooms.DataBind();
            }
        }
        catch (Exception sqe)
        {
            GridFITRooms.DataSource = null;
            GridFITRooms.DataBind();
            GridGITRooms.DataSource = null;
            GridGITRooms.DataBind();
        }
    }
    //private void BindRoomServiceGrids()
    //{
    //    try
    //    {
    //        blCard._Action = "GetServices";
    //        dtGetReturnedData = dlcard.BindControls(blCard);
    //        if (dtGetReturnedData.Rows.Count > 0)
    //        {
    //            GridFITServices.DataSource = dtGetReturnedData;
    //            GridFITServices.DataBind();
    //            GridGITServices.DataSource = dtGetReturnedData;
    //            GridGITServices.DataBind();
    //        }
    //        else
    //        {
    //            GridFITServices.DataSource = null;
    //            GridFITServices.DataBind();
    //            GridGITServices.DataSource = null;
    //            GridGITServices.DataBind();
    //        }
    //    }
    //    catch (Exception sqe)
    //    {
    //        GridFITServices.DataSource = null;
    //        GridFITServices.DataBind();
    //        GridGITServices.DataSource = null;
    //        GridGITServices.DataBind();
    //    }
    //}
    private string DecideOperatingDays()
    {
        try
        {
            string StrOperatingDays = string.Empty;
            for (int loopCounter = 0; loopCounter < GridOperatingDays.Rows.Count; loopCounter++)
            {
                CheckBox cbCheck = (CheckBox)GridOperatingDays.Rows[loopCounter].Cells[0].FindControl("cbCheck");
                string DayName = GridOperatingDays.Rows[loopCounter].Cells[2].Text;
                if (cbCheck.Checked == true)
                {
                    if (StrOperatingDays == string.Empty)
                        StrOperatingDays = DayName;
                    else
                        StrOperatingDays = StrOperatingDays + "," + DayName;
                }
            }
            return StrOperatingDays;
        }
        catch (Exception sqe)
        {
            return "Not Define";
        }
    }
    private int AddFitGitRoomRate(BALRateCard obj)
    {
        try
        {
            #region Insert RoomRate For Fit/Git
            obj._Action = "InsertFitGitRoomRate";

            for (int LoopCounter = 0; LoopCounter < GridFITRooms.Rows.Count; LoopCounter++)
            {
                try
                {
                    #region Insert Fit Room Rate
                    obj._FitOrGit = "FIT";
                    HiddenField hdfnRoomTypeIdFit = (HiddenField)GridFITRooms.Rows[LoopCounter].Cells[0].FindControl("hdnfRoomTypeId");
                    TextBox txtCorrespondingRateFIT = (TextBox)GridFITRooms.Rows[LoopCounter].Cells[1].FindControl("txtFitRate");

                    obj._RoomTypeId = Convert.ToInt32(hdfnRoomTypeIdFit.Value.ToString());
                    if (txtCorrespondingRateFIT.Text != string.Empty)
                        obj._Amt = Convert.ToDecimal(txtCorrespondingRateFIT.Text.ToString().Trim());
                    else
                        obj._Amt = 0;
                    getQueryResponse = 0;
                    getQueryResponse = dlcard.AddFitGitRoomRate(obj);
                    #endregion

                    #region insert Git Room Rate
                    obj._FitOrGit = "GIT";
                    HiddenField hdfnRoomTypeIdGit = (HiddenField)GridGITRooms.Rows[LoopCounter].Cells[0].FindControl("hdnfRoomTypeId");
                    TextBox txtCorrespondingRateGit = (TextBox)GridGITRooms.Rows[LoopCounter].Cells[1].FindControl("txtFitRate");

                    obj._RoomTypeId = Convert.ToInt32(hdfnRoomTypeIdGit.Value.ToString());
                    if (txtCorrespondingRateFIT.Text != string.Empty)
                        obj._Amt = Convert.ToDecimal(txtCorrespondingRateFIT.Text.ToString().Trim());
                    else
                        obj._Amt = 0;
                    getQueryResponse = 0;
                    getQueryResponse = dlcard.AddFitGitRoomRate(obj);




                    #endregion

                    #region Add Fit / Git Quad-ExtraBed (This Part Of Code Will Exicute  when Loop Run Last Time)
                    if (LoopCounter == GridFITRooms.Rows.Count - 1)
                    {
                        #region FitQuad
                        obj._Action = "AddQuad";
                        obj._FitOrGit = "FIT";
                        obj._Quad = Convert.ToDecimal(txtFITQuad.Text.ToString().Trim());
                        obj._ExtraBed = Convert.ToDecimal(txtFItExtraBed.Text.ToString().Trim());
                        getQueryResponse = dlcard.AddFitGitQuad(obj);
                        #endregion

                        #region Git Quad
                        obj._FitOrGit = "GIT";
                        obj._Quad = Convert.ToDecimal(txtGitQuad.Text.ToString().Trim());
                        obj._ExtraBed = Convert.ToDecimal(txtGitExtraBed.Text.ToString().Trim());
                        getQueryResponse = dlcard.AddFitGitQuad(obj);
                        #endregion

                    }
                    #endregion

                }
                catch (Exception sqe)
                {
                    continue;
                }
            }
            #endregion

            #region Insert Service For Fit/Git
            obj._Action = "InsertFitGitMealRate";

           
                try
                {
                    #region Insert Fit Service Rates
                 
                    TextBox txtWlcmDrnk = (TextBox)gdvMealRatesFIT.Rows[0].FindControl("txtWelcomeDrinkFIT");
                    TextBox txtBrkfst = (TextBox)gdvMealRatesFIT.Rows[0].FindControl("txtBreakfastFIT");
                    TextBox txtlunch = (TextBox)gdvMealRatesFIT.Rows[0].FindControl("txtLunchFIT");
                    TextBox txtDinner = (TextBox)gdvMealRatesFIT.Rows[0].FindControl("txtDinnerFIT");
                    TextBox txtEvesnacks = (TextBox)gdvMealRatesFIT.Rows[0].FindControl("txtEveSnacksFIT");
                    obj.WelcomeDrink = Convert.ToDouble(txtWlcmDrnk.Text);
                    obj.Breakfast = Convert.ToDouble(txtBrkfst.Text);
                    obj.Lunch = Convert.ToDouble(txtlunch.Text);
                    obj.Dinner = Convert.ToDouble(txtDinner.Text);
                    obj.EveSnacks = Convert.ToDouble(txtEvesnacks.Text);
                    obj._FitOrGit = "FIT";
                    
               
                    getQueryResponse = 0;
                    getQueryResponse = dlcard.AddFitGitMealRate(obj);
                    #endregion

                    #region Insert Git Service Rate
                  
                    TextBox txtWlcmDrnkGIT = (TextBox)gdvMealRatesFIT.Rows[0].FindControl("txtWelcomeDrinkGIT");
                    TextBox txtBrkfstGIT = (TextBox)gdvMealRatesFIT.Rows[0].FindControl("txtBreakfastGIT");
                    TextBox txtlunchGIT = (TextBox)gdvMealRatesFIT.Rows[0].FindControl("txtLunchGIT");
                    TextBox txtDinnerGIT = (TextBox)gdvMealRatesFIT.Rows[0].FindControl("txtDinnerGIT");
                    TextBox txtEvesnacksGIT = (TextBox)gdvMealRatesFIT.Rows[0].FindControl("txtEveSnacksGIT");
                    obj.WelcomeDrink = Convert.ToDouble(txtWlcmDrnk.Text);
                    obj.Breakfast = Convert.ToDouble(txtBrkfst.Text);
                    obj.Lunch = Convert.ToDouble(txtlunch.Text);
                    obj.Dinner = Convert.ToDouble(txtDinner.Text);
                    obj.EveSnacks = Convert.ToDouble(txtEvesnacks.Text);
                    obj._FitOrGit = "GIT";

                    getQueryResponse = 0;
                    getQueryResponse = dlcard.AddFitGitMealRate(obj);
                    #endregion
                }
                catch (Exception sqe)
                {
                    
                }

           
            #endregion

            return 1;
        }
        catch (Exception sqe)
        {
            return 0;
        }
    }
    #endregion

    #region Control Events
    protected void ServerValidation(object source, ServerValidateEventArgs args)
    {


        //args.IsValid = (CheckBox1.Checked == true);

    }
    protected void ddlAccomType_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            blCard._Action = "GetAccom";
            blCard._AccomTypeId = Convert.ToInt32(ddlAccomType.SelectedItem.Value);
            dtGetReturnedData = dlcard.GetAccom(blCard);
            if (dtGetReturnedData.Rows.Count > 0)
            {
                ddlAccom.Items.Clear();
                ddlAccom.DataSource = dtGetReturnedData;
                ddlAccom.DataTextField = "AccomName";
                ddlAccom.DataValueField = "AccomId";
                ddlAccom.DataBind();
                ddlAccom.Items.Insert(0, "-Select-");
            }
            else
            {
                ddlAccom.Items.Clear();
                ddlAccom.DataSource = null;
                ddlAccom.DataBind();
                ddlAccom.Items.Insert(0, "-No Accom-");
            }
        }
        catch (Exception sqe)
        {
            ddlAccom.Items.Clear();
            ddlAccom.DataSource = null;
            ddlAccom.DataBind();
            ddlAccom.Items.Insert(0, "-No Accom-");
        }
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.RawUrl);
    }
    protected void btnSbmit_Click(object sender, EventArgs e)
    {
        try
        {

            if (btnSbmit.Text == "Submit")
            {
                blCard._Action = "InsertRateCardData";
                blCard._RateCardId = ddlRatecategory.SelectedItem.Text + ddlRoomCategory.SelectedItem.Value.ToString() + ddlAccom.SelectedItem.Value.ToString();
                blCard._RateCategoryId = ddlRatecategory.SelectedItem.Value.ToString().Trim();
                blCard._AccomTypeId = Convert.ToInt32(ddlAccomType.SelectedItem.Value);
                blCard._AccomId = Convert.ToInt32(ddlAccom.SelectedItem.Value);
                blCard._RoomCategoryId = Convert.ToInt32(ddlRoomCategory.SelectedItem.Value);
                blCard._ValFrom = Convert.ToDateTime(txtvalFrom.Text.ToString().Trim());
                blCard._ValTo = Convert.ToDateTime(txtvalto.Text.ToString().Trim());
                blCard._Season = txtSeason.Text.ToString().Trim();
                blCard._minNights = Convert.ToInt32(txtMinNights.Text.ToString().Trim());
                blCard._OperatingDays = DecideOperatingDays();
                if (cbAllowExtraBed.Checked == true)
                    blCard._AlloExtraBed = true;
                else
                    blCard._AlloExtraBed = false;
                if (cbWebEnabled.Checked == true)
                    blCard._WebEnabled = true;
                else
                    blCard._WebEnabled = false;
                if (cbTaxInclusive.Checked == true)
                    blCard._TaxInclusive = true;
                else
                    blCard._TaxInclusive = false;
                if (cbCommisssionable.Checked == true)
                    blCard._CommissionEnabled = true;
                else
                    blCard._CommissionEnabled = false;
                blCard._RateTypeId = 0;
                blCard._Currency = txtCurrency.Text.ToString().Trim();
                blCard._Remark = txtRemark.Text.ToString().Trim();
                blCard.GITPaxFrom = Convert.ToInt32(txtGITPAXRange.Text.Trim());
                blCard.TaxPct = Convert.ToDouble(txtTaxPer.Text.Trim());
                getQueryResponse = dlcard.AddParentRateCard(blCard);//calling DAL Function
                if (getQueryResponse > 0)
                {
                    int FitGitInsert = AddFitGitRoomRate(blCard);
                    if (FitGitInsert > 0)
                    {
                        Response.Redirect(Request.RawUrl);
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Showstatus", "javascript:alert('Rate card created successfully.')", true);

                    }
                    else
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Showstatus", "javascript:alert('Cant create rate card for these combinations. either wrong entry or combination already exists.')", true);

                }
                else
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Showstatus", "javascript:alert('Cant create rate card for these combinations. either wrong entry or combination already exists.')", true);
            }
            else
            {



            }
        }
        catch
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Showstatus", "javascript:alert('Cant create rate card for these combinations. either wrong entry or combination already exists.')", true);
        }
    }
    #endregion

   
}