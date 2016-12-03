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
using System.Collections.Generic;
using FarHorizon.Reservations.BusinessServices;
using FarHorizon.Reservations.Common;
using FarHorizon.Reservations.Common.DataEntities.Client;
using FarHorizon.Reservations.Common.DataEntities.Masters;

using FarHorizon.Reservations.MasterServices;
using FarHorizon.Reservations.Bases;
using FarHorizon.Reservations.Bases.BasePages;

public partial class ClientUI_TouristCountReport : ClientBasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {        
        AddAttributes();

        if (!IsPostBack)
        {
            FillAccomodationTypes();            
            if (SessionServices.TouristCount_SelectedCheckInDate != null)
                txtStartDate.Text = SessionServices.TouristCount_SelectedCheckInDate;
            if (SessionServices.TouristCount_SelectedCheckOutDate != null)
                txtEndDate.Text = SessionServices.TouristCount_SelectedCheckOutDate;
            if (SessionServices.TouristCount_SelectedBookingStatus != null)
                //ddlBookingStatusTypes.SelectedValue = SessionHandler.TouristCount_SelectedBookingStatus;
                if (SessionServices.TouristCount_SelectedAccomodationType != null)
                    ddlAccomType.SelectedValue = SessionServices.TouristCount_SelectedAccomodationType;

            btnShow_Click(sender, e);
        }
    }

    private void AddAttributes()
    {
        //txtStartDate.Attributes.Add("onchange", "fillEndDate()");
        txtStartDate.Attributes.Add("onchange", "return fillEndDate('" + txtStartDate.ClientID + "','" + txtEndDate.ClientID + "');");
        btnShow.Attributes.Add("onclick", "return validateBeforeGettingBookings()");
    }
    private void FillAccomodationTypes()
    {
        AccomTypeDTO[] oAccomTypeData = GetAccomodationTypeDetails();
        ddlAccomType.Items.Clear();
        //ddlAccomSubtype.Items.Clear();
        SortedList slAccomTypes = new SortedList();
        slAccomTypes.Add(-1, "Choose Accom Type");
        slAccomTypes.Add(0, "All");
        if (oAccomTypeData != null)
        {
            for (int i = 0; i < oAccomTypeData.Length; i++)
            {
                slAccomTypes.Add(Convert.ToInt32(oAccomTypeData[i].AccomodationTypeId), Convert.ToString(oAccomTypeData[i].AccomodationType));
            }
        }
        ddlAccomType.DataSource = slAccomTypes;
        ddlAccomType.DataTextField = "value";
        ddlAccomType.DataValueField = "key";
        ddlAccomType.DataBind();
    }

    private AccomTypeDTO[] GetAccomodationTypeDetails()
    {
        //Filling the Object which contains the Accomodation type and also all the respective accomodations
        //This is saved in the session and then the Drop downs are filled.
        AccomodationTypeMaster objATM;
        objATM = new AccomodationTypeMaster();
        AccomTypeDTO[] oAccomTypeData = null;
        //following line has been commented by vijay to get the acomodations user wise
        //oAccomTypeData = objATM.GetAccomTypeWithAccomDetails(0); 

        oAccomTypeData = objATM.GetAccomTypeWithAccomDetails();
        return oAccomTypeData;
    }

    protected void dgBookings_ItemCommand(object source, DataGridCommandEventArgs e)
    {
        string cFormUrl = string.Empty;
        try
        {
            if (e.Item.ItemIndex >= 0)
            {
                int iBookingID = Convert.ToInt32(dgTouristCount.DataKeys[e.Item.ItemIndex].ToString());
                switch (e.CommandName.ToString().ToUpper())
                {
                    case "VIEWBOOKING":
                        Response.Redirect("Booking.aspx?bid=" + iBookingID.ToString() + "&mode=view");
                        break;
                    case "VIEWTOURIST":
                        Response.Redirect("ViewTourists.aspx?bid=" + iBookingID.ToString());
                        break;
                    default:
                        break;
                }
            }
        }
        catch (Exception exp)
        {
            GF.LogError("TouristCount.dgBookings_ItemCommand", exp.Message);
            return;
        }

    }

    protected void btnShow_Click(object sender, EventArgs e)
    {
        SessionServices.DeleteSession(Constants._TouristCount_BookingData);
        if (txtStartDate.Text != "")
            SessionServices.TouristCount_SelectedCheckInDate = txtStartDate.Text.ToString();
        if (txtEndDate.Text != "")
            SessionServices.TouristCount_SelectedCheckOutDate = txtEndDate.Text.ToString();
        if (ddlAccomType.SelectedIndex != 0)
            SessionServices.TouristCount_SelectedAccomodationType = ddlAccomType.SelectedValue;
        //if (ddlBookingStatusTypes.SelectedIndex != 0)
        //SessionHandler.TouristCount_SelectedBookingStatus = ddlBookingStatusTypes.SelectedValue;
        RefreshGrid();
    }

    protected void dgBookings_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
    {
        dgTouristCount.CurrentPageIndex = e.NewPageIndex;
        RefreshGrid();
    }

    protected void dgBookings_ItemDataBound(object sender, DataGridItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            if (string.Compare(e.Item.Cells[5].Text, "BOOKED", true) == 0)
            {
                e.Item.Cells[0].BackColor = System.Drawing.Color.Aqua;
                e.Item.Cells[10].Text = "";
                e.Item.Cells[11].Text = "";
                e.Item.Cells[12].Text = "";
                e.Item.Cells[13].Text = "";
                e.Item.Cells[14].Text = "";

                //e.Item.Cells[10].Visible = false;
                //e.Item.Cells[11].Visible = false;
                //e.Item.Cells[12].Visible = false;
                //e.Item.Cells[13].Visible = false;
                //e.Item.Cells[14].Visible = false;
            }
            else if (string.Compare(e.Item.Cells[5].Text, "CONFIRMED", true) == 0)
            {
                e.Item.Cells[0].BackColor = System.Drawing.Color.Lime;
                e.Item.Cells[6].Text = "";

                LinkButton bc;
                bc = (LinkButton)(e.Item.Cells[9].Controls[0]);
                if (bc != null)
                    bc.Text = "Edit Confirmation";
                e.Item.Cells[10].Visible = true;
                e.Item.Cells[11].Visible = true;
                e.Item.Cells[12].Visible = true;
                e.Item.Cells[13].Visible = true;
                e.Item.Cells[14].Visible = true;
            }
            else if (string.Compare(e.Item.Cells[5].Text, "CANCELLED", true) == 0)
            {
                e.Item.Cells[0].BackColor = System.Drawing.Color.Red;
                e.Item.Cells[6].Text = "";
                e.Item.Cells[7].Text = "";
                e.Item.Cells[9].Text = "";
                e.Item.Cells[10].Text = "";
                e.Item.Cells[11].Text = "";
                e.Item.Cells[12].Text = "";
                e.Item.Cells[13].Text = "";
                e.Item.Cells[14].Text = "";
            }
            else if (string.Compare(e.Item.Cells[5].Text, "WAITLISTED", true) == 0)
            {
                e.Item.Cells[0].BackColor = System.Drawing.Color.Orange;
                e.Item.Cells[8].Text = "";
                e.Item.Cells[9].Text = "";
                e.Item.Cells[10].Text = "";
                e.Item.Cells[11].Text = "";
                e.Item.Cells[12].Text = "";
                e.Item.Cells[13].Text = "";
                e.Item.Cells[14].Text = "";
            }
            if (Boolean.Parse(e.Item.Cells[15].Text) == true)
            {
                e.Item.Cells[8].Text = "";
                e.Item.Cells[9].Text = "";
                e.Item.Cells[10].Text = "";
                e.Item.Cells[11].Text = "";
                e.Item.Cells[12].Text = "";
                e.Item.Cells[13].Text = "";
                e.Item.Cells[14].Text = "";
            }

            /*if (CForm)
            {
                e.Item.Cells[6].Text = "";
                e.Item.Cells[7].Text = "";
                e.Item.Cells[8].Text = "";
                e.Item.Cells[9].Text = "";
                e.Item.Cells[10].Text = "";
                e.Item.Cells[11].Text = "";
                e.Item.Cells[14].Text = "";

                dgTouristCount.Columns[6].Visible = false;
                dgTouristCount.Columns[7].Visible = false;
                dgTouristCount.Columns[8].Visible = false;
                dgTouristCount.Columns[9].Visible = false;
                dgTouristCount.Columns[10].Visible = false;
                dgTouristCount.Columns[11].Visible = false;
                dgTouristCount.Columns[14].Visible = false;

                dgTouristCount.Columns[12].Visible = true;
                dgTouristCount.Columns[13].Visible = true;
            }
            else
            {
                e.Item.Cells[12].Text = "";
                e.Item.Cells[13].Text = "";
                dgTouristCount.Columns[12].Visible = false;
                dgTouristCount.Columns[13].Visible = false;
            }*/
        }
    }

    private void FillBookingStatusTypes()
    {
        /*BookingStatusMaster oBookingStatusMaster;
        clsBookingStatusDTO[] oBookingStatusData = null;
        ListItem l = null;
        oBookingStatusMaster = new BookingStatusMaster();
        oBookingStatusData = oBookingStatusMaster.GetData();

        ddlBookingStatusTypes.Items.Clear();

        l = new ListItem();
        l.Text = "Choose";
        l.Value = "-1";
        ddlBookingStatusTypes.Items.Insert(0, l);

        l = new ListItem();
        l.Text = "All";
        l.Value = "0";
        ddlBookingStatusTypes.Items.Insert(1, l);

        if (oBookingStatusData != null)
        {
            for (int i = 0; i < oBookingStatusData.Length; i++)
            {
                l = new ListItem();
                l.Text = oBookingStatusData[i].BookingStatusType;
                l.Value = Convert.ToString(oBookingStatusData[i].BookingStatusId);
                ddlBookingStatusTypes.Items.Insert(i + 2, l);
            }
        }
         */
    }

    private void RefreshGrid()
    {
        BookingReportServices bookingReportManager = new BookingReportServices();
        List<clsTouristCountDTO> touristCountDto = null;
        DateTime checkInDate, checkOutDate;
        DateTime.TryParse(txtStartDate.Text, out checkInDate);
        DateTime.TryParse(txtEndDate.Text, out checkOutDate);

        int AccomTypeId = 0;
        Int32.TryParse(ddlAccomType.SelectedValue.ToString(), out AccomTypeId);

        if (AccomTypeId <= 0) AccomTypeId = 0; //To handle the -1 value of Choose option.

        if (SessionServices.TouristCount_BookingData == null)
        {
            if (checkInDate != DateTime.MinValue && checkOutDate != DateTime.MinValue)
            {
                touristCountDto = bookingReportManager.GetTouristCount(checkInDate, checkOutDate, AccomTypeId, 0);
                SessionServices.TouristCount_BookingData = touristCountDto;
            }
        }
        else
        {
            touristCountDto = SessionServices.TouristCount_BookingData;
        }

        dgTouristCount.DataSource = null;
        dgTouristCount.DataBind();
        if (touristCountDto != null && touristCountDto.Count > 0)
        {
            dgTouristCount.DataSource = touristCountDto;
            dgTouristCount.DataBind();
        }
        else
        {
            dgTouristCount.DataSource = null;
            dgTouristCount.DataBind();
            if (IsPostBack)
            {
                base.DisplayAlert("Bookings are not found for the mentioned criteria.");
            }
        }

        bookingReportManager = null;
        touristCountDto = null;
    }

    private ENums.BookingStatusTypes GetBookingStatusType(string BookingStatus)
    {
        ENums.BookingStatusTypes BST = ENums.BookingStatusTypes.NONE;
        switch (BookingStatus.ToUpper())
        {
            case "CHOOSE":
            case "ALL":
                BST = ENums.BookingStatusTypes.NONE;
                break;
            case "BOOKED":
                BST = ENums.BookingStatusTypes.BOOKED;
                break;
            case "CONFIRMED":
                BST = ENums.BookingStatusTypes.CONFIRMED;
                break;
            case "WAITLISTED":
                BST = ENums.BookingStatusTypes.WAITLISTED;
                break;
            case "CANCELLED":
                BST = ENums.BookingStatusTypes.CANCELLED;
                break;
            default:
                break;
        }
        return BST;
    }

    protected void ddlAccomType_SelectedIndexChanged(object sender, EventArgs e)
    {
        //FillAccomodations(Convert.ToInt32(ddlAccomType.SelectedValue));
    }
}
