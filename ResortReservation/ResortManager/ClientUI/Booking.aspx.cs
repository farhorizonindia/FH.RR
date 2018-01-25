using System;
using System.Collections;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using FarHorizon.Reservations.Bases.BasePages;
using FarHorizon.Reservations.BusinessServices;
using FarHorizon.Reservations.Common;
using FarHorizon.Reservations.Common.DataEntities.Client;
using FarHorizon.Reservations.Common.DataEntities.Masters;
using FarHorizon.Reservations.MasterServices;

using System.Data;
using System.Configuration;
using System.Data.SqlClient;
using FarHorizon.Reservations.BusinessServices.Online.BAL;
using FarHorizon.Reservations.BusinessServices.Online.DAL;
using System.Text;
using FarHorizon.Reservations.DataBaseManager;
using FarHorizon.Reservations.BusinessTier.BusinessLogic.BookingEngine;
using NewControls;


public partial class ClientUI_Booking : ClientBasePage
{
    BALHotelBooking blht = new BALHotelBooking();
    DALHotelBooking dlht = new DALHotelBooking();
    DALOpenDates opndal = new DALOpenDates();
    BALBooking blsr = new BALBooking();
    DALBooking dlsr = new DALBooking();
    BALPackageMaster bpm = new BALPackageMaster();
    DALPackageMaster dpm = new DALPackageMaster();
    DALAgentPayment dagent = new DALAgentPayment();
    DataTable Returndt;
    DataView dv;
    double total = 0;
    decimal paid = 0;
    AccomTypeDTO[] oAccomTypeData;
    int iBookingId = 0;
    Table tblMaster = null;
    BALAgentPayment blAgentpayment = new BALAgentPayment();
    DALAgentPayment dlAgentpayment = new DALAgentPayment();
    int GetQueryResponse = 0;
    DataTable dtGetReturenedData;

    BALLinks blLinks = new BALLinks();
    DALLinks dlLinks = new DALLinks();
    DataTable dtGetReturnedData;
    int totalEventHandlersAdded = 0;
    int eventCounter = 0;

    List<AddRoomEventingTracker> addRoomEventingTrackers;
    DatabaseManager oDB;
    #region Event handlers
    protected void Page_Load(object sender, EventArgs e)
    {
        string sCtrlName = "";
        DateTime sd;
        DateTime ed;
        int iAccomId = 0;

        addRoomEventingTrackers = new List<AddRoomEventingTracker>();

        sCtrlName = GetPostBackControlID();
        ClearSessionVariables();

        if (Request.QueryString["bid"] != null)
            int.TryParse(Request.QueryString["bid"].ToString(), out iBookingId);

        #region Adding Attributes
        AddAttributesToControls();
        #endregion Adding Attributes
        SetButtonsState();
        #region Not Postback
        if (!IsPostBack)
        {
            //  btnGetAvailableRooms.Enabled = true;
            SetAccomodationTypeDetails();
            FillAccomodationTypes();
            FillAgents();
            SessionServices.Booking_BookingId = -1; ;
            btnReset.Visible = true;
            getlocalagent();
            #region If Existing Booking
            if (iBookingId > 0)
            {
                //  btnGetAvailableRooms.Enabled = false;

                SessionServices.Booking_BookingId = -1; ;
                FillDetailsinFields(iBookingId);
                btnConfirmBooking.Enabled = true;
                btnReset.Visible = false;
                DateTime.TryParse(txtStartDate.Text, out sd);
                DateTime.TryParse(txtEndDate.Text, out ed);
                if (ddlAccomName.SelectedItem != null)
                    int.TryParse(ddlAccomName.SelectedItem.Value, out iAccomId);

                if (Convert.ToBoolean(hdnchartered.Value) == true)
                {
                    PrepareRoomChartpgload(sd, ed.AddHours(12).AddMinutes(60).AddSeconds(60), iAccomId);
                }
                else
                {
                    PrepareRoomChart(sd, ed.AddHours(12).AddMinutes(60).AddSeconds(60), iAccomId);
                }


                #region Buttons State when Existing Booking


                #endregion Buttons State when Existing Booking
            }
            #endregion

            #region If New Booking
            if (iBookingId == 0)
            {
                txtBookingStatus.Text = string.Empty;
                int AccomTypeId = 0, AccomId = 0;
                DateTime StartDate = DateTime.MinValue;

                AccomTypeId = Convert.ToInt32(Request.QueryString["AccomTypeId"]);
                FillAccomodations(AccomTypeId);
                AccomId = Convert.ToInt32(Request.QueryString["AccomId"]);
                StartDate = Convert.ToDateTime(Request.QueryString["sdate"]);
                ddlAccomType.SelectedValue = Convert.ToString(AccomTypeId);
                ddlAccomName.SelectedValue = Convert.ToString(AccomId);
                if (StartDate != DateTime.MinValue)
                {
                    txtStartDate.Text = GF.GetDD_MMM_YYYY(StartDate, false);
                    txtEndDate.Text = GF.GetDD_MMM_YYYY(StartDate.AddDays(1), false);
                }
                else
                {
                    txtStartDate.Text = "";
                    txtEndDate.Text = "";
                }
                if (ddlAccomType.SelectedValue == "8" || ddlAccomType.SelectedValue == "0")
                {

                    // Out.Visible = false;
                    Out.Style.Add("display", "none");
                    ddlpackage.Visible = true;
                    lP.Visible = true;
                    lblPackages.Visible = true;
                }
                else
                {

                    // Out.Visible = true;
                    Out.Style.Add("display", "true");
                    ddlpackage.Visible = false;
                    lP.Visible = false;
                    lblPackages.Visible = false;
                }
                #region Button State When New Booking
                btnBookTour.Visible = true;
                btnBookTour.Enabled = true;

                btnConfirmBooking.Visible = true;
                btnConfirmBooking.Enabled = false;

                btnCancel.Visible = true;
                btnCancel.Enabled = false;

                btnReset.Visible = true;
                btnReset.Enabled = true;

                #endregion Button State When New Booking

            }
            else
            {
                if (ddlAccomType.SelectedValue == "8")
                {

                    // Out.Visible = false;
                    Out.Style.Add("display", "none");
                    ddlpackage.Visible = true;
                    lP.Visible = true;
                    lblPackages.Visible = true;
                }
                else
                {

                    // Out.Visible = true;
                    Out.Style.Add("display", "true");
                    ddlpackage.Visible = false;
                    lP.Visible = false;
                    lblPackages.Visible = false;
                }
            }
            #endregion
        }
        #endregion

        #region IsPostBack = True
        if (IsPostBack)
        {
            if (iBookingId == 0)
            {
                txtBookingStatus.Text = string.Empty;
                btnDeleteBooking.Visible = false;
            }
            if (string.Compare(sCtrlName, "ddlAccomType", true) == 0)
            {
                RemoveRoomObjectFromSession();
            }
            if (sCtrlName == "btnGetAvailableRooms")
            {
                #region If btnGetAvailableRooms is Clicked
                if (txtStartDate.Text != "" && txtEndDate.Text != "" && ddlAccomType.SelectedIndex != 0 && ddlAccomName.SelectedIndex != 0)
                {
                    RemoveRoomObjectFromSession();
                }
                else
                {
                    lblErrorMsg.Text = sCtrlName;
                }
                #endregion If btnGetAvailableRooms is Clicked
            }
            else if (string.Compare(sCtrlName, "btnConfirmBooking", true) == 0)
            {
            }
            else if (string.Compare(sCtrlName, "btnBookTour", true) == 0)
            {
                if (GetRoomObjectFromSession() == null)
                {
                    lblErrorMsg.Text = "Please click on 'Get Available Rooms' to get the current room status.";
                    return;
                }
            }
            else
            {
                //if (hdnchartered.Value != null && hdnchartered.Value.ToString() != "")
                //{
                //    if (Convert.ToBoolean(hdnchartered.Value) == true)
                //    {
                //        PrepareRoomChartpgload();
                //    }
                //    else
                //    {
                //        PrepareRoomChart();
                //    }
                //}
                //else
                //{
               
                    PrepareRoomChart();
                
                //}
            }
            if (ddlAccomType.SelectedValue == "8" || ddlAccomType.SelectedValue == "0")
            {
                //  txtEndDate.Visible = false;
                // Out.Visible = false;
                Out.Style.Add("display", "none");
                ddlpackage.Visible = true;
                lP.Visible = true;
                lblPackages.Visible = true;
            }
            else
            {
                //txtEndDate.Visible = true;
                //Out.Visible = true;
                Out.Style.Add("display", "true");
                ddlpackage.Visible = false;
                lP.Visible = false;
                lblPackages.Visible = false;
            }
        }
        else { totalEventHandlersAdded = 0; }
        #endregion IsPostBack = True

        if (iBookingId > 0)
        {
            hidStartDate.Value = txtStartDate.Text.ToString();
            hidEndDate.Value = txtEndDate.Text.ToString();
        }

        if (Request.QueryString["mode"] != null)
        {
            if (String.Compare(Request.QueryString["mode"], "view", true) == 0)
            {
                #region Button State When View Mode
                btnBookTour.Visible = true;
                btnBookTour.Enabled = false;

                btnConfirmBooking.Visible = true;
                btnConfirmBooking.Enabled = false;

                btnCancel.Visible = true;
                btnCancel.Enabled = false;

                btnReset.Visible = true;
                btnReset.Enabled = false;
                #endregion
            }
        }
        SendAccomodationSeasonDetailtoJS();

    }
    private decimal getcommission(int accomtype, int accomname, decimal paid)
    {
        decimal commission = 0;
        DataTable dt = dagent.selectbyaccom(accomtype, accomname);
        if (dt != null && dt.Rows.Count > 0)
        {
            commission = Convert.ToDecimal(dt.Rows[0]["Commision"].ToString());
            commission = (paid * commission) / 100;
        }
        return commission;
    }
    public void bindRoomRatesCruise(int totalpax)
    {
        try
        {
            ViewState["Rrate"] = null;
            blsr.AgentId = Convert.ToInt32(ddlAgentType.SelectedValue);
            blsr.AgentIdRef = Convert.ToInt32(ddlAgent.SelectedValue);

            blsr.action = "RoomRates";
            blsr.AgentId = Convert.ToInt32(ddlAgentType.SelectedValue);
            blsr.AgentIdRef = Convert.ToInt32(ddlAgent.SelectedValue);



            //blsr.action = "RoomRatesCustAgent";

            blsr._dtStartDate = Convert.ToDateTime(txtStartDate.Text.Trim());



            string strCon = ConfigurationManager.ConnectionStrings["ReservationConnectionString"].ConnectionString;

            SqlConnection cn = new SqlConnection(strCon);
            SqlDataAdapter da = new SqlDataAdapter("select packageId from tblcruiseopendates  where  convert(nvarchar(27),CheckInDate,106)=   convert(nvarchar(27),@CheckInDate,106) and   convert(nvarchar(27),CheckOutDate,106)=   convert(nvarchar(27),@CheckOutDate,106) ", cn);
            da.SelectCommand.Parameters.AddWithValue("@CheckInDate", Convert.ToDateTime(txtStartDate.Text.Trim()));
            da.SelectCommand.Parameters.AddWithValue("@CheckOutDate", Convert.ToDateTime(txtEndDate.Text.Trim()));
            DataTable dtReturnData = new DataTable();
            da.Fill(dtReturnData);
            if (dtReturnData != null)
            {
                blsr.PackageId = dtReturnData.Rows[0][0].ToString();
            }
            else
            {
                blsr.PackageId = "";
            }




            blsr.totpax = totalpax;

            DataTable dt = new DataTable();


            dt = dlsr.GetRoomCategoryWiseRates(blsr);


            DataView dv = dt.DefaultView;
            dv.Sort = "PPRoomRate asc";
            DataTable sortedDT = dv.ToTable();

            if (sortedDT != null)
            {
                if (sortedDT.Rows.Count > 0)
                {
                    gdvRatesCruise.DataSource = sortedDT;
                    gdvRatesCruise.DataBind();

                    ViewState["Rrate"] = sortedDT;





                }
                else
                {
                    gdvRatesCruise.DataSource = null;
                    gdvRatesCruise.DataBind();

                }
            }
            else
            {


                gdvRatesCruise.DataSource = null;
                gdvRatesCruise.DataBind();
            }

        }
        catch
        {

        }
    }

    private void bindRoomRates(int accmid, int Totpax, int agid, DateTime chkin, DateTime chkout, int norooms, int RtypeId)
    {
        try
        {
            ViewState["Rrate"] = null;

            blht.action = "RoomRate";

            //blht.action = "RoomRateCust";

            blht.Accomid = accmid;
            blht.TotPax = Totpax;
            blht.Reqnoofrooms = norooms;
            blht.checkin = chkin;
            blht.Checkout = chkout;
            blht.RoomTypeId = RtypeId;

            blht.agentid = agid;
            Returndt = dlht.GetHotelRates(blht);

            if (Returndt != null)
            {
                //Session["RoomInfo"] = Returndt;
                SessionServices.SaveSession<DataTable>("RoomInfo", Returndt);

                //  dv.RowFilter = "ActualRoomTypeId<>0";
                if (Returndt.Rows.Count > 0)
                {
                    gdvRatesHotel.DataSource = Returndt;
                    gdvRatesHotel.DataBind();
                }
                else
                {
                    gdvRatesHotel.DataSource = null;
                    gdvRatesHotel.DataBind();
                }
                ViewState["Rrate"] = Returndt;
            }
            else
            {
                gdvRatesHotel.DataSource = null;
                gdvRatesHotel.DataBind();
            }
        }
        catch
        {
        }
    }

    protected void Page_PreRender(object sender, EventArgs e)
    {
        string sCtrlName = GetPostBackControlID();
        if (string.Compare(sCtrlName, "ddlAccomType", true) == 0)
        {
            System.Web.UI.ScriptManager.GetCurrent(this).SetFocus(this.ddlAccomType);
        }
        if (string.Compare(sCtrlName, "btnGetAvailableRooms", true) == 0)
        {
            System.Web.UI.ScriptManager.GetCurrent(this).SetFocus(this.txtBookingRef);
        }
        if (sCtrlName.StartsWith("ddl*"))
        {
            System.Web.UI.ScriptManager.GetCurrent(this).SetFocus(sCtrlName);
        }
        if (!IsPostBack)
        {
            System.Web.UI.ScriptManager.GetCurrent(this).SetFocus(txtStartDate);
        }
    }

    protected void btnGetAvailableRooms_Click(object sender, EventArgs e)
    {
        if (ddlpackage.SelectedIndex > 0)
        {
            gdvRatesCruise.DataSource = null;
            gdvRatesHotel.DataSource = null;
            gdvRatesCruise.DataBind();
            gdvRatesHotel.DataBind();
            checkwaitlisted();
            DateTime sd, ed;
            int iAccomodationId;
            DateTime.TryParse(txtStartDate.Text, out sd);
            DateTime.TryParse(txtEndDate.Text, out ed);
            int.TryParse(ddlAccomName.SelectedValue.ToString(), out iAccomodationId);
            RemoveRoomObjectFromSession();
            PrepareRoomChart(sd, ed, iAccomodationId);
        }
        else if (ddlAccomName.SelectedValue != "7")
        {
            gdvRatesCruise.DataSource = null;
            gdvRatesHotel.DataSource = null;
            gdvRatesCruise.DataBind();
            gdvRatesHotel.DataBind();
            checkwaitlisted();
            DateTime sd, ed;
            int iAccomodationId;
            DateTime.TryParse(txtStartDate.Text, out sd);
            DateTime.TryParse(txtEndDate.Text, out ed);
            int.TryParse(ddlAccomName.SelectedValue.ToString(), out iAccomodationId);
            RemoveRoomObjectFromSession();
            PrepareRoomChart(sd, ed, iAccomodationId);

            DateTime StartDate = DateTime.MinValue;
            StartDate = Convert.ToDateTime(txtStartDate.Text);
            DateTime EndDate = DateTime.MinValue;
            EndDate = Convert.ToDateTime(txtEndDate.Text);
            String Diff = (EndDate - StartDate).TotalDays.ToString();
            txtNoOfNights.Text = Diff;


        }
    }

    protected void btnBookTour_Click(object sender, EventArgs e)
    {

        //if (GetRoomObjectFromSession() == null)
        //{
        //    string msg = "Please click on 'Get Available Rooms' to get the current room status.";
        //    lblErrorMsg.Text = msg;
        //    base.DisplayAlert(msg);
        //    return;
        //}

        //dont need this
        SaveBooking();


    }
    protected void btnReset_Click(object sender, EventArgs e)
    {
        SessionServices.Booking_BookingId = -1;
        base.DisplayAlert("Cleared every control");
        btnReset.Focus();
        ClearControls();
    }
    protected void btnConfirmBooking_Click(object sender, EventArgs e)
    {
        ClearSessionVariables();
        base.DisplayAlert("Redirecting to confirm the booking.");
        Response.Redirect("BookingConfirmation.aspx?bid=" + iBookingId);
    }
    protected void btnDeleteBooking_Click(object sender, EventArgs e)
    {
        DeleteBooking();
        ClearSessionVariables();
        base.DisplayAlert("Booking deleted, redirecting to after booking actions.");
        Response.Redirect("afterBookingactions.aspx?bid=" + iBookingId + "&bstatus=deleted");
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        CancelBooking();
        ClearSessionVariables();
        base.DisplayAlert("Booking cancelled, redirecting to after booking actions.");
        Response.Redirect("afterBookingactions.aspx?bid=" + iBookingId + "&bstatus=cancelled");

    }
    protected void ddlAccomType_SelectedIndexChanged(object sender, EventArgs e)
    {

        FillAccomodations(Convert.ToInt32(ddlAccomType.SelectedValue));
    }
    #endregion Event handlers

