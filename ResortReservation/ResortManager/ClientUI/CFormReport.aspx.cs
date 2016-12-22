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
using FarHorizon.Reservations.Common.DataEntities.Client;
using FarHorizon.Reservations.BusinessServices;
using FarHorizon.Reservations.Common;
using FarHorizon.Reservations.MasterServices;
using FarHorizon.Reservations.Bases;
using FarHorizon.Reservations.Bases.BasePages;

public partial class ClientUI_CFormReport : ClientBasePage
{
    int _bookingId;
    string _cFormType = string.Empty;

    public string CFormType
    {
        get { return _cFormType; }
        set { _cFormType = value; }
    }

    public int BookingId
    {
        get { return _bookingId; }
        set { _bookingId = value; }
    }

    protected void Page_Load(object sender, EventArgs e)
    {        
        if (Request.QueryString["bid"] != null)
            BookingId = Convert.ToInt32(Request.QueryString["bid"]);

        if (Request.QueryString["cftype"] != null)
            CFormType = Request.QueryString["cftype"];

        if (string.IsNullOrEmpty(CFormType))
        {
            CFormType = "fn";
        }

        FillHeaders();
        if (BookingId != 0)
        {
            PopulateCFormReport(BookingId);
        }
    }

    private void PopulateCFormReport(int bookingId)
    {
        BookingReportServices bookingReportManager;
        CFormReportDTO cFormReportDto;
        Table tblTouristDetails;
        Table tblGroupDetails;

        #region Get Tourist List
        bookingReportManager = new BookingReportServices();
        if (CFormType.ToUpper() == "FN")
        {
            cFormReportDto = bookingReportManager.GetCFormDataForForeignNationals(bookingId);
        }
        else if (CFormType.ToUpper() == "IN")
        {
            cFormReportDto = bookingReportManager.GetCFormDataForIndianNationals(bookingId);
        }
        else
        {
            cFormReportDto = bookingReportManager.GetCFormData(bookingId);
        }
        #endregion

        #region Validate Tourist List
        if (cFormReportDto.BookingTouristDetails == null || cFormReportDto.BookingTouristDetails.Length == 0)
        {
            string msg = string.Empty;
            if (CFormType.ToUpper() == "FN")
            {
                msg = "Please add foreign national tourists in this booking, before generating the C-Form report.";
            }
            else if (CFormType.ToUpper() == "IN")
            {
                msg = "Please add indian national tourists in this booking, before generating the C-Form report.";
            }
            else
            {
                msg = "Please add tourists in this booking, before generating the C-Form report.";
            }
            base.DisplayAlert(msg);
            return;
        }
        #endregion

        #region Populate HTML Controls
        tblTouristDetails = PopulateTouristDetailsTable(cFormReportDto);
        pnlTouristDetails.Controls.Add(tblTouristDetails);

        tblGroupDetails = PopulateGroupDetailsTable(cFormReportDto);
        pnlGroupDetails.Controls.Add(tblGroupDetails);
        #endregion

        PopulateLabels(cFormReportDto);
    }

    private void PopulateLabels(CFormReportDTO cFormReportDto)
    {
        if (cFormReportDto.BookingDetails != null)
        {
            lblForeignDetails.Text = "Foreign Client's Details: " + cFormReportDto.BookingDetails.AccomodationName;
            lblBookingRefNo.Text = cFormReportDto.CFormNo;
        }
    }

