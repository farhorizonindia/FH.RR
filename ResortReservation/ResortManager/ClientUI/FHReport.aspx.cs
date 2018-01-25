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

public partial class FHReport : ClientBasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            FillBookingStatusTypes();
            FillAccomodationTypes();
            FillAgents();
            if (SessionServices.ViewBooking_SelectedCheckInDate != null)
                txtStartDate.Text = SessionServices.ViewBooking_SelectedCheckInDate;
            if (SessionServices.ViewBooking_SelectedCheckOutDate != null)
                txtEndDate.Text = SessionServices.ViewBooking_SelectedCheckOutDate;
            if (SessionServices.ViewBooking_SelectedBookingStatus != null)
                ddlBookingStatusTypes.SelectedValue = SessionServices.ViewBooking_SelectedBookingStatus;
            if (SessionServices.ViewBooking_SelectedAccomodationType != null)
                ddlAccomType.SelectedValue = SessionServices.ViewBooking_SelectedAccomodationType;

            btnShow_Click(sender, e);
        }

    }
    private void RefreshGrid()
    {
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
            if (!String.IsNullOrEmpty(txtBookingCode.Text.Trim()) || (checkInDate != DateTime.MinValue && checkOutDate != DateTime.MinValue))
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

                oBookingData = oBookingManager.GetBookingsFH(getBookingsInput);
                if (bookingStatusType == ENums.BookingStatusTypes.PROPOSED)
                {
                    oBookingData = oBookingData.FindAll(delegate(ViewBookingDTO booking) { return booking.ProposedBooking == true; });
                }
                SessionServices.ViewBooking_BookingData = oBookingData;
            }
        }
        else
        {
            oBookingData = SessionServices.ViewBooking_BookingData;
        }

        dgBookings.DataSource = null;
        dgBookings.DataBind();
        if (oBookingData != null && oBookingData.Count > 0)
        {
            dgBookings.DataSource = oBookingData;
            if (dgBookings.PageCount > 0)
            {
                dgBookings.CurrentPageIndex = dgBookings.CurrentPageIndex > dgBookings.PageCount ? dgBookings.PageCount : dgBookings.CurrentPageIndex;
            }
            dgBookings.DataBind();
        }
        else
        {
            if (IsPostBack)
            {

                base.DisplayAlert("Bookings are not found for the mentioned criteria.");
            }
        }
        oBookingManager = null;
        oBookingData = null;
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

    private void FillAgents()
    {
        try
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
        catch { }
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
    protected void dgBookings_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
    {
        dgBookings.CurrentPageIndex = e.NewPageIndex;
        RefreshGrid();
    }
    protected void dgBookings_ItemDataBound(object sender, DataGridItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            #region Booking & Proposed
            if (string.Compare(e.Item.Cells[14].Text, "BOOKED", true) == 0)
            {
                e.Item.Cells[14].BackColor = System.Drawing.Color.Aqua;
                //    e.Item.Cells[10].Text = "";
                //   e.Item.Cells[11].Text = "";

                //    e.Item.Cells[14].Text = "";


                if (string.Compare(e.Item.Cells[15].Text.ToUpper(), "TRUE", true) == 0)
                {
                    e.Item.Cells[14].BackColor = System.Drawing.Color.Blue;
                    e.Item.Cells[14].ForeColor = System.Drawing.Color.White;
                    e.Item.Cells[14].Text = "PROPOSED";
                }
            }
            #endregion
            #region Confirmed
            else if (string.Compare(e.Item.Cells[14].Text, "CONFIRMED", true) == 0)
            {
                e.Item.Cells[14].BackColor = System.Drawing.Color.Lime;
                //   e.Item.Cells[6].Text = "";

                // LinkButton bc;
                ////  bc = (LinkButton)(e.Item.Cells[9].Controls[0]);
                //   if (bc != null)
                //       bc.Text = "Edit Confirmation";
                ////    e.Item.Cells[10].Visible = true;
                //    e.Item.Cells[11].Visible = true;
                //e.Item.Cells[12].Visible = true;
                //e.Item.Cells[13].Visible = true;
                //  e.Item.Cells[14].Visible = true;
            }
            #endregion
            #region Cancelled
            else if (string.Compare(e.Item.Cells[14].Text, "CANCELLED", true) == 0)
            {
                e.Item.Cells[14].BackColor = System.Drawing.Color.Red;
                //      e.Item.Cells[6].Text = "";
                //      e.Item.Cells[7].Text = "";
                ////     e.Item.Cells[9].Text = "";
                //    e.Item.Cells[10].Text = "";
                //    e.Item.Cells[11].Text = "";
                //e.Item.Cells[12].Text = "";
                //e.Item.Cells[13].Text = "";
                //  e.Item.Cells[14].Text = "";
            }
            #endregion
            #region Waitlisted
            else if (string.Compare(e.Item.Cells[14].Text, "WAITLISTED", true) == 0)
            {
                e.Item.Cells[14].BackColor = System.Drawing.Color.Orange;
                //  e.Item.Cells[8].Text = "";
                //   e.Item.Cells[9].Text = "";
                //   e.Item.Cells[10].Text = "";
                //   e.Item.Cells[11].Text = "";
                //e.Item.Cells[12].Text = "";
                //e.Item.Cells[13].Text = "";
                //  e.Item.Cells[14].Text = "";
            }
            #endregion

            #region Proposed
            //if (Boolean.Parse(e.Item.Cells[15].Text) == true)
            //{
            //    //   e.Item.Cells[8].Text = "";
            //    // e.Item.Cells[9].Text = "";
            //    //  e.Item.Cells[10].Text = "";
            //    // e.Item.Cells[11].Text = "";
            //    //e.Item.Cells[12].Text = "";
            //    //e.Item.Cells[13].Text = "";
            //    //   e.Item.Cells[14].Text = "";
            //}
            #endregion

            #region
            if (Boolean.Parse(e.Item.Cells[16].Text) == true)
            {
                e.Item.BackColor = System.Drawing.Color.Teal;
               
            }

            #endregion

        }
    }
    protected void btnExport_Click(object sender, EventArgs e)
    {
        ExportToExcel();
    }


    protected void ExportToExcel()
    {
        Response.Clear();
        Response.Buffer = true;
        Response.AddHeader("content-disposition", "attachment;filename=ResortBookingReport.xls");
        Response.Charset = "";
        Response.ContentType = "application/vnd.ms-excel";
        using (StringWriter sw = new StringWriter())
        {
            HtmlTextWriter hw = new HtmlTextWriter(sw);


            dgBookings.AllowPaging = false;
            RefreshGrid();




            dgBookings.RenderControl(hw);

            //style to format numbers to string
            string style = @"<style> .textmode { } </style>";
            Response.Write(style);
            Response.Output.Write(sw.ToString());
            Response.Flush();
            Response.End();
        }
    }

    public override void VerifyRenderingInServerForm(Control control)
    {
        /* Verifies that the control is rendered */
    }



}