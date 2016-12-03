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
using FarHorizon.Reservations.BusinessServices;
using FarHorizon.Reservations.Common;
using FarHorizon.Reservations.Common.DataEntities.Client;
using FarHorizon.Reservations.Common.DataEntities.Masters;

using FarHorizon.Reservations.MasterServices;
using FarHorizon.Reservations.Bases;
using FarHorizon.Reservations.Bases.BasePages;

public partial class ClientUI_waitListtedbookingmanagement : ClientBasePage
{
    #region Control Defined Functions
    protected void Page_Load(object sender, EventArgs e)
    {
        AddAttributes();        
        if (!IsPostBack)
        {
            FillAccomodations();
            initialize();
        }
        else
        {
            if (string.Compare(GetPostBackControlID(), "btnShow") != 0)
            {
                PrepareWaitListedBookingDetails();
            }
        }
    }
    
    protected void btnShow_Click(object sender, EventArgs e)
    {
        SessionServices.WaitListManagement_WaitListedBookings = null;
        RemoveListFromMainPanel();
        hfId.Value = String.Empty;
        PrepareWaitListedBookingDetails();
    }

    void btnSubmit_Click(object sender, EventArgs e)
    {
        PerformWaitListManagement(sender, e);
    }

    private void PerformWaitListManagement(object sender, EventArgs e)
    {
        Button btn = (Button)sender;
        string RoomCategory, RoomType, BookingId, checkBoxIdPrefix, RoomNo, RoomsSelected = "";
        int RoomCategoryId, RoomTypeId;
        SortedList slFullList = new SortedList();
        SortedList slRoomCateTypeList = new SortedList();
        string key, value;

        string[] AllKeys = Request.Form.AllKeys;
        if (btn == null)
            return;

        if (base.ValidateIfCommandAllowed(Request.Url.AbsolutePath, ENums.PageCommand.Update))
        {
            string[] splitId = btn.ID.Split('*');
            BookingId = splitId[1];
            splitId = null;
            checkBoxIdPrefix = "chk*" + BookingId;

            #region Prepare List
            for (int i = 0; i < AllKeys.Length; i++)
            {
                if (AllKeys[i].StartsWith(checkBoxIdPrefix))
                {
                    splitId = AllKeys[i].Split('*');

                    BookingId = splitId[1];
                    RoomCategory = splitId[2].ToString();
                    RoomType = splitId[3].ToString();
                    RoomNo = splitId[4];
                    RoomCategoryId = (splitId[5] == string.Empty ? 0 : Convert.ToInt32(splitId[5]));
                    RoomTypeId = (splitId[6] == string.Empty ? 0 : Convert.ToInt32(splitId[6]));

                    key = RoomNo;
                    value = BookingId + "*" + RoomCategory + "*" + RoomType + "*" + RoomCategoryId.ToString() + "*" + RoomTypeId.ToString();
                    slFullList.Add(key, value);

                    if (!slRoomCateTypeList.ContainsKey(value))
                    {
                        slRoomCateTypeList.Add(value, null);
                    }
                }
            }            
            #endregion

            #region Allocate Rooms
            RoomsSelected = "";

            for (int j = 0; j < slRoomCateTypeList.Count; j++)
            {
                for (int k = 0; k < slFullList.Count; k++)
                {
                    if (String.Compare(slRoomCateTypeList.GetKey(j).ToString(), slFullList.GetByIndex(k).ToString()) == 0)
                    {
                        RoomsSelected += slFullList.GetKey(k).ToString() + ",";
                    }
                }

                if (RoomsSelected.EndsWith(","))
                {
                    RoomsSelected = RoomsSelected.Substring(0, RoomsSelected.Length - 1);
                }

                splitId = slRoomCateTypeList.GetKey(j).ToString().Split('*');
                BookingId = splitId[0];
                RoomCategory = splitId[1];
                RoomType = splitId[2];
                RoomCategoryId = (splitId[3] == string.Empty ? 0 : Convert.ToInt32(splitId[3]));
                RoomTypeId = (splitId[4] == string.Empty ? 0 : Convert.ToInt32(splitId[4]));

                bool Allocated = AllocateRooms(Convert.ToInt32(BookingId), RoomsSelected, RoomCategoryId, RoomTypeId);

                if (Allocated == true)
                {
                    base.DisplayAlert("Rooms has been allocated");
                }
                else
                {
                    base.DisplayAlert("Rooms has not been allocated successfully");
                }

                SessionServices.WaitListManagement_WaitListedBookings = null;
                btnShow_Click(sender, e);
            }
            #endregion
        }
    }