    private Table PopulateGroupDetailsTable(CFormReportDTO cFormReportDto)
    {
        TableHeaderRow hRow;
        Table tblGroupDetails = new Table();
        TableRow dataRow;
        CityMaster cityMaster = null;

        string dateHandle = string.Empty;
        string timeHandle = string.Empty;
        hRow = GetGroupHeaderRow();
        tblGroupDetails.Rows.Add(hRow);
        tblGroupDetails.CssClass = "dataTables";

        if (cFormReportDto.BookingTouristDetails != null && cFormReportDto.BookingTouristDetails.Length > 0)
        {
            if (cFormReportDto.BookingTouristDetails[0].IndiaEntryDate == DateTime.MinValue || cFormReportDto.BookingTouristDetails[0].IndiaEntryDate == DateTime.MaxValue)
            {
                dateHandle = string.Empty;
            }
            else
            {
                dateHandle = GF.GetDD_MM_YYYY(cFormReportDto.BookingTouristDetails[0].IndiaEntryDate);
            }
            dataRow = GetGroupRow("Date of Entry into India", dateHandle);
            tblGroupDetails.Rows.Add(dataRow);

            dataRow = GetGroupRow("Proposed stay in India", cFormReportDto.BookingTouristDetails[0].ProposedStayInIndia);
            tblGroupDetails.Rows.Add(dataRow);
        }

        if (cityMaster == null)
            cityMaster = new CityMaster();
        string city = cityMaster.GetCityName(cFormReportDto.BookingDetails.ArrivalCityId);
        dataRow = GetGroupRow("Arrived From", city);
        tblGroupDetails.Rows.Add(dataRow);

        if (cFormReportDto.BookingDetails.ArrivalDateTime == DateTime.MinValue || cFormReportDto.BookingDetails.ArrivalDateTime == DateTime.MaxValue)
        {
            dateHandle = string.Empty;
            timeHandle = string.Empty;
        }
        else
        {
            dateHandle = GF.GetDD_MMM_YYYY(cFormReportDto.BookingDetails.ArrivalDateTime, false);
            timeHandle = GF.GetHHMM(cFormReportDto.BookingDetails.ArrivalDateTime);
        }
        dataRow = GetGroupRow("Date of Arrival", dateHandle);
        tblGroupDetails.Rows.Add(dataRow);

        dataRow = GetGroupRow("Time of Arrival", timeHandle);
        tblGroupDetails.Rows.Add(dataRow);

        TransportMaster transportMaster = new TransportMaster();
        string transportMode = transportMaster.GetTransportName(cFormReportDto.BookingDetails.ArrivalTransportId);
        dataRow = GetGroupRow("Mode of Transport", transportMode);
        tblGroupDetails.Rows.Add(dataRow);

        //TransportMaster transportMaster = new TransportMaster();
        //string transportMode = transportMaster.GetTransportName(cFormReportDto.BookingDetails.);
        dataRow = GetGroupRow("Vehicle Name/Type", cFormReportDto.BookingDetails.ArrivalVehicleNameType);
        tblGroupDetails.Rows.Add(dataRow);

        dataRow = GetGroupRow("Vehicle No.", cFormReportDto.BookingDetails.ArrivalVehicleNo);
        tblGroupDetails.Rows.Add(dataRow);

        //dataRow = GetGroupRow("Drivers Name", string.Empty);
        //tblGroupDetails.Rows.Add(dataRow);

        dataRow = GetGroupRow("Drivers No.", cFormReportDto.BookingDetails.ArrivalDriverPhoneNo);
        tblGroupDetails.Rows.Add(dataRow);

        dataRow = GetGroupRow("Transport Company", cFormReportDto.BookingDetails.ArrivalTransportCompany);
        tblGroupDetails.Rows.Add(dataRow);

        dataRow = GetGroupRow("Transport Phone No.", cFormReportDto.BookingDetails.ArrivalTransportCompanyPhoneNo);
        tblGroupDetails.Rows.Add(dataRow);
        if (cityMaster == null)
            cityMaster = new CityMaster();
        city = string.Empty;
        city = cityMaster.GetCityName(cFormReportDto.BookingDetails.DepartureCityId);
        dataRow = GetGroupRow("Next destination", city);
        tblGroupDetails.Rows.Add(dataRow);


        if (cFormReportDto.BookingDetails.DepartureDateTime == DateTime.MinValue || cFormReportDto.BookingDetails.DepartureDateTime == DateTime.MaxValue)
        {
            dateHandle = string.Empty;
            timeHandle = string.Empty;
        }
        else
        {
            dateHandle = GF.GetDD_MMM_YYYY(cFormReportDto.BookingDetails.DepartureDateTime, false);
            timeHandle = GF.GetHHMM(cFormReportDto.BookingDetails.DepartureDateTime);
        }
        dataRow = GetGroupRow("Date of departure", dateHandle);
        tblGroupDetails.Rows.Add(dataRow);

        dataRow = GetGroupRow("Time of Departure", timeHandle);
        tblGroupDetails.Rows.Add(dataRow);

        dataRow = GetGroupRow("Whether Employed in India", "No");
        tblGroupDetails.Rows.Add(dataRow);
        dataRow = GetGroupRow("Purpose of visit", "Tourism");
        tblGroupDetails.Rows.Add(dataRow);
        dataRow = GetGroupRow("Submitted on", string.Empty);
        tblGroupDetails.Rows.Add(dataRow);
        dataRow = GetGroupRow("Time", string.Empty);
        tblGroupDetails.Rows.Add(dataRow);
        dataRow = GetGroupRow("Incharge", string.Empty);
        tblGroupDetails.Rows.Add(dataRow);
        dataRow = GetGroupRow("Company Stamp", string.Empty);
        tblGroupDetails.Rows.Add(dataRow);
        dataRow = GetGroupRow("Received : Police Official", string.Empty);
        tblGroupDetails.Rows.Add(dataRow);
        dataRow = GetGroupRow("Police Stamp", string.Empty);
        tblGroupDetails.Rows.Add(dataRow);
        return tblGroupDetails;
    }

