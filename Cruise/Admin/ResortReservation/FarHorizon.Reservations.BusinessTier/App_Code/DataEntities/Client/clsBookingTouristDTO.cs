using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessTier.App_Code.DataEntities.Masters
{
    public class clsBookingTouristDTO
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
        private int _iProposedStayInIndia;
        private DateTime _dArrivalDateTime;
        private string _sArrivedFrom;
        private string _sVehicleNo;
        private string _sTransportCompany;
        private string _sTransportMode;
        private string _sRoomDetails;
        private string _sNextDestination;
        private DateTime _dDepartureDateTime;
        private bool _bEmployedinIndia;
        private string _sVisitPurpose;
        private string _sPermanentAddressInIndia;
        private string _sMealPlan;
        private string _sAllergies;
        private string _sMealPref;
        private string _sSpecialMessage;
        private int _iColumns;


        
#endregion Variables

        #region Properties
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
        public int ProposedStayInIndia
        {
            get { return _iProposedStayInIndia; }
            set { _iProposedStayInIndia = value; }
        }
        public DateTime ArrivalDateTime
        {
            get { return _dArrivalDateTime; }
            set { _dArrivalDateTime = value; }
        }
        public string ArrivedFrom
        {
            get { return _sArrivedFrom; }
            set { _sArrivedFrom = value; }
        }
        public string VehicleNo
        {
            get { return _sVehicleNo; }
            set { _sVehicleNo = value; }
        }
        public string TransportCompany
        {
            get { return _sTransportCompany; }
            set { _sTransportCompany = value; }
        }
        public string TransportMode
        {
            get { return _sTransportMode; }
            set { _sTransportMode = value; }
        }
        public string RoomDetails
        {
            get { return _sRoomDetails; }
            set { _sRoomDetails = value; }
        }
        public string NextDestination
        {
            get { return _sNextDestination; }
            set { _sNextDestination = value; }
        }
        public DateTime DepartureDateTime
        {
            get { return _dDepartureDateTime; }
            set { _dDepartureDateTime = value; }
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
        public string MealPlan
        {
            get { return _sMealPlan; }
            set { _sMealPlan = value; }
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
        
        #endregion Properties
    }
}
