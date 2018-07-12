using FarHorizon.DataSecurity;
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
using FarHorizon.Reservations.BusinessServices.Online.BAL;
using FarHorizon.Reservations.BusinessServices.Online.DAL;
using FarHorizon.Reservations.MasterServices;
using FarHorizon.Reservations.Bases;
using FarHorizon.Reservations.Bases.BasePages;



public partial class Cruise_Booking_Touristentry : System.Web.UI.Page
{
    BALBooking blsr = new BALBooking();
    DALBooking dlsr = new DALBooking();
    int _bookingId;
    int _touristNo;
    int paxcount = 0;

    public int BookingId
    {
        get { return _bookingId; }
        set { _bookingId = value; }
    }

    public int TouristNo
    {
        get { return _touristNo; }
        set { _touristNo = value; }
    }

    protected void Page_Load(object sender, EventArgs e)

    {

        // txtPassportNo.Text = "VDIzaDQ1aXMxMjRJNTRz";
        //string PassportNo = DataSecurityManager.Decrypt(txtPassportNo.Text.ToString());
        //string t = "VDIzaDQ1aXMxMjRJNTRz";
        //string PassportNo = DataSecurityManager.Decrypt(t.ToString());

        if (Session["CustName"] != null)
        {
            navlogin.Visible = false;
            lblUsername.Text = Session["CustName"].ToString();
        }
        else
        {
           // Response.Redirect("http://test1.adventureresortscruises.in/Cruise/Booking/searchproperty1.aspx");
        }
        TouristServices touristServices = null;
        //AddAttributes();


        SessionServices.TouristDetails_BookingNo = BookingId;


        if (!IsPostBack)
        {
            if (Request.QueryString["bid"] != null)
                BookingId = Convert.ToInt32(Request.QueryString["bid"]);
            if (Request.QueryString["tno"] != null)
                TouristNo = Convert.ToInt32(Request.QueryString["tno"]);
            RefreshGrid(BookingId);
            if (Request.QueryString["bid"] != null)
                BookingId = Convert.ToInt32(Request.QueryString["bid"]);
            blsr._iBookingId = BookingId;
            blsr.action = "fetchbybookingId";
            DataTable dt = dlsr.fetchbybookingid(blsr);
            if (dt != null && dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    paxcount = paxcount + Convert.ToInt32(dt.Rows[i]["NoOfPersons"].ToString());
                }
                Session["getpaxcount"] = paxcount;
            }
            FillNationality();
            radEmpNo.Checked = true;
            if (Request.QueryString["op"] == "edit")
            {
                btnSubmit.Text = "Update";
                if (touristServices == null)
                    touristServices = new TouristServices();
                BookingTouristDTO oBTData = touristServices.GetBookingTouristDetails(BookingId, TouristNo);
                if (oBTData != null)
                    FillTouristDetails(oBTData);
                touristServices = null;
                oBTData = null;
            }
            else
            {
                //btnDelete.Visible = false;
            }
            //System.Web.UI.ScriptManager.GetCurrent(this).SetFocus(this.ddlSuffix);
            //Response.Redirect("http://test.adventureresortscruises.in/Cruise/Booking/NewRegister.aspx?bid=" + BookingId);
        }
    }
    private void FillNationality()
    {
        NationalityMaster oNationalityMaster = new NationalityMaster();
        NationalityDTO[] oNationalityData = oNationalityMaster.GetData();
        ddlNationality.Items.Clear();
        SortedList slNationalityMaster = new SortedList();
        slNationalityMaster.Add("0", "Choose Nationality");
        if (oNationalityData != null)
        {
            for (int i = 0; i < oNationalityData.Length; i++)
            {
                slNationalityMaster.Add(Convert.ToString(oNationalityData[i].NationalityId), Convert.ToString(oNationalityData[i].Nationality));
            }
            ddlNationality.DataSource = slNationalityMaster;
            ddlNationality.DataTextField = "value";
            ddlNationality.DataValueField = "key";
            ddlNationality.DataBind();
        }
        oNationalityData = null;
        oNationalityMaster = null;
    }
    private void FillTouristDetails(BookingTouristDTO oBookingTouristDTO)
    {
        ddlSuffix.Text = oBookingTouristDTO.Suffix;
        txtAllerges.Text = oBookingTouristDTO.Allergies.ToString();
        txtArrivaldate.Text = oBookingTouristDTO.ArrivalDateTime == DateTime.MinValue ? "" : GF.Handle19000101(oBookingTouristDTO.ArrivalDateTime, false);
        txtBirthPlace.Text = oBookingTouristDTO.PlaceofBirth.ToString();
        txtDOB.Text = oBookingTouristDTO.DateOfBirth == DateTime.MinValue ? "" : GF.Handle19000101(oBookingTouristDTO.DateOfBirth, false);
        txtFirstName.Text = oBookingTouristDTO.FirstName.ToString();
        txtmdlname.Text = oBookingTouristDTO.MiddleName.ToString();
        txtLName.Text = oBookingTouristDTO.LastName.ToString();
        txtEmailId.Text = oBookingTouristDTO.EmailId.ToString();
        ddlGender.SelectedValue = oBookingTouristDTO.Gender == '\0' ? "0" : oBookingTouristDTO.Gender.ToString();
        ddlNationality.Text = oBookingTouristDTO.NationalityId.ToString();
        txtMealPref.Text = oBookingTouristDTO.MealPreferences.ToString();
        txtMessage.Text = oBookingTouristDTO.SpecialMessage.ToString();
        txtPassportExpiryDate.Text = oBookingTouristDTO.PassportExpiryDate == DateTime.MinValue ? "" : GF.Handle19000101(oBookingTouristDTO.PassportExpiryDate, false);
        txtPassportIssueDate.Text = oBookingTouristDTO.PassportIssueDate == DateTime.MinValue ? "" : GF.Handle19000101(oBookingTouristDTO.PassportIssueDate, false);
        txtPassportNo.Text = oBookingTouristDTO.PassportNo.ToString();
        txtPermAdd.Text = oBookingTouristDTO.PermanentAddressInIndia.ToString();
        txtProStayInIndia.Text = String.IsNullOrEmpty(oBookingTouristDTO.ProposedStayInIndia) ? String.Empty : oBookingTouristDTO.ProposedStayInIndia;

        txtRoomDetails.Text = oBookingTouristDTO.RoomDetails.ToString();

        txtVisaExpiryDate.Text = oBookingTouristDTO.VisaExpiryDate == DateTime.MinValue ? "" : GF.Handle19000101(oBookingTouristDTO.VisaExpiryDate, false);
        txtVisaNo.Text = oBookingTouristDTO.VisaNo.ToString();
        txtVisitPurpose.Text = oBookingTouristDTO.VisitPurpose.ToString();
        txtIndiaEntryDate.Text = oBookingTouristDTO.IndiaEntryDate == DateTime.MinValue ? "" : GF.Handle19000101(oBookingTouristDTO.IndiaEntryDate, false);
        txtDeparturedate.Text = oBookingTouristDTO.departuredate == DateTime.MinValue ? "" : GF.Handle19000101(oBookingTouristDTO.departuredate, false);
        txtVehicalno.Text = oBookingTouristDTO.arrivalvehiaclno.ToString();
        txtDepartureVehicalno.Text = oBookingTouristDTO.departurevehicalno.ToString();
        txtarrivalairport.Text = oBookingTouristDTO.arrivalairport.ToString();
        txtDepairpot.Text = oBookingTouristDTO.departureairport.ToString();

    }
    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        Session.Abandon();
        Session.RemoveAll();
        Response.Redirect("http://test1.adventureresortscruises.in/Cruise/Booking/searchproperty1.aspx");

    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        if (ValidateValues() == true)
        {
            if (btnSubmit.Text == "Submit")
            {
                //if ( Convert.ToInt32(Session["getpaxcount"].ToString())>=coustsave)
                {
                    SaveTouristDetail();
                    clearall();
                    if (Request.QueryString["bid"] != null)
                        BookingId = Convert.ToInt32(Request.QueryString["bid"]);
                    RefreshGrid(BookingId);
                    System.Web.UI.ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "AlertBox", "alert('Data Saved Successfully.');", true);
                    return;
                    //coustsave = 2;
                }
                //lblErrorMsg.Text = "Tourist has been added";
            }
            else
            {
                UpdateTouristDetails();
                clearall();
                if (Request.QueryString["bid"] != null)
                    BookingId = Convert.ToInt32(Request.QueryString["bid"]);
                RefreshGrid(BookingId);
                System.Web.UI.ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "AlertBox", "alert('Data Upadete Successfully.');", true);
                return;
                //lblErrorMsg.Text = "Tourist has been updated";
            }
        }




    }
    private bool ValidateValues()
    {
        int result;
        if (txtFirstName.Text.Trim() == "")
        {
            lblErrorMsg.Text = "First name cannot be blank.";
            return false;
        }
        if (txtLName.Text.Trim() == "")
        {
            lblErrorMsg.Text = "Last name cannot be blank.";
            return false;
        }
        if (txtProStayInIndia.Text.Trim() != "")
        {
            if (Int32.TryParse(txtProStayInIndia.Text, out result) == false)
            {
                lblErrorMsg.Text = "Proposed stay india can be a numeric value only.";
                return false;
            }
        }
        if (string.IsNullOrEmpty(ddlNationality.SelectedValue.ToString()))
        {
            lblErrorMsg.Text = "Please select Nationality. This cannot be blank.";
            return false;
        }
        return true;
    }
    private void RefreshGrid(int iBookingId)
    {
        TouristServices touristServices = new TouristServices();
        BookingTouristDTO[] oBTData = touristServices.GetBookingTourists(iBookingId);


        GridRoomPaxDetail.DataSource = oBTData;
        GridRoomPaxDetail.DataBind();

    }
    private void SaveTouristDetail()
    {
        int touristNo;
        //if (base.ValidateIfCommandAllowed(Request.Url.AbsoluteUri, ENums.PageCommand.Add))
        //{
        TouristServices touristServices = new TouristServices();
        BookingTouristDTO oTouristDTO = new BookingTouristDTO();
        if (Request.QueryString["bid"] != null)
            BookingId = Convert.ToInt32(Request.QueryString["bid"]);
        oTouristDTO = SetDatatoObject();
        touristServices.AddBookingTouristentry(oTouristDTO, out touristNo);
        touristServices = null;
        //EmptyUncommonFields();
        //Response.Redirect("afterBookingTouristactions.aspx?bid=" + BookingId + "&tno=" + touristNo + "&tstatus=ins");
        //}
    }
    private void clearall()
    {
        txtAllerges.Text = "";
        txtArrivaldate.Text = "";
        txtBirthPlace.Text = "";
        txtDOB.Text = "";
        txtEmailId.Text = "";
        txtFirstName.Text = "";
        txtIndiaEntryDate.Text = "";
        txtLName.Text = "";
        txtmdlname.Text = "";
        txtMealPref.Text = "";
        txtMessage.Text = "";
        txtPassportExpiryDate.Text = "";
        txtPassportIssueDate.Text = "";
        txtPassportNo.Text = "";
        txtPermAdd.Text = "";
        txtProStayInIndia.Text = "";
        txtRoomDetails.Text = "";
        txtVisaExpiryDate.Text = "";
        txtVisaExpiryDate.Text = "";
        txtVisaNo.Text = "";
        txtVisitPurpose.Text = "";
        txtVehicalno.Text = "";
        txtDeparturedate.Text = "";
        txtDepartureVehicalno.Text = "";
        txtarrivalairport.Text = "";
        txtDepairpot.Text = "";
        txtarrivalairport.Text = "";
        txtDepairpot.Text = "";
        txtDeparturedate.Text = "";
        FillNationality();

    }
    private BookingTouristDTO SetDatatoObject()
    {
        DateTime result;
        BookingTouristDTO oTouristDTO = new BookingTouristDTO();

        oTouristDTO.BookingId = BookingId;
        oTouristDTO.TouristNo = TouristNo;
        oTouristDTO.Allergies = txtAllerges.Text.ToString();

        result = DateTime.MinValue;
        if (txtArrivaldate.Text.Trim() != "")
            DateTime.TryParse(txtArrivaldate.Text, out result);
        if (result != DateTime.MinValue && result != DateTime.MaxValue)
            oTouristDTO.ArrivalDateTime = result;

        result = DateTime.MinValue;
        if (txtDOB.Text.Trim() != "")
            DateTime.TryParse(txtDOB.Text, out result);
        if (result != DateTime.MinValue && result != DateTime.MaxValue)
            oTouristDTO.DateOfBirth = result;

        oTouristDTO.EmployedinIndia = true;
        oTouristDTO.Suffix = ddlSuffix.Text.ToString();
        oTouristDTO.departurevehicalno = txtDepartureVehicalno.Text.ToString();
        oTouristDTO.arrivalvehiaclno = txtVehicalno.Text.ToString();

        oTouristDTO.arrivalvehiaclno = txtVehicalno.Text.ToString();
        oTouristDTO.FirstName = txtFirstName.Text.ToString();
        oTouristDTO.MiddleName = txtmdlname.Text.ToString();
        oTouristDTO.Gender = Convert.ToChar(ddlGender.SelectedValue.ToString());
        oTouristDTO.EmailId = txtEmailId.Text.ToString();
        result = DateTime.MinValue;
        if (txtIndiaEntryDate.Text.Trim() != "")
            DateTime.TryParse(txtIndiaEntryDate.Text, out result);
        if (result != DateTime.MinValue && result != DateTime.MaxValue)
            oTouristDTO.IndiaEntryDate = result;

        oTouristDTO.LastName = txtLName.Text.ToString();
        oTouristDTO.MealPreferences = txtMealPref.Text.ToString();
        oTouristDTO.NationalityId = Convert.ToInt32(ddlNationality.SelectedValue.ToString());

        result = DateTime.MinValue;
        if (txtPassportExpiryDate.Text.Trim() != "")
            DateTime.TryParse(txtPassportExpiryDate.Text, out result);
        if (result != DateTime.MinValue && result != DateTime.MaxValue)
            oTouristDTO.PassportExpiryDate = result;
        result = DateTime.MinValue;
        if (txtDeparturedate.Text.ToString() != "")
            DateTime.TryParse(txtDeparturedate.Text, out result);
        if (result != DateTime.MinValue && result != DateTime.MaxValue)
            oTouristDTO.departuredate = result;

        result = DateTime.MinValue;
        if (txtPassportIssueDate.Text.Trim() != "")
            DateTime.TryParse(txtPassportIssueDate.Text, out result);
        if (result != DateTime.MinValue && result != DateTime.MaxValue)
            oTouristDTO.PassportIssueDate = result;

        oTouristDTO.PassportNo = txtPassportNo.Text.ToString();
        oTouristDTO.arrivalairport = txtarrivalairport.Text.ToString();
        oTouristDTO.departureairport = txtDepairpot.Text.ToString();
        oTouristDTO.PermanentAddressInIndia = txtPermAdd.Text.ToString();
        oTouristDTO.PlaceofBirth = txtBirthPlace.Text.ToString();
        oTouristDTO.ProposedStayInIndia = txtProStayInIndia.Text.Trim();
        if (txtRoomDetails.Text != "")
        {
            oTouristDTO.RoomDetails = txtRoomDetails.Text.ToString();
        }
        oTouristDTO.SpecialMessage = txtMessage.Text.ToString();

        result = DateTime.MinValue;
        if (txtVisaExpiryDate.Text.Trim() != "")
            DateTime.TryParse(txtVisaExpiryDate.Text, out result);
        if (result != DateTime.MinValue && result != DateTime.MaxValue)
            oTouristDTO.VisaExpiryDate = result;

        oTouristDTO.VisaNo = txtVisaNo.Text.ToString();
        oTouristDTO.VisitPurpose = txtVisitPurpose.Text.ToString();
        return oTouristDTO;
    }
    private void UpdateTouristDetails()
    {
        //if (base.ValidateIfCommandAllowed(Request.Url.AbsoluteUri, ENums.PageCommand.Update))
        //{
        TouristServices touristServices = new TouristServices();
        BookingTouristDTO oTouristDTO = new BookingTouristDTO();
        TouristNo = Convert.ToInt32(Session["gettouristno"].ToString());
        BookingId = Convert.ToInt32(Session["getbookingid"].ToString());
        oTouristDTO = SetDatatoObject();
        touristServices.UpdateBookingTourist(oTouristDTO);

        //}
    }

    protected void GridRoomPaxDetail_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Edit")
        {
            if (Request.QueryString["bid"] != null)
                BookingId = Convert.ToInt32(Request.QueryString["bid"]);
            TouristNo = Convert.ToInt32(e.CommandArgument.ToString());
            Session["gettouristno"] = TouristNo;
            Session["getbookingid"] = BookingId;
            TouristServices touristServices = null;
            touristServices = new TouristServices();
            BookingTouristDTO oBTData = touristServices.GetBookingTouristDetails(BookingId, TouristNo);
            if (oBTData != null)
                FillTouristDetails(oBTData);
            SetDatatoObject();
            btnSubmit.Text = "Update";
            touristServices = null;
            oBTData = null;
        }
    }

    protected void GridRoomPaxDetail_RowEditing(object sender, GridViewEditEventArgs e)
    {

    }
}