    private bool AllocateRooms(int BookingId, string RoomList, int RoomCategoryId, int RoomTypeId)
    {
        BookingServices oBookingManager = new BookingServices();
        return oBookingManager.AllocateRoomsToWaitListedBooking(BookingId, RoomList, RoomCategoryId, RoomTypeId);
    }
    #endregion Control Defined Functions

    #region Helper Methods
    private void AddAttributes()
    {
        //txtStartDate.Attributes.Add("onchange", "fillEndDate();");
        txtStartDate.Attributes.Add("onchange", "return fillEndDate('" + txtStartDate.ClientID + "','" + txtEndDate.ClientID + "');");
        btnShow.Attributes.Add("onclick", "return validateSelection();");
    }

    private void initialize()
    {
        string sCheckInDate, sCheckOutDate, sAccomId, sBookingId;

        sCheckInDate = Request.QueryString["cid"];
        sCheckOutDate = Request.QueryString["cod"];
        sAccomId = Request.QueryString["accomid"];
        sBookingId = Request.QueryString["bid"];
        if (sCheckInDate != null)
            txtStartDate.Text = GF.GetDateFromYYYYMMDD(sCheckInDate).ToString();
        else
            txtStartDate.Text = string.Empty;

        if (sCheckOutDate != null)
            txtEndDate.Text = GF.GetDateFromYYYYMMDD(sCheckOutDate).ToString();
        else
            txtEndDate.Text = string.Empty;

        if (sBookingId != null)
            hfId.Value = sBookingId;
        else
            hfId.Value = "";
        if (sAccomId != null)
            ddlAccomodation.SelectedValue = sAccomId;
        else
            ddlAccomodation.SelectedIndex = 0;
    }

    private void FillAccomodations()
    {
        AccomodationMaster oAccomodationMaster = new AccomodationMaster();
        AccomodationDTO[] oAccomodationData = null;
        ListItem l;
        oAccomodationData = oAccomodationMaster.GetAccomodations();
        if (oAccomodationData != null)
        {
            l = new ListItem("Choose", "-1");
            ddlAccomodation.Items.Insert(0, l);
            l = new ListItem("All", "-2");
            ddlAccomodation.Items.Insert(1, l);
            for (int i = 0; i < oAccomodationData.Length; i++)
            {
                l = new ListItem(oAccomodationData[i].AccomodationName, oAccomodationData[i].AccomodationId.ToString());
                ddlAccomodation.Items.Insert(i + 2, l);
            }
        }
    }

    private void PrepareWaitListedBookingDetails()
    {
        DateTime CheckInDate, CheckOutDate;
        int CurrentBookingId, AccomId;
        RoomBookingDateWiseDTO[] oRoomBookingDateWiseData;
        BookedRooms[] oBookedRooms;
        DateTime.TryParse(txtStartDate.Text, out CheckInDate);
        DateTime.TryParse(txtEndDate.Text, out CheckOutDate);
        Table t;
        Panel p = null, p1 = null;
        bool anyWaitListedRecordFound = false;


        CurrentBookingId = 0; AccomId = 0;
        if (hfId.Value != string.Empty)
            CurrentBookingId = Convert.ToInt32(hfId.Value);
        else
            CurrentBookingId = 0;
        if (ddlAccomodation.SelectedValue != null)
        {
            if (Convert.ToInt32(ddlAccomodation.SelectedValue) > 0)
                AccomId = Convert.ToInt32(ddlAccomodation.SelectedValue);
            else
                AccomId = 0;
        }

        oRoomBookingDateWiseData = getWaitListedBookings(CheckInDate, CheckOutDate, CurrentBookingId, AccomId);
        if (oRoomBookingDateWiseData != null && oRoomBookingDateWiseData.Length > 0)
        {
            for (int i = 0; i < oRoomBookingDateWiseData.Length; i++)
            {
                p = new Panel();
                p.ID = Constants.PANEL_HEADER + "*" + oRoomBookingDateWiseData[i].BookingId.ToString();
                t = PrepareBookingHeaderDataRow(oRoomBookingDateWiseData[i]);
                p.Controls.Add(t);

                oBookedRooms = GetAllRooms(oRoomBookingDateWiseData[i].Startdate, oRoomBookingDateWiseData[i].Enddate, oRoomBookingDateWiseData[i].BookingId, oRoomBookingDateWiseData[i].AccomodationId);
                if (oBookedRooms != null && oBookedRooms.Length > 0)
                {
                    p1 = PrepareWaitListedBookingDetails(oBookedRooms, oRoomBookingDateWiseData[i].BookingId);
                    if (p1 != null)
                    {
                        p.Controls.Add(p1);
                    }
                    if (p != null && p1 != null)
                    {
                        AddListToMainPanel(p);
                        if (!anyWaitListedRecordFound)
                        {
                            anyWaitListedRecordFound = true;
                        }
                    }
                }
            }
        }
        if (!anyWaitListedRecordFound)
        {
            base.DisplayAlert("Waitlisted bookings not found");
        }
    }

