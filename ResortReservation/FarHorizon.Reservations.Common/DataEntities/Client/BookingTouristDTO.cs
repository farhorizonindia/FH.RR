using System;
using System.Collections.Generic;
using System.Text;

namespace FarHorizon.Reservations.Common.DataEntities.Client
{
    [Serializable]
    public class BookingTouristDTO
    {
        #region Variables
        private int _iBookingId;
        private string _sBookingCode;
        private int _iTouristNo;
        private string _sFirstName;
        private string _sMiddleName;
        private string _sLastName;
        private char _cGender;
        private int _iNationalityId;
        private string _sNationality;
        private string _sPassportNo;
        private DateTime _dDOB;
        private string _sPlaceofBirth;
        private DateTime _dPPIssueDate;
        private DateTime _dPPExpiryDate;
        private string _sVisaNo;
        private DateTime _dVisaExpiryDate;
        private DateTime _dIndiaEntryDate;
        private string _sProposedStayInIndia;
        private DateTime _dArrivalDateTime;
        private string _sRoomDetails;
        private bool _bEmployedinIndia;
        private string _sVisitPurpose;
        private string _sPermanentAddressInIndia;
        private string _sAllergies;
        private string _sMealPref;
        private string _sSpecialMessage;
        private int _iColumns;
        private string _suffix;
        private int _cFormNo;
        private string _emailid;
        private string _bookingRef;
        private string _agentname;
        private string _clientName;
        private string _accomname;
        private DateTime _checkindate;
        private DateTime _checkoutdate;

        #endregion Variables

        #region Properties

        public DateTime CheckinDate
        {
            get { return _checkindate; }
            set { _checkindate = value; }
        }

        public DateTime CheckoutDate
        {
            get { return _checkoutdate; }
            set { _checkoutdate = value; }
        }

        public string AccomName
        {
            get { return _accomname; }
            set { _accomname = value; }
        }

        public string BookingRef
        {
            get { return _bookingRef; }
            set { _bookingRef = value; }
        }

        public string AgentName
        {
            get { return _agentname; }
            set { _agentname = value; }
        }

        public string ClientName
        {
            get { return string.Format("{0} {1} {2}", FirstName, MiddleName, LastName); }
            set { _clientName = value; }
        }

        public string EmailId
        {
            get { return _emailid; }
            set { _emailid = value; }
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
        public int TouristNo
        {
            get { return _iTouristNo; }
            set { _iTouristNo = value; }
        }
        public string FirstName
        {
            get { return _sFirstName; }
            set { _sFirstName = value; }
        }
        public string MiddleName
        {
            get { return _sMiddleName; }
            set { _sMiddleName = value; }
        }
        public string LastName
        {
            get { return _sLastName; }
            set { _sLastName = value; }
        }
        public char Gender
        {
            get { return _cGender; }
            set { _cGender = value; }
        }
        public int NationalityId
        {
            get { return _iNationalityId; }
            set { _iNationalityId = value; }
        }
        public string Nationality
        {
            get { return _sNationality; }
            set { _sNationality = value; }
        }
        public string PassportNo
        {
            get { return _sPassportNo; }
            set { _sPassportNo = value; }
        }
        public DateTime DateOfBirth
        {
            get { return _dDOB; }
            set { _dDOB = value; }
        }
        public string PlaceofBirth
        {
            get { return _sPlaceofBirth; }
            set { _sPlaceofBirth = value; }
        }
        public DateTime PassportIssueDate
        {
            get { return _dPPIssueDate; }
            set { _dPPIssueDate = value; }
        }
        public DateTime PassportExpiryDate
        {
            get { return _dPPExpiryDate; }
            set { _dPPExpiryDate = value; }
        }
        public string VisaNo
        {
            get { return _sVisaNo; }
            set { _sVisaNo = value; }
        }
        public DateTime VisaExpiryDate
        {
            get { return _dVisaExpiryDate; }
            set { _dVisaExpiryDate = value; }
        }
        public DateTime IndiaEntryDate
        {
            get { return _dIndiaEntryDate; }
            set { _dIndiaEntryDate = value; }
        }
        public string ProposedStayInIndia
        {
            get { return _sProposedStayInIndia; }
            set { _sProposedStayInIndia = value; }
        }
        public DateTime ArrivalDateTime
        {
            get { return _dArrivalDateTime; }
            set { _dArrivalDateTime = value; }
        }

        public string RoomDetails
        {
            get { return _sRoomDetails; }
            set { _sRoomDetails = value; }
        }

        public bool EmployedinIndia
        {
            get { return _bEmployedinIndia; }
            set { _bEmployedinIndia = value; }
        }
        public string VisitPurpose
        {
            get { return _sVisitPurpose; }
            set { _sVisitPurpose = value; }
        }
        public string PermanentAddressInIndia
        {
            get { return _sPermanentAddressInIndia; }
            set { _sPermanentAddressInIndia = value; }
        }
        public string Allergies
        {
            get { return _sAllergies; }
            set { _sAllergies = value; }
        }
        public string MealPreferences
        {
            get { return _sMealPref; }
            set { _sMealPref = value; }
        }
        public string SpecialMessage
        {
            get { return _sSpecialMessage; }
            set { _sSpecialMessage = value; }
        }

        public string Suffix
        {
            get { return _suffix; }
            set { _suffix = value; }
        }

        public int CFormNo
        {
            get { return _cFormNo; }
            set { _cFormNo = value; }
        }
        #endregion Properties
    }

    [Serializable]
    public class clsTouristCountDTO
    {
        private int _iAccomTypeId;
        private string _sAccomType;
        private int _AccomId;
        private string _sAccom;
        private int _bookingId;
        private string _bookingRef;
        private DateTime _bookingStartDate;
        private DateTime _bookingEndDate;
        private int _totalTourist;

        public int AccomodationTypeId
        {
            get { return _iAccomTypeId; }
            set { _iAccomTypeId = value; }
        }
        public string AccomodationType
        {
            get { return _sAccomType; }
            set { _sAccomType = value; }
        }

        public int AccomodationId
        {
            get { return _AccomId; }
            set { _AccomId = value; }
        }

        public string AccomodationName
        {
            get { return _sAccom; }
            set { _sAccom = value; }
        }

        public int BookingId
        {
            get { return _bookingId; }
            set { _bookingId = value; }
        }

        public string BookingReference
        {
            get { return _bookingRef; }
            set { _bookingRef = value; }
        }

        public DateTime BookingStartDate
        {
            get { return _bookingStartDate; }
            set { _bookingStartDate = value; }
        }

        public DateTime BookingEndDate
        {
            get { return _bookingEndDate; }
            set { _bookingEndDate = value; }
        }

        public int TotalTourist
        {
            get { return _totalTourist; }
            set { _totalTourist = value; }
        }
    }
}
