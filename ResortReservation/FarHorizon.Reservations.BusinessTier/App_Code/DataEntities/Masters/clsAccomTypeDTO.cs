using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessTier.App_Code.DataEntities.Masters
{
    public class clsAccomTypeDTO
    {
        private int _iAccomTypeId;
        private string _sAccomType;
        private clsAccomodationDTO[] _AccomData;

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

        public clsAccomodationDTO[] Accomodations
        {
            get { return _AccomData; }
            set { _AccomData = value; }
        }
    }
}