    private HtmlGenericControl AddSeperator()
    {
        HtmlGenericControl seperatorDiv = new HtmlGenericControl("div");
        seperatorDiv.Attributes.Add("class", "seperator");
        return seperatorDiv;
    }


    private Table PrepareSubmitButtonRow(int BookingId, bool roomsAvailableTobeAllocated)
    {
        #region Adding Submit Button
        Table t = new Table();
        TableRow tr = new TableRow();
        TableCell tc = new TableCell();
        Button btnSubmit = new Button();
        btnSubmit.ID = Constants.BUTTON + "*" + BookingId.ToString();
        btnSubmit.Text = "Allocate Rooms";
        btnSubmit.EnableViewState = true;
        if (roomsAvailableTobeAllocated)
        {
            btnSubmit.Enabled = true;
        }
        else
        {
            btnSubmit.Enabled = false;
        }
        btnSubmit.Click += new EventHandler(btnSubmit_Click);
        btnSubmit.Attributes.Add("class", "appbutton btnSubmit");
        btnSubmit.Attributes.Add("onclick", "return validateRoomAllocation('" + BookingId.ToString() + "')");
        tc.Controls.Add(btnSubmit);
        tr.Cells.Add(tc);
        t.Rows.Add(tr);
        return t;
        #endregion
    }

    private Table PrepareBookingHeaderDataRow(RoomBookingDateWiseDTO oRoomBookingDateWiseData)
    {
        Table t = new Table();
        TableRow tr = new TableRow();
        TableCell tc;

        t.Attributes.Add("class", "bookingHeaderDataRowTable");

        string sPanelId = Constants.PANEL_DETAIL + "_" + oRoomBookingDateWiseData.BookingId;
        tc = new TableCell();
        HtmlImage showhideImage = new HtmlImage();
        showhideImage.Src = "~/images/icon_summary_plus_On.gif";
        showhideImage.ID = Constants.IMAGE + sPanelId;
        showhideImage.Attributes.Add("onclick", "showhide('" + sPanelId.ToString() + "')");
        tc.Controls.Add(showhideImage);
        tr.Cells.Add(tc);

        tc = new TableCell();
        tc.Attributes.Add("class", "bookingHeaderDataRowcell");
        tc.Text = "Booking Code: <br>" + oRoomBookingDateWiseData.BookingCode;
        tr.Cells.Add(tc);

        tc = new TableCell();
        tc.Attributes.Add("class", "bookingHeaderDataRowcell");
        tc.Text = "Booking Reference: <br>" + oRoomBookingDateWiseData.BookingReference;
        tr.Cells.Add(tc);

        tc = new TableCell();
        tc.Attributes.Add("class", "bookingHeaderDataRowcell");
        tc.Text = "Check In: <br>" + GF.GetDD_MM_YYYY(oRoomBookingDateWiseData.Startdate, false);
        tr.Cells.Add(tc);

        tc = new TableCell();
        tc.Attributes.Add("class", "bookingHeaderDataRowcell");
        tc.Text = "Check Out: <br>" + GF.GetDD_MMM_YYYY(oRoomBookingDateWiseData.Enddate, false);
        tr.Cells.Add(tc);

        if (oRoomBookingDateWiseData.AgentName != null && oRoomBookingDateWiseData.AgentName != string.Empty)
        {
            tc = new TableCell();
            tc.Attributes.Add("class", "bookingHeaderDataRowcell");
            tc.Text = "Agent Name: " + oRoomBookingDateWiseData.AgentName;
            tr.Cells.Add(tc);
        }

        t.Rows.Add(tr);
        return t;
    }

