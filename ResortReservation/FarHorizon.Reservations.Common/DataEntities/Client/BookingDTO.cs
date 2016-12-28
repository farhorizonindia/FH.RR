using System;
using System.Text;

namespace FarHorizon.Reservations.Common.DataEntities.Client
{
    public class BookingDTO
    {
        #region Data Members

        private int _iBookingId;
        private string _sBookingCode;
        private string _sBookingRef;
        private string _sTourID;
        private DateTime _dtStartDate;
        private string _sStartDate;
        private string _sEndDate;
        private DateTime _dtEndDate;
        private int _iAccomTypeId;
        private string _sAccomType;
        private int _iAccomId;
        private string _sAccomName;
        private int _iNoOfNights;
        private int _iNoOfPersons;
        private int _iAgentId;
        private string _sAgentName;
        private int _iBookingStatusId;
        private string _sBookingStatus;
        private int _iSeriesId;

        private string _sExOrderNo;
        private DateTime _dtVoucherDate;

        private DateTime _dtArrivaDT;
        private string _sArrivalTransport;
        private int _iArrivalCityId;
        private string _sArrivalVehicleNo;
        private string _ArrivalVehicleNameType;
        private string _sArrivalTransportCompany;
        private string _ArrivalTransportCompanyPhoneNo;
        private string _ArrivalDriverPhoneNo;

        private DateTime dtDepartureDT;
        private string _sDepartureTransport;
        private int _iDepartureCityId;
        private string _sDepartureVehicleNo;
        private string _DepartureVehicleNameType;
        private string _sDepartureTransportCompany;
        private string _DepartureTransportCompanyPhoneNo;
        private string _DepartureDriverPhoneNo;

        private int _iArrivalTransportId;
        private int _iDepartureTransportId;
        private int _fnCFormNoStart;
        private int _fnCFormNoEnd;
        private int _inCFormNoStart;
        private int _inCFormNoEnd;
        private bool _proposedBooking;
        private string _arrivalcity;
        private string _departurecity;
        private bool _chartered;


        #endregion

        public BookingDTO()
        {
            _sBookingCode = string.Empty;
            _sBookingRef = string.Empty;
            _sTourID = string.Empty;
            _sStartDate = string.Empty;
            _sEndDate = string.Empty;
            _sAccomType = string.Empty;

            _sAccomName = string.Empty;

            _sAgentName = string.Empty;
            _sBookingStatus = string.Empty;
            _sExOrderNo = string.Empty;
            _sArrivalTransport = string.Empty;

            _sArrivalVehicleNo = string.Empty;
            _ArrivalVehicleNameType = string.Empty;
            _sArrivalTransportCompany = string.Empty;
            _ArrivalTransportCompanyPhoneNo = string.Empty;
            _ArrivalDriverPhoneNo = string.Empty;
            _sDepartureTransport = string.Empty;

            _sDepartureVehicleNo = string.Empty;
            _DepartureVehicleNameType = string.Empty;
            _sDepartureTransportCompany = string.Empty;
            _DepartureTransportCompanyPhoneNo = string.Empty;
            _DepartureDriverPhoneNo = string.Empty;
            _arrivalcity = string.Empty;
            _departurecity = string.Empty;
        }

        #region Booking Data Members

        public bool Chartered
        {
            get { return _chartered; }
            set { _chartered = value; }
        }

        public string ArrivalCity
        {
            get
            { return _arrivalcity; }
            set
            { _arrivalcity = value; }
        }

        public string DepartureCity
        {
            get
            { return _departurecity; }
            set
            { _departurecity = value; }
        }

        public int BookingId
        {
            get
            { return _iBookingId; }
            set
            { _iBookingId = value; }
        }

        public string BookingCode
        {
            get
            { return _sBookingCode; }
            set
            { _sBookingCode = value; }
        }

        public string BookingReference
        {
            get { return _sBookingRef; }
            set { _sBookingRef = value; }
        }

        public string TourId
        {
            get
            { return _sTourID; }
            set
            { _sTourID = value; }
        }

        public DateTime StartDate
        {
            get
            {
                return _dtStartDate;
            }
            set
            {
                _dtStartDate = value;
            }
        }

        public DateTime EndDate
        {
            get
            {
                return _dtEndDate;
            }
            set
            {
                _dtEndDate = value;
            }
        }

        public string SDate
        {
            //It is the string representation of the StartDate
            get { return _sStartDate; }
            set { _sStartDate = value; }
        }

        public string EDate
        {
            //It is the string representation of the EndDate
            get { return _sEndDate; }
            set { _sEndDate = value; }
        }

