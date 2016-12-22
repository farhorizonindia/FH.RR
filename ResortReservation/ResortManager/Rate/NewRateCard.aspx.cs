using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Data;
using System.Data.SqlClient;

using System.Web.UI.WebControls;

using FarHorizon.Reservations.Bases.BasePages;

public partial class Rate_NewRateCard : MasterBasePage
{
    BALRateCard blCard = new BALRateCard();
    DALRateCard dlcard = new DALRateCard();
    DataTable dtGetReturnedData;
    int getQueryResponse = 0;
    DataView dv;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ddlCurrency.SelectedIndex = 1;
            this.BindOperatingDays();
            bindRatecards();
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

                    if (btnSbmit.Text == "Submit")
                    {
                        obj._Action = "InsertFitGitRoomRate";
                        getQueryResponse = dlcard.AddFitGitRoomRate(obj);
                    }
                    else
                    {
                        obj._Action = "UpdDetails";
                        getQueryResponse = dlcard.UpdateFitGitRoomRate(obj);
                    }
                    #endregion

                    #region insert Git Room Rate
                    obj._FitOrGit = "GIT";
                    HiddenField hdfnRoomTypeIdGit = (HiddenField)GridGITRooms.Rows[LoopCounter].Cells[0].FindControl("hdnfRoomTypeId");
                    TextBox txtCorrespondingRateGit = (TextBox)GridGITRooms.Rows[LoopCounter].Cells[1].FindControl("txtGitRate");

                    obj._RoomTypeId = Convert.ToInt32(hdfnRoomTypeIdGit.Value.ToString());
                    if (txtCorrespondingRateFIT.Text != string.Empty)
                        obj._Amt = Convert.ToDecimal(txtCorrespondingRateGit.Text.ToString().Trim());
                    else
                        obj._Amt = 0;
                    getQueryResponse = 0;
                    if (btnSbmit.Text == "Submit")
                    {
                        obj._Action = "InsertFitGitRoomRate";
                        getQueryResponse = dlcard.AddFitGitRoomRate(obj);
                    }
                    else
                    {
                        obj._Action = "UpdDetails";
                        getQueryResponse = dlcard.UpdateFitGitRoomRate(obj);
                    }




                    #endregion

                    #region Add Fit / Git Quad-ExtraBed (This Part Of Code Will Exicute  when Loop Run Last Time)
                    if (LoopCounter == GridFITRooms.Rows.Count - 1)
                    {
                        #region FitQuad

                        obj._FitOrGit = "FIT";
                        obj._Quad = Convert.ToDecimal(txtFITQuad.Text.ToString().Trim());
                        obj._ExtraBed = Convert.ToDecimal(txtFItExtraBed.Text.ToString().Trim());

                        if (btnSbmit.Text == "Submit")
                        {
                            obj._Action = "AddQuad";
                            getQueryResponse = dlcard.AddFitGitQuad(obj);
                        }
                        else
                        {
                            obj._Action = "updQuadExtra";
                            getQueryResponse = dlcard.updateFitGitQuad(obj);
                        }

                        #endregion

                        #region Git Quad
                        obj._FitOrGit = "GIT";
                        obj._Quad = Convert.ToDecimal(txtGitQuad.Text.ToString().Trim());
                        obj._ExtraBed = Convert.ToDecimal(txtGitExtraBed.Text.ToString().Trim());
                        if (btnSbmit.Text == "Submit")
                        {
                            obj._Action = "AddQuad";
                            getQueryResponse = dlcard.AddFitGitQuad(obj);
                        }
                        else
                        {
                            obj._Action = "updQuadExtra";
                            getQueryResponse = dlcard.updateFitGitQuad(obj);
                        }
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

                if (btnSbmit.Text == "Submit")
                {
                    obj._Action = "InsertFitGitMealRate";

                    getQueryResponse = dlcard.AddFitGitMealRate(obj);
                }
                else
                {
                    obj._Action = "updMeal";
                    getQueryResponse = dlcard.UpdateFitGitMealRate(obj);
                }
                #endregion

                #region Insert Git Service Rate

                TextBox txtWlcmDrnkGIT = (TextBox)gdvMealRatesGIT.Rows[0].FindControl("txtWelcomeDrinkGIT");
                TextBox txtBrkfstGIT = (TextBox)gdvMealRatesGIT.Rows[0].FindControl("txtBreakfastGIT");
                TextBox txtlunchGIT = (TextBox)gdvMealRatesGIT.Rows[0].FindControl("txtLunchGIT");
                TextBox txtDinnerGIT = (TextBox)gdvMealRatesGIT.Rows[0].FindControl("txtDinnerGIT");
                TextBox txtEvesnacksGIT = (TextBox)gdvMealRatesGIT.Rows[0].FindControl("txtEveSnacksGIT");
                obj.WelcomeDrink = Convert.ToDouble(txtWlcmDrnkGIT.Text);
                obj.Breakfast = Convert.ToDouble(txtBrkfstGIT.Text);
                obj.Lunch = Convert.ToDouble(txtlunchGIT.Text);
                obj.Dinner = Convert.ToDouble(txtDinnerGIT.Text);
                obj.EveSnacks = Convert.ToDouble(txtEvesnacksGIT.Text);
                obj._FitOrGit = "GIT";

                getQueryResponse = 0;
                if (btnSbmit.Text == "Submit")
                {
                    obj._Action = "InsertFitGitMealRate";

                    getQueryResponse = dlcard.AddFitGitMealRate(obj);
                }
                else
                {
                    obj._Action = "updMeal";
                    getQueryResponse = dlcard.UpdateFitGitMealRate(obj);
                }
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
        bindaccomodations(Convert.ToInt32(ddlAccomType.SelectedValue));
    }