    private TableRow GetGroupRow(string fieldName, string fieldValue)
    {
        TableRow tr = new TableRow();
        TableCell fieldCell = new TableCell();
        TableCell fieldValueCell = new TableCell();

        fieldCell.CssClass = "headerRowCell";
        fieldCell.Text = fieldName;

        fieldValueCell.CssClass = "dataRowCell";
        fieldValueCell.Text = fieldValue == string.Empty ? "&nbsp;" : fieldValue;

        tr.Controls.Add(fieldCell);
        tr.Controls.Add(fieldValueCell);
        return tr;
    }

    private TableHeaderRow GetGroupHeaderRow()
    {
        TableHeaderRow hRow = new TableHeaderRow();
        TableHeaderCell hCell;

        hCell = new TableHeaderCell();
        hCell.CssClass = "groupHeaderRowCell";
        hCell.Text = "Particulars";
        hRow.Cells.Add(hCell);

        hCell = new TableHeaderCell();
        hCell.CssClass = "groupHeaderRowCell";
        hCell.Text = "Details";
        hRow.Cells.Add(hCell);
        return hRow;
    }

    private Table PopulateTouristDetailsTable(CFormReportDTO cFormReportDto)
    {
        TableHeaderRow tableHeaderRow;
        Table tblTouristDetails;
        TableRow touristRow;

        tblTouristDetails = new Table();
        tblTouristDetails.CssClass = "dataTables";
        tableHeaderRow = GetTouristHeaderRow();
        tblTouristDetails.Rows.Add(tableHeaderRow);

        tblTouristDetails.Rows.Add(tableHeaderRow);

        if (cFormReportDto != null)
        {
            if (cFormReportDto.BookingTouristDetails != null && cFormReportDto.BookingTouristDetails.Length > 0)
            {
                for (int i = 0; i < cFormReportDto.BookingTouristDetails.Length; i++)
                {
                    touristRow = GetTouristRow(cFormReportDto.BookingTouristDetails[i]);
                    tblTouristDetails.Rows.Add(touristRow);
                }
            }
        }
        return tblTouristDetails;
    }

