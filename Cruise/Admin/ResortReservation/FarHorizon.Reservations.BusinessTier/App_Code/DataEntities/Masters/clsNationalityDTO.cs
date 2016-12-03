using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessTier.App_Code.DataEntities.Masters
{
    public class clsNationalityDTO
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