    public void bindaccomodations(int accomtypeid)
    {
        try
        {
            blCard._Action = "GetAccom";
            blCard._AccomTypeId = accomtypeid;
            DataTable dts = new DataTable();
            dts = dlcard.GetAccom(blCard);
            if (dts.Rows.Count > 0)
            {
                ddlAccom.Items.Clear();
                ddlAccom.DataSource = dts;
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
            blCard._Currency = ddlCurrency.SelectedValue.Trim();
            blCard._Remark = txtRemark.Text.ToString().Trim();
            blCard.GITPaxFrom = Convert.ToInt32(txtGITPAXRange.Text.Trim());
            blCard.TaxPct = Convert.ToDouble(txtTaxPer.Text.Trim());

            blCard.Description = txtRateDesc.Text;
            if (btnSbmit.Text == "Submit")
            {
                blCard._RateCardId = ddlRatecategory.SelectedItem.Text + ddlRoomCategory.SelectedItem.Value.ToString() + ddlAccom.SelectedItem.Value.ToString();
                blCard._Action = "InsertRateCardData";

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

                blCard._Action = "UpdMaster";
                blCard._RateCardId = hdnRateCardId.Value.ToString();
                getQueryResponse = dlcard.UpdateParentRateCard(blCard);//calling DAL Function
                if (getQueryResponse > 0)
                {
                    int FitGitInsert = AddFitGitRoomRate(blCard);
                    if (FitGitInsert > 0)
                    {
                        Response.Redirect(Request.RawUrl);
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Showstatus", "javascript:alert('Rate card created successfully.')", true);
                        btnSbmit.Text = "Submit";

                    }
                    else
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Showstatus", "javascript:alert('Cant create rate card for these combinations. either wrong entry or combination already exists.')", true);

                }
                else
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Showstatus", "javascript:alert('Cant create rate card for these combinations. either wrong entry or combination already exists.')", true);

            }
        }
        catch
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Showstatus", "javascript:alert('Cant create rate card for these combinations. either wrong entry or combination already exists.')", true);
        }
    }
    #endregion

    public void bindRatecards()
    {
        try
        {

            blCard._Action = "GetallRateCards";
            dtGetReturnedData = new DataTable();
            dtGetReturnedData = dlcard.getAllRatecards(blCard);
            if (dtGetReturnedData != null)
            {
                gdvRateCards.DataSource = dtGetReturnedData;
                gdvRateCards.DataBind();
                ViewState["RateCards"] = dtGetReturnedData;
            }

        }


        catch
        {
        }
    }