    private TableRow GetTouristRow(BookingTouristDTO bookingTouristDTO)
    {
        TableRow touristRow = new TableHeaderRow();
        TableCell touristCell;

        touristCell = new TableCell();
        touristCell.CssClass = "dataRowCell";
        touristCell.Text = bookingTouristDTO.CFormNo.ToString("0000");
        touristRow.Cells.Add(touristCell);

        touristCell = new TableCell();
        touristCell.CssClass = "dataRowCell";
        touristCell.Text = bookingTouristDTO.RoomDetails;
        touristRow.Cells.Add(touristCell);

        touristCell = new TableCell();
        touristCell.CssClass = "dataRowCell";
        touristCell.Text = bookingTouristDTO.RoomDetails;
        touristRow.Cells.Add(touristCell);

        touristCell = new TableCell();
        touristCell.CssClass = "dataRowCell";
        touristCell.Text = bookingTouristDTO.Suffix;
        touristRow.Cells.Add(touristCell);

        touristCell = new TableCell();
        touristCell.CssClass = "dataRowCell";
        touristCell.Text = bookingTouristDTO.FirstName;
        touristRow.Cells.Add(touristCell);

        touristCell = new TableCell();
        touristCell.CssClass = "dataRowCell";
        touristCell.Text = bookingTouristDTO.MiddleName;
        touristRow.Cells.Add(touristCell);

        touristCell = new TableCell();
        touristCell.CssClass = "dataRowCell";
        touristCell.Text = bookingTouristDTO.LastName;
        touristRow.Cells.Add(touristCell);

        touristCell = new TableCell();
        touristCell.CssClass = "dataRowCell";
        touristCell.Text = bookingTouristDTO.Gender.ToString();
        touristRow.Cells.Add(touristCell);

        touristCell = new TableCell();
        touristCell.CssClass = "dataRowCell";
        touristCell.Text = bookingTouristDTO.Nationality;
        touristRow.Cells.Add(touristCell);

        touristCell = new TableCell();
        touristCell.CssClass = "dataRowCell";
        if (bookingTouristDTO.DateOfBirth == DateTime.MinValue || bookingTouristDTO.DateOfBirth == DateTime.MaxValue)
        {
            touristCell.Text = string.Empty;
        }
        else
        {
            touristCell.Text = GF.GetDD_MM_YYYY(bookingTouristDTO.DateOfBirth);
        }
        touristRow.Cells.Add(touristCell);

        touristCell = new TableCell();
        touristCell.CssClass = "dataRowCell";
        touristCell.Text = bookingTouristDTO.PlaceofBirth;
        touristRow.Cells.Add(touristCell);

        touristCell = new TableCell();
        touristCell.CssClass = "dataRowCell";
        touristCell.Text = bookingTouristDTO.PassportNo;
        touristRow.Cells.Add(touristCell);

        touristCell = new TableCell();
        touristCell.CssClass = "dataRowCell";
        if (bookingTouristDTO.PassportIssueDate == DateTime.MinValue || bookingTouristDTO.PassportIssueDate == DateTime.MaxValue)
        {
            touristCell.Text = string.Empty;
        }
        else
        {
            touristCell.Text = GF.GetDD_MM_YYYY(bookingTouristDTO.PassportIssueDate);
        }
        touristRow.Cells.Add(touristCell);

        touristCell = new TableCell();
        touristCell.CssClass = "dataRowCell";
        if (bookingTouristDTO.PassportExpiryDate == DateTime.MinValue || bookingTouristDTO.PassportExpiryDate == DateTime.MaxValue)
        {
            touristCell.Text = string.Empty;
        }
        else
        {
            touristCell.Text = GF.GetDD_MM_YYYY(bookingTouristDTO.PassportExpiryDate);
        }
        touristRow.Cells.Add(touristCell);

        touristCell = new TableCell();
        touristCell.CssClass = "dataRowCell";
        touristCell.Text = bookingTouristDTO.VisaNo;
        touristRow.Cells.Add(touristCell);

        touristCell = new TableCell();
        touristCell.CssClass = "dataRowCell";
        if (bookingTouristDTO.VisaExpiryDate == DateTime.MinValue || bookingTouristDTO.VisaExpiryDate == DateTime.MaxValue)
        {
            touristCell.Text = string.Empty;
        }
        else
        {
            touristCell.Text = GF.GetDD_MM_YYYY(bookingTouristDTO.VisaExpiryDate);
        }
        touristRow.Cells.Add(touristCell);

        return touristRow;
    }