        public int AccomodationTypeId
        {
            get
            {
                return _iAccomTypeId;
            }
            set
            {
                _iAccomTypeId = value;
            }
        }

        public string AccomodationType
        {
            get
            {
                return _sAccomType;
            }
            set
            {
                _sAccomType = value;
            }
        }

        public int AccomodationId
        {
            get
            {
                return _iAccomId;
            }
            set
            {
                _iAccomId = value;
            }
        }

        public string AccomodationName
        {
            get
            {
                return _sAccomName;
            }
            set
            {
                _sAccomName = value;
            }
        }

        public int NoOfNights
        {
            get
            {
                return _iNoOfNights;
            }
            set
            {
                _iNoOfNights = value;
            }
        }

        public int NoOfPersons
        {
            get
            {
                return _iNoOfPersons;
            }
            set
            {
                _iNoOfPersons = value;
            }
        }

        public int AgentId
        {
            get { return _iAgentId; }
            set { _iAgentId = value; }
        }

        public string AgentName
        {
            get { return _sAgentName; }
            set { _sAgentName = value; }
        }

        public int BookingStatusId
        {
            get { return _iBookingStatusId; }
            set { _iBookingStatusId = value; }
        }
        public string BookingStatus
        {
            get { return _sBookingStatus; }
            set { _sBookingStatus = value; }
        }

        public int SeriesId
        {
            get { return _iSeriesId; }
            set { _iSeriesId = value; }
        }

        public string ExchangeOrderNo
        {
            get { return _sExOrderNo; }
            set { _sExOrderNo = value; }
        }
        public DateTime VoucherDate
        {
            get { return _dtVoucherDate; }
            set { _dtVoucherDate = value; }
        }
        public DateTime ArrivalDateTime
        {
            get { return _dtArrivaDT; }
            set { _dtArrivaDT = value; }
        }
        public string ArrivalTransport
        {
            get { return _sArrivalTransport; }
            set { _sArrivalTransport = value; }
        }
        public int ArrivalCityId
        {
            get { return _iArrivalCityId; }
            set { _iArrivalCityId = value; }
        }

        public string ArrivalVehicleNo
        {
            get { return _sArrivalVehicleNo; }
            set { _sArrivalVehicleNo = value; }
        }

        public string ArrivalTransportCompany
        {
            get { return _sArrivalTransportCompany; }
            set { _sArrivalTransportCompany = value; }
        }

        public string ArrivalVehicleNameType
        {
            get { return _ArrivalVehicleNameType; }
            set { _ArrivalVehicleNameType = value; }
        }
        public string ArrivalDriverPhoneNo
        {
            get { return _ArrivalDriverPhoneNo; }
            set { _ArrivalDriverPhoneNo = value; }
        }

        public string ArrivalTransportCompanyPhoneNo
        {
            get { return _ArrivalTransportCompanyPhoneNo; }
            set { _ArrivalTransportCompanyPhoneNo = value; }
        }

        public DateTime DepartureDateTime
        {
            get { return dtDepartureDT; }
            set { dtDepartureDT = value; }
        }
        public string DepartureTransport
        {
            get { return _sDepartureTransport; }
            set { _sDepartureTransport = value; }
        }
        public int DepartureCityId
        {
            get { return _iDepartureCityId; }
            set { _iDepartureCityId = value; }
        }

        public string DepartureVehicleNo
        {
            get { return _sDepartureVehicleNo; }
            set { _sDepartureVehicleNo = value; }
        }

        public string DepartureTransportCompany
        {
            get { return _sDepartureTransportCompany; }
            set { _sDepartureTransportCompany = value; }
        }

        public int ArrivalTransportId
        {
            get { return _iArrivalTransportId; }
            set { _iArrivalTransportId = value; }
        }

        public int DepartureTransportId
        {
            get { return _iDepartureTransportId; }
            set { _iDepartureTransportId = value; }
        }

        public string DepartureVehicleNameType
        {
            get { return _DepartureVehicleNameType; }
            set { _DepartureVehicleNameType = value; }
        }

        public string DepartureTransportCompanyPhoneNo
        {
            get { return _DepartureTransportCompanyPhoneNo; }
            set { _DepartureTransportCompanyPhoneNo = value; }
        }

        public string DepartureDriverPhoneNo
        {
            get { return _DepartureDriverPhoneNo; }
            set { _DepartureDriverPhoneNo = value; }
        }

        public int ForeignNationalCFormNoStart
        {
            get { return _fnCFormNoStart; }
            set { _fnCFormNoStart = value; }
        }

