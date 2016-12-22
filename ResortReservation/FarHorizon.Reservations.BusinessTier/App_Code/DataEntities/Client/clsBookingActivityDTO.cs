using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessTier.App_Code.DataEntities.Client
{
    public class clsBookingActivityDTO
    {
        int _iBookingId;
        int _iAccomId;
        string _sAccomodation;                
        int _iActivityId;
        string _sActivityName;        
        DateTime _dOperationDate;

        public int BookingId
        {
            get { return _iBookingId; }
            set { _iBookingId = value; }
        }

        public int AccomId
        {
            get { return _iAccomId; }
            set { _iAccomId = value; }
        }

        public string Accomodation
        {
            get { return _sAccomodation; }
            set { _sAccomodation = value; }
        }

        public int ActivityId
        {
            get { return _iActivityId; }
            set { _iActivityId = value; }
        }

        public string Activity
        {
            get { return _sActivityName; }
            set { _sActivityName = value; }
        }

        public DateTime OperationDate
        {
            get { return _dOperationDate; }
            set { _dOperationDate = value; }
        }
    }
}