    #region Helper Methods
    private void AddAttributesToControls()
    {
        btnGetAvailableRooms.Attributes.Add("onclick", "return validateBeforeGettingRooms();");
        btnBookTour.Attributes.Add("onclick", "return validateBeforeBookingRooms()");
        btnCancel.Attributes.Add("onclick", "return confirm('Are you sure you want to cancel this booking?')");
        btnDeleteBooking.Attributes.Add("onclick", "return confirm('Are you sure you want to delete this booking?')");

        /*if (iBookingId == 0)
            btnGetAvailableRooms.Attributes.Add("onclick", "return AddFunctionsToOnClickEventGetAvailableRooms()");*/
        btnConfirmBooking.Attributes.Add("onclick", "return doPostBackConfirmTour()");
        //txtStartDate.Attributes.Add("onchange", "return fillEndDate()");
        txtStartDate.Attributes.Add("onchange", "return fillEndDate('" + txtStartDate.ClientID + "','" + txtEndDate.ClientID + "');");

        //txtStartDate.Attributes.Add("onchange", "return validateBookingDates()");
        //txtEndDate.Attributes.Add("onchange", "return validateBookingDates()");
        //txtStartDate.Attributes.Add("onkeydown", "return disablebackspace()");
        //txtEndDate.Attributes.Add("onkeydown", "return disablebackspace()");

        txtNoOfPersons.Attributes.Add("onkeydown", "return disableInput()");
        txtNoOfNights.Attributes.Add("onkeydown", "return disableInput()");
        txtBookingStatus.Attributes.Add("onkeydown", "return disableInput()");
    }
    private void getlocalagent()
    {
        DataTable dt = dlAgentpayment.getlocalagent();
        if (dt != null && dt.Rows.Count > 0)
        {
            ddlAgentType.Items.Clear();
            ddlAgentType.DataSource = dt;

            ddlAgentType.DataTextField = "AgentName";
            ddlAgentType.DataValueField = "AgentId";
            ddlAgentType.DataBind();
            ddlAgentType.Items.Insert(0, "Select Main Agent");

        }
    }
    private void SetAccomodationTypeDetails()
    {
        //Filling the Object which contains the Accomodation type and also all the respective accomodations
        //This is saved in the session and then the Drop downs are filled.
        AccomTypeDTO[] oAccomTypeData;
        AccomodationTypeMaster objATM;
        objATM = new AccomodationTypeMaster();
        oAccomTypeData = objATM.GetAccomTypeWithAccomDetails(0);
        SessionServices.Booking_AccomodationData = oAccomTypeData;
        objATM = null;
    }

