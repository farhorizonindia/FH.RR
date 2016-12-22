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
using FarHorizon.Reservations.Common.DataEntities.Masters;
using FarHorizon.Reservations.Common.DataEntities.Client;
using FarHorizon.Reservations.MasterServices;
using FarHorizon.Reservations.Bases;
using FarHorizon.Reservations.Bases.BasePages;
using FarHorizon.Reservations.Common.DataEntities.InputOutput;
using System.Text;
using System.IO;

public partial class ClientUI_CruiseRepTest : ClientBasePage
{
    public DataSet ds1;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            headings1.Style.Add("display", "none");
            headings.Style.Add("display", "none");
            FillBookingStatusTypes();
            FillAccomodationTypes();


            FillAgents();

            // RefreshGrid();
            if (SessionServices.ViewBooking_SelectedCheckInDate != null)
                txtStartDate.Text = SessionServices.ViewBooking_SelectedCheckInDate;
            if (SessionServices.ViewBooking_SelectedCheckOutDate != null)
                txtEndDate.Text = SessionServices.ViewBooking_SelectedCheckOutDate;
            if (SessionServices.ViewBooking_SelectedBookingStatus != null)
                ddlBookingStatusTypes.SelectedValue = SessionServices.ViewBooking_SelectedBookingStatus;
            if (SessionServices.ViewBooking_SelectedAccomodationType != null)
                ddlAccomType.SelectedValue = SessionServices.ViewBooking_SelectedAccomodationType;

            //  btnShow_Click(sender, e);

            ddlAccomType.SelectedValue = "8";
            ddlAccomType.Enabled = false;
        }

        RefreshGrid();
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

    private AccomTypeDTO[] GetAccomodationTypeDetails()
    {
        //Filling the Object which contains the Accomodation type and also all the respective accomodations
        //This is saved in the session and then the Drop downs are filled.
        AccomodationTypeMaster accomodationTypeMaster;
        accomodationTypeMaster = new AccomodationTypeMaster();
        AccomTypeDTO[] oAccomTypeData = null;
        //following line has been commented by vijay to get the acomodations user wise
        //oAccomTypeData = objATM.GetAccomTypeWithAccomDetails(0); 

        oAccomTypeData = accomodationTypeMaster.GetAccomTypeWithAccomDetails();
        return oAccomTypeData;
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


    private void FillBookingStatusTypes()
    {
        BookingStatusMaster oBookingStatusMaster;
        BookingStatusDTO[] oBookingStatusData = null;
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
    }
    protected void btnShow_Click(object sender, EventArgs e)
    {
        SessionServices.DeleteSession(Constants._ViewBooking_BookingData);
        if (txtStartDate.Text != "")
            SessionServices.ViewBooking_SelectedCheckInDate = txtStartDate.Text.ToString();
        if (txtEndDate.Text != "")
            SessionServices.ViewBooking_SelectedCheckOutDate = txtEndDate.Text.ToString();
        if (ddlAccomType.SelectedIndex != 0)
            SessionServices.ViewBooking_SelectedAccomodationType = ddlAccomType.SelectedValue;
        if (ddlBookingStatusTypes.SelectedIndex != 0)
            SessionServices.ViewBooking_SelectedBookingStatus = ddlBookingStatusTypes.SelectedValue;
        RefreshGrid();
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
            case "PROPOSED":
                BST = ENums.BookingStatusTypes.PROPOSED;
                break;
            default:
                break;
        }
        return BST;
    }
    private void RefreshGrid()
    {


        ds1 = new DataSet();
        ENums.BookingStatusTypes bookingStatusType = ENums.BookingStatusTypes.NONE;
        ENums.BookingStatusTypes newBookingStatusType = ENums.BookingStatusTypes.NONE;
        BookingServices oBookingManager = new BookingServices();
        List<ViewBookingDTO> oBookingData = null;
        DateTime checkInDate, checkOutDate;
        DateTime.TryParse(txtStartDate.Text, out checkInDate);
        DateTime.TryParse(txtEndDate.Text, out checkOutDate);

        bookingStatusType = GetBookingStatusType(ddlBookingStatusTypes.SelectedItem.Text);
        int AccomTypeId = 0;

        Int32.TryParse(ddlAccomType.SelectedValue.ToString(), out AccomTypeId);

        if (AccomTypeId <= 0) AccomTypeId = 0; //To handle the -1 value of Choose option.

        if (SessionServices.ViewBooking_BookingData == null)
        {

            newBookingStatusType = bookingStatusType;
            if (bookingStatusType == ENums.BookingStatusTypes.PROPOSED)
            {
                newBookingStatusType = ENums.BookingStatusTypes.BOOKED;
            }

            cdtGetBookingsInput getBookingsInput = new cdtGetBookingsInput();
            getBookingsInput.AccomTypeId = AccomTypeId;
            getBookingsInput.FromDate = checkInDate;
            getBookingsInput.ToDate = checkOutDate;
            getBookingsInput.BookingStatusType = newBookingStatusType;
            getBookingsInput.BookingCode = txtBookingCode.Text.Trim();
            getBookingsInput.AgentId = Convert.ToInt32(ddlAgent.SelectedValue.ToString());



            ds1 = oBookingManager.GetBookingsCruiseFH(getBookingsInput);
            if (ds1.Tables[0].Rows.Count > 0)
            {
                headings1.Style.Remove("display");
                headings.Style.Remove("display");
            }
            else
            {
                headings1.Style.Add("display", "none");
                headings.Style.Add("display", "none");
            }

            if (bookingStatusType == ENums.BookingStatusTypes.PROPOSED)
            {
                oBookingData = oBookingData.FindAll(delegate(ViewBookingDTO booking) { return booking.ProposedBooking == true; });
            }
            SessionServices.ViewBooking_BookingData = oBookingData;

        }
        else
        {
            oBookingData = SessionServices.ViewBooking_BookingData;
        }

        //dgBookings.DataSource = null;
        //dgBookings.DataBind();
        //if (ds1 != null && ds1.Tables[0].Rows.Count > 0)
        //{
        //    dgBookings.DataSource = ds1;
        //    if (dgBookings.PageCount > 0)
        //    {
        //        dgBookings.CurrentPageIndex = dgBookings.CurrentPageIndex > dgBookings.PageCount ? dgBookings.PageCount : dgBookings.CurrentPageIndex;
        //    }
        //    dgBookings.DataBind();
        //}
        //else
        //{
        //    if (IsPostBack)
        //    {

        //        base.DisplayAlert("Bookings are not found for the mentioned criteria.");
        //    }
        //}
        oBookingManager = null;
        oBookingData = null;
    }
    protected void dgBookings_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
    {
        //dgBookings.CurrentPageIndex = e.NewPageIndex;
        RefreshGrid();
    }

    protected void btnExport_Click(object sender, EventArgs e)
    {
        ExportToExcel();
    }


    protected void ExportToExcel()
    {
        Response.ContentType = "application/x-msexcel";
        Response.AddHeader("Content-Disposition", "attachment;filename=ExcelFile.xls");
        Response.ContentEncoding = Encoding.UTF8;
        StringWriter tw = new StringWriter();
        HtmlTextWriter hw = new HtmlTextWriter(tw);
       //tbl.RenderControl(hw);
        Response.Write(tw.ToString());
        Response.End();
    }

    //public override void VerifyRenderingInServerForm(Control control)
    //{
    //    /* Verifies that the control is rendered */
    //}
    protected void btnExport_Click1(object sender, EventArgs e)
    {
        try
        {


        }

        catch
        {

        }
    }
}