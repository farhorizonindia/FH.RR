using FarHorizon.Reservations.Bases.BasePages;
using FarHorizon.Reservations.BusinessServices;
using FarHorizon.Reservations.Common;
using FarHorizon.Reservations.Common.DataEntities.Client;
using FarHorizon.Reservations.Common.DataEntities.Masters;
using FarHorizon.Reservations.MasterServices;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

public partial class ClientUI_SeriesBooking : ClientBasePage
{
    const int ROOMCOLUMNSTART = 4;
    static Hashtable ChangedSeriesDates;

    #region Control Defined Functions
    protected void Page_Load(object sender, EventArgs e)
    {
        int iSeriesId = 0;
        Table t = null;
        string sCtrlName = "";
        AddAttributes();

        if (Request.QueryString["sid"] != null && Request.QueryString["sid"] != "")
        {
            int.TryParse(Request.QueryString["sid"].ToString(), out iSeriesId);
        }

        if (!IsPostBack)
        {
            ChangedSeriesDates = new Hashtable();

            FillAccomodationTypes();
            FillAccomodations();
            FillAgents();
            FillDropDowns();

            if (iSeriesId > 0)
            {
                PrepareSeries(iSeriesId);
            }

            btnSaveSeries.Visible = false;
            ClearSessionVariables();
        }
        else
        {
            sCtrlName = GetPostBackControlID();
            if (sCtrlName != "ddlAccomType")
            {
                if (sCtrlName != "ddlAccomName")
                {
                    int accomodationId = Convert.ToInt32(ddlAccomName.SelectedItem.Value);
                    PrepareRoomCategoriesAndTypes(accomodationId);
                }
                if (sCtrlName != "btnGetRoomsForSeries")
                {
                    if (iSeriesId > 0)
                    {
                        PrepareSeries(iSeriesId);
                    }
                    else { PrepareSeries(false); }
                }
            }
        }
        lblRegenerateSeries.Text = "You have made changes to series dates, please click on 'Re-generate Series' button to get the correct status of rooms.";
    }