    private void SendAccomodationSeasonDetailtoJS()
    {
        List<AccomodationSeasonDatesDTO> accomodationSeasonDateList = new List<AccomodationSeasonDatesDTO>();
        AccomodationSeasonDatesDTO accomodationSeasonDate = new AccomodationSeasonDatesDTO();
        DateTime startdate, enddate;
        AccomTypeDTO[] oAccomTypeData = SessionServices.Booking_AccomodationData;
        if (oAccomTypeData == null)
        {
            SetAccomodationTypeDetails();
            oAccomTypeData = SessionServices.Booking_AccomodationData;
        }

        if (oAccomTypeData == null)
            return;

        for (int i = 0; i < oAccomTypeData.Length; i++)
        {
            if (oAccomTypeData[i].Accomodations != null)
            {
                for (int j = 0; j < oAccomTypeData[i].Accomodations.Length; j++)
                {
                    foreach (AccomodationSeasonDTO accomodationSeasonDto in oAccomTypeData[i].Accomodations[j].AccomodationSeasonList)
                    {
                        DateTime.TryParse(accomodationSeasonDto.SeasonStartDate.ToString(), out startdate);
                        DateTime.TryParse(accomodationSeasonDto.SeasonEndDate.ToString(), out enddate);

                        if (startdate != DateTime.MinValue || enddate != DateTime.MinValue)
                        {
                            if (GF.Handle19000101(startdate, false) != "" && GF.Handle19000101(enddate, false) != "")
                            {
                                accomodationSeasonDate = new AccomodationSeasonDatesDTO();
                                accomodationSeasonDate.AccomodationId = accomodationSeasonDto.AccomodationId.ToString();
                                accomodationSeasonDate.SeasonStartDate = GF.GetYYYYMMDD(startdate);
                                accomodationSeasonDate.SeasonEndDate = GF.GetYYYYMMDD(enddate);
                                accomodationSeasonDateList.Add(accomodationSeasonDate);
                            }
                        }
                    }
                }
            }
        }

        ClientScriptManager cs = Page.ClientScript;
        string JSONObjectName;
        JSONObjectName = base.ConvertObjetToJSON(accomodationSeasonDateList.ToArray());
        JSONObjectName = JSONObjectName.Replace(@"\", @"\\");

        if (!cs.IsStartupScriptRegistered("AccomodationDetails"))
            cs.RegisterStartupScript(GetType(), "AccomodationDetails", "setAccomodationDetails" + "('" + JSONObjectName + "');", true);
    }

    private void FillAgents()
    {
        try
        {
            AgentMaster oAgentMaster = new AgentMaster();
            AgentDTO[] oAgentData = oAgentMaster.GetData();
            ListItemCollection li = new ListItemCollection();
            ListItem l = new ListItem("Select Ref Agent", "0");
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

    private void FillAccomodationTypes()
    {

        ddlAccomName.Items.Clear();
        SortedList slAccomTypes = new SortedList();
        slAccomTypes.Add("0", "Choose");

        AccomTypeDTO[] oAccomTypeData = SessionServices.Booking_AccomodationData;
        if (oAccomTypeData == null)
        {
            SetAccomodationTypeDetails();
            oAccomTypeData = SessionServices.Booking_AccomodationData;
        }

        if (oAccomTypeData != null)
        {
            for (int i = 0; i < oAccomTypeData.Length; i++)
            {
                slAccomTypes.Add(Convert.ToString(oAccomTypeData[i].AccomodationTypeId), Convert.ToString(oAccomTypeData[i].AccomodationType));
            }
        }
        ddlAccomType.DataSource = slAccomTypes;
        ddlAccomType.DataTextField = "value";
        ddlAccomType.DataValueField = "key";
        ddlAccomType.DataBind();
    }

    private void FillAccomodations(int AccomodationTypeId)
    {
        oAccomTypeData = SessionServices.Booking_AccomodationData;
        ddlAccomName.Items.Clear();
        SortedList slAccomData = new SortedList();
        slAccomData.Add("0", "Choose");
        if (oAccomTypeData != null)
        {
            for (int i = 0; i < oAccomTypeData.Length; i++)
            {
                if (oAccomTypeData[i].AccomodationTypeId.CompareTo(AccomodationTypeId) == 0)
                {
                    if (oAccomTypeData[i].Accomodations != null)
                    {
                        for (int j = 0; j < oAccomTypeData[i].Accomodations.Length; j++)
                        {
                            if (!slAccomData.ContainsKey(Convert.ToString(oAccomTypeData[i].Accomodations[j].AccomodationId)))
                            {
                                slAccomData.Add(Convert.ToString(oAccomTypeData[i].Accomodations[j].AccomodationId), Convert.ToString(oAccomTypeData[i].Accomodations[j].AccomodationName));
                            }
                        }
                    }
                }
            }
            ddlAccomName.DataSource = slAccomData;
            ddlAccomName.DataTextField = "value";
            ddlAccomName.DataValueField = "key";
            ddlAccomName.DataBind();
        }
    }

    private void MySetFocus(DropDownList ddl)
    {

        ClientScriptManager cs = Page.ClientScript;
        string script = "document.getElementById('" + ddl.ClientID + "').focus();";
        if (!cs.IsStartupScriptRegistered("setfocus"))
            cs.RegisterStartupScript(ddl.GetType(), "setfocus", script, true);
    }

    private void FillDetailsinFields(int BookingId)
    {

        BookingServices oBookingManager = new BookingServices();
        BookingDTO oBookingData = null;
        DateTime dt;
        ListItem li = null;
        ListItem liref = null;
        if (BookingId != 0)
            oBookingData = oBookingManager.GetBookingDetails(BookingId);
        hdnchartered.Value = oBookingData.Chartered != null ? oBookingData.Chartered.ToString() : "False";
        if (oBookingData != null)
        {

            if (oBookingData.Chartered == true)
            {
                chkChartered.Checked = true;
            }
            else
            {
                chkChartered.Checked = false;
            }

            txtBookingRef.Text = oBookingData.BookingReference.ToString();
            dt = oBookingData.StartDate;
            txtStartDate.Text = GF.GetDD_MMM_YYYY(dt, false);
            dt = oBookingData.EndDate;
            txtEndDate.Text = GF.GetDD_MMM_YYYY(dt, false);
            txtNoOfNights.Text = oBookingData.NoOfNights.ToString();
            ddlAccomType.SelectedValue = oBookingData.AccomodationTypeId.ToString();
            FillAccomodations(Convert.ToInt32(ddlAccomType.SelectedValue));
            ddlAccomName.SelectedValue = oBookingData.AccomodationId.ToString();
            txtNoOfPersons.Text = oBookingData.NoOfPersons.ToString();
            Session["getpackegforedit"] = oBookingData.packagid.ToString();
            loadpackage();
            ddlpackage.SelectedValue = oBookingData.packagid.ToString();
            {
                rdProposedBookingYes.Checked = oBookingData.ProposedBooking;
            }


            rdProposedBookingNo.Checked = !oBookingData.ProposedBooking;



            SessionServices.Booking_TotalNights = oBookingData.NoOfNights;

            if (oBookingData.BookingStatusId == 1)
            {
                txtBookingStatus.Text = "Booked";
                txtBookingStatus.ForeColor = System.Drawing.Color.Blue;
            }
            else if (oBookingData.BookingStatusId == 2)
            {
                txtBookingStatus.Text = "Confirmed";
                txtBookingStatus.ForeColor = System.Drawing.Color.Green;
                rdProposedBookingNo.Enabled = false;
                rdProposedBookingYes.Enabled = false;
            }
            else if (oBookingData.BookingStatusId == 3)
            {
                txtBookingStatus.Text = "WaitListed";
                txtBookingStatus.ForeColor = System.Drawing.Color.Orange;
            }

            li = ddlAgentType.Items.FindByValue(oBookingData.AgentId.ToString());

            liref = ddlAgent.Items.FindByValue(oBookingData.AgentIdRef.ToString());

            if (li != null)
                ddlAgentType.SelectedIndex = ddlAgentType.Items.IndexOf(li);
            if (liref != null)
                ddlAgent.SelectedIndex = ddlAgent.Items.IndexOf(liref);
            btnBookTour.Text = "Update Tour";
        }
        else
        {
            ClearControls();
            btnBookTour.Text = "Book Tour";
        }
        oBookingManager = null;
        oBookingData = null;
    }

    private bool isChildObjectExists()
    {
        if (SessionServices.Booking_AllRoomsDataPAX != null)
            return true;
        else
            return false;
    }

    private void overRideRoomObject()
    {
        Object Child = SessionServices.Booking_AllRoomsDataPAX;
        if (Child != null)
        {
            SessionServices.Booking_AllRoomsData = (BookedRooms[])Child;
            SessionServices.DeleteSession(Constants._Booking_AllRoomsDataPAX);
            SessionServices.DeleteSession(Constants._BookingChangeRoomPax_DdlSelectedIndexes);
        }
    }

    private void ClearControls()
    {
        txtStartDate.Text = string.Empty;
        txtEndDate.Text = string.Empty;
        txtNoOfNights.Text = string.Empty;
        if (ddlAccomType.Items.Count > 0)
            ddlAccomType.SelectedIndex = 0;
        if (ddlAccomName.Items.Count > 0)
            ddlAccomName.SelectedIndex = 0;
        txtNoOfPersons.Text = string.Empty;
        ddlAgent.SelectedIndex = 0;
        rdProposedBookingYes.Checked = false;
        //rdProposedBookingNo.Checked = true;
    }

    private void PrepareRoomChart()
    {
        PrepareRoomChart(DateTime.MinValue, DateTime.MinValue, 0, false);
    }

    private void PrepareRoomChartpgload()
    {
        PrepareRoomChartpgload(DateTime.MinValue, DateTime.MinValue, 0, false);
    }

    private void PrepareRoomChart(DateTime dtStartDate, DateTime EndDate, int iAccomID)
    {
        PrepareRoomChart(dtStartDate, EndDate, iAccomID, true);
    }

    private void PrepareRoomChartpgload(DateTime dtStartDate, DateTime EndDate, int iAccomID)
    {
        PrepareRoomChartpgload(dtStartDate, EndDate, iAccomID, true);
    }

    private void PrepareRoomChartpgload(DateTime dtStartDate, DateTime dtEndDate, int iAccomID, bool PrepareFromDB)
    {
        BookedRooms[] oBookedRooms;
        pnlShowAvailableRoomNos.Controls.Clear();
        if (isChildObjectExists() == true)
        {
            PrepareFromDB = false;
            overRideRoomObject();
        }

        if (PrepareFromDB == true)
            oBookedRooms = GetAllRoomspgload(dtStartDate, dtEndDate, iAccomID);

        else
            oBookedRooms = GetAllRooms();

        if (oBookedRooms != null)
        {


            //for (int k = 0; k < oBookedRooms.Length; k++)
            //{

            //    if (oBookedRooms[k] != null)
            //        if (oBookedRooms[k].RoomStatus == Constants.WAITLISTED)
            //        {
            //            ViewState["atleastonewaitlisted"] = true;
            //            break;
            //        }
            //    ViewState["atleastonewaitlisted"] = false;

            //}
            GetRoomOtherBookings(dtStartDate, dtEndDate, iBookingId, 0, iAccomID, "");

            tblMaster = new Table();
            tblMaster = PrepareChart(oBookedRooms);
            if (tblMaster != null)
                pnlShowAvailableRoomNos.Controls.Add(tblMaster);
        }

    }

    private void checkwaitlisted()
    {
        try
        {
            DateTime sd;
            DateTime ed;
            int iAccomId = 0;
            DateTime.TryParse(txtStartDate.Text, out sd);
            DateTime.TryParse(txtEndDate.Text, out ed);
            if (ddlAccomName.SelectedItem != null)
                int.TryParse(ddlAccomName.SelectedItem.Value, out iAccomId);
            BookedRooms[] oBookedRooms;
            oBookedRooms = GetAllRooms(sd, ed, iAccomId);
            for (int k = 0; k < oBookedRooms.Length; k++)
            {
                if (oBookedRooms[k].RoomStatus == Constants.WAITLISTED || oBookedRooms[k].RoomStatus == Constants.BOOKED)
                {
                    ViewState["atleastonewaitlisted"] = true;
                    break;
                }
                ViewState["atleastonewaitlisted"] = false;

            }
        }
        catch { }
    }

    private void PrepareRoomChart(DateTime dtStartDate, DateTime dtEndDate, int iAccomID, bool PrepareFromDB)
    {
        BookedRooms[] oBookedRooms;
        pnlShowAvailableRoomNos.Controls.Clear();
        if (isChildObjectExists() == true)
        {
            PrepareFromDB = false;
            overRideRoomObject();
        }

        if (PrepareFromDB == true)
            oBookedRooms = GetAllRooms(dtStartDate, dtEndDate, iAccomID);

        else
            oBookedRooms = GetAllRooms();

        if (oBookedRooms != null)
        {

            //for (int k = 0; k < oBookedRooms.Length; k++)
            //{
            //    if (oBookedRooms[k].RoomStatus == Constants.WAITLISTED || oBookedRooms[k].RoomStatus == Constants.BOOKED)
            //    {
            //        ViewState["atleastonewaitlisted"] = true;
            //        break;
            //    }
            //    ViewState["atleastonewaitlisted"] = false;

            //}
            GetRoomOtherBookings(dtStartDate, dtEndDate, iBookingId, 0, iAccomID, "");

            tblMaster = new Table();
            tblMaster = PrepareChart(oBookedRooms);
            if (tblMaster != null)
                pnlShowAvailableRoomNos.Controls.Add(tblMaster);
        }

    }

    private Table PrepareChart(BookedRooms[] oAllRooms)
    {
        int iRoomsAvailable = 0, iRoomsBookedWithThisId = 0, iRoomsWaitListed = 0, iRBMain = 0, iRAMain = 0, iRWLMain = 0;
        int iPrevRBMain = 0, iPrevRAMain = 0, iPrevRWLMain = 0;
        int iRoomCounter = 0;
        string sPrevRoomCategory = "", sPrevRoomType = "", sPrevRoomNo = "";
        SortedList slRoomNo = new SortedList();

        //SortedList slRooms = new SortedList();
        const int TOTAL_CELLS_ALLOWED_PER_ROW = 20;
        if (oAllRooms == null)
        {
            lblErrorMsg.Text = "oAllrooms is null";
            base.DisplayAlert("Click on GetAvailable Rooms to get rooms.");
            btnGetAvailableRooms.Focus();
            return null;
        }
        Table tblRoomsMain = new Table();
        TableRow trRoomsmain = new TableRow();
        TableRow trRooms = null;
        TableCell tcRoomsMain = new TableCell();
        TableCell tcRoomsDDL = new TableCell();
        Panel pnlRooms = null;
        TableCell tcPrevCheckBoxCell = new TableCell();
        TableCell tcPrevRoomNoCell = new TableCell();
        Table tRooms = null;
        int iTotalPax = 0;
        int iRPax = 0, iRTotalRoomsPerType = 0;
        txtNoOfPersons.Text = string.Empty;

        tblRoomsMain.Style[HtmlTextWriterStyle.FontSize] = "x-small";
        tblRoomsMain.Style[HtmlTextWriterStyle.BorderWidth] = "1";
        //tblRoomsMain.BorderWidth = 2;
        //tblRoomsMain.Width = 900;

        for (int i = 0; i < oAllRooms.Length; i++)
        {
            if (oAllRooms[i] != null)
            {
                #region Adding Category
                if (oAllRooms[i].RoomCategory != sPrevRoomCategory)
                {
                    if (sPrevRoomCategory != "")
                    {
                        if (trRooms != null)
                        {
                            tRooms.Rows.Add(trRooms);
                            tRooms.Rows.Add(AddChangeRoomPaxURL(sPrevRoomCategory, sPrevRoomType, iBookingId));
                            pnlRooms = AddRoomsToPanel(tRooms, sPrevRoomCategory, sPrevRoomType);
                            trRoomsmain = new TableRow();
                            tcRoomsMain = new TableCell();
                            tcRoomsMain.Controls.Add(pnlRooms);
                            trRoomsmain.Cells.Add(tcRoomsMain);
                            tblRoomsMain.Rows.Add(trRoomsmain);
                        }
                    }
                    sPrevRoomCategory = oAllRooms[i].RoomCategory;
                    trRoomsmain = new TableRow();
                    tcRoomsMain = new TableCell();
                    tcRoomsMain.Controls.Add(AddCategory(sPrevRoomCategory));
                    trRoomsmain.Cells.Add(tcRoomsMain);
                    tblRoomsMain.Rows.Add(trRoomsmain);
                    sPrevRoomType = "";
                }
                #endregion Adding Category

                #region Adding RoomType Etc.
                if (oAllRooms[i].RoomType != sPrevRoomType)
                {
                    if (sPrevRoomType != "")
                    {
                        if (trRooms != null)
                        {
                            tRooms.Rows.Add(trRooms);
                            tRooms.Rows.Add(AddChangeRoomPaxURL(sPrevRoomCategory, sPrevRoomType, iBookingId));
                            pnlRooms = AddRoomsToPanel(tRooms, sPrevRoomCategory, sPrevRoomType);

                            /*SetRoomsPaxLabel(tblRoomsMain, iRPax, sPrevRoomCategory, sPrevRoomType);                            
                            SetTotalRooms(tblRoomsMain, iRBMain, iRAMain, iRWLMain, sPrevRoomCategory, sPrevRoomType, iRTotalRoomsPerType);
                            SetTotalRoomsBookedLabel(tblRoomsMain, iRBMain, sPrevRoomCategory, sPrevRoomType);*/

                            /*******************************
                            //Following is done because if the rooms booked are in waitlist then
                            //Populate the Room Booked label with the no. of waitlisted rooms.
                            if (iRBMain == 0 && iRWLMain > 0)
                                SetRoomsRequestedLabel(tblRoomsMain, iRWLMain, sPrevRoomCategory, sPrevRoomType);
                            else
                                SetRoomsRequestedLabel(tblRoomsMain, iRBMain, sPrevRoomCategory, sPrevRoomType);
                            /******************************/
                            /*SetRoomsWaitlistedLabel(tblRoomsMain, iRWLMain, sPrevRoomCategory, sPrevRoomType);
                            SetRoomsAvailableLabel(tblRoomsMain, iRAMain, sPrevRoomCategory, sPrevRoomType);
                            iRPax = 0;
                            iRAMain = 0;
                            iRBMain = 0;
                            iRWLMain = 0;
                            iRTotalRoomsPerType = 0;*/

                            trRoomsmain = new TableRow();
                            tcRoomsMain = new TableCell();
                            tcRoomsMain.Controls.Add(pnlRooms);
                            trRoomsmain.Cells.Add(tcRoomsMain);
                            tblRoomsMain.Rows.Add(trRoomsmain);
                        }
                    }

                    sPrevRoomType = oAllRooms[i].RoomType;
                    trRoomsmain = new TableRow();
                    tcRoomsMain = new TableCell();
                    tcRoomsMain.ColumnSpan = 20;

                    tcRoomsMain.Controls.Add(AddRoomTypePanel(sPrevRoomCategory, sPrevRoomType));
                    trRoomsmain.Cells.Add(tcRoomsMain);
                    tblRoomsMain.Rows.Add(trRoomsmain);
                    tRooms = new Table();
                    tRooms.Style[HtmlTextWriterStyle.FontFamily] = "verdana";
                    tRooms.Style[HtmlTextWriterStyle.FontSize] = "x-small";
                    trRooms = new TableRow();
                }
                #endregion Adding RoomType Etc.

                #region Adding Rooms
                if (iRoomCounter >= TOTAL_CELLS_ALLOWED_PER_ROW)
                {
                    if (trRooms != null)
                        tRooms.Rows.Add(trRooms);
                    trRooms = new TableRow();
                    iRoomCounter = 0;
                }
                bool bEntered = false;
                if (slRoomNo.Contains(oAllRooms[i].RoomNo))
                {
                    if (oAllRooms[i].BookingId == iBookingId)
                    {
                        slRoomNo.Remove(oAllRooms[i].RoomNo);
                        trRooms.Cells.Remove(tcPrevCheckBoxCell);
                        trRooms.Cells.Remove(tcPrevRoomNoCell);
                        if (iBookingId != 0)
                        {
                            iRBMain = iRBMain - iPrevRBMain;
                            iRAMain = iRAMain - iPrevRAMain;
                            iRWLMain = iRWLMain - iPrevRWLMain;
                        }
                        //iRTotalRoomsPerType = iRTotalRoomsPerType - iPrevRTotalRoomsPerType;
                        bEntered = true;
                    }
                }
                if (slRoomNo.Contains(oAllRooms[i].RoomNo) == false)
                {
                    if (bEntered == true)
                    {
                        trRooms.Cells.Remove(tcPrevCheckBoxCell);
                        trRooms.Cells.Remove(tcPrevRoomNoCell);
                        bEntered = false;
                    }

                    slRoomNo.Add(oAllRooms[i].RoomNo, oAllRooms[i].BookingId);


                    tcPrevCheckBoxCell = AddRoomCheckBox(oAllRooms[i]);


                    trRooms.Cells.Add(tcPrevCheckBoxCell);

                    tcPrevRoomNoCell = AddRoom(oAllRooms[i], iBookingId, out iRoomsBookedWithThisId, out iRoomsAvailable, out iRoomsWaitListed);
                    trRooms.Cells.Add(tcPrevRoomNoCell);

                    iRBMain = iRBMain + iRoomsBookedWithThisId;
                    iPrevRBMain = iRoomsBookedWithThisId;

                    iRAMain = iRAMain + iRoomsAvailable;
                    iPrevRAMain = iRoomsAvailable;

                    iRWLMain = iRWLMain + iRoomsWaitListed;
                    iPrevRWLMain = iRoomsWaitListed;

                    //if (iRoomCounter == 0)
                    //{
                    //    iRTotalRoomsPerType = iRTotalRoomsPerType + iRoomCounter;
                    //}
                    //else
                    //{

                    //}

                    if (sPrevRoomNo != oAllRooms[i].RoomNo)
                    {
                        iRoomCounter++;
                        iRTotalRoomsPerType = iRTotalRoomsPerType + 1;
                    }
                    sPrevRoomNo = oAllRooms[i].RoomNo;
                }
                #endregion Adding Rooms

                #region Calculating Total No. Of Pax
                if (oAllRooms[i].BookingId == iBookingId)
                {
                    if (oAllRooms[i].RoomStatus == Constants.BOOKED)
                    {
                        if (oAllRooms[i].PaxStaying > 0)
                        {
                            iTotalPax = iTotalPax + oAllRooms[i].PaxStaying;
                            iRPax = iRPax + oAllRooms[i].PaxStaying;
                        }
                        else
                        {
                            iTotalPax = iTotalPax + oAllRooms[i].NoOfBeds;
                            iRPax = iRPax + oAllRooms[i].NoOfBeds;
                            oAllRooms[i].PaxStaying = iRPax;
                        }
                    }
                }
                #endregion
            }
        }
        #region For Last Row
        if (trRooms != null)
        {
            tRooms.Rows.Add(trRooms);
            tRooms.Rows.Add(AddChangeRoomPaxURL(sPrevRoomCategory, sPrevRoomType, iBookingId));
            pnlRooms = AddRoomsToPanel(tRooms, sPrevRoomCategory, sPrevRoomType);
            trRoomsmain = new TableRow();
            tcRoomsMain = new TableCell();
            tcRoomsMain.Controls.Add(pnlRooms);
            trRoomsmain.Cells.Add(tcRoomsMain);
            tblRoomsMain.Rows.Add(trRoomsmain);
        }
        #endregion For Last Row

        //If not initiate by the room drop down then call the setRoomStatus.
        if (!GetPostBackControlID().StartsWith(Constants.DROPDOWNLIST_ROOMS))
        {
            SetRoomStatus(tblRoomsMain);
            SetRoomDropDownIndex();
        }
        return tblRoomsMain;
    }

    private void SetRoomDropDownIndex()
    {
        string JSONObjectName = "";
        SortedList sl = null;
        string[,] ddindexes = null;
        BookingServices oBookingManager = new BookingServices();
        try
        {
            sl = (SortedList)SessionServices.Booking_DdlSelectedIndexes;
            if (sl != null && sl.Count > 0)
            {
                ddindexes = new string[sl.Count, 2];
                for (int i = 0; i < sl.Count; i++)
                {
                    ddindexes[i, 0] = sl.GetByIndex(i).ToString();
                    ddindexes[i, 1] = sl.GetKey(i).ToString();
                }
                ClientScriptManager cs = Page.ClientScript;
                JSONObjectName = base.ConvertObjetToJSON(ddindexes);
                JSONObjectName = JSONObjectName.Replace(@"\", @"\\");
                if (!cs.IsStartupScriptRegistered("RoomDropDownSelectedIndexes"))
                    cs.RegisterStartupScript(sl.GetType(), "RoomDropDownSelectedIndexes", "setRoomDropDownSelectedIndexes" + "('" + JSONObjectName + "');", true);
                //cs.RegisterClientScriptBlock(sl.GetType(), "RoomDropDownSelectedIndexes", "setRoomDropDownSelectedIndexes" + "('" + JSONObjectName + "');", true);              
                //JSONObjectName = SendObjectToJS(oRoomBookingDataDateWise, "setRoomOtherBookings", "RoomOtherBookings", true);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
        SessionServices.Booking_DdlSelectedIndexes = null;
        oBookingManager = null;
    }

    private void SetBookingTotalPax(int iTotalPax)
    {
        txtNoOfPersons.Text = string.Empty;
        txtNoOfPersons.Text = iTotalPax.ToString();
    }
    private BookedRooms[] GetAllRooms()
    {
        //This will Return the Rooms from the Session
        return GetRoomObjectFromSession();
    }
    private BookedRooms[] GetAllRooms(DateTime dtStartDate, DateTime dtEndDate, int iAccomID)
    {
        //This will Return the Rooms from the Database
        BookedRooms[] oTotalRooms = null;
        BookingServices oBookingManager = null;

        if (dtStartDate == DateTime.MinValue || iAccomID <= 0 || dtEndDate == DateTime.MinValue)
        {
            RemoveRoomObjectFromSession();
            //SessionHandler"AllRooms"] = null;
            return null;
        }
        else
        {
            oBookingManager = new BookingServices();
            oTotalRooms = oBookingManager.GetAllRooms(dtStartDate, dtEndDate, iAccomID, iBookingId);
            //SessionHandler"AllRooms"] = oTotalRooms;
            SetRoomObjectToSession(oTotalRooms);
        }
        return oTotalRooms;
    }

    private BookedRooms[] GetAllRoomspgload(DateTime dtStartDate, DateTime dtEndDate, int iAccomID)
    {
        //This will Return the Rooms from the Database
        BookedRooms[] oTotalRooms = null;
        BookingServices oBookingManager = null;

        if (dtStartDate == DateTime.MinValue || iAccomID <= 0 || dtEndDate == DateTime.MinValue)
        {
            RemoveRoomObjectFromSession();
            //SessionHandler"AllRooms"] = null;
            return null;
        }
        else
        {
            oBookingManager = new BookingServices();
            oTotalRooms = oBookingManager.GetAllRoomspgload(dtStartDate, dtEndDate, iAccomID, iBookingId);
            //SessionHandler"AllRooms"] = oTotalRooms;
            SetRoomObjectToSession(oTotalRooms);
        }
        return oTotalRooms;
    }

    private void GetRoomOtherBookings(DateTime StartDate, DateTime EndDate, int CurrentBookingId, int AccomodationTypeId, int AccomodationId, string RoomNo)
    {

        BookingChartServices bookingChartServices;
        RoomBookingDateWiseDTO[] oRoomBookingDateWiseDTO = null;
        BookingChartDTO[] oRoomMaintenance = null;
        BookingServices oBookingManager = new BookingServices();
        bookingChartServices = new BookingChartServices();
        string JSONObjectName = "";
        string jsonobjmt = "";

        if (DateTime.Compare(StartDate, DateTime.MinValue) == 0)
            DateTime.TryParse(txtStartDate.Text, out StartDate);
        if (DateTime.Compare(EndDate, DateTime.MinValue) == 0)
            DateTime.TryParse(txtEndDate.Text, out EndDate);

        oRoomBookingDateWiseDTO = SessionServices.Booking_RoomBookingDateWiseDTO;
        if (oRoomBookingDateWiseDTO == null)
            oRoomBookingDateWiseDTO = oBookingManager.GetRoomOtherBookings(StartDate, EndDate, CurrentBookingId, AccomodationTypeId, AccomodationId, RoomNo);
        oRoomMaintenance = bookingChartServices.GetRoomDetmaintenance(0, 0, AccomodationId, StartDate, EndDate);
        SessionServices.Booking_RoomBookingDateWiseDTO = oRoomBookingDateWiseDTO;

        if (oRoomBookingDateWiseDTO != null && oRoomBookingDateWiseDTO.Length > 0)
        {
            ClientScriptManager cs = Page.ClientScript;
            JSONObjectName = base.ConvertObjetToJSON(oRoomBookingDateWiseDTO);
            JSONObjectName = JSONObjectName.Replace(@"\", @"\\");
            if (!cs.IsStartupScriptRegistered("RoomOtherBookings"))
                cs.RegisterStartupScript(GetType(), "RoomOtherBookings", "setRoomOtherBookings" + "('" + JSONObjectName + "');", true);

            //JSONObjectName = SendObjectToJS(oRoomBookingDataDateWise, "setRoomOtherBookings", "RoomOtherBookings", true);
        }

        if (oRoomMaintenance != null && oRoomMaintenance.Length > 0)
        {
            ClientScriptManager cs = Page.ClientScript;
            jsonobjmt = base.ConvertObjetToJSON(oRoomMaintenance);
            jsonobjmt = jsonobjmt.Replace(@"\", @"\\");
            if (!cs.IsStartupScriptRegistered("Roommaintenance"))
                cs.RegisterStartupScript(GetType(), "Roommaintenance", "setRoommaintenance" + "('" + jsonobjmt + "');", true);
        }
    }

    private void SetButtonsState()
    {
        #region Booked
        if (string.Compare(txtBookingStatus.Text, "BOOKED", true) == 0)
        {
            btnBookTour.Visible = true;
            btnBookTour.Enabled = true;

            btnConfirmBooking.Visible = true;
            btnConfirmBooking.Enabled = true;

            btnCancel.Visible = true;
            btnCancel.Enabled = true;

            btnReset.Visible = true;
            btnReset.Enabled = false;
        }
        #endregion
        #region Confirmed
        if (string.Compare(txtBookingStatus.Text, "CONFIRMED", true) == 0)
        {
            btnBookTour.Visible = true;
            btnBookTour.Enabled = false;

            btnConfirmBooking.Text = "Edit Confirmation";
            btnConfirmBooking.Visible = true;
            btnConfirmBooking.Enabled = true;

            btnCancel.Visible = true;
            btnCancel.Enabled = false;

            btnReset.Visible = true;
            btnReset.Enabled = false;
        }
        #endregion
        #region Waitlisted
        else if (string.Compare(txtBookingStatus.Text, "WAITLISTED", true) == 0)
        {
            btnBookTour.Visible = true;
            btnBookTour.Enabled = true;

            btnConfirmBooking.Text = "Confirm";
            btnConfirmBooking.Visible = false;
            btnConfirmBooking.Enabled = false;

            btnCancel.Visible = true;
            btnCancel.Enabled = true;

            btnReset.Visible = true;
            btnReset.Enabled = false;
        }
        #endregion
        #region Cancelled
        else if (string.Compare(txtBookingStatus.Text, "CANCELLED", true) == 0)
        {
            btnBookTour.Visible = true;
            btnBookTour.Enabled = false;

            btnConfirmBooking.Text = "Confirm";
            btnConfirmBooking.Visible = true;
            btnConfirmBooking.Enabled = false;

            btnCancel.Visible = true;
            btnCancel.Enabled = false;

            btnReset.Visible = true;
            btnReset.Enabled = false;
        }
        #endregion
        if (rdProposedBookingYes.Checked)
        {
            SessionServices.Booking_Propsed = "Proposed Booking";
            btnBookTour.Visible = true;
            btnBookTour.Enabled = true;

            btnConfirmBooking.Visible = true;
            btnConfirmBooking.Enabled = false;

            btnCancel.Visible = true;
            btnCancel.Enabled = true;

            btnReset.Visible = true;
            btnReset.Enabled = false;
        }
    }

    #region Room Object Session Management
    private void SetRoomObjectToSession(BookedRooms[] oAllRooms)
    {
        SessionServices.Booking_AllRoomsData = oAllRooms;
    }
    private BookedRooms[] GetRoomObjectFromSession()
    {
        return SessionServices.Booking_AllRoomsData;
    }
    private void RemoveRoomObjectFromSession()
    {
        SessionServices.DeleteSession(Constants._Booking_AllRoomsData);
        //SessionHandler.DeleteSession("AllRoomsData"); //Remove all the rooms from the session.
        SessionServices.DeleteSession(Constants._Booking_RoomBookingDateWiseData);
        //SessionHandler.DeleteSession("RoomBookingDateWiseData"); // Remove the booking data of the booking id from the session.
    }

    private void RemoveRoomPaxObjectFromSession()
    {

    }

    private void GetRoomPaxObjectFromSession()
    {
    }

    #endregion Room Object Session Management

    private TableRow AddChangeRoomPaxURL(string Category, string RoomType, int BookingId)
    {
        TableRow tr = new TableRow();
        TableCell tc = new TableCell();
        string sUrlChotu = "";
        string sLink = "";
        sUrlChotu = "BookingChangeRoomPax.aspx?bid=" + BookingId + "&roomCat=" + Category.Trim().Replace(" ", "~") + "&roomType=" + RoomType.Trim().Replace(" ", "~");
        sLink = "<a href='#' onclick=popUpRoomPaxChanger('" + sUrlChotu + "')> Change Rooms/Pax </a>";
        //sLink = "<a href='" + sUrlChotu + "'> Change Rooms/Pax </a>";        

        tc.Text = sLink;
        tc.ColumnSpan = 12;
        tr.Cells.Add(tc);
        return tr;
    }
    private static Panel AddRoomsToPanel(Table tRooms, string RoomCategory, string RoomType)
    {
        Panel p = new Panel();
        p.ID = Constants.PANEL_ROOMS + RoomCategory.Trim().Replace(" ", "").ToString() + "" + RoomType.Trim().Replace(" ", "").ToString();
        //p.Style[HtmlTextWriterStyle.Display] = "none";
        p.Style[HtmlTextWriterStyle.Display] = "block";
        p.Controls.Add(tRooms);
        return p;
    }

    private HtmlGenericControl AddCategory(string Category)
    {
        //Panel pCat = new Panel();
        //Table tCat = new Table();
        //TableRow trCat = new TableRow();
        //TableCell tcCat = new TableCell();

        HtmlGenericControl divCategory = new HtmlGenericControl("div");
        divCategory.ID = Category.Replace(" ", "").ToString();
        divCategory.Attributes.Add("class", "category");
        divCategory.InnerHtml = Category;
        //tcCat.Controls.Add(divCategory);
        ////tcCat.Text = Category;
        //tcCat.ColumnSpan = 18;
        //trCat.Cells.Add(tcCat);
        //tCat.Rows.Add(trCat);
        //pCat.Controls.Add(tCat);
        //return pCat;
        return divCategory;
    }

    private Panel AddRoomTypePanel(string Category, string RoomType)
    {
        Panel pCat = new Panel();
        Table tCat = new Table();
        TableRow trCat = new TableRow();

        //Adding Show Rooms Link Cell
        trCat.Cells.Add(AddShowRoomsButtonCell(Category, RoomType));

        //Adding Room Type Cell
        trCat.Cells.Add(AddRoomTypeCell(RoomType));

        //Adding Add Rooms Label Cell
        trCat.Cells.Add(AddAddRoomLabelCell(Category, RoomType));

        //Adding Add Room Drop down Cell
        trCat.Cells.Add(AddAddRoomDropDownCell(Category, RoomType));

        //Adding Room Booked Label Cell
        trCat.Cells.Add(AddRoomsRequestedCell(Category, RoomType));

        //Add Total Rooms Booked Cell
        trCat.Cells.Add(AdDatatalRoomBookedCell(Category, RoomType));

        //Add WaitListed Rooms Cell
        trCat.Cells.Add(AddWaitListedRoomsCell(Category, RoomType));

        //Adding Pax Label Cell
        trCat.Cells.Add(AddPaxLabelCell(Category, RoomType));


        //add total Price Label Cell
        trCat.Cells.Add(AddtotalPriceLabelCell(Category, RoomType));

        //add Rooms Available Label Cell
        trCat.Cells.Add(AddRoomsAvailableLabelCell(Category, RoomType));

        tCat.Rows.Add(trCat);
        pCat.Controls.Add(tCat);
        return pCat;
    }

    #region Room Header
    private static TableCell AddShowRoomsButtonCell(string Category, string RoomType)
    {
        string sPanelId = Constants.PANEL_ROOMS + Category.Trim().Replace(" ", "").ToString() + "" + RoomType.Trim();
        TableCell tc = new TableCell();
        tc.BorderWidth = 2;
        string LinkText = "showhide('" + sPanelId.ToString() + "')";
        HtmlImage showhideImage = new HtmlImage();
        showhideImage.Src = "~/images/icon_summary_minus_On.gif";
        showhideImage.ID = Constants.IMAGE + sPanelId;
        showhideImage.Attributes.Add("onclick", "showhide('" + sPanelId.ToString() + "')");
        tc.Controls.Add(showhideImage);
        return tc;
    }

    private static TableCell AddWaitListedRoomsCell(string Category, string RoomType)
    {
        TableCell tc = new TableCell();
        Label lbl = new Label();
        tc.Attributes.Add("class", "waitlistroomcell");
        tc.BorderWidth = 2;
        lbl.EnableViewState = false;
        lbl.ID = Constants.LABEL_ROOMS_WAITLISTED + Category.Trim().Replace(" ", "~") + "*" + RoomType.Trim().Replace(" ", "~");
        lbl.Text = "Wailisted Rooms: 0";
        tc.Controls.Add(lbl);
        return tc;
    }

    private static TableCell AdDatatalRoomBookedCell(string Category, string RoomType)
    {
        TableCell tc = new TableCell();
        Label lbl = new Label();
        tc.Attributes.Add("class", "totalroomcell");
        tc.BorderWidth = 2;
        tc.Visible = true;
        lbl.EnableViewState = false;
        lbl.ID = Constants.LABEL_TOTAL_ROOMS_BOOKED + Category.Trim().Replace(" ", "~") + "*" + RoomType.Trim().Replace(" ", "~");
        lbl.Text = "Total Rooms Booked: ";
        tc.Controls.Add(lbl);
        return tc;
    }

    private static TableCell AddRoomsAvailableLabelCell(string Category, string RoomType)
    {
        TableCell tc = new TableCell();
        Label lbl = new Label();
        tc.BorderWidth = 2;
        tc.Visible = false;
        lbl.EnableViewState = false;
        lbl.ID = Constants.LABEL_ROOMS_AVAILABLE + Category.Trim().Replace(" ", "~") + "*" + RoomType.Trim().Replace(" ", "~");
        lbl.Text = "Rooms Avaialble: ";
        tc.Controls.Add(lbl);
        return tc;
    }

    private TableCell AddAddRoomDropDownCell(string Category, string RoomType)
    {
        DropDownList ddlNoOfRoomsAvailable = new DropDownList();
        //HtmlSelect ddlNoOfRoomsAvailable = new HtmlSelect();
        TableCell tc = new TableCell();
        tc.BorderWidth = 1;
        ddlNoOfRoomsAvailable.EnableViewState = true;
        ddlNoOfRoomsAvailable.ID = Constants.DROPDOWNLIST_ROOMS + Category.Trim().Replace(" ", "~") + "*" + RoomType.Trim().Replace(" ", "~");
        ddlNoOfRoomsAvailable.Items.Clear();
        ddlNoOfRoomsAvailable.Items.Insert(0, "Choose");
        ddlNoOfRoomsAvailable.Items.Insert(1, "0");
        for (int i = 1; i < 101; i++)
        {
            ddlNoOfRoomsAvailable.Items.Insert((i + 1), i.ToString());
        }
        ddlNoOfRoomsAvailable.ClearSelection();
        ddlNoOfRoomsAvailable.SelectedIndex = 0;
        ddlNoOfRoomsAvailable.AutoPostBack = true;
        ddlNoOfRoomsAvailable.SelectedIndexChanged += new EventHandler(ddlNoOfRoomsAvailable_SelectedIndexChanged);
        totalEventHandlersAdded++;
        scmgrBooking.RegisterAsyncPostBackControl(ddlNoOfRoomsAvailable);
        tc.Controls.Add(ddlNoOfRoomsAvailable);
        return tc;
    }

    private static TableCell AddRoomTypeCell(string RoomType)
    {
        TableCell tc = new TableCell();
        tc.BorderWidth = 2;
        tc.Style.Add("display", "none");
        tc.Text = RoomType;
        ///tc.ColumnSpan = 6;
        //tc.Width = 100;
        tc.Attributes.Add("class", "roomtypecell");
        return tc;
    }

    private static TableCell AddRoomsRequestedCell(string Category, string RoomType)
    {
        TableCell tc = new TableCell();
        Label lbl = new Label();
        tc.BorderWidth = 2;
        lbl.EnableViewState = false;
        tc.Visible = false;
        lbl.ID = Constants.LABEL_ROOMS_BOOKED + Category.Trim().Replace(" ", "~") + "*" + RoomType.Trim().Replace(" ", "~");
        lbl.Text = "Rooms Requested: ";
        tc.Controls.Add(lbl);
        return tc;
    }

    private static TableCell AddPaxLabelCell(string Category, string RoomType)
    {
        TableCell tc = new TableCell();
        Label lbl = new Label();
        tc.Attributes.Add("class", "paxcell");
        tc.BorderWidth = 2;
        lbl.EnableViewState = false;
        lbl.ID = Constants.LABEL_PAX + Category.Trim().Replace(" ", "~") + "*" + RoomType.Trim().Replace(" ", "~");
        lbl.Text = "Pax: ";
        tc.Controls.Add(lbl);
        return tc;
    }


    private static TableCell AddtotalPriceLabelCell(string Category, string RoomType)
    {
        TableCell tc = new TableCell();
        Label lbl = new Label();
        tc.Attributes.Add("class", "paxcell");
        tc.BorderWidth = 2;
        lbl.EnableViewState = false;
        lbl.ID = Constants.LABEL_PRICE + Category.Trim().Replace(" ", "~") + "*" + RoomType.Trim().Replace(" ", "~");
        lbl.Text = "Pax: ";
        tc.Controls.Add(lbl);
        return tc;
    }

    private static TableCell AddAddRoomLabelCell(string Category, string RoomType)
    {
        TableCell tc = new TableCell();
        tc.BorderWidth = 2;
        tc.Text = "Add <b>'" + RoomType + "'</b> Rooms of <b>'" + Category + "'</b> category:";
        tc.Attributes.Add("class", "addroomlabelcell");
        return tc;
    }
    #endregion Room Header

    #region Rooms Row
    private TableCell AddRoom(BookedRooms oBookedRoom, int BookingId, out int RoomsBooked, out int RoomsAvailable, out int RoomsWaitListed)
    {
        int iRB = 0, iRA = 0, iRWL = 0;
        TableCell tcRoom = new TableCell();
        HtmlGenericControl divRoom = new HtmlGenericControl("div");
        divRoom.ID = Constants.DIV_ROOMNO + "*" + GF.ReplaceSpace(oBookedRoom.RoomCategory) + "*" + GF.ReplaceSpace(oBookedRoom.RoomType) + "*" + GF.ReplaceSpace(oBookedRoom.RoomNo);
        //tcRoom.ID = Constants.CELL_ROOMNO + "*" + GF.ReplaceSpace(oBookedRoom.RoomCategory) + "*" + GF.ReplaceSpace(oBookedRoom.RoomType) + "*" + GF.ReplaceSpace(oBookedRoom.RoomNo);
        divRoom.InnerHtml = oBookedRoom.RoomNo.ToString();

        if (oBookedRoom.RoomStatus == Constants.Maintainence)
        {
            divRoom.Attributes.Add("onmouseover", "javascript:showRoomMaintenance(" + oBookedRoom.RoomNo.ToString() + "," + BookingId.ToString() + ")");
            divRoom.Attributes.Add("onmouseout", "javascript:hideRoomOtherBookings()");

        }

        else
        {
            divRoom.Attributes.Add("onmouseover", "javascript:showRoomOtherBookings(" + oBookedRoom.RoomNo.ToString() + "," + BookingId.ToString() + ")");
            divRoom.Attributes.Add("onmouseout", "javascript:hideRoomOtherBookings()");

        }



        divRoom.Style.Add(HtmlTextWriterStyle.PaddingRight, "5px");
        divRoom.Style[HtmlTextWriterStyle.Color] = tcRoom.Style[HtmlTextWriterStyle.Color];
        tcRoom.Controls.Add(divRoom);
        RoomsBooked = iRB;
        RoomsAvailable = iRA;
        RoomsWaitListed = iRWL;
        return tcRoom;
    }
    private TableCell AddRoomCheckBox(BookedRooms oBookedRoom)
    {
        TableCell tcRoom = new TableCell();
        CheckBox chkRoomNo = new CheckBox();
        chkRoomNo = new CheckBox();
        chkRoomNo.EnableViewState = false;


        chkRoomNo.ID = Constants.CHECKBOX_ROOM_NO + GF.ReplaceSpace(oBookedRoom.RoomCategory) + "*" + GF.ReplaceSpace(oBookedRoom.RoomType) + "*" + GF.ReplaceSpace(oBookedRoom.RoomNo);


        chkRoomNo.Enabled = false;
        chkRoomNo = SetCheckBoxStatus(chkRoomNo, oBookedRoom);

        tcRoom.Controls.Add(chkRoomNo);

        return tcRoom;
    }
    private TableCell SetRoomStatus(TableCell tcRoom, BookedRooms oBookedRoom, int BookingId, out int RoomsBooked, out int RoomsAvailable, out int RoomsWaitListed)
    {
        TableCell tc = null;
        int iRB = 0, iRA = 0, iRWL = 0;
        tc = tcRoom;
        SortedList slRoomList;
        if (SessionServices.Booking_RoomsStatus != null)
            slRoomList = (SortedList)SessionServices.Booking_RoomsStatus;
        else
            slRoomList = new SortedList();

        if (oBookedRoom.BookingId != 0)
        {
            #region If This BookingId
            if (oBookedRoom.BookingId == BookingId)
            {
                slRoomList.Add(oBookedRoom.RoomNo, oBookedRoom.RoomStatus);
                #region BookedRoom of this Booking
                if (oBookedRoom.RoomStatus == Constants.BOOKED)
                {
                    tc.Style[HtmlTextWriterStyle.Color] = "Blue";
                    iRB++;
                }
                #endregion
                #region Avaialble Room of this Booking
                //Which is practically not possible
                else if (oBookedRoom.RoomStatus == Constants.AVAILABLE)
                {
                    tc.Style[HtmlTextWriterStyle.Color] = "Blue";
                }
                #endregion
                #region Waitlisted Room of this Booking
                else if (oBookedRoom.RoomStatus == Constants.WAITLISTED)
                {
                    tc.Style[HtmlTextWriterStyle.Color] = "Red";
                    iRWL++;
                }
                #endregion
                iRA++;
            }
            #endregion
            #region Other BookingId
            else if (oBookedRoom.BookingId != BookingId && oBookedRoom.BookingId != 0)
            {
                if (oBookedRoom.RoomStatus == Constants.BOOKED)
                {
                    tc.Style[HtmlTextWriterStyle.Color] = "Red";
                }
                else if (!slRoomList.ContainsKey(oBookedRoom.RoomNo))
                {
                    tc.Style[HtmlTextWriterStyle.Color] = "Green";
                }
            }
            #endregion
        }
        else if (oBookedRoom.BookingId == 0)
        {
            //This is practically not possible
            if (oBookedRoom.RoomStatus == Constants.WAITLISTED)
            {
                tc.Style[HtmlTextWriterStyle.Color] = "Red";
                iRWL++;
            }
            else
            {
                tc.Style[HtmlTextWriterStyle.Color] = "Green";
                iRA++;
                if (oBookedRoom.RoomStatus == Constants.BOOKED)
                    iRB++;
            }
        }
        RoomsBooked = iRB;
        RoomsAvailable = iRA;
        RoomsWaitListed = iRWL;
        return tc;
    }
    private CheckBox SetCheckBoxStatus(CheckBox chk, BookedRooms BookedRoom)
    {
        CheckBox ckB = null;
        ckB = chk;
        if (BookedRoom.BookingId == iBookingId && iBookingId != 0)
        {
            if (BookedRoom.RoomStatus == Constants.BOOKED || BookedRoom.RoomStatus == Constants.WAITLISTED)
                ckB.Checked = true;
            else
                ckB.Checked = false;
        }
        else if (BookedRoom.BookingId == 0 && BookedRoom.RoomStatus == Constants.BOOKED)
        {
            ckB.Checked = true;
        }
        else
            ckB.Checked = false;
        return ckB;
    }
    #endregion Rooms Row

    private void SetRoomsRequestedLabel(Table tblParent, int RoomsBooked, string Category, string RoomType)
    {
        Control ctrl = null;
        Label lbl = null;
        string ctrlID = Constants.LABEL_ROOMS_BOOKED + Category.Trim().Replace(" ", "~") + "*" + RoomType.Trim().Replace(" ", "~");
        ctrl = FindControl(tblParent, ctrlID);
        if (ctrl != null)
        {
            lbl = (Label)ctrl;
            lbl.Text = "Rooms Requested: " + RoomsBooked.ToString();
        }
    }

    private void SetRoomsAvailableLabel(Table tblParent, int Roomsavailable, string Category, string RoomType)
    {
        Control ctrl = null;
        Label lbl = null;
        string ctrlID = Constants.LABEL_ROOMS_AVAILABLE + Category.Trim().Replace(" ", "~") + "*" + RoomType.Trim().Replace(" ", "~");
        ctrl = FindControl(tblParent, ctrlID);
        if (ctrl != null)
        {
            lbl = (Label)ctrl;
            lbl.Text = "Rooms Available: " + Roomsavailable.ToString();
        }
    }

    private void SetRoomsWaitlistedLabel(Table tblParent, int Roomswaitlisted, string Category, string RoomType)
    {
        Control ctrl = null;
        Label lbl = null;
        string ctrlID = Constants.LABEL_ROOMS_WAITLISTED + Category.Trim().Replace(" ", "~") + "*" + RoomType.Trim().Replace(" ", "~");

        ctrl = FindControl(tblParent, ctrlID);
        if (ctrl != null)
        {
            lbl = (Label)ctrl;
            lbl.Text = "Waitlisted Rooms : " + Roomswaitlisted.ToString();
        }
    }

    private void SetRoomsPaxLabel(Table tblParent, int Pax, string Category, string RoomType)
    {
        Control ctrl = null;
        Label lbl = null;
        string ctrlID = Constants.LABEL_PAX + Category.Trim().Replace(" ", "~") + "*" + RoomType.Trim().Replace(" ", "~");
        ctrl = FindControl(tblParent, ctrlID);
        if (ctrl != null)
        {
            lbl = (Label)ctrl;
            lbl.Text = "Pax: " + Pax.ToString();
        }
    }

    private void SetRoomsTotalPriceLabel(Table tblParent, double Pax, string Category, string RoomType)
    {
        Control ctrl = null;
        Label lbl = null;
        string ctrlID = Constants.LABEL_PRICE + Category.Trim().Replace(" ", "~") + "*" + RoomType.Trim().Replace(" ", "~");
        ctrl = FindControl(tblParent, ctrlID);
        if (ctrl != null)
        {
            lbl = (Label)ctrl;
            // lbl.Text = "Total(Including Tax @9%): " + Pax.ToString();
            lbl.Text = "Total: " + Pax.ToString();
        }
    }

    private void SetTotalRoomsBookedLabel(Table tblParent, int RoomsBooked, string Category, string RoomType)
    {
        Control ctrl = null;
        Label lbl = null;
        string ctrlID = Constants.LABEL_TOTAL_ROOMS_BOOKED + Category.Trim().Replace(" ", "~") + "*" + RoomType.Trim().Replace(" ", "~");
        ctrl = FindControl(tblParent, ctrlID);
        if (ctrl != null)
        {
            lbl = (Label)ctrl;
            if (lbl != null)
                lbl.Text = "Total Rooms Booked: " + RoomsBooked.ToString();
        }
    }

    private void SetTotalRooms(Table tblParent, int RoomsBooked, int RoomsAvailable, int RoomsWaitlisted, string Category, string RoomType, int RoomsPerType)
    {
        int TotalRooms = RoomsBooked + RoomsWaitlisted;
        Control ctrl = null;
        DropDownList ddl = null;
        //HtmlSelect ddl;
        //ListItem l;
        int iIndex = 0;
        string ctrlID = Constants.DROPDOWNLIST_ROOMS + GF.ReplaceSpace(Category) + "*" + GF.ReplaceSpace(RoomType);
        ctrl = FindControl(tblParent, ctrlID);
        if (ctrl != null)
        {
            ddl = (DropDownList)ctrl;
            //ddl = (HtmlSelect)ctrl;
            if (ddl != null)
            {
                ddl.Items.Clear();
                ddl.Items.Insert(0, "Choose");
                ddl.Items.Insert(1, "0");
                for (int i = 0; i < RoomsPerType; i++)
                {
                    if (RoomsBooked > 0 && RoomsWaitlisted <= 0)
                    {
                        //if ((i + 1) == TotalRooms)
                        //iIndex = i + 2;
                        if ((i + 1) == RoomsBooked)
                            iIndex = i + 2;
                    }
                    else if (RoomsWaitlisted > 0 && RoomsBooked <= 0)
                    {
                        if ((i + 1) == RoomsWaitlisted)
                            iIndex = i + 2;
                    }
                    else if (RoomsBooked > 0 && RoomsWaitlisted > 0)
                    {
                        if ((i + 1) == TotalRooms)
                            iIndex = i + 2;
                    }
                    ddl.Items.Insert(i + 2, Convert.ToString(i + 1));
                }
                //ddl.Items[iIndex].Selected = true;
                ddl.SelectedIndex = iIndex;
                keepRoomDropDownsSelectedIndex(ddl.ID, iIndex);
            }
        }
    }

    private void keepRoomDropDownsSelectedIndex(string dropDownId, int selectedItemIndex)
    {
        SortedList sl = null;
        sl = (SortedList)SessionServices.Booking_DdlSelectedIndexes;
        if (sl == null)
            sl = new SortedList();

        if (sl.Contains(dropDownId))
            sl[dropDownId] = selectedItemIndex.ToString();
        else
            sl.Add(dropDownId, selectedItemIndex.ToString());
        SessionServices.Booking_DdlSelectedIndexes = sl;
    }

    private void ddlNoOfRoomsAvailable_SelectedIndexChanged(object sender, EventArgs e)
    {
        //This is the event handler to handle the no. of rooms selected for booking
        DropDownList ddl = null;
        int iRoomsToBeBooked = 0, iRoomsLeftToBeBooked = 0, iRoomsBooked = 0, iPax = 0, iTotalPax = 0;
        string[] sName;
        string sCtrlName = "", sCategory = "", sType = "";

        sCtrlName = GetPostBackControlID();
        eventCounter++;
        if (sCtrlName != string.Empty)
        {
            Control ctrl = FindControl(sCtrlName);
            ddl = (DropDownList)ctrl;
            int.TryParse(ddl.SelectedItem.Text, out iRoomsToBeBooked);

            if (addRoomEventingTrackers.Exists(t => string.Compare(t.ControlName, sCtrlName, StringComparison.OrdinalIgnoreCase) == 0 && (t.Value == iRoomsToBeBooked)))
            {
                if (eventCounter == totalEventHandlersAdded)
                {
                    addRoomEventingTrackers.Clear();
                }
                return;
            }
            else
            {
                AddRoomEventingTracker tracker = new AddRoomEventingTracker { ControlName = sCtrlName, Value = iRoomsToBeBooked };
                addRoomEventingTrackers.Add(tracker);
            }

            sName = ddl.ID.Split('*');
            if (sName.Length > 0)
                sCategory = sName[1].Trim().Replace("~", " ");
            if (sName.Length > 1)
                sType = sName[2].Trim().Replace("~", " ");

            SetRoomsBookeDataObject(tblMaster, sCategory, sType, iBookingId, iRoomsToBeBooked, out iRoomsBooked, out iRoomsLeftToBeBooked, out iPax, out iTotalPax);
            SetRoomsWaitListeDataObject(tblMaster, sCategory, sType, iRoomsLeftToBeBooked, iBookingId);
            SetRoomStatus(tblMaster);
            SetRoomDropDownIndex();
        }
    }

    private void SetRoomsBookeDataObject(Control ParentControl, string Category, string RoomType, int BookingId, int RoomsToBeBooked, out int RoomsBooked, out int RoomsLeftToBeBooked, out int PaxStaying, out int TotalPaxStaying)
    {
        int iChkSelected = 0;
        string scntrlId = "";
        BookedRooms[] oBR = null;
        SortedList sl = new SortedList();
        SortedList slRoomsAvailable = null;
        SortedList slBookedWithThisBooking = new SortedList();
        int iTotalPax = 0, iRPax = 0, slCounter = 0, listIndex = -1;
        int intmnt = 0;


        double itotalAmt = 0;

        oBR = GetRoomObjectFromSession();
        if (oBR == null)
        {
            RoomsBooked = 0;
            RoomsLeftToBeBooked = 0;
            PaxStaying = 0;
            TotalPaxStaying = 0;
            return;
        }

        #region Rooms List Management
        //Assigning the roms of this particualr category and type to the appropriate list
        for (int i = 0; i < oBR.Length; i++)
        {
            oBR[i].action = "AddPriceDetailsToo";
            //Assigning the rooms of this particualr category and type to the appropriate list
            if (GF.ReplaceSpace(oBR[i].RoomCategory) == GF.ReplaceSpace(Category) && GF.ReplaceSpace(oBR[i].RoomType) == GF.ReplaceSpace(RoomType))
            {
                scntrlId = Constants.CHECKBOX_ROOM_NO + GF.ReplaceSpace(Category) + "*" + GF.ReplaceSpace(RoomType) + "*" + oBR[i].RoomNo.Trim();
                SetCheckBoxStatus(ParentControl, scntrlId, false);

                if (oBR[i].BookingId == BookingId || oBR[i].BookingId == 0)
                {
                    if (!slBookedWithThisBooking.Contains(oBR[i].RoomNo))
                    {
                        slBookedWithThisBooking.Add(oBR[i].RoomNo, oBR[i].RoomStatus);
                    }
                    #region If Booked with this Id
                    if (oBR[i].RoomStatus.CompareTo(Constants.BOOKED) == 0 || oBR[i].RoomStatus.CompareTo('\0') == 0 || oBR[i].RoomStatus.CompareTo(Constants.AVAILABLE) == 0)
                    {
                        if (slRoomsAvailable == null)
                            slRoomsAvailable = new SortedList();
                        if (!slRoomsAvailable.ContainsValue(scntrlId))
                        {
                            slRoomsAvailable.Add(i, scntrlId);
                        }
                        else //If this Room was added by any other id, and later found that it is booked with the 
                        //current id then status is overridden with the index of the Current Booking Room
                        {
                            listIndex = slRoomsAvailable.IndexOfValue(scntrlId);
                            slRoomsAvailable.RemoveAt(listIndex);
                            slRoomsAvailable.Add(i, scntrlId);
                        }
                    }
                    else if (oBR[i].RoomStatus.CompareTo(Constants.Maintainence) == 0)
                    {
                        if (chkChartered.Checked)
                        {
                            if (slRoomsAvailable == null)
                                slRoomsAvailable = new SortedList();
                            if (!slRoomsAvailable.ContainsValue(scntrlId))
                            {
                                slRoomsAvailable.Add(i, scntrlId);
                            }
                        }
                        else
                        {
                            intmnt++;
                        }

                    }

                    #endregion
                    #region If Waitlisted with this booking
                    else if (oBR[i].RoomStatus.CompareTo(Constants.WAITLISTED) == 0)
                    {
                        if (slRoomsAvailable != null)
                        {
                            if (slRoomsAvailable.ContainsValue(scntrlId))
                            {
                                listIndex = slRoomsAvailable.IndexOfValue(scntrlId);
                                slRoomsAvailable.RemoveAt(listIndex);
                            }
                        }
                    }
                    #endregion
                }
                else if (oBR[i].BookingId != BookingId && oBR[i].BookingId != 0)
                {
                    if (oBR[i].RoomStatus.CompareTo(Constants.BOOKED) == 0)
                    {
                        if (slRoomsAvailable != null)
                        {
                            listIndex = slRoomsAvailable.IndexOfValue(scntrlId);

                            if (listIndex >= 0) //If ITem Exists
                                slRoomsAvailable.RemoveAt(listIndex);
                            listIndex = -1;
                        }
                    }
                }
            }
            else //Generating the Pax in other Room Category-Type of this booking
            {
                if (oBR[i].BookingId == BookingId)
                {
                    if (oBR[i].RoomStatus.CompareTo(Constants.BOOKED) == 0)
                    {

                        if (oBR[i].PaxStaying > 0)
                            iTotalPax = iTotalPax + oBR[i].PaxStaying;
                        else
                        {
                            iTotalPax = iTotalPax + oBR[i].DefaultNoOfBeds;
                            oBR[i].PaxStaying = oBR[i].DefaultNoOfBeds;
                        }
                    }
                }
            }
        }
        #endregion Rooms List Management

        #region Checkbox setter
        if (slRoomsAvailable != null)
        {
            int key = -1;
            string val = "";
            int index;
            //On the basis of list setting the value of each check box to either true or false.
            if (RoomsToBeBooked == 0) //If roooms selected are 0 then all the rooms are being freed in the object.
            {
                if (slRoomsAvailable != null && slRoomsAvailable.Count > 0)
                {
                    for (int j = 0; j < slRoomsAvailable.Count; j++)
                    {
                        int.TryParse(slRoomsAvailable.GetKey(slCounter).ToString(), out key);
                        val = slRoomsAvailable[key].ToString();

                        SetCheckBoxStatus(ParentControl, val, false);
                        index = key;
                        //Keeping the Current Status before releaseing the room
                        oBR[index].PrevBookingId = oBR[index].BookingId;
                        oBR[index].PrevRoomStatus = oBR[index].RoomStatus;
                        oBR[index].PrevPaxStaying = oBR[index].PaxStaying;

                        oBR[index].BookingId = 0;
                        oBR[index].RoomStatus = Constants.AVAILABLE;
                        oBR[index].PaxStaying = 0;

                        //oBR[index].BookingId = oBR[index].OriginalBookingId;
                        //oBR[index].PaxStaying = oBR[index].OriginalPaxStaying;
                        //oBR[index].RoomStatus = oBR[index].OriginalRoomStatus;

                        slCounter++;
                    }
                }
                slRoomsAvailable.Clear();
            }

            #region Rooms_Available
            if (slRoomsAvailable != null && slRoomsAvailable.Count > 0)
            {
                for (slCounter = 0; slCounter < slRoomsAvailable.Count; slCounter++)
                {
                    int.TryParse(slRoomsAvailable.GetKey(slCounter).ToString(), out key);
                    val = slRoomsAvailable[key].ToString();
                    index = key;
                    if (iChkSelected < RoomsToBeBooked)
                    {
                        oBR[index].BookingId = BookingId;
                        oBR[index].RoomStatus = Constants.BOOKED;


                        if (oBR[index].PaxStaying > 0)
                            iRPax = iRPax + oBR[index].PaxStaying;
                       
                        else
                            iRPax = iRPax + oBR[index].DefaultNoOfBeds;


                        if (oBR[index].PaxStaying <= 0)
                        {
                            oBR[index].PaxStaying = oBR[index].DefaultNoOfBeds;
                        }








                        SetCheckBoxStatus(ParentControl, val, true);

                        try
                        {
                            if (ddlAccomName.SelectedValue == "7")
                            {
                                bindRoomRatesCruise(iRPax);
                            }
                            else
                            {
                                bindRoomRates(Convert.ToInt32(ddlAccomName.SelectedValue), iRPax, Convert.ToInt32(ddlAgentType.SelectedValue), Convert.ToDateTime(txtStartDate.Text.Trim()), Convert.ToDateTime(txtEndDate.Text.Trim()), iChkSelected, oBR[index].RoomTypeId);
                            }

                            if (index > 0)
                            {
                                if (oBR[index].RoomCategory != oBR[index - 1].RoomCategory)
                                {

                                    total = 0;
                                }
                            }
                            else
                            {

                                total = 0;
                            }
                            //if (oBR[index].PaxStaying > 0)
                            //{
                            oBR[index].Price = CalcaulateRates(oBR[index].RoomCategoryId, oBR[index].RoomTypeId, oBR[index].PaxStaying);
                            //}
                            //else
                            //{
                            // oBR[index].Price = CalcaulateRates(oBR[index].RoomCategoryId, oBR[index].RoomTypeId, iRPax);
                            //}

                            oBR[index].action = "AddPriceDetailsToo";
                            oBR[index].PaymentId = "DR" + DateTime.Now.ToString("MMddhhmmssfff");

                            itotalAmt = itotalAmt + oBR[index].Price;


                            txtTotalAmount.Text = itotalAmt.ToString();
                            //   itotalAmt = itotalAmt + oBR[index].Price;

                        }
                        catch
                        {

                        }



                        key = -1;
                        val = "";
                        index = 0;
                        iChkSelected++;





                    }
                    else
                    {
                        //Keeping the Current Status before releaseing the room
                        if (oBR[index].PrevBookingId == oBR[index].BookingId || oBR[index].PrevBookingId == 0)
                        {
                            if (oBR[index].PrevBookingId != 0 && oBR[index].PrevRoomStatus != Constants.AVAILABLE)
                            {
                                oBR[index].PrevBookingId = oBR[index].BookingId;
                                oBR[index].PrevRoomStatus = oBR[index].RoomStatus;
                                oBR[index].PrevPaxStaying = oBR[index].PaxStaying;
                            }
                            //
                            oBR[index].BookingId = 0;
                            oBR[index].RoomStatus = Constants.AVAILABLE;
                            oBR[index].PaxStaying = 0;

                            //oBR[index].BookingId = oBR[index].OriginalBookingId;
                            //oBR[index].PaxStaying = oBR[index].OriginalPaxStaying;
                            //oBR[index].RoomStatus = oBR[index].OriginalRoomStatus;
                        }
                        else
                        {
                            oBR[index].BookingId = oBR[index].PrevBookingId;
                            oBR[index].RoomStatus = oBR[index].PrevRoomStatus;
                            oBR[index].PaxStaying = oBR[index].PrevPaxStaying;
                        }
                        SetCheckBoxStatus(ParentControl, val, false);
                        key = -1;
                        val = "";
                        index = 0;
                    }
                }
                //slRoomsAvailable.Clear();                                        
            }
            #endregion Rooms_Available
        }



        #endregion Checkbox setter
        if (slRoomsAvailable != null)
            slRoomsAvailable.Clear();
        slRoomsAvailable = null;

        RoomsLeftToBeBooked = RoomsToBeBooked - iChkSelected;
        RoomsBooked = iChkSelected;
        PaxStaying = iRPax;
        TotalPaxStaying = iTotalPax + iRPax;


        SetRoomObjectToSession(oBR);


    }

    private void SetRoomsWaitListeDataObject(Control ParentControl, string Category, string RoomType, int RoomsTobeWaitListed, int BookingId)
    {
        int iChkSelected = 0;
        string scntrlId = "";
        BookedRooms[] oBR = null;
        SortedList sl = new SortedList();
        SortedList slRoomsAvailable = null, slRoomsBooked_WithThisBooking = null, slRoomsBooked_WithOtherBooking = null;
        int iTotalPax = 0, slCounter = 0, listIndex = -1;

        oBR = GetRoomObjectFromSession();
        if (oBR == null)
            return;

        #region Rooms List Management
        //Assigning the roms of this particualr category and type ot the appropriate list
        #region Generating List
        for (int i = 0; i < oBR.Length; i++)
        {
            //Assigning the rooms of this particualr category and type to the appropriate list
            if (oBR[i].RoomCategory.Trim().Replace(" ", "~") == Category.Replace(" ", "~") &&
                oBR[i].RoomType.Trim().Replace(" ", "~") == RoomType.Replace(" ", "~"))
            {
                scntrlId = Constants.CHECKBOX_ROOM_NO + Category.Trim().Replace(" ", "~") + "*" +
                           RoomType.Trim().Replace(" ", "~") + "*" +
                           oBR[i].RoomNo.Trim().Replace(" ", "~").ToString();
                //SetCheckBoxStatus(ParentControl, scntrlId, false);

                if (oBR[i].BookingId == BookingId)
                {
                    if (oBR[i].RoomStatus.CompareTo(Constants.BOOKED) == 0 || oBR[i].RoomStatus.CompareTo('\0') == 0 || oBR[i].RoomStatus.CompareTo(Constants.AVAILABLE) == 0)
                    {
                        if (slRoomsAvailable == null)
                            slRoomsAvailable = new SortedList();
                        if (!slRoomsAvailable.ContainsValue(scntrlId))
                        {
                            //slRoomsAvailable.Add(scntrlId, scntrlId);
                            slRoomsAvailable.Add(i, scntrlId);
                        }
                    }
                    else if (oBR[i].RoomStatus.CompareTo(Constants.WAITLISTED) == 0)
                    {
                        if (slRoomsBooked_WithThisBooking == null)
                            slRoomsBooked_WithThisBooking = new SortedList();
                        if (slRoomsAvailable == null)
                            slRoomsAvailable = new SortedList();

                        if (!slRoomsBooked_WithThisBooking.ContainsValue(scntrlId))
                            slRoomsBooked_WithThisBooking.Add(i, scntrlId);
                        if (slRoomsAvailable.ContainsValue(scntrlId))
                        {
                            listIndex = slRoomsAvailable.IndexOfValue(scntrlId);
                            slRoomsAvailable.RemoveAt(listIndex);
                        }
                    }
                }
                else if (oBR[i].BookingId != BookingId)
                {
                    if (slRoomsBooked_WithOtherBooking == null)
                        slRoomsBooked_WithOtherBooking = new SortedList();

                    //If this rooms is already booked and available with this Booking 
                    //then do not add to the BookedWithOther list
                    if (oBR[i].RoomStatus == Constants.BOOKED)
                    {
                        if (slRoomsAvailable != null)
                        {
                            listIndex = slRoomsAvailable.IndexOfValue(scntrlId);
                            if (listIndex >= 0)
                                slRoomsAvailable.RemoveAt(listIndex);
                            listIndex = -1;
                        }
                    }
                    //else
                    //{
                    //    if (slRoomsAvailable == null)
                    //        slRoomsAvailable = new SortedList();
                    //    if (!slRoomsAvailable.ContainsValue(scntrlId))
                    //    {
                    //        slRoomsAvailable.Add(i, scntrlId);
                    //    }
                    //}
                    if (slRoomsAvailable != null)
                    {
                        if (!slRoomsAvailable.ContainsValue(scntrlId))
                        {
                            if (!slRoomsBooked_WithOtherBooking.ContainsValue(scntrlId))
                                slRoomsBooked_WithOtherBooking.Add(i, scntrlId);
                        }
                    }
                    //If this rooms is already booked and waitlisted with this Booking 
                    //then do not add to the BookedWithOther list
                    else if (slRoomsBooked_WithThisBooking != null)
                    {
                        if (!slRoomsBooked_WithThisBooking.ContainsValue(scntrlId))
                        {
                            if (!slRoomsBooked_WithOtherBooking.ContainsValue(scntrlId))
                                slRoomsBooked_WithOtherBooking.Add(i, scntrlId);
                        }
                    }
                    else
                    {
                        if (!slRoomsBooked_WithOtherBooking.ContainsValue(scntrlId))
                            slRoomsBooked_WithOtherBooking.Add(i, scntrlId);
                    }

                }
            }
        }
        #endregion Generating List

        #region Releasing all waitlisted rooms if rooms to be waitlisted is ZERO
        if (RoomsTobeWaitListed == 0)
        {
            for (int j = 0; j < oBR.Length; j++)
            {
                if (oBR[j].BookingId == iBookingId && oBR[j].RoomStatus == Constants.WAITLISTED &&
                    oBR[j].RoomCategory.Trim().ToUpper() == Category.Trim().ToUpper() &&
                    oBR[j].RoomType.Trim().ToUpper() == RoomType.Trim().ToUpper())
                {
                    oBR[j].BookingId = oBR[j].OriginalBookingId;
                    oBR[j].RoomStatus = oBR[j].OriginalRoomStatus;
                    oBR[j].PaxStaying = oBR[j].OriginalPaxStaying;
                }
            }
        }

        #endregion

        #region List Manager
        ////Remove the room from other list, if the rooms are in the Rooms available list.
        int listindex;
        if (slRoomsAvailable != null)
        {
            for (int i = 0; i < slRoomsAvailable.Count; i++)
            {
                if (slRoomsBooked_WithThisBooking != null)
                {
                    if (slRoomsBooked_WithThisBooking.ContainsValue(slRoomsAvailable.GetByIndex(i)))
                    {
                        listindex = -1;
                        listindex = slRoomsBooked_WithThisBooking.IndexOfValue(slRoomsAvailable.GetByIndex(i));
                        slRoomsBooked_WithThisBooking.RemoveAt(listindex);
                    }
                }
                if (slRoomsBooked_WithOtherBooking != null)
                {
                    if (slRoomsBooked_WithOtherBooking.ContainsValue(slRoomsAvailable.GetByIndex(i)))
                    {
                        listindex = -1;
                        listindex = slRoomsBooked_WithOtherBooking.IndexOfValue(slRoomsAvailable.GetByIndex(i));
                        slRoomsBooked_WithOtherBooking.RemoveAt(listindex);
                    }
                }
            }
        }

        //if (slRoomsBooked_WithThisBooking != null)
        //{
        //    for (int i = 0; i < slRoomsBooked_WithThisBooking.Count; i++)
        //    {
        //        if (slRoomsBooked_WithOtherBooking != null)
        //        {
        //            if (slRoomsBooked_WithOtherBooking.ContainsValue(slRoomsBooked_WithThisBooking.GetByIndex(i)))
        //            {
        //                listindex = -1;
        //                listindex = slRoomsBooked_WithOtherBooking.IndexOfValue(slRoomsBooked_WithThisBooking.GetByIndex(i));
        //                slRoomsBooked_WithOtherBooking.RemoveAt(listindex);
        //            }
        //        }
        //    }
        //}
        #endregion List Manager


        #endregion Rooms List Management

        #region Checkbox setter
        if (slRoomsAvailable != null || slRoomsBooked_WithThisBooking != null || slRoomsBooked_WithOtherBooking != null)
        {
            int index = 0;
            int key = 0;
            string val = "";

            //On the basis of list setting the value of each check box to either true or false.
            #region Rooms_Booked_WithThis_Booking
            if (slRoomsBooked_WithThisBooking != null && slRoomsBooked_WithThisBooking.Count > 0)
            {
                for (slCounter = 0; slCounter < slRoomsBooked_WithThisBooking.Count; slCounter++)
                {
                    int.TryParse(slRoomsBooked_WithThisBooking.GetKey(slCounter).ToString(), out key);
                    val = slRoomsBooked_WithThisBooking[key].ToString();
                    index = key;
                    if (iChkSelected < RoomsTobeWaitListed) //Rooms count starts from Zero
                    {
                        SetCheckBoxStatus(ParentControl, val, true);
                        oBR[index].PrevBookingId = oBR[index].BookingId;
                        oBR[index].PrevRoomStatus = oBR[index].RoomStatus;
                        oBR[index].PrevPaxStaying = oBR[index].PrevPaxStaying;

                        oBR[index].BookingId = BookingId;
                        oBR[index].RoomStatus = Constants.WAITLISTED;
                        oBR[index].PaxStaying = 0;


                        if (slRoomsBooked_WithOtherBooking != null)
                        {
                            if (slRoomsBooked_WithOtherBooking.ContainsValue(val))
                            {
                                listindex = slRoomsBooked_WithOtherBooking.IndexOfValue(val);
                                if (listindex >= 0)
                                    slRoomsBooked_WithOtherBooking.RemoveAt(listindex);

                            }
                        }

                        key = -1;
                        val = "";
                        index = 0;
                        iChkSelected++;
                    }
                    else
                    {
                        SetCheckBoxStatus(ParentControl, slRoomsBooked_WithThisBooking.GetByIndex(slCounter).ToString(), false);
                        if (oBR[index].BookingId == oBR[index].OriginalBookingId)
                        {
                            if (slRoomsBooked_WithOtherBooking.ContainsValue(val))
                            {
                                listindex = slRoomsBooked_WithOtherBooking.IndexOfValue(val);
                                int.TryParse(slRoomsBooked_WithOtherBooking.GetKey(listindex).ToString(), out key);
                                int newindex = key;
                                oBR[index].BookingId = oBR[newindex].BookingId;
                                oBR[index].RoomStatus = oBR[newindex].RoomStatus;
                                oBR[index].PaxStaying = oBR[newindex].PaxStaying;
                            }
                        }
                        else
                        {
                            oBR[index].BookingId = oBR[index].OriginalBookingId;
                            oBR[index].RoomStatus = oBR[index].OriginalRoomStatus;
                            oBR[index].PaxStaying = oBR[index].OriginalPaxStaying;
                        }
                    }
                }
                slRoomsBooked_WithThisBooking.Clear();

            }
            #endregion Rooms_Booked_WithThis_Booking

            #region Rooms_Booked_WithOtherBooking
            if (slRoomsBooked_WithOtherBooking != null && slRoomsBooked_WithOtherBooking.Count > 0)
            {
                for (slCounter = 0; slCounter < slRoomsBooked_WithOtherBooking.Count; slCounter++)
                {
                    if (iChkSelected < RoomsTobeWaitListed)
                    {
                        int.TryParse(slRoomsBooked_WithOtherBooking.GetKey(slCounter).ToString(), out key);
                        val = slRoomsBooked_WithOtherBooking[key].ToString();
                        SetCheckBoxStatus(ParentControl, val, true);
                        index = key;

                        oBR[index].PrevBookingId = oBR[index].BookingId;
                        oBR[index].PrevRoomStatus = oBR[index].RoomStatus;
                        oBR[index].PrevPaxStaying = oBR[index].PaxStaying;

                        oBR[index].BookingId = BookingId;
                        oBR[index].RoomStatus = Constants.WAITLISTED;
                        oBR[index].PaxStaying = 0;
                        key = -1;
                        val = "";
                        index = 0;
                        iChkSelected++;
                    }
                    else
                    {
                        SetCheckBoxStatus(ParentControl, slRoomsBooked_WithOtherBooking.GetByIndex(slCounter).ToString(), false);
                    }
                }
                slRoomsBooked_WithOtherBooking.Clear();
            }

            #endregion Rooms_Booked_WithOtherBooking


        }
        #endregion Checkbox setter

        SetTotalNoOfPerson(iTotalPax);
        SetRoomObjectToSession(oBR);
    }

    private void SetTotalNoOfPerson(int iTotalPax)
    {
        txtNoOfPersons.Text = string.Empty;
        txtNoOfPersons.Text = iTotalPax.ToString();
    }

    private void SetCheckBoxStatus(Control ParentControl, string ControlId, bool value)
    {
        //This Method is called when the dropdown of the rooms available is clicked on the booking screen. 
        //The other one is called through PrepareChart
        Control c = null;
        CheckBox ch = null;
        c = FindControl(ParentControl, ControlId);

        if (c != null)
            ch = (CheckBox)c;
        if (ch != null)
            ch.Checked = value;
    }

    private BookingDTMail[] BookDetailsformail(int bookingId)
    {
        BookingDTMail[] obd = null;
        BookingReportServices oBRM = new BookingReportServices();
        obd = oBRM.GetBookingDetailsMail(bookingId);
        if (obd != null)
        {
            return obd;


        }
        else
        {
            return null;
        }

    }

    private BookingRoomReportsDTO[] BookingRoomDetailsformail(int bookingid)
    {
        BookingRoomReportsDTO[] oBRRD = null;
        BookingReportServices oBRM = new BookingReportServices();
        oBRRD = oBRM.GetroomBookingDetailsMail(bookingid);
        if (oBRRD != null)
        {

            return oBRRD;
        }
        else
        {
            return null;
        }
    }

    public void InsertAmountDetails(decimal amt)
    {
        blsr.action = "AddPriceDetailsToo";
        blsr._Amt = amt;
        blsr.PaymentId = Session["BookingPayId"] == null ? Session["BookingPayId"].ToString() : string.Empty;
        blsr._PaidAmount = Session["Paid"] != null ? Convert.ToDouble(Session["Paid"]) : 0;
        int GetQueryResponse = dlsr.AddRoomBookingDetails(blsr);
    }

    private void SaveBooking()
    {
        RoomBookingInfoDTO oRoomBookingInfo = new RoomBookingInfoDTO();
        BookingServices objBookingMgr = null;
        //BookedRooms[] oBookedRooms = null;
        BookedRooms[] oTotallyRemovedRoomCategoryAndType = null;
        //BookingTouristDTO[] oBookingWaitListData = null;

        List<BookedRooms> oBookedRooms = null;
        List<BookingWaitListDTO> oBookingWaitlistList = null;

        //clsBookingWaitListData[] oOtherBookingWaitlist = null;
        bool bActionCompleted = false, bNewBooking = false;
        string[] sFormControls;
        bool bRoomsAreWaitlisted = false;
        try
        {
            oRoomBookingInfo.BookingData = new BookingDTO();
            oRoomBookingInfo.BookingData.BookingId = iBookingId;
            oRoomBookingInfo.BookingData.BookingReference = txtBookingRef.Text.ToString();
            oRoomBookingInfo.BookingData.StartDate = Convert.ToDateTime(txtStartDate.Text.ToString());
            oRoomBookingInfo.BookingData.EndDate = Convert.ToDateTime(txtEndDate.Text.ToString());
            oRoomBookingInfo.BookingData.AccomodationTypeId = Convert.ToInt32(ddlAccomType.SelectedItem.Value.ToString());
            oRoomBookingInfo.BookingData.AccomodationId = Convert.ToInt32(ddlAccomName.SelectedItem.Value.ToString());
            if (ddlAgentType.SelectedIndex > 0)
            {
                oRoomBookingInfo.BookingData.AgentType = ddlAgent.SelectedValue.ToString();
            }
            else
            {

            }

            try
            {
                oRoomBookingInfo.BookingData.NoOfNights = Convert.ToInt32(txtNoOfNights.Text.ToString());
            }
            catch
            {
                oRoomBookingInfo.BookingData.NoOfNights = 1;
            }
            try
            {
                oRoomBookingInfo.BookingData.NoOfPersons = Convert.ToInt32(txtNoOfPersons.Text.ToString());
            }
            catch
            {
                oRoomBookingInfo.BookingData.NoOfPersons = 1;
            }

            oRoomBookingInfo.BookingData.AgentId = Convert.ToInt32(ddlAgentType.SelectedItem.Value.ToString());
            oRoomBookingInfo.BookingData.AgentIdRef = Convert.ToInt32(ddlAgent.SelectedItem.Value.ToString());
            oRoomBookingInfo.BookingData.BookingStatusId = 1; //If the room is in booked state and no rooms is in waitlist state.
            oRoomBookingInfo.BookingData.ProposedBooking = rdProposedBookingYes.Checked == true ? true : false;
            oRoomBookingInfo.BookingData.Chartered = chkChartered.Checked == true ? true : false;
            oRoomBookingInfo.BookingData.packagid = ddlpackage.SelectedValue.ToString();


            objBookingMgr = new BookingServices();
            int iBRC = objBookingMgr.GetBookingReferenceCount(oRoomBookingInfo.BookingData);
            if (iBRC > 0)
            {
                base.DisplayAlert("The Booking Reference mentioned by you is not unique. Please enter a different reference number.");
                return;
            }

            sFormControls = Request.Form.AllKeys;

            //oBookedRooms = GetFinalizedRooms(iBookingId, oRoomBookingInfo.BookingData.AccomodationId);
            GetFinalizedRooms(iBookingId, oRoomBookingInfo.BookingData.AccomodationId, out oBookedRooms, out oBookingWaitlistList);
            for (int i = 0; i < oBookedRooms.Count; i++)
            {
                paid = paid + Convert.ToDecimal(oBookedRooms[i].Paid);
            }

            //oBookingWaitListData = GetWaitlistedRooms(iBookingId, oRoomBookingInfo.BookingData.AccomodationId, out bRoomsAreWaitlisted); //ADDED THE OUT PARAMETER TO KNOW THE BOOKING STATUS

            //if (oBookedRooms.Count == 0 && oBookingWaitlistList.Count == 0)
            //{
            //    base.DisplayAlert("Please choose rooms to be booked.");
            //    return;
            //}
            oRoomBookingInfo.BookingData.agentcommission = getcommission(Convert.ToInt32(ddlAccomType.SelectedValue.ToString()), Convert.ToInt32(ddlAccomName.SelectedValue.ToString()), paid);
            if (oBookingWaitlistList.Count > 0)
                oRoomBookingInfo.BookingData.BookingStatusId = 3;

            if (iBookingId == 0)
            {
                if (base.ValidateIfCommandAllowed(Request.Url.ToString(), ENums.PageCommand.Add))
                {
                    Session["BookDetMail"] = null;
                    Session["BookroomDetmail"] = null;
                    //if (Convert.ToBoolean(ViewState["atleastonewaitlisted"] == null ? false : ViewState["atleastonewaitlisted"]) == true && chkChartered.Checked == true)
                    //{
                    //    System.Web.UI.ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "AlertBox", "alert('All rooms must be available for Chartered Booking');", true);
                    //}
                    //else
                    //{
                    bActionCompleted = objBookingMgr.AddBooking(oRoomBookingInfo.BookingData, oBookedRooms.ToArray(), oBookingWaitlistList.ToArray(), out iBookingId);
                    bNewBooking = true;
                    //}

                    ViewState["atleastonewaitlisted"] = null;
                }
            }
            else if (iBookingId > 0)
            {
                bNewBooking = false;
                if (base.ValidateIfCommandAllowed(Request.Url.ToString(), ENums.PageCommand.Update))
                {
                    Session["BookDetMail"] = BookDetailsformail(iBookingId);
                    Session["BookroomDetmail"] = BookingRoomDetailsformail(iBookingId);
                    bActionCompleted = objBookingMgr.UpdateBooking(oRoomBookingInfo.BookingData, oBookedRooms.ToArray(), oBookingWaitlistList.ToArray(), oTotallyRemovedRoomCategoryAndType);
                }
            }

            if (bNewBooking == true)
            {
                if (bActionCompleted == true)
                {
                    base.DisplayAlert("The tour has been added successfully.");
                }
                else
                {
                    base.DisplayAlert("The tour has not been added successfully.");
                }
            }
            else if (bNewBooking == false)
            {
                if (bActionCompleted == true)
                {
                    base.DisplayAlert("The tour has been updated successfully.");
                }
                else
                {
                    base.DisplayAlert("The tour has not been updated successfully");
                }
            }
        }
        catch (Exception exp)
        {
            base.DisplayAlert(exp.Message);
        }
        finally
        {
            objBookingMgr = null;
            oRoomBookingInfo = null;
            oBookedRooms = null;
        }
        if (bActionCompleted == true)
        {
            ClearSessionVariables();
            //if(oBookingWaitListData !=null && oBookingWaitListData.Length >0)
            string updated = string.Empty;
            if (bNewBooking == true)
                updated = "&updated=false";
            else
                updated = "&updated=true";

            if (bRoomsAreWaitlisted == true)
                Response.Redirect("afterBookingactions.aspx?bid=" + iBookingId + "&bstatus=waitlisted" + updated);
            else if (rdProposedBookingYes.Checked == true)
                Response.Redirect("afterBookingactions.aspx?bid=" + iBookingId + "&bstatus=Proposed Booking" + updated);
            else
                Response.Redirect("afterBookingactions.aspx?bid=" + iBookingId + "&bstatus=booked" + updated);
        }
    }
    public bool AddBooking(BookingDTO oBookingData, BookedRooms[] oBookedRooms, BookingWaitListDTO[] oBookingWaitListData, out int BookingId)
    {
        int iBKID = 0;
        bool bActionCompleted = false;


        bActionCompleted = AddBooking(oBookingData, out iBKID);
        if (bActionCompleted == true)
        {
            if (oBookedRooms != null)
            {
                for (int i = 0; i < oBookedRooms.Length; i++)
                {
                    if (oBookedRooms[i] != null)
                        oBookedRooms[i].BookingId = iBKID;
                }
            }

            if (oBookingWaitListData != null)
            {
                for (int i = 0; i < oBookingWaitListData.Length; i++)
                {
                    if (oBookingWaitListData[i] != null)
                        oBookingWaitListData[i].BookingId = iBKID;
                }
            }
            if (oBookedRooms != null)
            {

            }
            if (bActionCompleted == true)
            {
                if (oBookingWaitListData != null)
                {

                }
            }
        }
        else
        {
            iBKID = 0;
        }
        BookingId = iBKID;
        return bActionCompleted;

    }

    private bool AddBooking(BookingDTO objBooking, out int BookingId)
    {
        int iBookingID;

        try
        {
            if (oDB == null)
                oDB = new DatabaseManager();
            string sProcName = "up_Ins_Booking";
            oDB.DbCmd = oDB.GetStoredProcCommand(sProcName);
            oDB.DbDatabase.AddInParameter(oDB.DbCmd, "@sBookingRef", DbType.String, objBooking.BookingReference.Trim());
            oDB.DbDatabase.AddInParameter(oDB.DbCmd, "@dtStartDate", DbType.DateTime, objBooking.StartDate);
            oDB.DbDatabase.AddInParameter(oDB.DbCmd, "@dtEndDate", DbType.DateTime, objBooking.EndDate);
            oDB.DbDatabase.AddInParameter(oDB.DbCmd, "@iAccomTypeId", DbType.Int32, objBooking.AccomodationTypeId);
            oDB.DbDatabase.AddInParameter(oDB.DbCmd, "@iAccomId", DbType.Int32, objBooking.AccomodationId);
            oDB.DbDatabase.AddInParameter(oDB.DbCmd, "@iAgentId", DbType.Int32, objBooking.AgentId);
            oDB.DbDatabase.AddInParameter(oDB.DbCmd, "@iNights", DbType.Int32, objBooking.NoOfNights);
            oDB.DbDatabase.AddInParameter(oDB.DbCmd, "@iPersons", DbType.Int32, objBooking.NoOfPersons);
            oDB.DbDatabase.AddInParameter(oDB.DbCmd, "@BookingStatusId", DbType.Int32, objBooking.BookingStatusId);
            oDB.DbDatabase.AddInParameter(oDB.DbCmd, "@SeriesId", DbType.Int32, objBooking.SeriesId);
            oDB.DbDatabase.AddInParameter(oDB.DbCmd, "@proposedBooking", DbType.Boolean, objBooking.ProposedBooking);
            oDB.DbDatabase.AddInParameter(oDB.DbCmd, "@chartered", DbType.Boolean, objBooking.Chartered);


            iBookingID = Convert.ToInt32(oDB.ExecuteScalar(oDB.DbCmd));
            BookingId = iBookingID;
        }
        catch (Exception exp)
        {
            objBooking = null;
            oDB = null;
            GF.LogError("clsBookingHandler.AddBooking", exp.Message);
            BookingId = 0;
            return false;
        }
        finally
        {
            objBooking = null;
            oDB = null;
        }
        return true;
    }
    private void DeleteBooking()
    {
        if (base.ValidateIfCommandAllowed(Request.Url.ToString(), ENums.PageCommand.Delete))
        {
            BookingServices oBookingManager;
            oBookingManager = new BookingServices();
            oBookingManager.DeleteBooking(iBookingId);
        }
    }

    private void CancelBooking()
    {
        if (base.ValidateIfCommandAllowed(Request.Url.ToString(), ENums.PageCommand.Cancel))
        {
            BookingServices oBookingManager;
            oBookingManager = new BookingServices();
            oBookingManager.CancelBooking(iBookingId);
        }
    }

    private BookingWaitListDTO[] GetWaitlistedRooms(int BookingId, int AccomId, out bool RoomsAreWaitlisted)
    {
        BookingWaitListDTO[] oBookingWaitListDTO = null;
        BookedRooms[] TotalRooms = null;
        string sPrevCategory = "", sPrevType = "";
        SortedList slWaitListedRoom = new SortedList();
        string key = "", val = "";
        string[] sCatType = null;
        int iTotalWaitListedRooms = 0, iCounter = 0;

        TotalRooms = GetRoomObjectFromSession();
        oBookingWaitListDTO = new BookingWaitListDTO[TotalRooms.Length];

        try
        {
            #region Generating the list of Rooms waitlisted, Category and Type Wise
            for (int i = 0; i < TotalRooms.Length; i++)
            {
                if (TotalRooms[i] != null)
                {
                    if (TotalRooms[i].RoomCategory != sPrevCategory || TotalRooms[i].RoomType != sPrevType)
                    {
                        iTotalWaitListedRooms = 0;
                        key = TotalRooms[i].RoomCategoryId.ToString() + "~" + TotalRooms[i].RoomTypeId.ToString();
                        slWaitListedRoom.Add(key, iTotalWaitListedRooms);
                        sPrevCategory = TotalRooms[i].RoomCategory;
                        sPrevType = TotalRooms[i].RoomType;
                    }
                    if (TotalRooms[i].BookingId == BookingId)
                    {
                        if (TotalRooms[i].RoomStatus == Constants.WAITLISTED)
                        {
                            iTotalWaitListedRooms++;
                            slWaitListedRoom[key] = iTotalWaitListedRooms;
                        }
                    }
                }
            }
            #endregion Generating the list of Rooms waitlisted, Category and Type Wise

            #region Populating the Object with the Rooms waitlisted
            iCounter = 0;
            iTotalWaitListedRooms = 0;
            key = ""; val = "";
            if (slWaitListedRoom != null)
            {
                for (int j = 0; j < slWaitListedRoom.Count; j++)
                {
                    key = slWaitListedRoom.GetKey(j).ToString();
                    val = slWaitListedRoom[key].ToString();

                    int.TryParse(val, out iTotalWaitListedRooms);
                    if (iTotalWaitListedRooms > 0)
                    {
                        sCatType = key.Split('~');
                        oBookingWaitListDTO[iCounter] = new BookingWaitListDTO();
                        oBookingWaitListDTO[iCounter].BookingId = BookingId;
                        oBookingWaitListDTO[iCounter].AccomId = AccomId;
                        oBookingWaitListDTO[iCounter].RoomCategoryId = Convert.ToInt32(sCatType[0].ToString());
                        oBookingWaitListDTO[iCounter].RoomTypeId = Convert.ToInt32(sCatType[1].ToString());
                        oBookingWaitListDTO[iCounter].No_Of_RoomsWaitListed = iTotalWaitListedRooms;
                        iCounter++;
                        key = ""; val = "";
                    }
                }
            }
            #endregion Populating the Object with the Rooms waitlisted
        }
        catch (Exception exp)
        {
            GF.LogError("ClientUI_Booking.GetWaitlistedRooms", exp.Message + " Time : " + DateTime.Now.ToString());
            RoomsAreWaitlisted = false;
            return null;
        }
        if (iCounter > 0)
            RoomsAreWaitlisted = true;
        else
            RoomsAreWaitlisted = false;
        //SessionHandler"SLB"] = null;
        return oBookingWaitListDTO;
    }

    private void GetFinalizedRooms(int BookingID, int AccomID, out List<BookedRooms> BookedRooms, out List<BookingWaitListDTO> BookingWaitListDTO)
    {
        int iAccomID = Convert.ToInt32(Request.Form["ddlAccomName"]);
        string[] sFormControls = Request.Form.AllKeys;
        BookedRooms[] oAllRooms = GetRoomObjectFromSession();

        List<BookedRooms> RoomsBookedList = new List<BookedRooms>();
        List<BookingWaitListDTO> RoomsWaitlistList = new List<BookingWaitListDTO>();

        BookedRooms oFinallyBookedRooms = null;
        BookingWaitListDTO oFinallyWaitListed = null;
        SortedList slFinalizedRooms = null;
        SortedList slWaitListed = new SortedList();
        string[] Idsplit = null;
        string Id = string.Empty;

        string Cat, Type, RoomNo;

        #region Get Selected Checkbox
        slFinalizedRooms = new SortedList();
        for (int i = 0; i < sFormControls.Length; i++)
        {
            if (sFormControls[i].StartsWith(Constants.CHECKBOX_ROOM_NO))
            {
                slFinalizedRooms.Add(sFormControls[i].ToString(), i);
            }
        }
        #endregion

        #region Selected Rooms
        for (int i = 0; i < slFinalizedRooms.Count; i++)
        {
            Id = Convert.ToString(slFinalizedRooms.GetKey(i));
            Idsplit = Id.Split('*');
            Cat = Idsplit[1];
            Type = Idsplit[2];
            RoomNo = Idsplit[3];
            for (int j = 0; j < oAllRooms.Length; j++)
            {
                if (oAllRooms[j].BookingId == BookingID && GF.ReplaceSpace(oAllRooms[j].RoomCategory) == Cat && oAllRooms[j].RoomType == Type && oAllRooms[j].RoomNo == RoomNo)
                {
                    #region Get Booked Rooms
                    if (oAllRooms[j].RoomStatus == Constants.BOOKED)
                    {
                        oFinallyBookedRooms = new BookedRooms();
                        oFinallyBookedRooms.BookingId = BookingID;
                        oFinallyBookedRooms.AccomodationId = AccomID;
                        oFinallyBookedRooms.RoomNo = oAllRooms[j].RoomNo;
                        oFinallyBookedRooms.StartDate = GF.ParseDate(txtStartDate.Text.ToString());
                        oFinallyBookedRooms.EndDate = GF.ParseDate(txtEndDate.Text.ToString());
                        oFinallyBookedRooms.PaxStaying = oAllRooms[j].PaxStaying != 0 ? oAllRooms[j].PaxStaying : oAllRooms[j].DefaultNoOfBeds;
                        oFinallyBookedRooms.ConvertTo_Double_Twin = oAllRooms[j].ConvertTo_Double_Twin;
                        oFinallyBookedRooms.RoomStatus = oAllRooms[j].RoomStatus;
                        oFinallyBookedRooms.RoomCategoryId = oAllRooms[j].RoomCategoryId;
                        try
                        {
                            if (txtCharterrates.Text != "")
                            {
                                oFinallyBookedRooms.Amount = Convert.ToDouble(txtCharterrates.Text.Trim());
                            }
                            else
                            {
                                oFinallyBookedRooms.Amount = Convert.ToDouble(txtTotalAmount.Text.Trim());
                            }
                            if (oAllRooms[j].PaymentId != null)
                            {
                                oFinallyBookedRooms.PaymentId = oAllRooms[j].PaymentId;
                            }
                            else
                            {
                                oFinallyBookedRooms.PaymentId = "DR" + DateTime.Now.ToString("MMddhhmmssfff");
                            }

                            oFinallyBookedRooms.Paid = oAllRooms[j].Paid;
                            oFinallyBookedRooms.action = oAllRooms[j].action;
                            oFinallyBookedRooms.Price = oAllRooms[j].Price;
                            oFinallyBookedRooms.taxableprice = 0;
                            oFinallyBookedRooms.tax = 0;
                            oFinallyBookedRooms.taxamount = 0;
                            oFinallyBookedRooms.Discount = 0;
                            oFinallyBookedRooms.DiscountPrice = 0;
                        }
                        catch
                        {
                        }


                        RoomsBookedList.Add(oFinallyBookedRooms);
                        break;
                    }
                    #endregion
                    #region Get WaitListed Rooms
                    else if (oAllRooms[j].RoomStatus == Constants.WAITLISTED)
                    {
                        string Key = oAllRooms[j].RoomCategory + "*" + oAllRooms[j].RoomType;
                        if (slWaitListed.ContainsKey(Key))
                        {
                            oFinallyWaitListed = (BookingWaitListDTO)slWaitListed[Key];
                            oFinallyWaitListed.No_Of_RoomsWaitListed++;
                            //oFinallyWaitListed.paxstying = oAllRooms[j].PaxStaying != 0 ? oAllRooms[j].PaxStaying : oAllRooms[j].DefaultNoOfBeds;
                        }
                        else
                        {
                            oFinallyWaitListed = new BookingWaitListDTO();
                            oFinallyWaitListed.BookingId = BookingID;
                            oFinallyWaitListed.AccomId = AccomID;
                            oFinallyWaitListed.RoomCategoryId = oAllRooms[j].RoomCategoryId;
                            oFinallyWaitListed.RoomTypeId = oAllRooms[j].RoomTypeId;
                            oFinallyWaitListed.No_Of_RoomsWaitListed = 1;
                            oFinallyWaitListed.paxstying = oAllRooms[j].PaxStaying;
                            slWaitListed.Add(Key, oFinallyWaitListed);
                            break;
                        }
                    }
                    #endregion
                }
            }
        }
        #endregion

        #region Set Rooms from Sorted List to Waitlisted Object
        for (int k = 0; k < slWaitListed.Count; k++)
        {
            RoomsWaitlistList.Add((BookingWaitListDTO)slWaitListed.GetByIndex(k));
        }
        #endregion

        BookedRooms = RoomsBookedList;
        BookingWaitListDTO = RoomsWaitlistList;
    }

    protected override Control FindControl(Control c, string ID)
    {
        try
        {
            Control cntrl = null;
            if (c != null)
            {
                if (c.HasControls() == false)
                {
                    if (c.ID != null)
                    {
                        if (c.ID.Replace("ABchkRoomNo", "chkRoomNo") == ID)
                            return c;
                        else
                            return null;
                    }
                }
                else if (c.Controls.Count > 0)
                {
                    if (c.Controls[0].GetType().ToString() == "System.Web.UI.LiteralControl")
                    {
                        if (c.ID != null)
                        {
                            if (c.ID.Replace("ABchkRoomNo", "chkRoomNo") == ID)
                                return c;
                            else
                                return null;
                        }
                    }
                    else
                    {
                        for (int i = 0; i < c.Controls.Count; i++)
                        {
                            cntrl = FindControl(c.Controls[i], ID);
                            if (cntrl != null)
                            {
                                break;
                            }
                        }
                    }
                }
            }
            return cntrl;
        }
        catch
        {
            return null;
        }


    }
    private void ClearSessionVariables()
    {
        SessionServices.DeleteSession(Constants._Booking_BookingId);
        SessionServices.DeleteSession(Constants._Booking_TotalNights);
        SessionServices.DeleteSession(Constants._Booking_RoomsStatus);
    }

    public double CalcaulateRates(int roomCatId, int roomtypeid, int pax)
    {
        try
        {

            Returndt = new DataTable();
            Returndt = ViewState["Rrate"] as DataTable;
            dv = new DataView();

            if (ddlAccomName.SelectedValue != "7")
            {
                dv = new DataView(Returndt, "RoomCategoryId='" + roomCatId + "' and RoomTypeId='" + roomtypeid + "'", "RoomCategoryId,RoomTypeId", DataViewRowState.CurrentRows);


                total = (Convert.ToInt32(txtNoOfNights.Text.Trim()) * Convert.ToDouble(dv.ToTable().Rows[0]["Amt"]));


            }
            else
            {
                dv = new DataView(Returndt, "roomcategoryid='" + roomCatId + "'", "RoomCategoryId", DataViewRowState.CurrentRows);
                string[] arr = { };
                if (pax < 2)
                {



                    string TaxStatus = (dv.ToTable().Rows[0][7].ToString());
                    string TaxValue = (dv.ToTable().Rows[0][6].ToString());
                    if (TaxStatus == "Tax Applied")
                    {
                        arr = dv.ToTable().Rows[0][5].ToString().Split(' ');




                    }
                    else if (TaxStatus == "Not Applied")
                    {
                        // arr = dv.ToTable().Rows[0][2].ToString().Split(' ');
                        arr = dv.ToTable().Rows[0][5].ToString().Split(' ');
                    }

                }
                else
                {
                    string TaxStatus = (dv.ToTable().Rows[0][7].ToString());
                    string TaxValue = (dv.ToTable().Rows[0][6].ToString());
                    if (TaxStatus == "Tax Applied")
                    {
                        arr = dv.ToTable().Rows[0][4].ToString().Split(' ');




                    }
                    else if (TaxStatus == "Not Applied")
                    {
                        // arr = dv.ToTable().Rows[0][1].ToString().Split(' ');
                        arr = dv.ToTable().Rows[0][4].ToString().Split(' ');
                    }
                }

                total = Convert.ToDouble(arr[1]) * Convert.ToDouble(pax);
            }
            return total;
        }
        catch (Exception ex)
        {
            return 0;
        }
    }

    private void SetRoomStatus(Table ParentControl)
    {
        BookedRooms[] oAllRooms = GetRoomObjectFromSession();
        RoomCategoryTypeRoomStatusCountDTO oRCTRSCDTO = null;

        SortedList slRooms = new SortedList();
        SortedList slCatTypeRooms = new SortedList();
        SortedList slBookedWithThisId = new SortedList();
        SortedList slRoomCount = new SortedList();
        Control c = null;
        HtmlGenericControl div = null;
        CheckBox chk = null;
        string Rooms;
        string[] RoomSplitter;

        char RoomStatus;
        char maintainstatus;
        double itotalAmt = 0;

        string Key = string.Empty, CatTypeKey = string.Empty, divId = string.Empty, chkId = string.Empty, Color = string.Empty;
        bool Checked = false;
        int iTotalPax = 0, TotalPaxforPrice = 0;

        #region Prepare Room List and thier respective Status
        for (int i = 0; i < oAllRooms.Length; i++)
        {
            Key = GF.ReplaceSpace(oAllRooms[i].RoomCategory) + "*" + GF.ReplaceSpace(oAllRooms[i].RoomType) + "*" + GF.ReplaceSpace(oAllRooms[i].RoomNo);
            CatTypeKey = oAllRooms[i].RoomCategoryId + "*" + oAllRooms[i].RoomTypeId;

            #region Count of Rooms for the Category_Type
            if (!slRoomCount.ContainsKey(CatTypeKey))
            {
                slRoomCount.Add(CatTypeKey, oAllRooms[i].RoomNo + ";");
            }
            else
            {
                Rooms = Convert.ToString(slRoomCount[CatTypeKey]);
                if (!Rooms.Contains(oAllRooms[i].RoomNo))
                    slRoomCount[CatTypeKey] = Rooms + oAllRooms[i].RoomNo + ";";
            }

            if (!slCatTypeRooms.ContainsKey(CatTypeKey))
            {
                oRCTRSCDTO = new RoomCategoryTypeRoomStatusCountDTO();
                oRCTRSCDTO.RoomCategoryId = oAllRooms[i].RoomCategoryId;
                oRCTRSCDTO.RoomCategory = oAllRooms[i].RoomCategory;
                oRCTRSCDTO.RoomTypeId = oAllRooms[i].RoomTypeId;
                oRCTRSCDTO.RoomType = oAllRooms[i].RoomType;
                //oRCTRSCDTO.TotalRooms = 1;                
            }
            else
            {
                oRCTRSCDTO = (RoomCategoryTypeRoomStatusCountDTO)slCatTypeRooms[CatTypeKey];
            }
            #endregion Count of Rooms for the Category_Type

            #region If this is a existing booking and room is in avialable state.
            if (oAllRooms[i].BookingId == 0 && iBookingId != 0) //If this is a new booking.
            {
                if (oAllRooms[i].RoomStatus == Constants.AVAILABLE)
                {
                    RoomStatus = oAllRooms[i].RoomStatus;
                    if (oAllRooms[i].PrevBookingId == iBookingId && iBookingId != 0)
                        RoomStatus = Constants.BOOKED_EARLIER;
                    if (!slRooms.ContainsKey(Key))
                        slRooms.Add(Key, RoomStatus);
                    else
                        slRooms[Key] = RoomStatus;
                }
                else if (oAllRooms[i].RoomStatus == Constants.Maintainence)
                {
                    RoomStatus = oAllRooms[i].RoomStatus;

                    if (!slRooms.ContainsKey(Key))
                        slRooms.Add(Key, RoomStatus);
                    else
                        slRooms[Key] = RoomStatus;
                }
            }
            #endregion

            #region If this is a new booking.
            if (oAllRooms[i].BookingId == 0 && iBookingId == 0)
            {

                if (oAllRooms[i].RoomStatus == Constants.BOOKED)
                {
                    RoomStatus = oAllRooms[i].RoomStatus;


                    if (!slRooms.ContainsKey(Key))
                        slRooms.Add(Key, RoomStatus);
                    else
                        slRooms[Key] = RoomStatus;
                    if (!slBookedWithThisId.ContainsKey(oAllRooms[i].RoomNo))
                        slBookedWithThisId.Add(oAllRooms[i].RoomNo, null);
                    oRCTRSCDTO.Booked = oRCTRSCDTO.Booked + 1;
                    if (oAllRooms[i].PaxStaying <= 0)
                    {
                        oAllRooms[i].PaxStaying = oAllRooms[i].DefaultNoOfBeds;
                    }





                    oRCTRSCDTO.TotalPax = oRCTRSCDTO.TotalPax + oAllRooms[i].PaxStaying;
                    try
                    {
                        if (ddlAccomName.SelectedValue == "7")
                        {

                            bindRoomRatesCruise(oRCTRSCDTO.TotalPax);
                        }
                        else
                        {
                            if (oAllRooms[i].ConvertTo_Double_Twin == true)
                            {
                                bindRoomRates(Convert.ToInt32(ddlAccomName.SelectedValue), oRCTRSCDTO.TotalPax, Convert.ToInt32(ddlAgentType.SelectedValue), Convert.ToDateTime(txtStartDate.Text.Trim()), Convert.ToDateTime(txtEndDate.Text.Trim()), oRCTRSCDTO.Booked, 2);
                            }
                            else
                            {
                                bindRoomRates(Convert.ToInt32(ddlAccomName.SelectedValue), oRCTRSCDTO.TotalPax, Convert.ToInt32(ddlAgentType.SelectedValue), Convert.ToDateTime(txtStartDate.Text.Trim()), Convert.ToDateTime(txtEndDate.Text.Trim()), oRCTRSCDTO.Booked, oAllRooms[i].RoomTypeId);
                            }
                        }

                        if (i > 0)
                        {
                            if (oAllRooms[i].RoomCategory != oAllRooms[i - 1].RoomCategory)
                            {

                                total = 0;
                            }
                        }
                        else
                        {

                            total = 0;
                        }
                        oAllRooms[i].Price = CalcaulateRates(oAllRooms[i].RoomCategoryId, oAllRooms[i].RoomTypeId, oAllRooms[i].PaxStaying);
                        oAllRooms[i].action = "AddPriceDetailsToo";
                        oAllRooms[i].PaymentId = "DR" + DateTime.Now.ToString("MMddhhmmssfff");


                        //   itotalAmt = itotalAmt + oBR[index].Price;

                    }
                    catch
                    {

                    }



                    itotalAmt = itotalAmt + oAllRooms[i].Price;
                    oRCTRSCDTO.TotalPriceCategory = oRCTRSCDTO.TotalPriceCategory + oAllRooms[i].Price;

                    txtTotalAmount.Text = itotalAmt.ToString();
                }
                else if (oAllRooms[i].RoomStatus == Constants.WAITLISTED)
                {
                    RoomStatus = oAllRooms[i].RoomStatus;
                    if (!slRooms.ContainsKey(Key))
                        slRooms.Add(Key, RoomStatus);
                    else
                        slRooms[Key] = RoomStatus;
                    if (!slBookedWithThisId.ContainsKey(oAllRooms[i].RoomNo))
                        slBookedWithThisId.Add(oAllRooms[i].RoomNo, null);
                    oRCTRSCDTO.WaitListed = oRCTRSCDTO.WaitListed + 1;
                    if (oAllRooms[i].PaxStaying <= 0)
                    {
                        oAllRooms[i].PaxStaying = oAllRooms[i].DefaultNoOfBeds;
                    }





                    oRCTRSCDTO.TotalPax = oRCTRSCDTO.TotalPax + oAllRooms[i].PaxStaying;
                }
                else if (oAllRooms[i].RoomStatus == Constants.AVAILABLE) //This shall not be working
                {
                    RoomStatus = oAllRooms[i].RoomStatus;
                    if (!slRooms.ContainsKey(Key))
                        slRooms.Add(Key, RoomStatus);
                    else
                        slRooms[Key] = RoomStatus;
                    //if (!slBookedWithThisId.ContainsKey(oAllRooms[i].RoomNo))
                    //  slBookedWithThisId.Add(oAllRooms[i].RoomNo, null);
                }

                else if (oAllRooms[i].RoomStatus == Constants.Maintainence)
                {


                    RoomStatus = oAllRooms[i].RoomStatus;
                    if (!slRooms.ContainsKey(Key))
                        slRooms.Add(Key, RoomStatus);
                    else
                        slRooms[Key] = RoomStatus;
                    if (!slBookedWithThisId.ContainsKey(oAllRooms[i].RoomNo))
                        slBookedWithThisId.Add(oAllRooms[i].RoomNo, null);
                    oRCTRSCDTO.Maintained = oRCTRSCDTO.Maintained + 1;

                }
            }
            #endregion

            #region This Booking ID
            if (oAllRooms[i].BookingId == iBookingId)
            {

                if (oAllRooms[i].BookingId != 0)
                {
                    if (!slBookedWithThisId.ContainsKey(oAllRooms[i].RoomNo))
                        slBookedWithThisId.Add(oAllRooms[i].RoomNo, null);

                    RoomStatus = oAllRooms[i].RoomStatus;
                    if (oAllRooms[i].PrevRoomStatus == Constants.AVAILABLE)
                        RoomStatus = Constants.BOOKED_NOW;

                    if (slRooms.ContainsKey(Key))
                        slRooms[Key] = RoomStatus;
                    else
                        slRooms.Add(Key, RoomStatus);

                    if (oAllRooms[i].RoomStatus == Constants.BOOKED)
                    {
                        if (oAllRooms[i].PaxStaying <= 0)
                        {
                            oAllRooms[i].PaxStaying = oAllRooms[i].DefaultNoOfBeds;
                        }



                        oRCTRSCDTO.Booked = oRCTRSCDTO.Booked + 1;
                        oRCTRSCDTO.TotalPax = oRCTRSCDTO.TotalPax + oAllRooms[i].PaxStaying;

                        oRCTRSCDTO.TotalPriceCategory = oRCTRSCDTO.TotalPriceCategory + oAllRooms[i].Price;
                        itotalAmt = itotalAmt + oAllRooms[i].Price;
                        txtTotalAmount.Text = itotalAmt.ToString();

                    }
                    else if (oAllRooms[i].RoomStatus == Constants.WAITLISTED)
                    {
                        oRCTRSCDTO.WaitListed = oRCTRSCDTO.WaitListed + 1;
                        //Currently Setting The Pax for the waitlisted room is not allowed, needs to be discussed.
                        //oRCTRSCDTO.TotalPax = oRCTRSCDTO.TotalPax + oAllRooms[i].PaxStaying == 0 ? oAllRooms[i].DefaultNoOfBeds : oAllRooms[i].PaxStaying;
                    }
                }



            }

            else if (oAllRooms[i].RoomStatus == Constants.Maintainence && iBookingId != 0)
            {
                oRCTRSCDTO.Maintained = oRCTRSCDTO.Maintained + 1;
                //Currently Setting The Pax for the waitlisted room is not allowed, needs to be discussed.
                //oRCTRSCDTO.TotalPax = oRCTRSCDTO.TotalPax + oAllRooms[i].PaxStaying == 0 ? oAllRooms[i].DefaultNoOfBeds : oAllRooms[i].PaxStaying;
            }
            #endregion

            #region Other Booking ID
            if (oAllRooms[i].BookingId != iBookingId && oAllRooms[i].BookingId != 0)
            {
                //IF the Room is booked with any other Booking then remove it from the                 
                //I am Doing this because I am expecting that if this room is booked wiht any other Id
                //Then that ID will override this and make it waitlidted or Booked, other wise, 
                //this room is available.
                if (!slBookedWithThisId.ContainsKey(oAllRooms[i].RoomNo))
                {
                    if (oAllRooms[i].RoomStatus == Constants.BOOKED)
                    {
                        if (!slRooms.ContainsKey(Key))
                        {
                            slRooms.Add(Key, Constants.WAITLISTED_WITH_OTHER_BOOKING);
                        }
                        else
                        {
                            slRooms[Key] = Constants.WAITLISTED_WITH_OTHER_BOOKING;
                        }
                    }
                    else if (oAllRooms[i].RoomStatus == Constants.WAITLISTED)
                    {
                        if (!slRooms.ContainsKey(Key))
                        {
                            slRooms.Add(Key, Constants.AVAILABLE);
                        }
                        else
                        {
                            RoomStatus = Convert.ToChar(slRooms[Key]);
                            if (RoomStatus != Constants.BOOKED_EARLIER && RoomStatus != Constants.WAITLISTED_WITH_OTHER_BOOKING)
                                slRooms[Key] = Constants.AVAILABLE;

                            /*slRooms[Key] = Constants.BOOKED_EARLIER;
                            if (Convert.ToChar(slRooms[Key]) != Constants.WAITLISTED_WITH_OTHER_BOOKING)*/
                        }
                    }
                    else if (oAllRooms[i].RoomStatus == Constants.AVAILABLE)
                    {
                        if (!slRooms.ContainsKey(Key))
                        {
                            slRooms.Add(Key, Constants.AVAILABLE);
                        }
                    }

                    else if (oAllRooms[i].RoomStatus == Constants.Maintainence)
                    {
                        if (!slRooms.ContainsKey(Key))
                        {
                            slRooms.Add(Key, Constants.Maintainence);
                        }
                    }

                }
            }
            #endregion
            slCatTypeRooms[CatTypeKey] = oRCTRSCDTO;
        }
        #endregion Prepare Room List and thier respective Status

        #region Set Check box and  Color Codes






        if (slRooms != null)
        {






            for (int i = 0; i < slRooms.Count; i++)
            {
                Key = Convert.ToString(slRooms.GetKey(i));
                RoomStatus = Convert.ToChar(slRooms.GetByIndex(i));
                divId = Constants.DIV_ROOMNO + "*" + Key;
                chkId = Constants.CHECKBOX_ROOM_NO + Key;



                c = FindControl(ParentControl, divId);
                if (c != null)
                {
                    div = (HtmlGenericControl)c;
                }

                c = FindControl(ParentControl, chkId);
                if (c != null)
                {
                    chk = (CheckBox)c;
                }



                if (RoomStatus == Constants.AVAILABLE)
                {
                    if (div != null)
                        Color = "Green";
                    if (chk != null)
                        Checked = false;
                }
                else if (RoomStatus == Constants.BOOKED)
                {
                    if (div != null)
                        Color = "Blue";
                    if (chk != null)
                        Checked = true;
                }
                else if (RoomStatus == Constants.BOOKED_NOW)
                {
                    if (div != null)
                        Color = "Green";
                    if (chk != null)
                        Checked = true;
                }
                else if (RoomStatus == Constants.BOOKED_EARLIER)
                {
                    if (div != null)
                        Color = "Blue";
                    if (chk != null)
                        Checked = false;
                }
                else if (RoomStatus == Constants.WAITLISTED)
                {
                    if (div != null)
                        Color = "Red";
                    if (chk != null)
                        Checked = true;
                }
                else if (RoomStatus == Constants.WAITLISTED_WITH_OTHER_BOOKING)
                {
                    if (div != null)
                        Color = "Red";
                    if (chk != null)
                        Checked = false;
                }

                else if (RoomStatus == Constants.Maintainence)
                {



                    if (div != null)
                        Color = "grey";
                    if (chk != null)
                        Checked = false;


                }
                if (div != null)
                    div.Style[HtmlTextWriterStyle.Color] = Color;
                if (chk != null)
                    chk.Checked = Checked;
            }
        }
        #endregion

        #region Setting the Total Rooms
        if (slRoomCount != null)
        {
            for (int i = 0; i < slRoomCount.Count; i++)
            {
                Key = Convert.ToString(slRoomCount.GetKey(i));
                Rooms = Convert.ToString(slRoomCount[Key]);

                if (slCatTypeRooms.ContainsKey(Key))
                {
                    RoomSplitter = Rooms.Split(';');
                    oRCTRSCDTO = new RoomCategoryTypeRoomStatusCountDTO();
                    oRCTRSCDTO = (RoomCategoryTypeRoomStatusCountDTO)slCatTypeRooms[Key];

                    oRCTRSCDTO.TotalRooms = RoomSplitter.Length - 1;

                    slCatTypeRooms.Remove(Key);
                    slCatTypeRooms.Add(Key, oRCTRSCDTO);
                }
            }
        }
        #endregion Setting the Total Rooms

        #region Setting the Labels
        if (slCatTypeRooms != null)
        {
            for (int i = 0; i < slCatTypeRooms.Count; i++)
            {
                oRCTRSCDTO = (RoomCategoryTypeRoomStatusCountDTO)slCatTypeRooms.GetByIndex(i);

                if (chkChartered.Checked)
                {
                    SetTotalRooms(ParentControl, oRCTRSCDTO.Booked, 0, oRCTRSCDTO.WaitListed, oRCTRSCDTO.RoomCategory, oRCTRSCDTO.RoomType, oRCTRSCDTO.TotalRooms);
                }
                else
                {
                    SetTotalRooms(ParentControl, oRCTRSCDTO.Booked, 0, oRCTRSCDTO.WaitListed, oRCTRSCDTO.RoomCategory, oRCTRSCDTO.RoomType, (oRCTRSCDTO.TotalRooms - oRCTRSCDTO.Maintained));
                }

                SetRoomsWaitlistedLabel(ParentControl, oRCTRSCDTO.WaitListed, oRCTRSCDTO.RoomCategory, oRCTRSCDTO.RoomType);
                SetTotalRoomsBookedLabel(ParentControl, oRCTRSCDTO.Booked, oRCTRSCDTO.RoomCategory, oRCTRSCDTO.RoomType);
                SetRoomsPaxLabel(ParentControl, oRCTRSCDTO.TotalPax, oRCTRSCDTO.RoomCategory, oRCTRSCDTO.RoomType);
                iTotalPax = iTotalPax + oRCTRSCDTO.TotalPax;

                // CalcaulateRates(oRCTRSCDTO.RoomCategoryId, oRCTRSCDTO.RoomTypeId, oRCTRSCDTO.TotalPax);


                SetRoomsTotalPriceLabel(ParentControl, oRCTRSCDTO.TotalPriceCategory, oRCTRSCDTO.RoomCategory, oRCTRSCDTO.RoomType);
            }
        }
        SetBookingTotalPax(iTotalPax);
        #endregion
    }
    #endregion Helper Methods
    private void loadpackage()
    {
        if (txtStartDate.Text != "" && txtEndDate.Text != "")
        {
            TimeSpan period = Convert.ToDateTime(txtEndDate.Text) - Convert.ToDateTime(txtStartDate.Text);
            int days = period.Days;
            bpm._Action = "Getpackagebydates";
            //bpm._NoOfNights = days;
            bpm.Checkin = Convert.ToDateTime(txtStartDate.Text).Date;
            bpm.checkout = Convert.ToDateTime(txtEndDate.Text).Date;
            DataTable dt = dpm.Getpackagebydates(bpm);
            ddlpackage.Items.Clear();

            ddlpackage.DataSource = dt;
            ddlpackage.DataValueField = "packageId";
            ddlpackage.DataTextField = "PackageName";
            ddlpackage.DataBind();
            ddlpackage.Items.Insert(0, "Select Package");
            if (Session["getpackegforedit"] != null)
            {
                ddlpackage.SelectedValue = Session["getpackegforedit"].ToString();
                Session["getpackegforedit"] = null;
            }
            if (dt != null && dt.Rows.Count > 0)
            {
                for (int i = 0; i < ddlpackage.Items.Count; i++)
                {
                    if (i == 0)
                    {
                        ListItem item = ddlpackage.Items[i];
                        item.Attributes["NooOfNights"] = "0";
                    }
                    else
                    {
                        ListItem item = ddlpackage.Items[i];
                        item.Attributes["NooOfNights"] = dt.Rows[i - 1]["NoOfNights"].ToString();
                    }
                }
            }
            StringBuilder builder = new StringBuilder();
            if (dt != null && dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    builder.Append(dt.Rows[i]["PackageName"].ToString()).Append("<br/>");
                }


            }
            lblPackages.Text = builder.ToString();
        }
    }
    protected void ddlAccomName_SelectedIndexChanged(object sender, EventArgs e)
    {

        //...
        if (txtStartDate.Text != "" && txtEndDate.Text != "")
        {
            TimeSpan period = Convert.ToDateTime(txtEndDate.Text) - Convert.ToDateTime(txtStartDate.Text);
            int days = period.Days;
            bpm._Action = "Getpackagebydates";
            //bpm._NoOfNights = days;
            bpm.Checkin = Convert.ToDateTime(txtStartDate.Text).Date;
            bpm.checkout = Convert.ToDateTime(txtEndDate.Text).Date;
            DataTable dt = dpm.Getpackagebydates(bpm);
            ddlpackage.Items.Clear();

            ddlpackage.DataSource = dt;
            ddlpackage.DataValueField = "packageId";
            ddlpackage.DataTextField = "PackageName";
            ddlpackage.DataBind();
            ddlpackage.Items.Insert(0, "Select Package");


            if (dt != null && dt.Rows.Count > 0)
            {
                for (int i = 0; i < ddlpackage.Items.Count; i++)
                {
                    if (i == 0)
                    {
                        ListItem item = ddlpackage.Items[i];
                        item.Attributes["NooOfNights"] = "0";
                    }
                    else
                    {
                        ListItem item = ddlpackage.Items[i];
                        item.Attributes["NooOfNights"] = dt.Rows[i - 1]["NoOfNights"].ToString();
                    }
                }
            }

            StringBuilder builder = new StringBuilder();
            if (dt != null && dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    builder.Append(dt.Rows[i]["PackageName"].ToString()).Append("<br/>");
                }


            }
            lblPackages.Text = builder.ToString();
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Showstatus", "javascript:alert('Check in and check out can not be blank')", true);
            return;
        }

    }
    protected void ddlPackage_SelectedIndexChanged(object sender, EventArgs e)
    {
        DateTime StartDate = DateTime.MinValue;
        StartDate = Convert.ToDateTime(txtStartDate.Text);
        txtNoOfNights.Text = ddlpackage.SelectedItem.Attributes["nooofnights"];
        txtEndDate.Text = GF.GetDD_MMM_YYYY(StartDate.AddDays(Convert.ToDouble(txtNoOfNights.Text)), false);
    }


    protected void chkChartered_CheckedChanged(object sender, EventArgs e)
    {
        gdvRatesCruise.DataSource = null;
        gdvRatesHotel.DataSource = null;
        gdvRatesCruise.DataBind();
        gdvRatesHotel.DataBind();
        checkwaitlisted();
        DateTime sd, ed;
        int iAccomodationId;
        DateTime.TryParse(txtStartDate.Text, out sd);
        DateTime.TryParse(txtEndDate.Text, out ed);
        int.TryParse(ddlAccomName.SelectedValue.ToString(), out iAccomodationId);
        RemoveRoomObjectFromSession();
        if (chkChartered.Checked == true)
        {
            PrepareRoomChartpgload(sd, ed, iAccomodationId, true);
          //  PrepareRoomChart(sd, ed, iAccomodationId);
        }
        else
        {
        }
    }


}

public class AddRoomEventingTracker
{
    public string ControlName { get; set; }
    public int Value { get; set; }
}