    private Panel PrepareWaitListedBookingDetails(BookedRooms[] oBookedRooms, int WaitListedBookingId)
    {
        Panel pnlBookingMain = new Panel();
        Panel pnlTemp = null;
        Table t;
        SortedList slWaitListedRooms;
        SortedList slAvailableRooms;
        SortedList slWaitListedCatnType = null;
        string RoomCategory, RoomType;
        int RoomCategoryId, RoomTypeId;
        int WaitListedRooms;
        bool roomsAvailable = false;
        slWaitListedRooms = getWaitListedRoomsList(oBookedRooms, WaitListedBookingId, out slWaitListedCatnType);
        if (slWaitListedRooms == null || slWaitListedRooms.Count == 0)
            return null;

        slAvailableRooms = getAvailableRoomsList(oBookedRooms, slWaitListedRooms, WaitListedBookingId);

        for (int i = 0; i < slWaitListedCatnType.Count; i++)
        {
            pnlBookingMain.ID = Constants.PANEL_DETAIL + "_" + WaitListedBookingId.ToString();
            pnlBookingMain.Attributes.Add("class", "pnlDetails");
            pnlBookingMain.Style[HtmlTextWriterStyle.Display] = "none";
            splitRoomCatType(Convert.ToString(slWaitListedCatnType.GetKey(i)), out RoomCategory, out RoomType, out RoomCategoryId, out RoomTypeId);
            WaitListedRooms = Convert.ToInt32(slWaitListedCatnType.GetByIndex(i));
            pnlTemp = PrepareBookingWaitListTable(WaitListedBookingId, slWaitListedRooms, RoomCategory, RoomType, RoomCategoryId, RoomTypeId, WaitListedRooms);
            pnlBookingMain.Controls.Add(pnlTemp);
            if (slAvailableRooms != null && slAvailableRooms.Count > 0)
            {
                roomsAvailable = true;
            }
            t = PrepareBookingRoomsAvailibilityTable(WaitListedBookingId, slAvailableRooms, slWaitListedRooms, RoomCategory, RoomType, RoomCategoryId, RoomTypeId);
            pnlBookingMain.Controls.Add(t);
            pnlBookingMain.Controls.Add(AddSeperator());
        }
        t = PrepareSubmitButtonRow(WaitListedBookingId, roomsAvailable);
        pnlBookingMain.Controls.Add(t);
        return pnlBookingMain;
    }

    private Panel PrepareBookingWaitListTable(int BookingId, SortedList slWaitListedRooms, string RoomCategory, string RoomType, int RoomCategoryId, int RoomTypeId, int RoomsWaitListed)
    {
        Table t;
        Panel pnlBookingWaitListTable = new Panel();
        t = PrepareCategoryTypeHeaderRow(RoomCategory, RoomType);
        pnlBookingWaitListTable.Controls.Add(t);
        t = PrepareBookingWaitlistLabel(BookingId, RoomCategory, RoomType, RoomsWaitListed);
        pnlBookingWaitListTable.Controls.Add(t);
        t = PrepareBookingWaitListedRoomsTable(BookingId, slWaitListedRooms, RoomCategory, RoomType, RoomCategoryId, RoomTypeId);
        pnlBookingWaitListTable.Controls.Add(t);
        return pnlBookingWaitListTable;
    }

    private Table PrepareCategoryTypeHeaderRow(string RoomCategory, string RoomType)
    {
        Table t = new Table();
        TableRow tr;
        TableCell tc;

        t.Attributes.Add("class", "categorytypeHeader");
        tr = new TableRow();

        tc = new TableCell();
        tc.Text = "Category: " + GF.RecoverSpace(RoomCategory);
        tc.Attributes.Add("class", "categorytypeHeaderdatacell");
        tr.Cells.Add(tc);

        tc = new TableCell();
        tc.Text = "Type: " + GF.RecoverSpace(RoomType);
        tc.Attributes.Add("class", "categorytypeHeaderdatacell");
        tr.Cells.Add(tc);

        t.Rows.Add(tr);
        return t;
    }

