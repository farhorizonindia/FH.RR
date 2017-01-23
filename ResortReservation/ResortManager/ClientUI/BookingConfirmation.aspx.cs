using FarHorizon.Reservations.Bases.BasePages;
using FarHorizon.Reservations.BusinessServices;
using FarHorizon.Reservations.Common;
using FarHorizon.Reservations.Common.DataEntities.Client;
using FarHorizon.Reservations.Common.DataEntities.Masters;
using FarHorizon.Reservations.MasterServices;
using System;
using System.Collections;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ClientUI_BookingConfirmation : ClientBasePage
{
    int iBookingId;
    static int iStaticCounter = 0;

    #region Control Functions
    protected void Page_Load(object sender, EventArgs e)
    {
        Table t;
        AddAttributes();
        iBookingId = Convert.ToInt32(Request.QueryString["bid"].ToString());

        if (!IsPostBack)
        {
            SessionServices.DeleteSession(Constants._BookingConfirmation_BookingMealPlanData);
            FillBookingDetails(iBookingId);
            FillCities();
            FillTransport();
            FillBookedRoomDetails(iBookingId);
            FillDateWiseMealControls();
            FillBookingMealPlanData(iBookingId);
            FillBookingActivities(iBookingId);
            #region Buttons State when Existing Booking
            SetButtonsState();
            #endregion Buttons State when Existing Booking
            System.Web.UI.ScriptManager.GetCurrent(this).SetFocus(this.txtExchangeOrderNo);
        }
        else if (IsPostBack)
        {
            iStaticCounter = 0;
            FillDateWiseMealControls();
            t = null;
            
            FillBookingMealPlanData(iBookingId);            
            FillBookedRoomDetails(iBookingId);            
        }
    }

    private void checkwaitlisted(DateTime sd, DateTime ed, int iAccomId)
    {
        BookedRooms[] oBookedRooms;
        oBookedRooms = GetAllRooms(sd, ed, iAccomId);
        for (int k = 0; k < oBookedRooms.Length; k++)
        {
            if ((oBookedRooms[k].RoomStatus == Constants.WAITLISTED || oBookedRooms[k].RoomStatus == Constants.BOOKED) && oBookedRooms[k].BookingId != iBookingId)
            {
                ViewState["atleastonewaitlisted"] = true;
                break;
            }
            ViewState["atleastonewaitlisted"] = false;

        }



    }

    private BookedRooms[] GetAllRooms(DateTime dtStartDate, DateTime dtEndDate, int iAccomID)
    {
        //This will Return the Rooms from the Database
        BookedRooms[] oTotalRooms = null;
        BookingServices oBookingManager = null;


        oBookingManager = new BookingServices();
        oTotalRooms = oBookingManager.GetAllRooms(dtStartDate, dtEndDate, iAccomID, iBookingId);

        return oTotalRooms;
    }

    protected void btnEditTour_Click(object sender, EventArgs e)
    {
        ClearSessionVariables();
        Response.Redirect("Booking.aspx?bid=" + iBookingId);
    }

    protected void btnReset_Click(object sender, EventArgs e)
    {
        ClearControls();
    }

    protected void btnAddTouristDetails_Click(object sender, EventArgs e)
    {
        ClearSessionVariables();
        Response.Redirect("touristDetails.aspx?bid=" + iBookingId);
    }

    protected void btnViewTourist_Click(object sender, EventArgs e)
    {
        ClearSessionVariables();
        Response.Redirect("ViewTourists.aspx?bid=" + iBookingId.ToString());
    }

    protected void btnConfirmBooking_Click(object sender, EventArgs e)
    {
        if (ValidateValues() == false)
            return;
        ConfirmBooking();
    }

    void ddl_SelectedIndexChanged(object sender, EventArgs e)
    {
        MealPlanMaster mealPlanMaster;
        MealPlanDTO mealPlanData;
        int iSelectedMeal;
        Control c = null;
        DropDownList ddlMP = null;
        CheckBox chk = null;
        string sCurrentDdl = "", schkId = "", sRequesterDdl = "";
        ddlMP = (DropDownList)sender;
        sCurrentDdl = ddlMP.ID;

        sRequesterDdl = GetControlId(GetPostBackControlID());

        if (string.Compare(sRequesterDdl, sCurrentDdl) == 0)
        {
            mealPlanMaster = new MealPlanMaster();
            iSelectedMeal = Convert.ToInt32(ddlMP.SelectedValue);
            mealPlanData = GetMealPlan(iSelectedMeal);

            #region Welcome Drink
            schkId = sCurrentDdl.Replace(Constants.DROPDOWNLIST_ROOMS + Constants.MEALPLAN, Constants.CHECKBOX_WELCOMEDRINK);
            c = FindControl(pnlDateWiseSchedule, schkId);
            if (c != null)
            {
                chk = (CheckBox)c;
                if (IsCheckInDateControl(schkId.Substring(6)))
                {
                    if (mealPlanData == null)
                        chk.Checked = false;
                    else
                        chk.Checked = mealPlanData.WelcomeDrink;
                }
                else
                {
                    chk.Checked = false;
                }
            }
            #endregion

            #region BreakFast
            schkId = sCurrentDdl.Replace(Constants.DROPDOWNLIST_ROOMS + Constants.MEALPLAN, Constants.CHECKBOX_BREAKFAST);
            c = FindControl(pnlDateWiseSchedule, schkId);
            if (c != null)
            {
                chk = (CheckBox)c;
                if (!IsCheckInDateControl(schkId.Substring(6)))
                {
                    if (mealPlanData == null)
                        chk.Checked = false;
                    else
                        chk.Checked = mealPlanData.Breakfast;
                }
                else
                {
                    chk.Checked = false;
                }
            }
            #endregion

            #region Lunch
            schkId = sCurrentDdl.Replace(Constants.DROPDOWNLIST_ROOMS + Constants.MEALPLAN, Constants.CHECKBOX_LUNCH);
            c = FindControl(pnlDateWiseSchedule, schkId);
            if (c != null)
            {
                chk = (CheckBox)c;
                if (!IsCheckOutDateControl(schkId.Substring(6)))
                {
                    if (mealPlanData == null)
                        chk.Checked = false;
                    else
                        chk.Checked = mealPlanData.Lunch;
                }
                else
                {
                    chk.Checked = false;
                }
            }
            #endregion

            #region Evening Snacks
            schkId = sCurrentDdl.Replace(Constants.DROPDOWNLIST_ROOMS + Constants.MEALPLAN, Constants.CHECKBOX_EVENINGSNACKS);
            c = FindControl(pnlDateWiseSchedule, schkId);
            if (c != null)
            {
                chk = (CheckBox)c;
                if (!IsCheckOutDateControl(schkId.Substring(6)))
                {
                    if (mealPlanData == null)
                        chk.Checked = false;
                    else
                        chk.Checked = mealPlanData.EveningSnacks;
                }
                else
                {
                    chk.Checked = false;
                }
            }
            #endregion

            #region Dinner
            schkId = sCurrentDdl.Replace(Constants.DROPDOWNLIST_ROOMS + Constants.MEALPLAN, Constants.CHECKBOX_DINNER);
            c = FindControl(pnlDateWiseSchedule, schkId);
            if (c != null)
            {
                chk = (CheckBox)c;
                if (!IsCheckOutDateControl(schkId.Substring(6)))
                {
                    if (mealPlanData == null)
                        chk.Checked = false;
                    else
                        chk.Checked = mealPlanData.Dinner;
                }
                else
                {
                    chk.Checked = false;
                }
            }
            #endregion

            System.Web.UI.ScriptManager.GetCurrent(this).SetFocus(ddlMP);
        }
    }

    protected void btnCancelBooking_Click(object sender, EventArgs e)
    {
        CancelBooking();
    }

    protected void btnCancelConfirmation_Click(object sender, EventArgs e)
    {
        if (base.ValidateIfCommandAllowed(Request.Url.AbsoluteUri, ENums.PageCommand.Cancel))
        {
            BookingServices oBookingManager;
            oBookingManager = new BookingServices();
            oBookingManager.CancelBookingConfirmation(iBookingId);
            ClearSessionVariables();
            Response.Redirect("afterBookingactions.aspx?bid=" + iBookingId + "&bstatus=confirmation_cancelled");
        }
    }

    #endregion Control Functions

    #region UserDefined Functions

    private void AddAttributes()
    {
        btnConfirmBooking.Attributes.Add("onclick", "return validateBeforeConfirmation()");
        //txtDepartureDriverPhoneNo.Attributes.Add("onkeypress", "return focusOnNextTab(e)");
        txtDepartureDriverPhoneNo.Attributes.Add("onkeydown", "setTextFocusOnTab(event)");
    }

    private void ClearControls()
    {
        CheckBox ch;
        Table t;
        txtExchangeOrderNo.Text = string.Empty;
        txtVoucherDate.Text = string.Empty;
        txtArrivalDate.Text = string.Empty;
        ddlArrivalTransport.SelectedIndex = 0;
        ddlArrivalCity.SelectedIndex = 0;
        txtDepartureDate.Text = string.Empty;
        ddlDepartureTransport.SelectedIndex = 0;
        ddlDepartureCity.SelectedIndex = 0;

        txtArrivalVehicleNameType.Text = string.Empty;
        txtArrivalTransportCompanyPhoneNo.Text = string.Empty;
        txtArrivalDriverPhoneNo.Text = string.Empty;

        txtDepartureVehicleNameType.Text = string.Empty;
        txtDepartureTransportCompanyPhoneNo.Text = string.Empty;
        txtDepartureDriverPhoneNo.Text = string.Empty;

        foreach (Control c in pnlDateWiseSchedule.Controls)
        {
            if (c.GetType().Name == "Table")
            {
                t = (Table)c;
                for (int i = 1; i < t.Rows.Count; i++)
                {
                    for (int j = 2; j < t.Rows[i].Cells.Count; j++)
                    {
                        ch = (CheckBox)t.Rows[i].Cells[j].Controls[0];
                        ch.Checked = false;
                    }
                }
            }
        }
    }

    private void FillCities()
    {
        CityMaster oCityMaster;
        CityDTO[] oCityData;
        SortedList slCity = new SortedList();

        ddlArrivalCity.Items.Clear();
        ddlDepartureCity.Items.Clear();
        oCityMaster = new CityMaster();
        oCityData = oCityMaster.GetData();
        slCity.Add("0", "Choose City");
        if (oCityData != null)
        {
            for (int i = 0; i < oCityData.Length; i++)
            {
                slCity.Add(Convert.ToString(oCityData[i].CityId), Convert.ToString(oCityData[i].CityName));
            }
        }
        ddlArrivalCity.DataSource = slCity;
        ddlArrivalCity.DataTextField = "value";
        ddlArrivalCity.DataValueField = "key";
        ddlArrivalCity.DataBind();

        ddlDepartureCity.DataSource = slCity;
        ddlDepartureCity.DataTextField = "value";
        ddlDepartureCity.DataValueField = "key";
        ddlDepartureCity.DataBind();
    }

    private void FillTransport()
    {
        TransportMaster oTransportMaster;
        TransportDTO[] oTransportData;
        SortedList slTransport = new SortedList();

        ddlArrivalTransport.Items.Clear();
        ddlDepartureTransport.Items.Clear();
        oTransportMaster = new TransportMaster();
        oTransportData = oTransportMaster.GetData();
        slTransport.Add("0", "Choose Transport");
        if (oTransportData != null)
        {
            for (int i = 0; i < oTransportData.Length; i++)
            {
                slTransport.Add(Convert.ToString(oTransportData[i].TransportId), Convert.ToString(oTransportData[i].TransportName));
            }
        }
        ddlArrivalTransport.DataSource = slTransport;
        ddlArrivalTransport.DataTextField = "value";
        ddlArrivalTransport.DataValueField = "key";
        ddlArrivalTransport.DataBind();

        ddlDepartureTransport.DataSource = slTransport;
        ddlDepartureTransport.DataTextField = "value";
        ddlDepartureTransport.DataValueField = "key";
        ddlDepartureTransport.DataBind();
    }

    private void FillBookingDetails(int BookingId)
    {
        BookingServices oBookingManager;
        BookingDTO oBookingData;
        oBookingManager = new BookingServices();
        oBookingData = oBookingManager.GetBookingDetails(BookingId);
        checkwaitlisted(oBookingData.StartDate, oBookingData.EndDate, oBookingData.AccomodationId);
        ViewState["chartered"] = oBookingData.Chartered == null ? false : oBookingData.Chartered;
        if (oBookingData == null)
            return;
        txtBookingRef.Text = oBookingData.BookingReference;
        if (oBookingData.BookingStatusId == 1)
        {
            txtBookingStatus.Text = "Booked";
            btnAddTouristDetails.Visible = false;
            btnCancelBooking.Visible = false;
            txtBookingStatus.ForeColor = System.Drawing.Color.Orange;
        }
        else if (oBookingData.BookingStatusId == 2)
        {
            txtBookingStatus.Text = "Confirmed";
            btnCancelBooking.Visible = false;
            btnAddTouristDetails.Visible = false;
            txtBookingStatus.ForeColor = System.Drawing.Color.Green;
        }
        txtAgent.Text = oBookingData.AgentName;
        txtNoOfPersons.Text = oBookingData.NoOfPersons.ToString();
        txtAccomType.Text = oBookingData.AccomodationType;
        txtAccomodation.Text = oBookingData.AccomodationName;
        hfAccomId.Value = oBookingData.AccomodationId.ToString();
        txtStartDate.Text = GF.GetDD_MMM_YYYY(oBookingData.StartDate, false);
        txtEndDate.Text = GF.GetDD_MMM_YYYY(oBookingData.EndDate, false);
        //  SetConfirmButtonVisibility(oBookingData.StartDate, oBookingData.EndDate);


        txtNoOfNights.Text = oBookingData.NoOfNights.ToString();
        if (oBookingData.ExchangeOrderNo != null)
            txtExchangeOrderNo.Text = oBookingData.ExchangeOrderNo.ToString();
        else
            txtExchangeOrderNo.Text = "";
        if (oBookingData.VoucherDate != DateTime.MinValue)
            txtVoucherDate.Text = GF.GetDD_MMM_YYYY(oBookingData.VoucherDate, false);
        else
            txtVoucherDate.Text = "";
        if (oBookingData.TourId != null)
            txtTourId.Text = oBookingData.TourId.ToString();
        else
            txtTourId.Text = "";

        if (oBookingData.ArrivalDateTime == DateTime.MinValue)
            txtArrivalDate.Text = txtStartDate.Text;
        else
            txtArrivalDate.Text = GF.GetDD_MMM_YYYY(oBookingData.ArrivalDateTime, true);

        ddlArrivalTransport.SelectedValue = Convert.ToString(oBookingData.ArrivalTransportId);
        ddlArrivalCity.SelectedValue = oBookingData.ArrivalCityId.ToString();

        if (oBookingData.DepartureDateTime == DateTime.MinValue)
            txtDepartureDate.Text = txtEndDate.Text;
        else
            txtDepartureDate.Text = GF.GetDD_MMM_YYYY(oBookingData.DepartureDateTime, true);

        ddlDepartureTransport.SelectedValue = Convert.ToString(oBookingData.DepartureTransportId);
        ddlDepartureCity.SelectedValue = Convert.ToString(oBookingData.DepartureCityId);
        txtArrivalVehicleNo.Text = Convert.ToString(oBookingData.ArrivalVehicleNo);
        txtDepartureVehicleNo.Text = Convert.ToString(oBookingData.DepartureVehicleNo);
        txtArrivalTrasnportCompany.Text = Convert.ToString(oBookingData.ArrivalTransportCompany);
        txtDepartureTransportCompany.Text = Convert.ToString(oBookingData.DepartureTransportCompany);

        txtArrivalVehicleNameType.Text = Convert.ToString(oBookingData.ArrivalVehicleNameType);
        txtArrivalTransportCompanyPhoneNo.Text = Convert.ToString(oBookingData.ArrivalTransportCompanyPhoneNo);
        txtArrivalDriverPhoneNo.Text = Convert.ToString(oBookingData.ArrivalDriverPhoneNo);

        txtDepartureVehicleNameType.Text = Convert.ToString(oBookingData.DepartureVehicleNameType);
        txtDepartureTransportCompanyPhoneNo.Text = Convert.ToString(oBookingData.DepartureTransportCompanyPhoneNo);
        txtDepartureDriverPhoneNo.Text = Convert.ToString(oBookingData.DepartureDriverPhoneNo);
    }

    public void SetConfirmButtonVisibility(DateTime frm, DateTime to)
    {
        BookingServices bsc = new BookingServices();
        DataSet dst = bsc.CheckifCharteredBookingExists(frm, to);
        if (dst.Tables.Count > 0)
        {
            if (dst.Tables[0].Rows.Count > 0)
            {
                btnConfirmBooking.Visible = false;
            }
            else
            {
                btnConfirmBooking.Visible = true;
            }
        }
        else
        {
            btnConfirmBooking.Visible = false;
        }
    }


    private void FillDateWiseMealControls()
    {
        DateTime sDate, eDate, CurrentDate;
        //Panel p;
        Table t = new Table();
        Table t1 = null; ;
        TableRow tr;
        TableCell tc;
        MealPlanDTO[] oMealPlanData = null;
        AccomActivityDTO[] oAccomActivityData = null;

        if (txtStartDate.Text.Trim() == "")
            return;
        if (txtEndDate.Text.Trim() == "")
            return;
        DateTime.TryParse(txtStartDate.Text, out sDate);
        DateTime.TryParse(txtEndDate.Text, out eDate);

        //sl = GetMealPlanList(); //Getting Values for all the Meal Combos        
        oMealPlanData = GetMealPlans();
        oAccomActivityData = GetActivitiesOfAccomodation();
        CurrentDate = sDate;

        while (CurrentDate <= eDate)
        {
            t1 = PrepareDateWiseControls(CurrentDate, oMealPlanData, oAccomActivityData);
            tr = new TableRow();
            tc = new TableCell();
            tc.Controls.Add(t1);
            tr.Cells.Add(tc);
            t.Rows.Add(tr);
            CurrentDate = CurrentDate.AddDays(1);
        }
        AddControlstoDateWiseMealPlanPanel(t);
    }

    private Table PrepareDateWiseControls(DateTime CurrentDate, MealPlanDTO[] oMealPlanData, AccomActivityDTO[] oAccomWiseActivityData)
    {

        Table Tmeals = new Table();
        Tmeals.BorderWidth = 1;
        Tmeals.BorderStyle = BorderStyle.Dotted;
        Table t;
        TableRow tr;
        TableCell tc;
        if (oMealPlanData != null)
        {
            t = null;
            t = MealControls(CurrentDate, oMealPlanData);
            tc = new TableCell();
            tc.Controls.Add(t);
            tr = new TableRow();
            tr.Cells.Add(tc);
            Tmeals.Rows.Add(tr);
        }
        if (oAccomWiseActivityData != null)
        {
            t = null;
            t = ActivityControls(CurrentDate, oAccomWiseActivityData);
            tc = new TableCell();
            tc.Controls.Add(t);
            tr = new TableRow();
            tr.Cells.Add(tc);
            Tmeals.Rows.Add(tr);
        }

        return Tmeals;
    }

    private Table MealControls(DateTime CurrentDate, MealPlanDTO[] oMealPlanData)
    {
        Table t = new Table();
        t.CssClass = "mealPlanTable";
        TableCell tc;
        ListItem l;
        DropDownList ddl;
        CheckBox ch;
        Label lbl;
        TableRow tr = new TableRow();

        if (iStaticCounter == 0)
        {
            #region HeaderCells
            //t = SetMealandActivitiesHeaderRow(t);
            #endregion
            iStaticCounter = 1;
        }

        tr = new TableRow();
        tc = new TableCell();
        //tc.Text = "Meals";
        //tc.Width = 50;        
        tr.Cells.Add(tc);

        #region Adding Date
        tc = new TableCell();
        tc.CssClass = "mealPlanCell";
        tc.Text = "Meal Date: " + GF.GetDD_MMM_YYYY(CurrentDate, false);
        tr.Cells.Add(tc);
        #endregion Adding Date

        #region Adding MealPlan Combo
        tc = new TableCell();
        tc.CssClass = "mealPlanCell";
        lbl = new Label();
        lbl.Text = "Meal Plan: ";

        ddl = new DropDownList();
        l = new ListItem();
        l.Value = "0";
        l.Text = "Choose";
        ddl.Items.Insert(0, l);
        ddl.CssClass = "mealPlan";

        ddl.ID = Constants.DROPDOWNLIST_ROOMS + Constants.MEALPLAN + CurrentDate.Year.ToString() + CurrentDate.Month.ToString("0#") + CurrentDate.Day.ToString("0#");

        for (int i = 0; i < oMealPlanData.Length; i++)
        {
            l = new ListItem();
            l.Value = oMealPlanData[i].MealPlanId.ToString();
            l.Text = oMealPlanData[i].MealPlanCode.ToString();
            ddl.Items.Insert(i + 1, l);
        }

        ddl.AutoPostBack = true;
        ddl.EnableViewState = true;
        ddl.SelectedIndexChanged += new EventHandler(ddl_SelectedIndexChanged);
        ddl.DataBind();
        tc.Controls.Add(lbl);
        tc.Controls.Add(ddl);
        tr.Cells.Add(tc);
        #endregion Adding MealPlan Combo

        #region Adding Welcome Drink
        tc = new TableCell();
        tc.CssClass = "mealPlanMealCell";
        ch = new CheckBox();
        ch.ID = Constants.CHECKBOX_WELCOMEDRINK + CurrentDate.Year.ToString() + CurrentDate.Month.ToString("0#") + CurrentDate.Day.ToString("0#");
        ch.Text = "Welcome Drink";
        tc.Controls.Add(ch);
        tr.Cells.Add(tc);
        #endregion Adding Welcome Drink

        #region Adding Breakfast
        tc = new TableCell();
        tc.CssClass = "mealPlanMealCell";
        ch = new CheckBox();
        ch.ID = Constants.CHECKBOX_BREAKFAST + CurrentDate.Year.ToString() + CurrentDate.Month.ToString("0#") + CurrentDate.Day.ToString("0#");
        ch.Text = "Breakfast";
        tc.Controls.Add(ch);
        tr.Cells.Add(tc);
        #endregion Adding Breakfast

        #region Adding Lunch
        tc = new TableCell();
        tc.CssClass = "mealPlanMealCell";
        ch = new CheckBox();
        ch.ID = Constants.CHECKBOX_LUNCH + CurrentDate.Year.ToString() + CurrentDate.Month.ToString("0#") + CurrentDate.Day.ToString("0#");
        ch.Text = "Lunch";
        tc.Controls.Add(ch);
        tr.Cells.Add(tc);
        #endregion Adding Lunch

        #region Adding Evening Snacks
        tc = new TableCell();
        tc.CssClass = "mealPlanMealCell";
        ch = new CheckBox();
        ch.ID = Constants.CHECKBOX_EVENINGSNACKS + CurrentDate.Year.ToString() + CurrentDate.Month.ToString("0#") + CurrentDate.Day.ToString("0#");
        ch.Text = "Evening Snacks";
        tc.Controls.Add(ch);
        tr.Cells.Add(tc);
        #endregion Adding Evening Snacks

        #region Adding Dinner
        tc = new TableCell();
        tc.CssClass = "mealPlanMealCell";
        ch = new CheckBox();
        ch.ID = Constants.CHECKBOX_DINNER + CurrentDate.Year.ToString() + CurrentDate.Month.ToString("0#") + CurrentDate.Day.ToString("0#");
        ch.Text = "Dinner";
        tc.Controls.Add(ch);
        tr.Cells.Add(tc);
        #endregion Adding Dinner

        t.Rows.Add(tr);
        return t;
    }

    private Table SetMealandActivitiesHeaderRow(Table t)
    {
        //t.BorderWidth = 2;
        t.ID = "tblMealPlanControls";
        TableCell tc = null;
        TableRow tr = null;
        tr = new TableRow();
        tc = new TableCell();
        tr.Cells.Add(tc);

        tc = new TableCell();
        tc.Text = "Date";
        tr.Cells.Add(tc);

        tc = new TableCell();
        tc.Text = "Meal Plan";
        tr.Cells.Add(tc);

        tc = new TableCell();
        tc.Text = "W";
        tc.Style.Add(HtmlTextWriterStyle.TextAlign, "left");
        //tc.Width = 100;
        tr.Cells.Add(tc);

        tc = new TableCell();
        tc.Style.Add(HtmlTextWriterStyle.TextAlign, "left");
        tc.Text = "B";
        //tc.Width = 100;
        tr.Cells.Add(tc);

        tc = new TableCell();
        tc.Style.Add(HtmlTextWriterStyle.TextAlign, "left");
        tc.Text = "L";
        //tc.Width = 100;
        tr.Cells.Add(tc);

        tc = new TableCell();
        tc.Style.Add(HtmlTextWriterStyle.TextAlign, "left");
        tc.Text = "S";
        //tc.Width = 100;
        tr.Cells.Add(tc);


        tc = new TableCell();
        tc.Style.Add(HtmlTextWriterStyle.TextAlign, "left");
        tc.Text = "D";
        //tc.Width = 100;
        tr.Cells.Add(tc);

        t.Rows.Add(tr);

        tr = new TableRow();
        tc = new TableCell();
        tc.ColumnSpan = 10;
        tc.Text = "<hr />";

        tr.Cells.Add(tc);
        t.Rows.Add(tr);
        return t;
    }

    private Table ActivityControls(DateTime CurrentDate, AccomActivityDTO[] oAccomWiseActivityData)
    {
        Table t = new Table();
        TableRow tr;
        TableCell tc;
        CheckBox ch;

        tr = new TableRow();
        tc = new TableCell(); //For Date Cell
        tc.Text = "Activities";
        tc.Width = 50;
        //tc.BorderWidth = 2;
        //tr.Cells.Add(tc); // For Meal Plan Combo
        //t.Rows.Add(tr);

        for (int i = 0; i < oAccomWiseActivityData.Length; i++)
        {
            if (i > 0)
            {
                tr = new TableRow();
                tc = new TableCell(); //For Date Cell
                tc.BorderWidth = 2;
                //tc.Width = 100;
            }
            tr.Cells.Add(tc); // For Meal Plan Combo

            //tc = new TableCell();
            //tr.Cells.Add(tc);

            #region Adding Activity Checkbox
            tc = new TableCell();
            //tc.BorderWidth = 2;
            ch = new CheckBox();
            ch.ID = Constants.CHECKBOX_ACTIVITY + oAccomWiseActivityData[i].ActivityId.ToString() + "*" + CurrentDate.Year.ToString() + CurrentDate.Month.ToString("0#") + CurrentDate.Day.ToString("0#");
            tc.Controls.Add(ch);
            tr.Cells.Add(tc);
            #endregion Adding Activity Checkbox

            #region Adding Activity Name
            tc = new TableCell();
            //tc.BorderWidth = 2;
            tc.Text = oAccomWiseActivityData[i].ActivityName;
            tc.ToolTip = oAccomWiseActivityData[i].ActivityDesc;
            tr.Cells.Add(tc);

            t.Rows.Add(tr);
            #endregion Adding Activity Name
        }
        return t;
    }

    private void FillBookingMealPlanData(int BookingId)
    {
        CheckBox chk = null;
        DropDownList ddl = null;
        Control c = null;
        DateTime dt;
        string sCntrlId = "";
        BookingMealPlanDTO[] oBMPD = null;

        oBMPD = GetBookingMealPlanData(BookingId);
        if (oBMPD != null && oBMPD.Length > 0)
        {
            for (int i = 0; i < oBMPD.Length; i++)
            {
                if (oBMPD[i] != null)
                {
                    dt = oBMPD[i].MealPlanDate;

                    #region Setting DDL
                    sCntrlId = Constants.DROPDOWNLIST_ROOMS + Constants.MEALPLAN + dt.Year.ToString() + dt.Month.ToString("0#") + dt.Day.ToString("0#");
                    c = FindControl(pnlDateWiseSchedule, sCntrlId);
                    if (c != null)
                    {
                        ddl = (DropDownList)c;
                        ddl.SelectedValue = oBMPD[i].MealPlanId.ToString();
                    }
                    #endregion Setting DDL

                    #region Setting Chks
                    if (oBMPD[i].WelcomeDrink == true)
                    {
                        sCntrlId = Constants.CHECKBOX_WELCOMEDRINK + dt.Year.ToString() + dt.Month.ToString("0#") + dt.Day.ToString("0#");
                        c = FindControl(pnlDateWiseSchedule, sCntrlId);
                        if (c != null)
                        {
                            chk = (CheckBox)c;
                            chk.Checked = true;
                        }
                    }

                    if (oBMPD[i].Breakfast == true)
                    {
                        sCntrlId = Constants.CHECKBOX_BREAKFAST + dt.Year.ToString() + dt.Month.ToString("0#") + dt.Day.ToString("0#");
                        c = FindControl(pnlDateWiseSchedule, sCntrlId);
                        if (c != null)
                        {
                            chk = (CheckBox)c;
                            chk.Checked = true;
                        }
                    }
                    if (oBMPD[i].Lunch == true)
                    {
                        sCntrlId = Constants.CHECKBOX_LUNCH + dt.Year.ToString() + dt.Month.ToString("0#") + dt.Day.ToString("0#");
                        c = FindControl(pnlDateWiseSchedule, sCntrlId);
                        if (c != null)
                        {
                            chk = (CheckBox)c;
                            chk.Checked = true;
                        }
                    }

                    if (oBMPD[i].EveningSnacks == true)
                    {
                        sCntrlId = Constants.CHECKBOX_EVENINGSNACKS + dt.Year.ToString() + dt.Month.ToString("0#") + dt.Day.ToString("0#");
                        c = FindControl(pnlDateWiseSchedule, sCntrlId);
                        if (c != null)
                        {
                            chk = (CheckBox)c;
                            chk.Checked = true;
                        }
                    }

                    if (oBMPD[i].Dinner == true)
                    {
                        sCntrlId = Constants.CHECKBOX_DINNER + dt.Year.ToString() + dt.Month.ToString("0#") + dt.Day.ToString("0#");
                        c = FindControl(pnlDateWiseSchedule, sCntrlId);
                        if (c != null)
                        {
                            chk = (CheckBox)c;
                            chk.Checked = true;
                        }
                    }
                    #endregion Setting Chks
                }
            }
        }
    }

    private void FillBookedRoomDetails(int BookingId)
    {
        ShowBookedRoomsPanel(GetTotalRoomsBooked(BookingId));
    }

    private void FillBookingActivities(int BookingId)
    {
        CheckBox chk = null;
        Control c = null;
        string sCntrlId = "";
        BookingActivityDTO[] oBAD = null;
        DateTime dt;

        oBAD = GetBookingActivityData(BookingId);
        if (oBAD != null && oBAD.Length > 0)
        {
            for (int i = 0; i < oBAD.Length; i++)
            {
                if (oBAD[i] != null)
                {
                    dt = oBAD[i].OperationDate;

                    #region Setting Chks
                    sCntrlId = Constants.CHECKBOX_ACTIVITY + oBAD[i].ActivityId.ToString() + "*" + dt.Year.ToString() + dt.Month.ToString("0#") + dt.Day.ToString("0#");
                    c = FindControl(pnlDateWiseSchedule, sCntrlId);
                    if (c != null)
                    {
                        chk = (CheckBox)c;
                        chk.Checked = true;
                    }

                    #endregion Setting Chks
                }
            }
        }

    }

    private void SaveActivitiesAndMealPlans(out BookingActivityDTO[] oBookingActivityDTO, out BookingMealPlanDTO[] oBookingMealPlanDTO)
    {
        Hashtable htBMealPlanData = new Hashtable();
        Hashtable htBActivityData = new Hashtable();
        DateTime sDate, eDate, CurrentDate;
        //Panel p;
        int iTotalDays = 0, iMaxActivities = 0;
        Table t = new Table();
        AccomActivityDTO[] oAAD = null;
        BookingMealPlanDTO[] oBMPDs = null;
        BookingMealPlanDTO oBMPD = null;
        BookingActivityDTO[] oBADs = null, oTotalBADs = null;

        oBookingActivityDTO = null;
        oBookingMealPlanDTO = null;

        oAAD = GetActivitiesOfAccomodation();

        if (txtStartDate.Text.Trim() == "")
            return;
        if (txtEndDate.Text.Trim() == "")
            return;
        DateTime.TryParse(txtStartDate.Text, out sDate);
        DateTime.TryParse(txtEndDate.Text, out eDate);
        TimeSpan ts = eDate.Subtract(sDate);
        iTotalDays = ts.Days;
        if (oAAD != null)
            iMaxActivities = oAAD.Length * iTotalDays;
        oTotalBADs = new BookingActivityDTO[iMaxActivities];

        CurrentDate = sDate;
        while (CurrentDate <= eDate)
        {
            oBMPD = SetMealPlanData(CurrentDate);
            if (oBMPD != null)
            {
                htBMealPlanData.Add(CurrentDate, oBMPD);
                oBMPD = null;
            }
            if (oAAD != null)
            {
                oBADs = SetActivityData(CurrentDate, oAAD);
                if (oBADs != null)
                {
                    htBActivityData.Add(CurrentDate, oBADs);
                }
            }
            CurrentDate = CurrentDate.AddDays(1);
        }

        CurrentDate = sDate;
        oBMPDs = new BookingMealPlanDTO[htBMealPlanData.Count];
        oBADs = new BookingActivityDTO[htBActivityData.Count];
        int itotalbads = 0, itotalbmpds = 0;
        while (CurrentDate <= eDate)
        {
            oBMPD = (BookingMealPlanDTO)htBMealPlanData[CurrentDate];
            oBMPDs[itotalbmpds] = oBMPD;
            itotalbmpds++;

            oBADs = (BookingActivityDTO[])htBActivityData[CurrentDate];
            if (oBADs != null)
            {
                for (int i = 0; i < oBADs.Length; i++)
                {
                    if (oBADs[i] != null)
                    {
                        oTotalBADs[itotalbads] = oBADs[i];
                        itotalbads++;
                    }
                }
            }
            CurrentDate = CurrentDate.AddDays(1);
        }
        oBookingActivityDTO = oTotalBADs;
        oBookingMealPlanDTO = oBMPDs;
    }

    private BookedRoomsTotal[] GetTotalRoomsBooked(int BookingId)
    {
        BookedRoomsTotal[] oRoomsBooked = null;
        RoomMaster oRoomMaster = null;
        if (SessionServices.BookingConfirmation_TotalRoomsBooked == null)
        {
            oRoomMaster = new RoomMaster();
            oRoomsBooked = oRoomMaster.GetTotalRoomsBooked(BookingId);
        }
        else
        {
            oRoomsBooked = SessionServices.BookingConfirmation_TotalRoomsBooked;
        }
        return oRoomsBooked;
    }

    private BookingMealPlanDTO[] GetBookingMealPlanData(int BookingId)
    {
        BookingMealPlanDTO[] oBMPD = null;
        BookingServices oBookingManager;
        if (SessionServices.BookingConfirmation_BookingMealPlanDTO == null)
        {
            oBookingManager = new BookingServices();
            oBMPD = oBookingManager.GetBookingMealPlanData(BookingId);
            SessionServices.BookingConfirmation_BookingMealPlanDTO = oBMPD;
        }
        else
        {
            oBMPD = SessionServices.BookingConfirmation_BookingMealPlanDTO;
        }
        return oBMPD;
    }

    private BookingActivityDTO[] GetBookingActivityData(int BookingId)
    {
        BookingActivityDTO[] oBAD = null;
        BookingServices oBookingManager;
        if (SessionServices.BookingConfirmation_BookingActivityData == null)
        {
            oBookingManager = new BookingServices();
            oBAD = oBookingManager.GetBookingActivities(BookingId);
            SessionServices.BookingConfirmation_BookingActivityData = oBAD;
        }
        else
        {
            oBAD = SessionServices.BookingConfirmation_BookingActivityData;
        }
        return oBAD;
    }

    private Table ShowBookedRoomsPanel(BookedRoomsTotal[] oRoomsBooked)
    {        
        Table tblMain = null;
        TableRow trHeading = null;
        TableRow tr = null;
        TableCell tc = null;
        if (oRoomsBooked == null)
        {
            pnlBookedRoomDetails.Controls.Clear();
            pnlBookedRoomDetails.Visible = false;
            return tblMain;
        }
        if (oRoomsBooked != null && oRoomsBooked.Length > 0)
        {
            #region Booked Rooms
            tblMain = new Table();
            trHeading = new TableRow();
            tc = new TableCell();
            tblMain.Attributes.Add("cellpadding", "5");

            tc = new TableCell();
            tc.Style[HtmlTextWriterStyle.Color] = "Blue";
            tc.Text = "Category";
            tc.Attributes.Add("align", "left");
            trHeading.Cells.Add(tc);

            tc = new TableCell();
            tc.Style[HtmlTextWriterStyle.Color] = "Blue";
            tc.Text = "Room Type";
            tc.Attributes.Add("align", "left");
            trHeading.Cells.Add(tc);

            tc = new TableCell();
            tc.Style[HtmlTextWriterStyle.Color] = "Blue";
            tc.Attributes.Add("align", "right");
            tc.Text = "Booked";
            trHeading.Cells.Add(tc);

            tblMain.Rows.Add(trHeading);

            for (int i = 0; i < oRoomsBooked.Length; i++)
            {
                if (oRoomsBooked[i].RoomType != null)
                {
                    tr = new TableRow();
                    tc = new TableCell();
                    tc.Attributes.Add("align", "left");
                    tc.Text = Convert.ToString(oRoomsBooked[i].RoomCategory.ToString());
                    tr.Cells.Add(tc);

                    tc = new TableCell();
                    tc.Attributes.Add("align", "left");
                    tc.Text = Convert.ToString(oRoomsBooked[i].RoomType.ToString());
                    tr.Cells.Add(tc);

                    tc = new TableCell();
                    tc.Attributes.Add("align", "right");
                    tc.Text = Convert.ToString(oRoomsBooked[i].RoomsBooked.ToString());
                    tr.Cells.Add(tc);
                    tblMain.Rows.Add(tr);
                }
            }
            AddControlstoBookedRoomsPanel(tblMain);
            #endregion Booked Rooms
        }
        return tblMain;
    }

    private void AddControlstoBookedRoomsPanel(Table t)
    {
        pnlBookedRoomDetails.Controls.Clear();
        if (t != null)
        {
            pnlBookedRoomDetails.Visible = true;
            pnlBookedRoomDetails.Controls.Add(t);
        }
        else
        {
            pnlBookedRoomDetails.Visible = false;
        }
    }

    private void AddControlstoDateWiseMealPlanPanel(Table t)
    {
        if (pnlDateWiseSchedule.Controls.Count > 0)
            pnlDateWiseSchedule.Controls.RemoveAt(0);

        pnlDateWiseSchedule.Controls.Add(t);        
    }

    private AccomActivityDTO[] GetActivitiesOfAccomodation()
    {
        int iAccomId = 0;
        AccomActivityDTO[] oAccomWiseActivityData;
        if (SessionServices.BookingConfirmation_AccomodationActivityData == null)
        {
            AccomActivityMaster oAccomWiseActivityMaster = new AccomActivityMaster();
            int.TryParse(hfAccomId.Value, out iAccomId);
            oAccomWiseActivityData = oAccomWiseActivityMaster.GetData(iAccomId);
            SessionServices.BookingConfirmation_AccomodationActivityData = oAccomWiseActivityData;
        }
        else
        {
            oAccomWiseActivityData = SessionServices.BookingConfirmation_AccomodationActivityData;
        }
        return oAccomWiseActivityData;
    }

    private MealPlanDTO[] GetMealPlans()
    {
        MealPlanMaster oMealPlanMaster;
        MealPlanDTO[] oMealPlanData;

        if (SessionServices.BookingConfirmation_MealPlanData == null)
        {
            oMealPlanMaster = new MealPlanMaster();
            oMealPlanData = oMealPlanMaster.GetMeals();
            SessionServices.BookingConfirmation_MealPlanData = oMealPlanData;
        }
        else
        {
            oMealPlanData = SessionServices.BookingConfirmation_MealPlanData;
        }
        return oMealPlanData;
    }

    private MealPlanDTO GetMealPlan(int MealPlanId)
    {
        MealPlanDTO[] oMealPlanData = null;
        MealPlanDTO oMPD = null;

        oMealPlanData = GetMealPlans();

        for (int i = 0; i < oMealPlanData.Length; i++)
        {
            if (oMealPlanData[i].MealPlanId == MealPlanId)
            {
                oMPD = new MealPlanDTO();
                oMPD = oMealPlanData[i];
                break;
            }
        }
        return oMPD;
    }

    private BookingMealPlanDTO[] SetMealPlanDataOld(int BookingId)
    {
        BookingMealPlanDTO[] oBMPD;
        CheckBox ch;
        DropDownList ddl;
        Table t;
        oBMPD = null;
        int iArrayVar = 0;
        foreach (Control c in pnlDateWiseSchedule.Controls)
        {
            if (c.GetType().Name == "Table")
            {
                t = (Table)c;
                oBMPD = new BookingMealPlanDTO[t.Rows.Count - 1];
                for (int i = 1; i < t.Rows.Count; i++)
                {
                    ddl = (DropDownList)t.Rows[i].Cells[1].Controls[0];
                    if (Convert.ToInt32(ddl.SelectedValue) != 0)
                    {
                        oBMPD[iArrayVar] = new BookingMealPlanDTO();
                        oBMPD[iArrayVar].MealPlanId = Convert.ToInt32(ddl.SelectedValue);
                        oBMPD[iArrayVar].BookingId = BookingId;
                        oBMPD[iArrayVar].MealPlanDate = Convert.ToDateTime(t.Rows[i].Cells[0].Text);

                        ch = (CheckBox)t.Rows[i].Cells[2].Controls[0];
                        if (ch.Checked == true)
                            oBMPD[iArrayVar].WelcomeDrink = true;

                        ch = (CheckBox)t.Rows[i].Cells[3].Controls[0];
                        if (ch.Checked == true)
                            oBMPD[iArrayVar].Breakfast = true;

                        ch = (CheckBox)t.Rows[i].Cells[4].Controls[0];
                        if (ch.Checked == true)
                            oBMPD[iArrayVar].Lunch = true;

                        ch = (CheckBox)t.Rows[i].Cells[5].Controls[0];
                        if (ch.Checked == true)
                            oBMPD[iArrayVar].Dinner = true;
                        iArrayVar += 1;
                    }
                }
            }

        }
        SessionServices.BookingConfirmation_BookingMealPlanDTO = oBMPD;
        return oBMPD;
    }

    private BookingMealPlanDTO SetMealPlanData(DateTime CurrentDate)
    {
        Control c1;
        DropDownList ddl = null;
        CheckBox chk = null;
        string chkID = "";
        string ddlID = "";
        BookingMealPlanDTO oBMPD;
        oBMPD = new BookingMealPlanDTO();
        oBMPD.MealPlanDate = CurrentDate;
        oBMPD.BookingId = iBookingId;

        #region Meal Plan Type
        ddlID = Constants.DROPDOWNLIST_ROOMS + Constants.MEALPLAN + CurrentDate.Year.ToString() + CurrentDate.Month.ToString("0#") + CurrentDate.Day.ToString("0#");
        c1 = FindControl(pnlDateWiseSchedule, ddlID);
        if (c1 != null)
        {
            if (c1 is System.Web.UI.WebControls.DropDownList)
            {
                ddl = (DropDownList)c1;
                oBMPD.MealPlanId = Convert.ToInt32(ddl.SelectedItem.Value.ToString());
                c1 = null;
            }
        }

        #endregion Meal Plan Type

        #region Welcome Drink
        chkID = Constants.CHECKBOX_WELCOMEDRINK + CurrentDate.Year.ToString() + CurrentDate.Month.ToString("0#") + CurrentDate.Day.ToString("0#");
        c1 = FindControl(pnlDateWiseSchedule, chkID);
        if (c1 != null)
        {
            if (c1 is System.Web.UI.WebControls.CheckBox)
            {
                chk = (CheckBox)c1;
                oBMPD.WelcomeDrink = chk.Checked;
                c1 = null;
                chk = null;
                chkID = "";
            }
        }
        #endregion  Welcome Drink

        #region Breakfast
        chkID = Constants.CHECKBOX_BREAKFAST + CurrentDate.Year.ToString() + CurrentDate.Month.ToString("0#") + CurrentDate.Day.ToString("0#");
        c1 = FindControl(pnlDateWiseSchedule, chkID);
        if (c1 != null)
        {
            if (c1 is System.Web.UI.WebControls.CheckBox)
            {
                chk = (CheckBox)c1;
                oBMPD.Breakfast = chk.Checked;
                c1 = null;
                chk = null;
                chkID = "";
            }
        }
        #endregion Breakfast

        #region Lunch
        chkID = Constants.CHECKBOX_LUNCH + CurrentDate.Year.ToString() + CurrentDate.Month.ToString("0#") + CurrentDate.Day.ToString("0#");
        c1 = FindControl(pnlDateWiseSchedule, chkID);
        if (c1 != null)
        {
            if (c1 is System.Web.UI.WebControls.CheckBox)
            {
                chk = (CheckBox)c1;
                oBMPD.Lunch = chk.Checked;
                c1 = null;
                chk = null;
                chkID = "";
            }
        }
        #endregion  Lunch

        #region Evening Snacks
        chkID = Constants.CHECKBOX_EVENINGSNACKS + CurrentDate.Year.ToString() + CurrentDate.Month.ToString("0#") + CurrentDate.Day.ToString("0#");
        c1 = FindControl(pnlDateWiseSchedule, chkID);
        if (c1 != null)
        {
            if (c1 is System.Web.UI.WebControls.CheckBox)
            {
                chk = (CheckBox)c1;
                oBMPD.EveningSnacks = chk.Checked;
                c1 = null;
                chk = null;
                chkID = "";
            }

        }
        #endregion Evening Snacks

        #region Dinner
        chkID = Constants.CHECKBOX_DINNER + CurrentDate.Year.ToString() + CurrentDate.Month.ToString("0#") + CurrentDate.Day.ToString("0#");
        c1 = FindControl(pnlDateWiseSchedule, chkID);
        if (c1 != null)
        {
            if (c1 is System.Web.UI.WebControls.CheckBox)
            {
                chk = (CheckBox)c1;
                oBMPD.Dinner = chk.Checked;
                c1 = null;
                chk = null;
                chkID = "";
            }

        }
        #endregion Dinner

        return oBMPD;

    }

    private BookingActivityDTO[] SetActivityData(DateTime CurrentDate, AccomActivityDTO[] AccomActivityDTO)
    {
        int iArrLen = 0;
        //int TotalBookingActivityCount, 
        //iArrLen = TotalBookingActivityCount * AccomActivityData.Length;
        iArrLen = AccomActivityDTO.Length;
        BookingActivityDTO[] oBAD = new BookingActivityDTO[iArrLen];
        CheckBox chk = null;
        Control c = null;
        string chkID = "";
        for (int i = 0; i < AccomActivityDTO.Length; i++)
        {
            chkID = Constants.CHECKBOX_ACTIVITY + AccomActivityDTO[i].ActivityId.ToString() + "*" + CurrentDate.Year.ToString() + CurrentDate.Month.ToString("0#") + CurrentDate.Day.ToString("0#");
            c = FindControl(pnlDateWiseSchedule, chkID);
            if (c != null)
            {
                if (c is CheckBox)
                {
                    chk = (CheckBox)c;
                    if (chk.Checked == true)
                    {
                        oBAD[i] = new BookingActivityDTO();
                        oBAD[i].ActivityId = AccomActivityDTO[i].ActivityId;
                        oBAD[i].AccomId = AccomActivityDTO[i].AccomodationId;
                        oBAD[i].BookingId = iBookingId;
                        oBAD[i].OperationDate = CurrentDate;
                    }
                }
            }
        }
        return oBAD;
    }

    private void SetButtonsState()
    {
        if (string.Compare(txtBookingStatus.Text, "BOOKED", true) == 0)
        {
            btnConfirmBooking.Text = "Confirm Booking";
            btnConfirmBooking.Visible = true;
            btnConfirmBooking.Enabled = true;

            btnCancelConfirmation.Visible = true;
            btnCancelConfirmation.Enabled = false;

            btnCancelBooking.Visible = false;
            btnCancelBooking.Enabled = false;

            btnEditTour.Visible = true;
            btnEditTour.Enabled = true;

            btnAddTouristDetails.Visible = true;
            btnAddTouristDetails.Enabled = false;

            btnViewTourist.Visible = true;
            btnViewTourist.Enabled = false;

            btnReset.Visible = true;
            btnReset.Enabled = true;
        }
        else if (string.Compare(txtBookingStatus.Text, "CONFIRMED", true) == 0)
        {
            btnConfirmBooking.Text = "Update Confirmation";


            btnConfirmBooking.Visible = true;
            btnConfirmBooking.Enabled = true;

            btnCancelConfirmation.Visible = true;
            btnCancelConfirmation.Enabled = true;

            btnCancelBooking.Visible = false;
            btnCancelBooking.Enabled = false;

            btnAddTouristDetails.Visible = true;
            btnAddTouristDetails.Enabled = true;

            btnViewTourist.Visible = true;
            btnViewTourist.Enabled = true;

            btnEditTour.Visible = true;
            btnEditTour.Enabled = false;

            btnReset.Visible = true;
            btnReset.Enabled = false;
        }
    }

    private BookingDTO[] Bookconfirmformail(int bookingId)
    {
        BookingDTO[] obd = null;
        BookingServices oBRM = new BookingServices();
        obd = oBRM.GetConfirmMailDetails(bookingId);
        if (obd != null)
        {

            return obd;

        }
        else
        {
            return null;
        }

    }


    private void ConfirmBooking()
    {
        BookingServices oBookingManager;
        BookingDTO oBookingData;
        BookingMealPlanDTO[] oBMealPlanDTO;
        BookingActivityDTO[] oBActivityDTO;
        DateTime dt;

        if (base.ValidateIfCommandAllowed(Request.Url.AbsoluteUri, ENums.PageCommand.Cancel))
        {
            #region Confirmation Data
            oBookingData = new BookingDTO();
            oBookingData.BookingId = iBookingId;
            oBookingData.TourId = txtTourId.Text.Trim();
            oBookingData.ExchangeOrderNo = txtExchangeOrderNo.Text.Trim();
            DateTime.TryParse(txtVoucherDate.Text.Trim(), out dt);
            if (dt != DateTime.MinValue)
                oBookingData.VoucherDate = dt;
            dt = DateTime.MinValue;
            DateTime.TryParse(txtArrivalDate.Text.Trim(), out dt);
            oBookingData.ArrivalDateTime = dt;
            oBookingData.ArrivalTransportId = Convert.ToInt32(ddlArrivalTransport.SelectedValue);
            oBookingData.ArrivalCityId = Convert.ToInt32(ddlArrivalCity.SelectedValue);

            oBookingData.ArrivalVehicleNameType = txtArrivalVehicleNameType.Text.Trim();
            oBookingData.ArrivalTransportCompanyPhoneNo = txtArrivalTransportCompanyPhoneNo.Text.Trim();
            oBookingData.ArrivalDriverPhoneNo = txtArrivalDriverPhoneNo.Text.Trim();

            oBookingData.DepartureVehicleNameType = txtDepartureVehicleNameType.Text.Trim();
            oBookingData.DepartureTransportCompanyPhoneNo = txtDepartureTransportCompanyPhoneNo.Text.Trim();
            oBookingData.DepartureDriverPhoneNo = txtDepartureDriverPhoneNo.Text.Trim();

            DateTime.TryParse(txtDepartureDate.Text.Trim(), out dt);
            oBookingData.DepartureDateTime = dt;
            oBookingData.DepartureTransportId = Convert.ToInt32(ddlDepartureTransport.SelectedValue);
            oBookingData.DepartureCityId = Convert.ToInt32(ddlDepartureCity.SelectedValue);
            oBookingData.ArrivalVehicleNo = txtArrivalVehicleNo.Text.Trim();
            oBookingData.ArrivalTransportCompany = txtArrivalTrasnportCompany.Text.Trim();
            oBookingData.DepartureVehicleNo = txtDepartureVehicleNo.Text.Trim();
            oBookingData.DepartureTransportCompany = txtDepartureTransportCompany.Text.Trim();
            #endregion Confirmation Data

            SaveActivitiesAndMealPlans(out oBActivityDTO, out oBMealPlanDTO);

            oBookingManager = new BookingServices();
            if (btnConfirmBooking.Text == "Update Confirmation")
            {
                Session["confirmDet"] = Bookconfirmformail(iBookingId);
            }
            else
            {
                Session["confirmDet"] = null;
            }


            if (Convert.ToBoolean(ViewState["atleastonewaitlisted"]) == true && Convert.ToBoolean(ViewState["chartered"]) == true)
            {
                System.Web.UI.ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "AlertBox", "alert('All rooms must be available for Chartered Booking confirmation');", true);
            }
            else
            {
                oBookingManager.ConfirmBooking(oBookingData);


                if (oBMealPlanDTO != null)
                    oBookingManager.AddBookingMealPlan(oBMealPlanDTO);
                if (oBActivityDTO != null)
                    oBookingManager.AddBookingActivities(oBActivityDTO);
                oBMealPlanDTO = null;
                oBActivityDTO = null;
                ClearSessionVariables();
                base.DisplayAlert("The tour has been confirmed successfully");
                Response.Redirect("afterBookingactions.aspx?bid=" + iBookingId + "&bstatus=confirmed");
            }

            //  ViewState["atleastonewaitlisted"] = null;
            // ViewState["chartered"] = null;
        }
    }




    private void CancelBooking()
    {
        if (base.ValidateIfCommandAllowed("Booking", ENums.PageCommand.Cancel))
        {
            BookingServices oBookingManager;
            oBookingManager = new BookingServices();
            oBookingManager.CancelBooking(iBookingId);
            ClearSessionVariables();
            Response.Redirect("afterBookingactions.aspx?bid=" + iBookingId + "&bstatus=cancelled");
        }

    }

    private bool ValidateValues()
    {
        DateTime vdt = DateTime.MinValue;
        string dates;
        if (txtExchangeOrderNo.Text.Trim() == "")
        {
            lblErrorMsg.Text = "Please enter the Exchange Order No.";
            return false;
        }
        if (txtVoucherDate.Text.Trim() == "")
        {
            lblErrorMsg.Text = "Please enter the voucher date.";
            return false;
        }

        DateTime.TryParse(txtVoucherDate.Text, out vdt);
        if (vdt == DateTime.MinValue)
        {
            lblErrorMsg.Text = "Incorrect voucher date.";
            return false;
        }

        dates = txtArrivalDate.Text.ToString();
        DateTime.TryParse(dates, out vdt);
        if (vdt == DateTime.MinValue)
        {
            lblErrorMsg.Text = "Incorrect Arrival Date.";
            return false;
        }
        //if (txtArrivalTransport.Text.Trim() == "")
        //{
        //    lblErrorMsg.Text = "Please select the Transport(arrival) method.";
        //    return false;
        //}
        //if (Convert.ToInt32(ddlArrivalCity.SelectedValue) <= 0)
        //{
        //    lblErrorMsg.Text = "Please select the Arrival City.";
        //    return false;
        //}


        dates = "";
        dates = txtDepartureDate.Text.ToString();

        DateTime.TryParse(dates, out vdt);
        if (vdt == DateTime.MinValue)
        {
            lblErrorMsg.Text = "Incorrect departure Date.";
            return false;
        }
        //if (txtDepartureTransport.Text.Trim() == "")
        //{
        //    lblErrorMsg.Text = "Please select the departure City.";
        //    return false;
        //}
        //if (Convert.ToInt32(ddlDepartureCity.SelectedValue) <= 0)
        //{
        //    lblErrorMsg.Text = "Please select the departure City.";
        //    return false;
        //}
        return true;
    }

    /// <summary>
    /// This method implement the logic, that whatever is the meal plan, only breakfast is allowed.
    /// </summary>
    /// <param name="ControlIdDatePart"></param>
    /// <returns></returns>
    private bool IsCheckInDateControl(string ControlIdDatePart)
    {
        string dd, mm, yyyy;
        DateTime ControlDate;
        DateTime CheckInDate;
        yyyy = ControlIdDatePart.Substring(0, 4);
        mm = ControlIdDatePart.Substring(4, 2);
        dd = ControlIdDatePart.Substring(6, 2);
        DateTime.TryParse(yyyy + "-" + mm + "-" + dd, out ControlDate);
        DateTime.TryParse(txtStartDate.Text, out CheckInDate);
        if (ControlDate != DateTime.MinValue)
        {
            if (CheckInDate == ControlDate)
                return true;
        }
        return false;
    }

    /// <summary>
    /// This method implement the logic, that whatever is the meal plan, only breakfast is allowed.
    /// </summary>
    /// <param name="ControlIdDatePart"></param>
    /// <returns></returns>
    private bool IsCheckOutDateControl(string ControlIdDatePart)
    {
        string dd, mm, yyyy;
        DateTime ControlDate;
        DateTime CheckOutDate;
        yyyy = ControlIdDatePart.Substring(0, 4);
        mm = ControlIdDatePart.Substring(4, 2);
        dd = ControlIdDatePart.Substring(6, 2);
        DateTime.TryParse(yyyy + "-" + mm + "-" + dd, out ControlDate);
        DateTime.TryParse(txtEndDate.Text, out CheckOutDate);
        if (ControlDate != DateTime.MinValue)
        {
            if (CheckOutDate == ControlDate)
                return true;
        }
        return false;
    }

    private void ClearSessionVariables()
    {
        SessionServices.BookingConfirmation_TotalRoomsBooked = null;
        SessionServices.BookingConfirmation_BookingMealPlanDTO = null;
        SessionServices.BookingConfirmation_BookingMealPlanDTO = null;
        SessionServices.BookingConfirmation_AccomodationActivityData = null;
    }

    #endregion
}