    protected void btnSaveSeries_Click(object sender, EventArgs e)
    {
        try
        {
            SaveSeries();
        }
        catch (Exception exp)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Error", "javascript:alert('" + exp.Message.ToString() + "')", true);
        }
    }

    //protected void ddlAccomType_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    int AccomTypeID = Convert.ToInt32(ddlAccomType.SelectedItem.Value);
    //    FillAccomodations(AccomTypeID);
    //}

    protected void ddlAccomType_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillAccomodations();
    }

    protected void ddlAccomName_SelectedIndexChanged(object sender, EventArgs e)
    {
        int accomodationId = Convert.ToInt32(ddlAccomName.SelectedItem.Value);
        PrepareRoomCategoriesAndTypes(accomodationId);
    }

    protected void btnGenerateSeries_Click(object sender, EventArgs e)
    {
        ChangedSeriesDates.Clear();
        PrepareSeries(false);
        btnSaveSeries.Visible = true;
    }

    protected void btnRegenerateSeries_Click(object sender, EventArgs e)
    {
        try
        {
            PrepareSeries(true);
            btnSaveSeries.Visible = true;
            ChangedSeriesDates.Clear();
        }
        catch (Exception exp)
        {
            btnSaveSeries.Visible = false;
            pnlRegenSeries.Attributes["style"] = "display:block";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Error", "javascript:alert('" + exp.Message.ToString() + "')", true);
        }
    }
    #endregion

    #region Helper Methods

    private void AddAttributes()
    {
        btnGenerateSeries.Attributes.Add("onclick", "return validateBeforeGeneratingSeries();");
        btnSaveSeries.Attributes.Add("onclick", "return validateBeforeSavingSeries();");

        ddlAccomType.Attributes.Add("onchange", "setValueToHiddenField('AccomType');");
        ddlAccomName.Attributes.Add("onchange", "setValueToHiddenField('Accom');");
        txtFirstCheckInDate.Attributes.Add("onchange", "setValueToHiddenField('FirstCheckInDate');");
        ddlNoOfNights.Attributes.Add("onchange", "setValueToHiddenField('NoOfNights');");
        ddlGap.Attributes.Add("onchange", "setValueToHiddenField('Gap');");
        ddlNoOfDeps.Attributes.Add("onchange", "setValueToHiddenField('Departures');");
    }

    private void FillDropDowns()
    {
        ListItem l = new ListItem("Choose", "0");
        ddlNoOfNights.Items.Add(l);
        for (int i = 0; i < 31; i++)
        {
            l = new ListItem(Convert.ToString(i + 1), Convert.ToString(i + 1));
            ddlNoOfNights.Items.Add(l);
        }

        l = new ListItem("Choose", "0");
        ddlGap.Items.Add(l);
        for (int i = 0; i < 50; i++)
        {
            l = new ListItem(Convert.ToString(i + 1), Convert.ToString(i + 1));
            ddlGap.Items.Add(l);
        }

        l = new ListItem("Choose", "0");
        ddlNoOfDeps.Items.Add(l);
        for (int i = 1; i < 26; i++)
        {
            l = new ListItem(Convert.ToString(i + 1), Convert.ToString(i + 1));
            ddlNoOfDeps.Items.Add(l);
        }
    }

    private AccomTypeDTO[] GetAccomodationTypeDetails()
    {
        //Filling the Object which contains the Accomodation type and also all the respective accomodations
        //This is saved in the session and then the Drop downs are filled.

        AccomodationTypeMaster objATM;
        if (SessionServices.Series_AccomodationData == null)
        {
            objATM = new AccomodationTypeMaster();
            SessionServices.Series_AccomodationData = objATM.GetAccomTypeWithAccomDetails(0);
        }
        objATM = null;
        return SessionServices.Series_AccomodationData;
    }

    private void FillAccomodationTypes()
    {
        ddlAccomType.Items.Clear();
        SortedList slAccomData = new SortedList();
        AccomTypeDTO[] oAccomTypeData = GetAccomodationTypeDetails();
        slAccomData.Add("0", "Choose Accom Type");
        if (oAccomTypeData != null)
        {
            for (int i = 0; i < oAccomTypeData.Length; i++)
            {
                if (!slAccomData.ContainsKey(Convert.ToString(oAccomTypeData[i].AccomodationTypeId)))
                    slAccomData.Add(Convert.ToString(oAccomTypeData[i].AccomodationTypeId), Convert.ToString(oAccomTypeData[i].AccomodationType));
            }
            ddlAccomType.DataSource = slAccomData;
            ddlAccomType.DataTextField = "value";
            ddlAccomType.DataValueField = "key";
            ddlAccomType.DataBind();
        }
    }

    private void FillAccomodations()
    {
        ddlAccomName.Items.Clear();
        int AccomTypeId = Convert.ToInt32(ddlAccomType.SelectedValue);
        SortedList slAccomData = new SortedList();
        AccomTypeDTO[] oAccomTypeData = GetAccomodationTypeDetails();
        slAccomData.Add("0", "Choose Accomodation");
        if (oAccomTypeData != null)
        {
            for (int i = 0; i < oAccomTypeData.Length; i++)
            {
                if (oAccomTypeData[i].Accomodations != null)
                {
                    for (int j = 0; j < oAccomTypeData[i].Accomodations.Length; j++)
                    {
                        if (AccomTypeId == oAccomTypeData[i].AccomodationTypeId)
                            slAccomData.Add(Convert.ToString(oAccomTypeData[i].Accomodations[j].AccomodationId), Convert.ToString(oAccomTypeData[i].Accomodations[j].AccomodationName));
                    }
                }
            }
            ddlAccomName.DataSource = slAccomData;
            ddlAccomName.DataTextField = "value";
            ddlAccomName.DataValueField = "key";
            ddlAccomName.DataBind();
        }
    }

    private void FillAgents()
    {
        AgentMaster oAgentMaster = new AgentMaster();
        AgentDTO[] oAgentData = oAgentMaster.GetData();
        ListItemCollection li = new ListItemCollection();
        ListItem l = new ListItem("Choose Agent", "0");
        ddlAgent.Items.Insert(0, l);
        if (oAgentData != null)
        {
            for (int i = 0; i < oAgentData.Length; i++)
            {
                l = new ListItem(oAgentData[i].AgentName.ToString(), oAgentData[i].AgentId.ToString());
                ddlAgent.Items.Insert(i + 1, l);
            }
        }
    }

    private void PrepareRoomCategoriesAndTypes(int accomodationId)
    {
        SeriesBookingServices oSeriesRooms = new SeriesBookingServices();
        SeriesDTO[] oSeriesDTO = oSeriesRooms.GetRoomsForSeries(accomodationId);
        Table tblMain = new Table();
        string sPrevRoomCategory = "";
        TableRow tr = null;
        TableCell tc = null;
        CheckBox chk = null;
        DropDownList ddl = null;
        Label lbl = null;
        ListItem l = null;
        if (oSeriesDTO != null)
        {
            for (int i = 0; i < oSeriesDTO.Length; i++)
            {
                if (sPrevRoomCategory != oSeriesDTO[i].RoomCategory.Trim().ToString())
                {
                    sPrevRoomCategory = oSeriesDTO[i].RoomCategory.Trim().ToString();
                    tr = new TableRow();
                    tc = new TableCell();
                    tc.Attributes.Add("class", "category");
                    tc.ColumnSpan = 4;
                    lbl = new Label();
                    lbl.Attributes.Add("class", "lblCategory");
                    lbl.Text = sPrevRoomCategory;
                    tc.Controls.Add(lbl);
                    tr.Cells.Add(tc);
                    tblMain.Rows.Add(tr);
                }
                tr = new TableRow();
                tc = new TableCell();
                chk = new CheckBox();
                chk.Attributes.Add("class", "chkCatType");
                chk.ID = Constants.CHECKBOX_CAT_TYPE + "*" + oSeriesDTO[i].RoomCategoryId.ToString() + "*" + oSeriesDTO[i].RoomTypeId.ToString();
                tc.Controls.Add(chk);
                tr.Cells.Add(tc);

                tc = new TableCell();
                lbl = new Label();
                lbl.Attributes.Add("class", "lblType");
                lbl.Text = oSeriesDTO[i].RoomType.ToString();
                tc.Controls.Add(lbl);
                tr.Cells.Add(tc);

                tc = new TableCell();

                ddl = new DropDownList();
                ddl.Attributes.Add("class", "select");
                ddl.ID = Constants.DROPDOWN_CAT_TYPE + "*" + oSeriesDTO[i].RoomCategoryId.ToString() + "*" + oSeriesDTO[i].RoomTypeId.ToString();
                ddl.Items.Insert(0, "Choose");
                for (int j = 1; j <= oSeriesDTO[i].TotalRooms; j++)
                {
                    l = new ListItem(j.ToString(), j.ToString());
                    ddl.Items.Insert(j, l);
                }
                tc.Controls.Add(ddl);
                tr.Cells.Add(tc);

                tblMain.Rows.Add(tr);
            }
            AddRoomsToPanel(tblMain);
        }
    }

    private TableRow PrepareRowRAW(TableRow DataRow)
    {
        TableRow tr = new TableRow();
        TableCell tc = null;
        TableCell tcNavigator;
        HtmlGenericControl div = new HtmlGenericControl("div");
        string[] sids;
        string rcId, rc, rtId, rt, IdPostFix;

        tc = new TableCell();
        div.ID = "divSelect";
        div.InnerHtml = "Check All";
        div.Attributes.Add("onclick", "selectCheckBoxes()");
        div.Attributes.Add("class", "divSelect");
        tc.Controls.Add(div);

        tc.Attributes.Add("class", "rawLabels");
        tr.Cells.Add(tc);

        tc = new TableCell();
        tc.Attributes.Add("class", "rawLabels");
        tc.Text = "Check In";
        tr.Cells.Add(tc);

        tc = new TableCell();
        tc.Attributes.Add("class", "rawLabels");
        tc.Text = "Check Out";
        tr.Cells.Add(tc);

        tc = new TableCell();
        tc.Attributes.Add("class", "rawLabels");
        tc.Text = "Proposed Booking";
        tr.Cells.Add(tc);

        //i is started from 4 as 3 column represents R and then AW respectively

        for (int i = ROOMCOLUMNSTART; i < DataRow.Cells.Count; i++)
        {
            tcNavigator = DataRow.Cells[i];
            sids = tcNavigator.ID.Split('*');
            rcId = sids[3]; //Getting the Room Category Id
            rtId = sids[4]; //Getting the Room Type Id

            rc = sids[5]; //Getting the Room Category;
            rt = sids[6]; // Getting the Room Type
            IdPostFix = rcId + "*" + rtId + "*" + rc + "*" + rt;
            tc = new TableCell();
            if (tcNavigator.ID.StartsWith(Constants.ROOM_REQUIRED_CELL))
            {
                tc.Text = "R";
                tc.Attributes.Add("class", "requestedHeader");
                tc.ID = Constants.ROOM_REQUIRED_CELL + IdPostFix;
            }
            if (tcNavigator.ID.StartsWith(Constants.ROOM_AVAILABLE_CELL))
            {
                tc.Text = "A";
                tc.Attributes.Add("class", "availableHeader");
                tc.ID = Constants.ROOM_AVAILABLE_CELL + IdPostFix;
            }
            if (tcNavigator.ID.StartsWith(Constants.ROOM_WAITLISTED_CELL))
            {
                tc.Text = "W";
                tc.Attributes.Add("class", "waitlistedHeader");
                tc.ID = Constants.ROOM_WAITLISTED_CELL + IdPostFix;
            }
            if (tcNavigator.ID.StartsWith(Constants.ROOM_BOOKED_CELL))
            {
                tc.Text = "B";
                tc.Attributes.Add("class", "bookedHeader");
                tc.ID = Constants.ROOM_BOOKED_CELL + IdPostFix;
            }
            tr.Cells.Add(tc);

        }
        return tr;
    }

    private TableRow PrepareRowTypes(TableRow RowRAW)
    {
        TableRow tr = new TableRow();
        TableCell tc = null;
        TableCell tcNavigator;
        string[] sids;

        for (int i = 0; i < ROOMCOLUMNSTART; i++)
        {
            tc = new TableCell();
            tr.Cells.Add(tc);
        }

        for (int i = ROOMCOLUMNSTART; i < RowRAW.Cells.Count; i++)
        {
            tcNavigator = RowRAW.Cells[i];
            if (tcNavigator.ID == null)
                tc.ColumnSpan = tc.ColumnSpan + 1;
            else
            {
                sids = tcNavigator.ID.Split('*');
                if (tcNavigator.ID.StartsWith(Constants.ROOM_REQUIRED_CELL))
                {
                    tc = new TableCell();
                    tc.Attributes.Add("class", "typeCell");
                    tc.Text = sids[4];
                    tc.ID = sids[1] + "*" + sids[2] + "*" + sids[3] + "*" + sids[4];
                    tc.ColumnSpan = 4;
                    tr.Cells.Add(tc);

                    tc = new TableCell();
                    tc.Attributes.Add("class", "categorySeperator");
                    tc.ID = Constants.SEPERATOR + "*" + sids[1] + "*" + sids[2];
                    tr.Cells.Add(tc);
                }
            }
        }
        return tr;
    }

    private TableRow PrepareRowCategories(TableRow RowTypes)
    {
        TableRow tr = new TableRow();
        TableCell tc = null;
        TableCell tcNavigator;
        string[] sids;
        string rc;

        for (int i = 0; i < ROOMCOLUMNSTART; i++)
        {
            tc = new TableCell();
            tr.Cells.Add(tc);
        }

        string sPrevCategory = "";
        for (int i = ROOMCOLUMNSTART; i < RowTypes.Cells.Count; i++)
        {
            tcNavigator = RowTypes.Cells[i];
            if (tcNavigator.ID != null)
            {
                sids = tcNavigator.ID.Split('*');
                if (sPrevCategory == "")
                {
                    sPrevCategory = sids[0];
                    rc = sids[2];
                    tc = new TableCell();
                    tc.ID = rc;
                    tc.Attributes.Add("class", "categoryCell");
                    tc.Text = GF.RecoverSpace(rc);
                    tc.Wrap = false;
                    tc.ColumnSpan = tcNavigator.ColumnSpan;
                }
                else if (sids[0] == sPrevCategory)
                {
                    tc.ColumnSpan += tcNavigator.ColumnSpan + 1;
                }
                else if (sids[0].StartsWith("sep"))
                {
                    continue;
                }
                else if (sids[0] != sPrevCategory)
                {
                    tr.Cells.Add(tc);

                    sPrevCategory = sids[0];
                    rc = sids[2];
                    tc = new TableCell();
                    tc.ID = Constants.SEPERATOR + "*" + sPrevCategory;
                    tc.Attributes.Add("class", "categorySeperator");
                    tr.Cells.Add(tc);

                    tc = new TableCell();
                    tc.Attributes.Add("class", "categoryCell");

                    tc.ID = sPrevCategory;
                    tc.Text = GF.RecoverSpace(rc);
                    tc.Wrap = false;
                    tc.ColumnSpan = tcNavigator.ColumnSpan;
                }
            }
        }
        tr.Cells.Add(tc);
        return tr;
    }

    private TableRow PrepareDataRow(Hashtable CategoryList, int departureNo)
    {
        return PrepareDataRow(CategoryList, false, departureNo);
    }

    private TableRow PrepareDataRow(Hashtable CategoryList, bool ExistingSeries, int departureNo)
    {
        TableRow tr = new TableRow();
        TableCell tc = null;
        CheckBox chk = null;
        TextBox txtCheckIn = null;
        TextBox txtCheckOut = null;
        CheckBox chkProposedBooking = null;
        Button btn = null;
        bool bDatesAdded = false;
        SeriesDetailDTO oSeriesDetailDTO = new SeriesDetailDTO();
        if (CategoryList == null)
            return null;
        ICollection Catlist = CategoryList.Keys;
        string IDPostFix = "", textBoxId = "", buttonId = "";

        int cntrlCntr = 0;
        foreach (string Category in Catlist)
        {
            oSeriesDetailDTO = (SeriesDetailDTO)CategoryList[Category];
            
            IDPostFix = departureNo + "*" + cntrlCntr;
            cntrlCntr++;

            #region Add Date Cells
            if (bDatesAdded == false)
            {
                #region Checkbox Cell
                tc = new TableCell();
                tc.Attributes.Add("class", "checkboxCells");
                chk = new CheckBox();
                chk.ID = Constants.CHECKBOX_BOOKING + "*" + IDPostFix;
                /// Setting the checkbox checked status, 
                /// if it is a new booking then it will be unchecked, else it will be checked.
                chk.Checked = ExistingSeries;
                tc.Controls.Add(chk);
                tr.Cells.Add(tc);
                #endregion

                #region Check In Date Cell
                tc = new TableCell();
                tc.Attributes.Add("class", "dateCells");
                txtCheckIn = new TextBox();
                txtCheckIn.Attributes.Add("class", "input");
                textBoxId = Constants.TEXTBOX_CHECKINDATE + "*" + IDPostFix;
                txtCheckIn.ID = textBoxId;
                txtCheckIn.Text = GF.GetDD_MMM_YYYY(oSeriesDetailDTO.CheckIn, false);
                //tc.Text = GF.GetDD_MMM_YYYY(oSeriesDetailDTO.CheckIn, false);
                tc.Controls.Add(txtCheckIn);
                btn = new Button();
                btn.Text = "...";
                btn.Attributes.Add("class", "datebutton");
                buttonId = Constants.BUTTON_CHECKINDATE + "*" + IDPostFix;
                btn.ID = buttonId;
                btn.Attributes.Add("onclick", "return setupCalendar('" + textBoxId + "','" + buttonId + "')");
                btn.Attributes.Add("onfocus", "return setupCalendar('" + textBoxId + "','" + buttonId + "')");
                tc.Controls.Add(btn);
                //tc.ID = oSeriesDetailDTO.CheckIn.ToShortDateString().ToString();
                tr.Cells.Add(tc);
                #endregion

                #region Check Out Date Cell
                tc = new TableCell();
                tc.Attributes.Add("class", "dateCells");
                txtCheckOut = new TextBox();
                txtCheckOut.Attributes.Add("class", "input");
                textBoxId = Constants.TEXTBOX_CHECKOUTDATE + "*" + IDPostFix;
                txtCheckOut.ID = textBoxId;
                txtCheckOut.Text = GF.GetDD_MMM_YYYY(oSeriesDetailDTO.CheckOut, false);
                //tc.Text = GF.GetDD_MMM_YYYY(oSeriesDetailDTO.CheckIn, false);
                tc.Controls.Add(txtCheckOut);
                btn = new Button();
                btn.Text = "...";
                btn.Attributes.Add("class", "datebutton");
                buttonId = Constants.BUTTON_CHECKOUTDATE + "*" + IDPostFix;
                btn.ID = buttonId;
                btn.Attributes.Add("onclick", "return setupCalendar('" + textBoxId + "','" + buttonId + "')");
                btn.Attributes.Add("onfocus", "return setupCalendar('" + textBoxId + "','" + buttonId + "')");
                tc.Controls.Add(btn);
                //tc.ID = oSeriesDetailDTO.CheckIn.ToShortDateString().ToString();
                tr.Cells.Add(tc);
                #endregion

                txtCheckIn.Attributes.Add("onchange", "return fillCheckOutDate('" + txtCheckIn.ClientID + "','" + txtCheckOut.ClientID + "');");

                #region Proposed Booking Cell
                tc = new TableCell();
                tc.Attributes.Add("class", "checkboxCells");
                chkProposedBooking = new CheckBox();
                chkProposedBooking.Attributes.Add("class", "");
                chkProposedBooking.ID = Constants.CHECKBOX_PROPOSED_BOOKING + "*" + IDPostFix;
                chkProposedBooking.Text = "Proposed Booking";
                chkProposedBooking.Checked = oSeriesDetailDTO.ProposedBooking;
                tc.Controls.Add(chkProposedBooking);
                tr.Cells.Add(tc);
                #endregion
                bDatesAdded = true;
            }
            #endregion Add Date Cells

            IDPostFix += "*";
            IDPostFix += oSeriesDetailDTO.RoomCategoryId.ToString() + "*";
            IDPostFix += oSeriesDetailDTO.RoomTypeId.ToString() + "*";
            IDPostFix += GF.ReplaceSpace(oSeriesDetailDTO.RoomCategory) + "*";
            IDPostFix += GF.ReplaceSpace(oSeriesDetailDTO.RoomType);

            #region Add Room Count Cells
            tc = new TableCell();
            tc.Attributes.Add("class", "requested");
            tc.Text = oSeriesDetailDTO.Requested.ToString();
            tc.ID = Constants.ROOM_REQUIRED_CELL + IDPostFix;
            tr.Cells.Add(tc);

            tc = new TableCell();
            tc.Attributes.Add("class", "available");
            if (ExistingSeries)
                tc.Text = "";
            else
                tc.Text = oSeriesDetailDTO.Available.ToString();
            tc.ID = Constants.ROOM_AVAILABLE_CELL + IDPostFix;
            tr.Cells.Add(tc);

            tc = new TableCell();
            tc.Attributes.Add("class", "waitlisted");
            tc.Text = oSeriesDetailDTO.Waitlisted.ToString();
            tc.ID = Constants.ROOM_WAITLISTED_CELL + IDPostFix;
            tr.Cells.Add(tc);

            tc = new TableCell();
            tc.Attributes.Add("class", "booked");
            //if (oSeriesDetailDTO.Requested > oSeriesDetailDTO.Available)
            //    tc.Text = "0";
            //else
            tc.Text = Convert.ToString(oSeriesDetailDTO.Requested - oSeriesDetailDTO.Waitlisted);
            tc.ID = Constants.ROOM_BOOKED_CELL + IDPostFix;
            tr.Cells.Add(tc);

            tc = new TableCell();
            tc.Attributes.Add("class", "categorySeperator");
            tc.Text = string.Empty;
            tc.ID = Constants.SEPERATOR + "*" + IDPostFix;
            tr.Cells.Add(tc);

            #endregion Add Room Count Cells
        }

        if (oSeriesDetailDTO.BookingId != 0)
        {
            tc = new TableCell();
            tc.ID = "BOOKING" + "*" + IDPostFix;
            HyperLink hl = new HyperLink();
            hl.Target = "_blank";
            hl.NavigateUrl = "Booking.aspx?bid=" + oSeriesDetailDTO.BookingId.ToString();
            hl.Text = "Booking Code: " + oSeriesDetailDTO.BookingCode + " - Booking Ref: " + oSeriesDetailDTO.BookingRef;
            tc.Controls.Add(hl);
            //tc.Text = "<a href=Booking.aspx?bid=" + oSeriesDetailDTO.BookingId.ToString() + ">Show Booking</a>";
            tr.Cells.Add(tc);
        }
        return tr;
    }

    private void SaveSeries()
    {
        SeriesDTO oSeriesDTO = new SeriesDTO();
        BookingDTO oBookingDTO = new BookingDTO();

        BookingServices oBoookingManager = new BookingServices();
        SeriesBookingServices oSM = new SeriesBookingServices();
        List<SeriesDetailDTO> oSeriesDetails;
        List<BookingOfSeriesDTO> BookingOfSeriesList = null;

        int iSeriesID = 0;
        bool bActionCompleted = false;

        if (base.ValidateIfCommandAllowed(Request.Url.AbsoluteUri, ENums.PageCommand.Add))
        {
            #region Prepare SeriesData
            oSeriesDTO = MapSeriesDataToObject();
            #endregion

            #region Prepare SeriesDetailData
            oSeriesDetails = MapSeriesDetailToObject();
            #endregion

            #region Prepare BookingData
            MapBookingMasterandDetailToObject(oSeriesDetails, out BookingOfSeriesList);

            if (BookingOfSeriesList == null)
            {
                base.DisplayAlert("The series cannot be added, please click on 'Generate Series' and try again.");
            }
            #endregion

            bActionCompleted = oSM.AddSeriesBooking(oSeriesDTO, oSeriesDetails, BookingOfSeriesList, out iSeriesID);

            if (bActionCompleted == true)
            {
                base.DisplayAlert("The series has been added successfully.");
            }
            else
            {
                base.DisplayAlert("The series has not been added successfully.");
            }
            if (bActionCompleted == true)
            {
                ClearSessionVariables();
                Response.Redirect("afterBookingSeriesactions.aspx?sid=" + iSeriesID + "&bstatus=booked");
            }
        }
    }

    private SeriesDTO MapSeriesDataToObject()
    {
        SeriesDTO oSeriesDTO = new SeriesDTO();
        oSeriesDTO.AccomTypeID = Convert.ToInt32(ddlAccomType.SelectedValue);
        oSeriesDTO.AccomodationID = Convert.ToInt32(ddlAccomName.SelectedValue);
        oSeriesDTO.SeriesName = txtSeriesName.Text.ToString();
        oSeriesDTO.AgentId = Convert.ToInt32(ddlAgent.SelectedValue);
        oSeriesDTO.NoOfNights = Convert.ToInt32(ddlNoOfNights.SelectedValue);
        oSeriesDTO.GAP = Convert.ToInt32(ddlGap.SelectedValue);
        oSeriesDTO.NoOfDepartures = Convert.ToInt32(ddlNoOfDeps.SelectedValue);
        oSeriesDTO.SeriesStartDate = Convert.ToDateTime(txtFirstCheckInDate.Text.ToString());
        return oSeriesDTO;
    }

    private List<SeriesDetailDTO> MapSeriesDetailToObject()
    {
        string[] sFormControls = Request.Form.AllKeys;
        string id = string.Empty;
        Control ctrl;
        TextBox txt;
        CheckBox chk;
        TableCell tc;
        TableRow ParentRow = null;
        DateTime dt;
        int RoomCount = 0;
        string[] IdSplit = null;

        List<SeriesDetailDTO> oSeriesDetailList = new List<SeriesDetailDTO>();
        SeriesDetailDTO oSeriesDetailDTO = null;

        for (int i = 0; i < sFormControls.Length; i++)
        {
            if (sFormControls[i].StartsWith(Constants.CHECKBOX_BOOKING))
            {
                //oSeriesDetailDTO = new SeriesDetailDTO();                               
                #region Get Booking Cells
                ctrl = FindControl(sFormControls[i].ToString());
                if (ctrl != null)
                {
                    ParentRow = (TableRow)ctrl.Parent.Parent;
                }
                if (ParentRow != null)
                {
                    for (int j = 0; j < ParentRow.Controls.Count; j++)
                    {
                        id = ParentRow.Controls[j].ID;
                        if (id != null)
                        {
                            ctrl = ParentRow.Controls[j];
                            if (ctrl != null)
                            {
                                #region Get Room Requested
                                if (id.StartsWith(Constants.ROOM_REQUIRED_CELL))
                                {
                                    tc = (TableCell)ctrl;
                                    if (tc != null)
                                        Int32.TryParse(tc.Text, out RoomCount);

                                    oSeriesDetailDTO = new SeriesDetailDTO();
                                    IdSplit = tc.ID.Split('*');

                                    oSeriesDetailDTO.Requested = RoomCount;
                                    oSeriesDetailDTO.RoomCategoryId = Convert.ToInt32(IdSplit[3].ToString());
                                    oSeriesDetailDTO.RoomTypeId = Convert.ToInt32(IdSplit[4].ToString());

                                    #region Get Check In Date
                                    id = sFormControls[i].Replace(Constants.CHECKBOX_BOOKING, Constants.TEXTBOX_CHECKINDATE);
                                    ctrl = FindControl(id);
                                    if (ctrl != null)
                                    {
                                        txt = (TextBox)ctrl;
                                        DateTime.TryParse(txt.Text, out dt);
                                        oSeriesDetailDTO.CheckIn = dt;
                                    }
                                    #endregion

                                    #region Get Check Out Date
                                    id = sFormControls[i].Replace(Constants.CHECKBOX_BOOKING, Constants.TEXTBOX_CHECKOUTDATE);
                                    ctrl = FindControl(id);
                                    if (ctrl != null)
                                    {
                                        txt = (TextBox)ctrl;
                                        DateTime.TryParse(txt.Text, out dt);
                                        oSeriesDetailDTO.CheckOut = dt;
                                    }
                                    #endregion

                                    #region Get Proposed Booking
                                    id = sFormControls[i].Replace(Constants.CHECKBOX_BOOKING, Constants.CHECKBOX_PROPOSED_BOOKING);
                                    ctrl = FindControl(id);
                                    if (ctrl != null)
                                    {
                                        chk = (CheckBox)ctrl;
                                        oSeriesDetailDTO.ProposedBooking = chk.Checked;
                                    }
                                    #endregion
                                }
                                #endregion
                                #region Get Room Available
                                if (id.StartsWith(Constants.ROOM_AVAILABLE_CELL))
                                {
                                    tc = (TableCell)ctrl;
                                    if (tc != null)
                                        Int32.TryParse(tc.Text, out RoomCount);
                                    oSeriesDetailDTO.Available = RoomCount;
                                }
                                #endregion
                                #region Get Room Waitlisted
                                if (id.StartsWith(Constants.ROOM_WAITLISTED_CELL))
                                {
                                    tc = (TableCell)ctrl;
                                    if (tc != null)
                                        Int32.TryParse(tc.Text, out RoomCount);
                                    oSeriesDetailDTO.Waitlisted = RoomCount;
                                }
                                #endregion
                                #region Get Room Booked
                                if (id.StartsWith(Constants.ROOM_BOOKED_CELL))
                                {
                                    tc = (TableCell)ctrl;
                                    if (tc != null)
                                        Int32.TryParse(tc.Text, out RoomCount);
                                    oSeriesDetailDTO.Confirmed = RoomCount;
                                }
                                #endregion
                                #region Adding to the List
                                if (id.StartsWith(Constants.SEPERATOR))
                                {
                                    if (oSeriesDetailDTO != null)
                                        oSeriesDetailList.Add(oSeriesDetailDTO);
                                }
                                #endregion Adding to the List
                            }
                        }
                    }
                    //if (oSeriesDetailDTO != null)
                    //oSeriesDetailList.Add(oSeriesDetailDTO);
                }
                #endregion

            }
        }
        return oSeriesDetailList;
    }

    private void MapBookingMasterandDetailToObject(List<SeriesDetailDTO> oSeriesDetails, out List<BookingOfSeriesDTO> BookingOfSeriesList)
    {
        //ICollection colRCAT;
        //Hashtable htRCAT;
        List<BookingOfSeriesDTO> BOSList = null;
        List<BookingDatesWithBookedRoomsDTO> oBookingDatesWithBookedRoomsList = null;
        BookingOfSeriesDTO BookingOfSeriesDTO = null;
        BookingOfSeriesDTO FinalizedBooking = null;
        BookingDTO CurrentBooking;
        bool found = false;

        //htRCAT = GetSelectedRoomCategories();
        //colRCAT = htRCAT.Keys;
        BOSList = new List<BookingOfSeriesDTO>();

        foreach (SeriesDetailDTO oSD in oSeriesDetails)
        {
            CurrentBooking = SetBookingData(oSD);
            if (SessionServices.Series_BookedRooms_WithDates != null)
                oBookingDatesWithBookedRoomsList = SessionServices.Series_BookedRooms_WithDates;
            else
            {
                BookingOfSeriesList = null;
                return;
            }

            foreach (BookingDatesWithBookedRoomsDTO oBDWBR in oBookingDatesWithBookedRoomsList)
            {
                if (DateTime.Compare(oSD.CheckIn, oBDWBR.CheckInDate) == 0 && DateTime.Compare(oSD.CheckOut, oBDWBR.CheckOutDate) == 0)
                {
                    GetFinalizedRooms(oSD, CurrentBooking, oBDWBR.BookedRooms, out FinalizedBooking);
                    if (BOSList.Count == 0)
                    {
                        BookingOfSeriesDTO = new BookingOfSeriesDTO();
                        BookingOfSeriesDTO.Booking = FinalizedBooking.Booking;
                        BookingOfSeriesDTO.BookedRooms = FinalizedBooking.BookedRooms;
                        BookingOfSeriesDTO.WaitListedRooms = FinalizedBooking.WaitListedRooms;
                        BOSList.Add(BookingOfSeriesDTO);
                    }
                    else
                    {
                        found = false;
                        foreach (BookingOfSeriesDTO o in BOSList)
                        {
                            if (o.Booking.StartDate == FinalizedBooking.Booking.StartDate && o.Booking.EndDate == FinalizedBooking.Booking.EndDate)
                            {
                                found = true;
                                if (FinalizedBooking.BookedRooms.Count > 0)
                                    o.BookedRooms.Add(FinalizedBooking.BookedRooms[0]);
                                if (FinalizedBooking.WaitListedRooms.Count > 0)
                                {
                                    BookingOfSeriesDTO.Booking.BookingStatusId = 3;
                                    o.WaitListedRooms.Add(FinalizedBooking.WaitListedRooms[0]);
                                }
                                break;
                            }
                        }
                        if (found == false)
                        {
                            BookingOfSeriesDTO = new BookingOfSeriesDTO();
                            BookingOfSeriesDTO.Booking = FinalizedBooking.Booking;
                            BookingOfSeriesDTO.BookedRooms = FinalizedBooking.BookedRooms;
                            BookingOfSeriesDTO.WaitListedRooms = FinalizedBooking.WaitListedRooms;
                            BOSList.Add(BookingOfSeriesDTO);
                            break;
                        }
                    }
                    break;
                }
            }
        }
        BookingOfSeriesList = BOSList;
    }

    private void GetFinalizedRooms(SeriesDetailDTO SeriesDetail, BookingDTO Booking, List<BookedRooms> AllRooms, out BookingOfSeriesDTO FinalizedBooking)
    {
        int NoofRoomsBooked = 0; ;
        string RoomNo = string.Empty, RoomCategory = string.Empty, RoomType = string.Empty;

        BookingOfSeriesDTO FB = new BookingOfSeriesDTO();
        List<BookedRooms> BookedRoomsList = new List<BookedRooms>();
        BookingWaitListDTO BookingWaitListDTO = null;
        List<BookingWaitListDTO> BookingWaitlistList = new List<BookingWaitListDTO>();

        foreach (BookedRooms Room in AllRooms)
        {
            if (NoofRoomsBooked >= SeriesDetail.Confirmed)
                break;
            if (SeriesDetail.RoomCategoryId == Room.RoomCategoryId && SeriesDetail.RoomTypeId == Room.RoomTypeId)
            {
                RoomCategory = Room.RoomCategory;
                RoomType = Room.RoomType;
                if ((Room.BookingId == 0 && Room.RoomStatus == Constants.AVAILABLE) || (Room.BookingId == Booking.BookingId && Room.RoomStatus == Constants.BOOKED))
                {
                    Room.StartDate = Booking.StartDate;
                    Room.EndDate = Booking.EndDate;
                    Room.RoomStatus = 'B';
                    BookedRoomsList.Add(Room);
                    NoofRoomsBooked++;
                }
            }
        }

        if (SeriesDetail.Waitlisted > 0)
        {
            BookingWaitListDTO = new BookingWaitListDTO();
            BookingWaitListDTO.AccomId = Booking.AccomodationId;
            BookingWaitListDTO.BookingId = Booking.BookingId;
            BookingWaitListDTO.BookingRef = Booking.BookingReference;
            BookingWaitListDTO.BookingType = Constants.WAITLISTED;
            BookingWaitListDTO.RoomCategory = RoomCategory;
            BookingWaitListDTO.RoomCategoryId = SeriesDetail.RoomCategoryId;
            BookingWaitListDTO.RoomType = RoomType;
            BookingWaitListDTO.RoomTypeId = SeriesDetail.RoomTypeId;
            BookingWaitListDTO.No_Of_RoomsWaitListed = SeriesDetail.Waitlisted;
        }
        if (BookingWaitListDTO != null)
            BookingWaitlistList.Add(BookingWaitListDTO);
        FB.Booking = Booking;
        FB.BookedRooms = BookedRoomsList;
        FB.WaitListedRooms = BookingWaitlistList;

        FinalizedBooking = FB;
    }

    private BookingDTO SetBookingData(SeriesDetailDTO seriesDto)
    {
        BookingDTO oBookingData = new BookingDTO();
        oBookingData.BookingReference = txtSeriesName.Text.ToString();
        oBookingData.AccomodationTypeId = Convert.ToInt32(ddlAccomType.SelectedItem.Value);
        oBookingData.AccomodationId = Convert.ToInt32(ddlAccomName.SelectedItem.Value);
        oBookingData.AgentId = Convert.ToInt32(ddlAgent.SelectedItem.Value);
        oBookingData.NoOfNights = Convert.ToInt32(ddlGap.SelectedItem.Value);
        oBookingData.StartDate = seriesDto.CheckIn;
        oBookingData.EndDate = seriesDto.CheckOut;
        oBookingData.NoOfPersons = 0;
        oBookingData.BookingStatusId = 1;
        oBookingData.ProposedBooking = seriesDto.ProposedBooking;
        return oBookingData;
    }

    private SeriesBookingDates[] FillSeriesDates(bool withModifiedDates)
    {
        DateTime dtFirstDepDate;
        int iAccomTypeId, iAccomId, iNoOfDeps, iGap, iNoOfNights;
        DateTime.TryParse(txtFirstCheckInDate.Text, out dtFirstDepDate);
        Int32.TryParse(ddlNoOfDeps.SelectedItem.Value, out iNoOfDeps);
        int.TryParse(ddlAccomType.SelectedItem.Value, out iAccomTypeId);
        int.TryParse(ddlAccomName.SelectedItem.Value, out iAccomId);
        int.TryParse(ddlGap.SelectedItem.Value, out iGap);
        int.TryParse(ddlNoOfNights.SelectedItem.Value, out iNoOfNights);
        SeriesBookingDates[] oSeriesBookingDates;
        DateTime dtEndDate;        

        try
        {
            if (!withModifiedDates)
            {
                oSeriesBookingDates = new SeriesBookingDates[iNoOfDeps];
                for (int i = 0; i < iNoOfDeps; i++)
                {
                    oSeriesBookingDates[i] = new SeriesBookingDates();
                    oSeriesBookingDates[i].AccomodationId = iAccomId;
                    oSeriesBookingDates[i].StartDate = dtFirstDepDate;
                    dtEndDate = dtFirstDepDate.AddDays(iNoOfNights);
                    oSeriesBookingDates[i].EndDate = dtEndDate;
                    dtFirstDepDate = dtEndDate.AddDays(iGap);
                }                
            }
            else
            {
                Hashtable htDates = GetSeriesDates(withModifiedDates);

                oSeriesBookingDates = new SeriesBookingDates[htDates.Count];
                int ctr = 0;
                foreach (Object key in htDates.Keys)
                {
                    oSeriesBookingDates[ctr] = new SeriesBookingDates();
                    oSeriesBookingDates[ctr].AccomodationId = iAccomId;

                    DateTime.TryParse(key.ToString(), out dtFirstDepDate);
                    oSeriesBookingDates[ctr].StartDate = dtFirstDepDate;

                    DateTime.TryParse(htDates[key].ToString(), out dtEndDate);
                    oSeriesBookingDates[ctr].EndDate = dtEndDate;
                    ctr++;
                }
            }
            return oSeriesBookingDates;
        }
        catch (Exception exp)
        {
            throw exp;
        }
    }

    private Hashtable GetCellsDataPerRow(List<BookedRooms> BookedRoomsList, Hashtable RoomCategoryAndType, DateTime StartDate, DateTime EndDate)
    {
        SeriesDetailDTO oSeriesDetailDTO = new SeriesDetailDTO();
        Hashtable SelectedCategoryandType = new Hashtable();
        int iRoomReq = 0;
        if (BookedRoomsList == null)
            return null;

        foreach (BookedRooms BookedRooms in BookedRoomsList)
        {
            string key = BookedRooms.RoomCategoryId.ToString() + "*" + BookedRooms.RoomTypeId.ToString();
            if (RoomCategoryAndType.ContainsKey(key))
            {
                iRoomReq = Convert.ToInt32(RoomCategoryAndType[key]);
                if (BookedRooms.RoomStatus == Constants.AVAILABLE || BookedRooms.RoomStatus == Constants.CANCELLED)
                {
                    // increment the structure AVAILABLE variable if condition is met, else assign memory to 
                    // new structure and set its AVAILABLE variable to 1
                    #region Adding Values To Available Variable
                    if (SelectedCategoryandType.ContainsKey(key))
                    {

                        oSeriesDetailDTO = (SeriesDetailDTO)SelectedCategoryandType[key];
                        oSeriesDetailDTO.Available += 1;
                    }
                    else
                    {
                        oSeriesDetailDTO = new SeriesDetailDTO();
                        oSeriesDetailDTO.Available = 1;
                    }
                    if (oSeriesDetailDTO.Waitlisted > 0)
                        oSeriesDetailDTO.Waitlisted -= 1;
                    #endregion

                }
                else
                {
                    // increment the structure WAITLISTED variable if condition is met, else assign memory to 
                    // new structure and set its WAITLISTED variable to 1
                    #region Adding Values To Waitlisted Variable
                    if (SelectedCategoryandType.ContainsKey(key))
                    {
                        if (oSeriesDetailDTO.Waitlisted < iRoomReq)
                        {
                            oSeriesDetailDTO = (SeriesDetailDTO)SelectedCategoryandType[key];
                            oSeriesDetailDTO.Waitlisted += 1;
                        }
                    }
                    else
                    {
                        oSeriesDetailDTO = new SeriesDetailDTO();
                        oSeriesDetailDTO.Waitlisted = 1;
                    }
                    #endregion Adding Values To Waitlisted Variable
                }
                oSeriesDetailDTO.CheckIn = StartDate;
                oSeriesDetailDTO.CheckOut = EndDate;
                oSeriesDetailDTO.RoomCategory = BookedRooms.RoomCategory.Trim();
                oSeriesDetailDTO.RoomType = BookedRooms.RoomType.Trim();
                oSeriesDetailDTO.RoomCategoryId = BookedRooms.RoomCategoryId;
                oSeriesDetailDTO.RoomTypeId = BookedRooms.RoomTypeId;
                int.TryParse(RoomCategoryAndType[key].ToString(), out iRoomReq);
                oSeriesDetailDTO.Requested = iRoomReq;

                // To check if the key already exists in the final collection that will be returned back to the caller
                // if yes, just assign value to that key
                // if no, add that up in the hashtable collection

                if (SelectedCategoryandType.ContainsKey(key))
                    SelectedCategoryandType[key] = oSeriesDetailDTO;
                else
                    SelectedCategoryandType.Add(key, oSeriesDetailDTO);
            }
        }
        return SelectedCategoryandType;
    }

    private Hashtable GetCheckedCategoryAndType()
    {
        Hashtable selectedCategoryandTypes = new Hashtable();
        string[] sFormControls = Request.Form.AllKeys;
        string Key = "";
        string Value = "";
        for (int i = 0; i < sFormControls.Length; i++)
        {
            if (sFormControls[i] == null)
                continue;

            if (sFormControls[i].StartsWith(Constants.CHECKBOX_CAT_TYPE))
            {
                Key = sFormControls[i].Replace(Constants.CHECKBOX_CAT_TYPE + "*", "");
                Value = Request.Form[sFormControls[i + 1]].ToString();
                selectedCategoryandTypes.Add(Key, Value);
            }
        }
        return selectedCategoryandTypes;
    }

    private Hashtable GetSelectedRoomCategories()
    {
        Hashtable htRCAT = new Hashtable();
        string[] sFormControls = Request.Form.AllKeys;
        for (int i = 0; i < sFormControls.Length; i++)
        {
            if (sFormControls[i].StartsWith(Constants.CHECKBOX_CAT_TYPE))// sFormControls[i].Length > 7)
            {
                string[] arrR = sFormControls[i].ToString().Split('*');
                string concatRCRT = arrR[1] + "*" + arrR[2];
                int iRoomsChosen = Convert.ToInt32(Request.Form[sFormControls[i + 1].ToString()]);
                htRCAT.Add(concatRCRT, iRoomsChosen);
            }
        }
        return htRCAT;
    }

    private TableRow GetSeriesRow(DateTime StartDate, DateTime EndDate, int RoomCategoryID, int RoomTypeID, BookedRooms[] BookedRooms)
    {
        TableRow tr = new TableRow();
        int iAvailable = 0, iWaitlisted = 0;

        TableCell tc = new TableCell();
        CheckBox chk = new CheckBox();
        tc.Controls.Add(chk);
        tr.Cells.Add(tc);

        tc = new TableCell();
        tc.Text = StartDate.ToShortDateString();
        tr.Cells.Add(tc);

        tc = new TableCell();
        tc.Text = EndDate.ToShortDateString();
        tr.Cells.Add(tc);

        for (int i = 0; i < BookedRooms.Length; i++)
        {
            if (BookedRooms[i].RoomCategoryId == RoomCategoryID && BookedRooms[i].RoomTypeId == RoomTypeID)
            {
                if (BookedRooms[i].RoomStatus == 'A' || BookedRooms[i].RoomStatus == Constants.CANCELLED)
                {
                    iAvailable++;
                }
                else
                {
                    iWaitlisted++;
                }
            }
        }

        tc = new TableCell();
        tc.Text = iAvailable.ToString();
        tr.Cells.Add(tc);

        tc = new TableCell();
        tc.Text = iWaitlisted.ToString();
        tr.Cells.Add(tc);

        return tr;

    }

    private List<BookingDatesWithBookedRoomsDTO> GetRoomsListBookingStatus(bool withModifiedDates)
    {
        BookingDatesWithBookedRoomsDTO BookingDatesDTO = null;
        List<BookingDatesWithBookedRoomsDTO> BookedRoomsWithDatesDTO = new List<BookingDatesWithBookedRoomsDTO>();
        SeriesBookingDates[] seriesBookingDates = FillSeriesDates(withModifiedDates);
        BookingServices bookingServices = new BookingServices();
        BookedRooms[] bookedRooms = null;
        for (int i = 0; i < seriesBookingDates.Length; i++)
        {
            bookedRooms = bookingServices.GetAllRooms(seriesBookingDates[i].StartDate, seriesBookingDates[i].EndDate, seriesBookingDates[i].AccomodationId, 0);
            BookingDatesDTO = new BookingDatesWithBookedRoomsDTO();
            BookingDatesDTO.CheckInDate = seriesBookingDates[i].StartDate;
            BookingDatesDTO.CheckOutDate = seriesBookingDates[i].EndDate;
            BookingDatesDTO.BookedRooms = new List<BookedRooms>(bookedRooms);
            BookedRoomsWithDatesDTO.Add(BookingDatesDTO);
        }
        BookedRoomsWithDatesDTO.Sort(new GenericComparer<BookingDatesWithBookedRoomsDTO>("CheckInDate", GenericComparer<BookingDatesWithBookedRoomsDTO>.SortOrder.Ascending));
        SessionServices.Series_BookedRooms_WithDates = BookedRoomsWithDatesDTO;
        return BookedRoomsWithDatesDTO;
    }

    private Hashtable GetSeriesDates()
    {
        string[] sFormControls = Request.Form.AllKeys;
        Hashtable htDates = new Hashtable();
        for (int i = 0; i < sFormControls.Length; i++)
        {
            if (sFormControls[i] != null && sFormControls[i].StartsWith("txt") && sFormControls[i].Contains("*") && sFormControls[i].Length > 20)
            {
                string[] arr = sFormControls[i].Split('*');
                if (!htDates.ContainsKey(arr[arr.Length - 2]))
                {
                    htDates.Add(arr[arr.Length - 2], arr[arr.Length - 1]);
                }
            }
        }
        return htDates;
    }

    private Hashtable GetSeriesDates(bool withModifiedDates)
    {
        Table tblSeries = null;
        Hashtable htDates = new Hashtable();
        String startDate, EndDate;
        if (pnlSeries.Controls.Count > 0)
        {
            try
            {
                tblSeries = (Table)pnlSeries.Controls[0];
            }
            catch
            {
                tblSeries = null;
            }
        }
        if (tblSeries != null)
        {
            foreach (TableRow tr in tblSeries.Rows)
            {
                startDate = EndDate = String.Empty;
                foreach (TableCell tc in tr.Cells)
                {
                    foreach (Control ctrl in tc.Controls)
                    {
                        if (ctrl != null && ctrl.GetType().ToString() == "System.Web.UI.WebControls.TextBox")
                        {
                            TextBox txt = (TextBox)ctrl;
                            if (txt.ID.ToUpper().StartsWith("TXTCHECKINDATE") && txt.ID.Contains("*"))
                            {
                                if (htDates.ContainsKey(txt.Text))
                                {
                                    throw new Exception("Two departues cannot start from same date.");                                    
                                }
                                htDates.Add(txt.Text, String.Empty);
                                startDate = txt.Text;
                            }
                            else if (txt.ID.ToUpper().StartsWith("TXTCHECKOUTDATE") && txt.ID.Contains("*"))
                            {
                                htDates[startDate] = txt.Text;
                            }
                        }
                    }
                }
            }
        }
        return htDates;
    }    

    private clsSeriesBookingDTO[] GetSeriesBookedObject(int BookingID)
    {
        Hashtable htRCAT = GetSelectedRoomCategories();
        Hashtable htDates = GetSeriesDates();
        ICollection colRCAT = htRCAT.Keys;
        ICollection colDates = htDates.Keys;
        int arrLen = htRCAT.Count * htDates.Count;
        clsSeriesBookingDTO[] oSeriesBookingDTO = new clsSeriesBookingDTO[arrLen];
        int iCounter = 0;
        foreach (string var in colRCAT)
        {
            string[] RCAT = var.Split('*');
            foreach (string col in colDates)
            {
                oSeriesBookingDTO[iCounter] = new clsSeriesBookingDTO();
                oSeriesBookingDTO[iCounter].BookingID = BookingID;
                oSeriesBookingDTO[iCounter].StartDate = Convert.ToDateTime(col);
                oSeriesBookingDTO[iCounter].EndDate = Convert.ToDateTime(htDates[col]);
                oSeriesBookingDTO[iCounter].RoomCategoryID = Convert.ToInt32(RCAT[0]);
                oSeriesBookingDTO[iCounter].RoomTypeID = Convert.ToInt32(RCAT[1]);
                oSeriesBookingDTO[iCounter].AccomodationID = Convert.ToInt32(ddlAccomName.SelectedItem.Value);
                oSeriesBookingDTO[iCounter].AccomTypeID = Convert.ToInt32(ddlAccomType.SelectedItem.Value);
                oSeriesBookingDTO[iCounter].NoOfRooms = Convert.ToInt32(htRCAT[var]);
                iCounter++;
            }
        }
        return oSeriesBookingDTO;
    }

    private void AddSeriesToPanel(Table tSeries)
    {
        pnlSeries.Controls.Clear();
        pnlSeries.Controls.Add(tSeries);
    }

    private void AddRoomsToPanel(Table tblMain)
    {
        pnlTotalRoomCount.Controls.Clear();
        pnlTotalRoomCount.Controls.Add(tblMain);        
    }

    #region Preprare Chart of Existing Series
    private void PrepareSeries(bool withModifiedDates)
    {
        Hashtable selectedCategoryandTypes = new Hashtable();
        Hashtable htCellsDataPerRow = new Hashtable();
        Hashtable htBookedRooms = new Hashtable();

        List<BookingDatesWithBookedRoomsDTO> bookingDatesWithBookedRoomsList;

        Table tSeries = new Table();
        TableRow tr = null, trHeaderRAW = null, trHeaderType = null, trHeaderCategory = null;
        tr = new TableRow();

        try
        {
            selectedCategoryandTypes = GetCheckedCategoryAndType();
            bookingDatesWithBookedRoomsList = GetRoomsListBookingStatus(withModifiedDates);

            int ctr = 0;
            foreach (BookingDatesWithBookedRoomsDTO obj in bookingDatesWithBookedRoomsList)
            {
                htCellsDataPerRow = GetCellsDataPerRow(obj.BookedRooms, selectedCategoryandTypes, obj.CheckInDate, obj.CheckOutDate);

                tr = PrepareDataRow(htCellsDataPerRow, ctr);
                ctr++;
                if (tr != null)
                    tSeries.Rows.Add(tr);
            }

            trHeaderRAW = PrepareRowRAW(tr);
            tSeries.Rows.AddAt(0, trHeaderRAW);

            trHeaderType = PrepareRowTypes(trHeaderRAW);
            tSeries.Rows.AddAt(0, trHeaderType);

            trHeaderCategory = PrepareRowCategories(trHeaderType);
            tSeries.Rows.AddAt(0, trHeaderCategory);

            AddSeriesToPanel(tSeries);
        }
        catch (Exception exp)
        {
            throw exp;
        }
    }

    private void PrepareSeries(int SeriesId)
    {
        SeriesBookingServices oSeriesManager = new SeriesBookingServices();
        SeriesDTO Series;
        List<SeriesDetailDTO> SeriesDetailList;
        //List<BookingDatesWithBookedRoomsDTO> BookingDatesWithBookedRoomsDTOs;

        Table tSeries = new Table();
        TableRow tr = null, trHeaderRAW = null, trHeaderType = null, trHeaderCategory = null;

        //Hashtable SelectedCategoryandTypes = new Hashtable();
        Hashtable htCellsDataPerRow = new Hashtable();
        Hashtable htBookedRooms = new Hashtable();

        #region Get Series and Series Details
        Series = oSeriesManager.GetSeries(SeriesId);
        SeriesDetailList = oSeriesManager.GetSeriesDetail(SeriesId);
        #endregion Get Series and Series Details

        #region Set Series To Controls
        SetSeriesDataToControls(Series);
        #endregion Set Series To Controls

        #region Set the Category/Type wise Rooms
        //To Make sure that the controls for Categories and Types exists.
        int accomodationId = Convert.ToInt32(ddlAccomName.SelectedItem.Value);
        PrepareRoomCategoriesAndTypes(accomodationId);

        SetCategoryAndTypesToControls(SeriesDetailList);
        #endregion Set the Category/Type wise Rooms

        #region Prepare each data row
        tr = new TableRow();

        int departureNo = 0;
        foreach (SeriesDetailDTO SeriesDetailDTO in SeriesDetailList)
        {
            string DateKey = SeriesDetailDTO.CheckIn.ToString();
            string key;
            if (htBookedRooms.ContainsKey(DateKey))
            {
                key = SeriesDetailDTO.RoomCategoryId.ToString() + "*" + SeriesDetailDTO.RoomTypeId.ToString();
                if (htCellsDataPerRow.ContainsKey(key))
                    htCellsDataPerRow[key] = SeriesDetailDTO;
                else
                {
                    htCellsDataPerRow.Add(key, SeriesDetailDTO);
                }
                htBookedRooms[DateKey] = htCellsDataPerRow;
            }
            else
            {
                key = SeriesDetailDTO.RoomCategoryId.ToString() + "*" + SeriesDetailDTO.RoomTypeId.ToString();
                htCellsDataPerRow = new Hashtable();
                htCellsDataPerRow.Add(key, SeriesDetailDTO);
                htBookedRooms.Add(DateKey, htCellsDataPerRow);
                if (htCellsDataPerRow != null)
                {
                    tr = PrepareDataRow(htCellsDataPerRow, true, departureNo);
                    if (tr != null)
                        tSeries.Rows.Add(tr);
                }
            }
            departureNo++;
        }
        #endregion

        #region Prepare Rooms Requested, Available and Waitlisted Header Row
        trHeaderRAW = PrepareRowRAW(tr);
        tSeries.Rows.AddAt(0, trHeaderRAW);
        #endregion

        #region Room Type Header Row
        trHeaderType = PrepareRowTypes(trHeaderRAW);
        tSeries.Rows.AddAt(0, trHeaderType);
        #endregion

        #region Prepare Room Category Header Row
        trHeaderCategory = PrepareRowCategories(trHeaderType);
        tSeries.Rows.AddAt(0, trHeaderCategory);
        #endregion

        #region Add Series To Panel
        AddSeriesToPanel(tSeries);
        #endregion
    }

    public ICollection SortedHashTable(Hashtable ht)
    {
        ArrayList sorter = new ArrayList();
        sorter.AddRange(ht);
        sorter.Sort();
        return sorter;
    }

    private void SetSeriesDataToControls(SeriesDTO SeriesDTO)
    {
        ddlAccomType.SelectedValue = SeriesDTO.AccomTypeID.ToString();
        FillAccomodations();
        ddlAccomName.SelectedValue = SeriesDTO.AccomodationID.ToString();
        ddlAgent.SelectedValue = SeriesDTO.AgentId.ToString();
        txtSeriesName.Text = SeriesDTO.SeriesName;
        ddlNoOfNights.SelectedValue = SeriesDTO.NoOfNights.ToString();
        ddlGap.SelectedValue = SeriesDTO.GAP.ToString();
        ddlNoOfDeps.SelectedValue = SeriesDTO.NoOfDepartures.ToString();
        txtFirstCheckInDate.Text = GF.GetDD_MMM_YYYY(SeriesDTO.SeriesStartDate, false);
    }

    private void SetCategoryAndTypesToControls(List<SeriesDetailDTO> SeriesDetailList)
    {
        string chkId = string.Empty, ddlId = string.Empty;
        Control c;
        CheckBox chk;
        DropDownList ddl;
        int RoomsRequested;
        foreach (SeriesDetailDTO SeriesDetailDTO in SeriesDetailList)
        {
            chkId = Constants.CHECKBOX_CAT_TYPE + "*" + SeriesDetailDTO.RoomCategoryId.ToString() + "*" + SeriesDetailDTO.RoomTypeId.ToString();
            ddlId = Constants.DROPDOWN_CAT_TYPE + "*" + SeriesDetailDTO.RoomCategoryId.ToString() + "*" + SeriesDetailDTO.RoomTypeId.ToString();
            RoomsRequested = SeriesDetailDTO.Requested;
            c = FindControl(chkId);
            if (c != null)
            {
                chk = (CheckBox)c;
                chk.Checked = true;
                c = null;
            }
            c = FindControl(ddlId);
            if (c != null)
            {
                ddl = (DropDownList)c;
                ddl.Text = RoomsRequested.ToString();
            }
        }
    }

    private Hashtable GetCellsDataPerRow(SeriesDetailDTO SeriesDetailDTO)
    {
        SeriesDetailDTO oSeriesDetailDTO = new SeriesDetailDTO();
        Hashtable SelectedCategoryandType = new Hashtable();
        if (SeriesDetailDTO == null)
            return null;

        string key = SeriesDetailDTO.RoomCategoryId.ToString() + "*" + SeriesDetailDTO.RoomTypeId.ToString();
        oSeriesDetailDTO = new SeriesDetailDTO();
        oSeriesDetailDTO.CheckIn = SeriesDetailDTO.CheckIn;
        oSeriesDetailDTO.CheckOut = SeriesDetailDTO.CheckOut;
        oSeriesDetailDTO.Requested = SeriesDetailDTO.Requested;
        oSeriesDetailDTO.Available = SeriesDetailDTO.Available;
        oSeriesDetailDTO.Waitlisted = SeriesDetailDTO.Waitlisted;
        oSeriesDetailDTO.RoomCategoryId = SeriesDetailDTO.RoomCategoryId;
        oSeriesDetailDTO.RoomTypeId = SeriesDetailDTO.RoomTypeId;
        oSeriesDetailDTO.RoomCategory = SeriesDetailDTO.RoomCategory;
        oSeriesDetailDTO.RoomType = SeriesDetailDTO.RoomType;
        oSeriesDetailDTO.ProposedBooking = SeriesDetailDTO.ProposedBooking;

        if (SelectedCategoryandType.ContainsKey(key))
            SelectedCategoryandType[key] = oSeriesDetailDTO;
        else
            SelectedCategoryandType.Add(key, oSeriesDetailDTO);
        return SelectedCategoryandType;
    }

    #endregion Preprare Chart of Existing Series

    private void ClearSessionVariables()
    {
        SessionServices.DeleteSession(Constants._Series_AccomodationData);
        SessionServices.DeleteSession(Constants._Series_BookedRooms_WithDates);
        SessionServices.DeleteSession(Constants._SeriesBooking_TableSeries);
        SessionServices.DeleteSession(Constants._SeriesBooking_TableTotalRoomCount);        

        ChangedSeriesDates.Clear();
    }

    [WebMethod]
    public static void PostSeriesDateChanges(string startDateTextBoxId, string startDateValue, string endDateTextBoxId, string endDateValue)
    {
        if (ChangedSeriesDates == null)
        {
            ChangedSeriesDates = new Hashtable();
        }

        if (ChangedSeriesDates.ContainsKey(startDateTextBoxId))
        {
            ChangedSeriesDates[startDateTextBoxId] = startDateValue;
        }
        else
        {
            ChangedSeriesDates.Add(startDateTextBoxId, startDateValue);
        }

        if (ChangedSeriesDates.ContainsKey(endDateTextBoxId))
        {
            ChangedSeriesDates[endDateTextBoxId] = endDateValue;
        }
        else
        {
            ChangedSeriesDates.Add(endDateTextBoxId, endDateValue);
        }
    }
    #endregion Helper Methods
}
