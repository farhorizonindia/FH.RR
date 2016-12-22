using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessTier.App_Code.DataEntities
{
    public class clsAccomTypeData
    {
        int _iAccomTypeId;
        string _sAccomType;
        clsAccomData[] _AccomData;

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

        public clsAccomData[] Accomodation
        {
            get { return _AccomData; }
            set { _AccomData = value; }
        }


    }
}