    private TableHeaderRow GetTouristHeaderRow()
    {
        TableHeaderRow hRow = new TableHeaderRow();
        TableHeaderCell hCell;

        hCell = new TableHeaderCell();
        hCell.CssClass = "headerRowCell";
        hCell.Text = "Tourist No";
        hRow.Cells.Add(hCell);

        hCell = new TableHeaderCell();
        hCell.CssClass = "headerRowCell";
        hCell.Text = "Room Type";
        hRow.Cells.Add(hCell);

        hCell = new TableHeaderCell();
        hCell.CssClass = "headerRowCell";
        hCell.Text = "Room No";
        hRow.Cells.Add(hCell);

        hCell = new TableHeaderCell();
        hCell.CssClass = "headerRowCell";
        hCell.Text = "Suffix";
        hRow.Cells.Add(hCell);

        hCell = new TableHeaderCell();
        hCell.CssClass = "headerRowCell";
        hCell.Text = "First<br/>Name";
        hRow.Cells.Add(hCell);

        hCell = new TableHeaderCell();
        hCell.CssClass = "headerRowCell";
        hCell.Text = "Middle<br/>Name";
        hRow.Cells.Add(hCell);

        hCell = new TableHeaderCell();
        hCell.CssClass = "headerRowCell";
        hCell.Text = "Last<br/>Name";
        hRow.Cells.Add(hCell);

        hCell = new TableHeaderCell();
        hCell.CssClass = "headerRowCell";
        hCell.Text = "Gender";
        hRow.Cells.Add(hCell);

        hCell = new TableHeaderCell();
        hCell.CssClass = "headerRowCell";
        hCell.Text = "Nationality";
        hRow.Cells.Add(hCell);

        hCell = new TableHeaderCell();
        hCell.CssClass = "headerRowCell";
        hCell.Text = "Date Of<br/>Birth";
        hRow.Cells.Add(hCell);

        hCell = new TableHeaderCell();
        hCell.CssClass = "headerRowCell";
        hCell.Text = "Place of<br/>Birth";
        hRow.Cells.Add(hCell);

        hCell = new TableHeaderCell();
        hCell.CssClass = "headerRowCell";
        hCell.Text = "Passport<br/>No.";
        hRow.Cells.Add(hCell);

        hCell = new TableHeaderCell();
        hCell.CssClass = "headerRowCell";
        hCell.Text = "Date of Issue<br/>of Passport";
        hRow.Cells.Add(hCell);

        hCell = new TableHeaderCell();
        hCell.CssClass = "headerRowCell";
        hCell.Text = "Date of Expiry<br/>of Passport";
        hRow.Cells.Add(hCell);

        hCell = new TableHeaderCell();
        hCell.CssClass = "headerRowCell";
        hCell.Text = "Visa<br/>Number";
        hRow.Cells.Add(hCell);

        hCell = new TableHeaderCell();
        hCell.CssClass = "headerRowCell";
        hCell.Text = "Date of Expiry<br/>of Visa";
        hRow.Cells.Add(hCell);
        return hRow;
    }

    private void FillHeaders()
    {
        lblHeader1.Text = "Hotel Arrival Performa C -- Rule 14 (In triplicate)";
        lblHeader2.Text = "Registration of foreigners' Rules, 1992";
        lblAddress.Text = "Permanent Address : c/o Far Horizon Tours Pvt. Ltd, 66. L.G.F. Charmwood Plaza, Eros Garden, Near Suraj Kund, Faridabad";
    }
}