    private Table PrepareBookingWaitlistLabel(int BookingId, string RoomCategory, string RoomType, int RoomsWaitListed)
    {
        Table t = new Table();
        TableRow tr = new TableRow();
        TableCell tc = new TableCell();

        tc = new TableCell();
        tc.Text = "Wait Listed Rooms: " + RoomsWaitListed.ToString();
        tc.Attributes.Add("class", "categorytypeHeaderdatacell");
        tr.Cells.Add(tc);

        tc = new TableCell();
        HiddenField hfNoOfRoomsWaitListed = new HiddenField();
        hfNoOfRoomsWaitListed.ID = Constants.HIDDENFIELD + "*" + BookingId.ToString() + "*" + RoomCategory + "*" + RoomType;
        hfNoOfRoomsWaitListed.Value = RoomsWaitListed.ToString();
        tc.Controls.Add(hfNoOfRoomsWaitListed);
        tr.Cells.Add(tc);
        t.Rows.Add(tr);
        return t;
    }

    private Table PrepareBookingWaitListedRoomsTable(int BookingId, SortedList slWaitListedRooms, string RoomCategory, string RoomType, int RoomCategoryId, int RoomTypeId)
    {
        Table t = new Table();
        TableRow tr = null;
        TableCell tc;
        CheckBox chk;
        SortedList slRooms = new SortedList();
        const int ROOM_CELLS_IN_ROW = 8;
        int totalCellsAdded = 0;
        string Id = "";
        #region generating List for this particular room category and type
        string Value = RoomCategory + "*" + RoomType + "*" + RoomCategoryId.ToString() + "*" + RoomTypeId.ToString();
        for (int i = 0; i < slWaitListedRooms.Count; i++)
        {
            if (string.Compare(Convert.ToString(slWaitListedRooms.GetByIndex(i)), Value) == 0)
            {
                slRooms.Add(slWaitListedRooms.GetKey(i), slWaitListedRooms.GetByIndex(i));
            }
        }
        #endregion

        #region Adding the Room Checkboxes
        for (int i = 0; i < slRooms.Count; i++)
        {
            if (totalCellsAdded == 0)
            {
                tr = new TableRow();
            }
            if (totalCellsAdded == ROOM_CELLS_IN_ROW)
            {
                t.Rows.Add(tr);
                tr = new TableRow();
                totalCellsAdded = 0;
            }
            tc = new TableCell();
            tc.Attributes.Add("class", "waitlistChkCell");
            chk = new CheckBox();
            //This WL is added so that at the time of submission, roomcategory shall not match.
            Id = Constants.CHECKBOX + "*" + "WL" + BookingId.ToString() + "*" + RoomCategory + "*" + RoomType + "*" + slRooms.GetKey(i).ToString() + "*" + RoomCategoryId.ToString() + "*" + RoomTypeId.ToString();
            chk.ID = Id;
            chk.Text = slRooms.GetKey(i).ToString();
            chk.Enabled = false;
            chk.Checked = true;
            tc.Controls.Add(chk);
            tr.Controls.Add(tc);
            totalCellsAdded++;
        }
        if (tr != null)
            t.Rows.Add(tr);
        #endregion

        return t;
    }

