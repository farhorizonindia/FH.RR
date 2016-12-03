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

public partial class ClientUI_touristDetails : ClientBasePage
{
    int _bookingId;
    int _touristNo;

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

    #region Control Functions
    protected void Page_Load(object sender, EventArgs e)
    {        
        TouristServices touristServices = null;
        AddAttributes();

        if (Request.QueryString["bid"] != null)
            BookingId = Convert.ToInt32(Request.QueryString["bid"]);
        if (Request.QueryString["tno"] != null)
            TouristNo = Convert.ToInt32(Request.QueryString["tno"]);

        SessionServices.TouristDetails_BookingNo = BookingId;

        if (SessionServices.TouristDetails_TouristNo > 0)
        {
            TouristNo = SessionServices.TouristDetails_TouristNo;
            touristServices = new TouristServices();
            BookingTouristDTO oBTData = touristServices.GetBookingTouristDetails(BookingId, TouristNo);
            if (oBTData != null)
                FillTouristDetails(oBTData);
            touristServices = null;
            oBTData = null;
            SessionServices.DeleteSession(Constants._TouristDetails_TouristNo);
        }
        if (!IsPostBack)
        {
            FillNationality();
            radEmpNo.Checked = true;
            if (Request.QueryString["op"] == "edit")
            {
                btnSaveTouristDetails.Text = "Update";
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
                btnDelete.Visible = false;
            }
            System.Web.UI.ScriptManager.GetCurrent(this).SetFocus(this.ddlSuffix);
        }
    }

    private void AddAttributes()
    {
        btnSearchTourists.Attributes.Add("onclick", "return LookupConditions()");
        btnSaveTouristDetails.Attributes.Add("onclick", "return ValidateForm");
    }

    protected void btnSaveTouristDetails_Click(object sender, EventArgs e)
    {
        if (ValidateValues() == true)
        {
            if (btnSaveTouristDetails.Text == "Save")
            {
                SaveTouristDetail();
                lblErrorMsg.Text = "Tourist has been added";
            }
            else
            {
                UpdateTouristDetails();
                lblErrorMsg.Text = "Tourist has been updated";
            }
        }
    }
    protected void btnDelete_Click(object sender, EventArgs e)
    {
        DeleteTourist();
    }

    #endregion

    #region UserDefined Functions
    private void FillTouristDetails(BookingTouristDTO oBookingTouristDTO)
    {
        ddlSuffix.Text = oBookingTouristDTO.Suffix;
        txtAllerges.Text = oBookingTouristDTO.Allergies.ToString();
        txtArrivaldate.Text = oBookingTouristDTO.ArrivalDateTime == DateTime.MinValue ? "" : GF.Handle19000101(oBookingTouristDTO.ArrivalDateTime, false);
        txtBirthPlace.Text = oBookingTouristDTO.PlaceofBirth.ToString();
        txtDOB.Text = oBookingTouristDTO.DateOfBirth == DateTime.MinValue ? "" : GF.Handle19000101(oBookingTouristDTO.DateOfBirth, false);
        txtFName.Text = oBookingTouristDTO.FirstName.ToString();
        txtMName.Text = oBookingTouristDTO.MiddleName.ToString();
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
    private void EmptyUncommonFields()
    {
        txtFName.Text = "";
        txtLName.Text = "";
        txtMName.Text = "";
        txtPassportExpiryDate.Text = "";
        txtPassportIssueDate.Text = "";
        txtPassportNo.Text = "";
        txtDOB.Text = "";
        txtBirthPlace.Text = "";
        txtVisaExpiryDate.Text = "";
        txtVisaNo.Text = "";
        txtIndiaEntryDate.Text = "";
        txtVisitPurpose.Text = "";
    }
    private bool ValidateValues()
    {
        int result;
        if (txtFName.Text.Trim() == "")
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

    private void SaveTouristDetail()
    {
        int touristNo;
        if (base.ValidateIfCommandAllowed(Request.Url.AbsoluteUri, ENums.PageCommand.Add))
        {
            TouristServices touristServices = new TouristServices();
            BookingTouristDTO oTouristDTO = new BookingTouristDTO();
            oTouristDTO = SetDatatoObject();
            touristServices.AddBookingTourist(oTouristDTO, out touristNo);
            touristServices = null;
            //EmptyUncommonFields();
            Response.Redirect("afterBookingTouristactions.aspx?bid=" + BookingId + "&tno=" + touristNo + "&tstatus=ins");
        }
    }

    private void UpdateTouristDetails()
    {
        if (base.ValidateIfCommandAllowed(Request.Url.AbsoluteUri, ENums.PageCommand.Update))
        {
            TouristServices touristServices = new TouristServices();
            BookingTouristDTO oTouristDTO = new BookingTouristDTO();
            oTouristDTO = SetDatatoObject();
            touristServices.UpdateBookingTourist(oTouristDTO);
            Response.Redirect("afterBookingTouristactions.aspx?bid=" + BookingId + "&tno=" + TouristNo + "&tstatus=upd");
        }
    }

    private void DeleteTourist()
    {
        if (base.ValidateIfCommandAllowed(Request.Url.AbsoluteUri, ENums.PageCommand.Delete))
        {
            TouristServices touristServices = new TouristServices();
            touristServices.DeleteBookingTourist(BookingId, TouristNo);
            Response.Redirect("afterBookingTouristactions.aspx?bid=" + BookingId + "&tno=" + TouristNo + "&tstatus=del");
        }
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
        oTouristDTO.FirstName = txtFName.Text.ToString();
        oTouristDTO.MiddleName = txtMName.Text.ToString();
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
        if (txtPassportIssueDate.Text.Trim() != "")
            DateTime.TryParse(txtPassportIssueDate.Text, out result);
        if (result != DateTime.MinValue && result != DateTime.MaxValue)
            oTouristDTO.PassportIssueDate = result;

        oTouristDTO.PassportNo = txtPassportNo.Text.ToString();
        oTouristDTO.PermanentAddressInIndia = txtPermAdd.Text.ToString();
        oTouristDTO.PlaceofBirth = txtBirthPlace.Text.ToString();
        oTouristDTO.ProposedStayInIndia = txtProStayInIndia.Text.Trim();
        oTouristDTO.RoomDetails = txtRoomDetails.Text.ToString();
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
    #endregion UserDefined Functions

    protected void btnSearchTourists_Click(object sender, EventArgs e)
    {

    }
}
