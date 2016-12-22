using System;
using System.Collections.Generic;
using System.Text;
using DataTier;

namespace BusinessTier.App_Code.DataEntities.Client
{
    
    public class clsBookingDTO
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
        private string _sArrivalTransportCompany;        

        private DateTime dtDepartureDT;
        private string _sDepartureTransport;
        private int _iDepartureCityId;
        private string _sDepartureVehicleNo;
        private string _sDepartureTransportCompany;

        private int _iArrivalTransportId;
        private int _iDepartureTransportId;        
               
        #endregion

        #region Booking Data Members

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
            { return _sTourID;}
            set
            {_sTourID = value;}
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
        public DateTime ArrivaDateTime
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

#endregion        
    }
}