    private Table PrepareBookingRoomsAvailibilityTable(int BookingId, SortedList slAvailableRooms, SortedList slWaitListedRooms, string RoomCategory, string RoomType, int RoomCategoryId, int RoomTypeId)
    {
        Table t = new Table();
        TableRow tr = null;
        TableCell tc;
        CheckBox chk;
        SortedList slRooms = new SortedList();
        const int ROOM_CELLS_IN_ROW = 8;
        int totalCellsAdded = 0;
        string Id = "";
        #region generating List for this particular room category and type
        string Value = RoomCategory + "*" + RoomType + "*" + RoomCategoryId.ToString() + "*" + RoomTypeId.ToString();
        for (int i = 0; i < slAvailableRooms.Count; i++)
        {
            if (string.Compare(Convert.ToString(slAvailableRooms.GetByIndex(i)), Value) == 0)
            {
                slRooms.Add(slAvailableRooms.GetKey(i), slAvailableRooms.GetByIndex(i));
            }
        }
        #endregion

        #region Adding the availibility Header
        t.Attributes.Add("class", "tblRooms");
        tr = new TableRow();
        tc = new TableCell();
        tc.Attributes.Add("class", "RoomsHeaderCell");
        tc.ColumnSpan = ROOM_CELLS_IN_ROW;
        tc.Text = "Now Available Rooms: " + slRooms.Count;
        tr.Cells.Add(tc);
        t.Rows.Add(tr);
        #endregion

        #region Adding the Room Checkboxes
        for (int i = 0; i < slRooms.Count; i++)
        {
            if (totalCellsAdded == 0)
            {
                tr = new TableRow();
            }
            if (totalCellsAdded == ROOM_CELLS_IN_ROW)
            {
                t.Rows.Add(tr);
                tr = new TableRow();
                totalCellsAdded = 0;
            }
            tc = new TableCell();
            tc.Attributes.Add("class", "availableChkCell");
            chk = new CheckBox();
            Id = Constants.CHECKBOX + "*" + BookingId.ToString() + "*" + RoomCategory + "*" + RoomType + "*" + slRooms.GetKey(i).ToString() + "*" + RoomCategoryId.ToString() + "*" + RoomTypeId.ToString();
            chk.ID = Id;

            if (slWaitListedRooms.ContainsKey(slRooms.GetKey(i)))
                chk.Checked = true;

            chk.Text = slRooms.GetKey(i).ToString();
            tc.Controls.Add(chk);
            tr.Controls.Add(tc);
            totalCellsAdded++;
        }
        if (tr != null)
            t.Rows.Add(tr);
        #endregion

        return t;
    }

    private SortedList getWaitListedRoomsList(BookedRooms[] oBookedRooms, int WaitListedBookingId, out SortedList WaitListedCatnType)
    {
        SortedList slWaitListedRooms = new SortedList();
        SortedList slCatType = new SortedList();
        string key, value;
        int wlRooms = 0;
        for (int i = 0; i < oBookedRooms.Length; i++)
        {
            if (oBookedRooms[i].BookingId == WaitListedBookingId && oBookedRooms[i].RoomStatus == Constants.WAITLISTED)
            {
                key = oBookedRooms[i].RoomNo;
                value = GF.ReplaceSpace(oBookedRooms[i].RoomCategory) + "*" + oBookedRooms[i].RoomType + "*" + oBookedRooms[i].RoomCategoryId.ToString() + "*" + oBookedRooms[i].RoomTypeId.ToString();
                if (!slWaitListedRooms.ContainsKey(key))
                {
                    slWaitListedRooms.Add(key, value);
                }
                if (!slCatType.ContainsKey(value))
                {
                    slCatType.Add(value, 1);
                }
                else
                {
                    wlRooms = Convert.ToInt32(slCatType[value]) + 1;
                    slCatType[value] = wlRooms;
                }
            }
        }

        WaitListedCatnType = slCatType;
        return slWaitListedRooms;
    }