    protected void gdvRateCards_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            if (e.CommandName == "Select")
            {
                LinkButton lnk = (LinkButton)e.CommandSource;
                GridViewRow grow = (GridViewRow)lnk.NamingContainer;
                int index = grow.RowIndex;
                string RatecardId = gdvRateCards.DataKeys[index].Value.ToString();
                hdnRateCardId.Value = RatecardId;
                blCard._Action = "GetRateCard";
                blCard._RateCardId = RatecardId;
                dtGetReturnedData = new DataTable();


                dtGetReturnedData = dlcard.getRatecardbyId(blCard);


                if (dtGetReturnedData != null)
                {
                   // ddlCurrency.SelectedValue = dtGetReturnedData.Rows[0]["Currency"].ToString();
                    txtGITPAXRange.Text = dtGetReturnedData.Rows[0]["GITPaxFrom"].ToString();
                    txtMinNights.Text = dtGetReturnedData.Rows[0]["MinNights"].ToString();
                    txtRateType.Text = dtGetReturnedData.Rows[0]["RateTypeId"].ToString();
                    txtRemark.Text = dtGetReturnedData.Rows[0]["Remark"].ToString();
                    txtSeason.Text = dtGetReturnedData.Rows[0]["Season"].ToString();
                    txtTaxPer.Text = dtGetReturnedData.Rows[0]["TaxPct"].ToString() == "" ? "0" : dtGetReturnedData.Rows[0]["TaxPct"].ToString();

                    txtvalFrom.Text = Convert.ToDateTime(dtGetReturnedData.Rows[0]["ValFrom"]).ToString("MM/dd/yyyy");
                    txtvalto.Text = Convert.ToDateTime(dtGetReturnedData.Rows[0]["ValTo"]).ToString("MM/dd/yyyy");
                    cbAllowExtraBed.Checked = Convert.ToBoolean(dtGetReturnedData.Rows[0]["AllowExtraBed"].ToString());
                    cbCommisssionable.Checked = Convert.ToBoolean(dtGetReturnedData.Rows[0]["CommissionEnable"].ToString());
                    cbTaxInclusive.Checked = Convert.ToBoolean(dtGetReturnedData.Rows[0]["TaxInclusive"].ToString());
                    cbWebEnabled.Checked = Convert.ToBoolean(dtGetReturnedData.Rows[0]["webEnabled"].ToString());

                    ddlAccomType.SelectedValue = dtGetReturnedData.Rows[0]["AccomTypeId"].ToString();
                    bindaccomodations(Convert.ToInt32(dtGetReturnedData.Rows[0]["AccomTypeId"].ToString()));
                    ddlAccom.SelectedValue = dtGetReturnedData.Rows[0]["AccomId"].ToString();
                    bindroomcat(Convert.ToInt32(dtGetReturnedData.Rows[0]["AccomId"].ToString()));

                    ddlRoomCategory.SelectedValue = dtGetReturnedData.Rows[0]["RoomCateId"].ToString();
                    ddlRatecategory.SelectedValue = dtGetReturnedData.Rows[0]["RatecatId"].ToString();
                    txtRateDesc.Text = dtGetReturnedData.Rows[0]["Description"].ToString();

                    string[] arr;
                    arr = dtGetReturnedData.Rows[0]["OperatingDays"].ToString().Split(',');
                    for (int j = 0; j < GridOperatingDays.Rows.Count; j++)
                    {
                        if (arr.Contains(GridOperatingDays.Rows[j].Cells[2].Text))
                        {
                            CheckBox chk = (CheckBox)GridOperatingDays.Rows[j].FindControl("cbCheck");
                            chk.Checked = true;

                        }
                        else
                        {
                            CheckBox chk = (CheckBox)GridOperatingDays.Rows[j].FindControl("cbCheck");
                            chk.Checked = false;
                        }
                    }



                    btnSbmit.Text = "Update";

                }

                blCard._Action = "getRateCardDetails";
                dtGetReturnedData = new DataTable();


                dtGetReturnedData = dlcard.getRatecardbyId(blCard);

                if (dtGetReturnedData != null)
                {


                    for (int k = 0; k < GridFITRooms.Rows.Count; k++)
                    {
                        HiddenField rtype = (HiddenField)GridFITRooms.Rows[k].FindControl("hdnfRoomTypeId");
                        TextBox txtfitrate = (TextBox)GridFITRooms.Rows[k].FindControl("txtFitRate");

                        dv = new DataView(dtGetReturnedData);
                        dv.RowFilter = "RoomTypeId='" + rtype.Value.ToString() + "' and FitOrGit='FIT'";
                        txtfitrate.Text = dv.ToTable().Rows[0]["Amt"].ToString();

                    }

                    for (int k = 0; k < GridGITRooms.Rows.Count; k++)
                    {
                        HiddenField rtype = (HiddenField)GridGITRooms.Rows[k].FindControl("hdnfRoomTypeId");
                        TextBox txtGitRate = (TextBox)GridGITRooms.Rows[k].FindControl("txtGitRate");

                        dv = new DataView(dtGetReturnedData);
                        dv.RowFilter = "RoomTypeId='" + rtype.Value.ToString() + "' and FitOrGit='GIT'";
                        txtGitRate.Text = dv.ToTable().Rows[0]["Amt"].ToString();

                    }



                }

                blCard._Action = "GetMealDetails";
                dtGetReturnedData = new DataTable();


                dtGetReturnedData = dlcard.getRatecardbyId(blCard);

                if (dtGetReturnedData != null)
                {
                    for (int j = 0; j < gdvMealRatesFIT.Rows.Count; j++)
                    {


                        TextBox txtWelcomeDrinkFIT = (TextBox)gdvMealRatesFIT.Rows[j].FindControl("txtWelcomeDrinkFIT");
                        TextBox txtBreakfastFIT = (TextBox)gdvMealRatesFIT.Rows[j].FindControl("txtBreakfastFIT");
                        TextBox txtLunchFIT = (TextBox)gdvMealRatesFIT.Rows[j].FindControl("txtLunchFIT");
                        TextBox txtDinnerFIT = (TextBox)gdvMealRatesFIT.Rows[j].FindControl("txtDinnerFIT");
                        TextBox txtEveSnacksFIT = (TextBox)gdvMealRatesFIT.Rows[j].FindControl("txtEveSnacksFIT");

                        dv = new DataView(dtGetReturnedData);
                        dv.RowFilter = "FitOrGit='FIT'";

                        txtWelcomeDrinkFIT.Text = dv.ToTable().Rows[0]["WelcomeDrink"].ToString();
                        txtBreakfastFIT.Text = dv.ToTable().Rows[0]["Breakfast"].ToString();
                        txtLunchFIT.Text = dv.ToTable().Rows[0]["Lunch"].ToString();
                        txtDinnerFIT.Text = dv.ToTable().Rows[0]["Dinner"].ToString();
                        txtEveSnacksFIT.Text = dv.ToTable().Rows[0]["EveSnacks"].ToString();

                    }

                    for (int j = 0; j < gdvMealRatesGIT.Rows.Count; j++)
                    {


                        TextBox txtWelcomeDrinkGIT = (TextBox)gdvMealRatesGIT.Rows[j].FindControl("txtWelcomeDrinkGIT");
                        TextBox txtBreakfastGIT = (TextBox)gdvMealRatesGIT.Rows[j].FindControl("txtBreakfastGIT");
                        TextBox txtLunchGIT = (TextBox)gdvMealRatesGIT.Rows[j].FindControl("txtLunchGIT");
                        TextBox txtDinnerGIT = (TextBox)gdvMealRatesGIT.Rows[j].FindControl("txtDinnerGIT");
                        TextBox txtEveSnacksGIT = (TextBox)gdvMealRatesGIT.Rows[j].FindControl("txtEveSnacksGIT");

                        dv = new DataView(dtGetReturnedData);
                        dv.RowFilter = "FitOrGit='GIT'";

                        txtWelcomeDrinkGIT.Text = dv.ToTable().Rows[0]["WelcomeDrink"].ToString();
                        txtBreakfastGIT.Text = dv.ToTable().Rows[0]["Breakfast"].ToString();
                        txtLunchGIT.Text = dv.ToTable().Rows[0]["Lunch"].ToString();
                        txtDinnerGIT.Text = dv.ToTable().Rows[0]["Dinner"].ToString();
                        txtEveSnacksGIT.Text = dv.ToTable().Rows[0]["EveSnacks"].ToString();

                    }


                }

                blCard._Action = "GetQuadExtraBed";
                dtGetReturnedData = new DataTable();


                dtGetReturnedData = dlcard.getRatecardbyId(blCard);

                if (dtGetReturnedData != null)
                {


                    dv = new DataView(dtGetReturnedData);
                    dv.RowFilter = "FitGit='FIT'";

                    txtFItExtraBed.Text = dv.ToTable().Rows[0]["ExtraBed"].ToString();
                    txtFITQuad.Text = dv.ToTable().Rows[0]["Quad"].ToString();

                    dv.RowFilter = "FitGit='GIT'";
                    txtGitExtraBed.Text = dv.ToTable().Rows[0]["ExtraBed"].ToString();
                    txtGitQuad.Text = dv.ToTable().Rows[0]["Quad"].ToString();

                }




            }








        }
        catch
        {
        }
    }
    protected void gdvRateCards_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            string ratecardid = gdvRateCards.DataKeys[e.RowIndex].Value.ToString();
            blCard._Action = "DeleteRateCard";
            blCard._RateCardId = ratecardid;
            int res = dlcard.DeleteHotelRateCard(blCard);
            if (res > 0)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Showstatus", "javascript:alert('Rate card Deleted')", true);
                bindRatecards();
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Showstatus", "javascript:alert('Rate card could not be  Deleted')", true);

            }





        }

        catch
        {

            ScriptManager.RegisterStartupScript(this, this.GetType(), "Showstatus", "javascript:alert('Rate card could not be  Deleted')", true);
        }
    }
    protected void ddlAccom_SelectedIndexChanged(object sender, EventArgs e)
    {


        DataTable dtratecard = ViewState["RateCards"] as DataTable;
        dv = new DataView(dtratecard);
        dv.RowFilter = "AccomId='" + ddlAccom.SelectedValue + "'";
        if (dv.ToTable().Rows.Count > 0)
        {
            gdvRateCards.DataSource = dv;
            gdvRateCards.DataBind();
        }

        else
        {
            gdvRateCards.DataSource = null;
            gdvRateCards.DataBind();
        }



        bindroomcat(Convert.ToInt32(ddlAccom.SelectedValue));
       
    }

    public void bindroomcat(int acmid)
    {
        try
        {
            blCard._Action = "getAllRoomCategory";
            blCard._AccomId = acmid;
            DataTable dtGetReturnedData1 = new DataTable();
            dtGetReturnedData1 = dlcard.GetRoomCatbyAccomid(blCard);
            if (dtGetReturnedData1.Rows.Count > 0)
            {
                ddlRoomCategory.DataSource = dtGetReturnedData1;
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

    protected void ddlRoomCategory_SelectedIndexChanged(object sender, EventArgs e)
    {
        DataTable dtratecard = ViewState["RateCards"] as DataTable;
        dv = new DataView(dtratecard);
        dv.RowFilter = "AccomId='" + ddlAccom.SelectedValue + "' and RoomCateId='"+ddlRoomCategory.SelectedValue+"'";
        if (dv.ToTable().Rows.Count > 0)
        {
            gdvRateCards.DataSource = dv;
            gdvRateCards.DataBind();
        }

        else
        {
            gdvRateCards.DataSource = null;
            gdvRateCards.DataBind();
        }
    }
    protected void ddlRatecategory_SelectedIndexChanged(object sender, EventArgs e)
    {
        DataTable dtratecard = ViewState["RateCards"] as DataTable;
        dv = new DataView(dtratecard);
        dv.RowFilter = "RatecatId='" + ddlRatecategory.SelectedValue + "'";
        if (dv.ToTable().Rows.Count > 0)
        {
            gdvRateCards.DataSource = dv;
            gdvRateCards.DataBind();
        }

        else
        {
            gdvRateCards.DataSource = null;
            gdvRateCards.DataBind();
        }
    }
}