        public int ForeignNationalCFormNoEnd
        {
            get { return _fnCFormNoEnd; }
            set { _fnCFormNoEnd = value; }
        }

        public int IndianNationalCFormNoStart
        {
            get { return _inCFormNoStart; }
            set { _inCFormNoStart = value; }
        }

        public int IndianNationalCFormNoEnd
        {
            get { return _inCFormNoEnd; }
            set { _inCFormNoEnd = value; }
        }
        public bool ProposedBooking
        {
            get { return _proposedBooking; }
            set { _proposedBooking = value; }
        }
        #endregion
    }

    public class ViewBookingDTO
    {
        #region Data Members
        private int _iBookingId;
        private string _sBookingCode;
        private string _sBookingRef;
        private DateTime _dtStartDate;
        private string _sStartDate;
        private string _sEndDate;
        private DateTime _dtEndDate;
        private string _sBookingStatus;
        private string _sAccomType;
        private bool _proposedBooking;
        private bool _hasForeignTourists;
        private bool _hasIndianTourists;
        private int _noofnights;
        private string _agentname;
        private int _sgl;
        private int _twn;
        private int _dbl;
        private int _trp;
        private int _total;
        private int _pax;
        private string _unit;
        private bool _charteredbooking;
        private double _bookingamt;
        private bool _PaymentStatus;
        private double _paidamt;
        private double _invoiceamt;


        #endregion



        #region Booking Data Members

        public double InvoiceAmount
        {
            get { return _invoiceamt; }
            set { _invoiceamt = value; }
        }


        public double PaidAmt
        {
            get { return _paidamt; }
            set { _paidamt = value; }
        }


        public double BookingAmt
        {
            get { return _bookingamt; }
            set { _bookingamt = value; }
        }


        public bool CharteredBooking
        {
            get { return _charteredbooking; }
            set { _charteredbooking = value; }
        }

        public int noofnights
        {
            get { return _noofnights; }
            set { _noofnights = value; }
        }

        public int SGL
        {
            get { return _sgl; }
            set { _sgl = value; }
        }

        public int TWN
        {
            get { return _twn; }
            set { _twn = value; }
        }

        public int TRP
        {
            get { return _trp; }
            set { _trp = value; }
        }

        public int DBL
        {
            get { return _dbl; }
            set { _dbl = value; }
        }

        public int Total
        {
            get { return _total; }
            set { _total = value; }
        }

        public int PAX
        {
            get { return _pax; }
            set { _pax = value; }
        }


        public string unit
        {
            get { return _unit; }
            set { _unit = value; }
        }


        public string agentname
        {
            get { return _agentname; }
            set { _agentname = value; }
        }

        public int BookingId
        {
            get { return _iBookingId; }
            set { _iBookingId = value; }
        }

        public string BookingCode
        {
            get { return _sBookingCode; }
            set { _sBookingCode = value; }
        }

        public string BookingReference
        {
            get { return _sBookingRef; }
            set { _sBookingRef = value; }
        }

        public DateTime StartDate
        {
            get { return _dtStartDate; }
            set { _dtStartDate = value; }
        }

        public DateTime EndDate
        {
            get { return _dtEndDate; }
            set { _dtEndDate = value; }
        }

        public string SDate
        {
            //It is the string representation of the StartDate
            get { return _sStartDate; }
            set { _sStartDate = value; }
        }


        public string EDate
        {
            //It is the string representation of the EndDate
            get { return _sEndDate; }
            set { _sEndDate = value; }
        }

        public string startdateformatted
        {
            get { return string.Format(_sStartDate, "0:dd-MMM-yyyy"); }
        }

        public string enddateformatted
        {
            get { return string.Format(_sEndDate, "0:dd-MMM-yyyy"); }
        }


        public bool PaymentStatus
        {
            get { return _PaymentStatus; }
            set { _PaymentStatus = value; }


        }

        public string AccomodationType
        {
            get { return _sAccomType; }
            set { _sAccomType = value; }
        }

        public string BookingStatus
        {
            get { return _sBookingStatus; }
            set { _sBookingStatus = value; }
        }

        public bool ProposedBooking
        {
            get { return _proposedBooking; }
            set { _proposedBooking = value; }
        }

        public bool HasForeignTourists
        {
            get { return _hasForeignTourists; }
            set { _hasForeignTourists = value; }
        }

        public bool HasIndianTourists
        {
            get { return _hasIndianTourists; }
            set { _hasIndianTourists = value; }
        }
        #endregion
    }
}