    private SortedList getAvailableRoomsList(BookedRooms[] oBookedRooms, SortedList slWaitListedRooms, int WaitListedBookingId)
    {
        string RoomCategory = string.Empty, RoomType = string.Empty;
        int RoomCategoryId = 0, RoomTypeId = 0;
        string Key, Value;
        SortedList slAvaialbleRooms = new SortedList();
        SortedList slBookedWithOtherBookings = new SortedList();
        for (int i = 0; i < slWaitListedRooms.Count; i++)
        {
            splitRoomCatType(Convert.ToString(slWaitListedRooms.GetByIndex(i)), out RoomCategory, out RoomType, out RoomCategoryId, out RoomTypeId);

            for (int j = 0; j < oBookedRooms.Length; j++)
            {
                Key = oBookedRooms[j].RoomNo;
                Value = GF.ReplaceSpace(oBookedRooms[j].RoomCategory) + "*" + oBookedRooms[j].RoomType + "*" + oBookedRooms[j].RoomCategoryId.ToString() + "*" + oBookedRooms[j].RoomTypeId.ToString();
                //TODO: Not sure about the rooms of the Cancelled booking so currently taking those rooms as available.
                if (oBookedRooms[j].BookingId != WaitListedBookingId && oBookedRooms[j].RoomStatus != Constants.BOOKED && GF.ReplaceSpace(oBookedRooms[j].RoomCategory) == RoomCategory && GF.ReplaceSpace(oBookedRooms[j].RoomType) == RoomType && oBookedRooms[j].RoomStatus.ToString()!="M")
                {
                    if (!slBookedWithOtherBookings.ContainsKey(Key))
                    {
                        if (!slAvaialbleRooms.ContainsKey(Key))
                        {
                            slAvaialbleRooms.Add(Key, Value);
                        }
                    }
                }
                //TODO Not sure about the rooms of the Cancelled Booking
                else if (oBookedRooms[j].BookingId != WaitListedBookingId && (oBookedRooms[j].RoomStatus == Constants.BOOKED) && GF.ReplaceSpace(oBookedRooms[j].RoomCategory) == RoomCategory && GF.ReplaceSpace(oBookedRooms[j].RoomType) == RoomType)
                {
                    if (!slBookedWithOtherBookings.ContainsKey(Key))
                        slBookedWithOtherBookings.Add(Key, null);
                    if (slAvaialbleRooms.ContainsKey(Key))
                    {
                        slAvaialbleRooms.Remove(Key);
                    }
                }
                else if (oBookedRooms[j].BookingId == WaitListedBookingId && (oBookedRooms[j].RoomStatus == Constants.WAITLISTED) && GF.ReplaceSpace(oBookedRooms[j].RoomCategory) == RoomCategory && GF.ReplaceSpace(oBookedRooms[j].RoomType) == RoomType)
                {
                    if (oBookedRooms[j].RoomNo == Key)
                    {
                        if (!slBookedWithOtherBookings.ContainsKey(Key))
                        {
                            if (!slAvaialbleRooms.ContainsKey(Key))
                                slAvaialbleRooms.Add(Key, Value);
                        }
                    }
                }
            }
        }
        return slAvaialbleRooms;
    }

    private RoomBookingDateWiseDTO[] getWaitListedBookings(DateTime CheckInDate, DateTime CheckOutDate, int notThisBookingId, int AccomodationId)
    {
        BookingServices oBookingManager;
        if (SessionServices.WaitListManagement_WaitListedBookings == null)
        {
            oBookingManager = new BookingServices();
            return oBookingManager.GetWaitListedBookings(CheckInDate, CheckOutDate, notThisBookingId, AccomodationId);
        }
        else
            return SessionServices.WaitListManagement_WaitListedBookings;
    }

    private BookedRooms[] GetAllRooms(DateTime CheckInDate, DateTime CheckOutDate, int CurrentBookingId, int AccomodationId)
    {
        BookingServices oBookingManager = new BookingServices();
        return oBookingManager.GetAllRooms(CheckInDate, CheckOutDate, AccomodationId, CurrentBookingId);
    }

    private void AddListToMainPanel(Panel pnlList)
    {
        //if (pnlwaitlistedbookings.Controls.Count > 0)
        //  pnlwaitlistedbookings.Controls.RemoveAt(0);
        pnlwaitlistedbookings.Controls.Add(pnlList);
    }
    private void RemoveListFromMainPanel()
    {
        pnlwaitlistedbookings.Controls.Clear();
    }

    private void splitRoomCatType(string RoomCatType, out string RoomCategory, out string RoomType, out int RoomCategoryId, out int RoomTypeId)
    {
        RoomCategory = string.Empty;
        RoomType = string.Empty;
        RoomCategoryId = 0;
        RoomTypeId = 0;

        string[] RCTArray;
        RCTArray = RoomCatType.Split('*');
        RoomCategory = RCTArray[0];
        RoomCategory = GF.ReplaceSpace(RoomCategory);
        if (RCTArray.Length >= 2)
            RoomType = RCTArray[1];
        if (RCTArray.Length >= 3)
            RoomCategoryId = Convert.ToInt32(RCTArray[2]);
        if (RCTArray.Length >= 4)
            RoomTypeId = Convert.ToInt32(RCTArray[3]);
    }

    #endregion Helper Methods

}
