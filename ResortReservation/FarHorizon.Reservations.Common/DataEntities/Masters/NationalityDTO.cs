using System;
using System.Collections.Generic;
using System.Text;

namespace FarHorizon.Reservations.Common.DataEntities.Masters
{
    [Serializable]
    public class NationalityDTO 
    {
        private int _iNationalityID;
        private string _sNationality;

        public int NationalityId
        {
            get { return _iNationalityID; }
            set { _iNationalityID = value; }
        }
        public string Nationality
        {
            get { return _sNationality; }
            set { _sNationality = value; }
        }

    }
}
